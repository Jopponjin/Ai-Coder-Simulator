using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH.Data;

namespace BH
{
    public class OnCreateNode : MonoBehaviour
    {
        UiHandler uiHandler;

        string buttonNodeType;

        private void OnEnable()
        {
            uiHandler = gameObject.GetComponentInParent<UiHandler>();
            buttonNodeType = gameObject.name;
        }

        public void NodeButtonPressed()
        {
            uiHandler.AddNodeToEditorSpace(gameObject);
        }

    }
}

