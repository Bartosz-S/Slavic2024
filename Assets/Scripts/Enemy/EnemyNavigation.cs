using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float patrollingStoppingDistance;
    [SerializeField] private float chasingStoppingDistance;
    

    enum State
    {
        idle = 0,
        patrolling = 1,
        chasing = 2,
    }
    [SerializeField] private State defaulState;
    [SerializeField] private List<Transform> patrollingTargets = new List<Transform>();
    private int currentTargetIndex = 0;
    private State currentState;

    private GameObject _player;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        currentState = defaulState;
    }
    private void FixedUpdate()
    {
        switch(currentState)
        {
            case State.idle: break;
            case State.patrolling:
                if(agent.remainingDistance <= patrollingStoppingDistance)
                {
                    GoToNextPatrolLocation();
                }
                break;
            case State.chasing:
                if (agent.remainingDistance <= chasingStoppingDistance)
                {
                    currentState = State.patrolling;
                    GoToNextPatrolLocation();
                }
                break;
        }
    }
    public void GoToAlarm()
    {
        agent.SetDestination(_player.transform.position);
        agent.stoppingDistance = chasingStoppingDistance;
        currentState = State.chasing;
    }
    public void GoToNextPatrolLocation()
    {
        if(patrollingTargets.Count <= 0)
        {
            return;
        }
        agent.stoppingDistance = patrollingStoppingDistance;
        currentTargetIndex = (currentTargetIndex + 1) % patrollingTargets.Count;
        agent.SetDestination(patrollingTargets[currentTargetIndex].position);
    }
    public void ChasePlayer()
    {
        
    }
}
