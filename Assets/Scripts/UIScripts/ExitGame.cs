using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("Exit Game button clicked");
        UnityEngine.Application.Quit();
    }
}
