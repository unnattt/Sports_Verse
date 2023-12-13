using UnityEngine;

namespace Sportsverse.World
{
    public interface IInteractable
    {
        void Interact(GameObject interactor);

        GameObject GetInteractable();

        void ResetObject();
    }
}