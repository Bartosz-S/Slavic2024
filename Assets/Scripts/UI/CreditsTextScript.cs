using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsSceneScript : MonoBehaviour
{
    [SerializeField] private TMP_Text creditsText;
    [SerializeField] private float creditsMoveSpeed;
    private void FixedUpdate()
    {
        creditsText.transform.position += Vector3.up * creditsMoveSpeed * Time.fixedDeltaTime;

    }
}
