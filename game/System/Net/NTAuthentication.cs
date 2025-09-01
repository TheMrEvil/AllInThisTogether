using System;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000568 RID: 1384
	internal class NTAuthentication
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002CDB RID: 11483 RVA: 0x000997CA File Offset: 0x000979CA
		internal bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x000997D2 File Offset: 0x000979D2
		internal bool IsValidContext
		{
			get
			{
				return this._securityContext != null && !this._securityContext.IsInvalid;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002CDD RID: 11485 RVA: 0x000997EC File Offset: 0x000979EC
		internal string Package
		{
			get
			{
				return this._package;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x000997F4 File Offset: 0x000979F4
		internal bool IsServer
		{
			get
			{
				return this._isServer;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002CDF RID: 11487 RVA: 0x000997FC File Offset: 0x000979FC
		internal string ClientSpecifiedSpn
		{
			get
			{
				if (this._clientSpecifiedSpn == null)
				{
					this._clientSpecifiedSpn = this.GetClientSpecifiedSpn();
				}
				return this._clientSpecifiedSpn;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x00099818 File Offset: 0x00097A18
		internal string ProtocolName
		{
			get
			{
				if (this._protocolName == null)
				{
					string text = null;
					if (this.IsValidContext)
					{
						text = NegotiateStreamPal.QueryContextAuthenticationPackage(this._securityContext);
						if (this.IsCompleted)
						{
							this._protocolName = text;
						}
					}
					return text ?? string.Empty;
				}
				return this._protocolName;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x00099863 File Offset: 0x00097A63
		internal bool IsKerberos
		{
			get
			{
				if (this._lastProtocolName == null)
				{
					this._lastProtocolName = this.ProtocolName;
				}
				return this._lastProtocolName == "Kerberos";
			}
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x00099886 File Offset: 0x00097A86
		internal NTAuthentication(bool isServer, string package, NetworkCredential credential, string spn, ContextFlagsPal requestedContextFlags, ChannelBinding channelBinding)
		{
			this.Initialize(isServer, package, credential, spn, requestedContextFlags, channelBinding);
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000998A0 File Offset: 0x00097AA0
		private void Initialize(bool isServer, string package, NetworkCredential credential, string spn, ContextFlagsPal requestedContextFlags, ChannelBinding channelBinding)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, package, spn, requestedContextFlags, "Initialize");
			}
			this._tokenSize = NegotiateStreamPal.QueryMaxTokenSize(package);
			this._isServer = isServer;
			this._spn = spn;
			this._securityContext = null;
			this._requestedContextFlags = requestedContextFlags;
			this._package = package;
			this._channelBinding = channelBinding;
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("Peer SPN-> '{0}'", new object[]
				{
					this._spn
				}), "Initialize");
			}
			if (credential == CredentialCache.DefaultCredentials)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "using DefaultCredentials", "Initialize");
				}
				this._credentialsHandle = NegotiateStreamPal.AcquireDefaultCredential(package, this._isServer);
				return;
			}
			this._credentialsHandle = NegotiateStreamPal.AcquireCredentialsHandle(package, this._isServer, credential);
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x00099974 File Offset: 0x00097B74
		internal SafeDeleteContext GetContext(out SecurityStatusPal status)
		{
			status = new SecurityStatusPal(SecurityStatusPalErrorCode.OK, null);
			if (!this.IsCompleted || !this.IsValidContext)
			{
				NetEventSource.Fail(this, "Should be called only when completed with success, currently is not!", "GetContext");
			}
			if (!this.IsServer)
			{
				NetEventSource.Fail(this, "The method must not be called by the client side!", "GetContext");
			}
			if (!this.IsValidContext)
			{
				status = new SecurityStatusPal(SecurityStatusPalErrorCode.InvalidHandle, null);
				return null;
			}
			return this._securityContext;
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000999E4 File Offset: 0x00097BE4
		internal void CloseContext()
		{
			if (this._securityContext != null && !this._securityContext.IsClosed)
			{
				this._securityContext.Dispose();
			}
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x00099A06 File Offset: 0x00097C06
		internal int VerifySignature(byte[] buffer, int offset, int count)
		{
			return NegotiateStreamPal.VerifySignature(this._securityContext, buffer, offset, count);
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x00099A16 File Offset: 0x00097C16
		internal int MakeSignature(byte[] buffer, int offset, int count, ref byte[] output)
		{
			return NegotiateStreamPal.MakeSignature(this._securityContext, buffer, offset, count, ref output);
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x00099A28 File Offset: 0x00097C28
		internal string GetOutgoingBlob(string incomingBlob)
		{
			byte[] array = null;
			if (incomingBlob != null && incomingBlob.Length > 0)
			{
				array = Convert.FromBase64String(incomingBlob);
			}
			byte[] array2 = null;
			if ((this.IsValidContext || this.IsCompleted) && array == null)
			{
				this._isCompleted = true;
			}
			else
			{
				SecurityStatusPal securityStatusPal;
				array2 = this.GetOutgoingBlob(array, true, out securityStatusPal);
			}
			string result = null;
			if (array2 != null && array2.Length != 0)
			{
				result = Convert.ToBase64String(array2);
			}
			if (this.IsCompleted)
			{
				this.CloseContext();
			}
			return result;
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x00099A94 File Offset: 0x00097C94
		internal byte[] GetOutgoingBlob(byte[] incomingBlob, bool thrownOnError)
		{
			SecurityStatusPal securityStatusPal;
			return this.GetOutgoingBlob(incomingBlob, thrownOnError, out securityStatusPal);
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x00099AAC File Offset: 0x00097CAC
		internal byte[] GetOutgoingBlob(byte[] incomingBlob, bool throwOnError, out SecurityStatusPal statusCode)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, incomingBlob, "GetOutgoingBlob");
			}
			SecurityBuffer[] inSecurityBufferArray = null;
			if (incomingBlob != null && this._channelBinding != null)
			{
				inSecurityBufferArray = new SecurityBuffer[]
				{
					new SecurityBuffer(incomingBlob, SecurityBufferType.SECBUFFER_TOKEN),
					new SecurityBuffer(this._channelBinding)
				};
			}
			else if (incomingBlob != null)
			{
				inSecurityBufferArray = new SecurityBuffer[]
				{
					new SecurityBuffer(incomingBlob, SecurityBufferType.SECBUFFER_TOKEN)
				};
			}
			else if (this._channelBinding != null)
			{
				inSecurityBufferArray = new SecurityBuffer[]
				{
					new SecurityBuffer(this._channelBinding)
				};
			}
			SecurityBuffer securityBuffer = new SecurityBuffer(this._tokenSize, SecurityBufferType.SECBUFFER_TOKEN);
			bool flag = this._securityContext == null;
			try
			{
				if (!this._isServer)
				{
					statusCode = NegotiateStreamPal.InitializeSecurityContext(this._credentialsHandle, ref this._securityContext, this._spn, this._requestedContextFlags, inSecurityBufferArray, securityBuffer, ref this._contextFlags);
					if (NetEventSource.IsEnabled)
					{
						NetEventSource.Info(this, FormattableStringFactory.Create("SSPIWrapper.InitializeSecurityContext() returns statusCode:0x{0:x8} ({1})", new object[]
						{
							(int)statusCode.ErrorCode,
							statusCode
						}), "GetOutgoingBlob");
					}
					if (statusCode.ErrorCode == SecurityStatusPalErrorCode.CompleteNeeded)
					{
						statusCode = NegotiateStreamPal.CompleteAuthToken(ref this._securityContext, new SecurityBuffer[]
						{
							securityBuffer
						});
						if (NetEventSource.IsEnabled)
						{
							NetEventSource.Info(this, FormattableStringFactory.Create("SSPIWrapper.CompleteAuthToken() returns statusCode:0x{0:x8} ({1})", new object[]
							{
								(int)statusCode.ErrorCode,
								statusCode
							}), "GetOutgoingBlob");
						}
						securityBuffer.token = null;
					}
				}
				else
				{
					statusCode = NegotiateStreamPal.AcceptSecurityContext(this._credentialsHandle, ref this._securityContext, this._requestedContextFlags, inSecurityBufferArray, securityBuffer, ref this._contextFlags);
					if (NetEventSource.IsEnabled)
					{
						NetEventSource.Info(this, FormattableStringFactory.Create("SSPIWrapper.AcceptSecurityContext() returns statusCode:0x{0:x8} ({1})", new object[]
						{
							(int)statusCode.ErrorCode,
							statusCode
						}), "GetOutgoingBlob");
					}
				}
			}
			finally
			{
				if (flag && this._credentialsHandle != null)
				{
					this._credentialsHandle.Dispose();
				}
			}
			if (statusCode.ErrorCode < SecurityStatusPalErrorCode.OutOfMemory)
			{
				if (flag && this._credentialsHandle != null)
				{
					SSPIHandleCache.CacheCredential(this._credentialsHandle);
				}
				if (statusCode.ErrorCode == SecurityStatusPalErrorCode.OK)
				{
					this._isCompleted = true;
				}
				else if (NetEventSource.IsEnabled && NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("need continue statusCode:0x{0:x8} ({1}) _securityContext:{2}", new object[]
					{
						(int)statusCode.ErrorCode,
						statusCode,
						this._securityContext
					}), "GetOutgoingBlob");
				}
				if (NetEventSource.IsEnabled && NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, FormattableStringFactory.Create("IsCompleted: {0}", new object[]
					{
						this.IsCompleted
					}), "GetOutgoingBlob");
				}
				return securityBuffer.token;
			}
			this.CloseContext();
			this._isCompleted = true;
			if (throwOnError)
			{
				Exception ex = NegotiateStreamPal.CreateExceptionFromError(statusCode);
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, ex, "GetOutgoingBlob");
				}
				throw ex;
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(this, FormattableStringFactory.Create("null statusCode:0x{0:x8} ({1})", new object[]
				{
					(int)statusCode.ErrorCode,
					statusCode
				}), "GetOutgoingBlob");
			}
			return null;
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x00099DF0 File Offset: 0x00097FF0
		private string GetClientSpecifiedSpn()
		{
			if (!this.IsValidContext || !this.IsCompleted)
			{
				NetEventSource.Fail(this, "Trying to get the client SPN before handshaking is done!", "GetClientSpecifiedSpn");
			}
			string text = NegotiateStreamPal.QueryContextClientSpecifiedSpn(this._securityContext);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("The client specified SPN is [{0}]", new object[]
				{
					text
				}), "GetClientSpecifiedSpn");
			}
			return text;
		}

		// Token: 0x04001831 RID: 6193
		private bool _isServer;

		// Token: 0x04001832 RID: 6194
		private SafeFreeCredentials _credentialsHandle;

		// Token: 0x04001833 RID: 6195
		private SafeDeleteContext _securityContext;

		// Token: 0x04001834 RID: 6196
		private string _spn;

		// Token: 0x04001835 RID: 6197
		private int _tokenSize;

		// Token: 0x04001836 RID: 6198
		private ContextFlagsPal _requestedContextFlags;

		// Token: 0x04001837 RID: 6199
		private ContextFlagsPal _contextFlags;

		// Token: 0x04001838 RID: 6200
		private bool _isCompleted;

		// Token: 0x04001839 RID: 6201
		private string _package;

		// Token: 0x0400183A RID: 6202
		private string _lastProtocolName;

		// Token: 0x0400183B RID: 6203
		private string _protocolName;

		// Token: 0x0400183C RID: 6204
		private string _clientSpecifiedSpn;

		// Token: 0x0400183D RID: 6205
		private ChannelBinding _channelBinding;
	}
}
