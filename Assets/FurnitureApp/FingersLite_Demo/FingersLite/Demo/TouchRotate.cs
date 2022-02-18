using System;
using UnityEngine;

public class TouchRotate : MonoBehaviour
{
	private Vector2 touchDeltaPosition;

	private Vector3 TempVector;

	private float speed = 30f;

	private Vector2 DeviceRotation;


	private void Start()
	{
		touchDeltaPosition.x = touchDeltaPosition.x / (float)Screen.width;
	}

	private void Update()
	{
        if (Input.touchCount > 1)
        {
            Touch[] touches = Input.touches;
            for (int i = 0; i < touches.Length; i++)
            {
                Touch touch = touches[i];
                if (touch.phase == TouchPhase.Moved)
                {
                    touchDeltaPosition = touch.deltaPosition;
                    DeviceRotation.y = -touchDeltaPosition.x;
                    transform.Rotate(0f, DeviceRotation.y * speed * Time.deltaTime, 0f);
                }
            }
            if (transform.localEulerAngles.z != 0f)
            {
                Vector3 localEulerAngles = transform.localEulerAngles;
                localEulerAngles.z = 0f;
                transform.localEulerAngles = localEulerAngles;
            }

        }

    }
}
