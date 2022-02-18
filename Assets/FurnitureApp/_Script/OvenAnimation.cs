using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenAnimation : MonoBehaviour
{
    private Vector3 currentAngle;
    private bool DoOpen;
    private string AnimationState="CLOSE";
    public void Start()
    {
        currentAngle = transform.eulerAngles;
    }

    public void Update()
    {
        if(AnimationState=="OPEN")
        {
            currentAngle = new Vector3( 0,0, Mathf.LerpAngle(currentAngle.z,90, Time.deltaTime));
            transform.localEulerAngles = currentAngle;
        }
        else
        {
            currentAngle = new Vector3(0, 0, Mathf.LerpAngle(currentAngle.z, 0, Time.deltaTime));
            transform.localEulerAngles = currentAngle;
        }
    }

    private void OnMouseDown()
    {
        if (AnimationState == "OPEN")
        {
            AnimationState = "CLOSE";
        }
        else
        {
            AnimationState = "OPEN";
        }
    }
}
