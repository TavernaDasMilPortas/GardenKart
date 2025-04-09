using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KartGame.KartSystems
{
    public class PlayerPlacement : MonoBehaviour
    {
        [SerializeField] public ArcadeKart kart;
        public int placement;
        public int lap ;
        public bool startRace = false;
        private void Awake()
        {
            lap = -1;
        }
    }
}
