using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Key Settings")]
    [SerializeField] private Transform handSocket;
    [SerializeField] private Vector3 keyPosition;
    [SerializeField] private Vector3 keyRotation;

    private Transform heldKey;

    public bool HasKey { get; private set; }
    public bool HasUsedKey { get; private set; }
    public bool PasswordCorrect { get; private set; }

    public bool PickUpKey(Transform key)
    {
        if (HasKey || HasUsedKey)
            return false;

        heldKey = key;

        key.SetParent(handSocket, false);
        key.localPosition = keyPosition;
        key.localEulerAngles = keyRotation;

        Collider[] colliders =
            key.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

        Rigidbody[] rigidbodies =
            key.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        HasKey = true;

        Debug.Log("Key Picked Up!");

        return true;
    }

    public void UseKey()
    {
        if (!HasKey || heldKey == null)
            return;

        Destroy(heldKey.gameObject);

        heldKey = null;
        HasKey = false;
        HasUsedKey = true;

        Debug.Log("Key Used!");
    }

    public void SetPasswordCorrect()
    {
        PasswordCorrect = true;
    }
}
