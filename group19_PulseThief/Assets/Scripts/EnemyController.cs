using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Settings")]
    [SerializeField] private float chaseRange = 8.0f;
    [SerializeField] private float stopDistance = 0.2f;

    private NavMeshAgent agent;
    private Vector3 spawnPoint;
    private bool insidePulse;

    // We will have 3 states
    public enum EnemyState
    {
        Idle,
        Chase,
        Return
    }
    [SerializeField] private EnemyState currentState = EnemyState.Idle;


    void Start()
    {
        InitializeAgent();
        FindPlayer();
        SnapToNavMesh();
        spawnPoint = transform.position;
        currentState = EnemyState.Idle;
    }

    void Update()
    {
        if (!IsValidSetup()) return;

        UpdateState();
        HandleState();
    }

    private void InitializeAgent()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component missing on " + gameObject.name);
            return;
        }

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = stopDistance;
    }

    private void FindPlayer()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
                Debug.Log("Player found successfully");
            }
            else
            {
                Debug.LogError("Player object not found. Ensure 'Player' tag is assigned.");
            }
        }
    }

    private void SnapToNavMesh()
    {
        if (agent == null) return;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
            Debug.Log($"{gameObject.name} snapped to NavMesh at {hit.position}");
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} could not snap to NavMesh. Ensure NavMesh is baked.");
        }
    }

    private bool IsValidSetup()
    {
        return player != null && agent != null && agent.isOnNavMesh;
    }

    void UpdateState()
    {
        if (SafeZonesController.InSafeZone)
        {
            currentState = EnemyState.Return;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Return;
        }
    }


    void HandleState()
    {
        switch(currentState)
        {
            case EnemyState.Idle:
                agent.isStopped = true;
                break;

            case EnemyState.Chase:
                agent.isStopped = false;
                if (insidePulse)
                {
                    agent.stoppingDistance = 1.2f;
                }
                else
                {
                    agent.stoppingDistance = 0.1f;
                }
                agent.SetDestination(player.position);
                break;

            case EnemyState.Return:
                agent.isStopped = false;
                agent.SetDestination(spawnPoint);
                if (Vector3.Distance(transform.position, player.position) <= 0.2f)
                {
                    currentState = EnemyState.Idle;
                    agent.isStopped = true;
                }
                break;
        }
    }

    public void EnterPulse()
    {
        insidePulse = true;
    }

    public void ExitPulse()
    {
        insidePulse = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Chase range visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Stop distance visualization
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}