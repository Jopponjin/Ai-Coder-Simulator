using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnShipDestroy : MonoBehaviour
{
    public delegate void OnDestroy();
    public static event OnDestroy OnShipDestoyed;


    public void OnKillShip()
    {
        if (OnShipDestoyed != null)
        {
            OnShipDestoyed();
        }
    }
}
