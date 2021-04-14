using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsInteraction : MonoBehaviour
{
    [SerializeField]
    GameObject objectToModify;
    public void ChangeMaterialOnMouseOver(Color newColor, bool resetColor)
    {
        var deafultObjConfig = objectToModify;

        if (!resetColor)
        {
            //objectToModify.GetComponent<Material>().SetColor = newColor;
        }
        else
        {
            //objectToModify
        }
    }

}
