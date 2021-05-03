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

        // Start is called before the first frame update
        void Awake()
        {
            //if (allNodesInEditor != null)
            //{
            //    Debug.Log("[NODEEDITOR]: " + allNodesInEditor.Count + " In editor list.");

            //    for (int i = 0; i < allNodesInEditor.Count; i++)
            //    {
            //        if (allNodesInEditor[i] != null)
            //        {
            //            nodeDataPack.ListOfNodes.Add(allNodesInEditor[i]);
            //        }
            //        else
            //        {
            //            Debug.Log("[NODEEDITOR]: " + allNodesInEditor[i].gameObject.name + " is NULL!");
            //        }

            //    }
            //}

        }

        public void SendDebugSignal()
        {
            
        }

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
