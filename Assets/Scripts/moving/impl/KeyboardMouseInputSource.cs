using System;
using System.Collections.Generic;
using UnityEngine;

namespace moving.impl
{
    public class KeyboardMouseInputSource : MonoBehaviour, IInputSource
    {
        private readonly Dictionary<InputCode, KeyCode> _inputToKeyCode = new();

        private void Awake()
        {
            _inputToKeyCode[InputCode.MoveLeft] = KeyCode.A;
            _inputToKeyCode[InputCode.MoveRight] = KeyCode.D;
            _inputToKeyCode[InputCode.Jump] = KeyCode.Space;
            _inputToKeyCode[InputCode.Dash] = KeyCode.Mouse1;
        }

        private void Start()
        {
            var inputSourcesHandler = GetComponent<PlayerMovementController>().InputSourcesHandler;
            inputSourcesHandler.RegisterSource(this);
        }

        public bool IsDown(InputCode code)
        {
            var key = InputToKeyCode(code);
            return Input.GetKeyDown(key);
        }

        public bool IsPressed(InputCode code)
        {
            var key = InputToKeyCode(code);
            return Input.GetKey(key);
        }

        public bool IsUp(InputCode code)
        {
            var key = InputToKeyCode(code);
            return Input.GetKeyUp(key);
        }

        public RuntimePlatform[] GetAllowPlatforms()
        {
            return new[]
            {
                RuntimePlatform.WindowsPlayer, RuntimePlatform.LinuxPlayer, RuntimePlatform.OSXPlayer,
                RuntimePlatform.WindowsEditor, RuntimePlatform.LinuxEditor, RuntimePlatform.OSXEditor
            };
        }

        private KeyCode InputToKeyCode(InputCode input)
        {
            if (!_inputToKeyCode.ContainsKey(input))
            {
                throw new ArgumentException("There isn't keycode for: " + input);
            }

            return _inputToKeyCode[input];
        }
    }
}