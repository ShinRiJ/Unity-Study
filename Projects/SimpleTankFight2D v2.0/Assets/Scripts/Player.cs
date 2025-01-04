using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : ShotableTank
{
    private Boolean _onReload = false;
    [SerializeField] private GameObject _boldGun;

    protected override void Move()
    {
        transform.Translate(Vector2.down * Input.GetAxis("Vertical") * _movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime * 20);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(0, 0, -_rotationSpeed * Time.deltaTime * 20);
    }

    public void Update()
    {
        Move();
        SetAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetMouseButton(0))
            StartCoroutine(ShotDelayCarotine());
            
    }

    public override void Shoot()
    {
        _pooler.SpawnFromPool(_projectileTag, _shootPoint.position, _boldGun.transform.rotation);
    }

    public override void SetAngle(Vector3 target)
    {
        Vector3 direction = target - _boldGun.transform.position;
        Single angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angle = Quaternion.Euler(0, 0, angleZ + _offsetRotation);
        _boldGun.transform.rotation = Quaternion.Lerp(_boldGun.transform.rotation, angle, _rotationSpeed);
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

    public override void RestoreHealth(int health)
    {
        base.RestoreHealth(health);
        _ui.UpdateHP(Health);
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
