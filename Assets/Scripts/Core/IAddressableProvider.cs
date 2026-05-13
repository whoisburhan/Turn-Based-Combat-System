using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.AddressableAssets;

namespace TurnBasedCombat.Core
{
    public interface IAddressableProvider
    {
        UniTask<T> LoadAssetAsync<T>(AssetReference assetReference, CancellationToken ct) where T : class;
        UniTask<T> LoadAssetAsync<T>(string key, CancellationToken ct) where T : class;
        void ReleaseAsset(object asset);
    }
}
