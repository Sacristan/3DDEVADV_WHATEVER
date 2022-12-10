using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event System.Action<int> OnScoreUpdate;

    public static GameManager instance;

    [SerializeField] Collectable collectablePrefab;
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
        Debug.Log($"Collected thingie score: {score}");
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
