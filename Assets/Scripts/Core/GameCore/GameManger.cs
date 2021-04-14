using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class GameManger : MonoBehaviour
    {
        AiCoreScript aiCoreScript;

        public List<GameObject> AiAgents;


        public void UpdateAllAgents()
        {
            for (int i = 0; i < AiAgents.Count; i++)
            {
                //if (AiAgents[i].gameObject.GetComponent<>())
                //{

                //}
                aiCoreScript.SendSignalToPin();
            }
        }
    }
}

