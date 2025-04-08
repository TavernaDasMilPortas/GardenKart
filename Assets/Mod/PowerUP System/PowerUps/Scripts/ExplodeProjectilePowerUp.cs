using UnityEngine;
using KartGame.KartSystems;

public class ExplodeProjectilePowerUp : ModPowerUp
{
    public GameObject shellPrefab;
    public Transform firePoint;
    private ArcadeKart kart;

    private void Awake()
    {
        PowerUpName = "Blue Shell";
        Description = "Dispara um projétil que persegue e explode ao contato com o primeiro corredor.";
        Chance = 10;
    }

    public override void Initialize(ArcadeKart targetKart)
    {
        kart = targetKart;
        firePoint = kart.transform.Find("FlightPoint");

        if (firePoint == null)
        {
            Debug.LogWarning("BlueShellPowerUp: Nenhum ponto de disparo encontrado. Use um objeto vazio chamado 'FlightPoint' no kart.");
        }
    }

    public override void PowerUpActive()
    {
        if (kart == null || shellPrefab == null || firePoint == null)
        {
            Debug.LogWarning("BlueShellPowerUp: Dados insuficientes para disparar.");
            return;
        }

        var placements = FindObjectOfType<PlaceManegement>();
        if (placements == null)
        {
            Debug.LogWarning("BlueShellPowerUp: Sistema de colocação não encontrado.");
            return;
        }

        var all = placements.GetPlacementList();
        int selfIndex = all.FindIndex(p => p.kart == kart);
        if (selfIndex == -1) return;

        int targetIndex = selfIndex == 0 ? 1 : 0;
        if (targetIndex >= all.Count) return;

        var targetKart = all[targetIndex].kart;
        GameObject shellInstance = Instantiate(shellPrefab, firePoint.position, firePoint.rotation);

        var shell = shellInstance.GetComponent<ExplodeProjectile>();
        if (shell != null)
        {
            shell.Launch(kart, targetKart);
            Debug.Log($"BlueShellPowerUp: Disparando em direção ao kart '{targetKart.name}'");
        }
        else
        {
            Debug.LogWarning("BlueShellPowerUp: O projétil não possui componente 'BasicProjectile'");
        }
    }
}