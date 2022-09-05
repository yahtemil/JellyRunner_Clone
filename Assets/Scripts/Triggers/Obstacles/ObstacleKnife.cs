using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleKnife : ObstacleMain
{
    public override void ObstacleTrigger(PlayerControl playerControl)
    {
        playerControl.ObstacleKnifeTrigger();
    }
}
