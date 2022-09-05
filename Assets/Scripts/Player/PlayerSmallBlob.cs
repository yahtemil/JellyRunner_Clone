using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSmallBlob : PlayerControl
{
    float PosX;
    float _updateTime = 0f;

    private void Start()
    {
        MainPlayer.Instance.allAddedSmallBlobs.Add(this);
        if (MainPlayer.Instance.selectPlayerControl == MainPlayer.Instance.BigBlob)
        {
            gameObject.SetActive(false);
        }
        else
        {
            EmojiPlay();
        }
    }

    private void Update()
    {
        if (UpdateActive)
        {
            transform.transform.DOMoveX(PosX, 0.1f);
            _updateTime += Time.deltaTime;
            if (_updateTime >= 0.5f)
            {
                UpdateActive = false;
            }
        }
    }

    public override void EmojiPlay()
    {
        EmojiEffects[Random.Range(0, EmojiEffects.Length)].Play();
    }

    public void BlobSizeControl()
    {

    }

    public override void FunRightLeft()
    {
        if (!Active)
        {
            return;
        }
        float valX = transform.localPosition.x;
        transform.DOMoveX(4f, (4-transform.position.x) * 0.1f).OnComplete(() => transform.DOLocalMoveX(valX,0.3f));
    }

    public override void DikenliMazgalTrigger()
    {
        if (!Active)
        {
            return;
        }
        PosX = MainPlayer.Instance.DikenliMazgalGetPosX(transform.position.x);
        float endPosX = PosX - MainPlayer.Instance.BigBlob.transform.position.x;
        Vector3 localPos = transform.localPosition;
        localPos.x = endPosX;
        localPos.y = -0.5f;
        transform.DOLocalMove(localPos, 0.2f).OnComplete(() =>
        {
            MainPlayer.Instance.PlayerFallingDown(this);
            UpdateActive = true;
        });
    }

    public override void FunJump()
    {
        if (!Active)
        {
            return;
        }
        MainPlayer.Instance.FanJump();
    }

    public override void ObstacleKnifeTrigger()
    {
        if (!Active)
        {
            return;
        }
        MainPlayer.Instance.RemoveSmallBlob(this);
        DeathEffectPlay();
    }

    public override void ObstacleLavaTrigger()
    {
        if (!Active)
        {
            return;
        }
        MainPlayer.Instance.RemoveSmallBlob(this);
        LavaTriggerEffect.Play();
        gameObject.transform.parent.gameObject.transform.parent = null;
        gameObject.SetActive(false);
    }

    public override void ObstacleWallTrigger(ObstacleWall obstacleWall)
    {
        if (!Active)
        {
            return;
        }
        MainPlayer.Instance.RemoveSmallBlob(this); 
        DeathEffectPlay();
    }
}
