using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FireBase.TestsCriptsForDelete
{
    public class TestCrush : MonoBehaviour
    {
        [SerializeField] Button _crushButton;

        private void OnEnable()
        {
            _crushButton.onClick.AddListener(Crush);
        }

        private void Crush()
        {
            throw new Exception("Test Exception");
        }
    }
}