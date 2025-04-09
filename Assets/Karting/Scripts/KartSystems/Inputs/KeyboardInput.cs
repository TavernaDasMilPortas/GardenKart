using UnityEngine;

namespace KartGame.KartSystems
{
    public class KeyboardInput : BaseInput
    {
        [Header("Grupo 1 - WASD + Q/E")]
        public string[] group1Keys = new string[6] { "w", "a", "s", "d", "q", "e" };

        [Header("Grupo 2 - IJKL + U/O")]
        public string[] group2Keys = new string[6] { "i", "j", "k", "l", "u", "o" };

        [Header("Configurações")]
        public int groupIndex = 0; // 0 para grupo 1, 1 para grupo 2

        public bool PowerUpLeftPressed { get; private set; }
        public bool PowerUpRightPressed { get; private set; }

        public override InputData GenerateInput()
        {
            string[] keys = groupIndex == 0 ? group1Keys : group2Keys;

            float turn = 0f;
            bool accelerate = false;
            bool brake = false;

            if (Input.GetKey(keys[0])) accelerate = true;      // W ou I
            if (Input.GetKey(keys[1])) turn = -1f;             // A ou J
            if (Input.GetKey(keys[2])) brake = true;           // S ou K
            if (Input.GetKey(keys[3])) turn = 1f;              // D ou L

            PowerUpLeftPressed = Input.GetKeyDown(keys[4]);    // Q ou U
            PowerUpRightPressed = Input.GetKeyDown(keys[5]);   // E ou O

            return new InputData
            {
                Accelerate = accelerate,
                Brake = brake,
                TurnInput = turn
            };
        }
    }
}