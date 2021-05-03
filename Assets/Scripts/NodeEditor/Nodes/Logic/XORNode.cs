using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class XORNode : BaseNode
    {
        public override void ProcessOutput(GameObject m_shipRefrance)
        {
            if (inputPins[0].State != inputPins[1].State)
            {
                Debug.Log("[NODE] " + gameObject.name + "Has process signal.");
                int outputSignal = 1;
                outputPins[0].ReceivePinSignal(outputSignal, m_shipRefrance);
            }
        }
    }
}

