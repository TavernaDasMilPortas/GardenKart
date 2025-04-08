using UnityEngine;

public class StickToGround : MonoBehaviour
{
    public float rayHeight = 2f;
    public float groundOffset = 0.2f;
    public LayerMask groundMask;

    void LateUpdate()
    {
        Ray ray = new Ray(transform.position + Vector3.up * rayHeight, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, rayHeight + 5f, groundMask))
        {
            Vector3 pos = transform.position;
            pos.y = hit.point.y + groundOffset;
            transform.position = pos;
        }
    }
}