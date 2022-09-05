using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleUnderProp : ObstacleMain
{
    public bool FinishPath;
    public override void ObstacleTrigger(PlayerControl playerControl)
    {
        float val = MainPlayer.Instance.allAddedSmallBlobs.Count - 3;
        if (FinishPath)
        {
            val += 3;
        }
        for (int i = 0; i < val; i++)
        {
            playerControl.ObstacleLavaTrigger();
        }
    }
}
