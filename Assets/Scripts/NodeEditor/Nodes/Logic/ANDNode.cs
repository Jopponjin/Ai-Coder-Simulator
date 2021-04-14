using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class ANDNode : BaseNode
    {
        public override void ProcessOutput()
        {
            int outputSignal = inputPins[0].State & inputPins[1].State;

            if (outputSignal >= 1)
            {
                outputPins[0].ReceivePinSignal(1, null);
                Debug.Log("[NODE] " + gameObject.name + "Has process signal.");
                outputSignal = 0;
            }
        }
    }
}

