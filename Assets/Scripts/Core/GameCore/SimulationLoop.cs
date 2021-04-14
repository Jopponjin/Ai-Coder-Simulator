using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class SimulationLoop : MonoBehaviour
    {
		public static int simulationFrame { get; private set; }

		NodeEditor nodeEditor;

		public List<BaseNode> allNodes;

        public float minStepTime = 0.075f;
        float lastStepTime;

        void Awake()
        {
            simulationFrame = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

		void StepSimulation()
		{
			simulationFrame++;
			RefreshChipEditorReference();

			

			// Process inputs
			//List<NodeVisual> inputSignals = nodeEditor.inputsEditor.signals;
			// Tell all signal generators to send their signal out
			//for (int i = 0; i < inputSignals.Count; i++)
			//{
			//	(InputSignal)inputSignals[i]).SendSignal();
			//}

		}

		void RefreshChipEditorReference()
		{
			if (nodeEditor == null)
			{
				nodeEditor = FindObjectOfType<NodeEditor>();
			}
		}
	}
}

