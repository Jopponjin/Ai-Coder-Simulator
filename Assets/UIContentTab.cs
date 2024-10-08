using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH.Data;
using UnityEngine.UI;

namespace BH
{
    public class UIContentTab : MonoBehaviour
    {
        [SerializeField]
        NodeEditor nodeEditor;

        [Space]
        [Header("ContetnView")]
        [SerializeField]
        Button nodeButtonPrefab;

        OnCreateNode tempUiNode;

        public void PopulateContentView()
        {
            if (nodeEditor.allNodesInEditor != null)
            {
                for (int i = 0; i < nodeEditor.allNodesInEditor.Count; i++)
                {
                    Button m_tempButton = Instantiate(nodeButtonPrefab, parent : transform);

                    if (m_tempButton != null)
                    {
                        // Set the mock Ui buttons text and name to what is in the list index.
                        m_tempButton.name = nodeEditor.allNodesInEditor[i].GetComponent<BaseNode>().nodeName;
                        m_tempButton.GetComponent<OnCreateNode>().prefabName = nodeEditor.allNodesInEditor[i].GetComponent<BaseNode>().nodeName;
                        m_tempButton.GetComponentInChildren<Text>().text = nodeEditor.allNodesInEditor[i].GetComponent<BaseNode>().nodeName;
                    }
                }
            }
            else
            {
                Debug.Log("[UIContentTab]: List is empty!");
            }
        }
    }
}

