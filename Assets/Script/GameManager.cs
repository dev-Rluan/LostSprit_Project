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
    public GameObject UIPanel;
    public Image Img1;
    public Image Img2;

    CanvasGroup mainGrop;
    CanvasGroup GameOverGrop;
    //  public GameObject menuSet;

    void Start()
    {
        
    }
    public void GameStart()
    {
        //if (PlayerManager.Instance. == "fire")
        //{
        //    GameStartFire();
        //}
        //else
        //{
        //    GameStartWater();
        //}
    }


    //public void CanvasGroupOn(CanvasGroup cg)
    //{
    //    cg.alpha = 1;
    //    cg.interactable = true;
    //    cg.blocksRaycasts = true;
    //}
    //public void CanvasGroupOff(CanvasGroup cg)
    //{
    //    cg.alpha = 0;
    //    cg.interactable = false;
    //    cg.blocksRaycasts = false;
    //}

    //public void GameOver()
    //{
    //    CanvasGroupOff(mainGrop);
    //    CanvasGroupOn(GameOverGrop);
    //}

    public void GameStartFire()
    {
        StartMenu.SetActive(false);
        UIPanel.SetActive(true);
        MenuCam.SetActive(false);
        FirePlayer.gameObject.SetActive(true);
        WaterPlayer.gameObject.SetActive(false);
        Img1.color = new Color(1, 1, 1, 0);
        Img2.color = new Color(1, 1, 1, 0);
        
        //if (!isInstantiate)
        //{
        //    for (int i = 0; i < 4; i++)
        //    {
        //        Instantiate(Puzzleitem, PuzzlePos[i].position, Quaternion.identity);
        //    }
        //    isInstantiate = true;
        //}
    }
    void Update()
    {
        

     /*   if (Input.GetButtonDown("exc"))
        {
            if (menuSet.activeSelf)
                menuSet.SetActive(true);
            else
                menuSet.SetActive(false);
        }*/
           
    }
    public void GameStartWater()
    {
        StartMenu.SetActive(false);
        UIPanel.SetActive(true);
        MenuCam.SetActive(false);
        FirePlayer.gameObject.SetActive(false);
        WaterPlayer.gameObject.SetActive(true);
        Img1.color = new Color(1, 1, 1, 0);
        Img2.color = new Color(1, 1, 1, 0);

       

    }
    void LateUpdate()   //Update() 가 끝난 후 호출됨
    {
        if (WaterPlayer.cntitem[0] != 0)
        {
            Img1.color = new Color(1, 1, 1, WaterPlayer.cntitem[0] != 0 ? 1 : 0); //RGB는 건들이지 않고 alpha값만 변경
        }
        else if (WaterPlayer.cntitem[1] != 0)
        {
            Img2.color = new Color(1, 1, 1, WaterPlayer.cntitem[1] != 0 ? 1 : 0); //RGB는 건들이지 않고 alpha값만 변경
        }

        if (FirePlayer.cntitem[0] != 0)
        {
            Img1.color = new Color(1, 1, 1, FirePlayer.cntitem[0] != 0 ? 1 : 0); //RGB는 건들이지 않고 alpha값만 변경
        }
        else if (FirePlayer.cntitem[1] != 0)
        {
            Img2.color = new Color(1, 1, 1, FirePlayer.cntitem[1] != 0 ? 1 : 0); //RGB는 건들이지 않고 alpha값만 변경
        }

    }
  /*  public void GameExit()
    {
       Application.Quit();
    }*/

}
