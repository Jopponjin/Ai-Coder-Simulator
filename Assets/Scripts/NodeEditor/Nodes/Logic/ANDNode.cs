using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class ANDNode : BaseNode
    {
        public override void ProcessOutput(GameObject m_shipRefrance)
        {
            
            int outputSignal = inputPins[0].State & inputPins[1].State;

            if (outputSignal > 1)
            {
                Debug.Log("[NODE] " + gameObject.name + "Has process signal. " + outputSignal);
                outputPins[0].ReceivePinSignal(outputSignal, m_shipRefrance);
                
            }
        }
    }
}

