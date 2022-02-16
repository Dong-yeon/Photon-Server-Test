using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomData : MonoBehaviour
{
    [SerializeField] Text RoomInfoText; 
    
    RoomInfo _roomInfo;

    public void SetUp(RoomInfo _info) // 방 정보 받아오기
    {
        _roomInfo = _info;
        RoomInfoText.text = $"{_roomInfo.Name} ({_roomInfo.PlayerCount}/{_roomInfo.MaxPlayers})";
    }

    public void OnClick() // 클릭하면 방으로 입장
    {
        NetworkManager.Instance.JoinRoom(_roomInfo); // 방으로 입장 NetworkManager의 JoinRoom 실행
    }
}
