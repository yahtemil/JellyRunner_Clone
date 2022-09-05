using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFan : ObstacleMain
{
    Collider col;
    public bool Right;

    private void Start()
    {
        col = GetComponent<Collider>();
    }
    public override void ObstacleTrigger(PlayerControl playerControl)
    {
        if (!Right)
        {
            MainPlayer.Instance.FanJumpBlob();
            col.enabled = false;
        }
        else
        {
            playerControl.FunRightLeft();
        }

    }
}
