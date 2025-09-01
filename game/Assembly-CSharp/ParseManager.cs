using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Parse;
using Parse.Abstractions.Infrastructure;
using Parse.Infrastructure;
using Parse.Platform.Configuration;
using QFSW.QC;
using SimpleJSON;
using UnityEngine;

// Token: 0x0200010A RID: 266
public static class ParseManager
{
	// Token: 0x06000C8B RID: 3211 RVA: 0x000508D4 File Offset: 0x0004EAD4
	public static Task<bool> EditorInit(string masterKey)
	{
		ParseManager.<EditorInit>d__6 <EditorInit>d__;
		<EditorInit>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
		<EditorInit>d__.<>1__state = -1;
		<EditorInit>d__.<>t__builder.Start<ParseManager.<EditorInit>d__6>(ref <EditorInit>d__);
		return <EditorInit>d__.<>t__builder.Task;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x00050910 File Offset: 0x0004EB10
	public static void Initialize()
	{
		if (ParseManager.Initialized)
		{
			return;
		}
		ParseManager.Developers = new List<string>();
		ParseManager.IsBanned = false;
		new ParseClient(new ServerConnectionData
		{
			ApplicationID = "vellum",
			ServerURI = "https://vellumgame.com/parse/",
			Key = "850ccebc9f4b26f1ba1a5dc8ab6e390"
		}, new LateInitializedMutableServiceHub(), new IServiceHubMutator[]
		{
			new MetadataMutator
			{
				EnvironmentData = new EnvironmentData
				{
					OSVersion = SystemInfo.operatingSystem,
					Platform = string.Format("Unity {0} on {1}", Application.unityVersion, SystemInfo.operatingSystemFamily),
					TimeZone = TimeZoneInfo.Local.StandardName
				},
				HostManifestData = new HostManifestData
				{
					Name = Application.productName,
					Identifier = Application.productName,
					ShortVersion = Application.version,
					Version = Application.version
				}
			},
			new AbsoluteCacheLocationMutator
			{
				CustomAbsoluteCacheFilePath = string.Format("{0}{1}Parse.cache", Application.persistentDataPath.Replace('/', Path.DirectorySeparatorChar), Path.DirectorySeparatorChar)
			}
		}).Publicize();
		UnityEngine.Debug.Log("Parse Initialized");
		ParseManager.Initialized = true;
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x00050A44 File Offset: 0x0004EC44
	public static void Login(string userid, string username)
	{
		ParseManager.<Login>d__8 <Login>d__;
		<Login>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<Login>d__.userid = userid;
		<Login>d__.username = username;
		<Login>d__.<>1__state = -1;
		<Login>d__.<>t__builder.Start<ParseManager.<Login>d__8>(ref <Login>d__);
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00050A84 File Offset: 0x0004EC84
	public static void DoneTutorial()
	{
		if (!ParseManager.Initialized)
		{
			return;
		}
		ParseUser currentUser = ParseClient.Instance.GetCurrentUser();
		if (currentUser == null)
		{
			return;
		}
		currentUser.Set("DoneTutorial", true);
		currentUser.SaveAsync(default(CancellationToken));
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x00050ACC File Offset: 0x0004ECCC
	public static void SaveUnlocks()
	{
		if (!ParseManager.Initialized)
		{
			return;
		}
		ParseUser currentUser = ParseClient.Instance.GetCurrentUser();
		if (currentUser == null)
		{
			return;
		}
		JSONNode jsonnode = new JSONObject();
		JSONArray jsonarray = new JSONArray();
		foreach (UnlockDB.CoreUnlock coreUnlock in UnlockDB.DB.Cores)
		{
			if (UnlockManager.IsCoreUnlocked(coreUnlock.Core))
			{
				jsonarray.Add(coreUnlock.Core.Root.Name);
			}
		}
		jsonnode.Add("cores", jsonarray);
		JSONArray jsonarray2 = new JSONArray();
		foreach (UnlockDB.AbilityUnlock abilityUnlock in UnlockDB.DB.Abilities)
		{
			if (UnlockManager.IsAbilityUnlocked(abilityUnlock.Ability))
			{
				jsonarray2.Add(abilityUnlock.Ability.Root.Usage.AbilityMetadata.Name);
			}
		}
		jsonnode.Add("abilities", jsonarray2);
		JSONArray jsonarray3 = new JSONArray();
		foreach (UnlockDB.GenreUnlock genreUnlock in UnlockDB.DB.Genres)
		{
			if (UnlockManager.IsGenreUnlocked(genreUnlock.Genre))
			{
				jsonarray3.Add(genreUnlock.Genre.Root.ShortName);
			}
		}
		jsonnode.Add("tomes", jsonarray3);
		JSONArray jsonarray4 = new JSONArray();
		foreach (UnlockDB.BindingUnlock bindingUnlock in UnlockDB.DB.Bindings)
		{
			if (UnlockManager.IsBindingUnlocked(bindingUnlock.Binding))
			{
				jsonarray4.Add(bindingUnlock.Binding.Root.Name);
			}
		}
		jsonnode.Add("bindings", jsonarray4);
		List<string> list = new List<string>();
		foreach (Cosmetic cosmetic in CosmeticDB.AllCosmetics)
		{
			if (UnlockManager.IsCosmeticUnlocked(cosmetic))
			{
				list.Add(cosmetic.GUID);
			}
		}
		currentUser.Set("Cosmetics", list);
		currentUser.Set("Unlocks", jsonnode.ToString());
		currentUser.SaveAsync(default(CancellationToken));
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x00050D88 File Offset: 0x0004EF88
	public static void SaveStats()
	{
		if (!ParseManager.Initialized)
		{
			return;
		}
		ParseUser currentUser = ParseClient.Instance.GetCurrentUser();
		if (currentUser == null)
		{
			return;
		}
		JSONNode jsonnode = new JSONObject();
		jsonnode.Add("HighestBinding", GameStats.GetGlobalStat(GameStats.Stat.MaxBinding, 0));
		jsonnode.Add("TomesPlayed", GameStats.GetGlobalStat(GameStats.Stat.TomesPlayed, 0));
		jsonnode.Add("BindAttune", Progression.BindingAttunement);
		jsonnode.Add("Quillmarks", Currency.LoadoutCoin);
		jsonnode.Add("Gildings", Currency.Gildings);
		currentUser.Set("Stats", jsonnode.ToString());
		currentUser.Set("InkLevel", Progression.InkLevel);
		currentUser.Set("Ascended", Progression.PrestigeCount);
		ParseObject parseObject = currentUser;
		string key = "Talents";
		Progression.EquippedTalents talentBuild = Progression.TalentBuild;
		parseObject.Set(key, ((talentBuild != null) ? talentBuild.ToString() : null) ?? "");
		currentUser.Set("TomesWon", GameStats.GetGlobalStat(GameStats.Stat.TomesWon, 0));
		currentUser.SaveAsync(default(CancellationToken));
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x00050EA8 File Offset: 0x0004F0A8
	public static void UploadRun(string jsonData, string version, string genre, int playerCount, int bindingLevel, bool won, float duration, List<string> bindings, List<string> tornPages, List<string> fontPowers, string playerData, string lossData = "")
	{
		if (!ParseManager.CanInteractParse())
		{
			return;
		}
		ParseACL acl = new ParseACL(ParseClient.Instance.GetCurrentUser());
		ParseObject parseObject = new ParseObject("GameResults", null)
		{
			ACL = acl
		};
		parseObject.Set("version", version);
		parseObject.Set("genre", genre);
		parseObject.Set("playerCount", playerCount);
		parseObject.Set("bindingLevel", bindingLevel);
		parseObject.Set("won", won);
		parseObject.Set("bindings", bindings);
		parseObject.Set("tornPages", tornPages);
		parseObject.Set("fontPowers", fontPowers);
		parseObject.Set("playerData", playerData);
		parseObject.Set("duration", (int)duration);
		if (!won)
		{
			parseObject.Set("lossInfo", lossData);
		}
		byte[] bytes = Encoding.UTF8.GetBytes(jsonData);
		ParseFile value = new ParseFile("rundata.txt", bytes, null);
		parseObject["runData"] = value;
		parseObject.SaveAsync(default(CancellationToken));
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x00050FBC File Offset: 0x0004F1BC
	public static void UploadQuit(string jsonData, string version, string genre, int bindingLevel, List<string> bindings, List<string> tornPages, List<string> fontPowers, string playerData)
	{
		if (!ParseManager.CanInteractParse())
		{
			return;
		}
		ParseACL acl = new ParseACL(ParseClient.Instance.GetCurrentUser());
		ParseObject parseObject = new ParseObject("GameQuit", null);
		parseObject.ACL = acl;
		parseObject.Set("version", version);
		parseObject.Set("genre", genre);
		parseObject.Set("bindingLevel", bindingLevel);
		parseObject.Set("bindings", bindings);
		parseObject.Set("tornPages", tornPages);
		parseObject.Set("fontPowers", fontPowers);
		parseObject.Set("playerData", playerData);
		byte[] bytes = Encoding.UTF8.GetBytes(jsonData);
		ParseFile value = new ParseFile("rundata.txt", bytes, null);
		parseObject["runData"] = value;
		parseObject.SaveAsync(default(CancellationToken));
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x00051084 File Offset: 0x0004F284
	public static void UploadAppendix(string version, string genre, int bindingLevel, bool won, int playerCount, float level, List<string> bindings, List<string> tornPages, List<string> fontPowers, string playerData)
	{
		if (!ParseManager.CanInteractParse())
		{
			return;
		}
		ParseACL acl = new ParseACL(ParseClient.Instance.GetCurrentUser());
		ParseObject parseObject = new ParseObject("EndlessResults", null);
		parseObject.ACL = acl;
		parseObject.Set("version", version);
		parseObject.Set("genre", genre);
		parseObject.Set("won", won);
		parseObject.Set("bindingLevel", bindingLevel);
		parseObject.Set("playerCount", playerCount);
		parseObject.Set("levelReached", level);
		parseObject.Set("bindings", bindings);
		parseObject.Set("tornPages", tornPages);
		parseObject.Set("fontPowers", fontPowers);
		parseObject.Set("playerData", playerData);
		parseObject.SaveAsync(default(CancellationToken));
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x0005115C File Offset: 0x0004F35C
	public static void UploadRaidEncounter(string version, RaidRecord record, int playerCount, string playerData, string encounterData)
	{
		if (!ParseManager.CanInteractParse())
		{
			return;
		}
		ParseACL acl = new ParseACL(ParseClient.Instance.GetCurrentUser());
		ParseObject parseObject = new ParseObject("RaidResults", null);
		parseObject.ACL = acl;
		parseObject.Set("version", version);
		parseObject.Set("encounter", record.Encounter);
		parseObject.Set("result", record.EndResult.ToString());
		parseObject.Set("hard", record.HardMode);
		parseObject.Set("playerCount", playerCount);
		parseObject.Set("playerData", playerData);
		parseObject.Set("encounterData", encounterData);
		parseObject.Set("playerDeaths", record.DeathsTo);
		parseObject.SaveAsync(default(CancellationToken));
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x0005122C File Offset: 0x0004F42C
	public static Task<JSONNode> FetchTomeStats(string tomeID)
	{
		ParseManager.<FetchTomeStats>d__16 <FetchTomeStats>d__;
		<FetchTomeStats>d__.<>t__builder = AsyncTaskMethodBuilder<JSONNode>.Create();
		<FetchTomeStats>d__.tomeID = tomeID;
		<FetchTomeStats>d__.<>1__state = -1;
		<FetchTomeStats>d__.<>t__builder.Start<ParseManager.<FetchTomeStats>d__16>(ref <FetchTomeStats>d__);
		return <FetchTomeStats>d__.<>t__builder.Task;
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x00051270 File Offset: 0x0004F470
	public static Task<JSONNode> FetchChallengeStats(string challengeID, int loop)
	{
		ParseManager.<FetchChallengeStats>d__17 <FetchChallengeStats>d__;
		<FetchChallengeStats>d__.<>t__builder = AsyncTaskMethodBuilder<JSONNode>.Create();
		<FetchChallengeStats>d__.challengeID = challengeID;
		<FetchChallengeStats>d__.loop = loop;
		<FetchChallengeStats>d__.<>1__state = -1;
		<FetchChallengeStats>d__.<>t__builder.Start<ParseManager.<FetchChallengeStats>d__17>(ref <FetchChallengeStats>d__);
		return <FetchChallengeStats>d__.<>t__builder.Task;
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x000512BC File Offset: 0x0004F4BC
	public static void UploadChallenge(string version, string ID, int loop, bool won, int playerCount, int Appendix, float baseTime, float totalTime, string playerData, int specialStat)
	{
		if (!ParseManager.CanInteractParse())
		{
			return;
		}
		ParseACL acl = new ParseACL(ParseClient.Instance.GetCurrentUser());
		ParseObject parseObject = new ParseObject("ChallengeResults", null);
		parseObject.ACL = acl;
		parseObject.Set("version", version);
		parseObject.Set("challenge", ID);
		parseObject.Set("loop", loop);
		parseObject.Set("won", won);
		parseObject.Set("appendix", Appendix);
		parseObject.Set("playerCount", playerCount);
		parseObject.Set("baseTime", (int)baseTime);
		parseObject.Set("totalTime", (int)totalTime);
		parseObject.Set("playerData", playerData);
		parseObject.Set("specialStat", specialStat);
		parseObject.SaveAsync(default(CancellationToken));
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x000513A8 File Offset: 0x0004F5A8
	public static void FetchData(string runObj)
	{
		ParseManager.<FetchData>d__19 <FetchData>d__;
		<FetchData>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<FetchData>d__.<>1__state = -1;
		<FetchData>d__.<>t__builder.Start<ParseManager.<FetchData>d__19>(ref <FetchData>d__);
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x000513D7 File Offset: 0x0004F5D7
	public static bool IsDeveloper(string userID)
	{
		return ParseManager.Developers.Contains(userID);
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x000513E4 File Offset: 0x0004F5E4
	private static bool CanInteractParse()
	{
		if (!ParseManager.Initialized)
		{
			UnityEngine.Debug.LogError("Parse not initialized - Can't upload run data");
			return false;
		}
		if (ParseClient.Instance == null || ParseClient.Instance.GetCurrentUser() == null)
		{
			UnityEngine.Debug.LogError("Parse Client/User Not Valid - Can't upload run data");
			return false;
		}
		return true;
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x00051419 File Offset: 0x0004F619
	// Note: this type is marked as 'beforefieldinit'.
	static ParseManager()
	{
	}

	// Token: 0x04000A05 RID: 2565
	private const string AppID = "vellum";

	// Token: 0x04000A06 RID: 2566
	private const string ServerURI = "https://vellumgame.com/parse/";

	// Token: 0x04000A07 RID: 2567
	private const string NetKey = "850ccebc9f4b26f1ba1a5dc8ab6e390";

	// Token: 0x04000A08 RID: 2568
	private static List<string> Developers = new List<string>();

	// Token: 0x04000A09 RID: 2569
	public static bool Initialized;

	// Token: 0x04000A0A RID: 2570
	public static bool IsBanned = false;

	// Token: 0x02000509 RID: 1289
	[CompilerGenerated]
	[StructLayout(LayoutKind.Auto)]
	private struct <EditorInit>d__6 : IAsyncStateMachine
	{
		// Token: 0x060023A3 RID: 9123 RVA: 0x000CB0C8 File Offset: 0x000C92C8
		void IAsyncStateMachine.MoveNext()
		{
			bool result;
			try
			{
				result = false;
			}
			catch (Exception exception)
			{
				this.<>1__state = -2;
				this.<>t__builder.SetException(exception);
				return;
			}
			this.<>1__state = -2;
			this.<>t__builder.SetResult(result);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000CB114 File Offset: 0x000C9314
		[DebuggerHidden]
		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.<>t__builder.SetStateMachine(stateMachine);
		}

		// Token: 0x04002596 RID: 9622
		public int <>1__state;

		// Token: 0x04002597 RID: 9623
		public AsyncTaskMethodBuilder<bool> <>t__builder;
	}

	// Token: 0x0200050A RID: 1290
	[CompilerGenerated]
	private sealed class <>c__DisplayClass8_0
	{
		// Token: 0x060023A5 RID: 9125 RVA: 0x000CB122 File Offset: 0x000C9322
		public <>c__DisplayClass8_0()
		{
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x000CB12C File Offset: 0x000C932C
		internal void <Login>b__0(Task<ParseConfiguration> t)
		{
			if (t.IsFaulted || t.IsCanceled)
			{
				AggregateException exception = t.Exception;
				UnityEngine.Debug.LogError((exception != null) ? exception.Message : null);
				return;
			}
			ParseManager.Developers = t.Result.Get<IList<string>>("Developers").ToList<string>();
			if (ParseManager.IsDeveloper(this.userid))
			{
				QuantumConsole.IsDev = true;
			}
		}

		// Token: 0x04002598 RID: 9624
		public string userid;
	}

	// Token: 0x0200050B RID: 1291
	[CompilerGenerated]
	[StructLayout(LayoutKind.Auto)]
	private struct <Login>d__8 : IAsyncStateMachine
	{
		// Token: 0x060023A7 RID: 9127 RVA: 0x000CB190 File Offset: 0x000C9390
		void IAsyncStateMachine.MoveNext()
		{
			int num = this.<>1__state;
			try
			{
				TaskAwaiter awaiter;
				switch (num)
				{
				case 0:
					break;
				case 1:
					awaiter = this.<>u__2;
					this.<>u__2 = default(TaskAwaiter);
					this.<>1__state = -1;
					goto IL_198;
				case 2:
					awaiter = this.<>u__2;
					this.<>u__2 = default(TaskAwaiter);
					this.<>1__state = -1;
					goto IL_2A0;
				case 3:
					awaiter = this.<>u__2;
					this.<>u__2 = default(TaskAwaiter);
					this.<>1__state = -1;
					goto IL_329;
				default:
					this.<>8__1 = new ParseManager.<>c__DisplayClass8_0();
					this.<>8__1.userid = this.userid;
					if (!ParseManager.Initialized)
					{
						UnityEngine.Debug.LogError("Parse Control not yet initialized, can't log in");
						goto IL_360;
					}
					this.<client>5__2 = ParseClient.Instance;
					if (this.<client>5__2 == null)
					{
						UnityEngine.Debug.LogError("No ParseClient Initialized, can't log in");
						goto IL_360;
					}
					this.<user>5__3 = null;
					break;
				}
				try
				{
					TaskAwaiter<ParseUser> awaiter2;
					if (num != 0)
					{
						awaiter2 = this.<client>5__2.LogInAsync(this.<>8__1.userid, this.<>8__1.userid, default(CancellationToken)).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ParseUser>, ParseManager.<Login>d__8>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<ParseUser>);
						this.<>1__state = -1;
					}
					ParseUser result = awaiter2.GetResult();
					this.<user>5__3 = result;
				}
				catch
				{
				}
				if (this.<user>5__3 != null)
				{
					UnityEngine.Debug.Log("Parse User Found!");
					goto IL_1C6;
				}
				UnityEngine.Debug.Log("Login Failed - Creating new user");
				awaiter = this.<client>5__2.SignUpAsync(this.<>8__1.userid, this.<>8__1.userid, default(CancellationToken)).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					this.<>1__state = 1;
					this.<>u__2 = awaiter;
					this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, ParseManager.<Login>d__8>(ref awaiter, ref this);
					return;
				}
				IL_198:
				awaiter.GetResult();
				this.<user>5__3 = this.<client>5__2.GetCurrentUser();
				UnityEngine.Debug.Log("Parse User Created");
				IL_1C6:
				string a = "";
				if (this.<user>5__3.ContainsKey("nickname"))
				{
					this.<user>5__3.Get<string>("nickname");
				}
				if (a != this.username)
				{
					this.<user>5__3.Set("nickname", this.username);
				}
				this.<user>5__3.Set("LatestVersion", Application.version);
				ParseManager.IsBanned = this.<user>5__3.Get<bool>("Banned");
				awaiter = this.<user>5__3.SaveAsync(default(CancellationToken)).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					this.<>1__state = 2;
					this.<>u__2 = awaiter;
					this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, ParseManager.<Login>d__8>(ref awaiter, ref this);
					return;
				}
				IL_2A0:
				awaiter.GetResult();
				UnityEngine.Debug.Log("Parse User Updated - Ready!");
				awaiter = this.<client>5__2.GetConfigurationAsync(default(CancellationToken)).ContinueWith(new Action<Task<ParseConfiguration>>(this.<>8__1.<Login>b__0)).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					this.<>1__state = 3;
					this.<>u__2 = awaiter;
					this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, ParseManager.<Login>d__8>(ref awaiter, ref this);
					return;
				}
				IL_329:
				awaiter.GetResult();
			}
			catch (Exception exception)
			{
				this.<>1__state = -2;
				this.<>8__1 = null;
				this.<client>5__2 = null;
				this.<user>5__3 = null;
				this.<>t__builder.SetException(exception);
				return;
			}
			IL_360:
			this.<>1__state = -2;
			this.<>8__1 = null;
			this.<client>5__2 = null;
			this.<user>5__3 = null;
			this.<>t__builder.SetResult();
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x000CB55C File Offset: 0x000C975C
		[DebuggerHidden]
		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.<>t__builder.SetStateMachine(stateMachine);
		}

		// Token: 0x04002599 RID: 9625
		public int <>1__state;

		// Token: 0x0400259A RID: 9626
		public AsyncVoidMethodBuilder <>t__builder;

		// Token: 0x0400259B RID: 9627
		public string userid;

		// Token: 0x0400259C RID: 9628
		private ParseManager.<>c__DisplayClass8_0 <>8__1;

		// Token: 0x0400259D RID: 9629
		public string username;

		// Token: 0x0400259E RID: 9630
		private ParseClient <client>5__2;

		// Token: 0x0400259F RID: 9631
		private ParseUser <user>5__3;

		// Token: 0x040025A0 RID: 9632
		private TaskAwaiter<ParseUser> <>u__1;

		// Token: 0x040025A1 RID: 9633
		private TaskAwaiter <>u__2;
	}

	// Token: 0x0200050C RID: 1292
	[CompilerGenerated]
	[StructLayout(LayoutKind.Auto)]
	private struct <FetchTomeStats>d__16 : IAsyncStateMachine
	{
		// Token: 0x060023A9 RID: 9129 RVA: 0x000CB56C File Offset: 0x000C976C
		void IAsyncStateMachine.MoveNext()
		{
			int num = this.<>1__state;
			JSONNode result;
			try
			{
				ParseQuery<ParseObject> parseQuery;
				if (num != 0)
				{
					if (!ParseManager.CanInteractParse())
					{
						result = null;
						goto IL_158;
					}
					parseQuery = ParseClient.Instance.GetQuery("TomeStats");
					parseQuery = parseQuery.WhereEqualTo("TomeID", this.tomeID);
				}
				try
				{
					TaskAwaiter<ParseObject> awaiter;
					if (num != 0)
					{
						awaiter = parseQuery.FirstAsync().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ParseObject>, ParseManager.<FetchTomeStats>d__16>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<ParseObject>);
						this.<>1__state = -1;
					}
					ParseObject result2 = awaiter.GetResult();
					if (result2 != null)
					{
						JSONNode jsonnode = new JSONObject();
						if (result2.ContainsKey("Bindings"))
						{
							JSONNode aItem = JSON.Parse(result2.Get<string>("Bindings"));
							jsonnode.Add("Bindings", aItem);
						}
						if (result2.ContainsKey("Timers"))
						{
							JSONNode aItem2 = JSON.Parse(result2.Get<string>("Timers"));
							jsonnode.Add("Timers", aItem2);
						}
						if (result2.ContainsKey("Appendix"))
						{
							JSONNode aItem3 = JSON.Parse(result2.Get<string>("Appendix"));
							jsonnode.Add("Appendix", aItem3);
						}
						result = jsonnode;
						goto IL_158;
					}
				}
				catch (Exception)
				{
				}
				result = null;
			}
			catch (Exception exception)
			{
				this.<>1__state = -2;
				this.<>t__builder.SetException(exception);
				return;
			}
			IL_158:
			this.<>1__state = -2;
			this.<>t__builder.SetResult(result);
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x000CB71C File Offset: 0x000C991C
		[DebuggerHidden]
		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.<>t__builder.SetStateMachine(stateMachine);
		}

		// Token: 0x040025A2 RID: 9634
		public int <>1__state;

		// Token: 0x040025A3 RID: 9635
		public AsyncTaskMethodBuilder<JSONNode> <>t__builder;

		// Token: 0x040025A4 RID: 9636
		public string tomeID;

		// Token: 0x040025A5 RID: 9637
		private TaskAwaiter<ParseObject> <>u__1;
	}

	// Token: 0x0200050D RID: 1293
	[CompilerGenerated]
	[StructLayout(LayoutKind.Auto)]
	private struct <FetchChallengeStats>d__17 : IAsyncStateMachine
	{
		// Token: 0x060023AB RID: 9131 RVA: 0x000CB72C File Offset: 0x000C992C
		void IAsyncStateMachine.MoveNext()
		{
			int num = this.<>1__state;
			JSONNode result;
			try
			{
				ParseQuery<ParseObject> parseQuery;
				if (num != 0)
				{
					if (!ParseManager.CanInteractParse())
					{
						result = null;
						goto IL_16F;
					}
					parseQuery = ParseClient.Instance.GetQuery("ChallengeStats");
					parseQuery = parseQuery.WhereEqualTo("Challenge", this.challengeID);
					parseQuery = parseQuery.WhereEqualTo("Loop", this.loop);
				}
				try
				{
					TaskAwaiter<ParseObject> awaiter;
					if (num != 0)
					{
						awaiter = parseQuery.FirstAsync().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ParseObject>, ParseManager.<FetchChallengeStats>d__17>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<ParseObject>);
						this.<>1__state = -1;
					}
					ParseObject result2 = awaiter.GetResult();
					if (result2 != null)
					{
						JSONNode jsonnode = new JSONObject();
						if (result2.ContainsKey("Timers"))
						{
							JSONNode aItem = JSON.Parse(result2.Get<string>("Timers"));
							jsonnode.Add("Timers", aItem);
						}
						if (result2.ContainsKey("Appendix"))
						{
							JSONNode aItem2 = JSON.Parse(result2.Get<string>("Appendix"));
							jsonnode.Add("Appendix", aItem2);
						}
						if (result2.ContainsKey("SpecialStats"))
						{
							JSONNode aItem3 = JSON.Parse(result2.Get<string>("SpecialStats"));
							jsonnode.Add("SpecialStats", aItem3);
						}
						result = jsonnode;
						goto IL_16F;
					}
				}
				catch (Exception)
				{
				}
				result = null;
			}
			catch (Exception exception)
			{
				this.<>1__state = -2;
				this.<>t__builder.SetException(exception);
				return;
			}
			IL_16F:
			this.<>1__state = -2;
			this.<>t__builder.SetResult(result);
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000CB8F0 File Offset: 0x000C9AF0
		[DebuggerHidden]
		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.<>t__builder.SetStateMachine(stateMachine);
		}

		// Token: 0x040025A6 RID: 9638
		public int <>1__state;

		// Token: 0x040025A7 RID: 9639
		public AsyncTaskMethodBuilder<JSONNode> <>t__builder;

		// Token: 0x040025A8 RID: 9640
		public string challengeID;

		// Token: 0x040025A9 RID: 9641
		public int loop;

		// Token: 0x040025AA RID: 9642
		private TaskAwaiter<ParseObject> <>u__1;
	}

	// Token: 0x0200050E RID: 1294
	[CompilerGenerated]
	[StructLayout(LayoutKind.Auto)]
	private struct <FetchData>d__19 : IAsyncStateMachine
	{
		// Token: 0x060023AD RID: 9133 RVA: 0x000CB900 File Offset: 0x000C9B00
		void IAsyncStateMachine.MoveNext()
		{
			int num = this.<>1__state;
			try
			{
				TaskAwaiter<ParseObject> awaiter;
				if (num != 0)
				{
					if (!ParseManager.CanInteractParse())
					{
						goto IL_B0;
					}
					ParseQuery<ParseObject> query = ParseClient.Instance.GetQuery("Runs");
					query.WhereEqualTo("Version", 1.0);
					awaiter = query.FirstAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ParseObject>, ParseManager.<FetchData>d__19>(ref awaiter, ref this);
						return;
					}
				}
				else
				{
					awaiter = this.<>u__1;
					this.<>u__1 = default(TaskAwaiter<ParseObject>);
					this.<>1__state = -1;
				}
				awaiter.GetResult();
			}
			catch (Exception exception)
			{
				this.<>1__state = -2;
				this.<>t__builder.SetException(exception);
				return;
			}
			IL_B0:
			this.<>1__state = -2;
			this.<>t__builder.SetResult();
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000CB9E0 File Offset: 0x000C9BE0
		[DebuggerHidden]
		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.<>t__builder.SetStateMachine(stateMachine);
		}

		// Token: 0x040025AB RID: 9643
		public int <>1__state;

		// Token: 0x040025AC RID: 9644
		public AsyncVoidMethodBuilder <>t__builder;

		// Token: 0x040025AD RID: 9645
		private TaskAwaiter<ParseObject> <>u__1;
	}
}
