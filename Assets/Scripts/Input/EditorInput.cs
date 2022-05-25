using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using BH.Data;

namespace BH
{
    public class EditorInput : MonoBehaviour
    {
        public EditorControls inputActions;
        
        public InputAction mouseSelectAction;
        public InputAction deleteAction;
        public InputAction multiSelectAction;
        public InputAction gridSnapAction;
        public InputAction undoAction;
        public InputAction redoAction;

        public bool undoTriggerd = false;

        private void OnEnable()
        {
            inputActions = new EditorControls();

            mouseSelectAction = inputActions.Editor.MouseSelect;
            mouseSelectAction.Enable();

            deleteAction = inputActions.Editor.Delete;
            deleteAction.Enable();

            multiSelectAction = inputActions.Editor.MultiSelect;
            multiSelectAction.Enable();

            gridSnapAction = inputActions.Editor.GridSnap;
            gridSnapAction.Enable();

            redoAction = inputActions.Editor.Redo;
            redoAction.AddCompositeBinding("ButtonWithOneModifier")
                .With("Button", "<Keyboard>/leftCtrl")
                .With("Modifier", "<Keyboard>/x");
            redoAction.Enable();

            undoAction = inputActions.Editor.Undo;
            undoAction.AddCompositeBinding("ButtonWithOneModifier")
                .With("Button", "<Keyboard>/leftCtrl")
                .With("Modifier", "<Keyboard>/z");
            undoAction.Enable();
        }

        private void OnDisable()
        {
            mouseSelectAction.Disable();
            deleteAction.Disable();
            gridSnapAction.Disable();
            undoAction.Disable();
            redoAction.Disable();
        }
        
    }
}

