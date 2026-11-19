using UnityEngine;

public class KeyInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt = "Ambil Kunci [E]";

    public string GetPrompt()
    {
        return prompt;
    }

    public void Interact(GameObject interactor)
    {
        PlayerInventory inventory =
            interactor.GetComponentInParent<PlayerInventory>();

        if (inventory != null)
        {
            inventory.PickUpKey(transform);
        }
    }

}
