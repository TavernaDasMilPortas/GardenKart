using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    [SerializeField] public ModPowerUp currentlyPowerUp;

    // Update is called once per frame
    void Update()
    {
        if (currentlyPowerUp != null)
        {
            if (Input.GetKeyDown(KeyCode.Q)) // cima
            {
                if (currentlyPowerUp is SeekingProjectliePowerUp redShell)
                {
                    redShell.SetDirection(true); // true = acima
                }
                currentlyPowerUp.PowerUpActive();
                currentlyPowerUp = null;
            }
            else if (Input.GetKeyDown(KeyCode.E)) // baixo
            {
                if (currentlyPowerUp is SeekingProjectliePowerUp redShell)
                {
                    redShell.SetDirection(false); // false = abaixo
                }
                currentlyPowerUp.PowerUpActive();
                currentlyPowerUp = null;
            }
            
        }
    }
}
