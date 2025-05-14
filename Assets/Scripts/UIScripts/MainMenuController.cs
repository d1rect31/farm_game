using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject fadeIn;
    public void StartGame()
    {
        StartCoroutine(StartGameSequence());
    }

    private IEnumerator StartGameSequence()
    {
        Instantiate(fadeIn);
        yield return new WaitForSeconds(2f);
        Debug.Log("Start Game button clicked");
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game button clicked");
        Application.Quit();
    }
}
