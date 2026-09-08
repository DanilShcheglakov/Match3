using Assets.Scripts.Animations;
using Assets.Scripts.Game.GameStateMachine;
using Assets.Scripts.Game.GridSystem;
using Assets.Scripts.Game.MatchedTiles;
using Assets.Scripts.Game.Tiles;
using Assets.Scripts.Game.Utils;
using Assets.Scripts.Input;
using Assets.Scripts.Levels;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Game.Board
{
    public class GameBoard : MonoBehaviour
    {
        private readonly List<Tile> _tilesToRefill = new List<Tile>();
        private Grid _grid;
        private TilePool _tilePool;    
        private IAnimation _animationManager;
        private MatchFinder _matchFinder;
        private BlankTilesSetup _blankTilesSetup;

        [Inject]
        private void Construct(Grid grid,
            TilePool tilePool, BlankTilesSetup blankTilesSetup, 
            IAnimation animationManager, MatchFinder matchFinder)
        {
            _blankTilesSetup = blankTilesSetup;
            _grid = grid;
            _tilePool = tilePool;
            _animationManager = animationManager;
            _matchFinder = matchFinder;
        }

        public void CreateBoard()
        {
            FillBoard();

            while (_matchFinder.CheckBoardForMatches(_grid))
            {
                Debug.Log("Created Board");
                ClearBoard();
                FillBoard();
            }
            _matchFinder.ClearTilesToRemove();

            RevalTile();
        }

        private void RevalTile()
        {
            foreach (var tile in _tilesToRefill)
            {
                var gameObjectTile = tile.gameObject;
                _animationManager.Reveal(gameObjectTile, 1f);
            }
        }

        private void ClearBoard()
        {
            if (_tilesToRefill == null) return;

            foreach (var tile in _tilesToRefill)
            {
                _grid.Setvalue(tile.transform.position, null);

                tile.gameObject.SetActive(false);
            }
            _tilesToRefill.Clear();            
        }

        private void FillBoard()
        {
            for (int x = 0; x < _grid.Wigth; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_blankTilesSetup.Blanks[x, y])
                    {
                        if (_grid.Getvalue(x, y)) continue;
                        var blankTile = _tilePool.CreateBlankTile(_grid.GridToWorld(x, y), transform);
                        _grid.Setvalue(x, y, blankTile);
                    }
                    else
                    {
                        var tile = _tilePool.GetTile(_grid.GridToWorld(x, y), transform);
                        _grid.Setvalue(x, y, tile);
                        tile.gameObject.SetActive(true);
                        _tilesToRefill.Add(tile);
                    }
                }
            }
        }
    }
}
