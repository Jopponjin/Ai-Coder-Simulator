using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour
{
    [SerializeField]
    bool applyCustomInput = false;

    Vector3 pos;
    [SerializeField]
    float zoomSpeed = 50f;

    [SerializeField]
    float minPosZ;

    [SerializeField]
    float maxPosZ;

    [SerializeField]
    LayerMask layerInteraction;

    [SerializeField]
    GameObject interactedObject;


    // Update is called once per frame
    void Update()
    {
        if (applyCustomInput)
        {
            pos = transform.position;
            if (Input.GetMouseButton(2))
            {
                //Debug.Log("[MOUSE]:" + KeyCode.Mouse2);
                Vector3 newCameraPos = -GetWorldMousePostionDown();
                transform.position = newCameraPos;
            }

            if (Camera.main.transform.position.z <= maxPosZ | Camera.main.transform.position.z >= minPosZ)
            {
                float m_zoom = Input.GetAxis("MouseScrollWheel");
                pos.z = m_zoom * zoomSpeed * Time.deltaTime;
                transform.position = pos;
            }    
        }


        if (Input.GetMouseButton(0))
        {
            var hitObj = GetMouseRayHit();
            if (hitObj.transform.gameObject.layer == 8)
            {
                Debug.Log("[RAYCAST]: Interaction with: " + interactedObject.name);
                interactedObject = hitObj.transform.gameObject;
                if (interactedObject.GetComponent<GraphicsInteraction>() != null)
                {

                }
            }

            if (hitObj.transform.gameObject.layer == 6)
            {
                Debug.Log("[RAYCAST]: Interaction with: " + interactedObject.name);
                interactedObject = hitObj.transform.gameObject;
            }
        }
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


}
