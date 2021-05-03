using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BH.Data;

namespace BH
{
    public class SimulationLoop : MonoBehaviour
    {

		public GameManger gameManger;

        public float minStepTime = 0.075f;
        float currentStepTime;

        public bool simulationState = false;

        void Awake()
        {
			currentStepTime = 0;
		}

        public void StartSimLoop()
        {
            simulationState = true;
        }

        public void StopSimLoop()
        {
            simulationState = false;
        }

        void FixedUpdate()
        {
            if (simulationState)
            {
                if (currentStepTime >= minStepTime)
                {
                    StepSimulation();
                    currentStepTime = 0;
                }
                else
                {
                    currentStepTime++;
                }
            }
		}

		void StepSimulation()
		{
            if (gameManger.IsInDebugMode)
            {
                gameManger.UpdateAgentInDebugMode();
            }
            else
            {
                gameManger.UpdateAgentsInPlayMode();
                // Process inputs
                //List<NodeVisual> inputSignals = nodeEditor.inputsEditor.signals;
                // Tell all signal generators to send their signal out
                //for (int i = 0; i < inputSignals.Count; i++)
                //{
                //	(InputSignal)inputSignals[i]).SendSignal();
                //}
            }



        }
	}
}

