using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Tiles
{
    [Serializable]
    public class BlankTile
    {
        [SerializeField] private int _xPosition;
        [SerializeField] private int _yPosition;

        public int XPosition => _xPosition;
        public int YPosition => _yPosition;
    }
}
