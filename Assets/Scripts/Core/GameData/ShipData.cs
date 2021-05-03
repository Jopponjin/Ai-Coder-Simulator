using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH.Data
{
    [CreateAssetMenu(fileName = "ShipData", menuName = "Ship System / Defualt Ship Data ")]
    public class ShipData : ScriptableObject
    {
        public enum ShipTeam
        {
            None,
            Friendly,
            Enemy
        }
        public ShipTeam shipTeam
        {
            get => shipTeam;
            set => shipTeam = value;
        }

        public GameObject shipGameObject;

        public GameObject ShipGameObject
        {
            get => shipGameObject;
            set => shipGameObject = value;
        }

        public bool IsShipSet()
        {
            return ShipGameObject == null;
        }

    }
}

