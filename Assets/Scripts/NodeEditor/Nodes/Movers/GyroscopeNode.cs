using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH.Data;
using BH;

public class GyroscopeNode : BaseNode
{
    [SerializeField] ShipData shipData;

    [SerializeField] Rigidbody shipRb;

    [SerializeField] Quaternion gyroOriginRot;

    [SerializeField] float gyroForce = 10f;

    private void Awake()
    {
        gyroOriginRot = transform.rotation;
        shipData.ShipGameObject.GetComponent<Rigidbody>();
    }

    public override void ProcessOutput(GameObject m_shipRefrance)
    {
        int outputSignal = inputPins[0].State;

        if (outputSignal >= 1)
        {
            Debug.Log("[NODE]: " + gameObject.name + " has process signal.");

            outputPins[0].ReceivePinSignal(outputSignal, null);
            outputSignal = 0;
        }
    }


    void ActivateGyro()
    {
        if (transform.rotation.x == gyroOriginRot.x)
        {
            Quaternion newRot = Quaternion.RotateTowards(shipRb.rotation, gyroOriginRot, gyroForce);
            shipRb.MoveRotation(newRot);
        }
    }

}
