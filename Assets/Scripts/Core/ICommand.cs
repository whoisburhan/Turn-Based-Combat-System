using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace TurnBasedCombat.Core
{
    public interface ICommand
    {
        UniTask ExecuteAsync(CancellationToken ct);
    }
}
