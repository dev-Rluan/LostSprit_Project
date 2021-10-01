using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject MenuCam;
  //  public GameObject FireCam;
    public PlayerController WaterPlayer;
    public PlayerController FirePlayer;
    public GameObject StartMenu;
    

   
    
    public void GameStartFire()
    {

        StartMenu.SetActive(false);
        MenuCam.SetActive(false);
        FirePlayer.gameObject.SetActive(true);
        WaterPlayer.gameObject.SetActive(false);
        


    }
    public void GameStartWater()
    {

        StartMenu.SetActive(false);
        MenuCam.SetActive(false);
        FirePlayer.gameObject.SetActive(false);
        WaterPlayer.gameObject.SetActive(true);
    }
}
