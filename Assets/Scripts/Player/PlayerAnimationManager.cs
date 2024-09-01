using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private float WalkingSpeedThreshold = 0.1f;
    [SerializeField] private Transform PlayerMeshTransform;

    private Animator animator;
    private Rigidbody playerRigidBody;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 velocity = playerRigidBody.velocity;

        if(velocity.magnitude > WalkingSpeedThreshold)
        {
            PlayerMeshTransform.rotation = Quaternion.LookRotation(velocity.normalized, Vector3.up);
            animator.SetBool("is_moving", true);
        }
        else
        {
            animator.SetBool("is_moving", false);
        }

    }
}
