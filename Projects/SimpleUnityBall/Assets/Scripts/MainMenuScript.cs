using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Single xBound = 5f;
    [SerializeField] private Single zBound = 5f;
    [SerializeField] private Single destroyBorder = -15f;

    private GameObject ball;

    public void Update()
    {
        BallSpawner();
        BallDestroy(destroyBorder);
    }

    private void BallSpawner()
    {
        if (ball != null)
            return;

        Single rXBound = UnityEngine.Random.Range(-xBound, xBound);
        Single rZBound = UnityEngine.Random.Range(-zBound, zBound);

        Vector3 ballPosInit = new Vector3(rXBound, transform.position.y, rZBound);

        ball = Instantiate<GameObject>(ballPrefab, ballPosInit, Quaternion.identity);
        ball.transform.parent = transform;
    }

    private void BallDestroy(Single value)
    {
        if(ball.transform.position.y < value)
            Destroy(ball);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void AppExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #endif

        Application.Quit();
        Debug.Log("Exit");
    }
}
