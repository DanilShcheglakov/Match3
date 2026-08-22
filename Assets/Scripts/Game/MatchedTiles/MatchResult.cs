using Assets.Scripts.Game.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.MatchedTiles
{
    public class MatchResult
    {
        private List<Tile> _connectedTiles;
        private MatchDirection _matchDirection;

        public MatchResult(List<Tile> connectedTiles, MatchDirection matchDirection)
        {
            _connectedTiles = connectedTiles;
            _matchDirection = matchDirection;
        }

        public List<Tile> ConnectedTiles => _connectedTiles;
        public MatchDirection MatchDirection => _matchDirection;
    }
}
