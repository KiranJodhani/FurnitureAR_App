using System.Collections;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
using UnityEngine.Apple.ReplayKit;
#endif
using System;
using UnityEngine.Video;
using System.IO;

public class VideoRecordManager : MonoBehaviour
{
    string RecoringError = "";
    public GameObject StartIcon;
    public GameObject StopIcon;

    private void Awake()
    {
    }

    void Start()
    {
        #if UNITY_IOS
        if(ReplayKit.isRecording)
        {
            StartIcon.SetActive(false);
            StopIcon.SetActive(true);
        }
        else
        {
            StartIcon.SetActive(true);
            StopIcon.SetActive(false);
        }
#endif
    }

    public void StartTheRecording()
    {
#if UNITY_IOS
        if (!ReplayKit.isRecording)
        {

            Start_iOS_Recording();
        }
#endif
        //else
        //{
        //    StopTheRecording();
        //}
    }

    private IEnumerator DelayCallRecord()
    {
        yield return new WaitForSeconds(0.1f);
        StopIcon.SetActive(true);
    }

    void Start_iOS_Recording()
    {
#if UNITY_IOS
        try
        {
            if (!ReplayKit.isRecording)
            {
                print("#### Start_iOS_Recording");
                ReplayKit.StartRecording(true);
                Invoke("CheckiOSRecordingStatus", 0.2f);
            }
        }
        catch (Exception e)
        {
            RecoringError = e.ToString();
        }
#endif

    }

    void CheckiOSRecordingStatus()
    {
#if UNITY_IOS
        print("#### CheckiOSRecordingStatus " + ReplayKit.isRecording);
        if (ReplayKit.isRecording)
        {
            StopIcon.SetActive(true);
            StartIcon.SetActive(false);
        }
#endif
    }

    public void StopTheRecording()
    {
        StopIcon.SetActive(false);
        StartIcon.SetActive(true);
#if UNITY_IOS
        try
        {
            if (ReplayKit.isRecording)
            {
                ReplayKit.StopRecording();
            }
            Open_iOS_Preview();
        }
        catch (Exception e)
        {
            RecoringError = e.ToString();
        }
#endif
    }

    void Open_iOS_Preview()
    {
#if UNITY_IOS
        if (ReplayKit.recordingAvailable)
        {
            ReplayKit.Preview();
        }
        else
        {
            Invoke("Open_iOS_Preview", 0.1f);
        }
#endif
    }

    public void TakeAndShareScreenshot()
    {
        StartCoroutine(TakeSSAndShare());
    }

    private IEnumerator TakeSSAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "AR_Image.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath).SetSubject("AR App").SetText("It was a nice AR Experience!").Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).SetText( "Hello world!" ).SetTarget( "com.whatsapp" ).Share();
    }
}