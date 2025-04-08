using System.Collections.Generic;
using UnityEngine;

public class WayPointPath : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();

    public Transform GetWaypoint(int index)
    {
        if (index < 0 || index >= waypoints.Count) return null;
        return waypoints[index];
    }

    public int GetNextIndex(int currentIndex)
    {
        return (currentIndex + 1) % waypoints.Count;
    }

    public int Count => waypoints.Count;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (int i = 0; i < waypoints.Count; i++)
        {
            if (waypoints[i] == null) continue;

            Gizmos.DrawSphere(waypoints[i].position, 0.5f);

            int nextIndex = (i + 1) % waypoints.Count;
            if (waypoints[nextIndex] != null)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[nextIndex].position);
            }
        }
    }
}
