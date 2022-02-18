using UnityEngine;
using DigitalRubyShared;

public class ObjectScaleAndRotateManager : MonoBehaviour 
{
    public GameObject SelectedObject;
    private ScaleGestureRecognizer scaleGesture;


    private void Start()
    {
        CreateScaleGesture();
    }

    private void ScaleGestureCallback(GestureRecognizer gesture)
    {
        if(SelectedObject)
        {
            if (gesture.State == GestureRecognizerState.Executing)
            {
                SelectedObject.transform.localScale *= scaleGesture.ScaleMultiplier;
            }
        }
        
    }
    private void CreateScaleGesture()
    {
        scaleGesture = new ScaleGestureRecognizer();
        scaleGesture.StateUpdated += ScaleGestureCallback;
        FingersScript.Instance.AddGesture(scaleGesture);
    }
    
    private void LateUpdate()
    {
        int touchCount = Input.touchCount;
        if (FingersScript.Instance.TreatMousePointerAsFinger && Input.mousePresent)
        {
            touchCount += (Input.GetMouseButton(0) ? 1 : 0);
            touchCount += (Input.GetMouseButton(1) ? 1 : 0);
            touchCount += (Input.GetMouseButton(2) ? 1 : 0);
        }
        string touchIds = string.Empty;
        int gestureTouchCount = 0;
        foreach (GestureRecognizer g in FingersScript.Instance.Gestures)
        {
            gestureTouchCount += g.CurrentTrackedTouches.Count;
        }
        foreach (GestureTouch t in FingersScript.Instance.CurrentTouches)
        {
            touchIds += ":" + t.Id + ":";
        }
    }

   
}
