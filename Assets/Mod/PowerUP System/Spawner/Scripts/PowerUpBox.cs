using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class PowerUpBox : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class PowerUpEntry
    {
        public ModPowerUp powerUpPrefab;
        public int chance; // Quanto maior, mais provável
    }

    public List<PowerUpEntry> availablePowerUps;
    

    private void OnTriggerEnter(Collider other)
    {
        var rb = other.attachedRigidbody;
        if (rb && rb.GetComponent<ArcadeKart>())
        {   
            var kart = rb.GetComponent<ArcadeKart>();
            var kartPW = kart.GetComponent<PlayerPowerUp>();
            if (kart)
            {
                ModPowerUp selected = GetRandomPowerUp();
                if (selected != null)
                {
                    ModPowerUp instance = Instantiate(selected);
                    instance.Initialize(kart);
                    kartPW.currentlyPowerUp = instance;
                }

                Destroy(gameObject); // Remove a powerUpBox da pista
            }
        }

        ModPowerUp GetRandomPowerUp()
        {
            int totalChance = 0;
            foreach (var p in availablePowerUps)
                totalChance += p.chance;

            int roll = Random.Range(0, totalChance);
            int sum = 0;
            foreach (var p in availablePowerUps)
            {
                sum += p.chance;
                if (roll < sum)
                {
                    return p.powerUpPrefab;
                }
            }

            Debug.LogWarning("PowerUpBox: Nenhum power-up foi sorteado.");
            return null;
        }
    }
}
