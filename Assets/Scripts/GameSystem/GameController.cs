using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private float DetectionSpeed = 0.1f;
    private float CurrentDetectionLevel = 0;
    [HideInInspector] public UnityEvent<float> DetectionLevelChanged = new UnityEvent<float>();
    void Start()
    {
        foreach (EnemyVision enemyVision in FindObjectsByType<EnemyVision>(FindObjectsSortMode.None))
        {
            enemyVision.PlayerDetected.AddListener(OnPlayerDetected);
        }
    }

    void OnPlayerDetected(float distance)
    {
        CurrentDetectionLevel += ((1 - distance) / 2 + 0.5f) * Time.deltaTime * DetectionSpeed;
        if (CurrentDetectionLevel >= 1)
        {
            GameOver();
        }

        DetectionLevelChanged.Invoke(CurrentDetectionLevel);
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
