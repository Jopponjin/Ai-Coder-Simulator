using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BH.Data;
using TMPro;

namespace BH
{
    public class KeybindHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        KeybinData keybinData;

        public Button cancelButtonBind;
        public Button deleteButtonBind;

        [SerializeField]
        TextMeshPro cancelTextField;

        [SerializeField]
        TextMeshPro deletedTextField;

        void Awake()
        {
            keybinData.Defaultkeybinds();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                
            }
        }

        

        
    }
}

