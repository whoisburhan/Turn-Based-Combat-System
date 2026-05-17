using TurnBasedCombat.Core;
using UnityEngine;
using Zenject;

namespace TurnBasedCombat.Infrastructure
{
    public class CombatSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAddressableProvider>().To<AddressableProvider>().AsSingle();
        }
    }
}
