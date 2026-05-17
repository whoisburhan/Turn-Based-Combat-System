using UnityEngine;
using Zenject;

namespace TurnBasedCombat.Infrastructure
{
    public class ProjectInfrastructureInstaller : MonoInstaller
    {
        [SerializeField] private InputReader _inputReader;

        public override void InstallBindings()
        {
            Container.Bind<InputReader>().FromScriptableObject(_inputReader).AsSingle();
        }
    }
}
