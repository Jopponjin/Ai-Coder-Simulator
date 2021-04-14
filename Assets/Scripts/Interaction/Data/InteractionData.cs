using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH.Data
{
    [CreateAssetMenu(fileName = "InteractionData", menuName = "Interaction System/Interaction Data")]
    public class InteractionData : ScriptableObject
    {
		private InteractionBase interactbleOne;
		private InteractionBase interactableTwo;

		public InteractionBase InteractableOne
        {
            get => interactbleOne;
            set => interactbleOne = value;
        }

		public InteractionBase InteractableTwo
        {
			get => interactableTwo;
			set => interactableTwo = value;
        }


		public void OnInteract(GameObject gameObject, Vector2 newPosition)
		{
			interactbleOne.OnInteract(gameObject, newPosition);
		}

		public void OnHold(GameObject gameObject, Vector2 newPosition)
		{
			interactbleOne.OnHold(gameObject, newPosition);
		}

		public void OnRelease(GameObject gameObject)
		{
			interactbleOne.OnRelease(gameObject);
		}

		public bool IsSameInteractableOne(InteractionBase newInteractble)
		{
			return InteractableOne == newInteractble;
		}

		public bool IsSameInteractableTwo(InteractionBase newInteractble)
		{
			return interactableTwo == newInteractble;
		}


		public bool IsEmptyOne()
		{
			return InteractableOne == null;
		}

		public bool IsEmptyTwo()
		{
			return InteractableTwo == null;
		}


		public void ResetDataOne()
		{
			InteractableOne = null;
		}

		public void ResetDataTwo()
		{
			InteractableTwo = null;
		}

		// --------------- Input data --------------- //

		public bool InteractedClicked
        {
            get => interactedClicked;
            set => interactedClicked = value;
        }

        public bool InteractedReleased
        {
            get => interactedRelease;
            set => interactedRelease = value;
        }

        public void ResetInput()
		{
			interactedClicked = false;
			interactedRelease = false;
		}

		private bool interactedClicked;

		private bool interactedRelease;

	}
}

