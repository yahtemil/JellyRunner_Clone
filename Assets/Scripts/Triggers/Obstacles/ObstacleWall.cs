using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWall : ObstacleMain
{
    public Rigidbody[] AllRb;

    private void Start()
    {
        for (int i = 0; i < AllRb.Length; i++)
        {
            float xPos = -3.5f + (i % 9) * 1f;
            float yPos = 0.25f + (i / 9) * 0.5f;
            if ((i/9)%2 == 1)
            {
                xPos = -4f + (i % 9) * 1f;
                yPos = 0.25f + (i / 9) * 0.5f;
            }
            else
            {
                if (i%9 == 8)
                {
                    AllRb[i].gameObject.SetActive(false);
                }
            }
            AllRb[i].gameObject.transform.localPosition = new Vector3(xPos, yPos, 0f);
        }
    }

    IEnumerator AllRbForwardAddForce()
    {
        Vector3 childObjectPos = MainPlayer.Instance.childObject.transform.position;
        yield return new WaitForSeconds(0f);
        for (int i = 0; i < AllRb.Length; i++)
        {
            AllRb[i].isKinematic = false;
            float force = 4500f / Mathf.Abs(Vector3.Distance(AllRb[i].transform.position, childObjectPos));
            AllRb[i].AddForce(Vector3.forward * force);
        }
    }

    public void ForwardAddForce()
    {
        StartCoroutine(AllRbForwardAddForce());
    }

    public override void ObstacleTrigger(PlayerControl playerControl)
    {
        playerControl.ObstacleWallTrigger(this);
        
        Debug.Log("Obstacle Wall Trigger");
    }

    public override void AnimPlay()
    {
        Debug.Log("There is no animation in this obstacle.");
    }
}
