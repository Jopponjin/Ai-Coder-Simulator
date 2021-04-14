using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH;

namespace BH.Data
{
    [CreateAssetMenu(fileName = "KeybindData", menuName = "Input System/KeyBind Data")]
    public class KeybinData : ScriptableObject
    {
        public Dictionary<string, KeyCode> playerKeybinds = new Dictionary<string, KeyCode>();

         KeyCode currentKeyDown;
         KeyCode[] currentKeysDown;

        public Dictionary<string, KeyCode> PlayerKeyBindsList
        {
            get => playerKeybinds;
            set => playerKeybinds = value;
        }

        public KeyCode CurrentSingelKey
        {
            get => currentKeyDown;
            set => currentKeyDown = value;
        }

        public KeyCode[] CurrentKeysDown
        {
            get => currentKeysDown;
            set => currentKeysDown = value;
        }

        public bool IsSameSingelKey(KeyCode keyCode)
        {
            return CurrentSingelKey == keyCode;
        }

        public bool IsSameKeys(KeyCode[] keyCodes)
        {
            return CurrentKeysDown == keyCodes;
        }

        public void SaveKeybinds()
        {
            
        }

        public void LoadKeybinds()
        {

        }

        public void Defaultkeybinds()
        {
            Debug.Log("[KEYBINDS]: Adding default settings to Key binds.");
            PlayerKeyBindsList.Add("Cancel", KeyCode.Escape);
            PlayerKeyBindsList.Add("Delete", KeyCode.Delete);
            PlayerKeyBindsList.Add("Confirm", KeyCode.Return);
            PlayerKeyBindsList.Add("Cancel Mouse", KeyCode.Mouse1);
        }

    }
}

