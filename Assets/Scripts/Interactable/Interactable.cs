using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] public bool isAvailable = true;
    LayerMask PlayerMask;

    [SerializeField] private UnityEvent InteractionEvent;
    [SerializeField] private UnityEvent OnPlayerLeave = new UnityEvent();
    [SerializeField] private UnityEvent OnPlayerEnter = new UnityEvent();

    private void Start()
    {
        PlayerMask = LayerMask.GetMask("Player");

    }
    private void OnTriggerEnter(Collider other)
    {
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
            Debug.Log("Found!");
            InteractionEvent.Invoke();
        }
    }
    
}
