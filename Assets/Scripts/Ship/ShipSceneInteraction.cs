using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH;
using BH.Data;

public class ShipSceneInteraction : MonoBehaviour
{
    [SerializeField] ShipData shipData;


    private void OnCollisionEnter(Collision collision)
    {
        shipData.shipContact = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        shipData.shipContactStay = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        shipData.shipContact = false;
        shipData.shipContactStay = false;
    }
}
