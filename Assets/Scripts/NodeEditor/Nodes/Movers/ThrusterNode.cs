using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BH.Data;
using System;

namespace BH
{
    public class ThrusterNode : BaseNode
    {
        [SerializeField] ShipData shipData;

        [Space]
        [SerializeField] InputField forceInputField;

        [SerializeField] Toggle toggelRevert;

        [SerializeField] Toggle toggelX;

        [SerializeField] Toggle toggelY;

        [SerializeField] Toggle toggelZ;

        [Space]
        [SerializeField] int force = 1;

        [Space]
        [SerializeField] bool revert;

        [SerializeField] bool inputX;

        [SerializeField] bool inputY;

        [SerializeField] bool inputZ;

        [SerializeField] Rigidbody shipRb;

        private void Awake()
        {
            shipRb = shipData.ShipGameObject.GetComponent<Rigidbody>();
        }

        public override void ProcessOutput(GameObject m_shipRefrance)
        {
            int outputSignal = inputPins[0].State;

            if (outputSignal >= 1 & shipRb != null)
            {
                //Debug.Log("[NODE]: " + gameObject.name + " has process signal.");

                AddforceToObject(0, 0, 0, force);

                outputPins[0].ReceivePinSignal(outputSignal, null);
                outputSignal = 0;
            }
        }
        public void IsRevertOn()
        {
            if (toggelRevert.isOn) revert = true;
            else revert = false;
        }

        public void UseXAxis()
        {
            if (toggelX.isOn) inputX = true;
            else inputX = false;
        }

        public void UseYAxis()
        {
            if (toggelY.isOn) inputY = true;
            else inputY = false;
        }

        public void UseZAxis()
        {
            if (toggelZ.isOn) inputZ = true;
            else inputZ = false;
            
        }

        public void ChangeForceOutput()
        {
            force = int.Parse(forceInputField.text);
        }
        

        void AddforceToObject(float m_XAxis, float m_YAxis, float m_ZAxis, float m_Force)
        {
            if (!inputX) m_XAxis = 0;
            else if (inputX & revert) m_XAxis =- 1;
            else m_XAxis = 1;

            if (!inputY) m_YAxis = 0;
            else if (inputY & revert) m_YAxis = -1;
            else m_YAxis = 1;

            if (!inputZ) m_ZAxis = 0;
            else if (inputZ & revert) m_ZAxis =- 1;
            else m_ZAxis = 1;

            shipRb.AddRelativeForce(new Vector3(m_XAxis, m_YAxis, m_ZAxis) * m_Force, ForceMode.VelocityChange);
        }
    }
}
