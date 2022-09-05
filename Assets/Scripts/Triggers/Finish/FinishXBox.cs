using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FinishXBox : MonoBehaviour
{
    public TextMeshPro Xtext;
    public MeshRenderer meshRenderer;
    public int XValue;

    private void Start()
    {
        meshRenderer.material.color = FinishTrigger.Instance.GetRandomColor();
        Xtext.text = "x" + XValue.ToString();
        if (XValue != 10)
        {
            Vector3 pos = gameObject.transform.position;
            if (XValue %2 == 0)
            {
                pos.x = Random.Range(-2.5f, -1f);
            }
            else
            {
                pos.x = Random.Range(1, 2.5f);
            }

            gameObject.transform.position = pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameState = GameManager.GameStates.Completed;
            LevelManager.Instance.PlayerPrefsSetLevel();
            MainPlayer.Instance.selectPlayerControl.DeathEffectPlay();
            UIManager.Instance.CompletedPanelUI.SetActive(true);
            LevelManager.Instance.PlayerPrefsSetGold(XValue);
            float minPosX = transform.position.x - 1.75f;
            float maxPosX = transform.position.x + 1.75f;
            float bloodPosX = MainPlayer.Instance.BigBlob.BloodObject.transform.position.x;
            if (bloodPosX >= minPosX && bloodPosX <= maxPosX)
            {
                MainPlayer.Instance.selectPlayerControl.BloodObject.SetActive(true);
                MainPlayer.Instance.selectPlayerControl.BloodObject.transform.DOScale(Vector3.one * 0.5f, 1.5f);
            }
        }
    }
}
