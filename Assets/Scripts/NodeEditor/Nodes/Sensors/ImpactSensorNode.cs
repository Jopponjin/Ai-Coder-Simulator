using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH;
using BH.Data;

public class ImpactSensorNode : BaseNode
{
    [SerializeField] ShipData shipData;

    Rigidbody shipRb;

    [SerializeField] Collider shipCollider = default;

    [SerializeField] bool impactDetected;

    private void Awake()
    {
        shipRb = shipData.ShipGameObject.GetComponent<Rigidbody>();
        shipCollider = shipData.ShipGameObject.GetComponent<Collider>();

    }

    public override void ProcessOutput(GameObject m_shipRefrance)
    {
        int outputSignal = inputPins[0].State;

        if (outputSignal >= 1)
        {
            Debug.Log("[NODE]: " + gameObject.name + " has process signal.");
            outputPins[0].ReceivePinSignal(outputSignal, null);
            outputSignal = 0;
        }
        if (shipData.shipContact)
        {
            impactDetected = true;
            outputPins[1].ReceivePinSignal(1, null);
        }
        else if (!shipData.shipContact)
        {
            impactDetected = false;
        }
    }


    

}
