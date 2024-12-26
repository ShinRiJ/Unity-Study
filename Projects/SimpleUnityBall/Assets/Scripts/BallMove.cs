using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallMove : MonoBehaviour
{
    private Rigidbody ball;
    public Boolean isStay = false;

    [SerializeField] private Single powerImpulse = 80F;

    private void Start()
    {
        ball = gameObject.GetComponent<Rigidbody>();
    }

    public void MoveUp()
    {
        ball.AddForce(Vector3.forward * powerImpulse);
    }

    public void MoveDown()
    {
        ball.AddForce(Vector3.back * powerImpulse);
    }

    public void MoveRight()
    {
        ball.AddForce(Vector3.right * powerImpulse);
    }

    public void MoveLeft()
    {
        ball.AddForce(Vector3.left * powerImpulse);
    }

    public void Update()
    {
        if (ball.transform.position.y < -15f && !isStay)
            ball.transform.position = new Vector3(0, 1.5f, 0);

        if (isStay)
            return;

        if (Input.GetKeyDown(KeyCode.A))
            MoveLeft();
        else if (Input.GetKeyDown(KeyCode.D))
            MoveRight();

        if (Input.GetKeyDown(KeyCode.W))
            MoveUp();
        else if (Input.GetKeyDown(KeyCode.S))
            MoveDown();
    }
}
