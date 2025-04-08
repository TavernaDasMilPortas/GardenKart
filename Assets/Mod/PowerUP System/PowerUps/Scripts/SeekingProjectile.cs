using UnityEngine;
using UnityEngine.AI;
using KartGame.KartSystems;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class SeekingProjectile : MonoBehaviour
{
    public float speed = 30f;
    public float angularSpeed = 120f; // Velocidade de rotação em graus por segundo
    public float acceleration = 50f;

    private ArcadeKart ownerKart;
    private ArcadeKart targetKart;
    private NavMeshAgent agent;
    private class KartStatsBackup
    {
        public float TopSpeed;
        public float Acceleration;
    }

    private Dictionary<ArcadeKart, KartStatsBackup> stunnedKarts = new Dictionary<ArcadeKart, KartStatsBackup>();

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = acceleration;
        agent.updateRotation = true;
        agent.updateUpAxis = false; // Para evitar problemas em ambientes 2D ou em pistas com inclinação fixa

        gameObject.layer = LayerMask.NameToLayer("Projectile");
    }

    public void Launch(ArcadeKart owner, ArcadeKart target)
    {
        ownerKart = owner;
        targetKart = target;

        // Ignora colisões com o dono
        Collider[] ownerColliders = owner.GetComponentsInChildren<Collider>();
        Collider myCollider = GetComponent<Collider>();
        foreach (var col in ownerColliders)
            Physics.IgnoreCollision(myCollider, col);
    }

    void Update()
    {
        if (targetKart == null || agent == null) return;

        agent.SetDestination(targetKart.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var kart = collision.rigidbody?.GetComponent<ArcadeKart>();
        if (kart != null && kart != ownerKart)
        {
            kart.ApplyTemporarySlowdown(0.5f, 2.5f);
            Destroy(gameObject);
        }
    }





    
}