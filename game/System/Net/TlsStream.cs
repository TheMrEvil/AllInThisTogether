using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	// Token: 0x0200056F RID: 1391
	internal class TlsStream : NetworkStream
	{
		// Token: 0x06002CF5 RID: 11509 RVA: 0x0009A1D6 File Offset: 0x000983D6
		public TlsStream(NetworkStream stream, Socket socket, string host, X509CertificateCollection clientCertificates) : base(socket)
		{
			this._sslStream = new SslStream(stream, false, ServicePointManager.ServerCertificateValidationCallback);
			this._host = host;
			this._clientCertificates = clientCertificates;
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x0009A200 File Offset: 0x00098400
		public void AuthenticateAsClient()
		{
			this._sslStream.AuthenticateAsClient(this._host, this._clientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, ServicePointManager.CheckCertificateRevocationList);
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x0009A223 File Offset: 0x00098423
		public IAsyncResult BeginAuthenticateAsClient(AsyncCallback asyncCallback, object state)
		{
			return this._sslStream.BeginAuthenticateAsClient(this._host, this._clientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, ServicePointManager.CheckCertificateRevocationList, asyncCallback, state);
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x0009A248 File Offset: 0x00098448
		public void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			this._sslStream.EndAuthenticateAsClient(asyncResult);
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x0009A256 File Offset: 0x00098456
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this._sslStream.BeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x0009A26A File Offset: 0x0009846A
		public override void EndWrite(IAsyncResult result)
		{
			this._sslStream.EndWrite(result);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x0009A278 File Offset: 0x00098478
		public override void Write(byte[] buffer, int offset, int size)
		{
			this._sslStream.Write(buffer, offset, size);
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x0009A288 File Offset: 0x00098488
		public override int Read(byte[] buffer, int offset, int size)
		{
			return this._sslStream.Read(buffer, offset, size);
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x0009A298 File Offset: 0x00098498
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._sslStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x0009A2AC File Offset: 0x000984AC
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._sslStream.EndRead(asyncResult);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x0009A2BA File Offset: 0x000984BA
		public override void Close()
		{
			base.Close();
			if (this._sslStream != null)
			{
				this._sslStream.Close();
			}
		}

		// Token: 0x0400186F RID: 6255
		private SslStream _sslStream;

		// Token: 0x04001870 RID: 6256
		private string _host;

		// Token: 0x04001871 RID: 6257
		private X509CertificateCollection _clientCertificates;
	}
}
