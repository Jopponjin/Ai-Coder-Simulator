using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class InteractionBase : MonoBehaviour, IInteractbleInterface
    {
        public Vector3 startPosition = Vector3.zero;

        [Header("Interaction Settings")]
        public bool isInteractble = true;
        public bool holdInteract;

        public bool IsInteractble => isInteractble;

        public bool HoldInteract => holdInteract;

        public virtual void OffFocus(GameObject interactingAgent)
        {
        }

        public virtual void OnFocus(GameObject interactingAgent)
        {
        }

        public virtual void OnHold(GameObject interactingAgent, Vector3 newPosition)
        {
        }

        public virtual void OnInteract(GameObject interactingAgent, Vector3 clickPosition)
        {
        }

        public virtual void OnRelease(GameObject interactingAgent)
        {
        }
    }
}

