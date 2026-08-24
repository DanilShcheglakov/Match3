using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(RawImage))]
    public class BackGroundScroll : MonoBehaviour
    {
        [SerializeField] private float _scrollSpeed = 0.07f;
        [SerializeField] private float _xDirection = 1f;
        [SerializeField] private float _yDirection = 1f;

        private RawImage _backgroundImage;
        private Vector2 _uvRectSize;

        private async void Awake()
        {
            _backgroundImage = GetComponent<RawImage>();
            _uvRectSize = _backgroundImage.uvRect.size;

            try
            {
                await Scroll();
            }
            catch (OperationCanceledException)
            { }
        }

        private async UniTask Scroll()
        {
            while (destroyCancellationToken.IsCancellationRequested == false)
            {
                _backgroundImage.uvRect = new Rect
                    (_backgroundImage.uvRect.position + new Vector2(_xDirection * _scrollSpeed, _yDirection * _scrollSpeed) *
                    Time.deltaTime, _uvRectSize);

                await UniTask.Yield(PlayerLoopTiming.Update, destroyCancellationToken);
            }
        }

    }
}