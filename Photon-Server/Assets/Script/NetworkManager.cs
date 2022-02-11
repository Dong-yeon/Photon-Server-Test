using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    string networkState;
    // Start is called before the first frame update
    void Start() => PhotonNetwork.ConnectUsingSettings(); // 씬이 시작되면 포톤네트워크 연결시작

    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby(); // 마스터서버에 연결이되면 포톤네트워크의 로비로 연결

    public override void OnJoinedLobby() => PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 4 }, null);
    // 로비로 연결이되면 => 포톤네트워크에 룸이 생성되어있으면 조인 / 없으면 룸생성 (룸 옵션 중 최대 인원은 4명까지)


    void Update()
    {
        string curNetworkState = PhotonNetwork.NetworkClientState.ToString(); // 현재 연결되어있는 네트워크위치 = 포톤네트워크의 네트워크이름
        if (networkState != curNetworkState) // 만약 네트워크위치가 현재 네트워크 위치가 아니면
        {
            networkState = curNetworkState; // 네트워크위치를 현재 네트워크 위치로 수정
            print(networkState); // 현재 네트워크 위치 출력
        }
        
    }
}
