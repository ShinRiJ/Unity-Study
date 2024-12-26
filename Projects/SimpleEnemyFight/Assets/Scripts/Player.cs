using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Single _speed = 5.0f;
    [SerializeField] private List<Friend> myArmy;
    [SerializeField] private GameObject friendPrefab;
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private TMP_Text _text;

    private Vector3 previousPosition;
    private GameObject currentFriend;
    public Boolean isAlive = true;

    void Start()
    {
        myArmy = new List<Friend>();
        previousPosition = transform.position;
        currentFriend = Instantiate(friendPrefab, new Vector3(2, 0, -2), Quaternion.identity);
        enemyObject.GetComponent<Enemy>().BuffTarget = currentFriend;
    }

    // Update is called once per frame
    void Update()
    {

        _text.text = myArmy.Count.ToString();

        if (gameObject is null)
            return;
        
        Vector3 displacement = Vector3.Normalize((transform.position - previousPosition) / Time.deltaTime);
        previousPosition = transform.position;

        if (-0.05 < transform.position.y  && transform.position.y < 0.5)
        {
            if (Input.GetAxis("Horizontal") != 0)
                gameObject.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * _speed, Space.World);

            if (Input.GetAxis("Vertical") != 0)
                gameObject.transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * _speed, Space.World);

            if (Input.GetKeyDown(KeyCode.Space))
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(displacement.x, 1.5f, displacement.z) * 100 * 2f);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
#nullable enable
        Enemy? myEnemy = collision.gameObject.GetComponent<Enemy>();
        if (myEnemy is not null)
        {
            
            while (true)
            {
                if (myArmy.Count > 0)
                {
                    if (myArmy[0].isAlive())
                        myEnemy.GetHit(myArmy[0].Damage);
                    else
                    {
                        myArmy.RemoveAt(0);
                        continue;
                    }

                    myArmy[0].GetHit(myEnemy.Damage);

                    print($"Мой боец: {myArmy[0].GetInfo()}");
                    print($"Противник: {myEnemy.GetInfo()}");
                }
                break;
            }

            if (myArmy.Count == 0)
            {
                Destroy(gameObject);
                isAlive = false;
            }

            if (!myEnemy.isAlive())
                Destroy(collision.gameObject);

            gameObject.GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * 100 * 2.5f);
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10);
        }
#nullable disable
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Friend")
        {
            myArmy.Add(new Friend());
            print(myArmy[myArmy.Count - 1].GetInfo());

            Destroy(other.gameObject);
            currentFriend = Instantiate(friendPrefab, new Vector3(UnityEngine.Random.Range(-9, 9), 0, UnityEngine.Random.Range(-4, 9)), Quaternion.identity);
            if(enemyObject.gameObject is not null)
                enemyObject.GetComponent<Enemy>().BuffTarget = currentFriend;
        }    
    }
}

[Serializable]
public class Friend
{
    public Single Health;
    public Single Damage;

    public Friend()
    {
        Health = UnityEngine.Random.Range(10, 30);
        Damage = UnityEngine.Random.Range(2, 10);
    }

    public void GetHit(Single damage)
    {
        Health -= damage;
    }

    public Boolean isAlive() => Health > 0;

    public String GetInfo() => $"HP: {Health}; DMG: {Damage}.";
}
