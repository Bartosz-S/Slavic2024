using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerControls controls;

    [SerializeField] private float movementSpeed;
    [SerializeField] private Rigidbody rbody;

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.Enable();
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        ConnectActions();
    }
    private void OnDisable()
    {
        DisconnectActions();
    }

    private void ConnectActions()
    {

    }
    private void DisconnectActions()
    {

    }
   
    private void Move()
    {
        Vector2 direction = controls.Player.Move.ReadValue<Vector2>().normalized;
        Vector3 direction3D = new Vector3(direction.x, 0, direction.y);
        rbody.position += direction3D * movementSpeed * Time.deltaTime;
    }
}
