using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BH.Data;

namespace BH
{
    public class FieldSensorNode : BaseNode
    {
        [SerializeField] ShipData shipData;

        [SerializeField] SphereCollider sphereTrigger;

        [Space]
        [Header("Debug")]
        [SerializeField] bool colTriggerEnter;

        [SerializeField] GameObject recentGameObjectInTigger;
        [SerializeField] GameObject[] objectsInTrigger;

        public override void ProcessOutput(GameObject m_shipRefrance)
        {
            int outputSignal = inputPins[0].State;

            if (outputSignal >= 1)
            {
                Debug.Log("[NODE]: " + gameObject.name + " has process signal.");

                outputPins[0].ReceivePinSignal(outputSignal, null);
                outputSignal = 0;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != recentGameObjectInTigger)
            {
                recentGameObjectInTigger = other.gameObject;

                for (int i = 0; i < objectsInTrigger.Length; i++)
                {
                    objectsInTrigger[objectsInTrigger.Length] = other.gameObject;
                }
            }
            else
            {
                for (int i = 0; i < objectsInTrigger.Length; i++)
                {
                    if (other.gameObject != objectsInTrigger[i])
                    {
                        objectsInTrigger[objectsInTrigger.Length] = other.gameObject;
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (recentGameObjectInTigger == other.gameObject)
            {
                recentGameObjectInTigger = null;
            }
            else
            {
                for (int i = 0; i < objectsInTrigger.Length; i++)
                {
                    if (other.gameObject == objectsInTrigger[i])
                    {
                        objectsInTrigger[i] = null;
                    }
                }
            }
        }

    }
}

