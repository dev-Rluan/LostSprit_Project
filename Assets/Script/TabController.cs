using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour
{
    public GameObject EscMenu;
    public GameObject TabMenu;

    private bool paused = false;
    private bool tabtab = false;

    // Start is called before the first frame update
    void Start()
    {
        EscMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("esc"))
        {
            paused = !paused;
        }
        else if (paused)
        {
            EscMenu.SetActive(true);
            
        }
        else if(!paused){
            EscMenu.SetActive(false);
        }

        if (Input.GetButtonDown("tab"))
        {
            tabtab = !tabtab;
        }
        else if (tabtab)
        {
            TabMenu.SetActive(true);

        }
        else if (!tabtab)
        {
            TabMenu.SetActive(false);
        }
    }

    public void Resume()
    {
        paused = !paused;
    }

    public void Option()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }




}
