using UnityEngine;
using KartGame.KartSystems;

public class BasicProjectilePowerUp : ModPowerUp
{
    public GameObject shellPrefab;
    public Transform firePoint;
    private ArcadeKart kart;

    private void Awake()
    {
        PowerUpName = "Green Shell";
        Description = "Dispara um projétil em linha reta.";
        Chance = 10;
    }

    public override void Initialize(ArcadeKart targetKart)
    {
        kart = targetKart;
        // Tenta encontrar o fire point no kart
        firePoint = kart.transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogWarning("GreenShellPowerUp: Nenhum ponto de disparo encontrado. Use um objeto vazio chamado 'FirePoint' no kart.");
        }
    }

    public override void PowerUpActive()
    {
        if (kart == null || shellPrefab == null || firePoint == null)
        {
            Debug.LogWarning("GreenShellPowerUp: Dados insuficientes para disparar.");
            return;
        }

        GameObject shellInstance = GameObject.Instantiate(shellPrefab, firePoint.position, firePoint.rotation);
        var shell = shellInstance.GetComponent<BasicProjectile>();
        if (shell != null)
        {
            shell.Launch(kart);
        }
    }
}