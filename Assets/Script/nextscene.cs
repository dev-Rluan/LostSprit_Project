using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleToRobby1 : MonoBehaviour
{
     void OnMouseDown()
    {
        Debug.Log("button press");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
