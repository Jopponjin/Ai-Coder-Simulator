using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class NodeInteraction : InteractionBase
    {

        public override void OnInteract(GameObject interactingAgent, Vector3 newPosition)
        {
            startPosition = gameObject.transform.position - newPosition;
        }

        public override void OnHold(GameObject interactingAgent, Vector3 newPosition)
        {

            
            Vector3 offsetNormlized = startPosition.normalized;

            transform.position = newPosition - offsetNormlized; 
        }

        public void SpawnNode(Node m_nodePrefab)
        {
            var newNode = Instantiate(m_nodePrefab, gameObject.transform);
            newNode.gameObject.SetActive(true);
        }

        public void DeleteNode(GameObject m_node)
        {
            Destroy(m_node);
            Debug.Log("[NODEINTERACTION]: Node "+ m_node.gameObject.name +" deleted!");
        }
    }
}

