using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class PacketHandler
{
	// n
	public static void S_EnterGameHandler(PacketSession session, IPacket packet)
	{		

	}

	// 내가 입장한 상태에서 다른사람이 들어왔을때 추가
	public static void S_BroadcastEnterGameHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastEnterGame pkt = packet as S_BroadcastEnterGame; 
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.EnterGame(pkt);


	}
	// 누군가가 나갔을때
	public static void S_BroadcastLeaveGameHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastLeaveGame pkt = packet as S_BroadcastLeaveGame;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.LeaveGame(pkt);

	}
	// 주변의 플레이어들 리스트를 불러온다
	public static void S_PlayerListHandler(PacketSession session, IPacket packet)
	{
		S_PlayerList pkt = packet as S_PlayerList;		

		PlayerManager.Instance.Add(pkt);		
	}
	// 누군가가 이동하였을때
	public static void S_BroadCastMoveHandler(PacketSession session, IPacket packet)
	{
		S_BroadCastMove pkt = packet as S_BroadCastMove;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.Move(pkt);
	}

	public static void S_BroadCastDestroyItemHandler(PacketSession session, IPacket packet)
	{
        S_BroadCastDestroyItem pkt = packet as S_BroadCastDestroyItem;
        ServerSession serverSession = session as ServerSession;

        //PlayerManager.Instance.DestroyItem(pkt);

    }
	public static void S_BroadCastGameOverHandler(PacketSession session, IPacket packet)
	{
		S_BroadCastGameOver pkt = packet as S_BroadCastGameOver;
		ServerSession serverSession = session as ServerSession;
	}
	public static void S_LoginResultHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;
		S_LoginResult resultPacket = packet as S_LoginResult;
		Debug.Log("여기까지옴  1");
		
		if(resultPacket.result == 1)
        {
			PlayerManager.Instance.LoginOk();
			PlayerManager.Instance.PlayerId = resultPacket.id;
		}
        else
        {
			//실패경우
        }
	}
	public static void S_BroadCastRotHandler(PacketSession session, IPacket packet)
	{
		S_BroadCastRot pkt = packet as S_BroadCastRot;

		PlayerManager.Instance.Rot(pkt);
	}

	public static void S_ReadyCancelHandler(PacketSession session, IPacket pacekt)
    {

    }

	public static void S_BroadCastReadyHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;
	}
	public static void S_BroadCastDropItemHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;
	}
	public static void S_BroadCastItemEventHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;
	}
	public static void S_RoomListHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;
		S_RoomList pkt = packet as S_RoomList;
		foreach (S_RoomList.Room room in pkt.rooms)
		{
            Debug.Log(room.host + room.maxPlayer + room.nowPlayer + room.stage + room.state + room.roomId);			
		}
		PlayerManager.Instance.RoomListRecv(pkt);
	}
	public static void S_RankListHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;

	}
	public static void S_CreateRoomResultHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;
		S_CreateRoomResult pkt = packet as S_CreateRoomResult;

		PlayerManager.Instance.CreateRoomResult(pkt);
		Debug.Log($"room Create : {pkt.title}, {pkt.maxPlayer}, {pkt.nowPlayer}, {pkt.stage}");
	}
	public static void S_EnterRoomOkHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;
		S_EnterRoomOk pkt = packet as S_EnterRoomOk;
		
		PlayerManager.Instance.EnterRoom(pkt);		
	}
	public static void S_RoomInfoHandler(PacketSession session, IPacket packet)
	{
		S_RoomInfo pkt = packet as S_RoomInfo;

		PlayerManager.Instance.RoomInfo(pkt);
	}
	public static void S_RoomConnFaildHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;
		S_RoomConnFaild pkt = packet as S_RoomConnFaild;
		Debug.Log(pkt.result);
	}
	public static void S_BroadCastEnterRoomHandler(PacketSession session, IPacket packet)
	{
		//ServerSession serverSession = session as ServerSession;
		//S_BroadCastEnterRoom pkt = new S_BroadCastEnterRoom();
		//PlayerManager.Instance.EnterUser(pkt);
		//Debug.Log(pkt.playerId + " 입장 ");
	}
	public static void S_NewRankingHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;
	}
	public static void S_ReadyHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;
	}
	public static void S_GameStartHandler(PacketSession session, IPacket packet)
	{
		S_GameStart pkt = packet as S_GameStart;
		PlayerManager.Instance.GameStart(pkt.stageCode);

	}
	public static void S_GameClearHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;
	}
	public static void S_GameOverHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;
	}
	public static void S_GameStartFaildHandler(PacketSession session, IPacket packet)
	{
		ServerSession serverSession = session as ServerSession;
		S_GameStart pkt = new S_GameStart();
		Debug.Log("시작 실패");
	}


}

