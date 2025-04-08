using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;
using UnityEngine.UI;

public class ModPowerUp : MonoBehaviour, IPowerUp
{
    // Start is called before the first frame update
    public string PowerUpName { get; set; }
    public string Description { get; set; }
    public int Chance { get; set; }
    [SerializeField] public Sprite icon;
    public virtual void PowerUpActive() { }
    public virtual void Initialize(ArcadeKart kart) { }

}
