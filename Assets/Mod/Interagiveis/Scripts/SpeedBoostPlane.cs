using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class SpeedBoostPlane : MonoBehaviour
{
    public SpeedBoost speedboost;

    // Guardamos os rigidbodies dos karts que já foram afetados
    private HashSet<Rigidbody> affectedBodies = new HashSet<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        var rb = other.attachedRigidbody;
        if (rb == null) return;

        // Se já foi afetado, não aplica de novo
        if (affectedBodies.Contains(rb)) return;

        var kart = rb.GetComponent<ArcadeKart>();
        if (kart == null) return;

        affectedBodies.Add(rb);

      
       speedboost.Initialize(kart);
       speedboost.PowerUpActive();
    }

    private void OnTriggerExit(Collider other)
    {
        var rb = other.attachedRigidbody;
        if (rb == null) return;

        // Quando o rigidbody sai completamente, remove da lista
        if (affectedBodies.Contains(rb))
        {
            affectedBodies.Remove(rb);
        }
    }
}


