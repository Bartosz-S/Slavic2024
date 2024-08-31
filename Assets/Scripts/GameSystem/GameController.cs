using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {
        foreach(EnemyVision enemyVision in FindObjectsByType<EnemyVision>(FindObjectsSortMode.None))
        {
            enemyVision.PlayerDetected.AddListener(OnPlayerDetected);
        }
    }

    void OnPlayerDetected()
    {
        Debug.Log("PlayerDetected");
    }
}
