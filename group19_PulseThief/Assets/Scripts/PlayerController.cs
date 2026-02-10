using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 moveInput;
    public GameObject pulsePrefab;
    [Header("Heartbeat")]
    [SerializeField] private float bpm = 60f;
    [SerializeField] private float bpmDecayrate = 5f;
    [SerializeField] private float bpmIncrease = 15f;
    [SerializeField] private float bpmMin = 40f;
    [SerializeField] private float bpmMax = 100f;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        // Pulse Function
        Pulse();
        HandleHeartBeat();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    void Pulse()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bpm += bpmIncrease;
            bpm = Mathf.Clamp(bpm, 0f, bpmMax);

            if (pulsePrefab != null)
            {
                GameObject pulse = Instantiate(pulsePrefab, transform.position, Quaternion.identity);

                PulseController pc = pulse.GetComponent<PulseController>();
                if (pc != null)
                {
                    float radius = Mathf.Lerp(0.5f, 3f, bpm / bpmMax);
                    pc.Initialize(radius);
                }
            }
        }
    }



    void HandleHeartBeat()
    {
        bpm -= bpmDecayrate * Time.deltaTime;
        bpm = Mathf.Clamp(bpm, 0f, bpmMax);

        if(bpm <= 0f)
        {
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log("Player has died!");
        // Implement death logic here (game over screen)
    }

}