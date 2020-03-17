using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Input
{
    public class InputService : MonoBehaviour, IInputService
    {
        public Action OnPaddleLeftDown { get; set; }
        public Action OnPaddleLeftUp { get; set; }
        public Action OnPaddleRightDown { get; set; }
        public Action OnPaddleRightUp { get; set; }

        private float screen_w;
        private float screen_h;
        public Text text;
        private void Awake()
        {
            screen_w = Screen.width;
        }

        public void OnPaddleUp(Vector2 inputValue)
        {
            text.text = "Up";
            if (inputValue.x <= screen_w / 2)
            {
                OnPaddleLeftDown?.Invoke();
            }
            else
            {
                OnPaddleRightDown?.Invoke();
            }
        }


        public void OnPaddleDown(Vector2 inputValue)
        {
            text.text = "Down";
            var point = inputValue;
            if (point.x <= screen_w / 2)
            {
                OnPaddleLeftUp?.Invoke();
            }
            else
            {
                OnPaddleRightUp?.Invoke();
            }
        }

        private void Update()
        {
            for (int i = 0; i < UnityEngine.Input.touchCount; i++)
            {
                Touch touch = UnityEngine.Input.GetTouch(i);

                if(touch.phase == TouchPhase.Began)
                    OnPaddleUp(touch.position);
                else if(touch.phase == TouchPhase.Ended)
                    OnPaddleDown(touch.position);
            }
        }
    }
}
