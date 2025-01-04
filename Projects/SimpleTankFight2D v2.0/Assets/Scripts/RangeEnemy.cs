using System;
using System.Collections;
using UnityEngine;

public class RangeEnemy : ShotableTank
{
    [SerializeField] private Single _distanceToPlayer = 5f;
    private Transform _target;
    [SerializeField] private Boolean _onReload = false;

    protected override void Move()
    {
        transform.Translate(Vector2.down * _movementSpeed * Time.deltaTime);
    }

    protected override void Start()
    {
        _target = GameObject.FindWithTag("Player").transform;
        base.Start();
    }

    private void Update()
    {
        if (_target != null)
        {
            SetAngle(_target.position);
            if(Vector2.Distance(transform.position, _target.position) > _distanceToPlayer)
                Move();
            else
                StartCoroutine(ShotDelayCarotine());
        }
    }

    private IEnumerator ShotDelayCarotine()
    {
        if (_onReload)
            yield break;
        Shoot();
        _onReload = true;
        yield return new WaitForSeconds(_reloadTime);
        _onReload = false;
    }
}
