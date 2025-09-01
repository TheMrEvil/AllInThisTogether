using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net
{
	// Token: 0x02000580 RID: 1408
	internal class CommandStream : NetworkStreamWrapper
	{
		// Token: 0x06002DA3 RID: 11683 RVA: 0x0009C340 File Offset: 0x0009A540
		internal CommandStream(TcpClient client) : base(client)
		{
			this._decoder = this._encoding.GetDecoder();
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x0009C370 File Offset: 0x0009A570
		internal virtual void Abort(Exception e)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, "closing control Stream", "Abort");
			}
			lock (this)
			{
				if (this._aborted)
				{
					return;
				}
				this._aborted = true;
			}
			try
			{
				base.Close(0);
			}
			finally
			{
				if (e != null)
				{
					this.InvokeRequestCallback(e);
				}
				else
				{
					this.InvokeRequestCallback(null);
				}
			}
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x0009C3F8 File Offset: 0x0009A5F8
		protected override void Dispose(bool disposing)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "Dispose");
			}
			this.InvokeRequestCallback(null);
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x0009C414 File Offset: 0x0009A614
		protected void InvokeRequestCallback(object obj)
		{
			WebRequest request = this._request;
			if (request != null)
			{
				((FtpWebRequest)request).RequestCallback(obj);
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x0009C437 File Offset: 0x0009A637
		internal bool RecoverableFailure
		{
			get
			{
				return this._recoverableFailure;
			}
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x0009C43F File Offset: 0x0009A63F
		protected void MarkAsRecoverableFailure()
		{
			if (this._index <= 1)
			{
				this._recoverableFailure = true;
			}
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x0009C454 File Offset: 0x0009A654
		internal Stream SubmitRequest(WebRequest request, bool isAsync, bool readInitalResponseOnConnect)
		{
			this.ClearState();
			CommandStream.PipelineEntry[] commands = this.BuildCommandsList(request);
			this.InitCommandPipeline(request, commands, isAsync);
			if (readInitalResponseOnConnect)
			{
				this._doSend = false;
				this._index = -1;
			}
			return this.ContinueCommandPipeline();
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x0009C48F File Offset: 0x0009A68F
		protected virtual void ClearState()
		{
			this.InitCommandPipeline(null, null, false);
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x00002F6A File Offset: 0x0000116A
		protected virtual CommandStream.PipelineEntry[] BuildCommandsList(WebRequest request)
		{
			return null;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x0009C49A File Offset: 0x0009A69A
		protected Exception GenerateException(string message, WebExceptionStatus status, Exception innerException)
		{
			return new WebException(message, innerException, status, null);
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x0009C4A5 File Offset: 0x0009A6A5
		protected Exception GenerateException(FtpStatusCode code, string statusDescription, Exception innerException)
		{
			return new WebException(SR.Format("The remote server returned an error: {0}.", NetRes.GetWebStatusCodeString(code, statusDescription)), innerException, WebExceptionStatus.ProtocolError, null);
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x0009C4C0 File Offset: 0x0009A6C0
		protected void InitCommandPipeline(WebRequest request, CommandStream.PipelineEntry[] commands, bool isAsync)
		{
			this._commands = commands;
			this._index = 0;
			this._request = request;
			this._aborted = false;
			this._doRead = true;
			this._doSend = true;
			this._currentResponseDescription = null;
			this._isAsync = isAsync;
			this._recoverableFailure = false;
			this._abortReason = string.Empty;
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x0009C518 File Offset: 0x0009A718
		internal void CheckContinuePipeline()
		{
			if (this._isAsync)
			{
				return;
			}
			try
			{
				this.ContinueCommandPipeline();
			}
			catch (Exception e)
			{
				this.Abort(e);
			}
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x0009C554 File Offset: 0x0009A754
		protected Stream ContinueCommandPipeline()
		{
			bool isAsync = this._isAsync;
			while (this._index < this._commands.Length)
			{
				if (this._doSend)
				{
					if (this._index < 0)
					{
						throw new InternalException();
					}
					byte[] bytes = this.Encoding.GetBytes(this._commands[this._index].Command);
					if (NetEventSource.Log.IsEnabled())
					{
						string text = this._commands[this._index].Command.Substring(0, this._commands[this._index].Command.Length - 2);
						if (this._commands[this._index].HasFlag(CommandStream.PipelineEntryFlags.DontLogParameter))
						{
							int num = text.IndexOf(' ');
							if (num != -1)
							{
								text = text.Substring(0, num) + " ********";
							}
						}
						if (NetEventSource.IsEnabled)
						{
							NetEventSource.Info(this, FormattableStringFactory.Create("Sending command {0}", new object[]
							{
								text
							}), "ContinueCommandPipeline");
						}
					}
					try
					{
						if (isAsync)
						{
							this.BeginWrite(bytes, 0, bytes.Length, CommandStream.s_writeCallbackDelegate, this);
						}
						else
						{
							this.Write(bytes, 0, bytes.Length);
						}
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
					if (isAsync)
					{
						return null;
					}
				}
				Stream result = null;
				if (this.PostSendCommandProcessing(ref result))
				{
					return result;
				}
			}
			lock (this)
			{
				this.Close();
			}
			return null;
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x0009C6E8 File Offset: 0x0009A8E8
		private bool PostSendCommandProcessing(ref Stream stream)
		{
			if (this._doRead)
			{
				bool isAsync = this._isAsync;
				int index = this._index;
				CommandStream.PipelineEntry[] commands = this._commands;
				try
				{
					ResponseDescription currentResponseDescription = this.ReceiveCommandResponse();
					if (isAsync)
					{
						return true;
					}
					this._currentResponseDescription = currentResponseDescription;
				}
				catch
				{
					if (index < 0 || index >= commands.Length || commands[index].Command != "QUIT\r\n")
					{
						throw;
					}
				}
			}
			return this.PostReadCommandProcessing(ref stream);
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x0009C768 File Offset: 0x0009A968
		private bool PostReadCommandProcessing(ref Stream stream)
		{
			if (this._index >= this._commands.Length)
			{
				return false;
			}
			this._doSend = false;
			this._doRead = false;
			CommandStream.PipelineEntry pipelineEntry;
			if (this._index == -1)
			{
				pipelineEntry = null;
			}
			else
			{
				pipelineEntry = this._commands[this._index];
			}
			CommandStream.PipelineInstruction pipelineInstruction;
			if (this._currentResponseDescription == null && pipelineEntry.Command == "QUIT\r\n")
			{
				pipelineInstruction = CommandStream.PipelineInstruction.Advance;
			}
			else
			{
				pipelineInstruction = this.PipelineCallback(pipelineEntry, this._currentResponseDescription, false, ref stream);
			}
			if (pipelineInstruction == CommandStream.PipelineInstruction.Abort)
			{
				Exception ex;
				if (this._abortReason != string.Empty)
				{
					ex = new WebException(this._abortReason);
				}
				else
				{
					ex = this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
				}
				this.Abort(ex);
				throw ex;
			}
			if (pipelineInstruction == CommandStream.PipelineInstruction.Advance)
			{
				this._currentResponseDescription = null;
				this._doSend = true;
				this._doRead = true;
				this._index++;
			}
			else
			{
				if (pipelineInstruction == CommandStream.PipelineInstruction.Pause)
				{
					return true;
				}
				if (pipelineInstruction == CommandStream.PipelineInstruction.GiveStream)
				{
					this._currentResponseDescription = null;
					this._doRead = true;
					if (this._isAsync)
					{
						this.ContinueCommandPipeline();
						this.InvokeRequestCallback(stream);
					}
					return true;
				}
				if (pipelineInstruction == CommandStream.PipelineInstruction.Reread)
				{
					this._currentResponseDescription = null;
					this._doRead = true;
				}
			}
			return false;
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual CommandStream.PipelineInstruction PipelineCallback(CommandStream.PipelineEntry entry, ResponseDescription response, bool timeout, ref Stream stream)
		{
			return CommandStream.PipelineInstruction.Abort;
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x0009C888 File Offset: 0x0009AA88
		private static void ReadCallback(IAsyncResult asyncResult)
		{
			ReceiveState receiveState = (ReceiveState)asyncResult.AsyncState;
			try
			{
				Stream connection = receiveState.Connection;
				int num = 0;
				try
				{
					num = connection.EndRead(asyncResult);
					if (num == 0)
					{
						receiveState.Connection.CloseSocket();
					}
				}
				catch (IOException)
				{
					receiveState.Connection.MarkAsRecoverableFailure();
					throw;
				}
				catch
				{
					throw;
				}
				receiveState.Connection.ReceiveCommandResponseCallback(receiveState, num);
			}
			catch (Exception e)
			{
				receiveState.Connection.Abort(e);
			}
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x0009C91C File Offset: 0x0009AB1C
		private static void WriteCallback(IAsyncResult asyncResult)
		{
			CommandStream commandStream = (CommandStream)asyncResult.AsyncState;
			try
			{
				try
				{
					commandStream.EndWrite(asyncResult);
				}
				catch (IOException)
				{
					commandStream.MarkAsRecoverableFailure();
					throw;
				}
				catch
				{
					throw;
				}
				Stream stream = null;
				if (!commandStream.PostSendCommandProcessing(ref stream))
				{
					commandStream.ContinueCommandPipeline();
				}
			}
			catch (Exception e)
			{
				commandStream.Abort(e);
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x0009C994 File Offset: 0x0009AB94
		// (set) Token: 0x06002DB7 RID: 11703 RVA: 0x0009C99C File Offset: 0x0009AB9C
		protected Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
			set
			{
				this._encoding = value;
				this._decoder = this._encoding.GetDecoder();
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool CheckValid(ResponseDescription response, ref int validThrough, ref int completeLength)
		{
			return false;
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x0009C9B8 File Offset: 0x0009ABB8
		private ResponseDescription ReceiveCommandResponse()
		{
			ReceiveState receiveState = new ReceiveState(this);
			try
			{
				if (this._buffer.Length > 0)
				{
					this.ReceiveCommandResponseCallback(receiveState, -1);
				}
				else
				{
					try
					{
						if (this._isAsync)
						{
							this.BeginRead(receiveState.Buffer, 0, receiveState.Buffer.Length, CommandStream.s_readCallbackDelegate, receiveState);
							return null;
						}
						int num = this.Read(receiveState.Buffer, 0, receiveState.Buffer.Length);
						if (num == 0)
						{
							base.CloseSocket();
						}
						this.ReceiveCommandResponseCallback(receiveState, num);
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is WebException)
				{
					throw;
				}
				throw this.GenerateException("The underlying connection was closed: An unexpected error occurred on a receive", WebExceptionStatus.ReceiveFailure, ex);
			}
			return receiveState.Resp;
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x0009CA90 File Offset: 0x0009AC90
		private void ReceiveCommandResponseCallback(ReceiveState state, int bytesRead)
		{
			int num = -1;
			for (;;)
			{
				int validThrough = state.ValidThrough;
				if (this._buffer.Length > 0)
				{
					state.Resp.StatusBuffer.Append(this._buffer);
					this._buffer = string.Empty;
					if (!this.CheckValid(state.Resp, ref validThrough, ref num))
					{
						break;
					}
				}
				else
				{
					if (bytesRead <= 0)
					{
						goto Block_3;
					}
					char[] array = new char[this._decoder.GetCharCount(state.Buffer, 0, bytesRead)];
					int chars = this._decoder.GetChars(state.Buffer, 0, bytesRead, array, 0, false);
					string text = new string(array, 0, chars);
					state.Resp.StatusBuffer.Append(text);
					if (!this.CheckValid(state.Resp, ref validThrough, ref num))
					{
						goto Block_4;
					}
					if (num >= 0)
					{
						int num2 = state.Resp.StatusBuffer.Length - num;
						if (num2 > 0)
						{
							this._buffer = text.Substring(text.Length - num2, num2);
						}
					}
				}
				if (num < 0)
				{
					state.ValidThrough = validThrough;
					try
					{
						if (this._isAsync)
						{
							this.BeginRead(state.Buffer, 0, state.Buffer.Length, CommandStream.s_readCallbackDelegate, state);
							return;
						}
						bytesRead = this.Read(state.Buffer, 0, state.Buffer.Length);
						if (bytesRead == 0)
						{
							base.CloseSocket();
						}
						continue;
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
					goto IL_17B;
				}
				goto IL_17B;
			}
			throw this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
			Block_3:
			throw this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
			Block_4:
			throw this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
			IL_17B:
			string text2 = state.Resp.StatusBuffer.ToString();
			state.Resp.StatusDescription = text2.Substring(0, num);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("Received response: {0}", new object[]
				{
					text2.Substring(0, num - 2)
				}), "ReceiveCommandResponseCallback");
			}
			if (this._isAsync)
			{
				if (state.Resp != null)
				{
					this._currentResponseDescription = state.Resp;
				}
				Stream stream = null;
				if (this.PostReadCommandProcessing(ref stream))
				{
					return;
				}
				this.ContinueCommandPipeline();
			}
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x0009CCB8 File Offset: 0x0009AEB8
		// Note: this type is marked as 'beforefieldinit'.
		static CommandStream()
		{
		}

		// Token: 0x040018F9 RID: 6393
		private static readonly AsyncCallback s_writeCallbackDelegate = new AsyncCallback(CommandStream.WriteCallback);

		// Token: 0x040018FA RID: 6394
		private static readonly AsyncCallback s_readCallbackDelegate = new AsyncCallback(CommandStream.ReadCallback);

		// Token: 0x040018FB RID: 6395
		private bool _recoverableFailure;

		// Token: 0x040018FC RID: 6396
		protected WebRequest _request;

		// Token: 0x040018FD RID: 6397
		protected bool _isAsync;

		// Token: 0x040018FE RID: 6398
		private bool _aborted;

		// Token: 0x040018FF RID: 6399
		protected CommandStream.PipelineEntry[] _commands;

		// Token: 0x04001900 RID: 6400
		protected int _index;

		// Token: 0x04001901 RID: 6401
		private bool _doRead;

		// Token: 0x04001902 RID: 6402
		private bool _doSend;

		// Token: 0x04001903 RID: 6403
		private ResponseDescription _currentResponseDescription;

		// Token: 0x04001904 RID: 6404
		protected string _abortReason;

		// Token: 0x04001905 RID: 6405
		private const int WaitingForPipeline = 1;

		// Token: 0x04001906 RID: 6406
		private const int CompletedPipeline = 2;

		// Token: 0x04001907 RID: 6407
		private string _buffer = string.Empty;

		// Token: 0x04001908 RID: 6408
		private Encoding _encoding = Encoding.UTF8;

		// Token: 0x04001909 RID: 6409
		private Decoder _decoder;

		// Token: 0x02000581 RID: 1409
		internal enum PipelineInstruction
		{
			// Token: 0x0400190B RID: 6411
			Abort,
			// Token: 0x0400190C RID: 6412
			Advance,
			// Token: 0x0400190D RID: 6413
			Pause,
			// Token: 0x0400190E RID: 6414
			Reread,
			// Token: 0x0400190F RID: 6415
			GiveStream
		}

		// Token: 0x02000582 RID: 1410
		[Flags]
		internal enum PipelineEntryFlags
		{
			// Token: 0x04001911 RID: 6417
			UserCommand = 1,
			// Token: 0x04001912 RID: 6418
			GiveDataStream = 2,
			// Token: 0x04001913 RID: 6419
			CreateDataConnection = 4,
			// Token: 0x04001914 RID: 6420
			DontLogParameter = 8
		}

		// Token: 0x02000583 RID: 1411
		internal class PipelineEntry
		{
			// Token: 0x06002DBC RID: 11708 RVA: 0x0009CCDC File Offset: 0x0009AEDC
			internal PipelineEntry(string command)
			{
				this.Command = command;
			}

			// Token: 0x06002DBD RID: 11709 RVA: 0x0009CCEB File Offset: 0x0009AEEB
			internal PipelineEntry(string command, CommandStream.PipelineEntryFlags flags)
			{
				this.Command = command;
				this.Flags = flags;
			}

			// Token: 0x06002DBE RID: 11710 RVA: 0x0009CD01 File Offset: 0x0009AF01
			internal bool HasFlag(CommandStream.PipelineEntryFlags flags)
			{
				return (this.Flags & flags) > (CommandStream.PipelineEntryFlags)0;
			}

			// Token: 0x04001915 RID: 6421
			internal string Command;

			// Token: 0x04001916 RID: 6422
			internal CommandStream.PipelineEntryFlags Flags;
		}
	}
}
