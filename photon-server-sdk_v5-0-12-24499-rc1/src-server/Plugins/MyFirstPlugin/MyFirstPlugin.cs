using Photon.Hive.Plugin;
using System.Collections.Generic;
using System.IO;

namespace MyFirstPlugin
{
	public class MyFirstPlugin : PluginBase
	{
		public override string Name => "MyFirstPlugin";
		public override string Version => base.Version;
		public override bool IsPersistent => base.IsPersistent;
		IPluginLogger pluginLogger; // IPluginLooger 변수 설정

		// 로그기록
		public const string PluginName = "MyFirstPlugin";
		//IPluginLogger pluginLogger;

		// 커스텀 타입 직렬화 역질렬화
		// SetupInstance에서 등록, using System.IO;
		// 클라이언트도 클래스 포함 동일하게 하면 직렬화나 비직렬화시 이벤트 전달등 통신하기 쉽다.
/*		public override bool SetupInstance(IPluginHost host, Dictionary<string, string> config, out string errorMsg)
		{
			host.TryRegisterType(typeof(CustomPluginType), 1, SerializeCustomPluginType, DeserializeCustomPluginType);

			// 로그 기록
			pluginLogger = host.CreateLogger(PluginName);
			pluginLogger.Debug("debug"); // 일반
			pluginLogger.Warn("warn"); // 경고
			pluginLogger.Error("error"); // 오류
			pluginLogger.Fatal("fatal"); // 치명적 오류

			return base.SetupInstance(host, config, out errorMsg);
		}*/

/*		class CustomPluginType // 원하는 커스텀 클래스 생성
		{
			public int intField;
			public string stringField;
		}*/

		// 직렬화 예시 object -> byte[]
/*		byte[] SerializeCustomPluginType(object customPluginObject)
		{
			var customPluginType = customPluginObject as CustomPluginType;
			if (customPluginObject == null) return null;

			var stream = new MemoryStream();
			var binaryWriter = new BinaryWriter(stream);

			binaryWriter.Write(customPluginType.intField);
			binaryWriter.Write(customPluginType.stringField);
			return stream.ToArray();
		}*/

		// 역질렬화 예시 byte[] -> object
/*		object DeserializeCustomPluginType(byte[] bytes)
		{
			CustomPluginType customPluginType = new CustomPluginType();

			var stream = new MemoryStream(bytes);
			var binaryReader = new BinaryReader(stream);

			customPluginType.intField = binaryReader.ReadInt32();
			customPluginType.stringField = binaryReader.ReadString();
			return customPluginType;
		}*/

		void TestProperties()
		{
			PluginHost.LogInfo($"### AppVersion {AppVersion}");
			PluginHost.LogInfo($"### AppId {AppId}");
			PluginHost.LogInfo($"### Region {Region}");
			PluginHost.LogInfo($"### Cloud {Cloud}");
			PluginHost.LogInfo($"### EnvironmentVerion {EnvironmentVerion}");
			PluginHost.LogInfo($"### Name {Name}");
			PluginHost.LogInfo($"### Version {Version}");
			PluginHost.LogInfo($"### IsPersistent {IsPersistent}");
		}

        public override bool SetupInstance(IPluginHost host, Dictionary<string, string> config, out string errorMsg)
        {
			pluginLogger = host.CreateLogger(PluginName);

			pluginLogger.Debug("debug");
			pluginLogger.Debug("warn");
			pluginLogger.Debug("error");
			pluginLogger.Debug("fatal");

            return base.SetupInstance(host, config, out errorMsg);
        }

        // 플러그인에서 콜백을 받으면 ICallInfo 형태로 info로 들어오게 된다.
        public override void BeforeCloseGame(IBeforeCloseGameCallInfo info) // 방을 나가긴 전에 받는 콜백
		{
			// 방에서 나가기 전
			PluginHost.LogInfo($"### BeforeCloseGame by user {info.UserId}");
			info.Continue();
		}

		public override void BeforeJoin(IBeforeJoinGameCallInfo info) // 방에 참가하기 전에 받는 콜백
		{
			// 방에 참가하기 전
			PluginHost.LogInfo($"### BeforeJoin {info.Request.GameId} by user {info.UserId}");
			info.Continue();
		}

		public override void BeforeSetProperties(IBeforeSetPropertiesCallInfo info) // 커스텀 프로퍼티를 Set 하기전에 받는 콜백
		{
			// 커스텀 프로퍼티를 Set하기 전
			PluginHost.LogInfo($"### BeforeSetProperties {info.Request.Properties.Keys} by user {info.UserId}");
			info.Continue();
		}

		public override void OnCloseGame(ICloseGameCallInfo info) // 게임에서 나간 후 받는 콜백
		{
			// 방에서 나간 후
			PluginHost.LogInfo($"### OnCloseGame by user {info.UserId}");
			info.Continue();
		}

		public override void OnCreateGame(ICreateGameCallInfo info) // 방을 만든 후 받는 콜백
		{
			// 방을 만든 후
			PluginHost.LogInfo($"### OnCreateGame {info.Request.GameId} by user {info.UserId}");
			TestProperties();
			info.Continue(); // 기본 Photon 처리를 재개 base.OnCreateGame(info); 와 같다.

			// 클라이언트에게 오류 응답을 반환하며 처리를 취소
			/*			var errorDate = new System.Collections.Generic.Dictionary<byte, object>()
						{ { 1, "1번에러" }, {2, "2번에러"} };
						info.Fail("에러 메시지", errorData: null);*/

			// 플러그인에서 이벤트 전송
			// 이벤트 코드는 200미안으로

/*			var data = new Dictionary<byte, object>()
			{
				{ 3, "원하는 데이터" }
			};
			// 1번 방법
			BroadcastEvent(3, data);
			// 2번 방법 세분화
			PluginHost.BroadcastEvent(ReciverGroup.All, 0, 0, 3, data, CacheOperations.DoNotCache);

			// 일회용 타이머
			PluginHost.CreateOneTimeTimer(
				info,
				() =>
				{
					PluginHost.LogInfo("### 일회용타이머");
				}, 3000); // 단위 millsecond (1000 = 1초)

			// 반복 타이머
			PluginHost.CreateTimer(
				() =>
				{
					PluginHost.LogInfo("### 반복타이머");
				}, 3000, 2000); // 3초후 실행, 2초간격으로 반복실행*/
		}

		public override void OnJoin(IJoinGameCallInfo info) // 방에 접속한 후 받는 콜백
		{
			// 방에 참가 후
			PluginHost.LogInfo($"### OnJoin {info.Request.GameId} by user {info.UserId}");
			info.Continue();
		}

		public override void OnLeave(ILeaveGameCallInfo info) // 방에서 나간 후 받는 콜백
		{
			// 방에서 나간 후
			PluginHost.LogInfo($"### OnLeave by user {info.UserId}");
			info.Continue();
		}

		public override void OnRaiseEvent(IRaiseEventCallInfo info) // RaiseEvent 발생 후 받는 콜백
		{
			// RaiseEvent 발생 후
			PluginHost.LogInfo($"### OnRaiseEvent {info.Request.Data} by user {info.UserId}");
			info.Continue();
			// info.Cancel(); // 클라이언트에게 오류나 알림없이 처리를 건너뜀.
			// OnRaiseEvent와 BeforeSetProperties에서만 사용가능
			// OnRaiseEvent에서는 들어오는 Event를 무시
			// BeforeSetProperties에서는 속성 변경이 취소

			/*			ICallInfo 프로퍼티
						// 요청이 지연되지 않았는지
						bool isNew = info.IsNew;

						// 요청이 이미 처리되었는지 Continue, Cancel, Fail 메소드 호출됨
						bool isProcessed = info.IsProcessed;

						// 요청이 성공적으로 처리되었는지 Continue 메소드 호출됨
						bool isCanceld = info.IsCanceled;

						// 요청이 내부적으로 일시 중지되었는지 ( 예 동기 HTTP요청 전송)
						bool isPaused = info.IsPaused;

						// 요청이 내부적으로 지연되었는지 ( 예 비동기 HTTP요청 전송)
						bool isDeferred = info.IsDeferred;

						// 요청이 실패했는지 Fail 메소드 호출됨
						bool isFailed = info.IsFailed;*/

		}

		public override void OnSetProperties(ISetPropertiesCallInfo info) // 커스텀 프로퍼티를 Set 한 후 받는 콜백
		{
			// 커스텀 프로퍼티 Set 후
			PluginHost.LogInfo($"### OnSetProperties {info.Request.Properties.Keys} by user {info.UserId}");
			info.Continue();
		}

		protected override void OnChangeMasterClientId(int oldId, int newId) // 마스터클라이언트를 바꾼 후 받는 콜백
		{
			// 마스터 클라이언트가 바뀐 후
			PluginHost.LogInfo($"### OnChangeMasterClientId oldId {oldId}, newId {newId}");
			base.OnChangeMasterClientId(oldId, newId);
		}

    }
}