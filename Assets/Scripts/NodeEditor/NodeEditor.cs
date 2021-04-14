using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH.Data;

namespace BH
{
    public class NodeEditor : MonoBehaviour
    {
        public AiCoreScript coreScript;
        public NodeDataPack nodeDataPack;

        Timer timer;

        [SerializeField]
        AiCoreData coreData;

        [SerializeField]
        bool isLoopingSignal;

        public List<GameObject> allNodesInEditor;

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

        // Update is called once per frame
        void Update()
        {
            if (isLoopingSignal)
            {
                if (timer.ExpireReset())
                {
                    SendSignalFromAiCore();
                }
            }
        }

        public void SendSignalFromAiCore()
        {
            coreScript.SendSignalToPin();
        }

        public void CreateNode(string nodeName)
        {
            //Debug.Log("[EVENTSYS]: Node editor event called!");
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
