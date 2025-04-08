using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;
using UnityEngine.UI;

public interface IPowerUp
{
    string PowerUpName { get; set; }
    string Description { get; set; }
    int Chance { get; set; }
    void PowerUpActive();
    void Initialize(ArcadeKart kart);
}