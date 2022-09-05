using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLava : ObstacleMain
{
    public override void ObstacleTrigger(PlayerControl playerControl)
    {
        playerControl.ObstacleLavaTrigger();

        Debug.Log("Obstacle Wall Trigger");
    }
}
