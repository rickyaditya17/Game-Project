using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class NoteUI : MonoBehaviour
{
    [Header("Note UI")]
    [SerializeField] private GameObject notePanel;
    [SerializeField] private TMP_Text noteTitle;
    [SerializeField] private TMP_Text noteContent;

    public bool IsReading { get; private set; }

    void Start()
    {
        notePanel.SetActive(false);
    }

    void Update()
    {
        if (IsReading &&
            Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseNote();
        }
    }

    public void ShowNote(string title, string content)
    {
        noteTitle.text = title;
        noteContent.text = content;

        notePanel.SetActive(true);
        IsReading = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseNote()
    {
        notePanel.SetActive(false);
        IsReading = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
