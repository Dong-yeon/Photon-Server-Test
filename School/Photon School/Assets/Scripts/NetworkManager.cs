using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // 포톤 기능 사용
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks // 다른 포톤의 반응 받아들이기
{

    [SerializeField] InputField idInput, pwInput; // id, pw 변수 설정 
    [SerializeField] InputField roomInput; // 방이름 변수 설정
    [SerializeField] InputField maximumInput; // 방인원 변수 설정
    [SerializeField] Toggle privateToggle; // 비밀방 체크

    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;

    public static NetworkManager Instance; // NetManager 스크립트를 메서드로 사용하기 위해 선언
    void Awake()
    {
        Screen.SetResolution(960, 540, false); // 게임화면 960,540으로 고정
        Instance = this; // 메서드로 사용
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings(); // 세팅되어있는 포톤네트워크로 연결

    public override void OnConnectedToMaster()
    {
        Debug.Log("1. 서버 접속 완료");
        PhotonNetwork.LocalPlayer.NickName = idInput.text; // 포톤네트워크의 로컬플레이어의 닉네임은 입력한 id
        PhotonNetwork.JoinLobby();
        PhotonNetwork.LoadLevel(1); // 로비씬인 1로 이동
    }

    public void DisConnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("서버 연결 끊김");
    }

/*    public void JoinLooby()
    {
        Debug.Log("로비로 이동");
    }*/

    public override void OnJoinedLobby() // 로비에 연결시 작동
    {
        Debug.Log("2. 로비로 이동");
    }

    public void OnCreateRoom() // 방 만들기
    {
            RoomOptions ro = new RoomOptions(); // 룸 옵션을 새로 지정
            ro.MaxPlayers = byte.Parse(maximumInput.text); // 방 최대 인원은 지정한 인원수
            if (string.IsNullOrEmpty(roomInput.text) || string.IsNullOrEmpty(maximumInput.text))
            {
                return; // 방 이름이 빈값이면 방이 만들어지지 않음 + 방인원을 지정하지 않으면 방이 만들어지지 않음
            }
            if (byte.Parse(maximumInput.text) > 16) // 방 인원을 16인 이상으로 지정하면 방이 만들어지지 않음
        {
            return; 
        }

            PhotonNetwork.CreateRoom(roomInput.text, ro, TypedLobby.Default);
            /*        PhotonNetwork.CreateRoom(roomInput.text); // 포톤 네트워크 기능으로 roomInput.text의 이름으로 방을 만든다.*/
            Debug.Log("3. 방 생성 완료");
        
    }

    public override void OnCreateRoomFailed(short returnCode, string message) // 방 만들기 실패시 작동
    {
        /*errorText.text = "Room Creation Faile" + message;*/
        MenuManager.Instance.OpenMenu("error");
    }

    public override void OnJoinedRoom()//방에 들어갔을때 작동
    {
        PhotonNetwork.LoadLevel(2); // scene 번호가 2인 school scene으로 이동.
        /*        MenuManager.Instance.OpenMenu("room");//룸 메뉴 열기
                roomNameText.text = PhotonNetwork.CurrentRoom.Name; // 들어간 방 이름 표시*/
        Debug.Log("4. 방 입장완료");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name); // 포톤 네트워크의 JoinRoom기능 해당이름을 가진 방으로 접속한다.
    }
    
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) // 포톤의 룸리스트 기능
    {
        foreach (Transform trans in roomListContent) // 존재하는 모든 roomListContent
        {
            Destroy(trans.gameObject); // 룸리스트가 업데이트 될때마다 싹 지우기
        }
        for (int i = 0; i < roomList.Count; i++) // 방의 갯수만큼 반복
        {
            if (roomList[i].RemovedFromList) // 사라진 방은 취급 X
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomData>().SetUp(roomList[i]);
            // instantiate로 prefab을 roomListContent위치에 만들어주고 그 프리펩을 i번째 룸리스트로 만들어줌
        }
        base.OnRoomListUpdate(roomList);
    }

}
