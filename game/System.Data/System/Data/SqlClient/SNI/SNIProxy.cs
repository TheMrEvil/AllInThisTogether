using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200029A RID: 666
	internal class SNIProxy
	{
		// Token: 0x06001EAC RID: 7852 RVA: 0x00007EED File Offset: 0x000060ED
		public void Terminate()
		{
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00090EDC File Offset: 0x0008F0DC
		public uint EnableSsl(SNIHandle handle, uint options)
		{
			uint result;
			try
			{
				result = handle.EnableSsl(options);
			}
			catch (Exception sniException)
			{
				result = SNICommon.ReportSNIError(SNIProviders.SSL_PROV, 31U, sniException);
			}
			return result;
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x00090F14 File Offset: 0x0008F114
		public uint DisableSsl(SNIHandle handle)
		{
			handle.DisableSsl();
			return 0U;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00090F20 File Offset: 0x0008F120
		public void GenSspiClientContext(SspiClientContextStatus sspiClientContextStatus, byte[] receivedBuff, ref byte[] sendBuff, byte[] serverName)
		{
			SafeDeleteContext securityContext = sspiClientContextStatus.SecurityContext;
			ContextFlagsPal contextFlags = sspiClientContextStatus.ContextFlags;
			SafeFreeCredentials credentialsHandle = sspiClientContextStatus.CredentialsHandle;
			string package = "Negotiate";
			if (securityContext == null)
			{
				credentialsHandle = NegotiateStreamPal.AcquireDefaultCredential(package, false);
			}
			SecurityBuffer[] inSecurityBufferArray;
			if (receivedBuff != null)
			{
				inSecurityBufferArray = new SecurityBuffer[]
				{
					new SecurityBuffer(receivedBuff, SecurityBufferType.SECBUFFER_TOKEN)
				};
			}
			else
			{
				inSecurityBufferArray = new SecurityBuffer[0];
			}
			SecurityBuffer securityBuffer = new SecurityBuffer(NegotiateStreamPal.QueryMaxTokenSize(package), SecurityBufferType.SECBUFFER_TOKEN);
			ContextFlagsPal requestedContextFlags = ContextFlagsPal.Delegate | ContextFlagsPal.MutualAuth | ContextFlagsPal.Confidentiality | ContextFlagsPal.Connection;
			string @string = Encoding.UTF8.GetString(serverName);
			SecurityStatusPal securityStatusPal = NegotiateStreamPal.InitializeSecurityContext(credentialsHandle, ref securityContext, @string, requestedContextFlags, inSecurityBufferArray, securityBuffer, ref contextFlags);
			if (securityStatusPal.ErrorCode == SecurityStatusPalErrorCode.CompleteNeeded || securityStatusPal.ErrorCode == SecurityStatusPalErrorCode.CompAndContinue)
			{
				inSecurityBufferArray = new SecurityBuffer[]
				{
					securityBuffer
				};
				securityStatusPal = NegotiateStreamPal.CompleteAuthToken(ref securityContext, inSecurityBufferArray);
				securityBuffer.token = null;
			}
			sendBuff = securityBuffer.token;
			if (sendBuff == null)
			{
				sendBuff = Array.Empty<byte>();
			}
			sspiClientContextStatus.SecurityContext = securityContext;
			sspiClientContextStatus.ContextFlags = contextFlags;
			sspiClientContextStatus.CredentialsHandle = credentialsHandle;
			if (!SNIProxy.IsErrorStatus(securityStatusPal.ErrorCode))
			{
				return;
			}
			if (securityStatusPal.ErrorCode == SecurityStatusPalErrorCode.InternalError)
			{
				throw new InvalidOperationException(SQLMessage.KerberosTicketMissingError() + "\n" + securityStatusPal.ToString());
			}
			throw new InvalidOperationException(SQLMessage.SSPIGenerateError() + "\n" + securityStatusPal.ToString());
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00091066 File Offset: 0x0008F266
		private static bool IsErrorStatus(SecurityStatusPalErrorCode errorCode)
		{
			return errorCode != SecurityStatusPalErrorCode.NotSet && errorCode != SecurityStatusPalErrorCode.OK && errorCode != SecurityStatusPalErrorCode.ContinueNeeded && errorCode != SecurityStatusPalErrorCode.CompleteNeeded && errorCode != SecurityStatusPalErrorCode.CompAndContinue && errorCode != SecurityStatusPalErrorCode.ContextExpired && errorCode != SecurityStatusPalErrorCode.CredentialsNeeded && errorCode != SecurityStatusPalErrorCode.Renegotiate;
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0004549D File Offset: 0x0004369D
		public uint InitializeSspiPackage(ref uint maxLength)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x0009108C File Offset: 0x0008F28C
		public uint SetConnectionBufferSize(SNIHandle handle, uint bufferSize)
		{
			handle.SetBufferSize((int)bufferSize);
			return 0U;
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00091098 File Offset: 0x0008F298
		public uint PacketGetData(SNIPacket packet, byte[] inBuff, ref uint dataSize)
		{
			int num = 0;
			packet.GetData(inBuff, ref num);
			dataSize = (uint)num;
			return 0U;
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x000910B4 File Offset: 0x0008F2B4
		public uint ReadSyncOverAsync(SNIHandle handle, out SNIPacket packet, int timeout)
		{
			return handle.Receive(out packet, timeout);
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x000910BE File Offset: 0x0008F2BE
		public uint GetConnectionId(SNIHandle handle, ref Guid clientConnectionId)
		{
			clientConnectionId = handle.ConnectionId;
			return 0U;
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x000910D0 File Offset: 0x0008F2D0
		public uint WritePacket(SNIHandle handle, SNIPacket packet, bool sync)
		{
			SNIPacket snipacket = packet.Clone();
			uint result;
			if (sync)
			{
				result = handle.Send(snipacket);
				snipacket.Dispose();
			}
			else
			{
				result = handle.SendAsync(snipacket, true, null);
			}
			return result;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x00091104 File Offset: 0x0008F304
		public SNIHandle CreateConnectionHandle(object callbackObject, string fullServerName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, ref byte[] spnBuffer, bool flushCache, bool async, bool parallel, bool isIntegratedSecurity)
		{
			instanceName = new byte[1];
			bool flag;
			string localDBDataSource = this.GetLocalDBDataSource(fullServerName, out flag);
			if (flag)
			{
				return null;
			}
			fullServerName = (localDBDataSource ?? fullServerName);
			DataSource dataSource = DataSource.ParseServerName(fullServerName);
			if (dataSource == null)
			{
				return null;
			}
			SNIHandle result = null;
			switch (dataSource.ConnectionProtocol)
			{
			case DataSource.Protocol.TCP:
			case DataSource.Protocol.None:
			case DataSource.Protocol.Admin:
				result = this.CreateTcpHandle(dataSource, timerExpire, callbackObject, parallel);
				break;
			case DataSource.Protocol.NP:
				result = this.CreateNpHandle(dataSource, timerExpire, callbackObject, parallel);
				break;
			}
			if (isIntegratedSecurity)
			{
				try
				{
					spnBuffer = SNIProxy.GetSqlServerSPN(dataSource);
				}
				catch (Exception sniException)
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.INVALID_PROV, 44U, sniException);
				}
			}
			return result;
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x000911B8 File Offset: 0x0008F3B8
		private static byte[] GetSqlServerSPN(DataSource dataSource)
		{
			string serverName = dataSource.ServerName;
			string portOrInstanceName = null;
			if (dataSource.Port != -1)
			{
				portOrInstanceName = dataSource.Port.ToString();
			}
			else if (!string.IsNullOrWhiteSpace(dataSource.InstanceName))
			{
				portOrInstanceName = dataSource.InstanceName;
			}
			else if (dataSource.ConnectionProtocol == DataSource.Protocol.TCP)
			{
				portOrInstanceName = 1433.ToString();
			}
			return SNIProxy.GetSqlServerSPN(serverName, portOrInstanceName);
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x0009121C File Offset: 0x0008F41C
		private static byte[] GetSqlServerSPN(string hostNameOrAddress, string portOrInstanceName)
		{
			IPHostEntry iphostEntry = null;
			string str;
			try
			{
				iphostEntry = Dns.GetHostEntry(hostNameOrAddress);
			}
			catch (SocketException)
			{
			}
			finally
			{
				str = (((iphostEntry != null) ? iphostEntry.HostName : null) ?? hostNameOrAddress);
			}
			string text = "MSSQLSvc/" + str;
			if (!string.IsNullOrWhiteSpace(portOrInstanceName))
			{
				text = text + ":" + portOrInstanceName;
			}
			return Encoding.UTF8.GetBytes(text);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00091294 File Offset: 0x0008F494
		private SNITCPHandle CreateTcpHandle(DataSource details, long timerExpire, object callbackObject, bool parallel)
		{
			string serverName = details.ServerName;
			if (string.IsNullOrWhiteSpace(serverName))
			{
				SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 0U, 25U, string.Empty);
				return null;
			}
			int port = -1;
			bool flag = details.ConnectionProtocol == DataSource.Protocol.Admin;
			if (details.IsSsrpRequired)
			{
				try
				{
					port = (flag ? SSRP.GetDacPortByInstanceName(serverName, details.InstanceName) : SSRP.GetPortByInstanceName(serverName, details.InstanceName));
					goto IL_98;
				}
				catch (SocketException sniException)
				{
					SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.TCP_PROV, 25U, sniException);
					return null;
				}
			}
			if (details.Port != -1)
			{
				port = details.Port;
			}
			else
			{
				port = (flag ? 1434 : 1433);
			}
			IL_98:
			return new SNITCPHandle(serverName, port, timerExpire, callbackObject, parallel);
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00091358 File Offset: 0x0008F558
		private SNINpHandle CreateNpHandle(DataSource details, long timerExpire, object callbackObject, bool parallel)
		{
			if (parallel)
			{
				SNICommon.ReportSNIError(SNIProviders.NP_PROV, 0U, 49U, string.Empty);
				return null;
			}
			return new SNINpHandle(details.PipeHostName, details.PipeName, timerExpire, callbackObject);
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x00091382 File Offset: 0x0008F582
		public uint ReadAsync(SNIHandle handle, out SNIPacket packet)
		{
			packet = null;
			return handle.ReceiveAsync(ref packet);
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x0009138E File Offset: 0x0008F58E
		public void PacketSetData(SNIPacket packet, byte[] data, int length)
		{
			packet.SetData(data, length);
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00091398 File Offset: 0x0008F598
		public void PacketRelease(SNIPacket packet)
		{
			packet.Release();
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x000913A0 File Offset: 0x0008F5A0
		public uint CheckConnection(SNIHandle handle)
		{
			return handle.CheckConnection();
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x000913A8 File Offset: 0x0008F5A8
		public SNIError GetLastError()
		{
			return SNILoadHandle.SingletonInstance.LastError;
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x000913B4 File Offset: 0x0008F5B4
		private string GetLocalDBDataSource(string fullServerName, out bool error)
		{
			string result = null;
			bool flag;
			string localDBInstance = DataSource.GetLocalDBInstance(fullServerName, out flag);
			if (flag)
			{
				error = true;
				return null;
			}
			if (!string.IsNullOrEmpty(localDBInstance))
			{
				result = LocalDB.GetLocalDBConnectionString(localDBInstance);
				if (fullServerName == null)
				{
					error = true;
					return null;
				}
			}
			error = false;
			return result;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x00003D93 File Offset: 0x00001F93
		public SNIProxy()
		{
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000913EF File Offset: 0x0008F5EF
		// Note: this type is marked as 'beforefieldinit'.
		static SNIProxy()
		{
		}

		// Token: 0x04001546 RID: 5446
		private const int DefaultSqlServerPort = 1433;

		// Token: 0x04001547 RID: 5447
		private const int DefaultSqlServerDacPort = 1434;

		// Token: 0x04001548 RID: 5448
		private const string SqlServerSpnHeader = "MSSQLSvc";

		// Token: 0x04001549 RID: 5449
		public static readonly SNIProxy Singleton = new SNIProxy();

		// Token: 0x0200029B RID: 667
		internal class SspiClientContextResult
		{
			// Token: 0x06001EC4 RID: 7876 RVA: 0x00003D93 File Offset: 0x00001F93
			public SspiClientContextResult()
			{
			}

			// Token: 0x0400154A RID: 5450
			internal const uint OK = 0U;

			// Token: 0x0400154B RID: 5451
			internal const uint Failed = 1U;

			// Token: 0x0400154C RID: 5452
			internal const uint KerberosTicketMissing = 2U;
		}
	}
}
