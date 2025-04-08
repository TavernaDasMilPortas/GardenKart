using UnityEngine;
using KartGame.KartSystems;

[RequireComponent(typeof(Rigidbody))]
public class BasicProjectile : MonoBehaviour
{
    public float speed = 40f;
    public float lifeTime = 5f;

    private Rigidbody rb;
    private ArcadeKart ownerKart;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        gameObject.layer = LayerMask.NameToLayer("Projectile");
    }

    public void Launch(ArcadeKart owner)
    {
        ownerKart = owner;

        Collider myCollider = GetComponent<Collider>();
        Collider[] ownerColliders = owner.GetComponentsInChildren<Collider>();

        foreach (var col in ownerColliders)
        {
            Physics.IgnoreCollision(myCollider, col); // ignora TODOS os colliders do kart
        }

        Collider[] allProjectiles = FindObjectsOfType<Collider>();
        foreach (var col in allProjectiles)
        {
            if (col != null && col != myCollider && col.gameObject.layer == gameObject.layer)
            {
                Physics.IgnoreCollision(myCollider, col);
            }
        }

        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var otherRb = collision.rigidbody;
        var kart = otherRb != null ? otherRb.GetComponent<ArcadeKart>() : null;

        if (kart != null && kart != ownerKart)
        {
            kart.ApplyTemporarySlowdown(0.5f, 1f);
            Destroy(gameObject);
        }
        else
        {
            Ricochet(collision.contacts[0].normal);
        }
    }

    private void Ricochet(Vector3 normal)
    {
        Vector3 reflected = Vector3.Reflect(rb.linearVelocity.normalized, normal);
        rb.linearVelocity = reflected * speed;
    }


}
