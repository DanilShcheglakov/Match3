using Assets.Scripts.Animations;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Menu.UI
{
    public class MenuAnimations : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _levelButtons = new List<GameObject>();

        private CancellationTokenSource _cts;
        private IAnimation _animation;

        public async UniTask StartAnimation()
        {
            _cts = new CancellationTokenSource();

            foreach (var button in _levelButtons)
            {
                button.SetActive(true);
                await _animation.Reveal(button, 0.2f);
            }
        }

        [Inject] private void Construct(IAnimation animation)
        {
            _animation = animation;
        }
    }
}