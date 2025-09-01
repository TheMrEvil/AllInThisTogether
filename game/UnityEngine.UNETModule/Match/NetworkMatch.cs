using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200001C RID: 28
	[Obsolete("The matchmaker and relay feature will be removed in the future, minimal support will continue until this can be safely done.")]
	public class NetworkMatch : MonoBehaviour
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00004940 File Offset: 0x00002B40
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00004958 File Offset: 0x00002B58
		public Uri baseUri
		{
			get
			{
				return this.m_BaseUri;
			}
			set
			{
				this.m_BaseUri = value;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00003C81 File Offset: 0x00001E81
		[Obsolete("This function is not used any longer to interface with the matchmaker. Please set up your project by logging in through the editor connect dialog.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetProgramAppID(AppID programAppID)
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004964 File Offset: 0x00002B64
		public Coroutine CreateMatch(string matchName, uint matchSize, bool matchAdvertise, string matchPassword, string publicClientAddress, string privateClientAddress, int eloScoreForMatch, int requestDomain, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
		{
			bool flag = Application.platform == RuntimePlatform.WebGLPlayer;
			Coroutine result;
			if (flag)
			{
				Debug.LogError("Matchmaking is not supported on WebGL player.");
				result = null;
			}
			else
			{
				result = this.CreateMatch(new CreateMatchRequest
				{
					name = matchName,
					size = matchSize,
					advertise = matchAdvertise,
					password = matchPassword,
					publicAddress = publicClientAddress,
					privateAddress = privateClientAddress,
					eloScore = eloScoreForMatch,
					domain = requestDomain
				}, callback);
			}
			return result;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000049E8 File Offset: 0x00002BE8
		internal Coroutine CreateMatch(CreateMatchRequest req, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
		{
			bool flag = callback == null;
			Coroutine result;
			if (flag)
			{
				Debug.Log("callback supplied is null, aborting CreateMatch Request.");
				result = null;
			}
			else
			{
				Uri uri = new Uri(this.baseUri, "/json/reply/CreateMatchRequest");
				string str = "MatchMakingClient Create :";
				Uri uri2 = uri;
				Debug.Log(str + ((uri2 != null) ? uri2.ToString() : null));
				WWWForm wwwform = new WWWForm();
				wwwform.AddField("version", Request.currentVersion);
				wwwform.AddField("projectId", Application.cloudProjectId);
				wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
				wwwform.AddField("accessTokenString", 0);
				wwwform.AddField("domain", req.domain);
				wwwform.AddField("name", req.name);
				wwwform.AddField("size", req.size.ToString());
				wwwform.AddField("advertise", req.advertise.ToString());
				wwwform.AddField("password", req.password);
				wwwform.AddField("publicAddress", req.publicAddress);
				wwwform.AddField("privateAddress", req.privateAddress);
				wwwform.AddField("eloScore", req.eloScore.ToString());
				wwwform.headers["Accept"] = "application/json";
				UnityWebRequest client = UnityWebRequest.Post(uri.ToString(), wwwform);
				result = base.StartCoroutine(this.ProcessMatchResponse<CreateMatchResponse, NetworkMatch.DataResponseDelegate<MatchInfo>>(client, new NetworkMatch.InternalResponseDelegate<CreateMatchResponse, NetworkMatch.DataResponseDelegate<MatchInfo>>(this.OnMatchCreate), callback));
			}
			return result;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004B84 File Offset: 0x00002D84
		internal virtual void OnMatchCreate(CreateMatchResponse response, NetworkMatch.DataResponseDelegate<MatchInfo> userCallback)
		{
			bool success = response.success;
			if (success)
			{
				Utility.SetAccessTokenForNetwork((NetworkID)response.networkId, new NetworkAccessToken(response.accessTokenString));
			}
			userCallback(response.success, response.extendedInfo, new MatchInfo(response));
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004BCC File Offset: 0x00002DCC
		public Coroutine JoinMatch(NetworkID netId, string matchPassword, string publicClientAddress, string privateClientAddress, int eloScoreForClient, int requestDomain, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
		{
			return this.JoinMatch(new JoinMatchRequest
			{
				networkId = netId,
				password = matchPassword,
				publicAddress = publicClientAddress,
				privateAddress = privateClientAddress,
				eloScore = eloScoreForClient,
				domain = requestDomain
			}, callback);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004C20 File Offset: 0x00002E20
		internal Coroutine JoinMatch(JoinMatchRequest req, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
		{
			bool flag = callback == null;
			Coroutine result;
			if (flag)
			{
				Debug.Log("callback supplied is null, aborting JoinMatch Request.");
				result = null;
			}
			else
			{
				Uri uri = new Uri(this.baseUri, "/json/reply/JoinMatchRequest");
				string str = "MatchMakingClient Join :";
				Uri uri2 = uri;
				Debug.Log(str + ((uri2 != null) ? uri2.ToString() : null));
				WWWForm wwwform = new WWWForm();
				wwwform.AddField("version", Request.currentVersion);
				wwwform.AddField("projectId", Application.cloudProjectId);
				wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
				wwwform.AddField("accessTokenString", 0);
				wwwform.AddField("domain", req.domain);
				wwwform.AddField("networkId", req.networkId.ToString());
				wwwform.AddField("password", req.password);
				wwwform.AddField("publicAddress", req.publicAddress);
				wwwform.AddField("privateAddress", req.privateAddress);
				wwwform.AddField("eloScore", req.eloScore.ToString());
				wwwform.headers["Accept"] = "application/json";
				UnityWebRequest client = UnityWebRequest.Post(uri.ToString(), wwwform);
				result = base.StartCoroutine(this.ProcessMatchResponse<JoinMatchResponse, NetworkMatch.DataResponseDelegate<MatchInfo>>(client, new NetworkMatch.InternalResponseDelegate<JoinMatchResponse, NetworkMatch.DataResponseDelegate<MatchInfo>>(this.OnMatchJoined), callback));
			}
			return result;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004D94 File Offset: 0x00002F94
		internal void OnMatchJoined(JoinMatchResponse response, NetworkMatch.DataResponseDelegate<MatchInfo> userCallback)
		{
			bool success = response.success;
			if (success)
			{
				Utility.SetAccessTokenForNetwork((NetworkID)response.networkId, new NetworkAccessToken(response.accessTokenString));
			}
			userCallback(response.success, response.extendedInfo, new MatchInfo(response));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004DDC File Offset: 0x00002FDC
		public Coroutine DestroyMatch(NetworkID netId, int requestDomain, NetworkMatch.BasicResponseDelegate callback)
		{
			return this.DestroyMatch(new DestroyMatchRequest
			{
				networkId = netId,
				domain = requestDomain
			}, callback);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004E0C File Offset: 0x0000300C
		internal Coroutine DestroyMatch(DestroyMatchRequest req, NetworkMatch.BasicResponseDelegate callback)
		{
			bool flag = callback == null;
			Coroutine result;
			if (flag)
			{
				Debug.Log("callback supplied is null, aborting DestroyMatch Request.");
				result = null;
			}
			else
			{
				Uri uri = new Uri(this.baseUri, "/json/reply/DestroyMatchRequest");
				string str = "MatchMakingClient Destroy :";
				Uri uri2 = uri;
				Debug.Log(str + ((uri2 != null) ? uri2.ToString() : null));
				WWWForm wwwform = new WWWForm();
				wwwform.AddField("version", Request.currentVersion);
				wwwform.AddField("projectId", Application.cloudProjectId);
				wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
				wwwform.AddField("accessTokenString", Utility.GetAccessTokenForNetwork(req.networkId).GetByteString());
				wwwform.AddField("domain", req.domain);
				wwwform.AddField("networkId", req.networkId.ToString());
				wwwform.headers["Accept"] = "application/json";
				UnityWebRequest client = UnityWebRequest.Post(uri.ToString(), wwwform);
				result = base.StartCoroutine(this.ProcessMatchResponse<BasicResponse, NetworkMatch.BasicResponseDelegate>(client, new NetworkMatch.InternalResponseDelegate<BasicResponse, NetworkMatch.BasicResponseDelegate>(this.OnMatchDestroyed), callback));
			}
			return result;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00004F3E File Offset: 0x0000313E
		internal void OnMatchDestroyed(BasicResponse response, NetworkMatch.BasicResponseDelegate userCallback)
		{
			userCallback(response.success, response.extendedInfo);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004F54 File Offset: 0x00003154
		public Coroutine DropConnection(NetworkID netId, NodeID dropNodeId, int requestDomain, NetworkMatch.BasicResponseDelegate callback)
		{
			return this.DropConnection(new DropConnectionRequest
			{
				networkId = netId,
				nodeId = dropNodeId,
				domain = requestDomain
			}, callback);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004F8C File Offset: 0x0000318C
		internal Coroutine DropConnection(DropConnectionRequest req, NetworkMatch.BasicResponseDelegate callback)
		{
			bool flag = callback == null;
			Coroutine result;
			if (flag)
			{
				Debug.Log("callback supplied is null, aborting DropConnection Request.");
				result = null;
			}
			else
			{
				Uri uri = new Uri(this.baseUri, "/json/reply/DropConnectionRequest");
				string str = "MatchMakingClient DropConnection :";
				Uri uri2 = uri;
				Debug.Log(str + ((uri2 != null) ? uri2.ToString() : null));
				WWWForm wwwform = new WWWForm();
				wwwform.AddField("version", Request.currentVersion);
				wwwform.AddField("projectId", Application.cloudProjectId);
				wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
				wwwform.AddField("accessTokenString", Utility.GetAccessTokenForNetwork(req.networkId).GetByteString());
				wwwform.AddField("domain", req.domain);
				wwwform.AddField("networkId", req.networkId.ToString());
				wwwform.AddField("nodeId", req.nodeId.ToString());
				wwwform.headers["Accept"] = "application/json";
				UnityWebRequest client = UnityWebRequest.Post(uri.ToString(), wwwform);
				result = base.StartCoroutine(this.ProcessMatchResponse<DropConnectionResponse, NetworkMatch.BasicResponseDelegate>(client, new NetworkMatch.InternalResponseDelegate<DropConnectionResponse, NetworkMatch.BasicResponseDelegate>(this.OnDropConnection), callback));
			}
			return result;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00004F3E File Offset: 0x0000313E
		internal void OnDropConnection(DropConnectionResponse response, NetworkMatch.BasicResponseDelegate userCallback)
		{
			userCallback(response.success, response.extendedInfo);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000050E0 File Offset: 0x000032E0
		public Coroutine ListMatches(int startPageNumber, int resultPageSize, string matchNameFilter, bool filterOutPrivateMatchesFromResults, int eloScoreTarget, int requestDomain, NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>> callback)
		{
			bool flag = Application.platform == RuntimePlatform.WebGLPlayer;
			Coroutine result;
			if (flag)
			{
				Debug.LogError("Matchmaking is not supported on WebGL player.");
				result = null;
			}
			else
			{
				result = this.ListMatches(new ListMatchRequest
				{
					pageNum = startPageNumber,
					pageSize = resultPageSize,
					nameFilter = matchNameFilter,
					filterOutPrivateMatches = filterOutPrivateMatchesFromResults,
					eloScore = eloScoreTarget,
					domain = requestDomain
				}, callback);
			}
			return result;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005150 File Offset: 0x00003350
		internal Coroutine ListMatches(ListMatchRequest req, NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>> callback)
		{
			bool flag = callback == null;
			Coroutine result;
			if (flag)
			{
				Debug.Log("callback supplied is null, aborting ListMatch Request.");
				result = null;
			}
			else
			{
				Uri uri = new Uri(this.baseUri, "/json/reply/ListMatchRequest");
				string str = "MatchMakingClient ListMatches :";
				Uri uri2 = uri;
				Debug.Log(str + ((uri2 != null) ? uri2.ToString() : null));
				WWWForm wwwform = new WWWForm();
				wwwform.AddField("version", Request.currentVersion);
				wwwform.AddField("projectId", Application.cloudProjectId);
				wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
				wwwform.AddField("accessTokenString", 0);
				wwwform.AddField("domain", req.domain);
				wwwform.AddField("pageSize", req.pageSize);
				wwwform.AddField("pageNum", req.pageNum);
				wwwform.AddField("nameFilter", req.nameFilter);
				wwwform.AddField("filterOutPrivateMatches", req.filterOutPrivateMatches.ToString());
				wwwform.AddField("eloScore", req.eloScore.ToString());
				wwwform.headers["Accept"] = "application/json";
				UnityWebRequest client = UnityWebRequest.Post(uri.ToString(), wwwform);
				result = base.StartCoroutine(this.ProcessMatchResponse<ListMatchResponse, NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>>>(client, new NetworkMatch.InternalResponseDelegate<ListMatchResponse, NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>>>(this.OnMatchList), callback));
			}
			return result;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000052C0 File Offset: 0x000034C0
		internal void OnMatchList(ListMatchResponse response, NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>> userCallback)
		{
			List<MatchInfoSnapshot> list = new List<MatchInfoSnapshot>();
			foreach (MatchDesc matchDesc in response.matches)
			{
				list.Add(new MatchInfoSnapshot(matchDesc));
			}
			userCallback(response.success, response.extendedInfo, list);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005338 File Offset: 0x00003538
		public Coroutine SetMatchAttributes(NetworkID networkId, bool isListed, int requestDomain, NetworkMatch.BasicResponseDelegate callback)
		{
			return this.SetMatchAttributes(new SetMatchAttributesRequest
			{
				networkId = networkId,
				isListed = isListed,
				domain = requestDomain
			}, callback);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005370 File Offset: 0x00003570
		internal Coroutine SetMatchAttributes(SetMatchAttributesRequest req, NetworkMatch.BasicResponseDelegate callback)
		{
			bool flag = callback == null;
			Coroutine result;
			if (flag)
			{
				Debug.Log("callback supplied is null, aborting SetMatchAttributes Request.");
				result = null;
			}
			else
			{
				Uri uri = new Uri(this.baseUri, "/json/reply/SetMatchAttributesRequest");
				string str = "MatchMakingClient SetMatchAttributes :";
				Uri uri2 = uri;
				Debug.Log(str + ((uri2 != null) ? uri2.ToString() : null));
				WWWForm wwwform = new WWWForm();
				wwwform.AddField("version", Request.currentVersion);
				wwwform.AddField("projectId", Application.cloudProjectId);
				wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
				wwwform.AddField("accessTokenString", Utility.GetAccessTokenForNetwork(req.networkId).GetByteString());
				wwwform.AddField("domain", req.domain);
				wwwform.AddField("networkId", req.networkId.ToString());
				wwwform.AddField("isListed", req.isListed.ToString());
				wwwform.headers["Accept"] = "application/json";
				UnityWebRequest client = UnityWebRequest.Post(uri.ToString(), wwwform);
				result = base.StartCoroutine(this.ProcessMatchResponse<BasicResponse, NetworkMatch.BasicResponseDelegate>(client, new NetworkMatch.InternalResponseDelegate<BasicResponse, NetworkMatch.BasicResponseDelegate>(this.OnSetMatchAttributes), callback));
			}
			return result;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004F3E File Offset: 0x0000313E
		internal void OnSetMatchAttributes(BasicResponse response, NetworkMatch.BasicResponseDelegate userCallback)
		{
			userCallback(response.success, response.extendedInfo);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000054BD File Offset: 0x000036BD
		private IEnumerator ProcessMatchResponse<JSONRESPONSE, USERRESPONSEDELEGATETYPE>(UnityWebRequest client, NetworkMatch.InternalResponseDelegate<JSONRESPONSE, USERRESPONSEDELEGATETYPE> internalCallback, USERRESPONSEDELEGATETYPE userCallback) where JSONRESPONSE : Response, new()
		{
			yield return client.SendWebRequest();
			JSONRESPONSE jsonInterface = Activator.CreateInstance<JSONRESPONSE>();
			bool flag = client.result == UnityWebRequest.Result.Success;
			if (flag)
			{
				try
				{
					JsonUtility.FromJsonOverwrite(client.downloadHandler.text, jsonInterface);
				}
				catch (ArgumentException ex)
				{
					ArgumentException exception = ex;
					jsonInterface.SetFailure(UnityString.Format("ArgumentException:[{0}] ", new object[]
					{
						exception.ToString()
					}));
				}
			}
			else
			{
				jsonInterface.SetFailure(UnityString.Format("Request error:[{0}] Raw response:[{1}]", new object[]
				{
					client.error,
					client.downloadHandler.text
				}));
			}
			client.Dispose();
			internalCallback(jsonInterface, userCallback);
			yield break;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000054E1 File Offset: 0x000036E1
		public NetworkMatch()
		{
		}

		// Token: 0x0400008E RID: 142
		private Uri m_BaseUri = new Uri("https://mm.unet.unity3d.com");

		// Token: 0x0200001D RID: 29
		// (Invoke) Token: 0x06000180 RID: 384
		public delegate void BasicResponseDelegate(bool success, string extendedInfo);

		// Token: 0x0200001E RID: 30
		// (Invoke) Token: 0x06000184 RID: 388
		public delegate void DataResponseDelegate<T>(bool success, string extendedInfo, T responseData);

		// Token: 0x0200001F RID: 31
		// (Invoke) Token: 0x06000188 RID: 392
		private delegate void InternalResponseDelegate<T, U>(T response, U userCallback);

		// Token: 0x02000020 RID: 32
		[CompilerGenerated]
		private sealed class <ProcessMatchResponse>d__26<JSONRESPONSE, USERRESPONSEDELEGATETYPE> : IEnumerator<object>, IEnumerator, IDisposable where JSONRESPONSE : Response, new()
		{
			// Token: 0x0600018B RID: 395 RVA: 0x000054FA File Offset: 0x000036FA
			[DebuggerHidden]
			public <ProcessMatchResponse>d__26(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600018C RID: 396 RVA: 0x0000550A File Offset: 0x0000370A
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600018D RID: 397 RVA: 0x0000550C File Offset: 0x0000370C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = client.SendWebRequest();
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				jsonInterface = Activator.CreateInstance<JSONRESPONSE>();
				bool flag = client.result == UnityWebRequest.Result.Success;
				if (flag)
				{
					try
					{
						JsonUtility.FromJsonOverwrite(client.downloadHandler.text, jsonInterface);
					}
					catch (ArgumentException ex)
					{
						exception = ex;
						jsonInterface.SetFailure(UnityString.Format("ArgumentException:[{0}] ", new object[]
						{
							exception.ToString()
						}));
					}
				}
				else
				{
					jsonInterface.SetFailure(UnityString.Format("Request error:[{0}] Raw response:[{1}]", new object[]
					{
						client.error,
						client.downloadHandler.text
					}));
				}
				client.Dispose();
				internalCallback(jsonInterface, userCallback);
				return false;
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x0600018E RID: 398 RVA: 0x00005654 File Offset: 0x00003854
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600018F RID: 399 RVA: 0x0000565C File Offset: 0x0000385C
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000190 RID: 400 RVA: 0x00005654 File Offset: 0x00003854
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400008F RID: 143
			private int <>1__state;

			// Token: 0x04000090 RID: 144
			private object <>2__current;

			// Token: 0x04000091 RID: 145
			public UnityWebRequest client;

			// Token: 0x04000092 RID: 146
			public NetworkMatch.InternalResponseDelegate<JSONRESPONSE, USERRESPONSEDELEGATETYPE> internalCallback;

			// Token: 0x04000093 RID: 147
			public USERRESPONSEDELEGATETYPE userCallback;

			// Token: 0x04000094 RID: 148
			public NetworkMatch <>4__this;

			// Token: 0x04000095 RID: 149
			private JSONRESPONSE <jsonInterface>5__1;

			// Token: 0x04000096 RID: 150
			private ArgumentException <exception>5__2;
		}
	}
}
