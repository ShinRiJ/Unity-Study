using System;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    [SerializeField] protected Int32 _maxHealth = 30;
    [SerializeField] protected Single _movementSpeed = 3f;
    [SerializeField] private Single _rotationSpeed = 7f;
    [SerializeField] private Single _offsetRotation = 90f;
    [SerializeField] private Int32 _points = 0;

    protected UI _ui;
    protected Rigidbody2D _rigidbody;
    public Int32 Health { get; protected set; }

    virtual protected void Start()
    {
        Health = _maxHealth;
        _rigidbody = GetComponent<Rigidbody2D>();
        _ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }

    public virtual void TakeDamage(Int32 damage)
    { 
        Health -= damage;

        if (Health <= 0)
        {
            Stats.Score += _points;
            _ui.UpdateScoreAndLevel();
            Destroy(gameObject);
        }
    }

    protected abstract void Move();

    public void SetAngle(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        Single angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angle = Quaternion.Euler(0, 0, angleZ + _offsetRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, angle, _rotationSpeed);
    }


}
