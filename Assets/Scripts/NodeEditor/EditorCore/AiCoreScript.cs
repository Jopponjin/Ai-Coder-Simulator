using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH.Data;

namespace BH
{
    public class AiCoreScript : MonoBehaviour
    {
        [SerializeField]
        Pin pin;
        
        public void SendSignalToPin(GameObject m_agentGameObject)
        {
            pin.ReceivePinSignal(1, m_agentGameObject);
        }
    }
}

