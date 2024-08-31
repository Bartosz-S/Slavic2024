using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingTargetScript : MonoBehaviour
{
    [SerializeField] private float radius;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }


}
