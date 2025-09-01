using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Networking;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[Obsolete("Use UnityWebRequest, a fully featured replacement which is more efficient and has additional features")]
	public class WWW : CustomYieldInstruction, IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static string EscapeURL(string s)
		{
			return WWW.EscapeURL(s, Encoding.UTF8);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002070 File Offset: 0x00000270
		public static string EscapeURL(string s, Encoding e)
		{
			return UnityWebRequest.EscapeURL(s, e);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000208C File Offset: 0x0000028C
		public static string UnEscapeURL(string s)
		{
			return WWW.UnEscapeURL(s, Encoding.UTF8);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020AC File Offset: 0x000002AC
		public static string UnEscapeURL(string s, Encoding e)
		{
			return UnityWebRequest.UnEscapeURL(s, e);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C8 File Offset: 0x000002C8
		public static WWW LoadFromCacheOrDownload(string url, int version)
		{
			return WWW.LoadFromCacheOrDownload(url, version, 0U);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E4 File Offset: 0x000002E4
		public static WWW LoadFromCacheOrDownload(string url, int version, uint crc)
		{
			Hash128 hash = new Hash128(0U, 0U, 0U, (uint)version);
			return WWW.LoadFromCacheOrDownload(url, hash, crc);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000210C File Offset: 0x0000030C
		public static WWW LoadFromCacheOrDownload(string url, Hash128 hash)
		{
			return WWW.LoadFromCacheOrDownload(url, hash, 0U);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002128 File Offset: 0x00000328
		public static WWW LoadFromCacheOrDownload(string url, Hash128 hash, uint crc)
		{
			return new WWW(url, "", hash, crc);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002148 File Offset: 0x00000348
		public static WWW LoadFromCacheOrDownload(string url, CachedAssetBundle cachedBundle, uint crc = 0U)
		{
			return new WWW(url, cachedBundle.name, cachedBundle.hash, crc);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216F File Offset: 0x0000036F
		public WWW(string url)
		{
			this._uwr = UnityWebRequest.Get(url);
			this._uwr.SendWebRequest();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002191 File Offset: 0x00000391
		public WWW(string url, WWWForm form)
		{
			this._uwr = UnityWebRequest.Post(url, form);
			this._uwr.chunkedTransfer = false;
			this._uwr.SendWebRequest();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021C4 File Offset: 0x000003C4
		public WWW(string url, byte[] postData)
		{
			this._uwr = new UnityWebRequest(url, "POST");
			this._uwr.chunkedTransfer = false;
			UploadHandler uploadHandler = new UploadHandlerRaw(postData);
			uploadHandler.contentType = "application/x-www-form-urlencoded";
			this._uwr.uploadHandler = uploadHandler;
			this._uwr.downloadHandler = new DownloadHandlerBuffer();
			this._uwr.SendWebRequest();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002234 File Offset: 0x00000434
		[Obsolete("This overload is deprecated. Use UnityEngine.WWW.WWW(string, byte[], System.Collections.Generic.Dictionary<string, string>) instead.")]
		public WWW(string url, byte[] postData, Hashtable headers)
		{
			string method = (postData == null) ? "GET" : "POST";
			this._uwr = new UnityWebRequest(url, method);
			this._uwr.chunkedTransfer = false;
			UploadHandler uploadHandler = new UploadHandlerRaw(postData);
			uploadHandler.contentType = "application/x-www-form-urlencoded";
			this._uwr.uploadHandler = uploadHandler;
			this._uwr.downloadHandler = new DownloadHandlerBuffer();
			foreach (object obj in headers.Keys)
			{
				this._uwr.SetRequestHeader((string)obj, (string)headers[obj]);
			}
			this._uwr.SendWebRequest();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002314 File Offset: 0x00000514
		public WWW(string url, byte[] postData, Dictionary<string, string> headers)
		{
			string method = (postData == null) ? "GET" : "POST";
			this._uwr = new UnityWebRequest(url, method);
			this._uwr.chunkedTransfer = false;
			UploadHandler uploadHandler = new UploadHandlerRaw(postData);
			uploadHandler.contentType = "application/x-www-form-urlencoded";
			this._uwr.uploadHandler = uploadHandler;
			this._uwr.downloadHandler = new DownloadHandlerBuffer();
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				this._uwr.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
			}
			this._uwr.SendWebRequest();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023E8 File Offset: 0x000005E8
		internal WWW(string url, string name, Hash128 hash, uint crc)
		{
			this._uwr = UnityWebRequestAssetBundle.GetAssetBundle(url, new CachedAssetBundle(name, hash), crc);
			this._uwr.SendWebRequest();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002414 File Offset: 0x00000614
		public AssetBundle assetBundle
		{
			get
			{
				bool flag = this._assetBundle == null;
				if (flag)
				{
					bool flag2 = !this.WaitUntilDoneIfPossible();
					if (flag2)
					{
						return null;
					}
					bool flag3 = this._uwr.result == UnityWebRequest.Result.ConnectionError;
					if (flag3)
					{
						return null;
					}
					DownloadHandlerAssetBundle downloadHandlerAssetBundle = this._uwr.downloadHandler as DownloadHandlerAssetBundle;
					bool flag4 = downloadHandlerAssetBundle != null;
					if (flag4)
					{
						this._assetBundle = downloadHandlerAssetBundle.assetBundle;
					}
					else
					{
						byte[] bytes = this.bytes;
						bool flag5 = bytes == null;
						if (flag5)
						{
							return null;
						}
						this._assetBundle = AssetBundle.LoadFromMemory(bytes);
					}
				}
				return this._assetBundle;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000024B8 File Offset: 0x000006B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Obsolete msg (UnityUpgradable) -> * UnityEngine.WWW.GetAudioClip()", true)]
		public Object audioClip
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000024CC File Offset: 0x000006CC
		public byte[] bytes
		{
			get
			{
				bool flag = !this.WaitUntilDoneIfPossible();
				byte[] result;
				if (flag)
				{
					result = new byte[0];
				}
				else
				{
					bool flag2 = this._uwr.result == UnityWebRequest.Result.ConnectionError;
					if (flag2)
					{
						result = new byte[0];
					}
					else
					{
						DownloadHandler downloadHandler = this._uwr.downloadHandler;
						bool flag3 = downloadHandler == null;
						if (flag3)
						{
							result = new byte[0];
						}
						else
						{
							result = downloadHandler.data;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002534 File Offset: 0x00000734
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Obsolete msg (UnityUpgradable) -> * UnityEngine.WWW.GetMovieTexture()", true)]
		public Object movie
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002548 File Offset: 0x00000748
		[Obsolete("WWW.size is obsolete. Please use WWW.bytesDownloaded instead")]
		public int size
		{
			get
			{
				return this.bytesDownloaded;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002560 File Offset: 0x00000760
		public int bytesDownloaded
		{
			get
			{
				return (int)this._uwr.downloadedBytes;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002580 File Offset: 0x00000780
		public string error
		{
			get
			{
				bool flag = !this._uwr.isDone;
				string result;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = this._uwr.result == UnityWebRequest.Result.ConnectionError;
					if (flag2)
					{
						result = this._uwr.error;
					}
					else
					{
						bool flag3 = this._uwr.responseCode >= 400L;
						if (flag3)
						{
							string httpstatusString = UnityWebRequest.GetHTTPStatusString(this._uwr.responseCode);
							result = string.Format("{0} {1}", this._uwr.responseCode, httpstatusString);
						}
						else
						{
							result = null;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002618 File Offset: 0x00000818
		public bool isDone
		{
			get
			{
				return this._uwr.isDone;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002638 File Offset: 0x00000838
		public float progress
		{
			get
			{
				float num = this._uwr.downloadProgress;
				bool flag = num < 0f;
				if (flag)
				{
					num = 0f;
				}
				return num;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000266C File Offset: 0x0000086C
		public Dictionary<string, string> responseHeaders
		{
			get
			{
				bool flag = !this.isDone;
				Dictionary<string, string> result;
				if (flag)
				{
					result = new Dictionary<string, string>();
				}
				else
				{
					bool flag2 = this._responseHeaders == null;
					if (flag2)
					{
						this._responseHeaders = this._uwr.GetResponseHeaders();
						bool flag3 = this._responseHeaders != null;
						if (flag3)
						{
							string httpstatusString = UnityWebRequest.GetHTTPStatusString(this._uwr.responseCode);
							this._responseHeaders["STATUS"] = string.Format("HTTP/1.1 {0} {1}", this._uwr.responseCode, httpstatusString);
						}
						else
						{
							this._responseHeaders = new Dictionary<string, string>();
						}
					}
					result = this._responseHeaders;
				}
				return result;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002718 File Offset: 0x00000918
		[Obsolete("Please use WWW.text instead. (UnityUpgradable) -> text", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string data
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002730 File Offset: 0x00000930
		public string text
		{
			get
			{
				bool flag = !this.WaitUntilDoneIfPossible();
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					bool flag2 = this._uwr.result == UnityWebRequest.Result.ConnectionError;
					if (flag2)
					{
						result = "";
					}
					else
					{
						DownloadHandler downloadHandler = this._uwr.downloadHandler;
						bool flag3 = downloadHandler == null;
						if (flag3)
						{
							result = "";
						}
						else
						{
							result = downloadHandler.text;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002798 File Offset: 0x00000998
		private Texture2D CreateTextureFromDownloadedData(bool markNonReadable)
		{
			bool flag = !this.WaitUntilDoneIfPossible();
			Texture2D result;
			if (flag)
			{
				result = new Texture2D(2, 2);
			}
			else
			{
				bool flag2 = this._uwr.result == UnityWebRequest.Result.ConnectionError;
				if (flag2)
				{
					result = null;
				}
				else
				{
					DownloadHandler downloadHandler = this._uwr.downloadHandler;
					bool flag3 = downloadHandler == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						Texture2D texture2D = new Texture2D(2, 2);
						texture2D.LoadImage(downloadHandler.data, markNonReadable);
						result = texture2D;
					}
				}
			}
			return result;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000280C File Offset: 0x00000A0C
		public Texture2D texture
		{
			get
			{
				return this.CreateTextureFromDownloadedData(false);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002828 File Offset: 0x00000A28
		public Texture2D textureNonReadable
		{
			get
			{
				return this.CreateTextureFromDownloadedData(true);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002844 File Offset: 0x00000A44
		public void LoadImageIntoTexture(Texture2D texture)
		{
			bool flag = !this.WaitUntilDoneIfPossible();
			if (!flag)
			{
				bool flag2 = this._uwr.result == UnityWebRequest.Result.ConnectionError;
				if (flag2)
				{
					Debug.LogError("Cannot load image: download failed");
				}
				else
				{
					DownloadHandler downloadHandler = this._uwr.downloadHandler;
					bool flag3 = downloadHandler == null;
					if (flag3)
					{
						Debug.LogError("Cannot load image: internal error");
					}
					else
					{
						texture.LoadImage(downloadHandler.data, false);
					}
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000028B1 File Offset: 0x00000AB1
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000028B9 File Offset: 0x00000AB9
		public ThreadPriority threadPriority
		{
			[CompilerGenerated]
			get
			{
				return this.<threadPriority>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<threadPriority>k__BackingField = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000028C4 File Offset: 0x00000AC4
		public float uploadProgress
		{
			get
			{
				float num = this._uwr.uploadProgress;
				bool flag = num < 0f;
				if (flag)
				{
					num = 0f;
				}
				return num;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000028F8 File Offset: 0x00000AF8
		public string url
		{
			get
			{
				return this._uwr.url;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002918 File Offset: 0x00000B18
		public override bool keepWaiting
		{
			get
			{
				return this._uwr != null && !this._uwr.isDone;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002944 File Offset: 0x00000B44
		public void Dispose()
		{
			bool flag = this._uwr != null;
			if (flag)
			{
				this._uwr.Dispose();
				this._uwr = null;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002974 File Offset: 0x00000B74
		internal Object GetAudioClipInternal(bool threeD, bool stream, bool compressed, AudioType audioType)
		{
			return WebRequestWWW.InternalCreateAudioClipUsingDH(this._uwr.downloadHandler, this._uwr.url, stream, compressed, audioType);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000029A8 File Offset: 0x00000BA8
		public AudioClip GetAudioClip()
		{
			return this.GetAudioClip(true, false, AudioType.UNKNOWN);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029C4 File Offset: 0x00000BC4
		public AudioClip GetAudioClip(bool threeD)
		{
			return this.GetAudioClip(threeD, false, AudioType.UNKNOWN);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000029E0 File Offset: 0x00000BE0
		public AudioClip GetAudioClip(bool threeD, bool stream)
		{
			return this.GetAudioClip(threeD, stream, AudioType.UNKNOWN);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000029FC File Offset: 0x00000BFC
		public AudioClip GetAudioClip(bool threeD, bool stream, AudioType audioType)
		{
			return (AudioClip)this.GetAudioClipInternal(threeD, stream, false, audioType);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A20 File Offset: 0x00000C20
		public AudioClip GetAudioClipCompressed()
		{
			return this.GetAudioClipCompressed(false, AudioType.UNKNOWN);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A3C File Offset: 0x00000C3C
		public AudioClip GetAudioClipCompressed(bool threeD)
		{
			return this.GetAudioClipCompressed(threeD, AudioType.UNKNOWN);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A58 File Offset: 0x00000C58
		public AudioClip GetAudioClipCompressed(bool threeD, AudioType audioType)
		{
			return (AudioClip)this.GetAudioClipInternal(threeD, false, true, audioType);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A79 File Offset: 0x00000C79
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("MovieTexture is deprecated. Use VideoPlayer instead.", false)]
		public MovieTexture GetMovieTexture()
		{
			throw new Exception("MovieTexture has been removed from Unity. Use VideoPlayer instead.");
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A88 File Offset: 0x00000C88
		private bool WaitUntilDoneIfPossible()
		{
			bool isDone = this._uwr.isDone;
			bool result;
			if (isDone)
			{
				result = true;
			}
			else
			{
				bool flag = this.url.StartsWith("file://", StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					while (!this._uwr.isDone)
					{
					}
					result = true;
				}
				else
				{
					Debug.LogError("You are trying to load data from a www stream which has not completed the download yet.\nYou need to yield the download or wait until isDone returns true.");
					result = false;
				}
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ThreadPriority <threadPriority>k__BackingField;

		// Token: 0x04000002 RID: 2
		private UnityWebRequest _uwr;

		// Token: 0x04000003 RID: 3
		private AssetBundle _assetBundle;

		// Token: 0x04000004 RID: 4
		private Dictionary<string, string> _responseHeaders;
	}
}
