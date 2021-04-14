using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH.Data
{
    [CreateAssetMenu(fileName = "NodeData", menuName = "Node/Node Pack Data")]
    public class NodeDataPack : ScriptableObject
    {
        List<GameObject> listOfNodes;

        public List<GameObject> ListOfNodes
        {
            get => listOfNodes;
            set => listOfNodes = value;
        }
    }
}

