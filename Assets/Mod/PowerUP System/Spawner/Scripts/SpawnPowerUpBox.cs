using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUpBox : MonoBehaviour
{
    public PowerUpBox powerUpBox;
    [SerializeField] GameObject powerUpBoxPrefab;
    public Vector3 spawnPoint;
    private float tempoParaRespawn = 7f;
    private float tempoSemPowerUp = 0f;
    
    void Start()
    {
        spawnPoint = transform.position + Vector3.up;
        SpawnPrefab();
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpBox == null)
        {
            tempoSemPowerUp += Time.deltaTime;

            if (tempoSemPowerUp >= tempoParaRespawn)
            {
                SpawnPrefab();
                tempoSemPowerUp = 0f;
            }
        }
        else
        {
            // Zera o contador se o powerUpBox ainda estiver presente
            tempoSemPowerUp = 0f;
        }
    }

    public void SpawnPrefab()
    {
        powerUpBox = Instantiate(powerUpBoxPrefab, spawnPoint, transform.rotation, transform).GetComponent<PowerUpBox>();
    }
}

