using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace TurnBasedCombat.Gamplay
{
    public class CombatOrchestrator : IInitializable, IDisposable
    {
        private readonly CommandProcessor _commandProcessor;
        private readonly CancellationTokenSource _cts = new();

        private CombatState _currentState;
        public event Action<CombatState> OnStateChanged;

        public CombatOrchestrator(CommandProcessor commandProcessor) 
        {
            _commandProcessor = commandProcessor;
        }

        public void Initialize()
        {
            RunCombatLoopAsync(_cts.Token).Forget();
        }
        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        private async UniTaskVoid RunCombatLoopAsync(CancellationToken ct) 
        {
            ChangeState(CombatState.Setup);
            await UniTask.Delay(1000, cancellationToken: ct);

            while (!ct.IsCancellationRequested) 
            {
                // 1. Player Planning Phase
                ChangeState(CombatState.PlayerTurn);
                await WaitForPlayerCommandAsync(ct);

                // 2. Execustion Phase (Execute all queued moves/attacks)
                ChangeState(CombatState.CommandExecution);
                await _commandProcessor.ExecuteAllAsync(ct);

                if(CheckWinLoseConditions(out var finalState)) 
                {
                    ChangeState(finalState);
                    break;
                }

                // 3.Enemy Turn Phase
                ChangeState(CombatState.EnemyTurn);
                await ProcessEnemyAIAsync(ct);

                // 4 Execution Phase for Enemy actions
                ChangeState(CombatState.CommandExecution);
                await _commandProcessor.ExecuteAllAsync(ct);

                if(CheckWinLoseConditions(out finalState)) 
                {
                    ChangeState(finalState);
                    break;
                }
            }
        }

        private void ChangeState(CombatState newState) 
        {
            _currentState = newState;
            Debug.Log($"[Orchestrator] State Chnaged to : {_currentState}");
            OnStateChanged?.Invoke(newState);
        }

        private async UniTask WaitForPlayerCommandAsync(CancellationToken ct) 
        {
            // For now, we simulate the player picking an action after a brief delay.
            // In Milestone 6, this will await actual UI/Input click events.
            Debug.Log("[Orchestrator] Waiting for player command...");
            await UniTask.Delay(2000, cancellationToken: ct);
        }

        private async UniTask ProcessEnemyAIAsync(CancellationToken ct) 
        {
            Debug.Log("[Orchestrator] Enemy Thinking...");
            await UniTask.Delay(1500, cancellationToken: ct);
        }

        private bool CheckWinLoseConditions(out CombatState finalState)
        {
            finalState = CombatState.Victory;
            return false;

        }
    }
}
