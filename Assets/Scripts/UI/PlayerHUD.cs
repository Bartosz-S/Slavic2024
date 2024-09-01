using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Slider DetectionLevelSlider;
    void Start()
    {
        DetectionLevelSlider.value = 0;
        GameController gameController = FindObjectOfType<GameController>();
        gameController.DetectionLevelChanged.AddListener(OnDetectionLevelChanged);
    }

    private void OnDetectionLevelChanged(float newDetectionLevel)
    {
        DetectionLevelSlider.value = newDetectionLevel;
    }
}
