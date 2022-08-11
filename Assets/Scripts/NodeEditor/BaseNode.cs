using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class BaseNode : Node
    {
        [Header("Debug")]
        public string nodeName = "";
        public int currentState;
        public bool isDirty = false;

        public enum NodeUiState
        {
            None,
            Selected,
            OnHold
        }

        public NodeUiState nodeUi;

        public void SelectSignal(int UiState)
        {
            switch (UiState)
            {
                case 0:
                    nodeUi = NodeUiState.None;
                    break;
                case 1:
                    nodeUi = NodeUiState.Selected;
                    break;
                case 2:
                    nodeUi = NodeUiState.OnHold;
                    break;
                default:
                    nodeUi = NodeUiState.None;
                    break;
            }
        }

        public void SetHighlighted()
        {

        }
    }
}

