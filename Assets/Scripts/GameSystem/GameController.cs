using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Object to steal
    [SerializeField] private Interactable MacGuffin;
    [SerializeField] private Interactable EscapeObject;
    [SerializeField] private float DetectionSpeed = 0.1f;
    private float CurrentDetectionLevel = 0;
    [HideInInspector] public UnityEvent<float> DetectionLevelChanged = new UnityEvent<float>();
    void Start()
    {
        foreach (EnemyVision enemyVision in FindObjectsByType<EnemyVision>(FindObjectsSortMode.None))
        {
            enemyVision.PlayerDetected.AddListener(OnPlayerDetected);
        }
        MacGuffin.InteractionEvent.AddListener(OnMacGuffinInteract);
        EscapeObject.InteractionEvent.AddListener(OnEscapeObjectInteract);

        EscapeObject.isAvailable = false;
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

    void Win()
    {
        SceneManager.LoadScene("Win");
    }

    void OnMacGuffinInteract()
    {
        MacGuffin.isAvailable = false;
        EscapeObject.isAvailable = true;
        MacGuffin.gameObject.SetActive(false);
    }

    void OnEscapeObjectInteract()
    {
        Win();
    }
}
