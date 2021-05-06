using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class Node : MonoBehaviour
    {
        public Pin[] inputPins;
        public Pin[] outputPins;

        int numberOfSignalsRecived;

        public virtual void ReceiveInputSignal(Pin pin, GameObject m_shipRefrance)
        {
            numberOfSignalsRecived++;

            if (numberOfSignalsRecived == inputPins.Length) 
            {
                ProcessOutput(m_shipRefrance);
                numberOfSignalsRecived = 0;
            }
        }

        public virtual void ProcessOutput(GameObject m_shipRefrance) { }
    }
}

