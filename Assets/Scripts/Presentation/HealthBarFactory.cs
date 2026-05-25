using Cysharp.Threading.Tasks;
using System.Threading;
using TurnBasedCombat.Core;
using UnityEngine;
using Zenject;

namespace TurnBasedCombat.Presentation
{
    public class HealthBarFactory
    {
        private readonly DiContainer _container;
        private readonly IAddressableProvider _addressableProvider;

        public HealthBarFactory(DiContainer container, IAddressableProvider addressableProvider)
        {
            _container = container;
            _addressableProvider = addressableProvider;
        }

        public async UniTask<HealthBarView> CreateHealthBarAsync(string addressableKey, Transform parent, CancellationToken ct) 
        {
            ct.ThrowIfCancellationRequested();
            GameObject prefab = await _addressableProvider.LoadAssetAsync<GameObject>(addressableKey, ct);

            GameObject instance = _container.InstantiatePrefab(prefab, parent);
            
            if(instance.TryGetComponent<HealthBarView>(out var healthBarView))
            {
                return healthBarView;
            }

            throw new MissingComponentException($"[HealthBarFactory] The instantiated prefab '{addressableKey}' does not contain a HealthBarView component.");
        }
    }
}
