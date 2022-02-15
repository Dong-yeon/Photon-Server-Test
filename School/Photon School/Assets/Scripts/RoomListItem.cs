using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // 포톤 기능 사용
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] Text text;
    private RoomInfo info; // 포톤 리얼타임의 방 정보 기능


    public RoomInfo RoomInfo
    {
        get
        {
            return info;
        }
        set
        {
            info = value;
            // Ex : room_03 (1/2)
            text.text = $"{info.Name} ({info.PlayerCount}/{info.MaxPlayers})";
        }
    }
    public void SetUp(RoomInfo _info) // 방 정보 받아오기
    {
        info = _info;
        text.text = _info.Name;
    }

    public void OnClick() // 클릭하면 방으로 입장
    {
        NetworkManager.Instance.JoinRoom(info); // 방으로 입장 NetworkManager의 JoinRoom 실행
    }
}
