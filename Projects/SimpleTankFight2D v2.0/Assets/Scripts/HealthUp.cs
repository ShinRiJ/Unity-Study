using System;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    public event Action HealthUpUsed;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Tank>()?.RestoreHealth(2);
        HealthUpUsed?.Invoke();
    }
}
