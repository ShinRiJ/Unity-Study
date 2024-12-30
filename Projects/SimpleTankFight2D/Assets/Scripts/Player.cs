using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : ShotableTank
{
    private Boolean _onReload = false;

    protected override void Move()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody.linearVelocity = direction.normalized * _movementSpeed;
    }

    public void Update()
    {
        Move();
        SetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetMouseButton(0))
            StartCoroutine(ShotDelayCarotine());
            
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

    public override void TakeDamage(Int32 damage)
    {
        Health -= damage;
        _ui.UpdateHP(Health);

        if (Health <= 0)
        {
            Stats.ResetAllStats();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
