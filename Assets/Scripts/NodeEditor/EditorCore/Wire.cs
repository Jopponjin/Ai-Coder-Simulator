using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class Wire : MonoBehaviour
    {
        //public Material simpleMat;
        //public Color editCol;
        //public int resolution = 10;
        //public float selectedThickness = 1.2f;

        LineRenderer lineRenderer;

        public BoxCollider boxCollider;
        public bool wireConnected;
        public Pin startPin;
        public Pin endPin;
        float depth;
        [SerializeField]
        float colAngle;

        [Header("Debug")]
        public bool isDirty = false;

        void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            boxCollider = GetComponent<BoxCollider>();
        }

        public Pin NodeInputPin 
        {
            get
            {
                if (startPin.pinType == Pin.PinType.NodeInput) return startPin;
                else return endPin;
            }
        }

        public Pin NodeOutputPin
        {
            get
            {
                if (startPin.pinType == Pin.PinType.NodeOutput) return startPin;
                else return endPin;
                
            }
        }

        private void LateUpdate()
        {
            if (wireConnected)
            {
                float m_depthOffset = 3f;
                transform.localPosition = Vector3.back * (depth + m_depthOffset);

                UpdateWirePositions(startPin.transform.position, endPin.transform.position);
                UpdateColliderPosition(startPin.transform.position, endPin.transform.position);
            }
        }

        public void Connect(Pin m_inputPin,   Pin m_outputPin)
        {
            ConnectToFirstPin(m_inputPin);
            Place(m_outputPin);
        }

        public void ConnectToFirstPin(Pin m_startPin)
        {
            startPin = m_startPin;
        }

        public void Place(Pin m_endPin)
        {
            endPin = m_endPin;
            wireConnected = true;
        }

        public void UpdateWirePositions(Vector3 m_startPinPos, Vector3 m_endPinPos)
        {
            gameObject.transform.position = new Vector3(0, 0, 0);
            lineRenderer.SetPosition(0, m_startPinPos);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, m_endPinPos);
        }

        public void UpdateColliderPosition(Vector3 startPos, Vector3 endPos)
        {
            if (boxCollider != null)
            {
                if (wireConnected)
                {
                    boxCollider.transform.position = lineRenderer.transform.position;

                    boxCollider.transform.position = startPos + (endPos - startPos) / 2;
                    boxCollider.transform.LookAt(startPos);

                    float wireDistance = (endPos - startPos).magnitude;
                    boxCollider.size = new Vector3(0.0f, 0.2f, wireDistance);
                }
            }
        }
    }
}
