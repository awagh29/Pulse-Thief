using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0, 0, -10);

    void LateUpdate()
    {
        if (player == null) return;

        // Follow position only
        Vector3 desiredPosition = new Vector3(
            player.position.x + offset.x,
            player.position.y + offset.y,
            offset.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Keep camera rotation fixed
        transform.rotation = Quaternion.identity;
    }
}
