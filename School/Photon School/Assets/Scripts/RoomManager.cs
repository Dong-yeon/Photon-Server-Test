using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO; // Path 사용을 위해 사용

public class RoomManager : MonoBehaviourPunCallbacks // 다른 포톤 반응 받아들이기
{
    public static RoomManager Instance; // Room Manger 스크립트를 메서드로 사용하기 위해 선언

    void Awake()
    {
        if (Instance) // 다른 룸매니저의 존재확인
        {
            Destroy(gameObject); // 다른 룸매니저가 있으면 파괴
            return;
        }
        DontDestroyOnLoad(gameObject); // 룸매니저가 나 혼자면 그대로 유지
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
        // 활성화가 되면 씬 매니저의 OnSceneLoaded에 체인을 건다.
        // 씬이 바뀔때마다 작동
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // 비활성화되면 씬 매니저의 체인을 지운다.
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode load)
    {
        if (scene.buildIndex == 2) // 게임씬이면, 0은 로그인 1은 로비씬이다.
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
            // 포톤 프리펩에 있는 플레이어 매니저를 저 위치에 저 각도로 만들어준다.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
