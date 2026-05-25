using Cysharp.Threading.Tasks;
using System.Threading;
using TurnBasedCombat.Core;
using UnityEngine;
using Zenject;

namespace TurnBasedCombat.Presentation
{
    public class UnitVisualFactory
    {
        private readonly DiContainer _container;
        private readonly IAddressableProvider _addressableProvider;

        public UnitVisualFactory(DiContainer container, IAddressableProvider addressableProvider)
        {
            _container = container;
            _addressableProvider = addressableProvider;
        }

        public async UniTask<UnitVisual> CreateVisualAsync(string addressableKey, Vector2Int spawnGridPos, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            GameObject prefab = await _addressableProvider.LoadAssetAsync<GameObject>(addressableKey, ct);
            Vector3 worldPos = new Vector3(spawnGridPos.x, spawnGridPos.y, 0f);

            GameObject instance = _container.InstantiatePrefab(prefab, worldPos, Quaternion.identity, null);

            if (instance.TryGetComponent<UnitVisual>(out var unitVisual))
            {
                return unitVisual;
            }

            throw new MissingComponentException($"[UnitVisualFactory] The instantiated prefab '{addressableKey}' does not contain a UnitVisual component.");
        }
    }
}
