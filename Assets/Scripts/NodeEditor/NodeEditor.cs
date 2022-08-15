using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH.Data;

namespace BH
{
    public class NodeEditor : MonoBehaviour
    {
        public NodeDataPack nodeDataPack;
        public List<GameObject> allNodesInEditor;
        [Space]
        public Transform nodeSpawnParent;

        [Header("Debug")]
        [SerializeField]
        bool isLoopingSignal;

        [SerializeField]
        bool isDebugMode;

        private void Awake()
        {
            if (!nodeSpawnParent) nodeSpawnParent = transform.parent;
        }

        public void CreateNode(string nodeName)
        {
            for (int i = 0; i < allNodesInEditor.Count; i++)
            {
                if (allNodesInEditor[i].GetComponent<BaseNode>().name == nodeName)
                {
                     GameObject spawnedNode = Instantiate(allNodesInEditor[i], nodeSpawnParent);
                    spawnedNode.transform.position = new Vector3(nodeSpawnParent.position.x, nodeSpawnParent.position.y, nodeSpawnParent.position.z + -0.1f);

                    //Debug.Log("[EVENTSYS]: Created " + allNodesInEditor[i].gameObject.name +" node!");
                }
            }
        }
    }
}
