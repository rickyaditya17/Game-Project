using UnityEngine;

public class KeypadInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private KeypadUI keypadUI;

    public string GetPrompt()
    {
        return "Gunakan Keypad [E]";
    }

    public void Interact(GameObject interactor)
    {
        if (keypadUI == null)
        {
            Debug.LogWarning("KeypadUI belum dihubungkan!");
            return;
        }

        keypadUI.OpenKeypad();
    }
}
