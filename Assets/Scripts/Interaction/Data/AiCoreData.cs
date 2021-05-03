using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AiCoreData", menuName = "Core /Ai Core Data")]
public class AiCoreData : ScriptableObject
{
    public float aiUpdateSpeed = 1f;

    public GameObject aiAgentObject;

    public GameObject AiAgentObject
    {
        set => aiAgentObject = value;
        get => aiAgentObject;
    }
}
