using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TurnBasedCombat.Infrastructure
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Combat/InputReader")]
    public class InputReader : ScriptableObject, GameInputControls.IPlayerActions
    {
        private GameInputControls _controls;

        public event Action OnAttackPerformed;
        public event Action<Vector2> OnMovePerformed;

        private void OnEnable()
        {
            if(_controls == null) 
            {
                _controls = new GameInputControls();
                _controls.Player.SetCallbacks(this);
            }

            _controls.Player.Enable();
        }

        private void OnDiaable() 
        {
            _controls.Player.Disable();
        }

        public void OnAttcak(InputAction.CallbackContext context)
        {
            if (context.performed) 
            {
                OnAttackPerformed?.Invoke();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed) 
            {
                OnMovePerformed?.Invoke(context.ReadValue<Vector2>());
            }
        }

        
    }
}
