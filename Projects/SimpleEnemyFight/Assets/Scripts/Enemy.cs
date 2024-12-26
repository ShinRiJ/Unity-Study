using System;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Single _health;
    [SerializeField] public Single Damage;
    [SerializeField] private GameObject friendPrefab;

    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;
    public GameObject BuffTarget;

    private float _speed = 2.5f;

    public Enemy()
    {
        _health = 100;
        Damage = 5;
    }

    public void GetHit(Single damage)
    {
        _health -= damage;
    }

    public Boolean isAlive() => _health > 0;

    public String GetInfo() => $"HP: {_health}; DMG: {Damage}.";

    public void Update()
    {
        _text.text = ((Int32)_health) > 0 ? ((Int32)_health).ToString() : 0.ToString();

        if (_player.isAlive && gameObject is not null)
        {
            Vector3 direction;

            if (BuffTarget is not null)
                if(Vector3.Distance(transform.position, _player.transform.position) < Vector3.Distance(transform.position, BuffTarget.transform.position))
                    direction = Vector3.Normalize(_player.transform.position - gameObject.transform.position);
                else
                    direction = Vector3.Normalize(BuffTarget.transform.position - gameObject.transform.position);
            else
                direction = Vector3.Normalize(_player.transform.position - gameObject.transform.position);

            gameObject.transform.Translate(direction * Time.deltaTime * _speed, Space.World);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
#nullable enable
        Player? player = collision.gameObject.GetComponent<Player>();
        if (player is not null)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * 100 * 2.5f);
        }
#nullable disable
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Friend")
        {
            Friend target = new Friend();

            _health += target.Health;
            Destroy(other.gameObject);
            BuffTarget = Instantiate(friendPrefab, new Vector3(UnityEngine.Random.Range(-9, 9), 0, UnityEngine.Random.Range(-4, 9)), Quaternion.identity);
        }
    }
}
