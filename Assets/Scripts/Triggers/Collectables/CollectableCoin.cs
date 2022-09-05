using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    public ParticleSystem CoinEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.AddGold();
            CoinEffect.Play();
            gameObject.SetActive(false);
        }
    }
}
