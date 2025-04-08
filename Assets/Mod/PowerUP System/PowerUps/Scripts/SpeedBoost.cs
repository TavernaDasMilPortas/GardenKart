using UnityEngine;
using KartGame.KartSystems;
using System.Collections;
using UnityEngine.UI;

public class SpeedBoost : ModPowerUp
{
    public float boostMultiplier = 2f;
    public float boostDuration = 0.1f;
    private ArcadeKart kart;

    private void Awake()
    {
        PowerUpName = "Speed Boost";
        Description = "Aumenta a velocidade do kart temporariamente.";
        Chance = 10;
    }

    public override void Initialize(ArcadeKart targetKart)
    {
        kart = targetKart;
    }

    public override void PowerUpActive()
    {
        if (kart == null)
        {
            Debug.LogWarning("SpeedBoost: Nenhum kart alvo foi definido!");
            return;
        }

        Debug.Log("SpeedBoost: Aumentando velocidade do kart.");
        StartCoroutine(ApplySpeedBoost());
    }

    private IEnumerator ApplySpeedBoost()
    {
        float originalSpeed = kart.baseStats.TopSpeed;
        float boostedSpeed = originalSpeed * boostMultiplier;
        float slowdownSpeed = originalSpeed * 0.7f; // Reduz a 70% do valor original temporariamente

        // Aplica o boost
        kart.baseStats.TopSpeed = boostedSpeed;
        Debug.Log("SpeedBoost: Velocidade aumentada.");

        yield return new WaitForSeconds(0.7f); 

        // Restaura a velocidade normal
        kart.baseStats.TopSpeed = originalSpeed;
        Debug.Log("SpeedBoost: Velocidade restaurada ao normal.");
    }
}

