using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class BaseNode : Node
    {
        [Header("Debug")]
        public string nodeName;
        public SpriteRenderer nodeSprite;
        public int currentState;
        public Color nodeColorOff;
        public Color nodeColorOn;
        
        public void SetDisplayState(int state)
        {
            if (nodeSprite != null)
            {
                if (state == 1)
                {
                    nodeSprite.color = nodeColorOn;
                }
                else
                {
                    nodeSprite.color = nodeColorOff;
                }
            }
        }
    }
}

