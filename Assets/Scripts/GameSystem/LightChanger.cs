using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightChanger : MonoBehaviour
{
    [SerializeField] private Light MainLight;
    [SerializeField] private Color AlarmColor;
    [SerializeField] private float ChangeSpeed;
    private Color DefaultColor;
    private bool AlarmPlaying;
    float currentLerpValue = 0f;
    int currentLerpDirecion = 1;

    void Start()
    {
        DefaultColor = MainLight.color;
    }

    void Update()
    {
        if (AlarmPlaying)
        {
            currentLerpValue += currentLerpDirecion * ChangeSpeed * Time.deltaTime;
            MainLight.color = Color.Lerp(DefaultColor, AlarmColor, currentLerpValue);

            if (currentLerpValue >= 1 && currentLerpDirecion == 1)
            {
                currentLerpDirecion = -1;
            }
            else if (currentLerpValue <= 0 && currentLerpDirecion == -1)
            {
                currentLerpDirecion = 1;
            }
        }
    }

    public void SetAlarmPlaying(bool playing)
    {
        AlarmPlaying = playing;
        currentLerpValue = 0.0f;
        currentLerpDirecion = 1;
    }
}
