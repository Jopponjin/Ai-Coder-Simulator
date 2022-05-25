using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH.Data;
using BH;

namespace BH
{
    public class EditorInteraction : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField]
        EditorInput editorInput;

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

        [Space]
        [Header("Debug")]
        [SerializeField]
        private GameObject currentInteractingObject;

        [SerializeField]
        private GameObject clickedInteractedObject;

        [SerializeField]
        private GameObject currentOnFocusObject;
        [Space]

        public List<GameObject> selectedObjects = new List<GameObject>();
        List<EditorScene> editorHistory = new List<EditorScene>();

        [Space]
        [SerializeField]
        Vector3 mouseWorldPosition;

        [SerializeField]
        private bool holdInteracting;
        [SerializeField]
        private bool clickInteracting;

        private void Awake()
        {
            editorInput = GetComponent<EditorInput>();
            pinAndWireHandler = GetComponent<PinAndWireHandler>();
            nodeInteraction = GetComponent<NodeInteraction>();
        }

        // Update is called once per frame
        void Update()
        {
            mouseWorldPosition = GetWorldMousePostionDown();
            MouseInteractionLogic();
            KeyboardBindInteraction();
        }

        #region MouseInteraction

        
        private void MouseInteractionLogic()
        {
            RaycastHit hitFocus;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hitFocus))
            {
                // Apply focused object
                if (hitFocus.transform.gameObject != null)
                {
                    if (hitFocus.transform.gameObject.layer == 10)
                    {
                        currentOnFocusObject = hitFocus.transform.gameObject;
                    }
                }
            }
            else
            {
                currentOnFocusObject = null;
            }
            

            if (Input.GetMouseButtonDown(0))
            {
                interactionData.InteractedClicked = true;

                //Aplly clicked object
                if (hitFocus.collider)
                {
                    if (hitFocus.transform.gameObject.CompareTag("Node"))
                    {
                        if (editorInput.multiSelectAction.WasPressedThisFrame() && !ObjectNotInList(hitFocus.transform.gameObject))
                        {
                            //Multi select
                            selectedObjects.Add(hitFocus.transform.gameObject);
                            clickInteracting = true;

                            CheckInteraction();
                        }
                        else
                        {
                            //Singel select
                            selectedObjects.Clear();
                            selectedObjects.Add(hitFocus.transform.gameObject);
                            clickInteracting = true;

                            CheckInteraction();
                        }

                    }

                    if (hitFocus.transform.gameObject.CompareTag("Wire"))
                    {
                        if (editorInput.multiSelectAction.WasPressedThisFrame() && !ObjectNotInList(hitFocus.transform.gameObject))
                        {
                            //Multi select
                            selectedObjects.Add(hitFocus.transform.gameObject);
                            clickInteracting = true;

                            CheckInteraction();
                        }
                        else
                        {
                            //Singel select
                            selectedObjects.Clear();
                            selectedObjects.Add(hitFocus.transform.gameObject);
                            clickInteracting = true;

                            CheckInteraction();
                        }
                    }
                }
                else
                {
                    clickedInteractedObject = null;

                    if (!editorInput.multiSelectAction.IsPressed())
                    {
                        selectedObjects.Clear();
                    }
                }
            }


            if (Input.GetMouseButton(0))
            {
                interactionData.InteractedClicked = true;

                clickInteracting = false;

                //Apply current object
                if (currentInteractingObject.CompareTag("Node"))
                {
                    //Dont need to add to list as we do that on click.
                    holdInteracting = true;
                    CheckInteraction();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                interactionData.ResetInput();
                interactionData.ResetDataOne();
                interactionData.ResetDataTwo();

                holdInteracting = false;
                clickInteracting = false;
            }
        }



        void CheckInteraction()
        {
            if (selectedObjects.Count > 0)
            {
                for (int i = 0; i < selectedObjects.Count; i++)
                {
                    ApplyInteractionLogic(selectedObjects[i].GetComponent<InteractionBase>());
                }
            }
        }

        void ApplyInteractionLogic(InteractionBase interactionBase)
        {
            if (clickInteracting)
            {
                Debug.Log("Click interaction");
                interactionBase.OnInteract(gameObject, mouseWorldPosition);
            }
            if (holdInteracting)
            {
                Debug.Log("Hold interaction");
                interactionBase.OnHold(gameObject, mouseWorldPosition);
            }
            
        }

        void RestInteraction()
        {
            selectedObjects.Clear();
        }

        void OldApplyInteractionLogic()
        {
            if (interactionData.InteractableOne != null)
            {
                interactionData.InteractableOne.OnFocus(gameObject);
            }
            if (interactionData.InteractedReleased)
            {
                holdInteracting = false;
                interactionData.ResetDataOne();
                interactionData.ResetInput();
            }
            if (holdInteracting)
            {
                if (interactionData.InteractableOne.IsInteractble && !interactionData.InteractableOne.HoldInteract)
                {
                    AddToEditorHistory();
                    interactionData.OnInteract(gameObject, mouseWorldPosition);
                }

                if (interactionData.InteractableOne.IsInteractble && interactionData.InteractableOne.HoldInteract)
                {
                    interactionData.OnHold(gameObject, mouseWorldPosition);
                }
            }
        }
        #endregion

        void KeyboardBindInteraction()
        {
            if (editorInput.undoAction.WasPressedThisFrame())
            {
                Debug.Log("InputController.cs: Undo called!");
            }
            if (editorInput.redoAction.WasPressedThisFrame())
            {
                Debug.Log("InputController.cs: Redo§§ called!");
            }


            if (editorInput.deleteAction.WasPressedThisFrame())
            {
                if (clickedInteractedObject != null)
                {
                    if (clickedInteractedObject.tag == "Node")
                    {
                        pinAndWireHandler.DeleteNodesWires(clickedInteractedObject.GetComponent<Node>());
                        nodeInteraction.DeleteNode(clickedInteractedObject);
                        clickedInteractedObject = null;
                    }
                    else if (clickedInteractedObject.tag == "Wire")
                    {
                        pinAndWireHandler.DestroyWire(clickedInteractedObject.GetComponent<Wire>());
                        clickedInteractedObject = null;
                    }
                }
            }
            
        }


        bool ObjectNotInList(GameObject interactedObject)
        {
            for (int i = 0; i < selectedObjects.Count; i++)
            {
                if (interactedObject == selectedObjects[i])
                {
                    return true;
                }
            }
            return false;
        }


        void AddToEditorHistory()
        {
            for (int i = 0; i < editorHistory.Count; i++)
            {
                if (!editorHistory[i].nodes[i].isDirty)
                {
                    //EditorScene newScene = 
                }
            }
        
        }

        void RemoveFromEditorHistory()
        {

        }

        Vector3 GetWorldMousePostionDown()
        {
            Plane plane = new Plane(Vector3.back, 0f);
            float m_Distance;

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(mouseRay, out m_Distance))
            {
                Vector3 m_newMouseValue = mouseRay.GetPoint(m_Distance);
                return m_newMouseValue;
            }
            return Vector3.zero;
        }


    }
}

[Serializable]
public class EditorScene
{
    public BaseNode[] nodes = new BaseNode[50];
    public Wire[] wires = new Wire[100];


}
