using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class NOTNode : BaseNode
    {
        public override void ProcessOutput(GameObject m_shipRefrance)
        {
            Debug.Log("[NODE] " + gameObject.name + "Has process signal.");
            int outputSignal = 1 - inputPins[0].State;
            outputPins[0].ReceivePinSignal(outputSignal, m_shipRefrance);
            outputSignal = 0;
        }
    }
}

