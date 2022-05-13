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

        Timer timer;
        [Header("Debug")]
        [SerializeField]
        bool isLoopingSignal;

        [SerializeField]
        bool isDebugMode;
        

        public void CreateNode(string nodeName)
        {
            for (int i = 0; i < allNodesInEditor.Count; i++)
            {
                if (allNodesInEditor[i].gameObject.name == nodeName)
                {
                    Instantiate(allNodesInEditor[i], transform.root);
                    Debug.Log("[EVENTSYS]: Created " + allNodesInEditor[i].gameObject.name +" node!");
                }
            }
        }
    }
}
