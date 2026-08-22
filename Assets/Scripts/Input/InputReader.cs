using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using VContainer;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Assets.Scripts.Input
{
    internal class InputReader : IDisposable
    {
        public event Action Click;

        private InputSystem _inputs;
        private InputAction _positionAction;
        private InputAction _fireAction;

        private bool _isFire;

        public InputReader()
        {
            _inputs = new InputSystem();
            _inputs.Player.Attack.performed += OnClick;
        }

        public void EnableInputs(bool value)
        {
            if (value)
                _inputs.Enable();
            else
                _inputs.Disable();
        }

        public Vector2 Position() => _inputs.Player.Select.ReadValue<Vector2>();

        private void OnClick(InputAction.CallbackContext context) => Click?.Invoke();

        public void Dispose() => _inputs.Player.Attack.performed -= OnClick;
    }
}
