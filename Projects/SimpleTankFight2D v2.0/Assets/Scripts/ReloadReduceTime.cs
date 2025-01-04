using UnityEngine;
using System;

public class ReloadReduceTime : MonoBehaviour
{
    public event Action ReloadReduceUsed;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<ShotableTank>() != null)
        {
            collision.gameObject.GetComponent<ShotableTank>()?.ReloadReduceUsed(0.02f);
            ReloadReduceUsed?.Invoke();
        }
    }
}
