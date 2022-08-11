using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH.Data;

namespace BH
{
    public class OnCreateNode : MonoBehaviour
    {
        UiHandler uiHandler;
        public string prefabName = "";

        private void OnEnable()
        {
            uiHandler = gameObject.GetComponentInParent<UiHandler>();
        }

        public void NodeButtonPressed()
        {
            uiHandler.AddNodeToEditorSpace(gameObject);
        }

    }
}

