using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private bool canInteract = true;
    [SerializeField] private bool isAvailable = true;
    LayerMask PlayerMask;

    [SerializeField] private UnityEvent InteractionEvent;

    private void Start()
    {
        PlayerMask = LayerMask.GetMask("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if((PlayerMask & (1 << other.gameObject.layer)) != 0)
        {
            other.gameObject.GetComponent<PlayerInputManager>().PlayerInteract.AddListener(OnInteracting);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((PlayerMask & (1 << other.gameObject.layer)) != 0)
        {
            other.gameObject.GetComponent<PlayerInputManager>().PlayerInteract.RemoveListener(OnInteracting);
        }
    }

    private void OnInteracting()
    {
        if (canInteract)
        {
            Debug.Log("Found!");
        }
    }
}
