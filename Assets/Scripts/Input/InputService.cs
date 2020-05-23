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
        public Action OnNudge { get; set; }
        public Action<Vector3> ShootPlunger { get; set; }
        public Action<Vector3> StartPlunger { get; set; }

        private Vector3 touchStart;
        private float screen_w;
        private float screen_h;

        private static float PLUNGER_MAX_X;
        private static float PLUNGER_MIN_X;
        private static float PLUNGER_MAX_Y;
        private static float PLUNGER_MIN_Y;
        public Text text;

        private void Awake()
        {
            screen_w = Screen.width;
            screen_h = Screen.height;
            InitPlungerArea();
        }

        private void InitPlungerArea()
        {
            PLUNGER_MAX_X = screen_w;
            PLUNGER_MIN_X = screen_w / 3;
            PLUNGER_MAX_Y = screen_h / 3;
            PLUNGER_MIN_Y = 0;
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
            DebugInput();
#endif

            if (UnityEngine.Input.acceleration.sqrMagnitude > 3)
            {
                OnNudge?.Invoke();
            }

            for (int i = 0; i < UnityEngine.Input.touchCount; i++)
            {
                Touch touch = UnityEngine.Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)
                {
                    OnPaddleUp(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    OnPaddleDown(touch.position);
                }
            }

            CameraInput();
            PlungerInput();
            PanningInput();
        }

        private void PanningInput()
        {
            if (UnityEngine.Input.touchCount == 1)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && !IsTouchInPlungerArea(touch.position))
                {
                    touchStart = Camera.main.ScreenToWorldPoint(touch.position);
                }

                if(!IsTouchInPlungerArea(touch.position)){
                    Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(touch.position);
                    Pan?.Invoke(direction);
                }
            }
        }

        private void CameraInput()
        {
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
        }

        private void DebugInput()
        {
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


            // if (UnityEngine.Input.GetKey(KeyCode.Space))
            // {
            //     OnNudge?.Invoke();
            // }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                StartPlunger?.Invoke(new Vector3(0, 1, 0));
            }

            if (UnityEngine.Input.GetKeyUp(KeyCode.Space))
            {
                ShootPlunger?.Invoke(new Vector3(0, -10000, 0));
            }
        }

        private void PlungerInput()
        {
            if (UnityEngine.Input.touchCount == 1)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);
                if (IsTouchInPlungerArea(touch.position))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        StartPlunger.Invoke(touch.position);
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        ShootPlunger.Invoke(touch.position);
                    }
                }
            }
        }

        private bool IsTouchInPlungerArea(Vector3 position)
        {
            return (position.x >= PLUNGER_MIN_X && position.x <= PLUNGER_MAX_X)
                    && (position.y <= PLUNGER_MAX_Y && position.y >= PLUNGER_MIN_Y);
        }
    }
}
