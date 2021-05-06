using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class InteractionPin : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField]
        PinAndWireHandler pinAndWireHandler;

        [SerializeField]
        private Camera mainCamera;

        [Header("Debug")]
        [SerializeField]
        private GameObject currentInteractionObject;

        [Space]
        [SerializeField]
        private Pin pin1;

        [SerializeField]
        private Pin pin2;

        [Space]
        [SerializeField]
        private GameObject currentOnFocusObject;

        [SerializeField]
        Vector3 mouseWorldPosition;

        [SerializeField]
        bool interacting;

        bool interactClicked;


        private void Awake()
        {
            pinAndWireHandler = gameObject.GetComponent<PinAndWireHandler>();
        }

        // Update is called once per frame
        void Update()
        {
            CallMouseRaycast();
        }

        private void CallMouseRaycast()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit m_hit;

                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out m_hit) && !interactClicked)
                {
                    if (m_hit.transform.gameObject != null)
                    {
                        //Debug.Log("[INTERACTIONPIN]: Hit an objects");
                        if (m_hit.transform.gameObject.CompareTag("Pin"))
                        {
                            currentInteractionObject = m_hit.transform.gameObject;
                            interacting = true;
                            interactClicked = true;
                            ApplyPinLogic();
                        }
                    }
                }
                if (pin1 && !pin2 || pin2 && !pin1)
                {
                    //Debug.Log("[INTERACTIONPIN]: Calling ApplyPinLogic() and m_hit is null.");
                    ApplyPinLogic();
                }

            }

            if (Input.GetMouseButtonUp(0))
            {
                currentInteractionObject = null;
                interactClicked = false;
                interacting = false;
            }
        }


        void ApplyPinLogic()
        {
            if (interacting)
            {
                if (currentInteractionObject.GetComponentInChildren<Pin>())
                {
                    Pin m_Pin = currentInteractionObject.GetComponentInChildren<Pin>();

                    //Debug.Log(" Found PIN class in:" + m_Pin.transform.gameObject.name);
                    if (m_Pin.pinType == Pin.PinType.NodeOutput)
                    {
                        pin1 = m_Pin;
                        //Debug.Log("Pin 1 location" + pin1.transform.position);
                    }
                    else if(m_Pin.pinType == Pin.PinType.NodeInput)
                    {
                        pin2 = m_Pin;
                        //Debug.Log("Pin 2 location" + pin2.transform.position);
                    }
                    
                }
                if (pin1 && pin2)
                {
                    //Debug.Log("Made connection between: " + pin1.gameObject.name + " And " + pin2.gameObject.name);
                    //Debug.Log("Pin 1 location" + pin1.transform.position);

                    pinAndWireHandler.HandleWirePlacement(pin1, pin2);

                    pin1 = null;
                    pin2 = null;
                }
            }
            else if (!interacting)
            {
                if (pin1 && !pin2 || pin2 && !pin1)
                {
                    Debug.Log("[INTERACTIONPIN]: Calling 'StopPlaceingWire()'");
                    pinAndWireHandler.StopPlaceingWire();

                    pin1 = null;
                    pin2 = null;
                    currentInteractionObject = null;
                }
            }
        }
    }
}

