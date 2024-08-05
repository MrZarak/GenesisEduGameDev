using System.Collections.Generic;
using UnityEngine;

namespace moving.impl
{
    public class UIInputSource : MonoBehaviour, IInputSource
    {
        private readonly Dictionary<InputCode, bool> _isDown = new();
        private readonly Dictionary<InputCode, bool> _isUp = new();
        private readonly Dictionary<InputCode, bool> _isPressed = new();

        private void Start()
        {
            var inputSourcesHandler = GetComponent<PlayerMovementController>().InputSourcesHandler;
            inputSourcesHandler.RegisterSource(this);
        }

        public bool IsDown(InputCode code)
        {
            var isDown = _isDown.ContainsKey(code) && _isDown[code];
            return isDown;
        }

        public bool IsPressed(InputCode code)
        {
            var isPressed = _isPressed.ContainsKey(code) && _isPressed[code];

            return isPressed;
        }

        private void LateUpdate()
        {
            _isDown.Clear();
            _isUp.Clear();
        }

        public bool IsUp(InputCode code)
        {
            var isUp = _isUp.ContainsKey(code) && _isUp[code];
            return isUp;
        }

        public RuntimePlatform[] GetAllowPlatforms()
        {
            return new[]
            {
                RuntimePlatform.Android, RuntimePlatform.IPhonePlayer,
                RuntimePlatform.WindowsEditor, RuntimePlatform.LinuxEditor, RuntimePlatform.OSXEditor
            };
        }

        public void UpdateState(InputCode code, bool up)
        {
            if (!up)
            {
                _isDown[code] = true;
                _isPressed[code] = true;
            }
            else
            {
                _isUp[code] = true;
                _isPressed[code] = false;
            }
        }
    }
}