using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleUnderProp : ObstacleMain
{
    public bool FinishPath;
    public override void ObstacleTrigger(PlayerControl playerControl)
    {
        float val = playerControl.gameObject.transform.localScale.x;
        float cal = (val - 1.5f) / 0.375f;
        if (FinishPath)
        {
            cal += 3;
        }
        for (int i = 0; i < cal; i++)
        {
            playerControl.ObstacleLavaTrigger();
        }
    }
}
