
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class KeypadUI : MonoBehaviour
{
    [Header("Keypad UI")]
    [SerializeField] private GameObject keypadPanel;
    [SerializeField] private TMP_Text passwordText;
    [SerializeField] private TMP_Text statusText;

    [Header("Password Settings")]
    [SerializeField] private string correctPassword = "3810";

    [Header("Player Inventory")]
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Player Controls")]
    [SerializeField] private Behaviour playerController;
    [SerializeField] private Behaviour cameraInput;
    [SerializeField] private PlayerInteraction playerInteraction;

    [Header("Number Buttons")]
    [SerializeField] private Button[] digitButtons;

    [Header("Control Buttons")]
    [SerializeField] private Button resetButton;
    [SerializeField] private Button enterButton;

    private string enteredPassword = "";
    private Coroutine statusCoroutine;

    public bool IsOpen { get; private set; }

    private void Start()
    {
        for (int i = 0; i < digitButtons.Length; i++)
        {
            int digit = i;

            digitButtons[i].onClick.AddListener(() =>
            {
                AddDigit(digit);
            });
        }

        resetButton.onClick.AddListener(ResetPassword);
        enterButton.onClick.AddListener(CheckPassword);

        keypadPanel.SetActive(false);
        statusText.gameObject.SetActive(false);

        IsOpen = false;
    }

    private void Update()
    {
        if (IsOpen &&
            Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseKeypad();
        }
    }

    public void OpenKeypad()
    {
      
        enteredPassword = "";
        UpdatePasswordDisplay();

        if (statusCoroutine != null)
        {
            StopCoroutine(statusCoroutine);
            statusCoroutine = null;
        }

        statusText.gameObject.SetActive(false);

        keypadPanel.SetActive(true);
        IsOpen = true;

        playerController.enabled = false;
        cameraInput.enabled = false;
        playerInteraction.setInteractionEnabled(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseKeypad()
    {
        keypadPanel.SetActive(false);
        IsOpen = false;

        playerController.enabled = true;
        cameraInput.enabled = true;
        playerInteraction.setInteractionEnabled(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void AddDigit(int digit)
    {
        if (enteredPassword.Length >= 4)
            return;

        enteredPassword += digit.ToString();
        UpdatePasswordDisplay();
    }

    private void ResetPassword()
    {
        enteredPassword = "";
        UpdatePasswordDisplay();

        if (statusCoroutine != null)
        {
            StopCoroutine(statusCoroutine);
            statusCoroutine = null;
        }

        statusText.gameObject.SetActive(false);
    }

    private void CheckPassword()
    {
        if (enteredPassword.Length != 4)
        {
            ShowStatus("MASUKKAN 4 DIGIT!");

            return;
        }

        if (enteredPassword == correctPassword)
        {
            if (playerInventory != null)
            {
                playerInventory.SetPasswordCorrect();
            }
            else
            {
                Debug.LogWarning("PlayerInventory belum dihubungkan!");
            }

            CloseKeypad();
            ShowStatus("PASSWORD BENAR!");
        }
        else
        {
            ShowStatus("PASSWORD SALAH!");
        }
    }

    private void ShowStatus(string message)
    {
        if (statusCoroutine != null)
        {
            StopCoroutine(statusCoroutine);
        }

        statusText.text = message;
        statusText.gameObject.SetActive(true);

        statusCoroutine = StartCoroutine(HideStatusAfterDelay());
    }

    private IEnumerator HideStatusAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        statusText.gameObject.SetActive(false);
        statusCoroutine = null;
    }

    private void UpdatePasswordDisplay()
    {
        passwordText.text = enteredPassword.Length == 0
            ? "_ _ _ _"
            : enteredPassword;
    }
}

