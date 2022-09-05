using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoSingleton<LevelManager>
{
    public GameObject[] FinishAnimCoins;
    [HideInInspector]
    public int LevelValue;
    [HideInInspector]
    public int CoinValue;
    [HideInInspector]
    public int AddCoinValue;

    private void Start()
    {
        LevelValue = PlayerPrefs.GetInt("Level", 1);
        CoinValue = PlayerPrefs.GetInt("Coin", 0);

        UIManager.Instance.GoldTextWrite();
        UIManager.Instance.LevelTextWrite();
        LoadLevel();
    }

    public void PlayerPrefsSetGold(int val)
    {
        if (AddCoinValue > 0)
        {
            CoinValue += (AddCoinValue * val);
            PlayerPrefs.SetInt("Coin", CoinValue);
            UIManager.Instance.ScoreTextXValue.gameObject.SetActive(true);
            UIManager.Instance.ScoreTextAddValue.gameObject.SetActive(true);
            UIManager.Instance.ScoreTextXValue.text = "x" + val.ToString();
            UIManager.Instance.ScoreTextAddValue.text = "+" + (AddCoinValue * val).ToString();
            StartCoroutine(FinishCoinAnim());
        }
    }

    IEnumerator FinishCoinAnim()
    {
        for (int i = 0; i < FinishAnimCoins.Length; i++)
        {
            FinishAnimCoins[i].gameObject.SetActive(true);
            FinishAnimCoins[i].transform.localPosition = new Vector3(Random.Range(-0.7f, 0.7f), Random.Range(-0.7f, 0.7f), 7.84f);

        }
        for (int i = 0; i < FinishAnimCoins.Length; i++)
        {
            FinishAnimCoins[i].transform.DOLocalMove(new Vector3(1.86f, 3.2f, 6.92f), 0.5f);
            FinishAnimCoins[i].transform.DOScale(new Vector3(20f, 20f, 31.46513f), 0.5f);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.35f);
        UIManager.Instance.GoldTextWrite();
        for (int i = 0; i < FinishAnimCoins.Length; i++)
        {
            FinishAnimCoins[i].gameObject.SetActive(false);
        }
    }

    public void PlayerPrefsSetLevel()
    {
        PlayerPrefs.SetInt("Level", LevelValue + 1);
    }

    public void LoadLevel()
    {
        int lvl = LevelValue % 2;
        if (lvl == 0)
        {
            lvl = 2;
        }
        GameObject level = Resources.Load<GameObject>("Levels/level" + lvl.ToString());
        Instantiate(level);
    }
}
