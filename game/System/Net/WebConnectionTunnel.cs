using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006C4 RID: 1732
	internal class WebConnectionTunnel
	{
		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x060037B4 RID: 14260 RVA: 0x000C3C82 File Offset: 0x000C1E82
		public HttpWebRequest Request
		{
			[CompilerGenerated]
			get
			{
				return this.<Request>k__BackingField;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x060037B5 RID: 14261 RVA: 0x000C3C8A File Offset: 0x000C1E8A
		public Uri ConnectUri
		{
			[CompilerGenerated]
			get
			{
				return this.<ConnectUri>k__BackingField;
			}
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000C3C92 File Offset: 0x000C1E92
		public WebConnectionTunnel(HttpWebRequest request, Uri connectUri)
		{
			this.Request = request;
			this.ConnectUri = connectUri;
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x060037B7 RID: 14263 RVA: 0x000C3CA8 File Offset: 0x000C1EA8
		// (set) Token: 0x060037B8 RID: 14264 RVA: 0x000C3CB0 File Offset: 0x000C1EB0
		public bool Success
		{
			[CompilerGenerated]
			get
			{
				return this.<Success>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Success>k__BackingField = value;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x060037B9 RID: 14265 RVA: 0x000C3CB9 File Offset: 0x000C1EB9
		// (set) Token: 0x060037BA RID: 14266 RVA: 0x000C3CC1 File Offset: 0x000C1EC1
		public bool CloseConnection
		{
			[CompilerGenerated]
			get
			{
				return this.<CloseConnection>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CloseConnection>k__BackingField = value;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x060037BB RID: 14267 RVA: 0x000C3CCA File Offset: 0x000C1ECA
		// (set) Token: 0x060037BC RID: 14268 RVA: 0x000C3CD2 File Offset: 0x000C1ED2
		public int StatusCode
		{
			[CompilerGenerated]
			get
			{
				return this.<StatusCode>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<StatusCode>k__BackingField = value;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x060037BD RID: 14269 RVA: 0x000C3CDB File Offset: 0x000C1EDB
		// (set) Token: 0x060037BE RID: 14270 RVA: 0x000C3CE3 File Offset: 0x000C1EE3
		public string StatusDescription
		{
			[CompilerGenerated]
			get
			{
				return this.<StatusDescription>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<StatusDescription>k__BackingField = value;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x000C3CEC File Offset: 0x000C1EEC
		// (set) Token: 0x060037C0 RID: 14272 RVA: 0x000C3CF4 File Offset: 0x000C1EF4
		public string[] Challenge
		{
			[CompilerGenerated]
			get
			{
				return this.<Challenge>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Challenge>k__BackingField = value;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x060037C1 RID: 14273 RVA: 0x000C3CFD File Offset: 0x000C1EFD
		// (set) Token: 0x060037C2 RID: 14274 RVA: 0x000C3D05 File Offset: 0x000C1F05
		public WebHeaderCollection Headers
		{
			[CompilerGenerated]
			get
			{
				return this.<Headers>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Headers>k__BackingField = value;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x060037C3 RID: 14275 RVA: 0x000C3D0E File Offset: 0x000C1F0E
		// (set) Token: 0x060037C4 RID: 14276 RVA: 0x000C3D16 File Offset: 0x000C1F16
		public Version ProxyVersion
		{
			[CompilerGenerated]
			get
			{
				return this.<ProxyVersion>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ProxyVersion>k__BackingField = value;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x060037C5 RID: 14277 RVA: 0x000C3D1F File Offset: 0x000C1F1F
		// (set) Token: 0x060037C6 RID: 14278 RVA: 0x000C3D27 File Offset: 0x000C1F27
		public byte[] Data
		{
			[CompilerGenerated]
			get
			{
				return this.<Data>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Data>k__BackingField = value;
			}
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000C3D30 File Offset: 0x000C1F30
		internal Task Initialize(Stream stream, CancellationToken cancellationToken)
		{
			WebConnectionTunnel.<Initialize>d__42 <Initialize>d__;
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.stream = stream;
			<Initialize>d__.cancellationToken = cancellationToken;
			<Initialize>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<WebConnectionTunnel.<Initialize>d__42>(ref <Initialize>d__);
			return <Initialize>d__.<>t__builder.Task;
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000C3D84 File Offset: 0x000C1F84
		private Task<ValueTuple<WebHeaderCollection, byte[], int>> ReadHeaders(Stream stream, CancellationToken cancellationToken)
		{
			WebConnectionTunnel.<ReadHeaders>d__43 <ReadHeaders>d__;
			<ReadHeaders>d__.<>4__this = this;
			<ReadHeaders>d__.stream = stream;
			<ReadHeaders>d__.cancellationToken = cancellationToken;
			<ReadHeaders>d__.<>t__builder = AsyncTaskMethodBuilder<ValueTuple<WebHeaderCollection, byte[], int>>.Create();
			<ReadHeaders>d__.<>1__state = -1;
			<ReadHeaders>d__.<>t__builder.Start<WebConnectionTunnel.<ReadHeaders>d__43>(ref <ReadHeaders>d__);
			return <ReadHeaders>d__.<>t__builder.Task;
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000C3DD8 File Offset: 0x000C1FD8
		private void FlushContents(Stream stream, int contentLength)
		{
			while (contentLength > 0)
			{
				byte[] buffer = new byte[contentLength];
				int num = stream.Read(buffer, 0, contentLength);
				if (num <= 0)
				{
					break;
				}
				contentLength -= num;
			}
		}

		// Token: 0x04002083 RID: 8323
		[CompilerGenerated]
		private readonly HttpWebRequest <Request>k__BackingField;

		// Token: 0x04002084 RID: 8324
		[CompilerGenerated]
		private readonly Uri <ConnectUri>k__BackingField;

		// Token: 0x04002085 RID: 8325
		private HttpWebRequest connectRequest;

		// Token: 0x04002086 RID: 8326
		private WebConnectionTunnel.NtlmAuthState ntlmAuthState;

		// Token: 0x04002087 RID: 8327
		[CompilerGenerated]
		private bool <Success>k__BackingField;

		// Token: 0x04002088 RID: 8328
		[CompilerGenerated]
		private bool <CloseConnection>k__BackingField;

		// Token: 0x04002089 RID: 8329
		[CompilerGenerated]
		private int <StatusCode>k__BackingField;

		// Token: 0x0400208A RID: 8330
		[CompilerGenerated]
		private string <StatusDescription>k__BackingField;

		// Token: 0x0400208B RID: 8331
		[CompilerGenerated]
		private string[] <Challenge>k__BackingField;

		// Token: 0x0400208C RID: 8332
		[CompilerGenerated]
		private WebHeaderCollection <Headers>k__BackingField;

		// Token: 0x0400208D RID: 8333
		[CompilerGenerated]
		private Version <ProxyVersion>k__BackingField;

		// Token: 0x0400208E RID: 8334
		[CompilerGenerated]
		private byte[] <Data>k__BackingField;

		// Token: 0x020006C5 RID: 1733
		private enum NtlmAuthState
		{
			// Token: 0x04002090 RID: 8336
			None,
			// Token: 0x04002091 RID: 8337
			Challenge,
			// Token: 0x04002092 RID: 8338
			Response
		}

		// Token: 0x020006C6 RID: 1734
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <Initialize>d__42 : IAsyncStateMachine
		{
			// Token: 0x060037CA RID: 14282 RVA: 0x000C3E08 File Offset: 0x000C2008
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebConnectionTunnel webConnectionTunnel = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<ValueTuple<WebHeaderCollection, byte[], int>>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<ValueTuple<WebHeaderCollection, byte[], int>>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_36F;
						}
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append("CONNECT ");
						stringBuilder.Append(webConnectionTunnel.Request.Address.Host);
						stringBuilder.Append(':');
						stringBuilder.Append(webConnectionTunnel.Request.Address.Port);
						stringBuilder.Append(" HTTP/");
						if (webConnectionTunnel.Request.ProtocolVersion == HttpVersion.Version11)
						{
							stringBuilder.Append("1.1");
						}
						else
						{
							stringBuilder.Append("1.0");
						}
						stringBuilder.Append("\r\nHost: ");
						stringBuilder.Append(webConnectionTunnel.Request.Address.Authority);
						bool flag = false;
						string[] challenge = webConnectionTunnel.Challenge;
						webConnectionTunnel.Challenge = null;
						string text = webConnectionTunnel.Request.Headers["Proxy-Authorization"];
						this.<have_auth>5__2 = (text != null);
						if (this.<have_auth>5__2)
						{
							stringBuilder.Append("\r\nProxy-Authorization: ");
							stringBuilder.Append(text);
							flag = text.ToUpper().Contains("NTLM");
						}
						else if (challenge != null && webConnectionTunnel.StatusCode == 407)
						{
							ICredentials credentials = webConnectionTunnel.Request.Proxy.Credentials;
							this.<have_auth>5__2 = true;
							if (webConnectionTunnel.connectRequest == null)
							{
								webConnectionTunnel.connectRequest = (HttpWebRequest)WebRequest.Create(string.Concat(new string[]
								{
									webConnectionTunnel.ConnectUri.Scheme,
									"://",
									webConnectionTunnel.ConnectUri.Host,
									":",
									webConnectionTunnel.ConnectUri.Port.ToString(),
									"/"
								}));
								webConnectionTunnel.connectRequest.Method = "CONNECT";
								webConnectionTunnel.connectRequest.Credentials = credentials;
							}
							if (credentials != null)
							{
								for (int i = 0; i < challenge.Length; i++)
								{
									Authorization authorization = AuthenticationManager.Authenticate(challenge[i], webConnectionTunnel.connectRequest, credentials);
									if (authorization != null)
									{
										flag = (authorization.ModuleAuthenticationType == "NTLM");
										stringBuilder.Append("\r\nProxy-Authorization: ");
										stringBuilder.Append(authorization.Message);
										break;
									}
								}
							}
						}
						if (flag)
						{
							stringBuilder.Append("\r\nProxy-Connection: keep-alive");
							webConnectionTunnel.ntlmAuthState++;
						}
						stringBuilder.Append("\r\n\r\n");
						webConnectionTunnel.StatusCode = 0;
						byte[] bytes = Encoding.Default.GetBytes(stringBuilder.ToString());
						awaiter2 = this.stream.WriteAsync(bytes, 0, bytes.Length, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebConnectionTunnel.<Initialize>d__42>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter2.GetResult();
					awaiter = webConnectionTunnel.ReadHeaders(this.stream, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<ValueTuple<WebHeaderCollection, byte[], int>>.ConfiguredTaskAwaiter, WebConnectionTunnel.<Initialize>d__42>(ref awaiter, ref this);
						return;
					}
					IL_36F:
					ValueTuple<WebHeaderCollection, byte[], int> result = awaiter.GetResult();
					webConnectionTunnel.Headers = result.Item1;
					webConnectionTunnel.Data = result.Item2;
					webConnectionTunnel.StatusCode = result.Item3;
					if ((!this.<have_auth>5__2 || webConnectionTunnel.ntlmAuthState == WebConnectionTunnel.NtlmAuthState.Challenge) && webConnectionTunnel.Headers != null && webConnectionTunnel.StatusCode == 407)
					{
						string text2 = webConnectionTunnel.Headers["Connection"];
						if (!string.IsNullOrEmpty(text2) && text2.ToLower() == "close")
						{
							webConnectionTunnel.CloseConnection = true;
						}
						webConnectionTunnel.Challenge = webConnectionTunnel.Headers.GetValues("Proxy-Authenticate");
						webConnectionTunnel.Success = false;
					}
					else
					{
						webConnectionTunnel.Success = (webConnectionTunnel.StatusCode == 200 && webConnectionTunnel.Headers != null);
					}
					if (webConnectionTunnel.Challenge == null && (webConnectionTunnel.StatusCode == 401 || webConnectionTunnel.StatusCode == 407))
					{
						HttpWebResponse response = new HttpWebResponse(webConnectionTunnel.ConnectUri, "CONNECT", (HttpStatusCode)webConnectionTunnel.StatusCode, webConnectionTunnel.Headers);
						throw new WebException((webConnectionTunnel.StatusCode == 407) ? "(407) Proxy Authentication Required" : "(401) Unauthorized", null, WebExceptionStatus.ProtocolError, response);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060037CB RID: 14283 RVA: 0x000C4304 File Offset: 0x000C2504
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002093 RID: 8339
			public int <>1__state;

			// Token: 0x04002094 RID: 8340
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04002095 RID: 8341
			public WebConnectionTunnel <>4__this;

			// Token: 0x04002096 RID: 8342
			public Stream stream;

			// Token: 0x04002097 RID: 8343
			public CancellationToken cancellationToken;

			// Token: 0x04002098 RID: 8344
			private bool <have_auth>5__2;

			// Token: 0x04002099 RID: 8345
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400209A RID: 8346
			private ConfiguredTaskAwaitable<ValueTuple<WebHeaderCollection, byte[], int>>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020006C7 RID: 1735
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadHeaders>d__43 : IAsyncStateMachine
		{
			// Token: 0x060037CC RID: 14284 RVA: 0x000C4314 File Offset: 0x000C2514
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebConnectionTunnel webConnectionTunnel = this.<>4__this;
				ValueTuple<WebHeaderCollection, byte[], int> result2;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_C4;
					}
					this.<retBuffer>5__2 = null;
					this.<status>5__3 = 200;
					this.<buffer>5__4 = new byte[1024];
					this.<ms>5__5 = new MemoryStream();
					IL_41:
					this.cancellationToken.ThrowIfCancellationRequested();
					awaiter = this.stream.ReadAsync(this.<buffer>5__4, 0, 1024, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, WebConnectionTunnel.<ReadHeaders>d__43>(ref awaiter, ref this);
						return;
					}
					IL_C4:
					int result = awaiter.GetResult();
					if (result == 0)
					{
						throw WebConnection.GetException(WebExceptionStatus.ServerProtocolViolation, null);
					}
					this.<ms>5__5.Write(this.<buffer>5__4, 0, result);
					int num2 = 0;
					string text = null;
					bool flag = false;
					WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
					while (WebConnection.ReadLine(this.<ms>5__5.GetBuffer(), ref num2, (int)this.<ms>5__5.Length, ref text))
					{
						if (text == null)
						{
							string text2 = webHeaderCollection["Content-Length"];
							int num3;
							if (string.IsNullOrEmpty(text2) || !int.TryParse(text2, out num3))
							{
								num3 = 0;
							}
							if (this.<ms>5__5.Length - (long)num2 - (long)num3 > 0L)
							{
								this.<retBuffer>5__2 = new byte[this.<ms>5__5.Length - (long)num2 - (long)num3];
								Buffer.BlockCopy(this.<ms>5__5.GetBuffer(), num2 + num3, this.<retBuffer>5__2, 0, this.<retBuffer>5__2.Length);
							}
							else
							{
								webConnectionTunnel.FlushContents(this.stream, num3 - (int)(this.<ms>5__5.Length - (long)num2));
							}
							result2 = new ValueTuple<WebHeaderCollection, byte[], int>(webHeaderCollection, this.<retBuffer>5__2, this.<status>5__3);
							goto IL_2BD;
						}
						if (flag)
						{
							webHeaderCollection.Add(text);
						}
						else
						{
							string[] array = text.Split(' ', StringSplitOptions.None);
							if (array.Length < 2)
							{
								throw WebConnection.GetException(WebExceptionStatus.ServerProtocolViolation, null);
							}
							if (string.Compare(array[0], "HTTP/1.1", true) == 0)
							{
								webConnectionTunnel.ProxyVersion = HttpVersion.Version11;
							}
							else
							{
								if (string.Compare(array[0], "HTTP/1.0", true) != 0)
								{
									throw WebConnection.GetException(WebExceptionStatus.ServerProtocolViolation, null);
								}
								webConnectionTunnel.ProxyVersion = HttpVersion.Version10;
							}
							this.<status>5__3 = (int)uint.Parse(array[1]);
							if (array.Length >= 3)
							{
								webConnectionTunnel.StatusDescription = string.Join(" ", array, 2, array.Length - 2);
							}
							flag = true;
						}
					}
					goto IL_41;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<retBuffer>5__2 = null;
					this.<buffer>5__4 = null;
					this.<ms>5__5 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2BD:
				this.<>1__state = -2;
				this.<retBuffer>5__2 = null;
				this.<buffer>5__4 = null;
				this.<ms>5__5 = null;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060037CD RID: 14285 RVA: 0x000C4624 File Offset: 0x000C2824
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400209B RID: 8347
			public int <>1__state;

			// Token: 0x0400209C RID: 8348
			public AsyncTaskMethodBuilder<ValueTuple<WebHeaderCollection, byte[], int>> <>t__builder;

			// Token: 0x0400209D RID: 8349
			public CancellationToken cancellationToken;

			// Token: 0x0400209E RID: 8350
			public Stream stream;

			// Token: 0x0400209F RID: 8351
			public WebConnectionTunnel <>4__this;

			// Token: 0x040020A0 RID: 8352
			private byte[] <retBuffer>5__2;

			// Token: 0x040020A1 RID: 8353
			private int <status>5__3;

			// Token: 0x040020A2 RID: 8354
			private byte[] <buffer>5__4;

			// Token: 0x040020A3 RID: 8355
			private MemoryStream <ms>5__5;

			// Token: 0x040020A4 RID: 8356
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
