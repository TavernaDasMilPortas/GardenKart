using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;
using TMPro;

public class PlaceManegement : MonoBehaviour
{
    [System.Serializable]
    public class Placement
    {
        public ArcadeKart kart;
        public int atualPlace;
        public int lap;
        public float progress;
        public TextMeshProUGUI uiText;
       
        public Placement Initialize(ArcadeKart kart, int atualPlace, int lap, TextMeshProUGUI uiText)
        {
            this.kart = kart;
            this.atualPlace = atualPlace;
            this.lap = lap;
            this.uiText = uiText;
            return this;
        }
    }

    [SerializeField] List<Placement> placementList = new List<Placement>();

    [Header("Path Settings")]
    [SerializeField] List<Transform> waypoints;
    [SerializeField] Transform startLine;
    [SerializeField] Transform finishLine;
    [SerializeField] Transform endLine;

    [Header("UI")]
    [SerializeField] Transform placementPanel;
    [SerializeField] TextMeshProUGUI placementTextPrefab;

    void Start()
    {
        foreach (var karts in FindObjectsOfType<PlayerPlacement>())
        {
            var newText = Instantiate(placementTextPrefab, placementPanel);
            var placement = new Placement().Initialize(karts.kart, karts.placement, karts.lap, newText);
            placementList.Add(placement);
        }
    }

    void Update()
    {
        // Garante que o valor da lap esteja sempre atualizado
        foreach (var placement in placementList)
        {
            var player = placement.kart.GetComponent<PlayerPlacement>();
            if (player != null)
            {
                placement.lap = player.lap; // sincroniza valor
            }
        }

        PlacementAtualize();
    }

    public void PlacementAtualize()
    {
        foreach (var placement in placementList)
        {
            placement.progress = CalculateProgressAlongPath(placement.kart.transform.position);
        }

        placementList.Sort((a, b) =>
        {
            int lapComparison = b.lap.CompareTo(a.lap);
            if (lapComparison != 0)
                return lapComparison;

            return b.progress.CompareTo(a.progress); // maior progresso = melhor colocação
        });

        for (int i = 0; i < placementList.Count; i++)
        {
            var placement = placementList[i];
            placement.atualPlace = i + 1;

            // Atualiza o texto
            placement.uiText.text = $"{placement.atualPlace}º - {placement.kart.name}";

            // Atualiza também no componente PlayerPlacement
            var playerPlacement = placement.kart.GetComponent<PlayerPlacement>();
            if (playerPlacement != null)
            {
                playerPlacement.placement = placement.atualPlace;
                playerPlacement.lap = placement.lap;
            }
        }
    }

    float CalculateProgressAlongPath(Vector3 position)
    {
        if (waypoints == null || waypoints.Count < 2)
            return 0f;

        float totalProgress = 0f;
        float closestDistance = float.MaxValue;
        float progressAtClosest = 0f;

        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Vector3 a = waypoints[i].position;
            Vector3 b = waypoints[i + 1].position;
            float segmentLength = Vector3.Distance(a, b);

            Vector3 projected = ProjectPointOnLineSegment(a, b, position);
            float distanceToProjected = Vector3.Distance(projected, position);

            totalProgress += segmentLength;

            if (distanceToProjected < closestDistance)
            {
                closestDistance = distanceToProjected;

                // Distância total até o ponto projetado
                float progressOnSegment = Vector3.Distance(a, projected);
                float progressBefore = 0f;

                for (int j = 0; j < i; j++)
                {
                    progressBefore += Vector3.Distance(waypoints[j].position, waypoints[j + 1].position);
                }

                progressAtClosest = progressBefore + progressOnSegment;
            }
        }

        return progressAtClosest;
    }

    Vector3 ProjectPointOnLineSegment(Vector3 a, Vector3 b, Vector3 point)
    {
        Vector3 ab = b - a;
        float abSquared = Vector3.SqrMagnitude(ab);
        if (abSquared == 0f) return a;

        float t = Vector3.Dot(point - a, ab) / abSquared;
        t = Mathf.Clamp01(t);
        return a + ab * t;
    }

    public List<Placement> GetPlacementList()
    {
        return placementList;
    }
}
