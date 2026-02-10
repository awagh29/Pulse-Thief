using UnityEngine;
using System.Collections;

public class PulseController : MonoBehaviour
{
    public float maxScale = 5f;
    public float pulseSpeed = 0.5f;
    public float fadeSpeed = 5f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("PulseEffectController requires a SpriteRenderer component.");
            Destroy(gameObject);
            return;
        }
        StartCoroutine(PulseAndFade());
    }

    public IEnumerator PulseAndFade()
    {
        Vector3 initialScale = transform.localScale;
        float timer = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime * pulseSpeed;
            transform.localScale = Vector3.Lerp(initialScale, new Vector3(maxScale, maxScale, 1f), timer);

            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(1f, 0f, timer * fadeSpeed);
            spriteRenderer.color = color;

            yield return null;
        }

        Color finalColor = spriteRenderer.color;
        finalColor.a = 0f;
        spriteRenderer.color = finalColor;
        Destroy(gameObject);
    }
    public void Initialize(float radius)
    {
        transform.localScale = Vector3.one * radius;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.EnterPulse();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.ExitPulse();
        }
    }

}
