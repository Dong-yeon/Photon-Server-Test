using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV; // 포톤뷰 선언

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine) // 포톤 네트워크가 내것이라면
        {
            CreateController(); // 플레이어 컨트롤러를 붙여준다.
        }
    }

    void CreateController() // 플레이어 컨트롤러 만들기
    {
        Debug.Log("Instantiated Player Controller");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Zombie"), Vector3.zero, Quaternion.identity);
        // 포톤 프리펩에 있는 플레이어 컨트롤러를 저 위치에 저 각도로 만들어주기
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
