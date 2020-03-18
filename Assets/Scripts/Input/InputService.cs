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
        public Action<float> Zoom { get; set; }
        public Action<Vector3> Pan { get; set; }
        private Vector3 touchStart;
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
#if UNITY_EDITOR
            if (UnityEngine.Input.GetKeyDown(KeyCode.A))
            {
                OnPaddleLeftDown?.Invoke();
            }
            if (UnityEngine.Input.GetKeyUp(KeyCode.A))
            {
                OnPaddleLeftUp?.Invoke();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.D))
            {
                OnPaddleRightDown?.Invoke();
            }
            if (UnityEngine.Input.GetKeyUp(KeyCode.D))
            {
                OnPaddleRightUp?.Invoke();
            }
#endif


            for (int i = 0; i < UnityEngine.Input.touchCount; i++)
            {
                Touch touch = UnityEngine.Input.GetTouch(i);

                if(touch.phase == TouchPhase.Began)
                    OnPaddleUp(touch.position);
                else if(touch.phase == TouchPhase.Ended)
                    OnPaddleDown(touch.position);
            }

            if (UnityEngine.Input.touchCount == 2)
            {
                Touch touch1 = UnityEngine.Input.GetTouch(0);
                Touch touch2 = UnityEngine.Input.GetTouch(1);

                Vector2 t1Prev = touch1.position - touch1.deltaPosition;
                Vector2 t2Prev = touch2.position - touch2.deltaPosition;
                float prevMag = (t1Prev - t2Prev).magnitude;
                float current = (touch1.position - touch2.position).magnitude;

                float diff = current - prevMag;
                Zoom?.Invoke(diff);
            }

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                touchStart = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            }

            if (UnityEngine.Input.GetMouseButton(0))
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                Pan?.Invoke(direction);
            }

        }
    }
}
