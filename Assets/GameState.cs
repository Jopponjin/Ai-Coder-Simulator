using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BH
{
    public class GameState : MonoBehaviour
    {
        public enum CurrentGameState
        {
            None,
            Editor,
            Game,
            Menu
        }


        public void PauseGame()
        {

        }

        public void ExitLevel()
        {

        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }
}

