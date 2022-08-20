using UnityEngine;
using BH;

public class CameraControllerEditor : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] Camera playerCamera = default;
    [SerializeField] GameObject editorUiCanvas = default;

    [SerializeField] int cameraFOV = 90;

    public enum CameraState
    {
        None,
        Editor,
        Debug,
        Stop,
        Play
    }
    public CameraState cameraState;

    [Space]
    [Header("Zoom")]
    [SerializeField] float zoomScalingFactor = 0.01f;

    [SerializeField] float editorScaleMax = 1.5f;

    [SerializeField] float editorScaleMin = 0.5f;

    [SerializeField]
    float scrollInput;

    [Header("Mouse Drag")]
    [SerializeField] float mouseDragSens = 0.5f;

    [SerializeField] Vector3 mouseOrigin;

    [Space]
    [Header("Free Camera")]
    [SerializeField] Vector3 cameraLookDir;

    [SerializeField] float mainSpeed = 100.0f; //regular speed

    [SerializeField] float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running

    [SerializeField] float maxShift = 1000.0f; 

    [SerializeField] int cameraSens = 5; 

    float camAngleX;
    float camAngleY;

    float maxAngelY;
    float minAngelY;

    Vector3 lastPanPosition;
    Vector3 newPanPosition;

    Vector3 cameraStartPos;
    Vector3 cameraStartRot;

    Vector3 editorBounds;

    #region Misc Variables

    float elapsedTime;
    float timeLimit = 3f;

    #endregion


    private void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        cameraState = CameraState.Editor;

        camAngleX = Vector3.Angle(Vector3.right, transform.right);
        camAngleY = Vector3.Angle(Vector3.up, transform.up);

        cameraStartPos = transform.position;
        cameraStartRot = transform.localEulerAngles;
    }


    void Update()
    {
        if (cameraState == CameraState.Editor)
        {
            Camera.main.orthographic = true;

            MouseCameraDrag();
            ScrollZoom();
        }

        if (cameraState == CameraState.Play)
        {
            Camera.main.orthographic = false;

            FreeCameraMovement();
        }

        if (cameraState == CameraState.Debug)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeLimit)
            {
                ApplyCameraSettings();
                elapsedTime = 0;
            }
        }
    }


    #region Editor Navigation

    void ScrollZoom()
    {
        scrollInput = Input.GetAxis("MouseScrollWheel") * zoomScalingFactor;

        if (scrollInput != 0f)
        {
            Debug.LogWarning("scrollInput is NOT 0");


            // Check if scaling is outside the bounderies for X and Y
            if (editorUiCanvas.transform.localScale.x >= editorScaleMax)
            {
                editorUiCanvas.transform.localScale = new Vector3(editorScaleMax, editorUiCanvas.transform.localScale.y, editorUiCanvas.transform.localScale.z);
            }
            else if (editorUiCanvas.transform.localScale.x <= editorScaleMin)
            {
                editorUiCanvas.transform.localScale = new Vector3(editorScaleMin, editorUiCanvas.transform.localScale.y, editorUiCanvas.transform.localScale.z);
            }

            if (editorUiCanvas.transform.localScale.y >= editorScaleMax)
            {
                editorUiCanvas.transform.localScale = new Vector3(editorUiCanvas.transform.localScale.x, editorScaleMax, editorUiCanvas.transform.localScale.z);
            }
            else if (editorUiCanvas.transform.localScale.y <= editorScaleMin)
            {
                editorUiCanvas.transform.localScale = new Vector3(editorUiCanvas.transform.localScale.x, editorScaleMin, editorUiCanvas.transform.localScale.z);
            }


            if (scrollInput > 0f)
            {
                editorUiCanvas.transform.localScale = new Vector3(editorUiCanvas.transform.localScale.x + 0.01f, editorUiCanvas.transform.localScale.y + 0.01f, editorUiCanvas.transform.localScale.z);
            }
            else if (scrollInput < 0)
            {
                editorUiCanvas.transform.localScale = new Vector3(editorUiCanvas.transform.localScale.x - 0.01f, editorUiCanvas.transform.localScale.y - 0.01f, editorUiCanvas.transform.localScale.z);
            }
        }
    }


    void MouseCameraDrag()
    {
        if (cameraState == CameraState.Editor)
        {
            if (Input.GetMouseButtonDown(2))
            {
                lastPanPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(2))
            {
                newPanPosition = Input.mousePosition;

                Vector3 offset = Camera.main.ScreenToViewportPoint(lastPanPosition - newPanPosition);
                Vector3 move = new Vector3(offset.x * (mouseDragSens * 10f), offset.y * (mouseDragSens * 10f), 0f);

                editorUiCanvas.transform.Translate(-move, Space.World);

                lastPanPosition = newPanPosition;
            }
        }
    }


    void FreeCameraMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetMouseButton(1))
        {
            camAngleY = ClampAngle(camAngleY, minAngelY, maxAngelY);

            camAngleX = Input.GetAxis("Mouse X") * cameraSens;
            camAngleY = Input.GetAxis("Mouse Y") * cameraSens;

            cameraLookDir = new Vector3(-camAngleY, camAngleX, 0);

            transform.localEulerAngles += new Vector3(cameraLookDir.x, cameraLookDir.y, 0);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        Vector3 cameraHorizontalDir = new Vector3();
        Vector3 cameraVertiaclDir = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            cameraHorizontalDir += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraHorizontalDir += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraHorizontalDir += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraHorizontalDir += new Vector3(1, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            cameraHorizontalDir = cameraHorizontalDir * shiftAdd;

            cameraHorizontalDir.x = Mathf.Clamp(cameraHorizontalDir.x, -maxShift, maxShift);
            cameraVertiaclDir.y = Mathf.Clamp(cameraHorizontalDir.y, -maxShift, maxShift);
            cameraHorizontalDir.z = Mathf.Clamp(cameraHorizontalDir.z, -maxShift, maxShift);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            cameraVertiaclDir += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            cameraVertiaclDir += new Vector3(0, -1, 0);
        }

        transform.Translate(cameraHorizontalDir, Space.World);
        transform.Translate(cameraVertiaclDir, Space.World);
    }

    #endregion


    #region Local Methods

    void ApplyCameraSettings()
    {
        playerCamera.fieldOfView = cameraFOV;
    }


    void SetCameraState(int m_stateNumber)
    {
        if (m_stateNumber == 1)
        {
            cameraState = CameraState.Editor;
            ApplyStateSettings(CameraState.Editor);
        }
        if (m_stateNumber == 2)
        {
            cameraState = CameraState.Debug;
            ApplyStateSettings(CameraState.Debug);
        }
        if (m_stateNumber == 3)
        {
            cameraState = CameraState.Stop;
            ApplyStateSettings(CameraState.Stop);
        }
        if (m_stateNumber == 4)
        {
            cameraState = CameraState.Play;
            ApplyStateSettings(CameraState.Play);
        }
    }


    void ApplyStateSettings(CameraState m_cameraState)
    {
        if (m_cameraState == CameraState.Stop)
        {
            transform.position = cameraStartPos;
            transform.localEulerAngles = cameraStartRot;
            cameraState = CameraState.Editor;
        }
        else if (m_cameraState == CameraState.Play)
        {

        }
        else if (m_cameraState == CameraState.Editor)
        {

        }
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }


    void DebugLogs()
    {
        Debug.LogWarning("Canvas scale is x: " + editorUiCanvas.transform.localScale.x + ", y: " + editorUiCanvas.transform.localScale.y);
        Debug.LogWarning("Editor canvas min max are: " + editorScaleMax + " , " + editorScaleMin);
    }
    #endregion



}
