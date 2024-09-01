using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerControls controls;

    [SerializeField] private float movementSpeed;
    [SerializeField] private Rigidbody rbody;
    [SerializeField] private Camera MainCamera;

    public UnityEvent PlayerInteract = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.Enable();
        ConnectActions();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ConnectActions()
    {
        controls.Player.Fire.performed += OnInteraction;
    }
    private void DisconnectActions()
    {
        controls.Player.Fire.performed -= OnInteraction;
    }
    private void OnInteraction(InputAction.CallbackContext context)
    {
        PlayerInteract.Invoke();
    }

    private void Move()
    {
        Vector3 RightVector = MainCamera.transform.right;
        Vector3 ForwardVector = Vector3.Cross(Vector3.up, RightVector).normalized;

        Vector2 direction = controls.Player.Move.ReadValue<Vector2>().normalized;
        Vector3 direction3D = (ForwardVector * -direction.y + RightVector * direction.x).normalized;
        rbody.velocity = direction3D * movementSpeed;
        
    }
}
