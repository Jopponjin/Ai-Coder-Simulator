using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH.Data;

namespace BH
{
    public class GameManger : MonoBehaviour
    {
        [SerializeField]
        ShipData shipData;

        [SerializeField]
        AiCoreScript aiCoreScript;

        public List<GameObject> AiAgents;

        [Header("Debug values")]
        public bool IsInDebugMode = false;

        [SerializeField] GameObject defualtTestShip;

        Vector3 shipStartPoint;
        Quaternion shipStartRotationPoint;

        private void Awake()
        {
            shipData.ShipGameObject = defualtTestShip;

            shipStartPoint = defualtTestShip.transform.position;
            shipStartRotationPoint = defualtTestShip.transform.rotation;

            if (!shipData.IsShipSet())
            {
                Debug.Log("[GAME-MANGER]: ship is set");

                if (shipData.ShipGameObject != null)
                {
                    Debug.Log("[GAME-MANGER]: ship is not NULL");
                }
            }
        }

        public void UpdateAgentInDebugMode()
        {
            aiCoreScript.SendSignalToPin(shipData.ShipGameObject);
        }

        public void UpdateAgentsInPlayMode()
        {
            //TODO: Eyyy, make an implamention for more than one ship.
            for (int i = 0; i < AiAgents.Count; i++)
            {
                
            }
        }

        public void ResetDebugScene()
        {
            defualtTestShip.transform.position = shipStartPoint;
            defualtTestShip.transform.rotation = shipStartRotationPoint;

            defualtTestShip.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            defualtTestShip.GetComponent<Rigidbody>().velocity = Vector3.zero;
            defualtTestShip.GetComponent<Rigidbody>().Sleep();
        }
    }
}

