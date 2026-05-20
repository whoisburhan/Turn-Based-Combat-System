using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace TurnBasedCombat.Core
{
    public interface IUnitMovementView
    {
        UniTask MoveToPositionAsync(Vector2Int targetGridPos, CancellationToken ct);
    }

    public interface IUnitAnimationView 
    {
        UniTask PlayAnimationAsync(string triggerName, CancellationToken ct);
    }
}
