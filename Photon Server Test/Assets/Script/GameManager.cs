using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.UI;
using PN = Photon.Pun.PhotonNetwork;
using Random = UnityEngine.Random;
public enum State { None, QuickMatching, QuickMatchDone, RacingStart, RacingDone }

public class GameManager : MonoBehaviourPunCallbacks
{
	public static GameManager Inst { get; private set; }
	void Awake()
	{
		Inst = this;

		ShowPanel("ConnectPanel");

		PN.SendRate = 30;
		PN.SerializationRate = 15;

		if (autoJoin)
			ConnectClick(null);

	}

	[Header("Debug")]
	[SerializeField] bool autoJoin;
	[SerializeField] byte autoMaxPlayer = 2;

	[Header("Panel")]
	[SerializeField] GameObject connectPanel;
	[SerializeField] GameObject lobbyPanel;
	[SerializeField] GameObject gamePanel;

	[Header("Lobby")]
	[SerializeField] Text quickMatchText;

	[Header("Game")]
	[SerializeField] Transform[] spawnPoints; // 자동차 스폰 포인트
	[SerializeField] UnityStandardAssets.Cameras.AutoCam autoCam;
	[SerializeField] Text timeText;
	[SerializeField] Text countdownText;

	public State state;

	public int GetIndex // 플레이어 인덱스 가지고 오는 부분
	{
		get
		{
			for (int i = 0; i < PN.PlayerList.Length; i++)
			{
				if (PN.PlayerList[i] == PN.LocalPlayer) // 플레이어 리스트가 현재 로컬플레이어와 같으면 i를 반환
					return i;
			}
			return -1;
		}
	}
	WaitForSeconds one = new WaitForSeconds(1);
	int racingStartTime;



	public void ShowPanel(string panelName) // true 값의 패널만 보이도록 설정
	{
		connectPanel.SetActive(false);
		lobbyPanel.SetActive(false);
		gamePanel.SetActive(false);

		if (panelName == connectPanel.name)
			connectPanel.SetActive(true);
		else if (panelName == lobbyPanel.name)
			lobbyPanel.SetActive(true);
		else if (panelName == gamePanel.name)
			gamePanel.SetActive(true);
	}


	public void ConnectClick(InputField nickInput) // Player 이름 설정
	{
		PN.ConnectUsingSettings();

		string nick = nickInput == null ? $"Player{Random.Range(0, 100)}" : nickInput.text;
		PN.NickName = nick;
	}

	public override void OnConnectedToMaster() => PN.JoinLobby(); // 서버에 연결되면 로비로 연결

	public override void OnJoinedLobby() // 로비로 연결되면 로비패널 띄움
	{
		ShowPanel("LobbyPanel");

		if (autoJoin)
			QuickMatchClick();
	}

	public void QuickMatchClick() // QuickMatch버튼을 클릭하면
	{
		if (state == State.None)
		{
			state = State.QuickMatching;
			quickMatchText.gameObject.SetActive(true);
			PN.JoinRandomOrCreateRoom(null, autoMaxPlayer, MatchmakingMode.FillRoom, null, null, // FillRoom 꽉찬 방
				$"room{Random.Range(0, 10000)}", new RoomOptions { MaxPlayers = autoMaxPlayer });
		}
		else if (state == State.QuickMatching)
		{
			state = State.None;
			quickMatchText.gameObject.SetActive(false);
			PN.LeaveRoom();
		}
	}

	public override void OnJoinedRoom()
	{
		PlayerChanged();
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		PlayerChanged();
	}

	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		PlayerChanged();
	}

	void PlayerChanged()
	{
		if (PN.CurrentRoom.PlayerCount == autoMaxPlayer) { }
		else if (PN.CurrentRoom.PlayerCount != PN.CurrentRoom.MaxPlayers)
			return;

		StartCoroutine(GameStartCo());
	}

	IEnumerator GameStartCo()
	{
		print("GameStart");
		ShowPanel("GamePanel");
		SpawnCar();

		yield return one;
		countdownText.text = "3";
		yield return one;
		countdownText.text = "2";
		yield return one;
		countdownText.text = "1";
		yield return one;
		countdownText.text = "GO";
		state = State.RacingStart;
		racingStartTime = PN.ServerTimestamp;

		yield return one;
		yield return one;
		countdownText.text = "";
	}

	void SpawnCar()
	{
		GameObject carObj = PN.Instantiate("Car", spawnPoints[GetIndex].position, spawnPoints[GetIndex].rotation);
	}

	public void SetCamTarget(Transform target)
	{
		autoCam.SetTarget(target);
	}

	void Update()
	{
		if (state == State.QuickMatching && PN.InRoom)
		{
			quickMatchText.text = $"{PN.CurrentRoom.PlayerCount} / {PN.CurrentRoom.MaxPlayers}";
		}

		TimeUpdate();
	}

	void TimeUpdate()
	{
		if (state != State.RacingStart)
			return;

		TimeSpan elapsedTime = TimeSpan.FromMilliseconds(PN.ServerTimestamp - racingStartTime);
		int milliseconds = (int)(elapsedTime.Milliseconds * 0.1f);
		timeText.text = $"{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}:{milliseconds:D2}";
		// 분 초 
	}
}
