using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent enemy;
    [SerializeField] GameObject player;
    [SerializeField] float speed = 3.5f;
    [SerializeField] float detectionRadius = 10f;
    [SerializeField] float pauseCooldown = 5f;
    private float pauseTimer = 0f;

    MovePlayer movePlayer = null;
    private Animator animator = null;

    public bool lit = false;
    [HideInInspector] public bool playerDetected = false;
    bool darkType = true;
    SphereCollider coll = null;

    MeshCollider lightCollider = null;

    private bool paused = false;
    

    private void Awake()
    {
        lightCollider = GameObject.Find("LightCollider").GetComponent<MeshCollider>();
        coll = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        movePlayer = player.GetComponent<MovePlayer>();
        animator = transform.parent.GetComponent<Animator>();
    }

    private void Start()
    {
        if (gameObject.transform.parent.CompareTag("Enemy_Light"))
        {
            Debug.Log("light type enemy");
            darkType = false;
        }
        else
            Debug.Log("dark type enemy");

        enemy.speed = speed;
    }

    void Update()
    {
        animator.SetFloat("Speed", enemy.speed);
        animator.SetBool("Lit", lit);

        coll.radius = detectionRadius;

        if (!lightCollider.enabled)
            lit = false;

        if (paused && pauseTimer > 0f)
            pauseTimer -= Time.deltaTime;
        else
            paused = false;

        if (paused || movePlayer.dead || movePlayer.trapped)
        {
            StopChasing();
            return;
        }


        if (darkType)
        {
            if (playerDetected && !lit)
                ChasePlayer();
            else
                StopChasing();
        }
        else
        {
            if (lit)
                ChasePlayer();
            else
                StopChasing();
        }
    }

    void ChasePlayer()
    {
        enemy.speed = speed;
        enemy.SetDestination(player.transform.position);
    }

    void StopChasing()
    {
        enemy.SetDestination(transform.position);
        enemy.speed = 0f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Vector3 playerPos = other.transform.position;
            Vector3 dir = playerPos - transform.position;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, dir, out hit, dir.magnitude))
            {
                Debug.DrawLine(transform.position, playerPos);
                Debug.DrawLine(hit.point, hit.point + new Vector3(0, 10, 0));

                if (hit.collider.gameObject.name != other.name)
                {
                    playerDetected = false;
                    return;
                }
            }

            playerDetected = true;
            if (!paused && (player.GetComponent<MovePlayer>().trapped || player.GetComponent<HealthManager>().stunned))
            {
                paused = true;
                pauseTimer = pauseCooldown;

            }

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
        }

    }
}
