using System.Collections;
using UnityEngine;

namespace Assets.Scripts.SceneLoading
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;

        public void SetActiveScreen(bool value) => _loadingScreen.SetActive(value);
    }
}