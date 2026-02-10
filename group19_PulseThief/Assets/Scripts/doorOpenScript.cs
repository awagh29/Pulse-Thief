using UnityEngine;
using UnityEngine.SceneManagement;

public class doorOpenScript : MonoBehaviour
{
    public int keysRequired = 3;

    private SpriteRenderer doorRenderer;

    void Start()
    {
        doorRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory inv = other.GetComponent<playerInventory>();

            if (inv.keysCollected >= keysRequired)
            {
                Debug.Log("Level Complete!");

                // Change door color to show it unlocked
                if (doorRenderer != null)
                    doorRenderer.color = Color.green;

                // Optional: load next level
                // SceneManager.LoadScene("NextScene");
            }
            else
            {
                Debug.Log("Need more keys!");
            }
        }
    }
}
