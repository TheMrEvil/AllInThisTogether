using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine.Networking
{
	// Token: 0x02000009 RID: 9
	[NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequest.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class UnityWebRequest : IDisposable
	{
		// Token: 0x0600004B RID: 75
		[NativeConditional("ENABLE_UNITYWEBREQUEST")]
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetWebErrorString(UnityWebRequest.UnityWebRequestError err);

		// Token: 0x0600004C RID: 76
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetHTTPStatusString(long responseCode);

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000035F2 File Offset: 0x000017F2
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000035FA File Offset: 0x000017FA
		public bool disposeCertificateHandlerOnDispose
		{
			[CompilerGenerated]
			get
			{
				return this.<disposeCertificateHandlerOnDispose>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<disposeCertificateHandlerOnDispose>k__BackingField = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003603 File Offset: 0x00001803
		// (set) Token: 0x06000050 RID: 80 RVA: 0x0000360B File Offset: 0x0000180B
		public bool disposeDownloadHandlerOnDispose
		{
			[CompilerGenerated]
			get
			{
				return this.<disposeDownloadHandlerOnDispose>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<disposeDownloadHandlerOnDispose>k__BackingField = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003614 File Offset: 0x00001814
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000361C File Offset: 0x0000181C
		public bool disposeUploadHandlerOnDispose
		{
			[CompilerGenerated]
			get
			{
				return this.<disposeUploadHandlerOnDispose>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<disposeUploadHandlerOnDispose>k__BackingField = value;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003625 File Offset: 0x00001825
		public static void ClearCookieCache()
		{
			UnityWebRequest.ClearCookieCache(null, null);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003630 File Offset: 0x00001830
		public static void ClearCookieCache(Uri uri)
		{
			bool flag = uri == null;
			if (flag)
			{
				UnityWebRequest.ClearCookieCache(null, null);
			}
			else
			{
				string host = uri.Host;
				string text = uri.AbsolutePath;
				bool flag2 = text == "/";
				if (flag2)
				{
					text = null;
				}
				UnityWebRequest.ClearCookieCache(host, text);
			}
		}

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearCookieCache(string domain, string path);

		// Token: 0x06000056 RID: 86
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Create();

		// Token: 0x06000057 RID: 87
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Release();

		// Token: 0x06000058 RID: 88 RVA: 0x0000367C File Offset: 0x0000187C
		internal void InternalDestroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Abort();
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000036B9 File Offset: 0x000018B9
		private void InternalSetDefaults()
		{
			this.disposeDownloadHandlerOnDispose = true;
			this.disposeUploadHandlerOnDispose = true;
			this.disposeCertificateHandlerOnDispose = true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000036D4 File Offset: 0x000018D4
		public UnityWebRequest()
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000036F0 File Offset: 0x000018F0
		public UnityWebRequest(string url)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003714 File Offset: 0x00001914
		public UnityWebRequest(Uri uri)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003738 File Offset: 0x00001938
		public UnityWebRequest(string url, string method)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003764 File Offset: 0x00001964
		public UnityWebRequest(Uri uri, string method)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
			this.method = method;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003790 File Offset: 0x00001990
		public UnityWebRequest(string url, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000037CD File Offset: 0x000019CD
		public UnityWebRequest(Uri uri, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000380C File Offset: 0x00001A0C
		~UnityWebRequest()
		{
			this.DisposeHandlers();
			this.InternalDestroy();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003844 File Offset: 0x00001A44
		public void Dispose()
		{
			this.DisposeHandlers();
			this.InternalDestroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000385C File Offset: 0x00001A5C
		private void DisposeHandlers()
		{
			bool disposeDownloadHandlerOnDispose = this.disposeDownloadHandlerOnDispose;
			if (disposeDownloadHandlerOnDispose)
			{
				DownloadHandler downloadHandler = this.downloadHandler;
				bool flag = downloadHandler != null;
				if (flag)
				{
					downloadHandler.Dispose();
				}
			}
			bool disposeUploadHandlerOnDispose = this.disposeUploadHandlerOnDispose;
			if (disposeUploadHandlerOnDispose)
			{
				UploadHandler uploadHandler = this.uploadHandler;
				bool flag2 = uploadHandler != null;
				if (flag2)
				{
					uploadHandler.Dispose();
				}
			}
			bool disposeCertificateHandlerOnDispose = this.disposeCertificateHandlerOnDispose;
			if (disposeCertificateHandlerOnDispose)
			{
				CertificateHandler certificateHandler = this.certificateHandler;
				bool flag3 = certificateHandler != null;
				if (flag3)
				{
					certificateHandler.Dispose();
				}
			}
		}

		// Token: 0x06000064 RID: 100
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern UnityWebRequestAsyncOperation BeginWebRequest();

		// Token: 0x06000065 RID: 101 RVA: 0x000038E4 File Offset: 0x00001AE4
		[Obsolete("Use SendWebRequest.  It returns a UnityWebRequestAsyncOperation which contains a reference to the WebRequest object.", false)]
		public AsyncOperation Send()
		{
			return this.SendWebRequest();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000038FC File Offset: 0x00001AFC
		public UnityWebRequestAsyncOperation SendWebRequest()
		{
			UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = this.BeginWebRequest();
			bool flag = unityWebRequestAsyncOperation != null;
			if (flag)
			{
				unityWebRequestAsyncOperation.webRequest = this;
			}
			return unityWebRequestAsyncOperation;
		}

		// Token: 0x06000067 RID: 103
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Abort();

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetMethod(UnityWebRequest.UnityWebRequestMethod methodType);

		// Token: 0x06000069 RID: 105 RVA: 0x00003928 File Offset: 0x00001B28
		internal void InternalSetMethod(UnityWebRequest.UnityWebRequestMethod methodType)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetMethod(methodType);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x0600006A RID: 106
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetCustomMethod(string customMethodName);

		// Token: 0x0600006B RID: 107 RVA: 0x0000396C File Offset: 0x00001B6C
		internal void InternalSetCustomMethod(string customMethodName)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetCustomMethod(customMethodName);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x0600006C RID: 108
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern UnityWebRequest.UnityWebRequestMethod GetMethod();

		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string GetCustomMethod();

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000039B0 File Offset: 0x00001BB0
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003A0C File Offset: 0x00001C0C
		public string method
		{
			get
			{
				string result;
				switch (this.GetMethod())
				{
				case UnityWebRequest.UnityWebRequestMethod.Get:
					result = "GET";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Post:
					result = "POST";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Put:
					result = "PUT";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Head:
					result = "HEAD";
					break;
				default:
					result = this.GetCustomMethod();
					break;
				}
				return result;
			}
			set
			{
				bool flag = string.IsNullOrEmpty(value);
				if (flag)
				{
					throw new ArgumentException("Cannot set a UnityWebRequest's method to an empty or null string");
				}
				string text = value.ToUpper();
				string a = text;
				if (!(a == "GET"))
				{
					if (!(a == "POST"))
					{
						if (!(a == "PUT"))
						{
							if (!(a == "HEAD"))
							{
								this.InternalSetCustomMethod(value.ToUpper());
							}
							else
							{
								this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Head);
							}
						}
						else
						{
							this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Put);
						}
					}
					else
					{
						this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Post);
					}
				}
				else
				{
					this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Get);
				}
			}
		}

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError GetError();

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003AA8 File Offset: 0x00001CA8
		public string error
		{
			get
			{
				UnityWebRequest.Result result = this.result;
				UnityWebRequest.Result result2 = result;
				string result3;
				if (result2 > UnityWebRequest.Result.Success)
				{
					if (result2 != UnityWebRequest.Result.ProtocolError)
					{
						result3 = UnityWebRequest.GetWebErrorString(this.GetError());
					}
					else
					{
						result3 = string.Format("HTTP/1.1 {0} {1}", this.responseCode, UnityWebRequest.GetHTTPStatusString(this.responseCode));
					}
				}
				else
				{
					result3 = null;
				}
				return result3;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000072 RID: 114
		// (set) Token: 0x06000073 RID: 115
		private extern bool use100Continue { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003B04 File Offset: 0x00001D04
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003B1C File Offset: 0x00001D1C
		public bool useHttpContinue
		{
			get
			{
				return this.use100Continue;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent and its 100-Continue setting cannot be altered");
				}
				this.use100Continue = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003B4C File Offset: 0x00001D4C
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003B64 File Offset: 0x00001D64
		public string url
		{
			get
			{
				return this.GetUrl();
			}
			set
			{
				string localUrl = "http://localhost/";
				this.InternalSetUrl(WebRequestUtils.MakeInitialUrl(value, localUrl));
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003B88 File Offset: 0x00001D88
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public Uri uri
		{
			get
			{
				return new Uri(this.GetUrl());
			}
			set
			{
				bool flag = !value.IsAbsoluteUri;
				if (flag)
				{
					throw new ArgumentException("URI must be absolute");
				}
				this.InternalSetUrl(WebRequestUtils.MakeUriString(value, value.OriginalString, false));
				this.m_Uri = value;
			}
		}

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetUrl();

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetUrl(string url);

		// Token: 0x0600007C RID: 124 RVA: 0x00003BEC File Offset: 0x00001DEC
		private void InternalSetUrl(string url)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its URL cannot be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetUrl(url);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007D RID: 125
		public extern long responseCode { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600007E RID: 126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetUploadProgress();

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsExecuting();

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003C30 File Offset: 0x00001E30
		public float uploadProgress
		{
			get
			{
				bool flag = !this.IsExecuting() && !this.isDone;
				float result;
				if (flag)
				{
					result = -1f;
				}
				else
				{
					result = this.GetUploadProgress();
				}
				return result;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000081 RID: 129
		public extern bool isModifiable { [NativeMethod("IsModifiable")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003C68 File Offset: 0x00001E68
		public bool isDone
		{
			get
			{
				return this.result > UnityWebRequest.Result.InProgress;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003C84 File Offset: 0x00001E84
		[Obsolete("UnityWebRequest.isNetworkError is deprecated. Use (UnityWebRequest.result == UnityWebRequest.Result.ConnectionError) instead.", false)]
		public bool isNetworkError
		{
			get
			{
				return this.result == UnityWebRequest.Result.ConnectionError;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003CA0 File Offset: 0x00001EA0
		[Obsolete("UnityWebRequest.isHttpError is deprecated. Use (UnityWebRequest.result == UnityWebRequest.Result.ProtocolError) instead.", false)]
		public bool isHttpError
		{
			get
			{
				return this.result == UnityWebRequest.Result.ProtocolError;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000085 RID: 133
		public extern UnityWebRequest.Result result { [NativeMethod("GetResult")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000086 RID: 134
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetDownloadProgress();

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003CBC File Offset: 0x00001EBC
		public float downloadProgress
		{
			get
			{
				bool flag = !this.IsExecuting() && !this.isDone;
				float result;
				if (flag)
				{
					result = -1f;
				}
				else
				{
					result = this.GetDownloadProgress();
				}
				return result;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000088 RID: 136
		public extern ulong uploadedBytes { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000089 RID: 137
		public extern ulong downloadedBytes { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetRedirectLimit();

		// Token: 0x0600008B RID: 139
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetRedirectLimitFromScripting(int limit);

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003CF4 File Offset: 0x00001EF4
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003D0C File Offset: 0x00001F0C
		public int redirectLimit
		{
			get
			{
				return this.GetRedirectLimit();
			}
			set
			{
				this.SetRedirectLimitFromScripting(value);
			}
		}

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetChunked();

		// Token: 0x0600008F RID: 143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetChunked(bool chunked);

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003D18 File Offset: 0x00001F18
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003D30 File Offset: 0x00001F30
		[Obsolete("HTTP/2 and many HTTP/1.1 servers don't support this; we recommend leaving it set to false (default).", false)]
		public bool chunkedTransfer
		{
			get
			{
				return this.GetChunked();
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent and its chunked transfer encoding setting cannot be altered");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetChunked(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		// Token: 0x06000092 RID: 146
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetRequestHeader(string name);

		// Token: 0x06000093 RID: 147
		[NativeMethod("SetRequestHeader")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern UnityWebRequest.UnityWebRequestError InternalSetRequestHeader(string name, string value);

		// Token: 0x06000094 RID: 148 RVA: 0x00003D74 File Offset: 0x00001F74
		public void SetRequestHeader(string name, string value)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (flag)
			{
				throw new ArgumentException("Cannot set a Request Header with a null or empty name");
			}
			bool flag2 = value == null;
			if (flag2)
			{
				throw new ArgumentException("Cannot set a Request header with a null");
			}
			bool flag3 = !this.isModifiable;
			if (flag3)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request headers cannot be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.InternalSetRequestHeader(name, value);
			bool flag4 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag4)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x06000095 RID: 149
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetResponseHeader(string name);

		// Token: 0x06000096 RID: 150
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string[] GetResponseHeaderKeys();

		// Token: 0x06000097 RID: 151 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public Dictionary<string, string> GetResponseHeaders()
		{
			string[] responseHeaderKeys = this.GetResponseHeaderKeys();
			bool flag = responseHeaderKeys == null || responseHeaderKeys.Length == 0;
			Dictionary<string, string> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(responseHeaderKeys.Length, StringComparer.OrdinalIgnoreCase);
				for (int i = 0; i < responseHeaderKeys.Length; i++)
				{
					string responseHeader = this.GetResponseHeader(responseHeaderKeys[i]);
					dictionary.Add(responseHeaderKeys[i], responseHeader);
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetUploadHandler(UploadHandler uh);

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003E54 File Offset: 0x00002054
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003E6C File Offset: 0x0000206C
		public UploadHandler uploadHandler
		{
			get
			{
				return this.m_UploadHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the upload handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetUploadHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_UploadHandler = value;
			}
		}

		// Token: 0x0600009B RID: 155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetDownloadHandler(DownloadHandler dh);

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003EB8 File Offset: 0x000020B8
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003ED0 File Offset: 0x000020D0
		public DownloadHandler downloadHandler
		{
			get
			{
				return this.m_DownloadHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the download handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetDownloadHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_DownloadHandler = value;
			}
		}

		// Token: 0x0600009E RID: 158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetCertificateHandler(CertificateHandler ch);

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003F1C File Offset: 0x0000211C
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003F34 File Offset: 0x00002134
		public CertificateHandler certificateHandler
		{
			get
			{
				return this.m_CertificateHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the certificate handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetCertificateHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_CertificateHandler = value;
			}
		}

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetTimeoutMsec();

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetTimeoutMsec(int timeout);

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003F80 File Offset: 0x00002180
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003FA0 File Offset: 0x000021A0
		public int timeout
		{
			get
			{
				return this.GetTimeoutMsec() / 1000;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the timeout");
				}
				value = Math.Max(value, 0);
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetTimeoutMsec(value * 1000);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetSuppressErrorsToConsole();

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequest.UnityWebRequestError SetSuppressErrorsToConsole(bool suppress);

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003FF4 File Offset: 0x000021F4
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000400C File Offset: 0x0000220C
		internal bool suppressErrorsToConsole
		{
			get
			{
				return this.GetSuppressErrorsToConsole();
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the timeout");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetSuppressErrorsToConsole(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004050 File Offset: 0x00002250
		public static UnityWebRequest Get(string uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004078 File Offset: 0x00002278
		public static UnityWebRequest Get(Uri uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000040A0 File Offset: 0x000022A0
		public static UnityWebRequest Delete(string uri)
		{
			return new UnityWebRequest(uri, "DELETE");
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000040C0 File Offset: 0x000022C0
		public static UnityWebRequest Delete(Uri uri)
		{
			return new UnityWebRequest(uri, "DELETE");
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000040E0 File Offset: 0x000022E0
		public static UnityWebRequest Head(string uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004100 File Offset: 0x00002300
		public static UnityWebRequest Head(Uri uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000411F File Offset: 0x0000231F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		public static UnityWebRequest GetTexture(string uri)
		{
			throw new NotSupportedException("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000411F File Offset: 0x0000231F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		public static UnityWebRequest GetTexture(string uri, bool nonReadable)
		{
			throw new NotSupportedException("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000412C File Offset: 0x0000232C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAudioClip is obsolete. Use UnityWebRequestMultimedia.GetAudioClip instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestMultimedia.GetAudioClip(*)", true)]
		public static UnityWebRequest GetAudioClip(string uri, AudioType audioType)
		{
			return null;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004140 File Offset: 0x00002340
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri)
		{
			return null;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004154 File Offset: 0x00002354
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, uint crc)
		{
			return null;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004168 File Offset: 0x00002368
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static UnityWebRequest GetAssetBundle(string uri, uint version, uint crc)
		{
			return null;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000417C File Offset: 0x0000237C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, Hash128 hash, uint crc)
		{
			return null;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004190 File Offset: 0x00002390
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, CachedAssetBundle cachedAssetBundle, uint crc)
		{
			return null;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000041A4 File Offset: 0x000023A4
		public static UnityWebRequest Put(string uri, byte[] bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000041D0 File Offset: 0x000023D0
		public static UnityWebRequest Put(Uri uri, byte[] bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000041FC File Offset: 0x000023FC
		public static UnityWebRequest Put(string uri, string bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyData)));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004230 File Offset: 0x00002430
		public static UnityWebRequest Put(Uri uri, string bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyData)));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004264 File Offset: 0x00002464
		public static UnityWebRequest Post(string uri, string postData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, postData);
			return unityWebRequest;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000428C File Offset: 0x0000248C
		public static UnityWebRequest Post(Uri uri, string postData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, postData);
			return unityWebRequest;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000042B4 File Offset: 0x000024B4
		private static void SetupPost(UnityWebRequest request, string postData)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			bool flag = string.IsNullOrEmpty(postData);
			if (!flag)
			{
				string s = WWWTranscoder.DataEncode(postData, Encoding.UTF8);
				byte[] bytes = Encoding.UTF8.GetBytes(s);
				request.uploadHandler = new UploadHandlerRaw(bytes);
				request.uploadHandler.contentType = "application/x-www-form-urlencoded";
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004314 File Offset: 0x00002514
		public static UnityWebRequest Post(string uri, WWWForm formData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formData);
			return unityWebRequest;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000433C File Offset: 0x0000253C
		public static UnityWebRequest Post(Uri uri, WWWForm formData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formData);
			return unityWebRequest;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004364 File Offset: 0x00002564
		private static void SetupPost(UnityWebRequest request, WWWForm formData)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			bool flag = formData == null;
			if (!flag)
			{
				byte[] array = formData.data;
				bool flag2 = array.Length == 0;
				if (flag2)
				{
					array = null;
				}
				bool flag3 = array != null;
				if (flag3)
				{
					request.uploadHandler = new UploadHandlerRaw(array);
				}
				Dictionary<string, string> headers = formData.headers;
				foreach (KeyValuePair<string, string> keyValuePair in headers)
				{
					request.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004410 File Offset: 0x00002610
		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] boundary = UnityWebRequest.GenerateBoundary();
			return UnityWebRequest.Post(uri, multipartFormSections, boundary);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004430 File Offset: 0x00002630
		public static UnityWebRequest Post(Uri uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] boundary = UnityWebRequest.GenerateBoundary();
			return UnityWebRequest.Post(uri, multipartFormSections, boundary);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004450 File Offset: 0x00002650
		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, multipartFormSections, boundary);
			return unityWebRequest;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004478 File Offset: 0x00002678
		public static UnityWebRequest Post(Uri uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, multipartFormSections, boundary);
			return unityWebRequest;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000044A0 File Offset: 0x000026A0
		private static void SetupPost(UnityWebRequest request, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			byte[] array = null;
			bool flag = multipartFormSections != null && multipartFormSections.Count != 0;
			if (flag)
			{
				array = UnityWebRequest.SerializeFormSections(multipartFormSections, boundary);
			}
			bool flag2 = array == null;
			if (!flag2)
			{
				request.uploadHandler = new UploadHandlerRaw(array)
				{
					contentType = "multipart/form-data; boundary=" + Encoding.UTF8.GetString(boundary, 0, boundary.Length)
				};
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004510 File Offset: 0x00002710
		public static UnityWebRequest Post(string uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formFields);
			return unityWebRequest;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004538 File Offset: 0x00002738
		public static UnityWebRequest Post(Uri uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formFields);
			return unityWebRequest;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004560 File Offset: 0x00002760
		private static void SetupPost(UnityWebRequest request, Dictionary<string, string> formFields)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			byte[] array = null;
			bool flag = formFields != null && formFields.Count != 0;
			if (flag)
			{
				array = UnityWebRequest.SerializeSimpleForm(formFields);
			}
			bool flag2 = array == null;
			if (!flag2)
			{
				request.uploadHandler = new UploadHandlerRaw(array)
				{
					contentType = "application/x-www-form-urlencoded"
				};
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000045BC File Offset: 0x000027BC
		public static string EscapeURL(string s)
		{
			return UnityWebRequest.EscapeURL(s, Encoding.UTF8);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000045DC File Offset: 0x000027DC
		public static string EscapeURL(string s, Encoding e)
		{
			bool flag = s == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = s == "";
				if (flag2)
				{
					result = "";
				}
				else
				{
					bool flag3 = e == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						byte[] bytes = e.GetBytes(s);
						byte[] bytes2 = WWWTranscoder.URLEncode(bytes);
						result = e.GetString(bytes2);
					}
				}
			}
			return result;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004638 File Offset: 0x00002838
		public static string UnEscapeURL(string s)
		{
			return UnityWebRequest.UnEscapeURL(s, Encoding.UTF8);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004658 File Offset: 0x00002858
		public static string UnEscapeURL(string s, Encoding e)
		{
			bool flag = s == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = s.IndexOf('%') == -1 && s.IndexOf('+') == -1;
				if (flag2)
				{
					result = s;
				}
				else
				{
					byte[] bytes = e.GetBytes(s);
					byte[] bytes2 = WWWTranscoder.URLDecode(bytes);
					result = e.GetString(bytes2);
				}
			}
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000046B0 File Offset: 0x000028B0
		public static byte[] SerializeFormSections(List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			bool flag = multipartFormSections == null || multipartFormSections.Count == 0;
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] bytes = Encoding.UTF8.GetBytes("\r\n");
				byte[] bytes2 = WWWForm.DefaultEncoding.GetBytes("--");
				int num = 0;
				foreach (IMultipartFormSection multipartFormSection in multipartFormSections)
				{
					num += 64 + multipartFormSection.sectionData.Length;
				}
				List<byte> list = new List<byte>(num);
				foreach (IMultipartFormSection multipartFormSection2 in multipartFormSections)
				{
					string str = "form-data";
					string sectionName = multipartFormSection2.sectionName;
					string fileName = multipartFormSection2.fileName;
					string text = "Content-Disposition: " + str;
					bool flag2 = !string.IsNullOrEmpty(sectionName);
					if (flag2)
					{
						text = text + "; name=\"" + sectionName + "\"";
					}
					bool flag3 = !string.IsNullOrEmpty(fileName);
					if (flag3)
					{
						text = text + "; filename=\"" + fileName + "\"";
					}
					text += "\r\n";
					string contentType = multipartFormSection2.contentType;
					bool flag4 = !string.IsNullOrEmpty(contentType);
					if (flag4)
					{
						text = text + "Content-Type: " + contentType + "\r\n";
					}
					list.AddRange(bytes);
					list.AddRange(bytes2);
					list.AddRange(boundary);
					list.AddRange(bytes);
					list.AddRange(Encoding.UTF8.GetBytes(text));
					list.AddRange(bytes);
					list.AddRange(multipartFormSection2.sectionData);
				}
				list.AddRange(bytes);
				list.AddRange(bytes2);
				list.AddRange(boundary);
				list.AddRange(bytes2);
				list.AddRange(bytes);
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000048E0 File Offset: 0x00002AE0
		public static byte[] GenerateBoundary()
		{
			byte[] array = new byte[40];
			for (int i = 0; i < 40; i++)
			{
				int num = Random.Range(48, 110);
				bool flag = num > 57;
				if (flag)
				{
					num += 7;
				}
				bool flag2 = num > 90;
				if (flag2)
				{
					num += 6;
				}
				array[i] = (byte)num;
			}
			return array;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004940 File Offset: 0x00002B40
		public static byte[] SerializeSimpleForm(Dictionary<string, string> formFields)
		{
			string text = "";
			foreach (KeyValuePair<string, string> keyValuePair in formFields)
			{
				bool flag = text.Length > 0;
				if (flag)
				{
					text += "&";
				}
				text = text + WWWTranscoder.DataEncode(keyValuePair.Key) + "=" + WWWTranscoder.DataEncode(keyValuePair.Value);
			}
			return Encoding.UTF8.GetBytes(text);
		}

		// Token: 0x04000021 RID: 33
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000022 RID: 34
		[NonSerialized]
		internal DownloadHandler m_DownloadHandler;

		// Token: 0x04000023 RID: 35
		[NonSerialized]
		internal UploadHandler m_UploadHandler;

		// Token: 0x04000024 RID: 36
		[NonSerialized]
		internal CertificateHandler m_CertificateHandler;

		// Token: 0x04000025 RID: 37
		[NonSerialized]
		internal Uri m_Uri;

		// Token: 0x04000026 RID: 38
		public const string kHttpVerbGET = "GET";

		// Token: 0x04000027 RID: 39
		public const string kHttpVerbHEAD = "HEAD";

		// Token: 0x04000028 RID: 40
		public const string kHttpVerbPOST = "POST";

		// Token: 0x04000029 RID: 41
		public const string kHttpVerbPUT = "PUT";

		// Token: 0x0400002A RID: 42
		public const string kHttpVerbCREATE = "CREATE";

		// Token: 0x0400002B RID: 43
		public const string kHttpVerbDELETE = "DELETE";

		// Token: 0x0400002C RID: 44
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <disposeCertificateHandlerOnDispose>k__BackingField;

		// Token: 0x0400002D RID: 45
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <disposeDownloadHandlerOnDispose>k__BackingField;

		// Token: 0x0400002E RID: 46
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <disposeUploadHandlerOnDispose>k__BackingField;

		// Token: 0x0200000A RID: 10
		internal enum UnityWebRequestMethod
		{
			// Token: 0x04000030 RID: 48
			Get,
			// Token: 0x04000031 RID: 49
			Post,
			// Token: 0x04000032 RID: 50
			Put,
			// Token: 0x04000033 RID: 51
			Head,
			// Token: 0x04000034 RID: 52
			Custom
		}

		// Token: 0x0200000B RID: 11
		internal enum UnityWebRequestError
		{
			// Token: 0x04000036 RID: 54
			OK,
			// Token: 0x04000037 RID: 55
			Unknown,
			// Token: 0x04000038 RID: 56
			SDKError,
			// Token: 0x04000039 RID: 57
			UnsupportedProtocol,
			// Token: 0x0400003A RID: 58
			MalformattedUrl,
			// Token: 0x0400003B RID: 59
			CannotResolveProxy,
			// Token: 0x0400003C RID: 60
			CannotResolveHost,
			// Token: 0x0400003D RID: 61
			CannotConnectToHost,
			// Token: 0x0400003E RID: 62
			AccessDenied,
			// Token: 0x0400003F RID: 63
			GenericHttpError,
			// Token: 0x04000040 RID: 64
			WriteError,
			// Token: 0x04000041 RID: 65
			ReadError,
			// Token: 0x04000042 RID: 66
			OutOfMemory,
			// Token: 0x04000043 RID: 67
			Timeout,
			// Token: 0x04000044 RID: 68
			HTTPPostError,
			// Token: 0x04000045 RID: 69
			SSLCannotConnect,
			// Token: 0x04000046 RID: 70
			Aborted,
			// Token: 0x04000047 RID: 71
			TooManyRedirects,
			// Token: 0x04000048 RID: 72
			ReceivedNoData,
			// Token: 0x04000049 RID: 73
			SSLNotSupported,
			// Token: 0x0400004A RID: 74
			FailedToSendData,
			// Token: 0x0400004B RID: 75
			FailedToReceiveData,
			// Token: 0x0400004C RID: 76
			SSLCertificateError,
			// Token: 0x0400004D RID: 77
			SSLCipherNotAvailable,
			// Token: 0x0400004E RID: 78
			SSLCACertError,
			// Token: 0x0400004F RID: 79
			UnrecognizedContentEncoding,
			// Token: 0x04000050 RID: 80
			LoginFailed,
			// Token: 0x04000051 RID: 81
			SSLShutdownFailed,
			// Token: 0x04000052 RID: 82
			NoInternetConnection
		}

		// Token: 0x0200000C RID: 12
		public enum Result
		{
			// Token: 0x04000054 RID: 84
			InProgress,
			// Token: 0x04000055 RID: 85
			Success,
			// Token: 0x04000056 RID: 86
			ConnectionError,
			// Token: 0x04000057 RID: 87
			ProtocolError,
			// Token: 0x04000058 RID: 88
			DataProcessingError
		}
	}
}
