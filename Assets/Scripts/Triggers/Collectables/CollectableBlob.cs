using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBlob : MonoBehaviour
{
    public PlayerControl playerControl;
    private bool activeCollectable = true;
    private float _animSpeed;
    public CollectableBlob collectableBlob;

    private void Start()
    {
        StartCoroutine(AnimTiming());
    }

    IEnumerator AnimTiming()
    {
        _animSpeed = playerControl.anim.speed;
        yield return new WaitForSeconds(0.1f);
        playerControl.anim.speed = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!activeCollectable)
            {
                return;
            }
            playerControl.anim.speed = _animSpeed;
            activeCollectable = false;
            transform.parent.gameObject.transform.parent = MainPlayer.Instance.childObject.transform;
            playerControl.enabled = true;
            if (MainPlayer.Instance.selectPlayerControl != MainPlayer.Instance.BigBlob)
            {
                playerControl.Active = true;
            }
            collectableBlob.enabled = false;
        }
    }
}
