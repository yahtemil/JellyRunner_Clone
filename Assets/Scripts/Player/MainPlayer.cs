using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class MainPlayer : MonoSingleton<MainPlayer>
{
    #region Serialized Variables

    public float Speed = 20f;

    public CinemachineVirtualCamera vcam;
    [HideInInspector]
    public CinemachineBasicMultiChannelPerlin vcamPerlin;
    [HideInInspector]
    public List<PlayerControl> allAddedSmallBlobs;
    [HideInInspector]
    public PlayerControl BigBlob;
    [HideInInspector]
    public PlayerControl selectPlayerControl;

    public GameObject childObject;

    private float firstPosX;

    private float minValueX = -4f;
    private float maxValueX = 4f;

    public ParticleSystem PingEffect;

    [HideInInspector]
    public int SmallBlobCounter = 0;
    private int jumpCounter = 0;

    #endregion

    #region Start Operations

    private void Start()
    {
        vcamPerlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    #endregion

    #region Update Operations
    private void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.Play
            || GameManager.Instance.GameState == GameManager.GameStates.Finish)
        {
            transform.position = transform.position + new Vector3(0f, 0f, Time.deltaTime * Speed);
        }
    }

    public void ClickDown()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.Failed
            || GameManager.Instance.GameState == GameManager.GameStates.Completed)
        {
            return;
        }
        firstPosX = childObject.transform.localPosition.x;
        SmallBlobsActive();

    }
    public void Click()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.Failed
            || GameManager.Instance.GameState == GameManager.GameStates.Completed)
        {
            return;
        }
        float inputValueX = SwerveConrol.Instance.GetLeftRightSlide();
        float EndPosX = firstPosX + inputValueX;
        if (EndPosX <= minValueX)
        {
            EndPosX = minValueX;
        }
        else if (EndPosX >= maxValueX)
        {
            EndPosX = maxValueX;
        }
        float rotateY = (EndPosX - childObject.transform.localPosition.x) * 25f;
        childObject.transform.DOLocalMoveX(EndPosX, 0.2f);
        BlobMoveRotateControl(rotateY);
    }

    public void ClickUp()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.Failed
            || GameManager.Instance.GameState == GameManager.GameStates.Completed)
        {
            return;
        }
        BlobMoveRotateControl(0f);
        BigBlobActive();        
        
    }

    #endregion

    #region Blob Operations

    public void AnimPlay(string PlayAnimText)
    {
        for (int i = 0; i < allAddedSmallBlobs.Count; i++)
        {
            allAddedSmallBlobs[i].AnimPlay(PlayAnimText);
        }
        BigBlob.AnimPlay(PlayAnimText);
    }

    public void BigBlobActive()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.Failed
            || GameManager.Instance.GameState == GameManager.GameStates.Completed
            || selectPlayerControl == BigBlob)
        {
            return;
        }
        SmallBlobCounter = 0;
        BigBlob.transform.localScale = Vector3.one * 0.75f;
        BigBlob.Active = true;
        for (int i = 0; i < allAddedSmallBlobs.Count; i++)
        {
            allAddedSmallBlobs[i].Active = false;
            allAddedSmallBlobs[i].transform.DOKill();
            float timing = Vector3.Distance(allAddedSmallBlobs[i].transform.position, BigBlob.transform.position) * 0.075f;
            SmallBlobClose(timing,i);
            allAddedSmallBlobs[i].transform.DOLocalMove(Vector3.zero, timing);
        }
        BigBlob.gameObject.transform.DOLocalRotate(Vector3.zero, 0.01f);
        selectPlayerControl = BigBlob;
        vcam.m_Follow = childObject.transform;
    }

    public void SmallBlobsActive()
    {
        if (GameManager.Instance.GameState != GameManager.GameStates.Play)
        {
            return;
        }
        PingEffect.Simulate(0f, true, true);
        PingEffect.Play();
        for (int i = 0; i < allAddedSmallBlobs.Count; i++)
        {
            allAddedSmallBlobs[i].gameObject.SetActive(true);
            allAddedSmallBlobs[i].Active = true;
            allAddedSmallBlobs[i].transform.DOKill();
            allAddedSmallBlobs[i].transform.DOLocalMove(RandomPos(), 0.25f);
            allAddedSmallBlobs[i].transform.DOLocalRotate(Vector3.zero, 0.01f);
            allAddedSmallBlobs[i].EmojiPlay();
        }
        BigBlob.Active = false;
        BigBlob.gameObject.SetActive(false);
        selectPlayerControl = allAddedSmallBlobs[0];
        vcam.m_Follow = allAddedSmallBlobs[0].transform;
    }

    public Vector3 RandomPos()
    {
        float randomMax = 2f;
        float randomMin = -2f;
        if (BigBlob.transform.position.x + 2f >= 4f)
        {
            randomMax = 0f;
            randomMin = -3f;
        }
        else if (BigBlob.transform.position.x - 2f <= -4f)
        {
            randomMax = 3f;
            randomMin = 0f;
        }
        Vector3 pos = new Vector3(Random.Range(randomMin, randomMax), 0f, Random.Range(-0.5f, 0.5f));
        return pos;
    }

    private void SmallBlobClose(float timing, int i)
    {
        StartCoroutine(SmallBlobCloseTiming(timing, i));
    }

    private IEnumerator SmallBlobCloseTiming(float timing,int i)
    {
        yield return new WaitForSeconds(timing);
        if (selectPlayerControl != BigBlob)
        {
            yield break;
        }
        SmallBlobCounter++;
        BigBlob.gameObject.SetActive(true);
        BigBlob.gameObject.transform.DOKill();
        BigBlob.gameObject.transform.DOScale(Vector3.one * 0.75f + Vector3.one * (0.375f * SmallBlobCounter), 0.2f);
        BigBlob.gameObject.transform.DOLocalRotate(Vector3.zero, 0.01f);
        if (i <= allAddedSmallBlobs.Count -1)
        {
            allAddedSmallBlobs[i].gameObject.SetActive(false);
        }
    }

    public void BlobMoveRotateControl(float RotateY)
    {
        for (int i = 0; i < allAddedSmallBlobs.Count; i++)
        {
            allAddedSmallBlobs[i].transform.DOLocalRotate(new Vector3(0f, RotateY, 0f), 0.2f);
            Vector3 pos = allAddedSmallBlobs[i].transform.position;
            if (pos.x < -4f)
            {
                pos.x = -4f;
                allAddedSmallBlobs[i].transform.position = pos;
            }
            else if (pos.x > 4f)
            {
                pos.x = 4f;
                allAddedSmallBlobs[i].transform.position = pos;
            }
        }
        BigBlob.transform.DOLocalRotate(new Vector3(0f, RotateY, 0f), 0.2f);
    }
    public void AddSmallBlob(PlayerControl addSmallBob)
    {
        allAddedSmallBlobs.Add(addSmallBob);
    }

    public void RemoveSmallBlob(PlayerControl deleteSmallBob)
    {
        if (allAddedSmallBlobs.Count - 1 < 0)
        {
            return;
        }
        if (deleteSmallBob == null)
        {
            deleteSmallBob = allAddedSmallBlobs[allAddedSmallBlobs.Count - 1];
        }
        allAddedSmallBlobs.Remove(deleteSmallBob);
        deleteSmallBob.gameObject.transform.parent.gameObject.transform.parent = null;
        Destroy(deleteSmallBob.gameObject);
        int SmallBlobCount = allAddedSmallBlobs.Count;
        Vector3 FinalScaleValue = Vector3.one * 0.75f + Vector3.one * (0.375f * SmallBlobCount);
        if (SmallBlobCount <= 0)
        {
            FinalScaleValue = Vector3.zero;
            GameManager.Instance.GameFailed();
        }
        BigBlob.gameObject.transform.DOScale(FinalScaleValue, 0.2f);
        vcamFollowControl();
    }

    public void MinMaxBlobValue()
    {
        minValueX = 0;
        maxValueX = 0;
        for (int i = 0; i < allAddedSmallBlobs.Count; i++)
        {
            float localPosX = allAddedSmallBlobs[i].transform.localPosition.x;
            if (localPosX < minValueX)
            {
                minValueX = localPosX;
            }
            else if (localPosX > maxValueX)
            {
                maxValueX = localPosX;
            }
        }
        minValueX = -4 - minValueX;
        maxValueX = 4 - maxValueX;
    }

    public void RotateControl()
    {
        float pos = allAddedSmallBlobs[0].transform.position.x;
        childObject.transform.DOMoveX(pos, 0f);
        for (int i = 0; i < allAddedSmallBlobs.Count; i++)
        {
            allAddedSmallBlobs[i].transform.DOLocalMoveX(allAddedSmallBlobs[i].transform.position.x - pos,0f); 
        }
    }

    #endregion

    #region Physics Operations

    public void PlayerFallingDown(PlayerControl playerControl)
    {
        StartCoroutine(FallingDownTiming(playerControl));
        //if (selectPlayerControl != BigBlob)
        //{
        //    childObject.transform.DOKill();
        //    childObject.transform.DOLocalMoveY(-10.5f, 0.8f);
        //}
    }

    IEnumerator FallingDownTiming(PlayerControl playerControl)
    {
        yield return new WaitForSeconds(0.1f);
        if (selectPlayerControl != BigBlob && allAddedSmallBlobs.Count > 0)
        {
            playerControl.transform.DOLocalMoveY(0f, 0.2f);
            childObject.transform.DOKill();
            childObject.transform.DOLocalMoveY(-10.5f, 0.8f);
        }
    }

    public float DikenliMazgalGetPosX(float ValueX)
    {
        float Value = 0.6f;
        float selectValue = Mathf.Abs(ValueX);
        float distance = 50f;
        for (int i = 0; i < 4; i++)
        {
            float val = (0.6f) + (1.3f * i);
            float distance3 = Mathf.Abs(selectValue - val);
            if (distance3 <= distance)
            {
                distance = distance3;
                Value = val;
            }
        }
        if (ValueX < 0)
        {
            Value *= -1f;
        }
        return Value;
    }

    public void RampaJump()
    {
        //childObject.transform.DOKill();
        jumpCounter++;
        AnimPlay("BigJump");
        StartCoroutine(JumpTiming(jumpCounter,6f,0.25f,0.5f));        
    }

    IEnumerator JumpTiming(int counter,float height,float timing,float timing2)
    {
        childObject.transform.DOLocalMoveY(height, timing).SetEase(Ease.Linear).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(timing);
        if (counter == jumpCounter)
        {
            childObject.transform.DOLocalMoveY(0f, timing2).SetEase(Ease.Linear).SetEase(Ease.InCubic);
        }
    }

    public void FanJumpBlob()
    {
        selectPlayerControl.FunJump();
    }

    public void FanRightLeftBlob()
    {
        selectPlayerControl.FunRightLeft();
    }

    public void FanJump()
    {
        //childObject.transform.DOKill();
        jumpCounter++;
        float y = childObject.transform.localPosition.y;
        StartCoroutine(JumpTiming(jumpCounter,y+5f,0.5f,0.5f));
    }
    

    #endregion

    #region Camera Control

    public void vcamFollowControl()
    {
        if (selectPlayerControl != BigBlob)
        {
            if (allAddedSmallBlobs.Count >= 1)
            {
                selectPlayerControl = allAddedSmallBlobs[0];
                vcam.m_Follow = selectPlayerControl.transform;
            }
        }
    }

    public void CameraShake()
    {
        StartCoroutine(CameraShakeTiming());
    }

    IEnumerator CameraShakeTiming()
    {
        vcamPerlin.m_AmplitudeGain = 3;
        vcamPerlin.m_FrequencyGain = 0.05f;
        yield return new WaitForSeconds(0.5f);
        vcamPerlin.m_AmplitudeGain = 0;
        vcamPerlin.m_FrequencyGain = 0;
    }

    #endregion
}
