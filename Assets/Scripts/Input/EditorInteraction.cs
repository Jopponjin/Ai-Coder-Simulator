using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BH.Data;
using BH;
using UnityEngine.EventSystems;

namespace BH
{
    public class EditorInteraction : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
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

        //Create a list of Raycast Results
        

        [Space]
        [SerializeField]
        Vector3 mouseWorldPosition;

        [SerializeField]
        private bool holdInteracting;
        [SerializeField]
        private bool clickInteracting;

        [SerializeField] GraphicRaycaster Raycaster;
        PointerEventData pointerEventData;

        [SerializeField] EventSystem eventSystem;
        [SerializeField] RectTransform canvasRect;


        public Transform parentToReturnTo = default;
        public Transform placeHolderParent = default;

        Vector2 nodeToMouseOffset;

        private void Awake()
        {
            editorInput = GetComponent<EditorInput>();
            pinAndWireHandler = GetComponent<PinAndWireHandler>();
            nodeInteraction = GetComponent<NodeInteraction>();
        }

        // Update is called once per frame
        void Update()
        {
            MouseInteractionLogic();
            KeyboardBindInteraction();
        }

        #region MouseInteraction

        
        private void MouseInteractionLogic()
        {

            RaycastHit hitFocus;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hitFocus))
            {

                Debug.Log("Hit something");

                // Apply focused object
                if (hitFocus.transform.gameObject.CompareTag("Node"))
                {
                    currentOnFocusObject = hitFocus.transform.gameObject;
                    Debug.Log("EditorInteraction.cs: Hitting " + currentOnFocusObject);
                }


            }
            else
            {
                currentOnFocusObject = null;
            }
            

            if (editorInput.mouseHoldAction.IsPressed())
            {
                interactionData.InteractedClicked = true;

                //Aplly clicked object
                if (clickInteracting)
                {
                    if (currentOnFocusObject.gameObject.CompareTag("Node"))
                    {
                        if (editorInput.multiSelectAction.WasPressedThisFrame() && !ObjectNotInList(currentOnFocusObject.gameObject))
                        {
                            //Multi select
                            selectedObjects.Add(currentOnFocusObject.gameObject);
                            clickInteracting = true;

                            CheckInteraction();
                        }
                        else
                        {
                            //Singel select
                            selectedObjects.Clear();
                            selectedObjects.Add(currentOnFocusObject.gameObject);
                            clickInteracting = true;

                            CheckInteraction();
                        }

                    }

                    if (currentOnFocusObject.gameObject.CompareTag("Wire"))
                    {
                        if (editorInput.multiSelectAction.WasPressedThisFrame() && !ObjectNotInList(currentOnFocusObject.gameObject))
                        {
                            //Multi select
                            selectedObjects.Add(currentOnFocusObject.gameObject);
                            clickInteracting = true;

                            CheckInteraction();
                        }
                        else
                        {
                            //Singel select
                            selectedObjects.Clear();
                            selectedObjects.Add(currentOnFocusObject.gameObject);
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


            if (editorInput.mouseSelectAction.WasPressedThisFrame())
            {
                interactionData.InteractedClicked = true;

                clickInteracting = false;

                //Apply current object
                if (clickInteracting && currentOnFocusObject.gameObject.CompareTag("Node"))
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


        public void OnPointerDown(PointerEventData eventData)
        {
            holdInteracting = true;
            nodeToMouseOffset = eventData.position - new Vector2(transform.position.x, transform.position.y);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            nodeToMouseOffset = eventData.position - new Vector2(transform.position.x, transform.position.y);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            holdInteracting = false;
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
                Debug.Log("InputController.cs: Redo called!");
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
    }
}

[Serializable]
public class EditorScene
{
    public BaseNode[] nodes = new BaseNode[50];
    public Wire[] wires = new Wire[100];


}


public static class RaycastUtilities
{
    public static bool PointerIsOverUI(Vector2 screenPos)
    {
        var hitObject = UIRaycast(ScreenPosToPointerData(screenPos));
        return hitObject != null && hitObject.layer == LayerMask.NameToLayer("UI");
    }

    public static GameObject UIRaycast(PointerEventData pointerData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count < 1 ? null : results[0].gameObject;
    }

    public static PointerEventData ScreenPosToPointerData(Vector2 screenPos)
       => new(EventSystem.current) { position = screenPos };
}
