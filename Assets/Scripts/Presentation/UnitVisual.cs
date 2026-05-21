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

        public UniTask PlayAnimationAsync(string triggerName, CancellationToken ct)
        {
            throw new System.NotImplementedException();
        }
    }
}
