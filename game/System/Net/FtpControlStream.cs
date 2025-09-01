using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net
{
	// Token: 0x02000587 RID: 1415
	internal class FtpControlStream : CommandStream
	{
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x0009CDF5 File Offset: 0x0009AFF5
		// (set) Token: 0x06002DC7 RID: 11719 RVA: 0x0009CE1E File Offset: 0x0009B01E
		internal NetworkCredential Credentials
		{
			get
			{
				if (this._credentials != null && this._credentials.IsAlive)
				{
					return (NetworkCredential)this._credentials.Target;
				}
				return null;
			}
			set
			{
				if (this._credentials == null)
				{
					this._credentials = new WeakReference(null);
				}
				this._credentials.Target = value;
			}
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x0009CE40 File Offset: 0x0009B040
		internal FtpControlStream(TcpClient client) : base(client)
		{
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x0009CE5C File Offset: 0x0009B05C
		internal void AbortConnect()
		{
			Socket dataSocket = this._dataSocket;
			if (dataSocket != null)
			{
				try
				{
					dataSocket.Close();
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x0009CE90 File Offset: 0x0009B090
		private static void AcceptCallback(IAsyncResult asyncResult)
		{
			FtpControlStream ftpControlStream = (FtpControlStream)asyncResult.AsyncState;
			Socket dataSocket = ftpControlStream._dataSocket;
			try
			{
				ftpControlStream._dataSocket = dataSocket.EndAccept(asyncResult);
				if (!ftpControlStream.ServerAddress.Equals(((IPEndPoint)ftpControlStream._dataSocket.RemoteEndPoint).Address))
				{
					ftpControlStream._dataSocket.Close();
					throw new WebException("The data connection was made from an address that is different than the address to which the FTP connection was made.", WebExceptionStatus.ProtocolError);
				}
				ftpControlStream.ContinueCommandPipeline();
			}
			catch (Exception obj)
			{
				ftpControlStream.CloseSocket();
				ftpControlStream.InvokeRequestCallback(obj);
			}
			finally
			{
				dataSocket.Close();
			}
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x0009CF34 File Offset: 0x0009B134
		private static void ConnectCallback(IAsyncResult asyncResult)
		{
			FtpControlStream ftpControlStream = (FtpControlStream)asyncResult.AsyncState;
			try
			{
				ftpControlStream._dataSocket.EndConnect(asyncResult);
				ftpControlStream.ContinueCommandPipeline();
			}
			catch (Exception obj)
			{
				ftpControlStream.CloseSocket();
				ftpControlStream.InvokeRequestCallback(obj);
			}
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x0009CF84 File Offset: 0x0009B184
		private static void SSLHandshakeCallback(IAsyncResult asyncResult)
		{
			FtpControlStream ftpControlStream = (FtpControlStream)asyncResult.AsyncState;
			try
			{
				ftpControlStream._tlsStream.EndAuthenticateAsClient(asyncResult);
				ftpControlStream.ContinueCommandPipeline();
			}
			catch (Exception obj)
			{
				ftpControlStream.CloseSocket();
				ftpControlStream.InvokeRequestCallback(obj);
			}
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x0009CFD4 File Offset: 0x0009B1D4
		private CommandStream.PipelineInstruction QueueOrCreateFtpDataStream(ref Stream stream)
		{
			if (this._dataSocket == null)
			{
				throw new InternalException();
			}
			if (this._tlsStream != null)
			{
				stream = new FtpDataStream(this._tlsStream, (FtpWebRequest)this._request, this.IsFtpDataStreamWriteable());
				this._tlsStream = null;
				return CommandStream.PipelineInstruction.GiveStream;
			}
			NetworkStream networkStream = new NetworkStream(this._dataSocket, true);
			if (base.UsingSecureStream)
			{
				FtpWebRequest ftpWebRequest = (FtpWebRequest)this._request;
				TlsStream tlsStream = new TlsStream(networkStream, this._dataSocket, ftpWebRequest.RequestUri.Host, ftpWebRequest.ClientCertificates);
				networkStream = tlsStream;
				if (this._isAsync)
				{
					this._tlsStream = tlsStream;
					tlsStream.BeginAuthenticateAsClient(FtpControlStream.s_SSLHandshakeCallback, this);
					return CommandStream.PipelineInstruction.Pause;
				}
				tlsStream.AuthenticateAsClient();
			}
			stream = new FtpDataStream(networkStream, (FtpWebRequest)this._request, this.IsFtpDataStreamWriteable());
			return CommandStream.PipelineInstruction.GiveStream;
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x0009D0A0 File Offset: 0x0009B2A0
		protected override void ClearState()
		{
			this._contentLength = -1L;
			this._lastModified = DateTime.MinValue;
			this._responseUri = null;
			this._dataHandshakeStarted = false;
			this.StatusCode = FtpStatusCode.Undefined;
			this.StatusLine = null;
			this._dataSocket = null;
			this._passiveEndPoint = null;
			this._tlsStream = null;
			base.ClearState();
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x0009D0F8 File Offset: 0x0009B2F8
		protected override CommandStream.PipelineInstruction PipelineCallback(CommandStream.PipelineEntry entry, ResponseDescription response, bool timeout, ref Stream stream)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("Command:{0} Description:{1}", new object[]
				{
					(entry != null) ? entry.Command : null,
					(response != null) ? response.StatusDescription : null
				}), "PipelineCallback");
			}
			if (response == null)
			{
				return CommandStream.PipelineInstruction.Abort;
			}
			FtpStatusCode status = (FtpStatusCode)response.Status;
			if (status != FtpStatusCode.ClosingControl)
			{
				this.StatusCode = status;
				this.StatusLine = response.StatusDescription;
			}
			if (response.InvalidStatusCode)
			{
				throw new WebException("The server returned a status code outside the valid range of 100-599.", WebExceptionStatus.ProtocolError);
			}
			if (this._index == -1)
			{
				if (status == FtpStatusCode.SendUserCommand)
				{
					this._bannerMessage = new StringBuilder();
					this._bannerMessage.Append(this.StatusLine);
					return CommandStream.PipelineInstruction.Advance;
				}
				if (status == FtpStatusCode.ServiceTemporarilyNotAvailable)
				{
					return CommandStream.PipelineInstruction.Reread;
				}
				throw base.GenerateException(status, response.StatusDescription, null);
			}
			else
			{
				if (entry.Command == "OPTS utf8 on\r\n")
				{
					if (response.PositiveCompletion)
					{
						base.Encoding = Encoding.UTF8;
					}
					else
					{
						base.Encoding = Encoding.Default;
					}
					return CommandStream.PipelineInstruction.Advance;
				}
				if (entry.Command.IndexOf("USER") != -1 && status == FtpStatusCode.LoggedInProceed)
				{
					this._loginState = FtpLoginState.LoggedIn;
					this._index++;
				}
				if (response.TransientFailure || response.PermanentFailure)
				{
					if (status == FtpStatusCode.ServiceNotAvailable)
					{
						base.MarkAsRecoverableFailure();
					}
					throw base.GenerateException(status, response.StatusDescription, null);
				}
				if (this._loginState != FtpLoginState.LoggedIn && entry.Command.IndexOf("PASS") != -1)
				{
					if (status != FtpStatusCode.NeedLoginAccount && status != FtpStatusCode.LoggedInProceed)
					{
						throw base.GenerateException(status, response.StatusDescription, null);
					}
					this._loginState = FtpLoginState.LoggedIn;
				}
				if (entry.HasFlag(CommandStream.PipelineEntryFlags.CreateDataConnection) && (response.PositiveCompletion || response.PositiveIntermediate))
				{
					bool flag;
					CommandStream.PipelineInstruction result = this.QueueOrCreateDataConection(entry, response, timeout, ref stream, out flag);
					if (!flag)
					{
						return result;
					}
				}
				if (status == FtpStatusCode.OpeningData || status == FtpStatusCode.DataAlreadyOpen)
				{
					if (this._dataSocket == null)
					{
						return CommandStream.PipelineInstruction.Abort;
					}
					if (!entry.HasFlag(CommandStream.PipelineEntryFlags.GiveDataStream))
					{
						this._abortReason = SR.Format("The status response ({0}) is not expected in response to '{1}' command.", status, entry.Command);
						return CommandStream.PipelineInstruction.Abort;
					}
					this.TryUpdateContentLength(response.StatusDescription);
					FtpWebRequest ftpWebRequest = (FtpWebRequest)this._request;
					if (ftpWebRequest.MethodInfo.ShouldParseForResponseUri)
					{
						this.TryUpdateResponseUri(response.StatusDescription, ftpWebRequest);
					}
					return this.QueueOrCreateFtpDataStream(ref stream);
				}
				else
				{
					if (status == FtpStatusCode.LoggedInProceed)
					{
						this._welcomeMessage.Append(this.StatusLine);
					}
					else if (status == FtpStatusCode.ClosingControl)
					{
						this._exitMessage.Append(response.StatusDescription);
						base.CloseSocket();
					}
					else if (status == FtpStatusCode.ServerWantsSecureSession)
					{
						if (!(base.NetworkStream is TlsStream))
						{
							FtpWebRequest ftpWebRequest2 = (FtpWebRequest)this._request;
							TlsStream tlsStream = new TlsStream(base.NetworkStream, base.Socket, ftpWebRequest2.RequestUri.Host, ftpWebRequest2.ClientCertificates);
							if (this._isAsync)
							{
								tlsStream.BeginAuthenticateAsClient(delegate(IAsyncResult ar)
								{
									try
									{
										tlsStream.EndAuthenticateAsClient(ar);
										this.NetworkStream = tlsStream;
										this.ContinueCommandPipeline();
									}
									catch (Exception obj)
									{
										this.CloseSocket();
										this.InvokeRequestCallback(obj);
									}
								}, null);
								return CommandStream.PipelineInstruction.Pause;
							}
							tlsStream.AuthenticateAsClient();
							base.NetworkStream = tlsStream;
						}
					}
					else if (status == FtpStatusCode.FileStatus)
					{
						FtpWebRequest ftpWebRequest3 = (FtpWebRequest)this._request;
						if (entry.Command.StartsWith("SIZE "))
						{
							this._contentLength = this.GetContentLengthFrom213Response(response.StatusDescription);
						}
						else if (entry.Command.StartsWith("MDTM "))
						{
							this._lastModified = this.GetLastModifiedFrom213Response(response.StatusDescription);
						}
					}
					else if (status == FtpStatusCode.PathnameCreated)
					{
						if (entry.Command == "PWD\r\n" && !entry.HasFlag(CommandStream.PipelineEntryFlags.UserCommand))
						{
							this._loginDirectory = this.GetLoginDirectory(response.StatusDescription);
						}
					}
					else if (entry.Command.IndexOf("CWD") != -1)
					{
						this._establishedServerDirectory = this._requestedServerDirectory;
					}
					if (response.PositiveIntermediate || (!base.UsingSecureStream && entry.Command == "AUTH TLS\r\n"))
					{
						return CommandStream.PipelineInstruction.Reread;
					}
					return CommandStream.PipelineInstruction.Advance;
				}
			}
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x0009D500 File Offset: 0x0009B700
		protected override CommandStream.PipelineEntry[] BuildCommandsList(WebRequest req)
		{
			bool flag = false;
			FtpWebRequest ftpWebRequest = (FtpWebRequest)req;
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "BuildCommandsList");
			}
			this._responseUri = ftpWebRequest.RequestUri;
			ArrayList arrayList = new ArrayList();
			if (ftpWebRequest.EnableSsl && !base.UsingSecureStream)
			{
				arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("AUTH", "TLS")));
				flag = true;
			}
			if (flag)
			{
				this._loginDirectory = null;
				this._establishedServerDirectory = null;
				this._requestedServerDirectory = null;
				this._currentTypeSetting = string.Empty;
				if (this._loginState == FtpLoginState.LoggedIn)
				{
					this._loginState = FtpLoginState.LoggedInButNeedsRelogin;
				}
			}
			if (this._loginState != FtpLoginState.LoggedIn)
			{
				this.Credentials = ftpWebRequest.Credentials.GetCredential(ftpWebRequest.RequestUri, "basic");
				this._welcomeMessage = new StringBuilder();
				this._exitMessage = new StringBuilder();
				string text = string.Empty;
				string text2 = string.Empty;
				if (this.Credentials != null)
				{
					text = this.Credentials.UserName;
					string domain = this.Credentials.Domain;
					if (!string.IsNullOrEmpty(domain))
					{
						text = domain + "\\" + text;
					}
					text2 = this.Credentials.Password;
				}
				if (text.Length == 0 && text2.Length == 0)
				{
					text = "anonymous";
					text2 = "anonymous@";
				}
				arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("USER", text)));
				arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("PASS", text2), CommandStream.PipelineEntryFlags.DontLogParameter));
				if (ftpWebRequest.EnableSsl && !base.UsingSecureStream)
				{
					arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("PBSZ", "0")));
					arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("PROT", "P")));
				}
				arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("OPTS", "utf8 on")));
				arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("PWD", null)));
			}
			FtpControlStream.GetPathOption pathOption = FtpControlStream.GetPathOption.Normal;
			if (ftpWebRequest.MethodInfo.HasFlag(FtpMethodFlags.DoesNotTakeParameter))
			{
				pathOption = FtpControlStream.GetPathOption.AssumeNoFilename;
			}
			else if (ftpWebRequest.MethodInfo.HasFlag(FtpMethodFlags.ParameterIsDirectory))
			{
				pathOption = FtpControlStream.GetPathOption.AssumeFilename;
			}
			string parameter;
			string text3;
			string text4;
			FtpControlStream.GetPathInfo(pathOption, ftpWebRequest.RequestUri, out parameter, out text3, out text4);
			if (text4.Length == 0 && ftpWebRequest.MethodInfo.HasFlag(FtpMethodFlags.TakesParameter))
			{
				throw new WebException("The requested URI is invalid for this FTP command.");
			}
			if (this._establishedServerDirectory != null && this._loginDirectory != null && this._establishedServerDirectory != this._loginDirectory)
			{
				arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("CWD", this._loginDirectory), CommandStream.PipelineEntryFlags.UserCommand));
				this._requestedServerDirectory = this._loginDirectory;
			}
			if (ftpWebRequest.MethodInfo.HasFlag(FtpMethodFlags.MustChangeWorkingDirectoryToPath) && text3.Length > 0)
			{
				arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("CWD", text3), CommandStream.PipelineEntryFlags.UserCommand));
				this._requestedServerDirectory = text3;
			}
			if (!ftpWebRequest.MethodInfo.IsCommandOnly)
			{
				string text5 = ftpWebRequest.UseBinary ? "I" : "A";
				if (this._currentTypeSetting != text5)
				{
					arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("TYPE", text5)));
					this._currentTypeSetting = text5;
				}
				if (ftpWebRequest.UsePassive)
				{
					string command = (base.ServerAddress.AddressFamily == AddressFamily.InterNetwork) ? "PASV" : "EPSV";
					arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand(command, null), CommandStream.PipelineEntryFlags.CreateDataConnection));
				}
				else
				{
					string command2 = (base.ServerAddress.AddressFamily == AddressFamily.InterNetwork) ? "PORT" : "EPRT";
					this.CreateFtpListenerSocket(ftpWebRequest);
					arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand(command2, this.GetPortCommandLine(ftpWebRequest))));
				}
				if (ftpWebRequest.ContentOffset > 0L)
				{
					arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("REST", ftpWebRequest.ContentOffset.ToString(CultureInfo.InvariantCulture))));
				}
			}
			CommandStream.PipelineEntryFlags pipelineEntryFlags = CommandStream.PipelineEntryFlags.UserCommand;
			if (!ftpWebRequest.MethodInfo.IsCommandOnly)
			{
				pipelineEntryFlags |= CommandStream.PipelineEntryFlags.GiveDataStream;
				if (!ftpWebRequest.UsePassive)
				{
					pipelineEntryFlags |= CommandStream.PipelineEntryFlags.CreateDataConnection;
				}
			}
			if (ftpWebRequest.MethodInfo.Operation == FtpOperation.Rename)
			{
				string str = (text3 == string.Empty) ? string.Empty : (text3 + "/");
				arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("RNFR", str + text4), pipelineEntryFlags));
				string parameter2;
				if (!string.IsNullOrEmpty(ftpWebRequest.RenameTo) && ftpWebRequest.RenameTo.StartsWith("/", StringComparison.OrdinalIgnoreCase))
				{
					parameter2 = ftpWebRequest.RenameTo;
				}
				else
				{
					parameter2 = str + ftpWebRequest.RenameTo;
				}
				arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("RNTO", parameter2), pipelineEntryFlags));
			}
			else if (ftpWebRequest.MethodInfo.HasFlag(FtpMethodFlags.DoesNotTakeParameter))
			{
				arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand(ftpWebRequest.Method, string.Empty), pipelineEntryFlags));
			}
			else if (ftpWebRequest.MethodInfo.HasFlag(FtpMethodFlags.MustChangeWorkingDirectoryToPath))
			{
				arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand(ftpWebRequest.Method, text4), pipelineEntryFlags));
			}
			else
			{
				arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand(ftpWebRequest.Method, parameter), pipelineEntryFlags));
			}
			arrayList.Add(new CommandStream.PipelineEntry(this.FormatFtpCommand("QUIT", null)));
			return (CommandStream.PipelineEntry[])arrayList.ToArray(typeof(CommandStream.PipelineEntry));
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x0009DA64 File Offset: 0x0009BC64
		private CommandStream.PipelineInstruction QueueOrCreateDataConection(CommandStream.PipelineEntry entry, ResponseDescription response, bool timeout, ref Stream stream, out bool isSocketReady)
		{
			isSocketReady = false;
			if (this._dataHandshakeStarted)
			{
				isSocketReady = true;
				return CommandStream.PipelineInstruction.Pause;
			}
			this._dataHandshakeStarted = true;
			bool flag = false;
			int num = -1;
			if (entry.Command == "PASV\r\n" || entry.Command == "EPSV\r\n")
			{
				if (!response.PositiveCompletion)
				{
					this._abortReason = SR.Format("The server failed the passive mode request with status response ({0}).", response.Status);
					return CommandStream.PipelineInstruction.Abort;
				}
				if (entry.Command == "PASV\r\n")
				{
					num = this.GetPortV4(response.StatusDescription);
				}
				else
				{
					num = this.GetPortV6(response.StatusDescription);
				}
				flag = true;
			}
			if (flag)
			{
				if (num == -1)
				{
					NetEventSource.Fail(this, "'port' not set.", "QueueOrCreateDataConection");
				}
				try
				{
					this._dataSocket = this.CreateFtpDataSocket((FtpWebRequest)this._request, base.Socket);
				}
				catch (ObjectDisposedException)
				{
					throw ExceptionHelper.RequestAbortedException;
				}
				IPEndPoint localEP = new IPEndPoint(((IPEndPoint)base.Socket.LocalEndPoint).Address, 0);
				this._dataSocket.Bind(localEP);
				this._passiveEndPoint = new IPEndPoint(base.ServerAddress, num);
			}
			CommandStream.PipelineInstruction result;
			if (this._passiveEndPoint != null)
			{
				IPEndPoint passiveEndPoint = this._passiveEndPoint;
				this._passiveEndPoint = null;
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "starting Connect()", "QueueOrCreateDataConection");
				}
				if (this._isAsync)
				{
					this._dataSocket.BeginConnect(passiveEndPoint, FtpControlStream.s_connectCallbackDelegate, this);
					result = CommandStream.PipelineInstruction.Pause;
				}
				else
				{
					this._dataSocket.Connect(passiveEndPoint);
					result = CommandStream.PipelineInstruction.Advance;
				}
			}
			else
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "starting Accept()", "QueueOrCreateDataConection");
				}
				if (this._isAsync)
				{
					this._dataSocket.BeginAccept(FtpControlStream.s_acceptCallbackDelegate, this);
					result = CommandStream.PipelineInstruction.Pause;
				}
				else
				{
					Socket dataSocket = this._dataSocket;
					try
					{
						this._dataSocket = this._dataSocket.Accept();
						if (!base.ServerAddress.Equals(((IPEndPoint)this._dataSocket.RemoteEndPoint).Address))
						{
							this._dataSocket.Close();
							throw new WebException("The data connection was made from an address that is different than the address to which the FTP connection was made.", WebExceptionStatus.ProtocolError);
						}
						isSocketReady = true;
						result = CommandStream.PipelineInstruction.Pause;
					}
					finally
					{
						dataSocket.Close();
					}
				}
			}
			return result;
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x0009DC98 File Offset: 0x0009BE98
		private static void GetPathInfo(FtpControlStream.GetPathOption pathOption, Uri uri, out string path, out string directory, out string filename)
		{
			path = uri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
			int num = path.LastIndexOf('/');
			if (pathOption == FtpControlStream.GetPathOption.AssumeFilename && num != -1 && num == path.Length - 1)
			{
				path = path.Substring(0, path.Length - 1);
				num = path.LastIndexOf('/');
			}
			if (pathOption == FtpControlStream.GetPathOption.AssumeNoFilename)
			{
				directory = path;
				filename = string.Empty;
			}
			else
			{
				directory = path.Substring(0, num + 1);
				filename = path.Substring(num + 1, path.Length - (num + 1));
			}
			if (directory.Length > 1 && directory[directory.Length - 1] == '/')
			{
				directory = directory.Substring(0, directory.Length - 1);
			}
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x0009DD54 File Offset: 0x0009BF54
		private string FormatAddress(IPAddress address, int Port)
		{
			byte[] addressBytes = address.GetAddressBytes();
			StringBuilder stringBuilder = new StringBuilder(32);
			foreach (byte value in addressBytes)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(',');
			}
			stringBuilder.Append(Port / 256);
			stringBuilder.Append(',');
			stringBuilder.Append(Port % 256);
			return stringBuilder.ToString();
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x0009DDC0 File Offset: 0x0009BFC0
		private string FormatAddressV6(IPAddress address, int port)
		{
			StringBuilder stringBuilder = new StringBuilder(43);
			string value = address.ToString();
			stringBuilder.Append("|2|");
			stringBuilder.Append(value);
			stringBuilder.Append('|');
			stringBuilder.Append(port.ToString(NumberFormatInfo.InvariantInfo));
			stringBuilder.Append('|');
			return stringBuilder.ToString();
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002DD5 RID: 11733 RVA: 0x0009DE19 File Offset: 0x0009C019
		internal long ContentLength
		{
			get
			{
				return this._contentLength;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x0009DE21 File Offset: 0x0009C021
		internal DateTime LastModified
		{
			get
			{
				return this._lastModified;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002DD7 RID: 11735 RVA: 0x0009DE29 File Offset: 0x0009C029
		internal Uri ResponseUri
		{
			get
			{
				return this._responseUri;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x0009DE31 File Offset: 0x0009C031
		internal string BannerMessage
		{
			get
			{
				if (this._bannerMessage == null)
				{
					return null;
				}
				return this._bannerMessage.ToString();
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002DD9 RID: 11737 RVA: 0x0009DE48 File Offset: 0x0009C048
		internal string WelcomeMessage
		{
			get
			{
				if (this._welcomeMessage == null)
				{
					return null;
				}
				return this._welcomeMessage.ToString();
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002DDA RID: 11738 RVA: 0x0009DE5F File Offset: 0x0009C05F
		internal string ExitMessage
		{
			get
			{
				if (this._exitMessage == null)
				{
					return null;
				}
				return this._exitMessage.ToString();
			}
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x0009DE76 File Offset: 0x0009C076
		private long GetContentLengthFrom213Response(string responseString)
		{
			string[] array = responseString.Split(new char[]
			{
				' '
			});
			if (array.Length < 2)
			{
				throw new FormatException(SR.Format("The response string '{0}' has invalid format.", responseString));
			}
			return Convert.ToInt64(array[1], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x0009DEAC File Offset: 0x0009C0AC
		private DateTime GetLastModifiedFrom213Response(string str)
		{
			DateTime result = this._lastModified;
			string[] array = str.Split(new char[]
			{
				' ',
				'.'
			});
			if (array.Length < 2)
			{
				return result;
			}
			string text = array[1];
			if (text.Length < 14)
			{
				return result;
			}
			int year = Convert.ToInt32(text.Substring(0, 4), NumberFormatInfo.InvariantInfo);
			int month = (int)Convert.ToInt16(text.Substring(4, 2), NumberFormatInfo.InvariantInfo);
			int day = (int)Convert.ToInt16(text.Substring(6, 2), NumberFormatInfo.InvariantInfo);
			int hour = (int)Convert.ToInt16(text.Substring(8, 2), NumberFormatInfo.InvariantInfo);
			int minute = (int)Convert.ToInt16(text.Substring(10, 2), NumberFormatInfo.InvariantInfo);
			int second = (int)Convert.ToInt16(text.Substring(12, 2), NumberFormatInfo.InvariantInfo);
			int millisecond = 0;
			if (array.Length > 2)
			{
				millisecond = (int)Convert.ToInt16(array[2], NumberFormatInfo.InvariantInfo);
			}
			try
			{
				result = new DateTime(year, month, day, hour, minute, second, millisecond);
				result = result.ToLocalTime();
			}
			catch (ArgumentOutOfRangeException)
			{
			}
			catch (ArgumentException)
			{
			}
			return result;
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x0009DFC4 File Offset: 0x0009C1C4
		private void TryUpdateResponseUri(string str, FtpWebRequest request)
		{
			Uri uri = request.RequestUri;
			int num = str.IndexOf("for ");
			if (num == -1)
			{
				return;
			}
			num += 4;
			int num2 = str.LastIndexOf('(');
			if (num2 == -1)
			{
				num2 = str.Length;
			}
			if (num2 <= num)
			{
				return;
			}
			string text = str.Substring(num, num2 - num);
			text = text.TrimEnd(new char[]
			{
				' ',
				'.',
				'\r',
				'\n'
			});
			string text2 = text.Replace("%", "%25");
			text2 = text2.Replace("#", "%23");
			string absolutePath = uri.AbsolutePath;
			if (absolutePath.Length > 0 && absolutePath[absolutePath.Length - 1] != '/')
			{
				uri = new UriBuilder(uri)
				{
					Path = absolutePath + "/"
				}.Uri;
			}
			Uri uri2;
			if (!Uri.TryCreate(uri, text2, out uri2))
			{
				throw new FormatException(SR.Format("The server returned the filename ({0}) which is not valid.", text));
			}
			if (!uri.IsBaseOf(uri2) || uri.Segments.Length != uri2.Segments.Length - 1)
			{
				throw new FormatException(SR.Format("The server returned the filename ({0}) which is not valid.", text));
			}
			this._responseUri = uri2;
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x0009E0E8 File Offset: 0x0009C2E8
		private void TryUpdateContentLength(string str)
		{
			int num = str.LastIndexOf("(");
			if (num != -1)
			{
				int num2 = str.IndexOf(" bytes).");
				if (num2 != -1 && num2 > num)
				{
					num++;
					long contentLength;
					if (long.TryParse(str.Substring(num, num2 - num), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out contentLength))
					{
						this._contentLength = contentLength;
					}
				}
			}
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x0009E140 File Offset: 0x0009C340
		private string GetLoginDirectory(string str)
		{
			int num = str.IndexOf('"');
			int num2 = str.LastIndexOf('"');
			if (num != -1 && num2 != -1 && num != num2)
			{
				return str.Substring(num + 1, num2 - num - 1);
			}
			return string.Empty;
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x0009E180 File Offset: 0x0009C380
		private int GetPortV4(string responseString)
		{
			string[] array = responseString.Split(new char[]
			{
				' ',
				'(',
				',',
				')'
			});
			if (array.Length <= 7)
			{
				throw new FormatException(SR.Format("The response string '{0}' has invalid format.", responseString));
			}
			int num = array.Length - 1;
			if (!char.IsNumber(array[num], 0))
			{
				num--;
			}
			return (int)Convert.ToByte(array[num--], NumberFormatInfo.InvariantInfo) | (int)Convert.ToByte(array[num--], NumberFormatInfo.InvariantInfo) << 8;
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x0009E1F8 File Offset: 0x0009C3F8
		private int GetPortV6(string responseString)
		{
			int num = responseString.LastIndexOf("(");
			int num2 = responseString.LastIndexOf(")");
			if (num == -1 || num2 <= num)
			{
				throw new FormatException(SR.Format("The response string '{0}' has invalid format.", responseString));
			}
			string[] array = responseString.Substring(num + 1, num2 - num - 1).Split(new char[]
			{
				'|'
			});
			if (array.Length < 4)
			{
				throw new FormatException(SR.Format("The response string '{0}' has invalid format.", responseString));
			}
			return Convert.ToInt32(array[3], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x0009E278 File Offset: 0x0009C478
		private void CreateFtpListenerSocket(FtpWebRequest request)
		{
			IPEndPoint localEP = new IPEndPoint(((IPEndPoint)base.Socket.LocalEndPoint).Address, 0);
			try
			{
				this._dataSocket = this.CreateFtpDataSocket(request, base.Socket);
			}
			catch (ObjectDisposedException)
			{
				throw ExceptionHelper.RequestAbortedException;
			}
			this._dataSocket.Bind(localEP);
			this._dataSocket.Listen(1);
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x0009E2E8 File Offset: 0x0009C4E8
		private string GetPortCommandLine(FtpWebRequest request)
		{
			string result;
			try
			{
				IPEndPoint ipendPoint = (IPEndPoint)this._dataSocket.LocalEndPoint;
				if (base.ServerAddress.AddressFamily == AddressFamily.InterNetwork)
				{
					result = this.FormatAddress(ipendPoint.Address, ipendPoint.Port);
				}
				else
				{
					if (base.ServerAddress.AddressFamily != AddressFamily.InterNetworkV6)
					{
						throw new InternalException();
					}
					result = this.FormatAddressV6(ipendPoint.Address, ipendPoint.Port);
				}
			}
			catch (Exception innerException)
			{
				throw base.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ProtocolError, innerException);
			}
			return result;
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x0009E374 File Offset: 0x0009C574
		private string FormatFtpCommand(string command, string parameter)
		{
			StringBuilder stringBuilder = new StringBuilder(command.Length + ((parameter != null) ? parameter.Length : 0) + 3);
			stringBuilder.Append(command);
			if (!string.IsNullOrEmpty(parameter))
			{
				stringBuilder.Append(' ');
				stringBuilder.Append(parameter);
			}
			stringBuilder.Append("\r\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x0009E3CF File Offset: 0x0009C5CF
		protected Socket CreateFtpDataSocket(FtpWebRequest request, Socket templateSocket)
		{
			return new Socket(templateSocket.AddressFamily, templateSocket.SocketType, templateSocket.ProtocolType);
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x0009E3E8 File Offset: 0x0009C5E8
		protected override bool CheckValid(ResponseDescription response, ref int validThrough, ref int completeLength)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("CheckValid({0})", new object[]
				{
					response.StatusBuffer
				}), "CheckValid");
			}
			if (response.StatusBuffer.Length < 4)
			{
				return true;
			}
			string text = response.StatusBuffer.ToString();
			if (response.Status == -1)
			{
				if (!char.IsDigit(text[0]) || !char.IsDigit(text[1]) || !char.IsDigit(text[2]) || (text[3] != ' ' && text[3] != '-'))
				{
					return false;
				}
				response.StatusCodeString = text.Substring(0, 3);
				response.Status = (int)Convert.ToInt16(response.StatusCodeString, NumberFormatInfo.InvariantInfo);
				if (text[3] == '-')
				{
					response.Multiline = true;
				}
			}
			int num;
			while ((num = text.IndexOf("\r\n", validThrough)) != -1)
			{
				int num2 = validThrough;
				validThrough = num + 2;
				if (!response.Multiline)
				{
					completeLength = validThrough;
					return true;
				}
				if (text.Length > num2 + 4 && text.Substring(num2, 3) == response.StatusCodeString && text[num2 + 3] == ' ')
				{
					completeLength = validThrough;
					return true;
				}
			}
			return true;
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x0009E520 File Offset: 0x0009C720
		private TriState IsFtpDataStreamWriteable()
		{
			FtpWebRequest ftpWebRequest = this._request as FtpWebRequest;
			if (ftpWebRequest != null)
			{
				if (ftpWebRequest.MethodInfo.IsUpload)
				{
					return TriState.True;
				}
				if (ftpWebRequest.MethodInfo.IsDownload)
				{
					return TriState.False;
				}
			}
			return TriState.Unspecified;
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x0009E55B File Offset: 0x0009C75B
		// Note: this type is marked as 'beforefieldinit'.
		static FtpControlStream()
		{
		}

		// Token: 0x04001927 RID: 6439
		private Socket _dataSocket;

		// Token: 0x04001928 RID: 6440
		private IPEndPoint _passiveEndPoint;

		// Token: 0x04001929 RID: 6441
		private TlsStream _tlsStream;

		// Token: 0x0400192A RID: 6442
		private StringBuilder _bannerMessage;

		// Token: 0x0400192B RID: 6443
		private StringBuilder _welcomeMessage;

		// Token: 0x0400192C RID: 6444
		private StringBuilder _exitMessage;

		// Token: 0x0400192D RID: 6445
		private WeakReference _credentials;

		// Token: 0x0400192E RID: 6446
		private string _currentTypeSetting = string.Empty;

		// Token: 0x0400192F RID: 6447
		private long _contentLength = -1L;

		// Token: 0x04001930 RID: 6448
		private DateTime _lastModified;

		// Token: 0x04001931 RID: 6449
		private bool _dataHandshakeStarted;

		// Token: 0x04001932 RID: 6450
		private string _loginDirectory;

		// Token: 0x04001933 RID: 6451
		private string _establishedServerDirectory;

		// Token: 0x04001934 RID: 6452
		private string _requestedServerDirectory;

		// Token: 0x04001935 RID: 6453
		private Uri _responseUri;

		// Token: 0x04001936 RID: 6454
		private FtpLoginState _loginState;

		// Token: 0x04001937 RID: 6455
		internal FtpStatusCode StatusCode;

		// Token: 0x04001938 RID: 6456
		internal string StatusLine;

		// Token: 0x04001939 RID: 6457
		private static readonly AsyncCallback s_acceptCallbackDelegate = new AsyncCallback(FtpControlStream.AcceptCallback);

		// Token: 0x0400193A RID: 6458
		private static readonly AsyncCallback s_connectCallbackDelegate = new AsyncCallback(FtpControlStream.ConnectCallback);

		// Token: 0x0400193B RID: 6459
		private static readonly AsyncCallback s_SSLHandshakeCallback = new AsyncCallback(FtpControlStream.SSLHandshakeCallback);

		// Token: 0x02000588 RID: 1416
		private enum GetPathOption
		{
			// Token: 0x0400193D RID: 6461
			Normal,
			// Token: 0x0400193E RID: 6462
			AssumeFilename,
			// Token: 0x0400193F RID: 6463
			AssumeNoFilename
		}

		// Token: 0x02000589 RID: 1417
		[CompilerGenerated]
		private sealed class <>c__DisplayClass31_0
		{
			// Token: 0x06002DE9 RID: 11753 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass31_0()
			{
			}

			// Token: 0x06002DEA RID: 11754 RVA: 0x0009E590 File Offset: 0x0009C790
			internal void <PipelineCallback>b__0(IAsyncResult ar)
			{
				try
				{
					this.tlsStream.EndAuthenticateAsClient(ar);
					this.<>4__this.NetworkStream = this.tlsStream;
					this.<>4__this.ContinueCommandPipeline();
				}
				catch (Exception obj)
				{
					this.<>4__this.CloseSocket();
					this.<>4__this.InvokeRequestCallback(obj);
				}
			}

			// Token: 0x04001940 RID: 6464
			public FtpControlStream <>4__this;

			// Token: 0x04001941 RID: 6465
			public TlsStream tlsStream;
		}
	}
}
