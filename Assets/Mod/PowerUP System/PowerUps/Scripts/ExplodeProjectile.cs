using UnityEngine;
using UnityEngine.AI;
using KartGame.KartSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class ExplodeProjectile : MonoBehaviour
{
    public float speed = 35f;
    public float angularSpeed = 360f;
    public float acceleration = 60f;
    public float explosionRadius = 5f;
    public float slowMultiplier = 0.5f;
    public float slowDuration = 2.5f;

    private ArcadeKart ownerKart;
    private ArcadeKart targetKart;
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = acceleration;
        agent.updateRotation = true;
        agent.updateUpAxis = false;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;

        gameObject.layer = LayerMask.NameToLayer("Projectile");
    }

    public void Launch(ArcadeKart owner, ArcadeKart target)
    {
        ownerKart = owner;
        targetKart = target;

        if (ownerKart != null)
        {
            foreach (var col in ownerKart.GetComponentsInChildren<Collider>())
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), col);
            }
        }

        if (targetKart == null)
        {
            Debug.LogWarning("ExplodeProjectile: Nenhum alvo definido no lançamento.");
        }
    }

    void Update()
    {
        if (targetKart != null)
        {
            agent.SetDestination(targetKart.transform.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody == null) return;

        var kart = collision.rigidbody.GetComponent<ArcadeKart>();
        if (kart != null && kart == targetKart)
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var col in colliders)
        {
            var kart = col.GetComponentInParent<ArcadeKart>();
            if (kart != null)
            {
                kart.ApplyTemporarySlowdown(slowMultiplier, slowDuration);
            }
        }

        // Adicione aqui efeitos visuais/sonoros da explosão se desejar
        Destroy(gameObject);
    }
}