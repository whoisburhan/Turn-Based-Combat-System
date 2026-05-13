using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TurnBasedCombat.Core;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TurnBasedCombat.Infrastructure
{
    public class AddressableProvider : IAddressableProvider
    {
        private readonly Dictionary<object, AsyncOperationHandle> _handles = new();

        public async UniTask<T> LoadAssetAsync<T>(AssetReference assetReference, CancellationToken ct) where T : class
        {
            var handle = assetReference.LoadAssetAsync<T>();
            var result = await handle.ToUniTask(cancellationToken: ct);

            _handles[result] = handle;

            return result;
        }

        public async UniTask<T> LoadAssetAsync<T>(string key, CancellationToken ct) where T : class
        {
            var handle = Addressables.LoadAssetAsync<T>(key: key);
            var result = await handle.ToUniTask(cancellationToken: ct);

            _handles[result] = handle;

            return result;
        }

        public void ReleaseAsset(object asset)
        {
            if(_handles.TryGetValue(asset, out var handle))
            {
                Addressables.Release(handle);
                _handles.Remove(asset);
            }
        }
    }
}
