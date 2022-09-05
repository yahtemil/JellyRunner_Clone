using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMain : MonoBehaviour
{
    public virtual void ObstacleTrigger(PlayerControl playerControl)
    {
        Debug.Log("Obstacle Trigger");
    }

    public virtual void ObstacleTriggerExit(PlayerControl playerControl)
    {
        Debug.Log("Obstacle Trigger Exit");
    }


    public virtual void AnimPlay()
    {
        Debug.Log("Animation Start");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControl playerControl = other.GetComponent<PlayerControl>();
            ObstacleTrigger(playerControl);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        }
    }
}
