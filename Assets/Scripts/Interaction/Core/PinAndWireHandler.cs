using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BH
{
	public class PinAndWireHandler : MonoBehaviour
	{
		public event System.Action onConnectionChanged;

		public Transform wireHolder;
		public Wire wirePrefab;

		[Header("Debug")]
		[SerializeField]
		Wire wireToPlace;

		[SerializeField]
		Pin wireStartPin;

		Dictionary<Pin, Wire> wiresByNodeInputPin;
		public List<Wire> allWires { get; private set; }

        private void Awake()
        {
			allWires = new List<Wire>();
			wiresByNodeInputPin = new Dictionary<Pin, Wire>();
		}

        public void HandleWireCreation(Pin startPin)
        {
			wireToPlace = Instantiate(wirePrefab, Vector3.zero, gameObject.transform.rotation);

			if (startPin)
			{
				wireStartPin = startPin;
				wireToPlace.ConnectToFirstPinWithWire(wireStartPin);
			}
		}

		public void HandleWirePlacement(Pin startPin, Pin endPin)
        {
            //Debug.Log("[PIN-WIRE]: 'HandleWirePlacement' called!");
            if (Pin.IsValidConnection(startPin, endPin))
            {
				Pin nodeInputPin = (startPin.pinType == Pin.PinType.NodeInput) ? startPin : endPin;

				wireToPlace.UpadteWireStartPoint(startPin.transform.position);
				wireToPlace.UpdateWireEndPoint(endPin.transform.position);

				wireToPlace.Place(endPin);
				Pin.MakeConnection(startPin, endPin);
				allWires.Add(wireToPlace);
				wiresByNodeInputPin.Add(nodeInputPin, wireToPlace);

				wireToPlace = null;
				onConnectionChanged?.Invoke();
			}
            else
            {
				StopPlaceingWire();
            }
        }

		public void DeleteNodeWires(Node node)
        {
			List<Wire> wiresToDestroy = new List<Wire>();
			Debug.Log("[PIN&WIRE]: Called deleteNodeWires on: " + node.transform.gameObject.name);

            foreach (var outputPins in node.outputPins)
            {
                foreach (var childPin in outputPins.childPins)
                {
					wiresToDestroy.Add(wiresByNodeInputPin[childPin]);
                }
            }
			foreach (var inputPin in node.inputPins)
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

		public Wire GetWire(Pin childPin)
        {
            if (wiresByNodeInputPin.ContainsKey(childPin))
            {
				return wiresByNodeInputPin[childPin];
            }
			return null;
        }

		void DestroyWire(Wire wire)
        {
			wiresByNodeInputPin.Remove(wire.NodeInputPin);
			allWires.Remove(wire);
			Pin.RemoveConnection(wire.startPin, wire.endPin);
			Destroy(wire.gameObject);

        }

		void RemoveConflictWire(Pin pin)
        {
			if (wiresByNodeInputPin.ContainsKey(pin))
            {
				DestroyWire(wiresByNodeInputPin[pin]);
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

