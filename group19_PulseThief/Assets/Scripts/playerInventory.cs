using UnityEngine;

public class playerInventory : MonoBehaviour
{
    public int keysCollected = 0;

    public void AddKeys(int amount)
    {
        keysCollected += amount;
        Debug.Log("Keys: " + keysCollected);
    }
}
