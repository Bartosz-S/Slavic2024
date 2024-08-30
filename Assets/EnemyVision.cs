using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;
using Unity.Properties;
using UnityEngine.Events;
using UnityEditor;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] public UnityEvent PlayerDetected;

    [SerializeField] SphereCollider RangeCollider;
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] float FOVAngle = 90;
    [SerializeField] float FOVDistance = 5;

    GameObject Player = null;
    void Start()
    {
        RangeCollider.radius = FOVDistance;
    }

    void Update()
    {
        //Vector3 start = gameObject.transform.position;
        //Vector3 direction = gameObject.transform.forward;
        //Handles.DrawSolidArc(start, Vector3.up, direction, FOVAngle, FOVDistance);
    }

    private void FixedUpdate()
    {
        if (Player != null)
        {
            // TODO: check if player in field of view
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other.gameObject))
        {
            Player = other.gameObject;

            //TODO: delete
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other.gameObject) && Player != null)
        {
            Player = null;

            //TODO: delete
            Debug.Log("Exit");
        }
    }

    private bool IsPlayer(GameObject gameObject)
    {
        return (PlayerLayer & (1 << gameObject.layer)) != 0;
    }
}
