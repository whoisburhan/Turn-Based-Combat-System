using Cysharp.Threading.Tasks;
using System.Threading;
using TurnBasedCombat.Core;
using UnityEngine;
using Zenject;

namespace TurnBasedCombat.Presentation
{
    public class UnitFactory
    {
        private readonly DiContainer _container;
        private readonly IAddressableProvider _addressableProvider;

        public UnitFactory(DiContainer container, IAddressableProvider addressableProvider) 
        {
            _container = container;
            _addressableProvider = addressableProvider;
        }

        public async UniTask<UnitVisual> CreateUnitAsync(string addressableKey, Vector2Int spawnGridPos, CancellationToken ct) 
        {
            ct.ThrowIfCancellationRequested();
            // 1.Fetch the prefab from the addressable system
            GameObject prefab = await _addressableProvider.LoadAssetAsync<GameObject>(addressableKey, ct);

            Vector3 spawnWorldPos = new Vector3(spawnGridPos.x, spawnGridPos.y, 0);

            // 2. Instantiate via zenject to maintain contextual container rules
            GameObject unitInstance = _container.InstantiatePrefab(prefab, spawnWorldPos, Quaternion.identity, null);

            if(unitInstance.TryGetComponent<UnitVisual>(out var unitVisual)) 
            {
                return unitVisual;
            }

            Debug.LogError($"[UnitFactory] The prefab at address '{addressableKey}' does not contain a UnitVisual component.");
            return null;
        }
    }
}
