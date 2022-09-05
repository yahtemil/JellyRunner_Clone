using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBigBlob : PlayerControl
{
    private void Start()
    {
        MainPlayer.Instance.BigBlob = this;
        MainPlayer.Instance.selectPlayerControl = this;
        anim.Play("Idle");
        int randomEmojiEffectValue = Random.Range(0, EmojiEffects.Length);
        EmojiEffects[randomEmojiEffectValue].gameObject.SetActive(true);
        EmojiEffects[randomEmojiEffectValue].Play();
    }

    public override void EmojiPlay()
    {
        
    }

    public override void FunJump()
    {

    }
    public override void FunRightLeft()
    {
        if (!Active)
        {
            return;
        }
    }

    public override void DikenliMazgalTrigger()
    {
        if (!Active)
        {
            return;
        }
    }

    public override void ObstacleKnifeTrigger()
    {
        if (!Active)
        {
            return;
        }
        MainPlayer.Instance.RemoveSmallBlob(null);
    }

    public override void ObstacleLavaTrigger()
    {
        if (!Active)
        {
            return;
        }
        MainPlayer.Instance.RemoveSmallBlob(null);
    }

    public override void ObstacleWallTrigger(ObstacleWall obstacleWall)
    {
        if (!Active)
        {
            return;
        }
        obstacleWall.ForwardAddForce();
        anim.Play("Jump");
        MainPlayer.Instance.CameraShake();
        Debug.Log("Big blob trigger");
    }
}
