using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Button startButton; // Reference to the button

    private void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnClick);
        }
        else
        {
            Debug.LogError("Start Button is not assigned in the Inspector.");
        }
    }

    public void OnClick()
    {
        Debug.Log("Start Game button clicked");
        SceneManager.LoadScene(1);
    }
}
