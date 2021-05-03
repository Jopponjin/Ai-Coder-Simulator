using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH;

public class CameraControllerEditor : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField]
    Camera playerCamera = default;

    [SerializeField]
    int cameraFOV = 80;

    [SerializeField] bool isInEditor;

    [SerializeField] bool isInDebug;

    [SerializeField] bool isInPlay;

    [Space]
    [Header("Zoom")]
    [SerializeField] float zoomSpeed = 10f;

    [SerializeField] float minPosZ = -10f;

    [SerializeField] float maxPosZ = -50f;

    [SerializeField]
    float scrollInput;

    [Header("Mouse Drag")]
    [SerializeField] float dragSpeed = 2;

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

    #region Misc Variables

    float elapsedTime;
    float timeLimit = 3f;

    #endregion


    private void Awake()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        camAngleX = Vector3.Angle(Vector3.right, transform.right);
        camAngleY = Vector3.Angle(Vector3.up, transform.up);

    }

    // Update is called once per frame
    void Update()
    {
        if (isInEditor)
        {
            MouseCameraDrag();
            ScrollZoom();
        }
        if (isInPlay)
        {
            FreeCameraMovement();
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timeLimit)
        {
            ApplyCameraSettings();
            elapsedTime = 0;
        }

    }

    void ScrollZoom()
    {
        scrollInput = Input.GetAxis("MouseScrollWheel") * zoomSpeed;

        if (playerCamera.transform.position.z >= maxPosZ & playerCamera.transform.position.z <= minPosZ)
        {
            playerCamera.transform.Translate(new Vector3(0, 0, scrollInput));
        }
        else if (playerCamera.transform.position.z < maxPosZ)
        {
            playerCamera.transform.position =
                new Vector3(transform.position.x, transform.position.y, maxPosZ + 2f);
        }
        else if (playerCamera.transform.position.z > minPosZ)
        {
            playerCamera.transform.position =
                new Vector3(transform.position.x, transform.position.y, minPosZ - 2f);
        }
    }

    void MouseCameraDrag()
    {
        if (Input.GetMouseButtonDown(2))
        {
            //Debug.Log("");
            //dragOrigin = Input.mousePosition;
            //Vector3 mouseScreenPos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);

            //Vector3 move = new Vector3(mouseScreenPos.x * dragSpeed, 0, mouseScreenPos.y * dragSpeed);
            //transform.Translate(move, Space.World);
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

    RaycastHit GetMouseRayHit()
    {
        RaycastHit m_hit;
        Ray m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(m_ray, out m_hit))
        {
            Debug.Log("[RAYCAST]: Hit gameobject: " + m_hit.transform.name);
            return m_hit;
        }
        return m_hit;
    }

    Vector3 GetWorldMousePostionDown()
    {
        RaycastHit m_hit;
        Ray m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(m_ray, out m_hit))
        {
            Debug.Log("[RAYCAST]: Hit point" + m_hit.point);
            return m_hit.point;
        }
        return Vector3.zero;
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)   angle += 360;
        if (angle > 360)    angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

}
