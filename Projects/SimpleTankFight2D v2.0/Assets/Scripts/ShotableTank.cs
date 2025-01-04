using System;
using UnityEngine;

public abstract class ShotableTank : Tank
{
    [SerializeField] protected String _projectileTag;
    [SerializeField] protected Transform _shootPoint;
    [SerializeField] protected Single _reloadTime = 0.5f;

    protected ObjectPooler _pooler;

    protected override void Start()
    {
        base.Start();
        _pooler = ObjectPooler.Instance;
    }

    public virtual void Shoot()
    {
        _pooler.SpawnFromPool(_projectileTag, _shootPoint.position, transform.rotation);
    }

    public void ReloadReduceUsed(Single reduce)
    {
        _reloadTime = (_reloadTime - reduce) > 0.1 ? (_reloadTime - reduce) : 0.1f;
    }
}
