using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Game.Tiles;
using UnityEngine;

namespace Assets.Scripts.Game.MatchedTiles
{
    public enum MatchDirection
    {
        Horizontal,
        Vertical,
        LongHorizontal,
        LongVertical,
        Multiply,
        None
    }

    public class MatchFinder
    {
        public MatchFinder()
        {
            TilesToRemove = new List<Tile>();
        }

        public List<Tile> TilesToRemove { get; }

        public MatchResult CurrentMetchResult { get; private set; }
        public bool CheckBoardForMatches(Grid grid)
        {
            var hasMatched = false;
            ClearTilesToRemove();

            for (int x = 0; x < grid.Wigth; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    var tile = grid.Getvalue(x, y);

                    if (tile == null) continue;

                    if (tile.IsInteractable == false && tile.IsMatched) continue;

                    MatchResult matchTiles = FindConnectedTiles(tile, grid);

                    if (matchTiles.ConnectedTiles.Count < 3) continue;
                    CurrentMetchResult = matchTiles;

                    TilesToRemove.AddRange(matchTiles.ConnectedTiles);

                    foreach (var connectedTile in matchTiles.ConnectedTiles)
                        connectedTile.Setmatched(true);

                    hasMatched = true;
                }
            }

            return hasMatched;
        }

        private MatchResult FindConnectedTiles(Tile tile, Grid grid)
        {
            List<Tile> connectedTiles = new List<Tile>();
            connectedTiles.Add(tile);

            var tileGridPosition = grid.WorldToGrid(tile.gameObject.transform.position);

            CheckDirection(tileGridPosition, Vector2Int.right, grid, tile, connectedTiles);
            CheckDirection(tileGridPosition, Vector2Int.left, grid, tile, connectedTiles);

            if (connectedTiles.Count == 3)
                return CheckForMultyMatch(connectedTiles, grid, Vector2Int.right,
                    MatchDirection.Horizontal);

            if (connectedTiles.Count > 3)
                return CheckForMultyMatch(connectedTiles, grid, Vector2Int.right,
                    MatchDirection.LongHorizontal);

            connectedTiles.Clear();
            connectedTiles.Add(tile);
            CheckDirection(tileGridPosition, Vector2Int.up, grid, tile, connectedTiles);
            CheckDirection(tileGridPosition, Vector2Int.down, grid, tile, connectedTiles);

            if (connectedTiles.Count == 3)
                return CheckForMultyMatch(connectedTiles, grid, Vector2Int.up,
                    MatchDirection.Vertical);

            if (connectedTiles.Count > 3)
                return CheckForMultyMatch(connectedTiles, grid, Vector2Int.up,
                    MatchDirection.LongVertical);

            connectedTiles.Clear();
            return new MatchResult(connectedTiles, MatchDirection.None);
        }

        private MatchResult CheckForMultyMatch(List<Tile> connectedTiles, Grid grid,
            Vector2Int direction, MatchDirection matchDirection)
        {
            foreach (Tile tile in connectedTiles)
            {
                var position = tile.transform.position;
                List<Tile> multiconnectedTiles = new List<Tile>();
                CheckDirection(grid.WorldToGrid(position), direction, grid, tile, multiconnectedTiles);
                CheckDirection(grid.WorldToGrid(position), direction * -1, grid, tile, multiconnectedTiles);

                if (multiconnectedTiles.Count <= 2) continue;

                multiconnectedTiles.AddRange(connectedTiles);
                return new MatchResult(connectedTiles, MatchDirection.Multiply);
            }

            return new MatchResult(connectedTiles, MatchDirection.Multiply);
        }

        public void ClearTilesToRemove()
        {
            for (int i = 0; i < TilesToRemove.Count; i++)
                TilesToRemove[i].Setmatched(false);

            TilesToRemove.Clear();
        }

        public void ClearCurrentMatchResult()
        {
            CurrentMetchResult.ConnectedTiles.Clear();
        }

        private void CheckDirection(Vector2Int position, Vector2Int direction,
            Grid grid, Tile tile, List<Tile> connectedTiles)
        {
            int x = position.x + direction.x;
            int y = position.y + direction.y;

            while (grid.IsValidPosition(x, y))
            {
                var neighbourTile = grid.Getvalue(x, y);

                if (neighbourTile == null)
                    continue;

                if (neighbourTile.IsInteractable && neighbourTile.IsMatched == false
                    && neighbourTile.TileConfig == tile.TileConfig)
                {
                    connectedTiles.Add(neighbourTile);
                    x += direction.x;
                    y += direction.y;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
