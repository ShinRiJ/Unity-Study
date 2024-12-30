using System;
using System.Collections;
using UnityEngine;

public class MeleeEnemy : Tank
{
    [SerializeField] private Int32 _damage = 5;
    private Transform _target;
    private Single _cooldown = 1;
    [SerializeField]private Boolean _onReload = false;

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
        if(_target != null && !_onReload)
        {
            Move();
            SetAngle(_target.position);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
            StartCoroutine(KickDelayCarotine(collision.gameObject));
    }

    private IEnumerator KickDelayCarotine(GameObject obj)
    {
        if (_onReload)
            yield break;
        obj.gameObject.GetComponent<Player>().TakeDamage(_damage);
        _onReload = true;
        yield return new WaitForSeconds(_cooldown);
        _onReload = false;
        _rigidbody.linearVelocity = Vector3.zero;
    }
}
