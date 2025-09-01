using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x02000271 RID: 625
	internal abstract class TdsParserStateObject
	{
		// Token: 0x06001D09 RID: 7433 RVA: 0x00089EFC File Offset: 0x000880FC
		internal TdsParserStateObject(TdsParser parser)
		{
			this._parser = parser;
			this.SetPacketSize(4096);
			this.IncrementPendingCallbacks();
			this._lastSuccessfulIOTimer = new LastIOTimer();
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x00089FB8 File Offset: 0x000881B8
		internal TdsParserStateObject(TdsParser parser, TdsParserStateObject physicalConnection, bool async)
		{
			this._parser = parser;
			this.SniContext = SniContext.Snix_GetMarsSession;
			this.SetPacketSize(this._parser._physicalStateObj._outBuff.Length);
			this.CreateSessionHandle(physicalConnection, async);
			if (this.IsFailedHandle())
			{
				this.AddError(parser.ProcessSNIError(this));
				this.ThrowExceptionAndWarning(false, false);
			}
			this.IncrementPendingCallbacks();
			this._lastSuccessfulIOTimer = parser._physicalStateObj._lastSuccessfulIOTimer;
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x0008A0B2 File Offset: 0x000882B2
		// (set) Token: 0x06001D0C RID: 7436 RVA: 0x0008A0BA File Offset: 0x000882BA
		internal bool BcpLock
		{
			get
			{
				return this._bcpLock;
			}
			set
			{
				this._bcpLock = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0008A0C3 File Offset: 0x000882C3
		internal bool HasOpenResult
		{
			get
			{
				return this._hasOpenResult;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0008A0CB File Offset: 0x000882CB
		internal bool IsOrphaned
		{
			get
			{
				return this._activateCount != 0 && !this._owner.IsAlive;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (set) Token: 0x06001D0F RID: 7439 RVA: 0x0008A0E8 File Offset: 0x000882E8
		internal object Owner
		{
			set
			{
				SqlDataReader sqlDataReader = value as SqlDataReader;
				if (sqlDataReader == null)
				{
					this._readerState = null;
				}
				else
				{
					this._readerState = sqlDataReader._sharedState;
				}
				this._owner.Target = value;
			}
		}

		// Token: 0x06001D10 RID: 7440
		internal abstract uint DisabeSsl();

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x0008A120 File Offset: 0x00088320
		internal bool HasOwner
		{
			get
			{
				return this._owner.IsAlive;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0008A12D File Offset: 0x0008832D
		internal TdsParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x06001D13 RID: 7443
		internal abstract uint EnableMars(ref uint info);

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0008A135 File Offset: 0x00088335
		// (set) Token: 0x06001D15 RID: 7445 RVA: 0x0008A13D File Offset: 0x0008833D
		internal SniContext SniContext
		{
			get
			{
				return this._sniContext;
			}
			set
			{
				this._sniContext = value;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001D16 RID: 7446
		internal abstract uint Status { get; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001D17 RID: 7447
		internal abstract object SessionHandle { get; }

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x0008A146 File Offset: 0x00088346
		internal bool TimeoutHasExpired
		{
			get
			{
				return TdsParserStaticMethods.TimeoutHasExpired(this._timeoutTime);
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0008A153 File Offset: 0x00088353
		// (set) Token: 0x06001D1A RID: 7450 RVA: 0x0008A17C File Offset: 0x0008837C
		internal long TimeoutTime
		{
			get
			{
				if (this._timeoutMilliseconds != 0L)
				{
					this._timeoutTime = TdsParserStaticMethods.GetTimeout(this._timeoutMilliseconds);
					this._timeoutMilliseconds = 0L;
				}
				return this._timeoutTime;
			}
			set
			{
				this._timeoutMilliseconds = 0L;
				this._timeoutTime = value;
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0008A190 File Offset: 0x00088390
		internal int GetTimeoutRemaining()
		{
			int result;
			if (this._timeoutMilliseconds != 0L)
			{
				result = (int)Math.Min(2147483647L, this._timeoutMilliseconds);
				this._timeoutTime = TdsParserStaticMethods.GetTimeout(this._timeoutMilliseconds);
				this._timeoutMilliseconds = 0L;
			}
			else
			{
				result = TdsParserStaticMethods.GetTimeoutMilliseconds(this._timeoutTime);
			}
			return result;
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0008A1E0 File Offset: 0x000883E0
		internal bool TryStartNewRow(bool isNullCompressed, int nullBitmapColumnsCount = 0)
		{
			if (this._snapshot != null)
			{
				this._snapshot.CloneNullBitmapInfo();
			}
			if (isNullCompressed)
			{
				if (!this._nullBitmapInfo.TryInitialize(this, nullBitmapColumnsCount))
				{
					return false;
				}
			}
			else
			{
				this._nullBitmapInfo.Clean();
			}
			return true;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0008A218 File Offset: 0x00088418
		internal bool IsRowTokenReady()
		{
			int num = Math.Min(this._inBytesPacket, this._inBytesRead - this._inBytesUsed) - 1;
			if (num > 0)
			{
				if (this._inBuff[this._inBytesUsed] == 209)
				{
					return true;
				}
				if (this._inBuff[this._inBytesUsed] == 210)
				{
					return 1 + (this._cleanupMetaData.Length + 7) / 8 <= num;
				}
			}
			return false;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0008A287 File Offset: 0x00088487
		internal bool IsNullCompressionBitSet(int columnOrdinal)
		{
			return this._nullBitmapInfo.IsGuaranteedNull(columnOrdinal);
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0008A295 File Offset: 0x00088495
		internal void Activate(object owner)
		{
			this.Owner = owner;
			Interlocked.Increment(ref this._activateCount);
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x0008A2AC File Offset: 0x000884AC
		internal void Cancel(object caller)
		{
			bool flag = false;
			try
			{
				while (!flag && this._parser.State != TdsParserState.Closed && this._parser.State != TdsParserState.Broken)
				{
					Monitor.TryEnter(this, 100, ref flag);
					if (flag && !this._cancelled && this._cancellationOwner.Target == caller)
					{
						this._cancelled = true;
						if (this._pendingData && !this._attentionSent)
						{
							bool flag2 = false;
							while (!flag2 && this._parser.State != TdsParserState.Closed && this._parser.State != TdsParserState.Broken)
							{
								try
								{
									this._parser.Connection._parserLock.Wait(false, 100, ref flag2);
									if (flag2)
									{
										this._parser.Connection.ThreadHasParserLockForClose = true;
										this.SendAttention(false);
									}
								}
								finally
								{
									if (flag2)
									{
										if (this._parser.Connection.ThreadHasParserLockForClose)
										{
											this._parser.Connection.ThreadHasParserLockForClose = false;
										}
										this._parser.Connection._parserLock.Release();
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0008A408 File Offset: 0x00088608
		internal void CancelRequest()
		{
			this.ResetBuffer();
			this._outputPacketNumber = 1;
			if (!this._bulkCopyWriteTimeout)
			{
				this.SendAttention(false);
				this.Parser.ProcessPendingAck(this);
			}
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x0008A434 File Offset: 0x00088634
		public void CheckSetResetConnectionState(uint error, CallbackType callbackType)
		{
			if (this._fResetEventOwned)
			{
				if (callbackType == CallbackType.Read && error == 0U)
				{
					this._parser._fResetConnection = false;
					this._fResetConnectionSent = false;
					this._fResetEventOwned = !this._parser._resetConnectionEvent.Set();
				}
				if (error != 0U)
				{
					this._fResetConnectionSent = false;
					this._fResetEventOwned = !this._parser._resetConnectionEvent.Set();
				}
			}
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x0008A4AA File Offset: 0x000886AA
		internal void CloseSession()
		{
			this.ResetCancelAndProcessAttention();
			this.Parser.PutSession(this);
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0008A4C0 File Offset: 0x000886C0
		private void ResetCancelAndProcessAttention()
		{
			lock (this)
			{
				this._cancelled = false;
				this._cancellationOwner.Target = null;
				if (this._attentionSent)
				{
					this.Parser.ProcessPendingAck(this);
				}
				this._internalTimeout = false;
			}
		}

		// Token: 0x06001D25 RID: 7461
		internal abstract void CreatePhysicalSNIHandle(string serverName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, ref byte[] spnBuffer, bool flushCache, bool async, bool fParallel, bool isIntegratedSecurity = false);

		// Token: 0x06001D26 RID: 7462
		internal abstract uint SniGetConnectionId(ref Guid clientConnectionId);

		// Token: 0x06001D27 RID: 7463
		internal abstract bool IsFailedHandle();

		// Token: 0x06001D28 RID: 7464
		protected abstract void CreateSessionHandle(TdsParserStateObject physicalConnection, bool async);

		// Token: 0x06001D29 RID: 7465
		protected abstract void FreeGcHandle(int remaining, bool release);

		// Token: 0x06001D2A RID: 7466
		internal abstract uint EnableSsl(ref uint info);

		// Token: 0x06001D2B RID: 7467
		internal abstract uint WaitForSSLHandShakeToComplete();

		// Token: 0x06001D2C RID: 7468
		internal abstract void Dispose();

		// Token: 0x06001D2D RID: 7469
		internal abstract void DisposePacketCache();

		// Token: 0x06001D2E RID: 7470
		internal abstract bool IsPacketEmpty(object readPacket);

		// Token: 0x06001D2F RID: 7471
		internal abstract object ReadSyncOverAsync(int timeoutRemaining, out uint error);

		// Token: 0x06001D30 RID: 7472
		internal abstract object ReadAsync(out uint error, ref object handle);

		// Token: 0x06001D31 RID: 7473
		internal abstract uint CheckConnection();

		// Token: 0x06001D32 RID: 7474
		internal abstract uint SetConnectionBufferSize(ref uint unsignedPacketSize);

		// Token: 0x06001D33 RID: 7475
		internal abstract void ReleasePacket(object syncReadPacket);

		// Token: 0x06001D34 RID: 7476
		protected abstract uint SNIPacketGetData(object packet, byte[] _inBuff, ref uint dataSize);

		// Token: 0x06001D35 RID: 7477
		internal abstract object GetResetWritePacket();

		// Token: 0x06001D36 RID: 7478
		internal abstract void ClearAllWritePackets();

		// Token: 0x06001D37 RID: 7479
		internal abstract object AddPacketToPendingList(object packet);

		// Token: 0x06001D38 RID: 7480
		protected abstract void RemovePacketFromPendingList(object pointer);

		// Token: 0x06001D39 RID: 7481
		internal abstract uint GenerateSspiClientContext(byte[] receivedBuff, uint receivedLength, ref byte[] sendBuff, ref uint sendLength, byte[] _sniSpnBuffer);

		// Token: 0x06001D3A RID: 7482 RVA: 0x0008A528 File Offset: 0x00088728
		internal bool Deactivate()
		{
			bool result = false;
			try
			{
				TdsParserState state = this.Parser.State;
				if (state != TdsParserState.Broken && state != TdsParserState.Closed)
				{
					if (this._pendingData)
					{
						this.Parser.DrainData(this);
					}
					if (this.HasOpenResult)
					{
						this.DecrementOpenResultCount();
					}
					this.ResetCancelAndProcessAttention();
					result = true;
				}
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x0008A594 File Offset: 0x00088794
		internal void RemoveOwner()
		{
			if (this._parser.MARSOn)
			{
				Interlocked.Decrement(ref this._activateCount);
			}
			this.Owner = null;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x0008A5B6 File Offset: 0x000887B6
		internal void DecrementOpenResultCount()
		{
			if (this._executedUnderTransaction == null)
			{
				this._parser.DecrementNonTransactedOpenResultCount();
			}
			else
			{
				this._executedUnderTransaction.DecrementAndObtainOpenResultCount();
				this._executedUnderTransaction = null;
			}
			this._hasOpenResult = false;
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x0008A5E8 File Offset: 0x000887E8
		internal int DecrementPendingCallbacks(bool release)
		{
			int num = Interlocked.Decrement(ref this._pendingCallbacks);
			this.FreeGcHandle(num, release);
			return num;
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x0008A60C File Offset: 0x0008880C
		internal void DisposeCounters()
		{
			Timer networkPacketTimeout = this._networkPacketTimeout;
			if (networkPacketTimeout != null)
			{
				this._networkPacketTimeout = null;
				networkPacketTimeout.Dispose();
			}
			if (Volatile.Read(ref this._readingCount) > 0)
			{
				SpinWait.SpinUntil(() => Volatile.Read(ref this._readingCount) == 0);
			}
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x0008A64F File Offset: 0x0008884F
		internal int IncrementAndObtainOpenResultCount(SqlInternalTransaction transaction)
		{
			this._hasOpenResult = true;
			if (transaction == null)
			{
				return this._parser.IncrementNonTransactedOpenResultCount();
			}
			this._executedUnderTransaction = transaction;
			return transaction.IncrementAndObtainOpenResultCount();
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x0008A674 File Offset: 0x00088874
		internal int IncrementPendingCallbacks()
		{
			return Interlocked.Increment(ref this._pendingCallbacks);
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0008A681 File Offset: 0x00088881
		internal void SetTimeoutSeconds(int timeout)
		{
			this.SetTimeoutMilliseconds((long)timeout * 1000L);
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x0008A692 File Offset: 0x00088892
		internal void SetTimeoutMilliseconds(long timeout)
		{
			if (timeout <= 0L)
			{
				this._timeoutMilliseconds = 0L;
				this._timeoutTime = long.MaxValue;
				return;
			}
			this._timeoutMilliseconds = timeout;
			this._timeoutTime = 0L;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0008A6C0 File Offset: 0x000888C0
		internal void StartSession(object cancellationOwner)
		{
			this._cancellationOwner.Target = cancellationOwner;
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0008A6CE File Offset: 0x000888CE
		internal void ThrowExceptionAndWarning(bool callerHasConnectionLock = false, bool asyncClose = false)
		{
			this._parser.ThrowExceptionAndWarning(this, callerHasConnectionLock, asyncClose);
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0008A6E0 File Offset: 0x000888E0
		internal Task ExecuteFlush()
		{
			Task result;
			lock (this)
			{
				if (this._cancelled && 1 == this._outputPacketNumber)
				{
					this.ResetBuffer();
					this._cancelled = false;
					throw SQL.OperationCancelled();
				}
				Task task = this.WritePacket(1, false);
				if (task == null)
				{
					this._pendingData = true;
					this._messageStatus = 0;
					result = null;
				}
				else
				{
					result = AsyncHelper.CreateContinuationTask(task, delegate()
					{
						this._pendingData = true;
						this._messageStatus = 0;
					}, null, null);
				}
			}
			return result;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0008A770 File Offset: 0x00088970
		internal bool TryProcessHeader()
		{
			if (this._partialHeaderBytesRead > 0 || this._inBytesUsed + this._inputHeaderLen > this._inBytesRead)
			{
				for (;;)
				{
					int num = Math.Min(this._inBytesRead - this._inBytesUsed, this._inputHeaderLen - this._partialHeaderBytesRead);
					Buffer.BlockCopy(this._inBuff, this._inBytesUsed, this._partialHeaderBuffer, this._partialHeaderBytesRead, num);
					this._partialHeaderBytesRead += num;
					this._inBytesUsed += num;
					if (this._partialHeaderBytesRead == this._inputHeaderLen)
					{
						this._partialHeaderBytesRead = 0;
						this._inBytesPacket = ((int)this._partialHeaderBuffer[2] << 8 | (int)this._partialHeaderBuffer[3]) - this._inputHeaderLen;
						this._messageStatus = this._partialHeaderBuffer[1];
					}
					else
					{
						if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
						{
							break;
						}
						if (!this.TryReadNetworkPacket())
						{
							return false;
						}
						if (this._internalTimeout)
						{
							goto Block_5;
						}
					}
					if (this._partialHeaderBytesRead == 0)
					{
						goto Block_6;
					}
				}
				this.ThrowExceptionAndWarning(false, false);
				return true;
				Block_5:
				this.ThrowExceptionAndWarning(false, false);
				return true;
				Block_6:;
			}
			else
			{
				this._messageStatus = this._inBuff[this._inBytesUsed + 1];
				this._inBytesPacket = ((int)this._inBuff[this._inBytesUsed + 2] << 8 | (int)this._inBuff[this._inBytesUsed + 2 + 1]) - this._inputHeaderLen;
				this._inBytesUsed += this._inputHeaderLen;
			}
			if (this._inBytesPacket < 0)
			{
				throw SQL.ParsingError();
			}
			return true;
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0008A8F4 File Offset: 0x00088AF4
		internal bool TryPrepareBuffer()
		{
			if (this._inBytesPacket == 0 && this._inBytesUsed < this._inBytesRead && !this.TryProcessHeader())
			{
				return false;
			}
			if (this._inBytesUsed == this._inBytesRead)
			{
				if (this._inBytesPacket > 0)
				{
					if (!this.TryReadNetworkPacket())
					{
						return false;
					}
				}
				else if (this._inBytesPacket == 0)
				{
					if (!this.TryReadNetworkPacket())
					{
						return false;
					}
					if (!this.TryProcessHeader())
					{
						return false;
					}
					if (this._inBytesUsed == this._inBytesRead && !this.TryReadNetworkPacket())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x0008A977 File Offset: 0x00088B77
		internal void ResetBuffer()
		{
			this._outBytesUsed = this._outputHeaderLen;
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0008A988 File Offset: 0x00088B88
		internal bool SetPacketSize(int size)
		{
			if (size > 32768)
			{
				throw SQL.InvalidPacketSize();
			}
			if (this._inBuff == null || this._inBuff.Length != size)
			{
				if (this._inBuff == null)
				{
					this._inBuff = new byte[size];
					this._inBytesRead = 0;
					this._inBytesUsed = 0;
				}
				else if (size != this._inBuff.Length)
				{
					if (this._inBytesRead > this._inBytesUsed)
					{
						byte[] inBuff = this._inBuff;
						this._inBuff = new byte[size];
						int num = this._inBytesRead - this._inBytesUsed;
						if (inBuff.Length < this._inBytesUsed + num || this._inBuff.Length < num)
						{
							throw SQL.InvalidInternalPacketSize(string.Concat(new string[]
							{
								SR.GetString("Invalid internal packet size:"),
								" ",
								inBuff.Length.ToString(),
								", ",
								this._inBytesUsed.ToString(),
								", ",
								num.ToString(),
								", ",
								this._inBuff.Length.ToString()
							}));
						}
						Buffer.BlockCopy(inBuff, this._inBytesUsed, this._inBuff, 0, num);
						this._inBytesRead -= this._inBytesUsed;
						this._inBytesUsed = 0;
					}
					else
					{
						this._inBuff = new byte[size];
						this._inBytesRead = 0;
						this._inBytesUsed = 0;
					}
				}
				this._outBuff = new byte[size];
				this._outBytesUsed = this._outputHeaderLen;
				return true;
			}
			return false;
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0008AB15 File Offset: 0x00088D15
		internal bool TryPeekByte(out byte value)
		{
			if (!this.TryReadByte(out value))
			{
				return false;
			}
			this._inBytesPacket++;
			this._inBytesUsed--;
			return true;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0008AB40 File Offset: 0x00088D40
		public bool TryReadByteArray(byte[] buff, int offset, int len)
		{
			int num;
			return this.TryReadByteArray(buff, offset, len, out num);
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x0008AB58 File Offset: 0x00088D58
		public bool TryReadByteArray(byte[] buff, int offset, int len, out int totalRead)
		{
			totalRead = 0;
			while (len > 0)
			{
				if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
				{
					return false;
				}
				int num = Math.Min(len, Math.Min(this._inBytesPacket, this._inBytesRead - this._inBytesUsed));
				if (buff != null)
				{
					Buffer.BlockCopy(this._inBuff, this._inBytesUsed, buff, offset + totalRead, num);
				}
				totalRead += num;
				this._inBytesUsed += num;
				this._inBytesPacket -= num;
				len -= num;
			}
			return this._messageStatus == 1 || (this._inBytesPacket != 0 && this._inBytesUsed != this._inBytesRead) || this.TryPrepareBuffer();
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0008AC24 File Offset: 0x00088E24
		internal bool TryReadByte(out byte value)
		{
			value = 0;
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				return false;
			}
			this._inBytesPacket--;
			byte[] inBuff = this._inBuff;
			int inBytesUsed = this._inBytesUsed;
			this._inBytesUsed = inBytesUsed + 1;
			value = inBuff[inBytesUsed];
			return true;
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x0008AC80 File Offset: 0x00088E80
		internal bool TryReadChar(out char value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 0, 2))
				{
					value = '\0';
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (char)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0008AD00 File Offset: 0x00088F00
		internal bool TryReadInt16(out short value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 0, 2))
				{
					value = 0;
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (short)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0008AD80 File Offset: 0x00088F80
		internal bool TryReadInt32(out int value)
		{
			if (this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToInt32(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 0, 4))
			{
				value = 0;
				return false;
			}
			value = BitConverter.ToInt32(this._bTmp, 0);
			return true;
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0008ADFC File Offset: 0x00088FFC
		internal bool TryReadInt64(out long value)
		{
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				value = 0L;
				return false;
			}
			if (this._bTmpRead <= 0 && this._inBytesUsed + 8 <= this._inBytesRead && this._inBytesPacket >= 8)
			{
				value = BitConverter.ToInt64(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 8;
				this._inBytesPacket -= 8;
				return true;
			}
			int num = 0;
			if (!this.TryReadByteArray(this._bTmp, this._bTmpRead, 8 - this._bTmpRead, out num))
			{
				this._bTmpRead += num;
				value = 0L;
				return false;
			}
			this._bTmpRead = 0;
			value = BitConverter.ToInt64(this._bTmp, 0);
			return true;
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0008AECC File Offset: 0x000890CC
		internal bool TryReadUInt16(out ushort value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 0, 2))
				{
					value = 0;
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (ushort)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0008AF4C File Offset: 0x0008914C
		internal bool TryReadUInt32(out uint value)
		{
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				value = 0U;
				return false;
			}
			if (this._bTmpRead <= 0 && this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToUInt32(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			int num = 0;
			if (!this.TryReadByteArray(this._bTmp, this._bTmpRead, 4 - this._bTmpRead, out num))
			{
				this._bTmpRead += num;
				value = 0U;
				return false;
			}
			this._bTmpRead = 0;
			value = BitConverter.ToUInt32(this._bTmp, 0);
			return true;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0008B018 File Offset: 0x00089218
		internal bool TryReadSingle(out float value)
		{
			if (this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToSingle(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 0, 4))
			{
				value = 0f;
				return false;
			}
			value = BitConverter.ToSingle(this._bTmp, 0);
			return true;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0008B098 File Offset: 0x00089298
		internal bool TryReadDouble(out double value)
		{
			if (this._inBytesUsed + 8 <= this._inBytesRead && this._inBytesPacket >= 8)
			{
				value = BitConverter.ToDouble(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 8;
				this._inBytesPacket -= 8;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 0, 8))
			{
				value = 0.0;
				return false;
			}
			value = BitConverter.ToDouble(this._bTmp, 0);
			return true;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0008B11C File Offset: 0x0008931C
		internal bool TryReadString(int length, out string value)
		{
			int num = length << 1;
			int index = 0;
			byte[] bytes;
			if (this._inBytesUsed + num > this._inBytesRead || this._inBytesPacket < num)
			{
				if (this._bTmp == null || this._bTmp.Length < num)
				{
					this._bTmp = new byte[num];
				}
				if (!this.TryReadByteArray(this._bTmp, 0, num))
				{
					value = null;
					return false;
				}
				bytes = this._bTmp;
			}
			else
			{
				bytes = this._inBuff;
				index = this._inBytesUsed;
				this._inBytesUsed += num;
				this._inBytesPacket -= num;
			}
			value = Encoding.Unicode.GetString(bytes, index, num);
			return true;
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0008B1C0 File Offset: 0x000893C0
		internal bool TryReadStringWithEncoding(int length, Encoding encoding, bool isPlp, out string value)
		{
			if (encoding == null)
			{
				if (isPlp)
				{
					ulong num;
					if (!this._parser.TrySkipPlpValue((ulong)((long)length), this, out num))
					{
						value = null;
						return false;
					}
				}
				else if (!this.TrySkipBytes(length))
				{
					value = null;
					return false;
				}
				this._parser.ThrowUnsupportedCollationEncountered(this);
			}
			byte[] bytes = null;
			int index = 0;
			if (isPlp)
			{
				if (!this.TryReadPlpBytes(ref bytes, 0, 2147483647, out length))
				{
					value = null;
					return false;
				}
			}
			else if (this._inBytesUsed + length > this._inBytesRead || this._inBytesPacket < length)
			{
				if (this._bTmp == null || this._bTmp.Length < length)
				{
					this._bTmp = new byte[length];
				}
				if (!this.TryReadByteArray(this._bTmp, 0, length))
				{
					value = null;
					return false;
				}
				bytes = this._bTmp;
			}
			else
			{
				bytes = this._inBuff;
				index = this._inBytesUsed;
				this._inBytesUsed += length;
				this._inBytesPacket -= length;
			}
			value = encoding.GetString(bytes, index, length);
			return true;
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0008B2B8 File Offset: 0x000894B8
		internal ulong ReadPlpLength(bool returnPlpNullIfNull)
		{
			ulong result;
			if (!this.TryReadPlpLength(returnPlpNullIfNull, out result))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return result;
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0008B2D8 File Offset: 0x000894D8
		internal bool TryReadPlpLength(bool returnPlpNullIfNull, out ulong lengthLeft)
		{
			bool flag = false;
			if (this._longlen == 0UL)
			{
				long longlen;
				if (!this.TryReadInt64(out longlen))
				{
					lengthLeft = 0UL;
					return false;
				}
				this._longlen = (ulong)longlen;
			}
			if (this._longlen == 18446744073709551615UL)
			{
				this._longlen = 0UL;
				this._longlenleft = 0UL;
				flag = true;
			}
			else
			{
				uint num;
				if (!this.TryReadUInt32(out num))
				{
					lengthLeft = 0UL;
					return false;
				}
				if (num == 0U)
				{
					this._longlenleft = 0UL;
					this._longlen = 0UL;
				}
				else
				{
					this._longlenleft = (ulong)num;
				}
			}
			if (flag && returnPlpNullIfNull)
			{
				lengthLeft = ulong.MaxValue;
				return true;
			}
			lengthLeft = this._longlenleft;
			return true;
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x0008B368 File Offset: 0x00089568
		internal int ReadPlpBytesChunk(byte[] buff, int offset, int len)
		{
			int num = (int)Math.Min(this._longlenleft, (ulong)((long)len));
			int result;
			bool flag = this.TryReadByteArray(buff, offset, num, out result);
			this._longlenleft -= (ulong)((long)num);
			if (!flag)
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return result;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0008B3A8 File Offset: 0x000895A8
		internal bool TryReadPlpBytes(ref byte[] buff, int offset, int len, out int totalBytesRead)
		{
			int num = 0;
			if (this._longlen == 0UL)
			{
				if (buff == null)
				{
					buff = Array.Empty<byte>();
				}
				totalBytesRead = 0;
				return true;
			}
			int i = len;
			if (buff == null && this._longlen != 18446744073709551614UL)
			{
				buff = new byte[Math.Min((int)this._longlen, len)];
			}
			if (this._longlenleft == 0UL)
			{
				ulong num2;
				if (!this.TryReadPlpLength(false, out num2))
				{
					totalBytesRead = 0;
					return false;
				}
				if (this._longlenleft == 0UL)
				{
					totalBytesRead = 0;
					return true;
				}
			}
			if (buff == null)
			{
				buff = new byte[this._longlenleft];
			}
			totalBytesRead = 0;
			while (i > 0)
			{
				int num3 = (int)Math.Min(this._longlenleft, (ulong)((long)i));
				if (buff.Length < offset + num3)
				{
					byte[] array = new byte[offset + num3];
					Buffer.BlockCopy(buff, 0, array, 0, offset);
					buff = array;
				}
				bool flag = this.TryReadByteArray(buff, offset, num3, out num);
				i -= num;
				offset += num;
				totalBytesRead += num;
				this._longlenleft -= (ulong)((long)num);
				if (!flag)
				{
					return false;
				}
				ulong num2;
				if (this._longlenleft == 0UL && !this.TryReadPlpLength(false, out num2))
				{
					return false;
				}
				if (this._longlenleft == 0UL)
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0008B4C0 File Offset: 0x000896C0
		internal bool TrySkipLongBytes(long num)
		{
			while (num > 0L)
			{
				int num2 = (int)Math.Min(2147483647L, num);
				if (!this.TryReadByteArray(null, 0, num2))
				{
					return false;
				}
				num -= (long)num2;
			}
			return true;
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0008B4F8 File Offset: 0x000896F8
		internal bool TrySkipBytes(int num)
		{
			return this.TryReadByteArray(null, 0, num);
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0008B503 File Offset: 0x00089703
		internal void SetSnapshot()
		{
			this._snapshot = new TdsParserStateObject.StateSnapshot(this);
			this._snapshot.Snap();
			this._snapshotReplay = false;
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0008B523 File Offset: 0x00089723
		internal void ResetSnapshot()
		{
			this._snapshot = null;
			this._snapshotReplay = false;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0008B534 File Offset: 0x00089734
		internal bool TryReadNetworkPacket()
		{
			if (this._snapshot != null)
			{
				if (this._snapshotReplay && this._snapshot.Replay())
				{
					return true;
				}
				this._inBuff = new byte[this._inBuff.Length];
			}
			if (this._syncOverAsync)
			{
				this.ReadSniSyncOverAsync();
				return true;
			}
			this.ReadSni(new TaskCompletionSource<object>());
			return false;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0008B58F File Offset: 0x0008978F
		internal void PrepareReplaySnapshot()
		{
			this._networkPacketTaskSource = null;
			this._snapshot.PrepareReplay();
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0008B5A4 File Offset: 0x000897A4
		internal void ReadSniSyncOverAsync()
		{
			if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			object obj = null;
			bool flag = false;
			try
			{
				Interlocked.Increment(ref this._readingCount);
				flag = true;
				uint num;
				obj = this.ReadSyncOverAsync(this.GetTimeoutRemaining(), out num);
				Interlocked.Decrement(ref this._readingCount);
				flag = false;
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(num, CallbackType.Read);
				}
				if (num == 0U)
				{
					this.ProcessSniPacket(obj, 0U);
				}
				else
				{
					this.ReadSniError(this, num);
				}
			}
			finally
			{
				if (flag)
				{
					Interlocked.Decrement(ref this._readingCount);
				}
				if (!this.IsPacketEmpty(obj))
				{
					this.ReleasePacket(obj);
				}
			}
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0008B660 File Offset: 0x00089860
		internal void OnConnectionClosed()
		{
			this.Parser.State = TdsParserState.Broken;
			this.Parser.Connection.BreakConnection();
			Interlocked.MemoryBarrier();
			TaskCompletionSource<object> taskCompletionSource = this._networkPacketTaskSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetException(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}
			taskCompletionSource = this._writeCompletionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetException(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0008B6C8 File Offset: 0x000898C8
		private void OnTimeout(object state)
		{
			if (!this._internalTimeout)
			{
				this._internalTimeout = true;
				lock (this)
				{
					if (!this._attentionSent)
					{
						this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U, null));
						TaskCompletionSource<object> source = this._networkPacketTaskSource;
						if (this._parser.Connection.IsInPool)
						{
							this._parser.State = TdsParserState.Broken;
							this._parser.Connection.BreakConnection();
							if (source != null)
							{
								source.TrySetCanceled();
							}
						}
						else if (this._parser.State == TdsParserState.OpenLoggedIn)
						{
							try
							{
								this.SendAttention(true);
							}
							catch (Exception e)
							{
								if (!ADP.IsCatchableExceptionType(e))
								{
									throw;
								}
								if (source != null)
								{
									source.TrySetCanceled();
								}
							}
						}
						if (source != null)
						{
							Task.Delay(5000).ContinueWith(delegate(Task _)
							{
								if (!source.Task.IsCompleted)
								{
									int num = this.IncrementPendingCallbacks();
									try
									{
										if (num == 3 && !source.Task.IsCompleted)
										{
											bool flag2 = false;
											try
											{
												this.CheckThrowSNIException();
											}
											catch (Exception exception)
											{
												if (source.TrySetException(exception))
												{
													flag2 = true;
												}
											}
											this._parser.State = TdsParserState.Broken;
											this._parser.Connection.BreakConnection();
											if (!flag2)
											{
												source.TrySetCanceled();
											}
										}
									}
									finally
									{
										this.DecrementPendingCallbacks(false);
									}
								}
							});
						}
					}
				}
			}
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0008B81C File Offset: 0x00089A1C
		internal void ReadSni(TaskCompletionSource<object> completion)
		{
			this._networkPacketTaskSource = completion;
			Interlocked.MemoryBarrier();
			if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			object obj = null;
			uint num = 0U;
			try
			{
				if (this._networkPacketTimeout == null)
				{
					this._networkPacketTimeout = ADP.UnsafeCreateTimer(new TimerCallback(this.OnTimeout), null, -1, -1);
				}
				int timeoutRemaining = this.GetTimeoutRemaining();
				if (timeoutRemaining > 0)
				{
					this.ChangeNetworkPacketTimeout(timeoutRemaining, -1);
				}
				object obj2 = null;
				Interlocked.Increment(ref this._readingCount);
				obj2 = this.SessionHandle;
				if (obj2 != null)
				{
					this.IncrementPendingCallbacks();
					obj = this.ReadAsync(out num, ref obj2);
					if (num != 0U && 997U != num)
					{
						this.DecrementPendingCallbacks(false);
					}
				}
				Interlocked.Decrement(ref this._readingCount);
				if (obj2 == null)
				{
					throw ADP.ClosedConnectionError();
				}
				if (num == 0U)
				{
					this.ReadAsyncCallback<object>(IntPtr.Zero, obj, 0U);
				}
				else if (997U != num)
				{
					this.ReadSniError(this, num);
					this._networkPacketTaskSource.TrySetResult(null);
					this.ChangeNetworkPacketTimeout(-1, -1);
				}
				else if (timeoutRemaining == 0)
				{
					this.ChangeNetworkPacketTimeout(0, -1);
				}
			}
			finally
			{
				if (!TdsParserStateObjectFactory.UseManagedSNI && !this.IsPacketEmpty(obj))
				{
					this.ReleasePacket(obj);
				}
			}
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0008B950 File Offset: 0x00089B50
		internal bool IsConnectionAlive(bool throwOnException)
		{
			bool result = true;
			if (DateTime.UtcNow.Ticks - this._lastSuccessfulIOTimer._value > 50000L)
			{
				if (this._parser == null || this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
				{
					result = false;
					if (throwOnException)
					{
						throw SQL.ConnectionDoomed();
					}
				}
				else if (this._pendingCallbacks <= 1 && (this._parser.Connection == null || this._parser.Connection.IsInPool))
				{
					object emptyReadPacket = this.EmptyReadPacket;
					try
					{
						this.SniContext = SniContext.Snix_Connect;
						uint num = this.CheckConnection();
						if (num != 0U && num != 258U)
						{
							result = false;
							if (throwOnException)
							{
								this.AddError(this._parser.ProcessSNIError(this));
								this.ThrowExceptionAndWarning(false, false);
							}
						}
						else
						{
							this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
						}
					}
					finally
					{
						if (!this.IsPacketEmpty(emptyReadPacket))
						{
							this.ReleasePacket(emptyReadPacket);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0008BA5C File Offset: 0x00089C5C
		internal bool ValidateSNIConnection()
		{
			if (this._parser == null || this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				return false;
			}
			if (DateTime.UtcNow.Ticks - this._lastSuccessfulIOTimer._value <= 50000L)
			{
				return true;
			}
			uint num = 0U;
			this.SniContext = SniContext.Snix_Connect;
			try
			{
				Interlocked.Increment(ref this._readingCount);
				num = this.CheckConnection();
			}
			finally
			{
				Interlocked.Decrement(ref this._readingCount);
			}
			return num == 0U || num == 258U;
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x0008BAFC File Offset: 0x00089CFC
		private void ReadSniError(TdsParserStateObject stateObj, uint error)
		{
			if (258U == error)
			{
				bool flag = false;
				if (this._internalTimeout)
				{
					flag = true;
				}
				else
				{
					stateObj._internalTimeout = true;
					this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U, null));
					if (!stateObj._attentionSent)
					{
						if (stateObj.Parser.State == TdsParserState.OpenLoggedIn)
						{
							stateObj.SendAttention(true);
							object obj = null;
							bool flag2 = false;
							try
							{
								Interlocked.Increment(ref this._readingCount);
								flag2 = true;
								obj = this.ReadSyncOverAsync(stateObj.GetTimeoutRemaining(), out error);
								Interlocked.Decrement(ref this._readingCount);
								flag2 = false;
								if (error == 0U)
								{
									stateObj.ProcessSniPacket(obj, 0U);
									return;
								}
								flag = true;
								goto IL_132;
							}
							finally
							{
								if (flag2)
								{
									Interlocked.Decrement(ref this._readingCount);
								}
								if (!this.IsPacketEmpty(obj))
								{
									this.ReleasePacket(obj);
								}
							}
						}
						if (this._parser._loginWithFailover)
						{
							this._parser.Disconnect();
						}
						else if (this._parser.State == TdsParserState.OpenNotLoggedIn && this._parser.Connection.ConnectionOptions.MultiSubnetFailover)
						{
							this._parser.Disconnect();
						}
						else
						{
							flag = true;
						}
					}
				}
				IL_132:
				if (flag)
				{
					this._parser.State = TdsParserState.Broken;
					this._parser.Connection.BreakConnection();
				}
			}
			else
			{
				this.AddError(this._parser.ProcessSNIError(stateObj));
			}
			this.ThrowExceptionAndWarning(false, false);
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0008BC88 File Offset: 0x00089E88
		public void ProcessSniPacket(object packet, uint error)
		{
			if (error != 0U)
			{
				if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					return;
				}
				this.AddError(this._parser.ProcessSNIError(this));
				return;
			}
			else
			{
				uint num = 0U;
				if (this.SNIPacketGetData(packet, this._inBuff, ref num) != 0U)
				{
					throw SQL.ParsingError();
				}
				if ((long)this._inBuff.Length < (long)((ulong)num))
				{
					throw SQL.InvalidInternalPacketSize(SR.GetString("Invalid array size."));
				}
				this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
				this._inBytesRead = (int)num;
				this._inBytesUsed = 0;
				if (this._snapshot != null)
				{
					this._snapshot.PushBuffer(this._inBuff, this._inBytesRead);
					if (this._snapshotReplay)
					{
						this._snapshot.Replay();
					}
				}
				this.SniReadStatisticsAndTracing();
				return;
			}
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0008BD5C File Offset: 0x00089F5C
		private void ChangeNetworkPacketTimeout(int dueTime, int period)
		{
			Timer networkPacketTimeout = this._networkPacketTimeout;
			if (networkPacketTimeout != null)
			{
				try
				{
					networkPacketTimeout.Change(dueTime, period);
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0008BD94 File Offset: 0x00089F94
		private void SetBufferSecureStrings()
		{
			if (this._securePasswords != null)
			{
				for (int i = 0; i < this._securePasswords.Length; i++)
				{
					if (this._securePasswords[i] != null)
					{
						IntPtr intPtr = IntPtr.Zero;
						try
						{
							intPtr = Marshal.SecureStringToBSTR(this._securePasswords[i]);
							byte[] array = new byte[this._securePasswords[i].Length * 2];
							Marshal.Copy(intPtr, array, 0, this._securePasswords[i].Length * 2);
							TdsParserStaticMethods.ObfuscatePassword(array);
							array.CopyTo(this._outBuff, this._securePasswordOffsetsInBuffer[i]);
						}
						finally
						{
							Marshal.ZeroFreeBSTR(intPtr);
						}
					}
				}
			}
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0008BE40 File Offset: 0x0008A040
		public void ReadAsyncCallback<T>(T packet, uint error)
		{
			this.ReadAsyncCallback<T>(IntPtr.Zero, packet, error);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0008BE50 File Offset: 0x0008A050
		public void ReadAsyncCallback<T>(IntPtr key, T packet, uint error)
		{
			TaskCompletionSource<object> source = this._networkPacketTaskSource;
			if (source == null && this._parser._pMarsPhysicalConObj == this)
			{
				return;
			}
			bool flag = true;
			try
			{
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(error, CallbackType.Read);
				}
				this.ChangeNetworkPacketTimeout(-1, -1);
				this.ProcessSniPacket(packet, error);
			}
			catch (Exception e)
			{
				flag = ADP.IsCatchableExceptionType(e);
				throw;
			}
			finally
			{
				int num = this.DecrementPendingCallbacks(false);
				if (flag && source != null && num < 2)
				{
					if (error == 0U)
					{
						if (this._executionContext != null)
						{
							ExecutionContext.Run(this._executionContext, delegate(object state)
							{
								source.TrySetResult(null);
							}, null);
						}
						else
						{
							source.TrySetResult(null);
						}
					}
					else if (this._executionContext != null)
					{
						ExecutionContext.Run(this._executionContext, delegate(object state)
						{
							this.ReadAsyncCallbackCaptureException(source);
						}, null);
					}
					else
					{
						this.ReadAsyncCallbackCaptureException(source);
					}
				}
			}
		}

		// Token: 0x06001D6E RID: 7534
		protected abstract bool CheckPacket(object packet, TaskCompletionSource<object> source);

		// Token: 0x06001D6F RID: 7535 RVA: 0x0008BF5C File Offset: 0x0008A15C
		private void ReadAsyncCallbackCaptureException(TaskCompletionSource<object> source)
		{
			bool flag = false;
			try
			{
				if (this._hasErrorOrWarning)
				{
					this.ThrowExceptionAndWarning(false, true);
				}
				else if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					throw ADP.ClosedConnectionError();
				}
			}
			catch (Exception exception)
			{
				if (source.TrySetException(exception))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				Task.Factory.StartNew(delegate()
				{
					this._parser.State = TdsParserState.Broken;
					this._parser.Connection.BreakConnection();
					source.TrySetCanceled();
				});
			}
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0008BFF4 File Offset: 0x0008A1F4
		public void WriteAsyncCallback<T>(T packet, uint sniError)
		{
			this.WriteAsyncCallback<T>(IntPtr.Zero, packet, sniError);
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0008C004 File Offset: 0x0008A204
		public void WriteAsyncCallback<T>(IntPtr key, T packet, uint sniError)
		{
			this.RemovePacketFromPendingList(packet);
			try
			{
				if (sniError != 0U)
				{
					try
					{
						this.AddError(this._parser.ProcessSNIError(this));
						this.ThrowExceptionAndWarning(false, true);
						goto IL_9E;
					}
					catch (Exception ex)
					{
						TaskCompletionSource<object> writeCompletionSource = this._writeCompletionSource;
						if (writeCompletionSource != null)
						{
							writeCompletionSource.TrySetException(ex);
						}
						else
						{
							this._delayedWriteAsyncCallbackException = ex;
							Interlocked.MemoryBarrier();
							writeCompletionSource = this._writeCompletionSource;
							if (writeCompletionSource != null)
							{
								Exception ex2 = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
								if (ex2 != null)
								{
									writeCompletionSource.TrySetException(ex2);
								}
							}
						}
						return;
					}
				}
				this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
			}
			finally
			{
				Interlocked.Decrement(ref this._asyncWriteCount);
			}
			IL_9E:
			TaskCompletionSource<object> writeCompletionSource2 = this._writeCompletionSource;
			if (this._asyncWriteCount == 0 && writeCompletionSource2 != null)
			{
				writeCompletionSource2.TrySetResult(null);
			}
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0008C0EC File Offset: 0x0008A2EC
		internal void WriteSecureString(SecureString secureString)
		{
			int num = (this._securePasswords[0] != null) ? 1 : 0;
			this._securePasswords[num] = secureString;
			this._securePasswordOffsetsInBuffer[num] = this._outBytesUsed;
			int num2 = secureString.Length * 2;
			this._outBytesUsed += num2;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x0008C138 File Offset: 0x0008A338
		internal void ResetSecurePasswordsInformation()
		{
			for (int i = 0; i < this._securePasswords.Length; i++)
			{
				this._securePasswords[i] = null;
				this._securePasswordOffsetsInBuffer[i] = 0;
			}
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0008C16C File Offset: 0x0008A36C
		internal Task WaitForAccumulatedWrites()
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			if (this._asyncWriteCount == 0)
			{
				return null;
			}
			this._writeCompletionSource = new TaskCompletionSource<object>();
			Task task = this._writeCompletionSource.Task;
			Interlocked.MemoryBarrier();
			if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
			{
				throw ADP.ClosedConnectionError();
			}
			ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			if (this._asyncWriteCount == 0 && (!task.IsCompleted || task.Exception == null))
			{
				task = null;
			}
			return task;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0008C208 File Offset: 0x0008A408
		internal void WriteByte(byte b)
		{
			if (this._outBytesUsed == this._outBuff.Length)
			{
				this.WritePacket(0, true);
			}
			byte[] outBuff = this._outBuff;
			int outBytesUsed = this._outBytesUsed;
			this._outBytesUsed = outBytesUsed + 1;
			outBuff[outBytesUsed] = b;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0008C248 File Offset: 0x0008A448
		internal Task WriteByteArray(byte[] b, int len, int offsetBuffer, bool canAccumulate = true, TaskCompletionSource<object> completion = null)
		{
			Task result2;
			try
			{
				bool asyncWrite = this._parser._asyncWrite;
				int num = offsetBuffer;
				while (this._outBytesUsed + len > this._outBuff.Length)
				{
					int num2 = this._outBuff.Length - this._outBytesUsed;
					Buffer.BlockCopy(b, num, this._outBuff, this._outBytesUsed, num2);
					num += num2;
					this._outBytesUsed += num2;
					len -= num2;
					Task task = this.WritePacket(0, canAccumulate);
					if (task != null)
					{
						Task result = null;
						if (completion == null)
						{
							completion = new TaskCompletionSource<object>();
							result = completion.Task;
						}
						this.WriteByteArraySetupContinuation(b, len, completion, num, task);
						return result;
					}
					if (len <= 0)
					{
						IL_B9:
						if (completion != null)
						{
							completion.SetResult(null);
						}
						return null;
					}
				}
				Buffer.BlockCopy(b, num, this._outBuff, this._outBytesUsed, len);
				this._outBytesUsed += len;
				goto IL_B9;
			}
			catch (Exception exception)
			{
				if (completion == null)
				{
					throw;
				}
				completion.SetException(exception);
				result2 = null;
			}
			return result2;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0008C348 File Offset: 0x0008A548
		private void WriteByteArraySetupContinuation(byte[] b, int len, TaskCompletionSource<object> completion, int offset, Task packetTask)
		{
			AsyncHelper.ContinueTask(packetTask, completion, delegate
			{
				this.WriteByteArray(b, len, offset, false, completion);
			}, this._parser.Connection, null, null, null, null);
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0008C3A8 File Offset: 0x0008A5A8
		internal Task WritePacket(byte flushMode, bool canAccumulate = false)
		{
			if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
			{
				throw ADP.ClosedConnectionError();
			}
			if ((this._parser.State == TdsParserState.OpenLoggedIn && !this._bulkCopyOpperationInProgress && this._outBytesUsed == this._outputHeaderLen + BitConverter.ToInt32(this._outBuff, this._outputHeaderLen) && this._outputPacketNumber == 1) || (this._outBytesUsed == this._outputHeaderLen && this._outputPacketNumber == 1))
			{
				return null;
			}
			byte outputPacketNumber = this._outputPacketNumber;
			bool flag = this._cancelled && this._parser._asyncWrite;
			byte b;
			if (flag)
			{
				b = 3;
				this._outputPacketNumber = 1;
			}
			else if (1 == flushMode)
			{
				b = 1;
				this._outputPacketNumber = 1;
			}
			else if (flushMode == 0)
			{
				b = 4;
				this._outputPacketNumber += 1;
			}
			else
			{
				b = 1;
			}
			this._outBuff[0] = this._outputMessageType;
			this._outBuff[1] = b;
			this._outBuff[2] = (byte)(this._outBytesUsed >> 8);
			this._outBuff[3] = (byte)(this._outBytesUsed & 255);
			this._outBuff[4] = 0;
			this._outBuff[5] = 0;
			this._outBuff[6] = outputPacketNumber;
			this._outBuff[7] = 0;
			this._parser.CheckResetConnection(this);
			Task task = this.WriteSni(canAccumulate);
			if (flag)
			{
				task = AsyncHelper.CreateContinuationTask(task, new Action(this.CancelWritePacket), this._parser.Connection, null);
			}
			return task;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0008C51C File Offset: 0x0008A71C
		private void CancelWritePacket()
		{
			this._parser.Connection.ThreadHasParserLockForClose = true;
			try
			{
				this.SendAttention(false);
				this.ResetCancelAndProcessAttention();
				throw SQL.OperationCancelled();
			}
			finally
			{
				this._parser.Connection.ThreadHasParserLockForClose = false;
			}
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0008C570 File Offset: 0x0008A770
		private Task SNIWritePacket(object packet, out uint sniError, bool canAccumulate, bool callerHasConnectionLock)
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			Task task = null;
			this._writeCompletionSource = null;
			object pointer = this.EmptyReadPacket;
			bool flag = !this._parser._asyncWrite;
			if (flag && this._asyncWriteCount > 0)
			{
				Task task2 = this.WaitForAccumulatedWrites();
				if (task2 != null)
				{
					try
					{
						task2.Wait();
					}
					catch (AggregateException ex2)
					{
						throw ex2.InnerException;
					}
				}
			}
			if (!flag)
			{
				pointer = this.AddPacketToPendingList(packet);
			}
			try
			{
			}
			finally
			{
				sniError = this.WritePacket(packet, flag);
			}
			if (sniError == 997U)
			{
				Interlocked.Increment(ref this._asyncWriteCount);
				if (!canAccumulate)
				{
					this._writeCompletionSource = new TaskCompletionSource<object>();
					task = this._writeCompletionSource.Task;
					Interlocked.MemoryBarrier();
					ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
					if (ex != null)
					{
						throw ex;
					}
					if (this._asyncWriteCount == 0 && (!task.IsCompleted || task.Exception == null))
					{
						task = null;
					}
				}
			}
			else
			{
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(sniError, CallbackType.Write);
				}
				if (sniError == 0U)
				{
					this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
					if (!flag)
					{
						this.RemovePacketFromPendingList(pointer);
					}
				}
				else
				{
					this.AddError(this._parser.ProcessSNIError(this));
					this.ThrowExceptionAndWarning(callerHasConnectionLock, false);
				}
			}
			return task;
		}

		// Token: 0x06001D7B RID: 7547
		internal abstract bool IsValidPacket(object packetPointer);

		// Token: 0x06001D7C RID: 7548
		internal abstract uint WritePacket(object packet, bool sync);

		// Token: 0x06001D7D RID: 7549 RVA: 0x0008C6D4 File Offset: 0x0008A8D4
		internal void SendAttention(bool mustTakeWriteLock = false)
		{
			if (!this._attentionSent)
			{
				if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					return;
				}
				object packet = this.CreateAndSetAttentionPacket();
				try
				{
					this._attentionSending = true;
					bool flag = false;
					if (mustTakeWriteLock && !this._parser.Connection.ThreadHasParserLockForClose)
					{
						flag = true;
						this._parser.Connection._parserLock.Wait(false);
						this._parser.Connection.ThreadHasParserLockForClose = true;
					}
					try
					{
						if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
						{
							return;
						}
						this._parser._asyncWrite = false;
						uint num;
						this.SNIWritePacket(packet, out num, false, false);
					}
					finally
					{
						if (flag)
						{
							this._parser.Connection.ThreadHasParserLockForClose = false;
							this._parser.Connection._parserLock.Release();
						}
					}
					this.SetTimeoutSeconds(5);
					this._attentionSent = true;
				}
				finally
				{
					this._attentionSending = false;
				}
			}
		}

		// Token: 0x06001D7E RID: 7550
		internal abstract object CreateAndSetAttentionPacket();

		// Token: 0x06001D7F RID: 7551
		internal abstract void SetPacketData(object packet, byte[] buffer, int bytesUsed);

		// Token: 0x06001D80 RID: 7552 RVA: 0x0008C7F4 File Offset: 0x0008A9F4
		private Task WriteSni(bool canAccumulate)
		{
			object resetWritePacket = this.GetResetWritePacket();
			this.SetBufferSecureStrings();
			this.SetPacketData(resetWritePacket, this._outBuff, this._outBytesUsed);
			uint num;
			Task result = this.SNIWritePacket(resetWritePacket, out num, canAccumulate, true);
			if (this._bulkCopyOpperationInProgress && this.GetTimeoutRemaining() == 0)
			{
				this._parser.Connection.ThreadHasParserLockForClose = true;
				try
				{
					this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U, null));
					this._bulkCopyWriteTimeout = true;
					this.SendAttention(false);
					this._parser.ProcessPendingAck(this);
					this.ThrowExceptionAndWarning(false, false);
				}
				finally
				{
					this._parser.Connection.ThreadHasParserLockForClose = false;
				}
			}
			if (this._parser.State == TdsParserState.OpenNotLoggedIn && this._parser.EncryptionOptions == EncryptionOptions.LOGIN)
			{
				this._parser.RemoveEncryption();
				this._parser.EncryptionOptions = EncryptionOptions.OFF;
				this.ClearAllWritePackets();
			}
			this.SniWriteStatisticsAndTracing();
			this.ResetBuffer();
			return result;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0008C91C File Offset: 0x0008AB1C
		private void SniReadStatisticsAndTracing()
		{
			SqlStatistics statistics = this.Parser.Statistics;
			if (statistics != null)
			{
				if (statistics.WaitForReply)
				{
					statistics.SafeIncrement(ref statistics._serverRoundtrips);
					statistics.ReleaseAndUpdateNetworkServerTimer();
				}
				statistics.SafeAdd(ref statistics._bytesReceived, (long)this._inBytesRead);
				statistics.SafeIncrement(ref statistics._buffersReceived);
			}
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0008C974 File Offset: 0x0008AB74
		private void SniWriteStatisticsAndTracing()
		{
			SqlStatistics statistics = this._parser.Statistics;
			if (statistics != null)
			{
				statistics.SafeIncrement(ref statistics._buffersSent);
				statistics.SafeAdd(ref statistics._bytesSent, (long)this._outBytesUsed);
				statistics.RequestNetworkServerTimer();
			}
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0008C9B8 File Offset: 0x0008ABB8
		[Conditional("DEBUG")]
		private void AssertValidState()
		{
			if (this._inBytesUsed < 0 || this._inBytesRead < 0)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "either _inBytesUsed or _inBytesRead is negative: {0}, {1}", this._inBytesUsed, this._inBytesRead);
			}
			else if (this._inBytesUsed > this._inBytesRead)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "_inBytesUsed > _inBytesRead: {0} > {1}", this._inBytesUsed, this._inBytesRead);
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x0008CA37 File Offset: 0x0008AC37
		internal bool HasErrorOrWarning
		{
			get
			{
				return this._hasErrorOrWarning;
			}
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0008CA40 File Offset: 0x0008AC40
		internal void AddError(SqlError error)
		{
			this._syncOverAsync = true;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = true;
				if (this._errors == null)
				{
					this._errors = new SqlErrorCollection();
				}
				this._errors.Add(error);
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x0008CAA8 File Offset: 0x0008ACA8
		internal int ErrorCount
		{
			get
			{
				int result = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._errors != null)
					{
						result = this._errors.Count;
					}
				}
				return result;
			}
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0008CAFC File Offset: 0x0008ACFC
		internal void AddWarning(SqlError error)
		{
			this._syncOverAsync = true;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = true;
				if (this._warnings == null)
				{
					this._warnings = new SqlErrorCollection();
				}
				this._warnings.Add(error);
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x0008CB64 File Offset: 0x0008AD64
		internal int WarningCount
		{
			get
			{
				int result = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._warnings != null)
					{
						result = this._warnings.Count;
					}
				}
				return result;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001D89 RID: 7561
		protected abstract object EmptyReadPacket { get; }

		// Token: 0x06001D8A RID: 7562 RVA: 0x0008CBB8 File Offset: 0x0008ADB8
		internal SqlErrorCollection GetFullErrorAndWarningCollection(out bool broken)
		{
			SqlErrorCollection result = new SqlErrorCollection();
			broken = false;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = false;
				this.AddErrorsToCollection(this._errors, ref result, ref broken);
				this.AddErrorsToCollection(this._warnings, ref result, ref broken);
				this._errors = null;
				this._warnings = null;
				this.AddErrorsToCollection(this._preAttentionErrors, ref result, ref broken);
				this.AddErrorsToCollection(this._preAttentionWarnings, ref result, ref broken);
				this._preAttentionErrors = null;
				this._preAttentionWarnings = null;
			}
			return result;
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0008CC5C File Offset: 0x0008AE5C
		private void AddErrorsToCollection(SqlErrorCollection inCollection, ref SqlErrorCollection collectionToAddTo, ref bool broken)
		{
			if (inCollection != null)
			{
				foreach (object obj in inCollection)
				{
					SqlError sqlError = (SqlError)obj;
					collectionToAddTo.Add(sqlError);
					broken |= (sqlError.Class >= 20);
				}
			}
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0008CCC8 File Offset: 0x0008AEC8
		internal void StoreErrorAndWarningForAttention()
		{
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = false;
				this._preAttentionErrors = this._errors;
				this._preAttentionWarnings = this._warnings;
				this._errors = null;
				this._warnings = null;
			}
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0008CD30 File Offset: 0x0008AF30
		internal void RestoreErrorAndWarningAfterAttention()
		{
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = ((this._preAttentionErrors != null && this._preAttentionErrors.Count > 0) || (this._preAttentionWarnings != null && this._preAttentionWarnings.Count > 0));
				this._errors = this._preAttentionErrors;
				this._warnings = this._preAttentionWarnings;
				this._preAttentionErrors = null;
				this._preAttentionWarnings = null;
			}
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0008CDC8 File Offset: 0x0008AFC8
		internal void CheckThrowSNIException()
		{
			if (this.HasErrorOrWarning)
			{
				this.ThrowExceptionAndWarning(false, false);
			}
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0008CDDC File Offset: 0x0008AFDC
		[Conditional("DEBUG")]
		internal void AssertStateIsClean()
		{
			TdsParser parser = this._parser;
			if (parser != null && parser.State != TdsParserState.Closed)
			{
				TdsParserState state = parser.State;
			}
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0008CE04 File Offset: 0x0008B004
		internal void CloneCleanupAltMetaDataSetArray()
		{
			if (this._snapshot != null)
			{
				this._snapshot.CloneCleanupAltMetaDataSetArray();
			}
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0008CE19 File Offset: 0x0008B019
		[CompilerGenerated]
		private bool <DisposeCounters>b__137_0()
		{
			return Volatile.Read(ref this._readingCount) == 0;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0008CE29 File Offset: 0x0008B029
		[CompilerGenerated]
		private void <ExecuteFlush>b__144_0()
		{
			this._pendingData = true;
			this._messageStatus = 0;
		}

		// Token: 0x04001430 RID: 5168
		private const int AttentionTimeoutSeconds = 5;

		// Token: 0x04001431 RID: 5169
		private const long CheckConnectionWindow = 50000L;

		// Token: 0x04001432 RID: 5170
		protected readonly TdsParser _parser;

		// Token: 0x04001433 RID: 5171
		private readonly WeakReference _owner = new WeakReference(null);

		// Token: 0x04001434 RID: 5172
		internal SqlDataReader.SharedState _readerState;

		// Token: 0x04001435 RID: 5173
		private int _activateCount;

		// Token: 0x04001436 RID: 5174
		internal readonly int _inputHeaderLen = 8;

		// Token: 0x04001437 RID: 5175
		internal readonly int _outputHeaderLen = 8;

		// Token: 0x04001438 RID: 5176
		internal byte[] _outBuff;

		// Token: 0x04001439 RID: 5177
		internal int _outBytesUsed = 8;

		// Token: 0x0400143A RID: 5178
		protected byte[] _inBuff;

		// Token: 0x0400143B RID: 5179
		internal int _inBytesUsed;

		// Token: 0x0400143C RID: 5180
		internal int _inBytesRead;

		// Token: 0x0400143D RID: 5181
		internal int _inBytesPacket;

		// Token: 0x0400143E RID: 5182
		internal byte _outputMessageType;

		// Token: 0x0400143F RID: 5183
		internal byte _messageStatus;

		// Token: 0x04001440 RID: 5184
		internal byte _outputPacketNumber = 1;

		// Token: 0x04001441 RID: 5185
		internal bool _pendingData;

		// Token: 0x04001442 RID: 5186
		internal volatile bool _fResetEventOwned;

		// Token: 0x04001443 RID: 5187
		internal volatile bool _fResetConnectionSent;

		// Token: 0x04001444 RID: 5188
		internal bool _errorTokenReceived;

		// Token: 0x04001445 RID: 5189
		internal bool _bulkCopyOpperationInProgress;

		// Token: 0x04001446 RID: 5190
		internal bool _bulkCopyWriteTimeout;

		// Token: 0x04001447 RID: 5191
		protected readonly object _writePacketLockObject = new object();

		// Token: 0x04001448 RID: 5192
		private int _pendingCallbacks;

		// Token: 0x04001449 RID: 5193
		private long _timeoutMilliseconds;

		// Token: 0x0400144A RID: 5194
		private long _timeoutTime;

		// Token: 0x0400144B RID: 5195
		internal volatile bool _attentionSent;

		// Token: 0x0400144C RID: 5196
		internal bool _attentionReceived;

		// Token: 0x0400144D RID: 5197
		internal volatile bool _attentionSending;

		// Token: 0x0400144E RID: 5198
		internal bool _internalTimeout;

		// Token: 0x0400144F RID: 5199
		private readonly LastIOTimer _lastSuccessfulIOTimer;

		// Token: 0x04001450 RID: 5200
		private SecureString[] _securePasswords = new SecureString[2];

		// Token: 0x04001451 RID: 5201
		private int[] _securePasswordOffsetsInBuffer = new int[2];

		// Token: 0x04001452 RID: 5202
		private bool _cancelled;

		// Token: 0x04001453 RID: 5203
		private const int _waitForCancellationLockPollTimeout = 100;

		// Token: 0x04001454 RID: 5204
		private WeakReference _cancellationOwner = new WeakReference(null);

		// Token: 0x04001455 RID: 5205
		internal bool _hasOpenResult;

		// Token: 0x04001456 RID: 5206
		internal SqlInternalTransaction _executedUnderTransaction;

		// Token: 0x04001457 RID: 5207
		internal ulong _longlen;

		// Token: 0x04001458 RID: 5208
		internal ulong _longlenleft;

		// Token: 0x04001459 RID: 5209
		internal int[] _decimalBits;

		// Token: 0x0400145A RID: 5210
		internal byte[] _bTmp = new byte[12];

		// Token: 0x0400145B RID: 5211
		internal int _bTmpRead;

		// Token: 0x0400145C RID: 5212
		internal Decoder _plpdecoder;

		// Token: 0x0400145D RID: 5213
		internal bool _accumulateInfoEvents;

		// Token: 0x0400145E RID: 5214
		internal List<SqlError> _pendingInfoEvents;

		// Token: 0x0400145F RID: 5215
		private byte[] _partialHeaderBuffer = new byte[8];

		// Token: 0x04001460 RID: 5216
		internal int _partialHeaderBytesRead;

		// Token: 0x04001461 RID: 5217
		internal _SqlMetaDataSet _cleanupMetaData;

		// Token: 0x04001462 RID: 5218
		internal _SqlMetaDataSetCollection _cleanupAltMetaDataSetArray;

		// Token: 0x04001463 RID: 5219
		internal bool _receivedColMetaData;

		// Token: 0x04001464 RID: 5220
		private SniContext _sniContext;

		// Token: 0x04001465 RID: 5221
		private bool _bcpLock;

		// Token: 0x04001466 RID: 5222
		private TdsParserStateObject.NullBitmap _nullBitmapInfo;

		// Token: 0x04001467 RID: 5223
		internal TaskCompletionSource<object> _networkPacketTaskSource;

		// Token: 0x04001468 RID: 5224
		private Timer _networkPacketTimeout;

		// Token: 0x04001469 RID: 5225
		internal bool _syncOverAsync = true;

		// Token: 0x0400146A RID: 5226
		private bool _snapshotReplay;

		// Token: 0x0400146B RID: 5227
		private TdsParserStateObject.StateSnapshot _snapshot;

		// Token: 0x0400146C RID: 5228
		internal ExecutionContext _executionContext;

		// Token: 0x0400146D RID: 5229
		internal bool _asyncReadWithoutSnapshot;

		// Token: 0x0400146E RID: 5230
		internal SqlErrorCollection _errors;

		// Token: 0x0400146F RID: 5231
		internal SqlErrorCollection _warnings;

		// Token: 0x04001470 RID: 5232
		internal object _errorAndWarningsLock = new object();

		// Token: 0x04001471 RID: 5233
		private bool _hasErrorOrWarning;

		// Token: 0x04001472 RID: 5234
		internal SqlErrorCollection _preAttentionErrors;

		// Token: 0x04001473 RID: 5235
		internal SqlErrorCollection _preAttentionWarnings;

		// Token: 0x04001474 RID: 5236
		private volatile TaskCompletionSource<object> _writeCompletionSource;

		// Token: 0x04001475 RID: 5237
		protected volatile int _asyncWriteCount;

		// Token: 0x04001476 RID: 5238
		private volatile Exception _delayedWriteAsyncCallbackException;

		// Token: 0x04001477 RID: 5239
		private int _readingCount;

		// Token: 0x02000272 RID: 626
		private struct NullBitmap
		{
			// Token: 0x06001D93 RID: 7571 RVA: 0x0008CE3C File Offset: 0x0008B03C
			internal bool TryInitialize(TdsParserStateObject stateObj, int columnsCount)
			{
				this._columnsCount = columnsCount;
				int num = (columnsCount + 7) / 8;
				if (this._nullBitmap == null || this._nullBitmap.Length != num)
				{
					this._nullBitmap = new byte[num];
				}
				return stateObj.TryReadByteArray(this._nullBitmap, 0, this._nullBitmap.Length);
			}

			// Token: 0x06001D94 RID: 7572 RVA: 0x0008CE8F File Offset: 0x0008B08F
			internal bool ReferenceEquals(TdsParserStateObject.NullBitmap obj)
			{
				return this._nullBitmap == obj._nullBitmap;
			}

			// Token: 0x06001D95 RID: 7573 RVA: 0x0008CEA0 File Offset: 0x0008B0A0
			internal TdsParserStateObject.NullBitmap Clone()
			{
				return new TdsParserStateObject.NullBitmap
				{
					_nullBitmap = ((this._nullBitmap == null) ? null : ((byte[])this._nullBitmap.Clone())),
					_columnsCount = this._columnsCount
				};
			}

			// Token: 0x06001D96 RID: 7574 RVA: 0x0008CEE5 File Offset: 0x0008B0E5
			internal void Clean()
			{
				this._columnsCount = 0;
			}

			// Token: 0x06001D97 RID: 7575 RVA: 0x0008CEF0 File Offset: 0x0008B0F0
			internal bool IsGuaranteedNull(int columnOrdinal)
			{
				if (this._columnsCount == 0)
				{
					return false;
				}
				byte b = (byte)(1 << (columnOrdinal & 7));
				byte b2 = this._nullBitmap[columnOrdinal >> 3];
				return (b & b2) > 0;
			}

			// Token: 0x04001478 RID: 5240
			private byte[] _nullBitmap;

			// Token: 0x04001479 RID: 5241
			private int _columnsCount;
		}

		// Token: 0x02000273 RID: 627
		private class PacketData
		{
			// Token: 0x06001D98 RID: 7576 RVA: 0x00003D93 File Offset: 0x00001F93
			public PacketData()
			{
			}

			// Token: 0x0400147A RID: 5242
			public byte[] Buffer;

			// Token: 0x0400147B RID: 5243
			public int Read;
		}

		// Token: 0x02000274 RID: 628
		private class StateSnapshot
		{
			// Token: 0x06001D99 RID: 7577 RVA: 0x0008CF20 File Offset: 0x0008B120
			public StateSnapshot(TdsParserStateObject state)
			{
				this._snapshotInBuffs = new List<TdsParserStateObject.PacketData>();
				this._stateObj = state;
			}

			// Token: 0x06001D9A RID: 7578 RVA: 0x0008CF3A File Offset: 0x0008B13A
			internal void CloneNullBitmapInfo()
			{
				if (this._stateObj._nullBitmapInfo.ReferenceEquals(this._snapshotNullBitmapInfo))
				{
					this._stateObj._nullBitmapInfo = this._stateObj._nullBitmapInfo.Clone();
				}
			}

			// Token: 0x06001D9B RID: 7579 RVA: 0x0008CF70 File Offset: 0x0008B170
			internal void CloneCleanupAltMetaDataSetArray()
			{
				if (this._stateObj._cleanupAltMetaDataSetArray != null && this._snapshotCleanupAltMetaDataSetArray == this._stateObj._cleanupAltMetaDataSetArray)
				{
					this._stateObj._cleanupAltMetaDataSetArray = (_SqlMetaDataSetCollection)this._stateObj._cleanupAltMetaDataSetArray.Clone();
				}
			}

			// Token: 0x06001D9C RID: 7580 RVA: 0x0008CFC0 File Offset: 0x0008B1C0
			internal void PushBuffer(byte[] buffer, int read)
			{
				TdsParserStateObject.PacketData packetData = new TdsParserStateObject.PacketData();
				packetData.Buffer = buffer;
				packetData.Read = read;
				this._snapshotInBuffs.Add(packetData);
			}

			// Token: 0x06001D9D RID: 7581 RVA: 0x0008CFF0 File Offset: 0x0008B1F0
			internal bool Replay()
			{
				if (this._snapshotInBuffCurrent < this._snapshotInBuffs.Count)
				{
					TdsParserStateObject.PacketData packetData = this._snapshotInBuffs[this._snapshotInBuffCurrent];
					this._stateObj._inBuff = packetData.Buffer;
					this._stateObj._inBytesUsed = 0;
					this._stateObj._inBytesRead = packetData.Read;
					this._snapshotInBuffCurrent++;
					return true;
				}
				return false;
			}

			// Token: 0x06001D9E RID: 7582 RVA: 0x0008D064 File Offset: 0x0008B264
			internal void Snap()
			{
				this._snapshotInBuffs.Clear();
				this._snapshotInBuffCurrent = 0;
				this._snapshotInBytesUsed = this._stateObj._inBytesUsed;
				this._snapshotInBytesPacket = this._stateObj._inBytesPacket;
				this._snapshotPendingData = this._stateObj._pendingData;
				this._snapshotErrorTokenReceived = this._stateObj._errorTokenReceived;
				this._snapshotMessageStatus = this._stateObj._messageStatus;
				this._snapshotNullBitmapInfo = this._stateObj._nullBitmapInfo;
				this._snapshotLongLen = this._stateObj._longlen;
				this._snapshotLongLenLeft = this._stateObj._longlenleft;
				this._snapshotCleanupMetaData = this._stateObj._cleanupMetaData;
				this._snapshotCleanupAltMetaDataSetArray = this._stateObj._cleanupAltMetaDataSetArray;
				this._snapshotHasOpenResult = this._stateObj._hasOpenResult;
				this._snapshotReceivedColumnMetadata = this._stateObj._receivedColMetaData;
				this._snapshotAttentionReceived = this._stateObj._attentionReceived;
				this.PushBuffer(this._stateObj._inBuff, this._stateObj._inBytesRead);
			}

			// Token: 0x06001D9F RID: 7583 RVA: 0x0008D17C File Offset: 0x0008B37C
			internal void ResetSnapshotState()
			{
				this._snapshotInBuffCurrent = 0;
				this.Replay();
				this._stateObj._inBytesUsed = this._snapshotInBytesUsed;
				this._stateObj._inBytesPacket = this._snapshotInBytesPacket;
				this._stateObj._pendingData = this._snapshotPendingData;
				this._stateObj._errorTokenReceived = this._snapshotErrorTokenReceived;
				this._stateObj._messageStatus = this._snapshotMessageStatus;
				this._stateObj._nullBitmapInfo = this._snapshotNullBitmapInfo;
				this._stateObj._cleanupMetaData = this._snapshotCleanupMetaData;
				this._stateObj._cleanupAltMetaDataSetArray = this._snapshotCleanupAltMetaDataSetArray;
				this._stateObj._hasOpenResult = this._snapshotHasOpenResult;
				this._stateObj._receivedColMetaData = this._snapshotReceivedColumnMetadata;
				this._stateObj._attentionReceived = this._snapshotAttentionReceived;
				this._stateObj._bTmpRead = 0;
				this._stateObj._partialHeaderBytesRead = 0;
				this._stateObj._longlen = this._snapshotLongLen;
				this._stateObj._longlenleft = this._snapshotLongLenLeft;
				this._stateObj._snapshotReplay = true;
			}

			// Token: 0x06001DA0 RID: 7584 RVA: 0x0008D298 File Offset: 0x0008B498
			internal void PrepareReplay()
			{
				this.ResetSnapshotState();
			}

			// Token: 0x0400147C RID: 5244
			private List<TdsParserStateObject.PacketData> _snapshotInBuffs;

			// Token: 0x0400147D RID: 5245
			private int _snapshotInBuffCurrent;

			// Token: 0x0400147E RID: 5246
			private int _snapshotInBytesUsed;

			// Token: 0x0400147F RID: 5247
			private int _snapshotInBytesPacket;

			// Token: 0x04001480 RID: 5248
			private bool _snapshotPendingData;

			// Token: 0x04001481 RID: 5249
			private bool _snapshotErrorTokenReceived;

			// Token: 0x04001482 RID: 5250
			private bool _snapshotHasOpenResult;

			// Token: 0x04001483 RID: 5251
			private bool _snapshotReceivedColumnMetadata;

			// Token: 0x04001484 RID: 5252
			private bool _snapshotAttentionReceived;

			// Token: 0x04001485 RID: 5253
			private byte _snapshotMessageStatus;

			// Token: 0x04001486 RID: 5254
			private TdsParserStateObject.NullBitmap _snapshotNullBitmapInfo;

			// Token: 0x04001487 RID: 5255
			private ulong _snapshotLongLen;

			// Token: 0x04001488 RID: 5256
			private ulong _snapshotLongLenLeft;

			// Token: 0x04001489 RID: 5257
			private _SqlMetaDataSet _snapshotCleanupMetaData;

			// Token: 0x0400148A RID: 5258
			private _SqlMetaDataSetCollection _snapshotCleanupAltMetaDataSetArray;

			// Token: 0x0400148B RID: 5259
			private readonly TdsParserStateObject _stateObj;
		}

		// Token: 0x02000275 RID: 629
		[CompilerGenerated]
		private sealed class <>c__DisplayClass175_0
		{
			// Token: 0x06001DA1 RID: 7585 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass175_0()
			{
			}

			// Token: 0x06001DA2 RID: 7586 RVA: 0x0008D2A0 File Offset: 0x0008B4A0
			internal void <OnTimeout>b__0(Task _)
			{
				if (!this.source.Task.IsCompleted)
				{
					int num = this.<>4__this.IncrementPendingCallbacks();
					try
					{
						if (num == 3 && !this.source.Task.IsCompleted)
						{
							bool flag = false;
							try
							{
								this.<>4__this.CheckThrowSNIException();
							}
							catch (Exception exception)
							{
								if (this.source.TrySetException(exception))
								{
									flag = true;
								}
							}
							this.<>4__this._parser.State = TdsParserState.Broken;
							this.<>4__this._parser.Connection.BreakConnection();
							if (!flag)
							{
								this.source.TrySetCanceled();
							}
						}
					}
					finally
					{
						this.<>4__this.DecrementPendingCallbacks(false);
					}
				}
			}

			// Token: 0x0400148C RID: 5260
			public TdsParserStateObject <>4__this;

			// Token: 0x0400148D RID: 5261
			public TaskCompletionSource<object> source;
		}

		// Token: 0x02000276 RID: 630
		[CompilerGenerated]
		private sealed class <>c__DisplayClass184_0<T>
		{
			// Token: 0x06001DA3 RID: 7587 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass184_0()
			{
			}

			// Token: 0x06001DA4 RID: 7588 RVA: 0x0008D368 File Offset: 0x0008B568
			internal void <ReadAsyncCallback>b__0(object state)
			{
				this.source.TrySetResult(null);
			}

			// Token: 0x06001DA5 RID: 7589 RVA: 0x0008D377 File Offset: 0x0008B577
			internal void <ReadAsyncCallback>b__1(object state)
			{
				this.<>4__this.ReadAsyncCallbackCaptureException(this.source);
			}

			// Token: 0x0400148E RID: 5262
			public TaskCompletionSource<object> source;

			// Token: 0x0400148F RID: 5263
			public TdsParserStateObject <>4__this;
		}

		// Token: 0x02000277 RID: 631
		[CompilerGenerated]
		private sealed class <>c__DisplayClass186_0
		{
			// Token: 0x06001DA6 RID: 7590 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass186_0()
			{
			}

			// Token: 0x06001DA7 RID: 7591 RVA: 0x0008D38A File Offset: 0x0008B58A
			internal void <ReadAsyncCallbackCaptureException>b__0()
			{
				this.<>4__this._parser.State = TdsParserState.Broken;
				this.<>4__this._parser.Connection.BreakConnection();
				this.source.TrySetCanceled();
			}

			// Token: 0x04001490 RID: 5264
			public TdsParserStateObject <>4__this;

			// Token: 0x04001491 RID: 5265
			public TaskCompletionSource<object> source;
		}

		// Token: 0x02000278 RID: 632
		[CompilerGenerated]
		private sealed class <>c__DisplayClass194_0
		{
			// Token: 0x06001DA8 RID: 7592 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass194_0()
			{
			}

			// Token: 0x06001DA9 RID: 7593 RVA: 0x0008D3BE File Offset: 0x0008B5BE
			internal void <WriteByteArraySetupContinuation>b__0()
			{
				this.<>4__this.WriteByteArray(this.b, this.len, this.offset, false, this.completion);
			}

			// Token: 0x04001492 RID: 5266
			public TdsParserStateObject <>4__this;

			// Token: 0x04001493 RID: 5267
			public byte[] b;

			// Token: 0x04001494 RID: 5268
			public int len;

			// Token: 0x04001495 RID: 5269
			public int offset;

			// Token: 0x04001496 RID: 5270
			public TaskCompletionSource<object> completion;
		}
	}
}
