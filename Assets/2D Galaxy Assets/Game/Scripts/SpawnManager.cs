using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemy;

    [SerializeField]
    private GameObject[] _powerups;

    [SerializeField]
    private GameManager _gameManager;


    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }
    private IEnumerator EnemySpawnRoutine()
    {
        while(_gameManager.gameOver == false)
        {
            Instantiate(_enemy, new Vector3(Random.Range(-15.65f, 15.71f), 7.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    private IEnumerator PowerUpSpawnRoutine()
    {
        while(_gameManager.gameOver == false)
        {
            int randomPowerUp = Random.Range(0,3);
             Instantiate(_powerups[randomPowerUp], new Vector3(Random.Range(-15.65f, 15.71f), 7.0f, 0), Quaternion.identity);
             yield return new WaitForSeconds(5.0f);
        }
        
    }

}
