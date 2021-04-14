using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class Wire : MonoBehaviour
    {
        //public Material simpleMat;
        LineRenderer lineRenderer;
        //public Color editCol;
        //public int resolution = 10;
        //public float selectedThickness = 1.2f;

        public EdgeCollider2D wireCollider;

        public bool wireConnected;

        public Pin startPin;
        public Pin endPin;
        float depth;
        bool selected;

        void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            wireCollider = GetComponent<EdgeCollider2D>();
        }

        public Pin NodeInputPin 
        {
            get
            {
                if (startPin.pinType == Pin.PinType.NodeInput)
                {
                    return startPin;
                }
                else
                {
                    return endPin;
                }
            }
        }

        public Pin NodeOutputPin
        {
            get
            {
                if (startPin.pinType == Pin.PinType.NodeOutput)
                {
                    return startPin;
                }
                else
                {
                    return endPin;
                }
            }
        }

        private void LateUpdate()
        {
            if (wireConnected)
            {
                float m_depthOffset = 3f;
                transform.localPosition = Vector3.back * (depth + m_depthOffset);

                UpadteWireStartPoint(startPin.transform.position);
                UpdateWireEndPoint(endPin.transform.position);
                UpdateColliderPosition(startPin.transform.position, endPin.transform.position);
            }
        }

        public void Connect(Pin m_inputPin,   Pin m_outputPin)
        {
            ConnectToFirstPinWithWire(m_inputPin);
            Place(m_outputPin);
        }

        public void ConnectToFirstPinWithWire(Pin m_startPin)
        {
            startPin = m_startPin;
        }

        // Connect the input pin to the output pin.
        public void Place(Pin m_endPin)
        {
            endPin = m_endPin;
            wireConnected = true;
        }

        public void UpadteWireStartPoint(Vector2 m_startPinPos)
        {
            lineRenderer.SetPosition(0, m_startPinPos);
        }

        public void UpdateWireEndPoint(Vector2 m_endPinPos)
        {
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, m_endPinPos);
        }

        public void UpdateColliderPosition(Vector2 m_startPos, Vector2 m_endPos)
        {
            if (wireConnected)
            {
                Vector2[] m_wireColPos = wireCollider.points;
                m_wireColPos[0] = m_startPos;
                m_wireColPos[m_wireColPos.Length - 1] = m_endPos;
                wireCollider.points = m_wireColPos;
            }
        }
    }
}
