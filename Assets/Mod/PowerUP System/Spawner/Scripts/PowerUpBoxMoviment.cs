using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBoxMoviment : MonoBehaviour
{

    Vector3 velocidadeRotacao = new Vector3(0, 70, 0); // Graus por segundo
    Vector3 variation = new Vector3(0f,0.2f,0f);
    Vector3 pontoA;
    Vector3 pontoB;
    float velocidade = 1f;

    private void Awake()
    {
        pontoA = transform.position + variation;
        pontoB = transform.position - variation;
    }
    void Update()
    {
        transform.Rotate(velocidadeRotacao * Time.deltaTime);
        float t = Mathf.PingPong(Time.time * velocidade, 1f);
        transform.position = Vector3.Lerp(pontoA, pontoB, t);

    }
}
