using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace TexasShootEm
{
    [CreateAssetMenu(menuName = "TexasShootEm/Input Reader", fileName = "InputReader")]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions
    {
        public event UnityAction OnAimEvent;
        public event UnityAction<Vector2> OnDirectionalEvent;
        
        private GameInput _gameInput;
        
        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.Gameplay.SetCallbacks(this);
            }
            
            EnableInput();
        }

        private void OnDisable() => DisableInput();

        public void EnableInput() => _gameInput.Gameplay.Enable();
        public void DisableInput() => _gameInput.Gameplay.Disable();

        public void OnAim(InputAction.CallbackContext context)
        {
            if (context.action.phase == InputActionPhase.Performed)
            {
                OnAimEvent?.Invoke();
            }
        }

        public void OnDirectional(InputAction.CallbackContext context)
        {
            if (context.action.phase == InputActionPhase.Performed)
            {
                OnDirectionalEvent?.Invoke(context.ReadValue<Vector2>());
            }
        }
    }
}
