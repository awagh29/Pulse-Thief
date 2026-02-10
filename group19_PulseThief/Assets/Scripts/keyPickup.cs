using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public int keyValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<playerInventory>().AddKeys(keyValue);
            Destroy(gameObject);
        }
    }
}
