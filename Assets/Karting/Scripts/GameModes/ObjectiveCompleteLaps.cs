using System.Collections;
using UnityEngine;
using KartGame.Track;
using KartGame.KartSystems;

public class ObjectiveCompleteLaps : Objective
{
    [Tooltip("How many complete laps must a player finish to win?")]
    public int lapsToComplete = 3;

    [Header("Notification")]
    [Tooltip("Show notification when remaining laps are below this threshold")]
    public int notificationLapsRemainingThreshold = 1;

    private PlayerPlacement[] allPlayers;
    private bool isVictoryTriggered = false;

    void Awake()
    {
        if (string.IsNullOrEmpty(title))
            title = $"Complete {lapsToComplete} Laps";
    }

    IEnumerator Start()
    {
        TimeManager.OnSetTime(totalTimeInSecs, isTimed, gameMode);
        TimeDisplay.OnSetLaps(lapsToComplete);
        yield return new WaitForEndOfFrame();

        allPlayers = FindObjectsOfType<PlayerPlacement>();
        Register();
    }

    void Update()
    {
        if (isVictoryTriggered || allPlayers == null) return;

        foreach (var player in allPlayers)
        {
            if (!player.startRace) continue;

            int lapsRemaining = Mathf.Max(0, lapsToComplete - player.lap);
            Debug.Log($"[{player.kart.name}] Laps restantes: {lapsRemaining}, Colocação: {player.placement}");

            // Verifica se é o líder e completou todas as voltas
            if (player.placement == 1 && player.lap > lapsToComplete)
            {
                DeclareVictory(player);
                break;
            }
        }
    }

    void DeclareVictory(PlayerPlacement winner)
    {
        isVictoryTriggered = true;

        string message = $"🏁 {winner.kart.name} venceu a corrida!";
        Debug.Log(message);

        CompleteObjective(string.Empty, GetUpdatedCounterAmount(winner), message);
    }

    protected override void ReachCheckpoint(int remaining) { }

    public override string GetUpdatedCounterAmount()
    {
        if (allPlayers != null && allPlayers.Length > 0)
        {
            var first = allPlayers[0];
            return $"{first.lap} / {lapsToComplete}";
        }

        return $"0 / {lapsToComplete}";
    }

    string GetUpdatedCounterAmount(PlayerPlacement player)
    {
        return $"{player.lap} / {lapsToComplete}";
    }
}