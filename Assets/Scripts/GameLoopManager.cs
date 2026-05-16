using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    [SerializeField] private Core mainCore;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private float surviveTimeGoal = 120f;

    private float timer;
    private bool gameIsActive = false;

    private void OnEnable()
    {
        if (mainCore != null)
        {
            mainCore.OnCoreDestroyed += HandleDefeat;
        }
    }

    private void OnDisable()
    {
        if (mainCore != null)
        {
            mainCore.OnCoreDestroyed -= HandleDefeat;
        }
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        Debug.Log("Systems Actived, Core Defence Active!");
        timer = 0f;
        gameIsActive = true;
        spawner.StartSpawning();
    }

    private void Update()
    {
        if (!gameIsActive) return;

        timer += Time.deltaTime;

        if (timer >= surviveTimeGoal)
        {
            HandleVictory();
        }
    }

    private void HandleDefeat()
    {
        gameIsActive = false;
        spawner.StopSpawning();
    }

    private void HandleVictory()
    {
        gameIsActive = false;
        spawner.StopSpawning();
        Debug.Log("MISSION SUCCESSFUL: Core Defended Successfuly!");
    }
}