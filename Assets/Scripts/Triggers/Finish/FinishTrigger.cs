using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishTrigger : MonoSingleton<FinishTrigger>
{
    public Color[] BoxColorList;
    private int colorCounter;

    public Color GetRandomColor()
    {
        Color color = BoxColorList[colorCounter];
        colorCounter++;
        if (colorCounter > BoxColorList.Length - 1)
        {
            colorCounter = 0;
        }
        return color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameState = GameManager.GameStates.Finish;
            MainPlayer.Instance.BigBlobActive();
            MainPlayer.Instance.selectPlayerControl.TrailObject.SetActive(true);
            StartCoroutine(Timing());
        }
    }

    IEnumerator Timing()
    {
        yield return new WaitForSeconds(0.3f);
        float timeValue = MainPlayer.Instance.BigBlob.transform.localScale.x * 1f;
        MainPlayer.Instance.BigBlob.gameObject.transform.DOKill();
        MainPlayer.Instance.BigBlob.gameObject.transform.DOKill(true);
        MainPlayer.Instance.BigBlob.gameObject.transform.DOScale(Vector3.one * 0.2f, timeValue);
        yield return new WaitForSeconds(timeValue);
        if (GameManager.Instance.GameState == GameManager.GameStates.Finish)
        {
            LevelManager.Instance.PlayerPrefsSetGold(1);
            GameManager.Instance.GameState = GameManager.GameStates.Completed;
            MainPlayer.Instance.selectPlayerControl.DeathEffectPlay();
            UIManager.Instance.CompletedPanelUI.SetActive(true);
            LevelManager.Instance.PlayerPrefsSetLevel();
        }
    }
}
