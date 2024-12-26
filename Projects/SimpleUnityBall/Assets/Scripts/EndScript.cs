using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    [SerializeField] private GameObject gratsObj;
    [SerializeField] private GameObject playerObj;

    public void Start()
    {
        Congrats(false);
    }

    public void OnTriggerStay(Collider other)
    {
        playerObj.GetComponent<BallMove>().isStay = true;
        Congrats(true);
        Invoke("EnterMainMenu", 5f);
    }

    private void Congrats(bool isActive)
    {
        gratsObj.SetActive(isActive);
    }

    private void EnterMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
