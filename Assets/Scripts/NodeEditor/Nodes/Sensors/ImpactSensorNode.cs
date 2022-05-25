using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH;
using BH.Data;

public class ImpactSensorNode : BaseNode
{
    [SerializeField] ShipData shipData;

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
            outputPins[1].ReceivePinSignal(1, null);
        }
        
    }
}
