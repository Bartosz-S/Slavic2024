using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] public bool isAvailable = true;
    LayerMask PlayerMask;

    [SerializeField] public UnityEvent InteractionEvent;
    [SerializeField] private UnityEvent OnPlayerLeave = new UnityEvent();
    [SerializeField] private UnityEvent OnPlayerEnter = new UnityEvent();

    public void SetAvailable(bool available)
    {
        isAvailable = available;
    }

    private void Start()
    {
        PlayerMask = LayerMask.GetMask("Player");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isAvailable)
            return;

        if((PlayerMask & (1 << other.gameObject.layer)) != 0)
        {
            other.gameObject.GetComponent<PlayerInputManager>().PlayerInteract.AddListener(OnInteracting);
            OnPlayerEnter.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((PlayerMask & (1 << other.gameObject.layer)) != 0)
        {
            other.gameObject.GetComponent<PlayerInputManager>().PlayerInteract.RemoveListener(OnInteracting);
            OnPlayerLeave.Invoke();
        }
    }

    private void OnInteracting()
    {
        if (isAvailable)
        {
            InteractionEvent.Invoke();
        }
    }
    
}
