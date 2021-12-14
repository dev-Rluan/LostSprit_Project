using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainToRobby : MonoBehaviour
{
    public InputField IF_StageName;
    
    //  public List<Button> Btn_Players;
    // private CreateGameRoomData roomData;
    static NetworkManager _network;
    static PlayerManager _playerManager;
    //string StageName="test";
    //int players=2;
    
    public void MaxPlayerCount(int count)
    {
       /* roomData.MaxPlayerCount = count;
        for (int i = 0; i < Btn_Players.Count; i++)
        {
            if (i== count )
            {
                Btn_Players[i].image.color = new Color(1f, 1f, 1f, 1f);

            }
            else
            {
                Btn_Players[i].image.color = new Color(1f, 1f, 1f, 0f);

            }
        }*/
    }
    /*public class CreateRoomData
    {
        public int MaxPlayerCount;
    }*/
    
    public void CreateRoom(S_CreateRoomResult packet)
    {
        Debug.Log("mainto Robby creatRoom ");
        _playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        _network = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        SceneManager.LoadScene(1, LoadSceneMode.Single);        
        DontDestroyOnLoad(_network);
        DontDestroyOnLoad(_playerManager);
    }
    // Update is called once per frame
    public void onMouseDown()
    {
        Debug.Log(IF_StageName.text);

       // IF_StageName = GetComponent<InputField>();
        //StageName = IF_StageName.text;
        NetworkManager.Instance.CreateRoom(IF_StageName.text, 2);
        PlayerManager.Instance.RoomTitle = IF_StageName.text;


    }
    public void EnterRoom()
    {
        Debug.Log("Enter Room");
        _network = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        _playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        DontDestroyOnLoad(_network);
        DontDestroyOnLoad(_playerManager);
    }
    void SaveData(string stageName, int players)
    {
        Debug.Log(IF_StageName);


        /*Btn_Players = GetComponent<List<Button>>();
        players = Btn_Players.Count;*/


        PlayerPrefs.SetString("StageName", stageName);
        PlayerPrefs.SetInt("Player", players);
    }
}
