using Assets.Scripts.Game.Tiles;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ResourcesLoading
{
    public class GameResourcesLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private GameObject _blankPrefab;
        [SerializeField] private TileConfig _blankConfigtConfig;
        [SerializeField] private TileSetConfig _tileSetConfig;

        public GameObject TilePrefab => _tilePrefab;
        public GameObject BlankPrefab => _blankPrefab;
        public TileSetConfig TileSetConfig => _tileSetConfig;
        public TileConfig BlankConfig => _blankConfigtConfig;
    }
}