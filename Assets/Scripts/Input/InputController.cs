using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH.Data;

namespace BH
{
    public class InputController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField]
        InteractionData interactionData;

        [SerializeField]
        KeybinData keybinData;

        [SerializeField]
        PinAndWireHandler pinAndWireHandler;

        [SerializeField]
        NodeInteraction nodeInteraction;

        [SerializeField]
        private Camera mainCamera;

        [Space]
        [Header("Interaction/Camera Settings")]
        [SerializeField]
        private LayerMask interactableLayer = 0;

        [SerializeField]
        List<string> currentKeyBinds;

        [Space]
        [Header("Debug")]
        [SerializeField]
        private GameObject currentInteractingObject;

        [SerializeField]
        private GameObject clickedInteractedObject;

        [SerializeField]
        private GameObject currentOnFocusObject;

        [SerializeField]
        Vector3 mouseWorldPosition;

        [SerializeField]
        private bool interacting;

        private void Start()
        {
            //foreach (var item in keybinData.PlayerKeyBindsList)
            //{
            //    currentKeyBinds.Add(item.Key);
            //}
        }

        // Update is called once per frame
        void Update()
        {
            mouseWorldPosition = GetWorldMousePostionDown();
            CallMouseRaycast();
            KeyboardBindInteraction();
        }

        #region MouseInteraction

        
        private void CallMouseRaycast()
        {
            if (Input.GetMouseButton(0))
            {
                interactionData.InteractedClicked = true;

                RaycastHit m_hit;
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out m_hit) && interactionData.InteractedClicked)
                {

                    //Debug.Log("[RAYCAST]: Hit an object " + m_hit.transform.name);
                    if (m_hit.transform.gameObject != null)
                    {
                        if (m_hit.transform.gameObject.CompareTag("Node"))
                        {
                            //Debug.Log("[INTERACTION]: Interaction with: " + m_hit.transform.name);
                            currentInteractingObject = m_hit.transform.gameObject;
                            interacting = true;
                            CheckInteractionData();
                        }
                    }
                    else
                    {
                        Debug.Log("[INTERACTION]: Raycast return null!");
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                interactionData.ResetInput();
                interactionData.ResetDataOne();
                interactionData.ResetDataTwo();
                interacting = false;
                currentInteractingObject = null;
            }

            if (Input.GetMouseButtonDown(0))
            {
                interactionData.InteractedClicked = true;

                RaycastHit m_hit;
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out m_hit) && interactionData.InteractedClicked)
                {
                    //Debug.Log("[RAYCAST]: Hit an object " + m_hit.transform.name);
                    if (m_hit.transform.gameObject != null)
                    {
                        if (m_hit.transform.gameObject.CompareTag("Node"))
                        {
                            //Debug.Log("[INTERACTION]: Interaction with: " + m_hit.transform.name);
                            clickedInteractedObject = m_hit.transform.gameObject;
                        }
                        if (m_hit.transform.gameObject.CompareTag("Wire"))
                        {
                            Debug.Log("[INTERACTION]: Interaction with: " + m_hit.transform.name);
                            clickedInteractedObject = m_hit.transform.gameObject;
                        }
                    }
                    else
                    {
                        Debug.Log("[INTERACTION]: Raycast return null!");
                    }
                }

            }

            RaycastHit m_hit2;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out m_hit2) && !interactionData.InteractedClicked)
            {
                if (m_hit2.transform.gameObject != null)
                {
                    if (m_hit2.transform.gameObject.layer == 10)
                    {
                        currentOnFocusObject = m_hit2.transform.gameObject;  
                    }
                }
            }
            else if (currentOnFocusObject)
            {
                currentOnFocusObject = null;
            }
        }

        void CheckInteractionData()
        {
            if (currentInteractingObject != null)
            {
                InteractionBase interactionComponent = currentInteractingObject.GetComponent<InteractionBase>();

                if (interactionData.IsEmptyOne())
                {
                    interactionData.InteractableOne = interactionComponent;
                    ApplyInteractionLogic();
                }
                if (interactionData.IsSameInteractableOne(interactionComponent))
                {
                    ApplyInteractionLogic();
                }
            }
        }

        void ApplyInteractionLogic()
        {
            if (interactionData.InteractableOne != null)
            {
                interactionData.InteractableOne.OnFocus(gameObject);
            }
            if (interactionData.InteractedClicked)
            {
                interacting = true;
            }
            if (interactionData.InteractedReleased)
            {
                interacting = false;
                interactionData.ResetDataOne();
                interactionData.ResetInput();
            }
            if (interacting)
            {
                if (interactionData.InteractableOne.IsInteractble && !interactionData.InteractableOne.HoldInteract)
                {
                    //Debug.Log("[ApplyInteractionLogic]: Calling 'OnInteract' on object in data.");

                    interactionData.OnInteract(gameObject, mouseWorldPosition);
                }

                if (interactionData.InteractableOne.IsInteractble && interactionData.InteractableOne.HoldInteract)
                {
                    //Debug.Log("[ApplyInteractionLogic]: Calling 'OnHold' on object in data.");

                    interactionData.OnHold(gameObject, mouseWorldPosition);
                }
            }
        }
        #endregion

        void KeyboardBindInteraction()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                if (clickedInteractedObject != null)
                {
                    if (clickedInteractedObject.tag == "Node")
                    {
                        pinAndWireHandler.DeleteNodeWires(clickedInteractedObject.GetComponent<Node>());
                        nodeInteraction.DeleteNode(clickedInteractedObject);
                        clickedInteractedObject = null;

                        //Debug.Log("[INTERACTION]: Deleted node wires");
                    }
                    //if (clickedInteractedObject.tag == "Wire")
                    //{
                    //    pinAndWireHandler.DeleteWireAndConnections(clickedInteractedObject.GetComponent<Wire>());
                    //    clickedInteractedObject = null;
                    //}
                }
            }
        }

        RaycastHit GetMouseRayHit()
        {
            RaycastHit m_hit;
            Ray m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(m_ray, out m_hit))
            {
                Debug.Log("[RAYCAST]: Hit an object " + m_hit.transform.name);
                return m_hit;
            }
            else
            {
                return m_hit;
            }
        }

        Vector3 GetWorldMousePostionDown()
        {
            Plane plane = new Plane(Vector3.back, 0f);
            float m_Distance;

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(mouseRay, out m_Distance))
            {
                
                //Vector3 m_newMouseValue = new Vector3(m_raycastHit.transform.position.x, m_raycastHit.transform.position.y, zClampDistance);

                Vector3 m_newMouseValue = mouseRay.GetPoint(m_Distance);
                return m_newMouseValue;
            }
            return Vector3.zero;
        }


    }
}

