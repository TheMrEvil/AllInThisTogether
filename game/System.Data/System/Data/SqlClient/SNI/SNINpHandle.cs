using System;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000296 RID: 662
	internal class SNINpHandle : SNIHandle
	{
		// Token: 0x06001E7D RID: 7805 RVA: 0x000903E8 File Offset: 0x0008E5E8
		public SNINpHandle(string serverName, string pipeName, long timerExpire, object callbackObject)
		{
			this._targetServer = serverName;
			this._callbackObject = callbackObject;
			try
			{
				this._pipeStream = new NamedPipeClientStream(serverName, pipeName, PipeDirection.InOut, PipeOptions.WriteThrough | PipeOptions.Asynchronous);
				if (9223372036854775807L == timerExpire)
				{
					this._pipeStream.Connect(-1);
				}
				else
				{
					TimeSpan timeSpan = DateTime.FromFileTime(timerExpire) - DateTime.Now;
					timeSpan = ((timeSpan.Ticks < 0L) ? TimeSpan.FromTicks(0L) : timeSpan);
					this._pipeStream.Connect((int)timeSpan.TotalMilliseconds);
				}
			}
			catch (TimeoutException sniException)
			{
				SNICommon.ReportSNIError(SNIProviders.NP_PROV, 40U, sniException);
				this._status = 1U;
				return;
			}
			catch (IOException sniException2)
			{
				SNICommon.ReportSNIError(SNIProviders.NP_PROV, 40U, sniException2);
				this._status = 1U;
				return;
			}
			if (!this._pipeStream.IsConnected || !this._pipeStream.CanWrite || !this._pipeStream.CanRead)
			{
				SNICommon.ReportSNIError(SNIProviders.NP_PROV, 0U, 40U, string.Empty);
				this._status = 1U;
				return;
			}
			this._sslOverTdsStream = new SslOverTdsStream(this._pipeStream);
			this._sslStream = new SslStream(this._sslOverTdsStream, true, new RemoteCertificateValidationCallback(this.ValidateServerCertificate), null);
			this._stream = this._pipeStream;
			this._status = 0U;
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x00090564 File Offset: 0x0008E764
		public override Guid ConnectionId
		{
			get
			{
				return this._connectionId;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x0009056C File Offset: 0x0008E76C
		public override uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00090574 File Offset: 0x0008E774
		public override uint CheckConnection()
		{
			if (!this._stream.CanWrite || !this._stream.CanRead)
			{
				return 1U;
			}
			return 0U;
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00090594 File Offset: 0x0008E794
		public override void Dispose()
		{
			lock (this)
			{
				if (this._sslOverTdsStream != null)
				{
					this._sslOverTdsStream.Dispose();
					this._sslOverTdsStream = null;
				}
				if (this._sslStream != null)
				{
					this._sslStream.Dispose();
					this._sslStream = null;
				}
				if (this._pipeStream != null)
				{
					this._pipeStream.Dispose();
					this._pipeStream = null;
				}
				this._stream = null;
			}
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00090620 File Offset: 0x0008E820
		public override uint Receive(out SNIPacket packet, int timeout)
		{
			uint result;
			lock (this)
			{
				packet = null;
				try
				{
					packet = new SNIPacket(this._bufferSize);
					packet.ReadFromStream(this._stream);
					if (packet.Length == 0)
					{
						Win32Exception ex = new Win32Exception();
						return this.ReportErrorAndReleasePacket(packet, (uint)ex.NativeErrorCode, 0U, ex.Message);
					}
				}
				catch (ObjectDisposedException sniException)
				{
					return this.ReportErrorAndReleasePacket(packet, sniException);
				}
				catch (IOException sniException2)
				{
					return this.ReportErrorAndReleasePacket(packet, sniException2);
				}
				result = 0U;
			}
			return result;
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x000906D4 File Offset: 0x0008E8D4
		public override uint ReceiveAsync(ref SNIPacket packet)
		{
			packet = new SNIPacket(this._bufferSize);
			uint result;
			try
			{
				packet.ReadFromStreamAsync(this._stream, this._receiveCallback);
				result = 997U;
			}
			catch (ObjectDisposedException sniException)
			{
				result = this.ReportErrorAndReleasePacket(packet, sniException);
			}
			catch (IOException sniException2)
			{
				result = this.ReportErrorAndReleasePacket(packet, sniException2);
			}
			return result;
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00090740 File Offset: 0x0008E940
		public override uint Send(SNIPacket packet)
		{
			uint result;
			lock (this)
			{
				try
				{
					packet.WriteToStream(this._stream);
					result = 0U;
				}
				catch (ObjectDisposedException sniException)
				{
					result = this.ReportErrorAndReleasePacket(packet, sniException);
				}
				catch (IOException sniException2)
				{
					result = this.ReportErrorAndReleasePacket(packet, sniException2);
				}
			}
			return result;
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x000907B8 File Offset: 0x0008E9B8
		public override uint SendAsync(SNIPacket packet, bool disposePacketAfterSendAsync, SNIAsyncCallback callback = null)
		{
			SNIAsyncCallback callback2 = callback ?? this._sendCallback;
			packet.WriteToStreamAsync(this._stream, callback2, SNIProviders.NP_PROV, disposePacketAfterSendAsync);
			return 997U;
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x000907E5 File Offset: 0x0008E9E5
		public override void SetAsyncCallbacks(SNIAsyncCallback receiveCallback, SNIAsyncCallback sendCallback)
		{
			this._receiveCallback = receiveCallback;
			this._sendCallback = sendCallback;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x000907F8 File Offset: 0x0008E9F8
		public override uint EnableSsl(uint options)
		{
			this._validateCert = ((options & 1U) > 0U);
			try
			{
				this._sslStream.AuthenticateAsClientAsync(this._targetServer).GetAwaiter().GetResult();
				this._sslOverTdsStream.FinishHandshake();
			}
			catch (AuthenticationException sniException)
			{
				return SNICommon.ReportSNIError(SNIProviders.NP_PROV, 35U, sniException);
			}
			catch (InvalidOperationException sniException2)
			{
				return SNICommon.ReportSNIError(SNIProviders.NP_PROV, 35U, sniException2);
			}
			this._stream = this._sslStream;
			return 0U;
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x00090884 File Offset: 0x0008EA84
		public override void DisableSsl()
		{
			this._sslStream.Dispose();
			this._sslStream = null;
			this._sslOverTdsStream.Dispose();
			this._sslOverTdsStream = null;
			this._stream = this._pipeStream;
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x000908B6 File Offset: 0x0008EAB6
		private bool ValidateServerCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
		{
			return !this._validateCert || SNICommon.ValidateSslServerCertificate(this._targetServer, sender, cert, chain, policyErrors);
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x000908D2 File Offset: 0x0008EAD2
		public override void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x000908DB File Offset: 0x0008EADB
		private uint ReportErrorAndReleasePacket(SNIPacket packet, Exception sniException)
		{
			if (packet != null)
			{
				packet.Release();
			}
			return SNICommon.ReportSNIError(SNIProviders.NP_PROV, 35U, sniException);
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x000908EF File Offset: 0x0008EAEF
		private uint ReportErrorAndReleasePacket(SNIPacket packet, uint nativeError, uint sniError, string errorMessage)
		{
			if (packet != null)
			{
				packet.Release();
			}
			return SNICommon.ReportSNIError(SNIProviders.NP_PROV, nativeError, sniError, errorMessage);
		}

		// Token: 0x04001524 RID: 5412
		internal const string DefaultPipePath = "sql\\query";

		// Token: 0x04001525 RID: 5413
		private const int MAX_PIPE_INSTANCES = 255;

		// Token: 0x04001526 RID: 5414
		private readonly string _targetServer;

		// Token: 0x04001527 RID: 5415
		private readonly object _callbackObject;

		// Token: 0x04001528 RID: 5416
		private Stream _stream;

		// Token: 0x04001529 RID: 5417
		private NamedPipeClientStream _pipeStream;

		// Token: 0x0400152A RID: 5418
		private SslOverTdsStream _sslOverTdsStream;

		// Token: 0x0400152B RID: 5419
		private SslStream _sslStream;

		// Token: 0x0400152C RID: 5420
		private SNIAsyncCallback _receiveCallback;

		// Token: 0x0400152D RID: 5421
		private SNIAsyncCallback _sendCallback;

		// Token: 0x0400152E RID: 5422
		private bool _validateCert = true;

		// Token: 0x0400152F RID: 5423
		private readonly uint _status = uint.MaxValue;

		// Token: 0x04001530 RID: 5424
		private int _bufferSize = 4096;

		// Token: 0x04001531 RID: 5425
		private readonly Guid _connectionId = Guid.NewGuid();
	}
}
