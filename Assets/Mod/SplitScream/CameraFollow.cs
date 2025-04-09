using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [Tooltip("Objeto que a câmera deve seguir.")]
    public Transform target;

    [Tooltip("Offset da câmera em relação ao alvo (em espaço local do alvo).")]
    public Vector3 offset = new Vector3(0, 5, -10);

    [Tooltip("Velocidade de suavização da posição da câmera.")]
    public float smoothSpeed = 5f;

    [Tooltip("Velocidade de suavização da rotação da câmera.")]
    public float rotationSmoothSpeed = 5f;

    [Tooltip("Índice do jogador (0 = topo, 1 = baixo).")]
    public int cameraIndex = 0;

    private Camera cam;
    private Vector3 currentVelocity;

    private void Start()
    {
        cam = GetComponent<Camera>();
        ConfigureViewport();
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Define a posição desejada com base na rotação Y do alvo
        Quaternion flatRotation = Quaternion.Euler(0, target.eulerAngles.y, 0);
        Vector3 desiredPosition = target.position + flatRotation * offset;

        // Suaviza a posição com SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, 1f / smoothSpeed);

        // Suaviza a rotação para olhar o alvo
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
    }

    private void ConfigureViewport()
    {
        if (cam == null) return;

        if (cameraIndex == 0)
        {
            cam.rect = new Rect(0f, 0.5f, 1f, 0.5f); // topo
        }
        else if (cameraIndex == 1)
        {
            cam.rect = new Rect(0f, 0f, 1f, 0.5f); // baixo
        }
        else
        {
            Debug.LogWarning($"[CameraFollow] Índice de câmera {cameraIndex} não é suportado para splitscreen de 2 jogadores.");
        }
    }
}