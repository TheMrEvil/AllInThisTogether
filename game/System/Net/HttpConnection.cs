using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000685 RID: 1669
	internal sealed class HttpConnection
	{
		// Token: 0x06003492 RID: 13458 RVA: 0x000B7B48 File Offset: 0x000B5D48
		public HttpConnection(Socket sock, EndPointListener epl, bool secure, X509Certificate cert)
		{
			this.sock = sock;
			this.epl = epl;
			this.secure = secure;
			this.cert = cert;
			if (!secure)
			{
				this.stream = new NetworkStream(sock, false);
			}
			else
			{
				this.ssl_stream = epl.Listener.CreateSslStream(new NetworkStream(sock, false), false, delegate(object t, X509Certificate c, X509Chain ch, SslPolicyErrors e)
				{
					if (c == null)
					{
						return true;
					}
					X509Certificate2 x509Certificate = c as X509Certificate2;
					if (x509Certificate == null)
					{
						x509Certificate = new X509Certificate2(c.GetRawCertData());
					}
					this.client_cert = x509Certificate;
					this.client_cert_errors = new int[]
					{
						(int)e
					};
					return true;
				});
				this.stream = this.ssl_stream;
			}
			this.timer = new Timer(new TimerCallback(this.OnTimeout), null, -1, -1);
			if (this.ssl_stream != null)
			{
				this.ssl_stream.AuthenticateAsServer(cert, true, (SslProtocols)ServicePointManager.SecurityProtocol, false);
			}
			this.Init();
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x000B7C02 File Offset: 0x000B5E02
		internal SslStream SslStream
		{
			get
			{
				return this.ssl_stream;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x000B7C0A File Offset: 0x000B5E0A
		internal int[] ClientCertificateErrors
		{
			get
			{
				return this.client_cert_errors;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000B7C12 File Offset: 0x000B5E12
		internal X509Certificate2 ClientCertificate
		{
			get
			{
				return this.client_cert;
			}
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x000B7C1C File Offset: 0x000B5E1C
		private void Init()
		{
			this.context_bound = false;
			this.i_stream = null;
			this.o_stream = null;
			this.prefix = null;
			this.chunked = false;
			this.ms = new MemoryStream();
			this.position = 0;
			this.input_state = HttpConnection.InputState.RequestLine;
			this.line_state = HttpConnection.LineState.None;
			this.context = new HttpListenerContext(this);
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x000B7C78 File Offset: 0x000B5E78
		public bool IsClosed
		{
			get
			{
				return this.sock == null;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x000B7C83 File Offset: 0x000B5E83
		public int Reuses
		{
			get
			{
				return this.reuses;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x000B7C8B File Offset: 0x000B5E8B
		public IPEndPoint LocalEndPoint
		{
			get
			{
				if (this.local_ep != null)
				{
					return this.local_ep;
				}
				this.local_ep = (IPEndPoint)this.sock.LocalEndPoint;
				return this.local_ep;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600349A RID: 13466 RVA: 0x000B7CB8 File Offset: 0x000B5EB8
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return (IPEndPoint)this.sock.RemoteEndPoint;
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x000B7CCA File Offset: 0x000B5ECA
		public bool IsSecure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600349C RID: 13468 RVA: 0x000B7CD2 File Offset: 0x000B5ED2
		// (set) Token: 0x0600349D RID: 13469 RVA: 0x000B7CDA File Offset: 0x000B5EDA
		public ListenerPrefix Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000B7CE3 File Offset: 0x000B5EE3
		private void OnTimeout(object unused)
		{
			this.CloseSocket();
			this.Unbind();
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000B7CF4 File Offset: 0x000B5EF4
		public void BeginReadRequest()
		{
			if (this.buffer == null)
			{
				this.buffer = new byte[8192];
			}
			try
			{
				if (this.reuses == 1)
				{
					this.s_timeout = 15000;
				}
				this.timer.Change(this.s_timeout, -1);
				this.stream.BeginRead(this.buffer, 0, 8192, HttpConnection.onread_cb, this);
			}
			catch
			{
				this.timer.Change(-1, -1);
				this.CloseSocket();
				this.Unbind();
			}
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000B7D90 File Offset: 0x000B5F90
		public RequestStream GetRequestStream(bool chunked, long contentlength)
		{
			if (this.i_stream == null)
			{
				byte[] array = this.ms.GetBuffer();
				int num = (int)this.ms.Length;
				this.ms = null;
				if (chunked)
				{
					this.chunked = true;
					this.context.Response.SendChunked = true;
					this.i_stream = new ChunkedInputStream(this.context, this.stream, array, this.position, num - this.position);
				}
				else
				{
					this.i_stream = new RequestStream(this.stream, array, this.position, num - this.position, contentlength);
				}
			}
			return this.i_stream;
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000B7E34 File Offset: 0x000B6034
		public ResponseStream GetResponseStream()
		{
			if (this.o_stream == null)
			{
				HttpListener listener = this.context.Listener;
				if (listener == null)
				{
					return new ResponseStream(this.stream, this.context.Response, true);
				}
				this.o_stream = new ResponseStream(this.stream, this.context.Response, listener.IgnoreWriteExceptions);
			}
			return this.o_stream;
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000B7E98 File Offset: 0x000B6098
		private static void OnRead(IAsyncResult ares)
		{
			((HttpConnection)ares.AsyncState).OnReadInternal(ares);
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000B7EAC File Offset: 0x000B60AC
		private void OnReadInternal(IAsyncResult ares)
		{
			this.timer.Change(-1, -1);
			int num = -1;
			try
			{
				num = this.stream.EndRead(ares);
				this.ms.Write(this.buffer, 0, num);
				if (this.ms.Length > 32768L)
				{
					this.SendError("Bad request", 400);
					this.Close(true);
					return;
				}
			}
			catch
			{
				if (this.ms != null && this.ms.Length > 0L)
				{
					this.SendError();
				}
				if (this.sock != null)
				{
					this.CloseSocket();
					this.Unbind();
				}
				return;
			}
			if (num == 0)
			{
				this.CloseSocket();
				this.Unbind();
				return;
			}
			if (this.ProcessInput(this.ms))
			{
				if (!this.context.HaveError && !this.context.Request.FinishInitialization())
				{
					this.Close(true);
					return;
				}
				if (this.context.HaveError)
				{
					this.SendError();
					this.Close(true);
					return;
				}
				if (!this.epl.BindContext(this.context))
				{
					this.SendError("Invalid host", 400);
					this.Close(true);
					return;
				}
				HttpListener listener = this.context.Listener;
				if (this.last_listener != listener)
				{
					this.RemoveConnection();
					listener.AddConnection(this);
					this.last_listener = listener;
				}
				this.context_bound = true;
				listener.RegisterContext(this.context);
				return;
			}
			else
			{
				this.stream.BeginRead(this.buffer, 0, 8192, HttpConnection.onread_cb, this);
			}
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000B804C File Offset: 0x000B624C
		private void RemoveConnection()
		{
			if (this.last_listener == null)
			{
				this.epl.RemoveConnection(this);
				return;
			}
			this.last_listener.RemoveConnection(this);
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000B8070 File Offset: 0x000B6270
		private bool ProcessInput(MemoryStream ms)
		{
			byte[] array = ms.GetBuffer();
			int num = (int)ms.Length;
			int num2 = 0;
			while (!this.context.HaveError)
			{
				if (this.position < num)
				{
					string text;
					try
					{
						text = this.ReadLine(array, this.position, num - this.position, ref num2);
						this.position += num2;
					}
					catch
					{
						this.context.ErrorMessage = "Bad request";
						this.context.ErrorStatus = 400;
						return true;
					}
					if (text == null)
					{
						goto IL_10D;
					}
					if (text == "")
					{
						if (this.input_state != HttpConnection.InputState.RequestLine)
						{
							this.current_line = null;
							ms = null;
							return true;
						}
						continue;
					}
					else
					{
						if (this.input_state == HttpConnection.InputState.RequestLine)
						{
							this.context.Request.SetRequestLine(text);
							this.input_state = HttpConnection.InputState.Headers;
							continue;
						}
						try
						{
							this.context.Request.AddHeader(text);
							continue;
						}
						catch (Exception ex)
						{
							this.context.ErrorMessage = ex.Message;
							this.context.ErrorStatus = 400;
							return true;
						}
						goto IL_10D;
					}
					bool result;
					return result;
				}
				IL_10D:
				if (num2 == num)
				{
					ms.SetLength(0L);
					this.position = 0;
				}
				return false;
			}
			return true;
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000B81C0 File Offset: 0x000B63C0
		private string ReadLine(byte[] buffer, int offset, int len, ref int used)
		{
			if (this.current_line == null)
			{
				this.current_line = new StringBuilder(128);
			}
			int num = offset + len;
			used = 0;
			int num2 = offset;
			while (num2 < num && this.line_state != HttpConnection.LineState.LF)
			{
				used++;
				byte b = buffer[num2];
				if (b == 13)
				{
					this.line_state = HttpConnection.LineState.CR;
				}
				else if (b == 10)
				{
					this.line_state = HttpConnection.LineState.LF;
				}
				else
				{
					this.current_line.Append((char)b);
				}
				num2++;
			}
			string result = null;
			if (this.line_state == HttpConnection.LineState.LF)
			{
				this.line_state = HttpConnection.LineState.None;
				result = this.current_line.ToString();
				this.current_line.Length = 0;
			}
			return result;
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000B8264 File Offset: 0x000B6464
		public void SendError(string msg, int status)
		{
			try
			{
				HttpListenerResponse response = this.context.Response;
				response.StatusCode = status;
				response.ContentType = "text/html";
				string arg = HttpStatusDescription.Get(status);
				string s;
				if (msg != null)
				{
					s = string.Format("<h1>{0} ({1})</h1>", arg, msg);
				}
				else
				{
					s = string.Format("<h1>{0}</h1>", arg);
				}
				byte[] bytes = this.context.Response.ContentEncoding.GetBytes(s);
				response.Close(bytes, false);
			}
			catch
			{
			}
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000B82E8 File Offset: 0x000B64E8
		public void SendError()
		{
			this.SendError(this.context.ErrorMessage, this.context.ErrorStatus);
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000B8306 File Offset: 0x000B6506
		private void Unbind()
		{
			if (this.context_bound)
			{
				this.epl.UnbindContext(this.context);
				this.context_bound = false;
			}
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000B8328 File Offset: 0x000B6528
		public void Close()
		{
			this.Close(false);
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000B8334 File Offset: 0x000B6534
		private void CloseSocket()
		{
			if (this.sock == null)
			{
				return;
			}
			try
			{
				this.sock.Close();
			}
			catch
			{
			}
			finally
			{
				this.sock = null;
			}
			this.RemoveConnection();
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000B8388 File Offset: 0x000B6588
		internal void Close(bool force_close)
		{
			if (this.sock != null)
			{
				Stream responseStream = this.GetResponseStream();
				if (responseStream != null)
				{
					responseStream.Close();
				}
				this.o_stream = null;
			}
			if (this.sock == null)
			{
				return;
			}
			force_close |= !this.context.Request.KeepAlive;
			if (!force_close)
			{
				force_close = (this.context.Response.Headers["connection"] == "close");
			}
			if (force_close || !this.context.Request.FlushInput())
			{
				Socket socket = this.sock;
				this.sock = null;
				try
				{
					if (socket != null)
					{
						socket.Shutdown(SocketShutdown.Both);
					}
				}
				catch
				{
				}
				finally
				{
					if (socket != null)
					{
						socket.Close();
					}
				}
				this.Unbind();
				this.RemoveConnection();
				return;
			}
			if (this.chunked && !this.context.Response.ForceCloseChunked)
			{
				this.reuses++;
				this.Unbind();
				this.Init();
				this.BeginReadRequest();
				return;
			}
			this.reuses++;
			this.Unbind();
			this.Init();
			this.BeginReadRequest();
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000B84C0 File Offset: 0x000B66C0
		// Note: this type is marked as 'beforefieldinit'.
		static HttpConnection()
		{
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000B84D4 File Offset: 0x000B66D4
		[CompilerGenerated]
		private bool <.ctor>b__24_0(object t, X509Certificate c, X509Chain ch, SslPolicyErrors e)
		{
			if (c == null)
			{
				return true;
			}
			X509Certificate2 x509Certificate = c as X509Certificate2;
			if (x509Certificate == null)
			{
				x509Certificate = new X509Certificate2(c.GetRawCertData());
			}
			this.client_cert = x509Certificate;
			this.client_cert_errors = new int[]
			{
				(int)e
			};
			return true;
		}

		// Token: 0x04001EAA RID: 7850
		private static AsyncCallback onread_cb = new AsyncCallback(HttpConnection.OnRead);

		// Token: 0x04001EAB RID: 7851
		private const int BufferSize = 8192;

		// Token: 0x04001EAC RID: 7852
		private Socket sock;

		// Token: 0x04001EAD RID: 7853
		private Stream stream;

		// Token: 0x04001EAE RID: 7854
		private EndPointListener epl;

		// Token: 0x04001EAF RID: 7855
		private MemoryStream ms;

		// Token: 0x04001EB0 RID: 7856
		private byte[] buffer;

		// Token: 0x04001EB1 RID: 7857
		private HttpListenerContext context;

		// Token: 0x04001EB2 RID: 7858
		private StringBuilder current_line;

		// Token: 0x04001EB3 RID: 7859
		private ListenerPrefix prefix;

		// Token: 0x04001EB4 RID: 7860
		private RequestStream i_stream;

		// Token: 0x04001EB5 RID: 7861
		private ResponseStream o_stream;

		// Token: 0x04001EB6 RID: 7862
		private bool chunked;

		// Token: 0x04001EB7 RID: 7863
		private int reuses;

		// Token: 0x04001EB8 RID: 7864
		private bool context_bound;

		// Token: 0x04001EB9 RID: 7865
		private bool secure;

		// Token: 0x04001EBA RID: 7866
		private X509Certificate cert;

		// Token: 0x04001EBB RID: 7867
		private int s_timeout = 90000;

		// Token: 0x04001EBC RID: 7868
		private Timer timer;

		// Token: 0x04001EBD RID: 7869
		private IPEndPoint local_ep;

		// Token: 0x04001EBE RID: 7870
		private HttpListener last_listener;

		// Token: 0x04001EBF RID: 7871
		private int[] client_cert_errors;

		// Token: 0x04001EC0 RID: 7872
		private X509Certificate2 client_cert;

		// Token: 0x04001EC1 RID: 7873
		private SslStream ssl_stream;

		// Token: 0x04001EC2 RID: 7874
		private HttpConnection.InputState input_state;

		// Token: 0x04001EC3 RID: 7875
		private HttpConnection.LineState line_state;

		// Token: 0x04001EC4 RID: 7876
		private int position;

		// Token: 0x02000686 RID: 1670
		private enum InputState
		{
			// Token: 0x04001EC6 RID: 7878
			RequestLine,
			// Token: 0x04001EC7 RID: 7879
			Headers
		}

		// Token: 0x02000687 RID: 1671
		private enum LineState
		{
			// Token: 0x04001EC9 RID: 7881
			None,
			// Token: 0x04001ECA RID: 7882
			CR,
			// Token: 0x04001ECB RID: 7883
			LF
		}
	}
}
