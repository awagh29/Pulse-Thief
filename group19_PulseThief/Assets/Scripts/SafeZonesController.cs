using UnityEngine;

public class SafeZonesController : MonoBehaviour
{
    public static int safeZoneCount;

    public static bool InSafeZone => safeZoneCount > 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            safeZoneCount++;
            Debug.Log("Entered SafeZone. Count = " + safeZoneCount);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            safeZoneCount--;
            Debug.Log("Exited SafeZone. Count = " + safeZoneCount);
        }
    }

}