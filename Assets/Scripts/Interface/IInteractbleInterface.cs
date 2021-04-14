using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public interface IInteractbleInterface
    {
        bool HoldInteract { get; }

        bool IsInteractble { get; }

        void OnFocus(GameObject interactingAgent);

        void OffFocus(GameObject interactingAgent);

        void OnInteract(GameObject interactingAgent, Vector2 newPosition);

        void OnHold(GameObject interactingAgent, Vector2 newPosition);

        void OnRelease(GameObject interactingAgent);
    }
}

