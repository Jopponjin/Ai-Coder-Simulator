using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BH
{
	public class PinAndWireHandler : MonoBehaviour
	{
		public Transform wireHolder;
		public Wire wirePrefab;

		GameObject editor;
		[SerializeField]
		Pin wireStartPin;
		[SerializeField]
		Wire wireToPlace;

		Dictionary<Pin, Wire> wiresByNodeInputPin;
		public List<Wire> allWires { get; private set; }

		public enum PinWireState { None, PlacingWire }
		public PinWireState currentState;

		private void Awake()
        {
			editor = GameObject.Find("Editor");
			allWires = new List<Wire>();
			wiresByNodeInputPin = new Dictionary<Pin, Wire>();
		}

		public void HandleWirePlacement(Pin m_startPin, Pin m_endPin)
        {
			if (Pin.IsValidConnection(m_startPin, m_endPin))
			{
				Pin nodeInputPin = (m_startPin.pinType == Pin.PinType.NodeInput) ? m_startPin : m_endPin;

				RemoveConflictWire(nodeInputPin);

				wireStartPin = m_startPin;
				wireToPlace = Instantiate(wirePrefab, Vector3.zero, gameObject.transform.rotation, parent: editor.transform);

				wireToPlace.ConnectToFirstPin(wireStartPin);
				wireToPlace.UpdateWirePositions(m_startPin.transform.position, m_endPin.transform.position);
				wireToPlace.Place(m_endPin);

				Pin.MakeConnection(m_startPin, m_endPin);
				allWires.Add(wireToPlace);
				wiresByNodeInputPin.Add(nodeInputPin, wireToPlace);

				wireToPlace = null;
			}
			else
			{
				StopPlaceingWire();
			}
		}

		public void DeleteNodesWires(Node m_node)
        {
			List<Wire> wiresToDestroy = new List<Wire>();
			Debug.Log("[PIN&WIRE]: Called deleteNodeWires on: " + m_node.transform.gameObject.name);

			foreach (var outputPins in m_node.outputPins)
			{
				foreach (var childPin in outputPins.childPins)
				{
					wiresToDestroy.Add(wiresByNodeInputPin[childPin]);
				}
			}

			foreach (var inputPin in m_node.inputPins)
			{
				if (inputPin.parentPin)
				{
					wiresToDestroy.Add(wiresByNodeInputPin[inputPin]);
				}
			}

			for (int i = 0; i < wiresToDestroy.Count; i++)
			{
				wiresToDestroy[i].wireConnected = false;
				DestroyWire(wiresToDestroy[i]);
			}
		}

		public Wire GetWire(Pin m_childPin)
        {
            if (wiresByNodeInputPin.ContainsKey(m_childPin))
            {
				return wiresByNodeInputPin[m_childPin];
            }
			return null;
        }

		public void DestroyWire(Wire m_wire)
        {
			//m_wire.wireConnected = false;
			wiresByNodeInputPin.Remove(m_wire.NodeInputPin);

			allWires.Remove(m_wire);
			Pin.RemoveConnection(m_wire.startPin, m_wire.endPin);
			Destroy(m_wire.gameObject);

        }

		void RemoveConflictWire(Pin m_pin)
        {
			if (wiresByNodeInputPin.ContainsKey(m_pin))
            {
				DestroyWire(wiresByNodeInputPin[m_pin]);
            }
        }

		public void StopPlaceingWire()
        {
            if (wireToPlace)
            {
				Destroy(wireToPlace.gameObject);
				wireToPlace = null;
				wireStartPin = null;
            }
        }

	}
}

