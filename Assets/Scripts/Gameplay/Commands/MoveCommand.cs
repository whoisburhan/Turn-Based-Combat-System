using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using TurnBasedCombat.Core;
using UnityEngine;

namespace TurnBasedCombat.Gamplay
{
    public class MoveCommand : ICommand
    {
        private readonly UnitModel _unit;
        private readonly Vector2Int _targetPosition;
        private readonly IUnitMovementView _movementView;


        public MoveCommand(UnitModel unit, Vector2Int targetPosition, IUnitMovementView movementView)
        {
            _unit = unit;
            _targetPosition = targetPosition;
            _movementView = movementView;
        }

        public async UniTask ExecuteAsync(CancellationToken ct)
        {
            _unit.SetPosition(_targetPosition);

            if (_movementView != null) 
            {
                await _movementView.MoveToPositionAsync(_targetPosition,ct);
            }
        }
    }
}
