using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDikenliMazgal : ObstacleMain
{
    public override void ObstacleTrigger(PlayerControl playerControl)
    {
        playerControl.DikenliMazgalTrigger();
    }
}
