using UnityEngine;

public class NoteInteraction : MonoBehaviour, IInteractable
{
    [Header("Note Information")]
    [SerializeField] private string noteTitle = "Catatan";

    [TextArea(5, 10)]
    [SerializeField] private string noteContent;

    [Header("Note UI")]
    [SerializeField] private NoteUI noteUI;

    public string GetPrompt()
    {
        return "Baca Catatan [E]";
    }

    public void Interact(GameObject interactor)
    {

        if (noteUI == null)
        {
            Debug.LogWarning("NoteUI belum dihubungkan!");
            return;
        }

        noteUI.ShowNote(noteTitle, noteContent);
    }

}

