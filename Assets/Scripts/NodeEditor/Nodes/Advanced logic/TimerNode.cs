using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using BH;
using BH.Data;

public class TimerNode : BaseNode
{
    [Header("Node Settings")]
    [SerializeField] float timeAmount = 5;

    [SerializeField] InputField timeInputField;

    [SerializeField] bool isTimerOut;

    [SerializeField] bool resetTimer;

    protected float currentCountDown;

    private void Awake()
    {
        timeInputField.placeholder.GetComponent<Text>().text = timeAmount.ToString();
        currentCountDown = timeAmount;
    }

    private void Update()
    {
        CountDownTimer();
    }

    public override void ProcessOutput(GameObject m_shipRefrance)
    {
        int outputSignal = inputPins[0].State;
        
        if (isTimerOut & outputSignal >= 1)
        {
            outputPins[0].ReceivePinSignal(outputSignal, null);
            Debug.Log("Calling signal");
            ResetTimer();
        }
    }

    public void SetTimerAmount()
    {
        timeAmount = float.Parse(timeInputField.text);
    }

    void CountDownTimer()
    {
        if (currentCountDown > 0)
        {
            currentCountDown -= Time.deltaTime;
        }
        if(currentCountDown < 0)
        {   
            isTimerOut = true;
        }
    }

    private void ResetTimer()
    {
        currentCountDown = timeAmount;
        isTimerOut = false;
    }

}
