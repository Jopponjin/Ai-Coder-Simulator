using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BH.Data;

namespace BH
{
    public class GameState : MonoBehaviour
    {
        public GameEvent onGameStartEvent;
        public GameEvent onGameStopEvent;
        public GameEvent onGameQuitEvent;
        public GameEvent onEditorResetEvent;

        public enum CurrentGameState
        {
            None,
            Editor,
            Game,
            Menu
        }

        public CurrentGameState currentGameState;

        public void StartGame()
        {
            onGameStartEvent.Raise();
            currentGameState = CurrentGameState.Game;
        }

        public void PauseGame()
        {
            currentGameState = CurrentGameState.Menu;
        }

        public void StopGame()
        {
            onGameStopEvent.Raise();
            currentGameState = CurrentGameState.Editor;
        }

        public void ExitGame()
        {
            onGameQuitEvent.Raise();
            currentGameState = CurrentGameState.Editor;
        }


        #region DebugStates

        public void ResetGame()
        {
            onEditorResetEvent.Raise();
            currentGameState = CurrentGameState.Editor;
        }

        #endregion


        public void QuitGame()
        {
            currentGameState = CurrentGameState.None;
            Application.Quit();
        }

    }
}

