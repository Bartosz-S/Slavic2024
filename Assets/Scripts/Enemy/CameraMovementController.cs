using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField] private AnimationCurve RotationCurve;
    [SerializeField] private float MaxCameraAngle;

    private Quaternion StartRotation;
    private bool Stop = false;

    private void Start()
    {
        StartRotation = gameObject.transform.rotation;
    }
    private void FixedUpdate()
    {
        if (Stop)
            return;

        gameObject.transform.rotation = Quaternion.AngleAxis(MaxCameraAngle * RotationCurve.Evaluate(Time.fixedTime), Vector3.up) * StartRotation;
    }

    public void SetStop(bool stop)
    {
        Stop = stop;
    }
}
