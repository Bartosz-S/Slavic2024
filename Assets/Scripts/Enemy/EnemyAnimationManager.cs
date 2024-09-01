using UnityEngine;

[RequireComponent(typeof(EnemyNavigation))]
public class EnemyAnimationManager : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        EnemyNavigation enemyNavigation = GetComponent<EnemyNavigation>();
        enemyNavigation.StartStanding.AddListener(OnStartStanding);
        enemyNavigation.StartWalking.AddListener(OnStartWalking);
        enemyNavigation.AggressiveSet.AddListener(OnAggressiveSet);
    }

    private void OnStartStanding()
    {
        animator.SetBool("Moving", false);
    }

    private void OnStartWalking()
    {
        animator.SetBool("Moving", true);
    }

    private void OnAggressiveSet(bool newAggressive)
    {
        animator.SetBool("Aggressive", newAggressive);
    }

}
