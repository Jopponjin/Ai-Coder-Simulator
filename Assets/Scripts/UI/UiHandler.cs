using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BH.Data;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace BH
{
    public class UiHandler : MonoBehaviour
    {
        [SerializeField]
        NodeDataPack nodeDataPack;

        [SerializeField]
        NodeEditor nodeEditor;

        [SerializeField]
        UIContentTab uiContentTab;

        [Space]
        [Header("ContetnView")]
        [SerializeField]
        ScrollView nodeScrollRect;


        private void Awake()
        {
            PopulateScrollView();
        }

        public void AddNodeToEditorSpace(GameObject m_nodeGameObject)
        {
            nodeEditor.CreateNode(m_nodeGameObject.name);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void PopulateScrollView()
        {
            uiContentTab.PopulateContentView();
        }
    }
}

