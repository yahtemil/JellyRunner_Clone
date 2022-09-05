using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject StartPanelUI;
    public GameObject GamePanelUI;
    public GameObject CompletedPanelUI;
    public GameObject FailedPanelUI;

    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI CoinText;

    public TextMeshProUGUI ScoreTextXValue;
    public TextMeshProUGUI ScoreTextAddValue;

    public void StartButton()
    {
        GameManager.Instance.GameState = GameManager.GameStates.Play;
        MainPlayer.Instance.AnimPlay("Run");
    }

    public void CompletedButton()
    {
        SceneManager.LoadScene(0);        
    }

    public void FailedButton()
    {
        SceneManager.LoadScene(0);
    }

    public void GoldTextWrite()
    {

        CoinText.text = LevelManager.Instance.CoinValue.ToString();
    }
    public void LevelTextWrite()
    {
        LevelText.text = "Level " + LevelManager.Instance.LevelValue.ToString();
    }

    public void AddGold()
    {
        LevelManager.Instance.AddCoinValue++;
        CoinText.text = (LevelManager.Instance.AddCoinValue + LevelManager.Instance.CoinValue).ToString();
    }
}
