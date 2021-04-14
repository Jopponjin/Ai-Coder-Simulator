using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BH
{
    public class AiCoreScript : MonoBehaviour
    {
        [SerializeField]
        Pin pin;
        
        public void SendSignalToPin()
        {
            pin.ReceivePinSignal(1, null);
        }
    }
}

