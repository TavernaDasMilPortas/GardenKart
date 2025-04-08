using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    public Transform startLine;
    public Transform endLine;
    public float clearTime = 1f; // tempo para limpar a hash de karts detectados

    private HashSet<Rigidbody> triggeredKarts = new HashSet<Rigidbody>();


    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null || triggeredKarts.Contains(rb))
        {
            Debug.Log($"Ignorando trigger duplicado de {other.name}");
            return;
        }

        var placement = other.GetComponentInParent<PlayerPlacement>();
        if (placement != null)
        {
            Vector3 pos = placement.kart.transform.position;

            Vector3 dirToEnd = (endLine.position - startLine.position).normalized;
            Vector3 dirFromStart = (pos - startLine.position);
            Vector3 dirFromEnd = (pos - endLine.position);

            bool passouNaLinha =
                Vector3.Dot(dirToEnd, dirFromStart) >= 0 &&
                Vector3.Dot(-dirToEnd, dirFromEnd) >= 0;

            Debug.Log($"[{placement.kart.name}] Verificando passagem pela linha...");

            if (passouNaLinha)
            {
                placement.lap++;
                Debug.Log($"✅ [{placement.kart.name}] completou a volta {placement.lap}");
                triggeredKarts.Add(rb);
                StartCoroutine(RemoveKartAfterDelay(rb));
            }
            else
            {
                Debug.Log($"❌ [{placement.kart.name}] não passou corretamente pela linha.");
            }
        }
        else
        {
            Debug.Log($"Nenhum PlayerPlacement encontrado em {other.name}");
        }
    }

    private IEnumerator RemoveKartAfterDelay(Rigidbody rb)
    {
        yield return new WaitForSeconds(clearTime);
        triggeredKarts.Remove(rb);
        Debug.Log($"Liberado para novo trigger: {rb.name}");
    }
}