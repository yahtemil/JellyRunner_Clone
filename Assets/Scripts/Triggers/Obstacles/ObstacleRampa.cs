using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRampa : ObstacleMain
{
    public override void ObstacleTrigger(PlayerControl playerControl)
    {
        MainPlayer.Instance.RampaJump();
    }
}
