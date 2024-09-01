using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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
        alarmed = 2,
    }
    [SerializeField] private State defaulState;
    [SerializeField] private List<Transform> patrollingTargets = new List<Transform>();
    [SerializeField] private List<Transform> patrollingTargetsOnAlarm = new List<Transform>();
    private List<Transform> currentTargets;

    [SerializeField] private AnimationCurve IdleRotationCurve;
    [SerializeField] private float MaxIdleRotation;
    [SerializeField] private float IdleDuration;
    [SerializeField] private float SpeedAggressive;
    [SerializeField] private float DefaultSpeed;
    [SerializeField] private float AccelerationAggressive;
    [SerializeField] private float DefaultAcceleration;
    [SerializeField] private float AngularSpeedAggressive;
    [SerializeField] private float DefaultAngularSpeed;

    [HideInInspector] public UnityEvent<bool> AggressiveSet = new UnityEvent<bool>();
    [HideInInspector] public UnityEvent StartWalking = new UnityEvent();
    [HideInInspector] public UnityEvent StartStanding = new UnityEvent();

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
        currentTargets = Aggressive ? patrollingTargetsOnAlarm : patrollingTargets;
        currentTargetIndex = 0;

        AggressiveSet.Invoke(Aggressive);
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
            case State.alarmed:
                if (agent.remainingDistance <= chasingStoppingDistance)
                {
                    StartIdle();
                }
                break;
        }
    }
    public void GoToAlarm()
    {
        agent.SetDestination(_player.transform.position);
        agent.stoppingDistance = chasingStoppingDistance;
        currentState = State.alarmed;

        StartWalking.Invoke();
    }
    public void GoToNextPatrolLocation()
    {
        if (currentTargets.Count <= 0)
        {
            return;
        }
        agent.stoppingDistance = patrollingStoppingDistance;
        currentTargetIndex = (currentTargetIndex + 1) % currentTargets.Count;
        agent.SetDestination(currentTargets[currentTargetIndex].position);
    }
    private void IdleRotationUpdate(float time)
    {
        gameObject.transform.rotation = Quaternion.AngleAxis(MaxIdleRotation * IdleRotationCurve.Evaluate(time), Vector3.up) * StartIdleRotation;
    }

    private void StartPatroling()
    {
        currentState = State.patrolling;
        GoToNextPatrolLocation();

        StartWalking.Invoke();
    }

    private void StartIdle()
    {
        StartIdleRotation = gameObject.transform.rotation;
        currentState = State.idle;
        CurrentIdleTime = 0.0f;

        StartStanding.Invoke();
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
            case State.alarmed:
                GoToAlarm();
                break;
            default:
                break;
        }
    }
}
