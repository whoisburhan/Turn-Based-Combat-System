using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using TurnBasedCombat.Core;
using UnityEngine;

namespace TurnBasedCombat.Presentation
{
    [RequireComponent(typeof(Animator))]
    public class UnitVisual : MonoBehaviour, IUnitMovementView, IUnitAnimationView
    {
        [SerializeField] private float _moveSpeed = 5f;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        public async UniTask MoveToPositionAsync(Vector2Int targetGridPos, CancellationToken ct)
        {
            Vector3 targetWorldPos = new Vector3(targetGridPos.x, targetGridPos.y, transform.position.z);
            
            _animator.SetBool("IsMoving", true);

            while (Vector3.Distance(transform.position, targetWorldPos) > 0.01f) 
            {
                ct.ThrowIfCancellationRequested();

                transform.position = Vector3.MoveTowards(transform.position, targetWorldPos, _moveSpeed * Time.deltaTime);

                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }

            transform.position = targetWorldPos;
            _animator.SetBool("IsMoving", false);
        }

        public async UniTask PlayAnimationAsync(string triggerName, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            _animator.SetTrigger(triggerName);

            await UniTask.Yield(PlayerLoopTiming.Update, ct); // Wait a frame for the animation to start

            var isInTransition = _animator.IsInTransition(0);

            var stateInfo = isInTransition? _animator.GetNextAnimatorStateInfo(0) :  _animator.GetCurrentAnimatorStateInfo(0);


            float runTimeSpeed = stateInfo.speed > 0 ? stateInfo.speed : 1f; // preventing zero or negative speed which would cause infinite wait
            float globalSpeed = _animator.speed > 0 ? _animator.speed : 1f;

            float clipDuration = stateInfo.length / (runTimeSpeed * globalSpeed);
            await UniTask.Delay((int)(clipDuration * 1000), cancellationToken:ct );


        }
    }
}
