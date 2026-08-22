using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace Assets.Scripts.Game.Tiles
{
    [CreateAssetMenu(fileName = "TileSetConfig", menuName = "Configs/TileSetConfig")]
    public class TileSetConfig : ScriptableObject
    {
        [SerializeField] private List<TileConfig> _set = new List<TileConfig>();

        public List<TileConfig> Set => _set;
    }
}