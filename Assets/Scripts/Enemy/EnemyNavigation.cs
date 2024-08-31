using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private bool Aggressive = false;
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

    [SerializeField] private AnimationCurve IdleRotationCurve;
    [SerializeField] private float MaxIdleRotation;
    [SerializeField] private float IdleDuration;
    [SerializeField] private float SpeedAggressive;
    [SerializeField] private float DefaultSpeed;
    [SerializeField] private float AccelerationAggressive;
    [SerializeField] private float DefaultAcceleration;
    [SerializeField] private float AngularSpeedAggressive;
    [SerializeField] private float DefaultAngularSpeed;

    private float CurrentIdleTime = 0.0f;
    private Quaternion StartIdleRotation;

    private int currentTargetIndex = 0;
    private State currentState;

    private GameObject _player;

    public void SetAggressive(bool newAggressive)
    {
        Aggressive = newAggressive;
        agent.speed = Aggressive ? SpeedAggressive : DefaultSpeed;
        agent.acceleration = Aggressive ? AccelerationAggressive : DefaultAcceleration;
        agent.angularSpeed = Aggressive ? AngularSpeedAggressive : DefaultAngularSpeed;
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        SetDefaultState();
        SetAggressive(Aggressive);
    }
    private void FixedUpdate()
    {
        switch (currentState)
        {
            case State.idle:
                CurrentIdleTime += Time.fixedDeltaTime;

                if (CurrentIdleTime >= IdleDuration)
                {
                    StartPatroling();
                    break;
                }

                IdleRotationUpdate(CurrentIdleTime);
                break;
            case State.patrolling:
                if (agent.remainingDistance <= patrollingStoppingDistance)
                {
                    if (Aggressive)
                        GoToNextPatrolLocation();
                    else
                        StartIdle();
                }
                break;
            case State.chasing:
                if (agent.remainingDistance <= chasingStoppingDistance)
                {
                    StartPatroling();
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
        if (patrollingTargets.Count <= 0)
        {
            return;
        }
        agent.stoppingDistance = patrollingStoppingDistance;
        currentTargetIndex = (currentTargetIndex + 1) % patrollingTargets.Count;
        agent.SetDestination(patrollingTargets[currentTargetIndex].position);
    }
    private void IdleRotationUpdate(float time)
    {
        gameObject.transform.rotation = Quaternion.AngleAxis(MaxIdleRotation * IdleRotationCurve.Evaluate(time), Vector3.up) * StartIdleRotation;
    }

    private void StartPatroling()
    {
        currentState = State.patrolling;
        GoToNextPatrolLocation();
    }

    private void StartIdle()
    {
        StartIdleRotation = gameObject.transform.rotation;
        currentState = State.idle;
        CurrentIdleTime = 0.0f;
    }

    private void SetDefaultState()
    {
        switch (defaulState)
        {
            case State.idle:
                StartIdle();
                break;
            case State.patrolling:
                StartPatroling();
                break;
            case State.chasing:
                GoToAlarm();
                break;
            default:
                break;
        }
    }
}
