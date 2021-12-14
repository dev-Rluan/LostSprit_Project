using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RobbyManager : MonoBehaviour
{

    public Text title;
    public Text stage;
    public Text playerCount;
    public Text user1;
    public Text ready1;


    static NetworkManager _network;
    static PlayerManager _player;

    public void GameStart(int stageCode)
    {
        _player = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        _network = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        Debug.Log("게임시작 요청");
        SceneManager.LoadScene(stageCode + 1, LoadSceneMode.Single);
        DontDestroyOnLoad(_network);
        DontDestroyOnLoad(_player);
        Debug.Log("게임시작");
    }
    public void GameStartResquest(string stageCode)
    {
        Debug.Log("GameStartResquest");
        NetworkManager.Instance.GameStart(stageCode);
    }
    public void GameStartON(string a)
    {
        Debug.Log("게임시작 요청 보냄1");
        Debug.Log(a);
    }


    public void RobbyRefresh()
    {
        Debug.Log("실행 되는 지 확인");
        
        //foreach (S_RoomInfo.PlayerReady info in PlayerManager.Instance.)
        //{
        //    user1.GetComponent<Text>().text = info.playerId;
        //    if(info.readyStatus == 0)
        //    {
        //        ready1.GetComponent<Text>().text = "wating";
        //    }
        //    else if(info.readyStatus == 1)
        //    {
        //        ready1.GetComponent<Text>().text = "fireReady";
        //    }
        //    else if (info.readyStatus == 2)
        //    {
        //        ready1.GetComponent<Text>().text = "WaterReady";
        //    }
        //}              
        title.GetComponent<Text>().text = PlayerManager.Instance.RoomTitle;
        playerCount.GetComponent<Text>().text = PlayerManager.Instance.NowPlayer + "/" + PlayerManager.Instance.MaxPlayer;
        
    }

    void Start()
    {
        NetworkManager.Instance.RoomInfoRequest();        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
