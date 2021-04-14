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

        public  virtual void ReceiveInputSignal(Pin pin, GameObject senderObject)
        {
            numberOfSignalsRecived++;
            if (numberOfSignalsRecived == inputPins.Length) 
            {
                ProcessOutput();
                numberOfSignalsRecived = 0;
            }
        }

        public virtual void ProcessOutput() { }
    }
}

