using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class Pin : InteractionBase
    {
        public Node node;

        public enum PinType { NodeInput, NodeOutput }
        public PinType pinType;

        // The pin from which this pin receives its input signal.
        [HideInInspector] public Pin parentPin;

        // The pins which this pin forwards its signal to.
        [HideInInspector] public List<Pin> childPins = new List<Pin>();

        //Current state of pin, 0 = off, 1 = on?
        [SerializeField]
        int currentState;

        bool isAiCore;
        public int State => currentState;

        public bool HasParent => parentPin != null | pinType == PinType.NodeOutput;

        public bool IsAiCore
        {
            get => isAiCore;
            set => isAiCore = value;
        }

        // Receive signal: 0 or 1 
        // Sets the current state to the signal
        // Passes the signal and gameObject ref on to any connected pin.
        public void ReceivePinSignal(int signal, GameObject senderObject)
        {
            currentState = signal;
            if (pinType == PinType.NodeInput && !IsAiCore)
            {
                //Debug.Log("[PIN]:" + gameObject.name + " Recive signal as a Input!");
                if (node != null)
                {
                    node.ReceiveInputSignal(this, senderObject);
                    currentState = 0;
                }
                else
                {
                    node = null;
                    currentState = 0;
                }
                
            }

            else if (pinType == PinType.NodeOutput)
            {
                for (int i = 0; i < childPins.Count; i++)
                {
                    //Debug.Log("List of Pin connected to: " + gameObject.name + " " + childPins[i].name);
                    childPins[i].ReceivePinSignal(signal, senderObject);
                    currentState = 0;
                }
            }
        }

        public static bool TryConnect(Pin pinA, Pin pinB)
        {
            Pin parentPin;
            Pin childPin;
            //Check to make sure pin are opposite.
            if (pinA.pinType != pinB.pinType)
            {
                if (pinA.pinType == PinType.NodeOutput)
                {
                    //If pinA is a output pin make it the parent.
                    parentPin = pinA;
                }
                else
                {
                    //Else if pinA is an output pinB is the parent.
                    parentPin = pinB;
                }
                if (parentPin == pinB)
                {
                    //If pinB is parent pinA must be a child pin in order to send the signal.
                    childPin = pinA;
                }
                else
                {
                    //Else if pinA is an parent pinB is the child.
                    childPin = pinB;
                }
                parentPin.childPins.Add(childPin);
                childPin.parentPin = parentPin;
                return true;
            }
            return false;
        }

        public static bool IsValidConnection(Pin m_pinA, Pin m_pinB)
        {
            return m_pinA.pinType != m_pinB.pinType;
        }

        public static void MakeConnection(Pin m_pinA, Pin m_pinB)
        {
            //Debug.Log("MakeConnection called on: " + gameObject.name);
            if (IsValidConnection(m_pinA, m_pinB))
            {
                Pin m_parentPin;                
                Pin m_childPin;
                if (m_pinA.pinType == PinType.NodeOutput)
                {
                    m_parentPin = m_pinA;
                }
                else
                {
                    m_parentPin = m_pinB;
                }
                if (m_pinA.pinType == PinType.NodeInput)
                {
                    m_childPin = m_pinA;
                }
                else
                {
                    m_childPin = m_pinB;
                }
                m_parentPin.childPins.Add(m_childPin);
                m_childPin.parentPin = m_parentPin;
            }
        }

        public static void RemoveConnection(Pin m_pinA, Pin m_pinB)
        {
            Pin m_parentPin;
            Pin m_childPin;
            if (m_pinA.pinType == PinType.NodeOutput)
            {
                m_parentPin = m_pinA;
            }
            else
            {
                m_parentPin = m_pinB;
            }
            if (m_pinA.pinType == PinType.NodeInput)
            {
                m_childPin = m_pinA;
            }
            else
            {
                m_childPin = m_pinB;
            }
            m_parentPin.childPins.Remove(m_childPin);
            m_childPin.parentPin = null;
        }
    }
}

