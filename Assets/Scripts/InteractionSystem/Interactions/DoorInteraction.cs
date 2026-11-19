using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float speed = 2f;

    bool isOpen;
    Quaternion closedRotation;
    Quaternion opendRotation;

    void Start()
    {
        closedRotation = transform.localRotation;
        opendRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        Quaternion target = isOpen ? opendRotation : closedRotation;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * speed);
    }

    public string GetPrompt()
    {
        return isOpen ? "Tutup PIntu [E]" : "Buka Pintu [E]";
    }

    public void Interact(GameObject interactor)
    {
        if (isOpen)
        {
            isOpen = false;
            return;
        }

        PlayerInventory inventory =
            interactor.GetComponentInParent<PlayerInventory>();

        if (inventory == null)
            return;

        // Periksa apakah kunci sudah pernah digunakan
        if (!inventory.HasUsedKey)
        {
            // Jika player memiliki kunci, gunakan kuncinya
            if (inventory.HasKey)
            {
                inventory.UseKey();

                Debug.Log("Kunci berhasil digunakan!");
                return;
            }

            Debug.Log("Pintu masih terkunci! Kamu membutuhkan kunci.");
            return;
        }

        // Periksa password
        if (!inventory.PasswordCorrect)
        {
            Debug.Log("Pintu masih memerlukan password!");
            return;
        }

        // Jika kedua persyaratan terpenuhi
        isOpen = true;

        Debug.Log("Pintu berhasil dibuka!");
    }
}   
