using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using TurnBasedCombat.Core;
using UnityEngine;

namespace TurnBasedCombat.Gamplay
{
    public class AttackCommand : ICommand
    {
        private readonly UnitModel _attacker;
        private readonly UnitModel _target;
        private readonly int _damage;
        private readonly IUnitAnimationView _attackAnimationView;


        public AttackCommand(UnitModel attacker, UnitModel target, int damage, IUnitAnimationView attackAnimationView)
        {
            _attacker = attacker;
            _target = target;
            _damage = damage;
            _attackAnimationView = attackAnimationView;
        }

        public async UniTask ExecuteAsync(CancellationToken ct)
        {
            if (_attackAnimationView != null) 
            {
                await _attackAnimationView.PlayAnimationAsync("Attack", ct);
            }

            _target.TakeDamage(_damage);
        }
    }
}
