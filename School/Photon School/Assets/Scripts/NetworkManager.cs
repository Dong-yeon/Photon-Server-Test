using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // 포톤 기능 사용
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks // 다른 포톤의 반응 받아들이기
{

    public InputField idInput, pwInput; // id, pw 변수 설정 


    void Awake() => Screen.SetResolution(960, 540, false); // 게임화면 960,540으로 고정

    public void Connect() => PhotonNetwork.ConnectUsingSettings(); // 세팅되어있는 포톤네트워크로 연결

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 접속 완료");
        PhotonNetwork.LocalPlayer.NickName = idInput.text; // 포톤네트워크의 로컬플레이어의 닉네임은 입력한 id
        SceneManager.LoadScene("Lobby");
    }

    public void DisConnect() => PhotonNetwork.Disconnect();

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("서버 연결 끊김");
    }

    public void JoinLooby()
    {
        Debug.Log("로비로 이동");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() => print("로비접속완료");

}
