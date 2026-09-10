using Assets.Scripts.Animations;
using Assets.Scripts.Audio;
using Assets.Scripts.Boot;
using Assets.Scripts.Data;
using Assets.Scripts.Save;
using Assets.Scripts.SceneLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.DI
{
    public  class RootScope : LifetimeScope
    {
        [SerializeField] private LoadingView _loadingView;
        [SerializeField] private AudioManager _audioManager;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BootEntryPoint>();
            builder.Register<GameData>(Lifetime.Singleton);
            builder.Register<SaveProgress>(Lifetime.Singleton);
            builder.Register<IAsyncSceneLoading, AsyncSceneLoading>(Lifetime.Singleton);
            builder.Register<IAnimation, AnimationManager>(Lifetime.Singleton);
            builder.RegisterInstance(_loadingView);
            builder.RegisterInstance(_audioManager);
        }
    }
}
