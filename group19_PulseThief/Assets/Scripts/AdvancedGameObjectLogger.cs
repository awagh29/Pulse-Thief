using UnityEngine;
using System.Text;

public class AdvancedGameObjectLogger : MonoBehaviour
{
    public void LogDetailedStatus()
    {
        StringBuilder statusBuilder = new StringBuilder();
        statusBuilder.AppendLine("--- GameObject Status Report ---");
        statusBuilder.AppendLine("Name: " + gameObject.name);
        statusBuilder.AppendLine("Active Self: " + gameObject.activeSelf);
        statusBuilder.AppendLine("Active in Hierarchy: " + gameObject.activeInHierarchy);

        // Get all components attached to the GameObject
        Component[] components = gameObject.GetComponents<Component>();
        statusBuilder.AppendLine("Components found: " + components.Length);

        foreach (Component component in components)
        {
            if (component != null)
            {
                statusBuilder.AppendLine("- Component Type: " + component.GetType().Name);
                // For a Transform component, you can add more details
                if (component is Transform transform)
                {
                    statusBuilder.AppendLine("  Position: " + transform.position);
                    statusBuilder.AppendLine("  Rotation: " + transform.rotation.eulerAngles);
                    statusBuilder.AppendLine("  Scale: " + transform.localScale);
                }
                // You can add more 'if' checks for specific components (e.g., Rigidbody, Renderer)
            }
        }

        Debug.Log(statusBuilder.ToString(), gameObject); // Use the context parameter for convenience
    }

    void Start()
    {
        LogDetailedStatus();
    }

    void Update()
    {
    }
}
