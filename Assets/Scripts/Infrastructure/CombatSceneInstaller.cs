using TurnBasedCombat.Core;
using TurnBasedCombat.Gamplay;
using UnityEngine;
using Zenject;

namespace TurnBasedCombat.Infrastructure
{
    public class CombatSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAddressableProvider>().To<AddressableProvider>().AsSingle();

            Container.Bind<CommandProcessor>().AsSingle();

            // Biniding the Orchestrator
            Container.BindInterfacesAndSelfTo<CombatOrchestrator>().AsSingle();
        }
    }
}
