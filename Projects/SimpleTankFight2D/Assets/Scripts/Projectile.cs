using UnityEngine;
using System;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Int32 _damage = 5;
    [SerializeField] private Single _speed = 5f;
    [SerializeField] private String _myTag = "";
    private Vector2 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);

        if(Vector2.Distance(_startPos, transform.position) > 25)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Tank>() != null && collision.gameObject.tag != _myTag)
        {
            collision.gameObject.GetComponent<Tank>().TakeDamage(_damage);
            Destroy(gameObject);
        }
        
    }
}
