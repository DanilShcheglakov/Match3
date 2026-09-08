using Assets.Scripts.Menu;
using Assets.Scripts.Menu.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VContainer;
using VContainer.Unity;
using UnityEngine;
using Assets.Scripts.Menu.UI;
using Assets.Scripts.UI;

namespace Assets.Scripts.DI
{
    internal class MenuScope : LifetimeScope
    {
        [SerializeField] private LevelsSequenceView _levelsSequenceView;
        [SerializeField] private MenuAnimations _menuAnimation;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuEntryPoint>();
            builder.Register<SetupLevelSequence>(Lifetime.Singleton);
            builder.Register<StartGame>(Lifetime.Singleton);
            builder.RegisterInstance(_levelsSequenceView);
            builder.RegisterInstance(_menuAnimation);
        }

    }
}
