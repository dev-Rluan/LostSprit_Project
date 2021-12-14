using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void onMouseDown()
    {
        Debug.Log("버튼 눌림");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
