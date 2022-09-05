using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameStates GameState;

    void Start()
    {
    }


    public void GameCompleted()
    {
        UIManager.Instance.CompletedPanelUI.SetActive(true);
        GameState = GameStates.Completed;
        Debug.Log("Game Completed");
    }

    public void GameFailed()
    {
        UIManager.Instance.FailedPanelUI.SetActive(true);
        GameState = GameStates.Failed;
        Debug.Log("Game Failed");
    }

    public enum GameStates
    {
        Start,
        Play,
        Failed,
        Completed,
        Finish
    }
}
