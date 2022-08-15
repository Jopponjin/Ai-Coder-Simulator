using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH;

public class CameraControllerEditor : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] Camera playerCamera = default;
    [SerializeField] Canvas EditorUiCanvas = default;

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
    [SerializeField] float zoomSpeed = 10f;

    [SerializeField] float editorScaleMax = -10f;

    [SerializeField] float editorScaleMin = -500f;

    [SerializeField]
    float scrollInput;

    [Header("Mouse Drag")]
    [SerializeField] float mousedragSens = 2;

    [SerializeField] Vector3 mouseOrigin;

    [Space]
    [Header("Free Camera")]
    [SerializeField] Vector3 cameraLookDir;

    [SerializeField] float mainSpeed = 100.0f; //regular speed

    [SerializeField] float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running

    [SerializeField] float maxShift = 1000.0f; //Maximum speed when holdin gshift

    [SerializeField] int cameraSens = 5; //How sensitive it with mouse

    float camAngleX;
    float camAngleY;

    float maxAngelY;
    float minAngelY;

    Vector3 mouseDragStart;

    Vector3 cameraStartPos;
    Vector3 cameraStartRot;

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
            MouseCameraDrag();
            ScrollZoom();
        }

        if (cameraState == CameraState.Play)
        {
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

    void ScrollZoom()
    {
        scrollInput = Input.GetAxis("MouseScrollWheel") * zoomSpeed;

        //if (scrollInput > 0f || scrollInput < 0f)
        //{
        //    Debug.Log("Scroll input = " + scrollInput);

        //    // Check to see if X is Greater, equal or less to the min or max.
        //    if
        //    (
        //        EditorUiCanvas.transform.localScale.x >= editorScaleMax && EditorUiCanvas.transform.localScale.x <= editorScaleMax ||
        //        EditorUiCanvas.transform.localScale.x >= editorScaleMin && EditorUiCanvas.transform.localScale.x <= editorScaleMin
        //    )
        //    {
        //        Debug.Log("Scroll input detected");

        //        EditorUiCanvas.transform.localScale = new Vector3(scrollInput * 0.1f, scrollInput * 0.1f, 1f);
        //    }
        //}


        if (scrollInput == 1f)
        {
            if (EditorUiCanvas.transform.localScale.x <= editorScaleMax && EditorUiCanvas.transform.localScale.y <= editorScaleMax)
            {
                Debug.Log("Scroll input detected");
                EditorUiCanvas.transform.localScale = new Vector3(EditorUiCanvas.transform.localScale.x - 0.01f, EditorUiCanvas.transform.localScale.y - 0.01f, 1);
            }
        }
        if (scrollInput == -1f)
        {
            if (EditorUiCanvas.transform.localScale.x >= editorScaleMin && EditorUiCanvas.transform.localScale.y >= editorScaleMin)
            {
                Debug.Log("Scroll input detected");
                EditorUiCanvas.transform.localScale = new Vector3(EditorUiCanvas.transform.localScale.x + 0.01f, EditorUiCanvas.transform.localScale.y + 0.01f, 1);
            }
        }
    }

    void MouseCameraDrag()
    {
        if (cameraState == CameraState.Editor)
        {
            if (Input.GetMouseButtonDown(2))
            {
                mouseDragStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
                mouseDragStart = Camera.main.ScreenToWorldPoint(mouseDragStart);
                mouseDragStart.z = transform.position.z;
            }
            if (Input.GetMouseButton(2))
            {
                Vector3 mouseDragDir = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
                mouseDragDir = Camera.main.ScreenToWorldPoint(mouseDragDir);
                mouseDragDir.z = transform.position.z;
                transform.position = transform.position - (mouseDragDir - mouseDragStart);
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

        transform.Translate(cameraHorizontalDir);
        transform.Translate(cameraVertiaclDir, Space.World);
    }

    public void ApplyCameraSettings()
    {
        playerCamera.fieldOfView = cameraFOV;
    }
    
    public void SetCameraState(int m_stateNumber)
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

        

    #region Local Methods

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
    #endregion



}
