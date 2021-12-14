using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager :MonoBehaviour
{
    PlayerController _myPlayer;
    LoginManager _loginManager;
    GameManager2 _gameManager;
    MainToRobby _mainToRobby;
    RobbyManager _robbyManager;
    Dictionary<string, Player> _players = new Dictionary<string, Player>();
    Dictionary<int, ObjectNum> _objects = new Dictionary<int, ObjectNum>();

    

    GameObject go1;
    GameObject go2;
    GameObject go3;
    GameObject go4;
    GameObject go5;
    GameObject go6;
    GameObject go7;
    GameObject go8;

    public static PlayerManager Instance { get; } = new PlayerManager();
    public string PlayerId { get; set; }
    public int SessionId { get; set; }
    public int RoomID { get; set; }
    public string Stage { get; set; }
    public string RoomTitle { get;  set; }
    public  int MaxPlayer { get; set; }
    public  int NowPlayer { get; set; }
    public string attr { get; set; } 
    public string Host { get; set; }
    

    // n
    public void GameStart(string stageCode)
    {
        _robbyManager = GameObject.Find("RobbyManager").GetComponent<RobbyManager>();
        Stage = stageCode;
        _robbyManager.GameStart(int.Parse(stageCode));
    }
    
    // 유저가 새로 방에 접속함
    public void EnterUser(S_BroadCastEnterRoom packet)
    {
        MainToRobby _IntoRobby = GameObject.Find("ManagerGroup").GetComponentInChildren<MainToRobby>();
        _IntoRobby.EnterRoom();
    }
    // 방에 접속 하였음 n 
    public void EnterRoom(S_EnterRoomOk packet)
    {      
        //foreach (S_EnterRoomOk.PlayerReady p in packet.playerReadys)
        //{
        //    Player _p = new Player();
        //    _p.PlayerId = p.playerId;
        //    if (_players.TryGetValue(p.playerId, out Player player))
        //    {
        //        _players[p.playerId] = player;
        //    }
        //    else
        //    {
        //        _players.Add(p.playerId, _p);
        //        Debug.Log("playerId : " + p.playerId + ", 플레이어 현재 상태" + p.readyStatus);
        //    }
        //    Debug.Log("playerId : " + p.playerId + ", 플레이어 현재 상태" + p.readyStatus);
        //}
        
        _mainToRobby = GameObject.Find("ManagerGroup").GetComponentInChildren<MainToRobby>();
        _mainToRobby.EnterRoom();
    }
    // 방 정보 가져와서 보여주기
    public void RoomInfo(S_RoomInfo packet)
    {
//        Stage = packet.stage;
//        MaxPlayer = packet.maxPlayer;
//        NowPlayer = packet.nowPlayer;
//        RoomTitle = packet.title;
//        Debug.Log("tesfjalkfdjs");
//;        foreach(S_RoomInfo.PlayerReady p in packet.playerReadys)
//        {
//            Player _p = new Player();
//            _p.PlayerId = p.playerId;
//            _p.ReadyState = p.readyStatus;
//            if (_players.TryGetValue(p.playerId, out Player player))
//            {
//                _players[p.playerId] = player;
//            }
//            else 
//            {
//                _players.Add(p.playerId, _p);
//            }
//            Debug.Log("playerId : " + p.playerId + ", 플레이어 현재 상태" + p.readyStatus); 
//        }
//        _robbyManager.RobbyRefresh();
    }
    public void LoginOk()
    {
        Debug.Log("여기까지옴  22");
        _loginManager = GameObject.Find("Main Camera").GetComponent<LoginManager>();
        Debug.Log("여기까지옴  33");
        _loginManager.LoginOk();
    }
    public void RoomListRecv(S_RoomList packet)
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager2>();
        _gameManager.ShowRoomList(packet);
    }
    //public void RankListRecv(S_RankList packet)
    //{
    //    _gameManager2.ShowRankList(packet);
    //}
   public void CreateRoomResult(S_CreateRoomResult packet)
    {
        MainToRobby _IntoRobby = GameObject.Find("ManagerGroup").GetComponentInChildren<MainToRobby>();
        _IntoRobby.CreateRoom(packet);
      
    }
    // 방 유저들 불러들임
    public void Add(S_PlayerList packet)
    {
        Object objB = Resources.Load("waterblend");
        Object objR = Resources.Load("fireblend");
        Object objBox = Resources.Load("MoveCube");
        Object objKey = Resources.Load("Key");
        Object objPuzzleCube = Resources.Load("Puzzle Cube");
        Object objDoor = Resources.Load("Door");

        Debug.Log("방 들어옴");
        // Box
        //Object objCube = Resources.Load("SingleCube5");
        //GameObject goCube = Object.Instantiate(objCube) as GameObject;
        //goCube.transform.position = new Vector3(10, 1, 10);

        //_myPlayer.cubeItem = goCube;


        foreach (S_PlayerList.Player p in packet.players)
        {
            GameObject go;
            if (p.attr == "fire")
            {
                if (p.isSelf != true)
                {
                    Debug.Log("1 e들어옴");
                    go = Object.Instantiate(objR) as GameObject;
                    Player player = go.AddComponent<Player>();
                    player.transform.position = new Vector3(p.posX, p.posY, p.posZ);
                    _players.Add(p.playerId, player);
                    Debug.Log(p.playerId + player);
                }
                else
                {
                    PlayerId = p.playerId;
                }
            }
            else
            {
                if (p.isSelf != true)
                {
                    go = Object.Instantiate(objB) as GameObject;
                    Player player = go.AddComponent<Player>();
                    player.transform.position = new Vector3(p.posX, p.posY, p.posZ);
                    _players.Add(p.playerId, player);
                }
                else
                {
                    PlayerId = p.playerId;
                }
            }
        }
        go1 = Object.Instantiate(objBox) as GameObject;
        ObjectNum Box = go1.AddComponent<ObjectNum>();
        Box.transform.position = new Vector3(10, 1, 10);
        Box.objectID = 1;
        _objects.Add(1, Box);

        go2 = Object.Instantiate(objKey) as GameObject;
        ObjectNum Key = go2.AddComponent<ObjectNum>();
        Key.transform.position = new Vector3(60, -1, 7);
        Key.objectID = 2;
        _objects.Add(2, Key);

        go3 = Object.Instantiate(objPuzzleCube) as GameObject;
        ObjectNum PuzzleCube1 = go3.AddComponent<ObjectNum>();
        PuzzleCube1.transform.position = new Vector3(222, 29, 10);
        PuzzleCube1.objectID = 3;
        _objects.Add(3, PuzzleCube1);

        go4 = Object.Instantiate(objPuzzleCube) as GameObject;
        ObjectNum PuzzleCube2 = go4.AddComponent<ObjectNum>();
        PuzzleCube2.transform.position = new Vector3(222, 29, 12);
        PuzzleCube2.objectID = 4;
        _objects.Add(4, PuzzleCube2);

        go5 = Object.Instantiate(objPuzzleCube) as GameObject;
        ObjectNum PuzzleCube3 = go5.AddComponent<ObjectNum>();
        PuzzleCube3.transform.position = new Vector3(220, 29, 12);
        PuzzleCube3.objectID = 5;
        _objects.Add(5, PuzzleCube3);

        go6 = Object.Instantiate(objPuzzleCube) as GameObject;
        ObjectNum PuzzleCube4 = go6.AddComponent<ObjectNum>();
        PuzzleCube4.transform.position = new Vector3(220, 29, 10);
        PuzzleCube4.objectID = 6;
        _objects.Add(6, PuzzleCube4);

        go7 = Object.Instantiate(objDoor) as GameObject;
        ObjectNum Door = go6.AddComponent<ObjectNum>();
        Door.transform.position = new Vector3((float)28, (float)32.71142, (float)-6.67);
        Door.transform.rotation = Quaternion.Euler(0, 90, 0);
        Door.objectID = 7;
        _objects.Add(7, Door);
    }
    //public void ObjectAdd(int i, float x, float y, float z)
    //{
    //    switch (i)
    //    {
    //        case 1:
    //            go1 = Object.Instantiate(objBox) as GameObject;
    //            ObjectNum Box = go1.AddComponent<ObjectNum>();
    //            Box.transform.position = new Vector3(x, y, z);
    //            Box.objectID = 1;
    //            _objects.Add(1, Box);
    //            break;
    //        case 2:
    //            go2 = Object.Instantiate(objKey) as GameObject;
    //            ObjectNum Key = go2.AddComponent<ObjectNum>();
    //            Key.transform.position = new Vector3(x, y, z);
    //            Key.objectID = 2;
    //            _objects.Add(2, Key);
    //            break;
    //        case 3:
    //            go3 = Object.Instantiate(objPuzzleCube) as GameObject;
    //            ObjectNum PuzzleCube1 = go3.AddComponent<ObjectNum>();
    //            PuzzleCube1.transform.position = new Vector3(x, y, z);
    //            PuzzleCube1.objectID = 3;
    //            _objects.Add(3, PuzzleCube1);
    //            break;
    //        case 4:
    //            go4 = Object.Instantiate(objPuzzleCube) as GameObject;
    //            ObjectNum PuzzleCube2 = go4.AddComponent<ObjectNum>();
    //            PuzzleCube2.transform.position = new Vector3(x, y, z);
    //            PuzzleCube2.objectID = 4;
    //            _objects.Add(4, PuzzleCube2);
    //            break;
    //        case 5:
    //            go5 = Object.Instantiate(objPuzzleCube) as GameObject;
    //            ObjectNum PuzzleCube3 = go5.AddComponent<ObjectNum>();
    //            PuzzleCube3.transform.position = new Vector3(x, y, z);
    //            PuzzleCube3.objectID = 5;
    //            _objects.Add(5, PuzzleCube3);
    //            break;
    //        case 6:
    //            go6 = Object.Instantiate(objPuzzleCube) as GameObject;
    //            ObjectNum PuzzleCube4 = go6.AddComponent<ObjectNum>();
    //            PuzzleCube4.transform.position = new Vector3(x, y, z);
    //            PuzzleCube4.objectID = 6;
    //            _objects.Add(6, PuzzleCube4);
    //            break;
    //        case 7:
    //            go7 = Object.Instantiate(objDoor) as GameObject;
    //            ObjectNum Door = go6.AddComponent<ObjectNum>();
    //            Door.transform.position = new Vector3((float)x, (float)y, (float)-z);
    //            Door.transform.rotation = Quaternion.Euler(0, 90, 0);
    //            Door.objectID = 7;
    //            _objects.Add(7, Door);
    //            break;
    //        case 8:
    //            go8 = Object.Instantiate(objDoor) as GameObject;
    //            ObjectNum OpenDoor = go6.AddComponent<ObjectNum>();
    //            OpenDoor.transform.position = new Vector3((float)28, (float)32.71142, (float)-6.67);
    //            OpenDoor.transform.rotation = Quaternion.Euler(0, 90, 0);
    //            OpenDoor.objectID = 7;
    //            _objects.Add(7, OpenDoor);
    //            break;
    //    }
    //}
    // 새로 들어온 유저 정보 저장
    public void EnterGame(S_BroadcastEnterGame packet)
    {
        Object objB = Resources.Load("waterblend");
        Object objR = Resources.Load("fireblend");

        if (PlayerId == packet.playerId)
            return;

        Debug.Log("게임 들어옴");

        GameObject go;
        if (packet.attr == "fire")
        {
            go = Object.Instantiate(objR) as GameObject;
        }
        else
        {
            go = Object.Instantiate(objB) as GameObject;
        }

        Player player = go.AddComponent<Player>();
        
        player.transform.position = new Vector3(packet.posX, packet.posY, packet.posZ);
        _players.Add(packet.playerId, player);

    }
    public void LeaveGame(S_BroadcastLeaveGame packet)
    {
        if(PlayerId == packet.playerId)
        {
            
        }
        else
        {
            Player player = null;
            if(_players.TryGetValue(packet.playerId, out player))
            {
                GameObject.Destroy(player.gameObject);
                Debug.Log("나감");
                _players.Remove(packet.playerId);
            }
        }
    }   
    public void Move(S_BroadCastMove packet)
    {
        // _myPlayer.transform.position = new Vector3(packet.posX, packet.posY, packet.posZ);
        Debug.Log("무브 ");
        Player player = null;
        
        if(_players.TryGetValue(packet.playerId, out player))
        {
            Debug.Log(packet.playerId + packet.posX + packet.posY + packet.posZ);
            player.transform.position = new Vector3(packet.posX, packet.posY, packet.posZ);
        }
    }
    public void Rot(S_BroadCastRot packet)
    {
        Player player = null;
        if (_players.TryGetValue(packet.playerId, out player))
        {
            player.transform.rotation = new Quaternion(packet.rotX, packet.rotY, packet.rotZ, packet.rotW);
        }
    }

    //public void DestroyItem(S_BoradCastDestroyItem packet)
    //{
    //    _myPlayer.cubeItem = null;

    //    //S_BoradCastDestroyItem pkt = packet as S_BoradCastDestroyItem;
    //    //string tag = pkt.item;
    //    //_myPlayer.DestroyItemEvent(tag);
    //}


}
