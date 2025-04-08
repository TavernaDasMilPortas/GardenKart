using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class SeekingProjectliePowerUp : ModPowerUp
{
    public GameObject shellPrefab;
    public Transform firePoint;
    private ArcadeKart kart;
    private bool shootUp = true; // Define a direção do disparo

    private void Awake()
    {
        PowerUpName = "Red Shell";
        Description = "Dispara um projétil que persegue o jogador em outra colocação.";
        Chance = 10;
    }

    public override void Initialize(ArcadeKart targetKart)
    {
        kart = targetKart;

        firePoint = kart.transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogWarning("SeekingPowerUp: Nenhum ponto de disparo encontrado. Use um objeto vazio chamado 'FirePoint' no kart.");
        }
    }

    public void SetDirection(bool up)
    {
        shootUp = up;
    }

    public override void PowerUpActive()
    {
        if (kart == null || shellPrefab == null || firePoint == null)
        {
            Debug.LogWarning("SeekingPowerUp: Dados insuficientes para disparar.");
            return;
        }

        var placements = FindObjectOfType<PlaceManegement>();
        if (placements == null)
        {
            Debug.LogWarning("SeekingPowerUp: Sistema de colocação não encontrado.");
            return;
        }

        var all = placements.GetPlacementList();
        int selfIndex = all.FindIndex(p => p.kart == kart);

        if (selfIndex == -1) return;

        int targetIndex = shootUp
            ? (selfIndex == 0 ? all.Count - 1 : selfIndex - 1) // se 1º, atira no último
            : (selfIndex + 1 < all.Count ? selfIndex + 1 : -1); // se não for o último

        if (targetIndex >= 0 && targetIndex < all.Count)
        {
            var targetKart = all[targetIndex].kart;

            GameObject shellInstance = Instantiate(shellPrefab, firePoint.position, firePoint.rotation);
            var shell = shellInstance.GetComponent<SeekingProjectile>();
            if (shell != null)
            {
                shell.Launch(kart, targetKart);
                Debug.Log($"SeekingPowerUp: Disparando em direção ao kart '{targetKart.name}'");
            }
        }
        else
        {
            Debug.Log("SeekingPowerUp: Nenhum kart válido para mirar.");
        }
    }
}
