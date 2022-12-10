using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event System.Action<int> OnScoreUpdate;
    public event System.Action OnGameWon;

    public static GameManager instance;

    [SerializeField] Collectable collectablePrefab;
    [SerializeField] int requiredWinAmount = 3;

    int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            throw new System.Exception("There can only be one GameManager");
        }
    }

    void Start()
    {
        SpawnCollectable();
    }

    public void HandleCollect()
    {
        SpawnCollectable();
        score += 1;
        OnScoreUpdate?.Invoke(score);

        if (score >= requiredWinAmount) WinGame();

        Debug.Log($"Collected thingie score: {score}");
    }

    void WinGame()
    {
        Debug.Log($"Game Won");
        OnGameWon?.Invoke();
        StartCoroutine(RestartGameRoutine());
    }

    IEnumerator RestartGameRoutine()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SpawnCollectable()
    {
        Vector3 currentPosition = this.transform.position;
        Vector3 collectableLocation = new Vector3(
            transform.position.x + Random.Range(-10, 10),
            currentPosition.y + 1f,
            transform.position.z + Random.Range(-10, 10)
        );

        Collectable collectable = Instantiate(collectablePrefab, collectableLocation, Quaternion.identity);
        // Collectable collectable = Instantiate(collectablePrefab);
        // collectable.transform.position = collectableLocation;
    }
}
