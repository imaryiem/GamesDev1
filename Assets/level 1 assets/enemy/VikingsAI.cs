using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] patrolPoints; // Array of predefined patrol points
    public float wanderTimer = 5f;   // Time between wander updates
    public float detectionRange = 10f; // Range within which the agent can detect the player
    public float attackRange = 2f; // Range within which the agent attacks the player
    public LayerMask playerLayer; // Layer to detect the player

    private float timer;
    private int currentPointIndex;
    private Transform player;

   void Start()
{
    timer = wanderTimer;
    GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); // Find the player by tag

    if (playerObject != null)
    {
        player = playerObject.transform;
        if (patrolPoints.Length > 0)
        {
            currentPointIndex = Random.Range(0, patrolPoints.Length);
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }
        else
        {
            Debug.LogWarning("No patrol points set!");
        }
    }
    else
    {
        Debug.LogError("Player GameObject not found! Make sure there is a GameObject tagged as 'Player'.");
    }
}


    void Update()
    {
        timer += Time.deltaTime;

        if (IsPlayerInSight())
        {
            FollowPlayer();
        }
        else
        {
            if (timer >= wanderTimer)
            {
                MoveToRandomPoint();
                timer = 0f;
            }
        }
    }

    bool IsPlayerInSight()
    {
        if (player == null)
        {
            return false;
        }

        RaycastHit hit;
        Vector3 rayDirection = player.position - transform.position;

        if (Physics.Raycast(transform.position, rayDirection, out hit, detectionRange, playerLayer))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    void FollowPlayer()
    {
        if (player == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            // Implement attack behavior here
            Debug.Log("Attacking the player!");
        }
    }

    void MoveToRandomPoint()
    {
        if (patrolPoints.Length == 0)
        {
            return;
        }

        currentPointIndex = Random.Range(0, patrolPoints.Length);
        agent.SetDestination(patrolPoints[currentPointIndex].position);
    }
}
