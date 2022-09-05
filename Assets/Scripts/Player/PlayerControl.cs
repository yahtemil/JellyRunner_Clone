using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    public Collider col;
    public Rigidbody rb;
    public Animator anim;
    public bool Active;
    public ParticleSystem[] EmojiEffects;
    public ParticleSystem DeathEffect;
    public ParticleSystem LavaTriggerEffect;
    public bool UpdateActive;
    public GameObject BloodObject;
    public GameObject TrailObject;

    public void DeathEffectPlay()
    {
        DeathEffect.gameObject.transform.localPosition = transform.localPosition;
        DeathEffect.gameObject.SetActive(true);
        gameObject.transform.parent.gameObject.transform.parent = null;
        gameObject.SetActive(false);
    }

    public virtual void ObstacleKnifeTrigger()
    {
        Debug.Log("Lava Trigger");
    }

    public virtual void ObstacleLavaTrigger() 
    {
        Debug.Log("Lava Trigger");
    }

    public virtual void ObstacleWallTrigger(ObstacleWall obstacleWall)
    {
        Debug.Log("Wall Trigger");
    }

    public virtual void EmojiPlay()
    {
        Debug.Log("Emejo Effect Play");
    }

    public virtual void FunJump()
    {

    }

    public virtual void FunRightLeft()
    {

    }

    public virtual void DikenliMazgalTrigger()
    {

    }

    public void AnimPlay(string PlayAnimText)
    {
        anim.Play(PlayAnimText);
    }

    public void CollectableCoinTrigger()
    {

    }

    public void CollectableBlobTrigger()
    {

    }

}
