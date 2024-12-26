using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private GameObject gratsObj;
    [SerializeField] private GameObject floorObj;
    [SerializeField] private GameObject playerObj;

    public void Start()
    {
        Congrats(false);
    }

    public void OnTriggerStay(Collider other)
    {
        playerObj.GetComponent<BallMove>().isStay = true;
        Congrats(true);
        Invoke("NextLvl", 3f);
        Invoke("MoveNextLevel", 5f);
    }

    private void NextLvl()
    {
        print("NextLevel!");
    }

    private void MoveNextLevel()
    {
        floorObj.GetComponent<Collider>().enabled = false;
    }

    private void Congrats(bool isActive)
    {
        gratsObj.SetActive(isActive);
    }

    public void Update()
    {
        if (playerObj.GetComponent<Transform>().position.y < -15f)
            SceneManager.LoadScene(2);
    }
}
