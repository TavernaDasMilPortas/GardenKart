using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;
using UnityEngine.UI;

public class PowerUpIcon : MonoBehaviour
{
    [SerializeField] public PlayerPowerUp kart;
    public Image imagem;
    public Sprite imagemBase;
    private void Update()
    {
        if (kart.currentlyPowerUp != null)
        {
            imagem.sprite = kart.currentlyPowerUp.icon;
        }
        else
        {
            imagem.sprite = imagemBase;
        }
    }
}
