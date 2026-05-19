using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TurnBasedCombat.Core;
using UnityEngine;

namespace TurnBasedCombat.Gamplay
{
    public class CommandProcessor
    {
        private readonly Queue<ICommand> _commandQueue = new();
        private bool _isProcessing;

        public bool IsProcessing => _isProcessing;

        public void Enqueue(ICommand command) 
        {
            if(command == null) throw new ArgumentNullException(nameof(command));
            _commandQueue.Enqueue(command);
        }

        public async UniTask ExecuteAllAsync(CancellationToken ct) 
        {
            if (_isProcessing) return;
            _isProcessing = true;

            try
            {
                while (_commandQueue.Count > 0) 
                {
                    ct.ThrowIfCancellationRequested();

                    var command = _commandQueue.Dequeue();

                    await command.ExecuteAsync(ct);
                }
            }
            catch 
            {
                Debug.LogError("[CommanProcessor] Execution halted via cancelationToken.");
                _commandQueue.Clear();
                throw;
            }
            finally 
            {
                _isProcessing = false;
            }
        }
    }
}
