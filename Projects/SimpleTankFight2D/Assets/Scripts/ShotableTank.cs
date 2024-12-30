using System;
using UnityEngine;

public abstract class ShotableTank : Tank
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _shootPoint;

    [SerializeField] protected Single _reloadTime = 0.5f;

    public void Shoot()
    {
        Instantiate(_projectile, _shootPoint.position, transform.rotation);
    }
}
