using DummyClient;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
	static ServerSession _session = new ServerSession();
	public static NetworkManager Instance { get; } = new NetworkManager();
	public void Send(ArraySegment<byte> senBuff)
    {
		_session.Send(senBuff);
    }
    void Start()
    {
		// DNS (Domain Name System)
		string host = Dns.GetHostName();
		IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
		IPAddress ip = IPAddress.Parse("172.18.7.116");
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

		Connector connector = new Connector();

		connector.Connect(endPoint,	
			() => { return _session; });
		
	}
	
    void Update()
    {
		List<IPacket> list = PacketQueue.Instance.PopAll();		
		
		foreach (IPacket packet in list)
		{
			PacketManager.Instance.HandlePacket(_session, packet);			
		}		
    }
    // 개선 방향 
    // public void send(T){
    //	Send(T.Write());
//	}
	public void Login(string id, string pwd)
	{
		C_Login loginPacket = new C_Login();
		loginPacket.id = id;
		loginPacket.pwd = pwd;
		Send(loginPacket.Write());
	}

	public void RoomInfoRequest()
    {
		C_RoomRefresh pkt = new C_RoomRefresh();
		Send(pkt.Write());
    }

	public void GameStart(string stageCode)
    {
		C_GameStart pkt = new C_GameStart();
		pkt.stageCode = stageCode;
		Send(pkt.Write());
    }

	public void GameOver()
    {
		C_GameOver pkt = new C_GameOver();
		Send(pkt.Write());
    }
	public void RoomListRequest()
	{
		C_RoomList pkt = new C_RoomList();
		Send(pkt.Write());
		Debug.Log("룸리스트 요청 보냄");
	}
	public void CreateRoom(string title, int maxPlayer)
    {
		C_CreateRoom pkt = new C_CreateRoom();
		pkt.title = title;
		pkt.maxUser = maxPlayer;
		Send(pkt.Write());
    }
	

	public void RankListRequest(string stageCode)
	{
		C_RankList pkt = new C_RankList();
		pkt.stageCode = stageCode;
		Send(pkt.Write());
		Debug.Log("랭크리스트 요청 보냄");
	}
	// 룸아이디에 접속 (이때 방에 처음 접속한다.)
	public void EnterRoom(int roomId)
    {
		C_RoomEnter pkt = new C_RoomEnter();
		pkt.roomId = roomId;
		Send(pkt.Write());
    }
	
	public void FirePlayerStart()
	{
		C_Enter enterPacket = new C_Enter();
		enterPacket.attr = "fire";
		enterPacket.posX = 0;
		enterPacket.posY = 1;
		enterPacket.posZ = 10;
		Send(enterPacket.Write());
	}

	public void WaterPlayerStart()
	{
		C_Enter enterPacket = new C_Enter();
		enterPacket.attr = "water";
		enterPacket.posX = 0;
		enterPacket.posY = 1;
		enterPacket.posZ = 5;
		Send(enterPacket.Write());
	}

    public void MovePlayer(float posX, float posY, float posZ)
    {
		C_Move movePacket = new C_Move();
		movePacket.posX = posX;
		movePacket.posY = posY;
		movePacket.posZ = posZ;
		Send(movePacket.Write());
	}
	public void DestroyObject(int tag)
    {
		//C_DestroyItem packet = new C_DestroyItem();
		//packet.item = tag;
		//Send(packet.Write());
	}

	public void GameOverEvent()
    {
		C_GameOver packet = new C_GameOver();
		Send(packet.Write());
    }

    public void RotPlayer(float rotX, float rotY, float rotZ, float rotW)
    {
		C_Rot rotPacket = new C_Rot();
		rotPacket.rotX = rotX;
		rotPacket.rotY = rotY;
		rotPacket.rotZ = rotZ;
		rotPacket.rotW = rotZ;
		Send(rotPacket.Write());
	}
}
