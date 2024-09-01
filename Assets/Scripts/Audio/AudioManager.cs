using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource ChillMusic;
    [SerializeField] private AudioSource HypeMusic;
    [SerializeField] private AudioSource Alarm;
    [SerializeField] private float AlarmFadeRate = 0.05f;

    void Start()
    {
        ChillMusic.Play();
    }

    void Update()
    {
        if (Alarm.isPlaying)
        {
            Alarm.volume -= AlarmFadeRate * Time.deltaTime;
        }
    }

    public void StartAlarm()
    {
        ChillMusic.Stop();
        HypeMusic.Play();
        Alarm.Play();
    }
}
