using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class MainAppManager : MonoBehaviour
{
    public static MainAppManager Instance;

    public GameObject SpawnedModel;
    //public GameObject[] ModelPrefab;
    //public Text SelectedModelText;
    //public GameObject MainAppPanel;
    public GameObject SearchSurfacePanel;
    public GameObject SelectedModel;
    public ObjectScaleAndRotateManager ObjectScaleAndRotateManager_Instance;


    public Vector3 EditorPosition;
    public Quaternion EditorRotation;

    public Text ButtonText;
    private void Awake()
    {
        if(!FurnitureDataManager.Instance)
        {
            SceneManager.LoadScene("SplashScreen");
        }
        Instance = this;
    }

    void Start()
    {
        int SelectedProduct = FurnitureDataManager.Instance.SelectedProduct;
        int SelectedCategory = FurnitureDataManager.Instance.SelectedCategory;
        SelectedModel = FurnitureDataManager.Instance.FurnitureDataInstance.catagories[SelectedCategory].products[SelectedProduct].product_model;
        //ReloadSession();
#if UNITY_EDITOR
        SpawnSelectedModelEditor(EditorPosition, EditorRotation);
#endif

    }

   
    public void SpawnSelectedModel(Vector3 Pos,Quaternion Rot)
    {
        if (Input.touchCount == 1)
        {
            if (SpawnedModel == null)
            {
                int SelectedProduct = FurnitureDataManager.Instance.SelectedProduct;
                int SelectedCategory = FurnitureDataManager.Instance.SelectedCategory;
                SpawnedModel = Instantiate(SelectedModel, Pos, Rot);
                SpawnedModel.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
            else
            {
                SpawnedModel.transform.position = Pos;
                SpawnedModel.transform.rotation = Rot;
            }

            ObjectScaleAndRotateManager_Instance.SelectedObject = SpawnedModel;
        }

    }

    public void SpawnSelectedModelEditor(Vector3 Pos, Quaternion Rot)
    {
        if (SpawnedModel == null)
        {
            int SelectedProduct = FurnitureDataManager.Instance.SelectedProduct;
            int SelectedCategory = FurnitureDataManager.Instance.SelectedCategory;
            SpawnedModel = Instantiate(SelectedModel, Pos, Rot);
            SpawnedModel.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        else
        {
            SpawnedModel.transform.position = Pos;
        }

        ObjectScaleAndRotateManager_Instance.SelectedObject = SpawnedModel;
    }

    [SerializeField]
    [Tooltip("The ARCameraManager which will produce frame events.")]
    ARCameraManager m_CameraManager;

    public ARCameraManager cameraManager
    {
        get { return m_CameraManager; }
        set
        {
            if (m_CameraManager == value)
                return;

            if (m_CameraManager != null)
                m_CameraManager.frameReceived -= FrameChanged;

            m_CameraManager = value;

            if (m_CameraManager != null & enabled)
                m_CameraManager.frameReceived += FrameChanged;
        }
    }

    [SerializeField]
    ARPlaneManager m_PlaneManager;

    public ARPlaneManager planeManager
    {
        get { return m_PlaneManager; }
        set { m_PlaneManager = value; }
    }


    void OnEnable()
    {
        if (m_CameraManager != null)
        {
            m_CameraManager.frameReceived += FrameChanged;
        }
    }

    void OnDisable()
    {
        if (m_CameraManager != null)
        {
            m_CameraManager.frameReceived -= FrameChanged;
        }
    }

    void FrameChanged(ARCameraFrameEventArgs args)
    {
        if (PlanesFound())
        {
            //MainAppPanel.SetActive(true);
            SearchSurfacePanel.SetActive(false);
        }
        else
        {
            //MainAppPanel.SetActive(false);
            SearchSurfacePanel.SetActive(true);
        }
    }

    bool PlanesFound()
    {
        if (planeManager == null)
            return false;
        return planeManager.trackables.count > 0;
    }


    public void OnClickBackButton()
    {
        MainMenuManager.DoShowHomeScreen = false;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnClickInfoButton()
    {
        Application.OpenURL("https://www.kitchenrama.com");
    }

    public void OnClickRealSizeButton()
    {
        if(SpawnedModel)
        {
            if(ButtonText.text=="Real Size")
            {
                SpawnedModel.transform.localScale = Vector3.one;
                ButtonText.text = "Small Size";
            }
            else
            {
                SpawnedModel.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                ButtonText.text = "Real Size";
            }
            
        }
    }

    public ARSession session;
    public GameObject sessionPrefab;

    public void ReloadSession()
    {
        if (session != null)
        {
            StartCoroutine(DoReload());
        }
    }


    IEnumerator DoReload()
    {
        Destroy(session.gameObject);
        yield return null;

        if (sessionPrefab != null)
        {
            session = Instantiate(sessionPrefab).GetComponent<ARSession>();
            //// Hook the buttons back up
            //resetButton.onClick.AddListener(session.Reset);
            //pauseButton.onClick.AddListener(() => { session.enabled = false; });
            //resumeButton.onClick.AddListener(() => { session.enabled = true; });
        }

    }

}
