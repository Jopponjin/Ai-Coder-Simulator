using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BH.Data;

namespace BH
{
    public class ThrusterNode : BaseNode
    {
        [HideInInspector]
        public AiCoreData coreData;

        public Toggle revert;
        public Toggle inputX;
        public Toggle inputY;
        public Toggle inputZ;

        [Space]
        [SerializeField]
        float force;
        int outputSignal;


        public override void ProcessOutput()
        {
            int outputSignal = inputPins[0].State;

            if (outputSignal >= 1)
            {
                Debug.Log("[NODE] " + gameObject.name + "Has process signal.");
                
                if (revert)
                {
                    if (inputX && !inputY && !inputZ)
                    {
                        coreData.aiAgentObject.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0) * force);
                        outputPins[0].ReceivePinSignal(1, null);
                        Debug.Log("[NODE] " + gameObject.name + "Has process signal.");
                        outputSignal = 0;
                    }
                    if (!inputX && inputY && !inputZ)
                    {
                        coreData.aiAgentObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1, 0) * force);
                        outputPins[0].ReceivePinSignal(1, null);
                        Debug.Log("[NODE] " + gameObject.name + "Has process signal.");
                        outputSignal = 0;
                    }
                    if (inputX && !inputY && inputZ)
                    {
                        coreData.aiAgentObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -1) * force);
                        outputPins[0].ReceivePinSignal(1, null);
                        Debug.Log("[NODE] " + gameObject.name + "Has process signal.");
                        outputSignal = 0;
                    }
                    else if (!revert)
                    {
                        if (inputX && !inputY && !inputZ)
                        {
                            coreData.aiAgentObject.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * force);
                            outputPins[0].ReceivePinSignal(1, null);
                            Debug.Log("[NODE] " + gameObject.name + "Has process signal.");
                            outputSignal = 0;
                        }
                        if (!inputX && inputY && !inputZ)
                        {
                            coreData.aiAgentObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * force);
                            outputPins[0].ReceivePinSignal(1, null);
                            Debug.Log("[NODE] " + gameObject.name + "Has process signal.");
                            outputSignal = 0;
                        }
                        if (inputX && !inputY && inputZ)
                        {
                            coreData.aiAgentObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1) * force);
                            outputPins[0].ReceivePinSignal(1, null);
                            Debug.Log("[NODE] " + gameObject.name + "Has process signal.");
                            outputSignal = 0;
                        }
                    }
                }
            }
        }
    }
}
