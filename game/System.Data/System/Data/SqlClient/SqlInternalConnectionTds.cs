using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x02000209 RID: 521
	internal sealed class SqlInternalConnectionTds : SqlInternalConnection, IDisposable
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x00074D2C File Offset: 0x00072F2C
		internal SessionData CurrentSessionData
		{
			get
			{
				if (this._currentSessionData != null)
				{
					this._currentSessionData._database = base.CurrentDatabase;
					this._currentSessionData._language = this._currentLanguage;
				}
				return this._currentSessionData;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x00074D5E File Offset: 0x00072F5E
		internal SqlConnectionTimeoutErrorInternal TimeoutErrorInternal
		{
			get
			{
				return this._timeoutErrorInternal;
			}
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00074D68 File Offset: 0x00072F68
		internal SqlInternalConnectionTds(DbConnectionPoolIdentity identity, SqlConnectionString connectionOptions, SqlCredential credential, object providerInfo, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance, SqlConnectionString userConnectionOptions = null, SessionData reconnectSessionData = null, bool applyTransientFaultHandling = false, string accessToken = null) : base(connectionOptions)
		{
			if (connectionOptions.ConnectRetryCount > 0)
			{
				this._recoverySessionData = reconnectSessionData;
				if (reconnectSessionData == null)
				{
					this._currentSessionData = new SessionData();
				}
				else
				{
					this._currentSessionData = new SessionData(this._recoverySessionData);
					this._originalDatabase = this._recoverySessionData._initialDatabase;
					this._originalLanguage = this._recoverySessionData._initialLanguage;
				}
			}
			if (accessToken != null)
			{
				this._accessTokenInBytes = Encoding.Unicode.GetBytes(accessToken);
			}
			this._identity = identity;
			this._poolGroupProviderInfo = (SqlConnectionPoolGroupProviderInfo)providerInfo;
			this._fResetConnection = connectionOptions.ConnectionReset;
			if (this._fResetConnection && this._recoverySessionData == null)
			{
				this._originalDatabase = connectionOptions.InitialCatalog;
				this._originalLanguage = connectionOptions.CurrentLanguage;
			}
			this._timeoutErrorInternal = new SqlConnectionTimeoutErrorInternal();
			this._credential = credential;
			this._parserLock.Wait(false);
			this.ThreadHasParserLockForClose = true;
			try
			{
				this._timeout = TimeoutTimer.StartSecondsTimeout(connectionOptions.ConnectTimeout);
				int num = applyTransientFaultHandling ? (connectionOptions.ConnectRetryCount + 1) : 1;
				int num2 = connectionOptions.ConnectRetryInterval * 1000;
				for (int i = 0; i < num; i++)
				{
					try
					{
						this.OpenLoginEnlist(this._timeout, connectionOptions, credential, newPassword, newSecurePassword, redirectedUserInstance);
						break;
					}
					catch (SqlException ex)
					{
						if (i + 1 == num || !applyTransientFaultHandling || this._timeout.IsExpired || this._timeout.MillisecondsRemaining < (long)num2 || !this.IsTransientError(ex))
						{
							throw ex;
						}
						Thread.Sleep(num2);
					}
				}
			}
			finally
			{
				this.ThreadHasParserLockForClose = false;
				this._parserLock.Release();
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00074F44 File Offset: 0x00073144
		private bool IsTransientError(SqlException exc)
		{
			if (exc == null)
			{
				return false;
			}
			foreach (object obj in exc.Errors)
			{
				SqlError sqlError = (SqlError)obj;
				if (SqlInternalConnectionTds.s_transientErrors.Contains(sqlError.Number))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00074FB4 File Offset: 0x000731B4
		internal Guid ClientConnectionId
		{
			get
			{
				return this._clientConnectionId;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x00074FBC File Offset: 0x000731BC
		internal Guid OriginalClientConnectionId
		{
			get
			{
				return this._originalClientConnectionId;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x00074FC4 File Offset: 0x000731C4
		internal string RoutingDestination
		{
			get
			{
				return this._routingDestination;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00074FCC File Offset: 0x000731CC
		internal override SqlInternalTransaction CurrentTransaction
		{
			get
			{
				return this._parser.CurrentTransaction;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x00074FD9 File Offset: 0x000731D9
		internal override SqlInternalTransaction AvailableInternalTransaction
		{
			get
			{
				if (!this._parser._fResetConnection)
				{
					return this.CurrentTransaction;
				}
				return null;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x00074FF2 File Offset: 0x000731F2
		internal override SqlInternalTransaction PendingTransaction
		{
			get
			{
				return this._parser.PendingTransaction;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x00074FFF File Offset: 0x000731FF
		internal DbConnectionPoolIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x00075007 File Offset: 0x00073207
		internal string InstanceName
		{
			get
			{
				return this._instanceName;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x0007500F File Offset: 0x0007320F
		internal override bool IsLockedForBulkCopy
		{
			get
			{
				return !this.Parser.MARSOn && this.Parser._physicalStateObj.BcpLock;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x00075030 File Offset: 0x00073230
		protected internal override bool IsNonPoolableTransactionRoot
		{
			get
			{
				return this.IsTransactionRoot && (!this.IsKatmaiOrNewer || base.Pool == null);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x0007504F File Offset: 0x0007324F
		internal override bool IsKatmaiOrNewer
		{
			get
			{
				return this._parser.IsKatmaiOrNewer;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x0007505C File Offset: 0x0007325C
		internal int PacketSize
		{
			get
			{
				return this._currentPacketSize;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x00075064 File Offset: 0x00073264
		internal TdsParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x0007506C File Offset: 0x0007326C
		internal string ServerProvidedFailOverPartner
		{
			get
			{
				return this._currentFailoverPartner;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x00075074 File Offset: 0x00073274
		internal SqlConnectionPoolGroupProviderInfo PoolGroupProviderInfo
		{
			get
			{
				return this._poolGroupProviderInfo;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x0007507C File Offset: 0x0007327C
		protected override bool ReadyToPrepareTransaction
		{
			get
			{
				return base.FindLiveReader(null) == null;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x00075088 File Offset: 0x00073288
		public override string ServerVersion
		{
			get
			{
				return string.Format(null, "{0:00}.{1:00}.{2:0000}", this._loginAck.majorVersion, (short)this._loginAck.minorVersion, this._loginAck.buildNum);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x00006D64 File Offset: 0x00004F64
		protected override bool UnbindOnTransactionCompletion
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x000750C8 File Offset: 0x000732C8
		protected override void ChangeDatabaseInternal(string database)
		{
			database = SqlConnection.FixupDatabaseTransactionName(database);
			this._parser.TdsExecuteSQLBatch("use " + database, base.ConnectionOptions.ConnectTimeout, null, this._parser._physicalStateObj, true, false);
			this._parser.Run(RunBehavior.UntilDone, null, null, null, this._parser._physicalStateObj);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00075128 File Offset: 0x00073328
		public override void Dispose()
		{
			try
			{
				TdsParser tdsParser = Interlocked.Exchange<TdsParser>(ref this._parser, null);
				if (tdsParser != null)
				{
					tdsParser.Disconnect();
				}
			}
			finally
			{
				this._loginAck = null;
				this._fConnectionOpen = false;
			}
			base.Dispose();
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00075174 File Offset: 0x00073374
		internal override void ValidateConnectionForExecute(SqlCommand command)
		{
			TdsParser parser = this._parser;
			if (parser == null || parser.State == TdsParserState.Broken || parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			SqlDataReader sqlDataReader = null;
			if (parser.MARSOn)
			{
				if (command != null)
				{
					sqlDataReader = base.FindLiveReader(command);
				}
			}
			else
			{
				if (this._asyncCommandCount > 0)
				{
					throw SQL.MARSUnspportedOnConnection();
				}
				sqlDataReader = base.FindLiveReader(null);
			}
			if (sqlDataReader != null)
			{
				throw ADP.OpenReaderExists();
			}
			if (!parser.MARSOn && parser._physicalStateObj._pendingData)
			{
				parser.DrainData(parser._physicalStateObj);
			}
			parser.RollbackOrphanedAPITransactions();
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00075200 File Offset: 0x00073400
		internal void CheckEnlistedTransactionBinding()
		{
			Transaction enlistedTransaction = base.EnlistedTransaction;
			if (enlistedTransaction != null)
			{
				if (base.ConnectionOptions.TransactionBinding == SqlConnectionString.TransactionBindingEnum.ExplicitUnbind)
				{
					Transaction obj = Transaction.Current;
					if (enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active || !enlistedTransaction.Equals(obj))
					{
						throw ADP.TransactionConnectionMismatch();
					}
				}
				else if (enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active)
				{
					if (base.EnlistedTransactionDisposed)
					{
						base.DetachTransaction(enlistedTransaction, true);
						return;
					}
					throw ADP.TransactionCompletedButNotDisposed();
				}
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00075273 File Offset: 0x00073473
		internal override bool IsConnectionAlive(bool throwOnException)
		{
			return this._parser._physicalStateObj.IsConnectionAlive(throwOnException);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00075286 File Offset: 0x00073486
		protected override void Activate(Transaction transaction)
		{
			if (null != transaction)
			{
				if (base.ConnectionOptions.Enlist)
				{
					base.Enlist(transaction);
					return;
				}
			}
			else
			{
				base.Enlist(null);
			}
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000752AD File Offset: 0x000734AD
		protected override void InternalDeactivate()
		{
			if (this._asyncCommandCount != 0)
			{
				base.DoomThisConnection();
			}
			if (!this.IsNonPoolableTransactionRoot && this._parser != null)
			{
				this._parser.Deactivate(base.IsConnectionDoomed);
				if (!base.IsConnectionDoomed)
				{
					this.ResetConnection();
				}
			}
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x000752EC File Offset: 0x000734EC
		private void ResetConnection()
		{
			if (this._fResetConnection)
			{
				this._parser.PrepareResetConnection(this.IsTransactionRoot && !this.IsNonPoolableTransactionRoot);
				base.CurrentDatabase = this._originalDatabase;
				this._currentLanguage = this._originalLanguage;
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00075338 File Offset: 0x00073538
		internal void DecrementAsyncCount()
		{
			Interlocked.Decrement(ref this._asyncCommandCount);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x00075346 File Offset: 0x00073546
		internal void IncrementAsyncCount()
		{
			Interlocked.Increment(ref this._asyncCommandCount);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x00075354 File Offset: 0x00073554
		internal override void DisconnectTransaction(SqlInternalTransaction internalTransaction)
		{
			TdsParser parser = this.Parser;
			if (parser != null)
			{
				parser.DisconnectTransaction(internalTransaction);
			}
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x00075372 File Offset: 0x00073572
		internal void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, IsolationLevel iso)
		{
			this.ExecuteTransaction(transactionRequest, name, iso, null, false);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00075380 File Offset: 0x00073580
		internal override void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest)
		{
			if (base.IsConnectionDoomed)
			{
				if (transactionRequest == SqlInternalConnection.TransactionRequest.Rollback || transactionRequest == SqlInternalConnection.TransactionRequest.IfRollback)
				{
					return;
				}
				throw SQL.ConnectionDoomed();
			}
			else
			{
				if ((transactionRequest == SqlInternalConnection.TransactionRequest.Commit || transactionRequest == SqlInternalConnection.TransactionRequest.Rollback || transactionRequest == SqlInternalConnection.TransactionRequest.IfRollback) && !this.Parser.MARSOn && this.Parser._physicalStateObj.BcpLock)
				{
					throw SQL.ConnectionLockedForBcpEvent();
				}
				string transactionName = (name == null) ? string.Empty : name;
				this.ExecuteTransactionYukon(transactionRequest, transactionName, iso, internalTransaction, isDelegateControlRequest);
				return;
			}
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x000753F0 File Offset: 0x000735F0
		internal void ExecuteTransactionYukon(SqlInternalConnection.TransactionRequest transactionRequest, string transactionName, IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest)
		{
			TdsEnums.TransactionManagerRequestType request = TdsEnums.TransactionManagerRequestType.Begin;
			if (iso <= IsolationLevel.ReadUncommitted)
			{
				if (iso == IsolationLevel.Unspecified)
				{
					TdsEnums.TransactionManagerIsolationLevel isoLevel = TdsEnums.TransactionManagerIsolationLevel.Unspecified;
					goto IL_7E;
				}
				if (iso == IsolationLevel.Chaos)
				{
					throw SQL.NotSupportedIsolationLevel(iso);
				}
				if (iso == IsolationLevel.ReadUncommitted)
				{
					TdsEnums.TransactionManagerIsolationLevel isoLevel = TdsEnums.TransactionManagerIsolationLevel.ReadUncommitted;
					goto IL_7E;
				}
			}
			else if (iso <= IsolationLevel.RepeatableRead)
			{
				if (iso == IsolationLevel.ReadCommitted)
				{
					TdsEnums.TransactionManagerIsolationLevel isoLevel = TdsEnums.TransactionManagerIsolationLevel.ReadCommitted;
					goto IL_7E;
				}
				if (iso == IsolationLevel.RepeatableRead)
				{
					TdsEnums.TransactionManagerIsolationLevel isoLevel = TdsEnums.TransactionManagerIsolationLevel.RepeatableRead;
					goto IL_7E;
				}
			}
			else
			{
				if (iso == IsolationLevel.Serializable)
				{
					TdsEnums.TransactionManagerIsolationLevel isoLevel = TdsEnums.TransactionManagerIsolationLevel.Serializable;
					goto IL_7E;
				}
				if (iso == IsolationLevel.Snapshot)
				{
					TdsEnums.TransactionManagerIsolationLevel isoLevel = TdsEnums.TransactionManagerIsolationLevel.Snapshot;
					goto IL_7E;
				}
			}
			throw ADP.InvalidIsolationLevel(iso);
			IL_7E:
			TdsParserStateObject tdsParserStateObject = this._parser._physicalStateObj;
			TdsParser parser = this._parser;
			bool flag = false;
			bool releaseConnectionLock = false;
			if (!this.ThreadHasParserLockForClose)
			{
				this._parserLock.Wait(false);
				this.ThreadHasParserLockForClose = true;
				releaseConnectionLock = true;
			}
			try
			{
				switch (transactionRequest)
				{
				case SqlInternalConnection.TransactionRequest.Begin:
					request = TdsEnums.TransactionManagerRequestType.Begin;
					break;
				case SqlInternalConnection.TransactionRequest.Promote:
					request = TdsEnums.TransactionManagerRequestType.Promote;
					break;
				case SqlInternalConnection.TransactionRequest.Commit:
					request = TdsEnums.TransactionManagerRequestType.Commit;
					break;
				case SqlInternalConnection.TransactionRequest.Rollback:
				case SqlInternalConnection.TransactionRequest.IfRollback:
					request = TdsEnums.TransactionManagerRequestType.Rollback;
					break;
				case SqlInternalConnection.TransactionRequest.Save:
					request = TdsEnums.TransactionManagerRequestType.Save;
					break;
				}
				if ((internalTransaction != null && internalTransaction.RestoreBrokenConnection) & releaseConnectionLock)
				{
					Task task = internalTransaction.Parent.Connection.ValidateAndReconnect(delegate
					{
						this.ThreadHasParserLockForClose = false;
						this._parserLock.Release();
						releaseConnectionLock = false;
					}, 0);
					if (task != null)
					{
						AsyncHelper.WaitForCompletion(task, 0, null, true);
						internalTransaction.ConnectionHasBeenRestored = true;
						return;
					}
				}
				if (internalTransaction != null && internalTransaction.IsDelegated)
				{
					if (this._parser.MARSOn)
					{
						tdsParserStateObject = this._parser.GetSession(this);
						flag = true;
					}
					else
					{
						int openResultsCount = internalTransaction.OpenResultsCount;
					}
				}
				TdsEnums.TransactionManagerIsolationLevel isoLevel;
				this._parser.TdsExecuteTransactionManagerRequest(null, request, transactionName, isoLevel, base.ConnectionOptions.ConnectTimeout, internalTransaction, tdsParserStateObject, isDelegateControlRequest);
			}
			finally
			{
				if (flag)
				{
					parser.PutSession(tdsParserStateObject);
				}
				if (releaseConnectionLock)
				{
					this.ThreadHasParserLockForClose = false;
					this._parserLock.Release();
				}
			}
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x000755CC File Offset: 0x000737CC
		internal override void DelegatedTransactionEnded()
		{
			base.DelegatedTransactionEnded();
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000755D4 File Offset: 0x000737D4
		protected override byte[] GetDTCAddress()
		{
			return this._parser.GetDTCAddress(base.ConnectionOptions.ConnectTimeout, this._parser.GetSession(this));
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x000755F8 File Offset: 0x000737F8
		protected override void PropagateTransactionCookie(byte[] cookie)
		{
			this._parser.PropagateDistributedTransaction(cookie, base.ConnectionOptions.ConnectTimeout, this._parser._physicalStateObj);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0007561C File Offset: 0x0007381C
		private void CompleteLogin(bool enlistOK)
		{
			this._parser.Run(RunBehavior.UntilDone, null, null, null, this._parser._physicalStateObj);
			if (this._routingInfo == null)
			{
				if (this._federatedAuthenticationRequested && !this._federatedAuthenticationAcknowledged)
				{
					throw SQL.ParsingError(ParsingErrorState.FedAuthNotAcknowledged);
				}
				if (!this._sessionRecoveryAcknowledged)
				{
					this._currentSessionData = null;
					if (this._recoverySessionData != null)
					{
						throw SQL.CR_NoCRAckAtReconnection(this);
					}
				}
				if (this._currentSessionData != null && this._recoverySessionData == null)
				{
					this._currentSessionData._initialDatabase = base.CurrentDatabase;
					this._currentSessionData._initialCollation = this._currentSessionData._collation;
					this._currentSessionData._initialLanguage = this._currentLanguage;
				}
				bool flag = this._parser.EncryptionOptions == EncryptionOptions.ON;
				if (this._recoverySessionData != null && this._recoverySessionData._encrypted != flag)
				{
					throw SQL.CR_EncryptionChanged(this);
				}
				if (this._currentSessionData != null)
				{
					this._currentSessionData._encrypted = flag;
				}
				this._recoverySessionData = null;
			}
			this._parser._physicalStateObj.SniContext = SniContext.Snix_EnableMars;
			this._parser.EnableMars();
			this._fConnectionOpen = true;
			if (enlistOK && base.ConnectionOptions.Enlist)
			{
				this._parser._physicalStateObj.SniContext = SniContext.Snix_AutoEnlist;
				Transaction currentTransaction = ADP.GetCurrentTransaction();
				base.Enlist(currentTransaction);
			}
			this._parser._physicalStateObj.SniContext = SniContext.Snix_Login;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00075778 File Offset: 0x00073978
		private void Login(ServerInfo server, TimeoutTimer timeout, string newPassword, SecureString newSecurePassword)
		{
			SqlLogin sqlLogin = new SqlLogin();
			base.CurrentDatabase = server.ResolvedDatabaseName;
			this._currentPacketSize = base.ConnectionOptions.PacketSize;
			this._currentLanguage = base.ConnectionOptions.CurrentLanguage;
			int timeout2 = 0;
			if (!timeout.IsInfinite)
			{
				long num = timeout.MillisecondsRemaining / 1000L;
				if (2147483647L > num)
				{
					timeout2 = (int)num;
				}
			}
			sqlLogin.timeout = timeout2;
			sqlLogin.userInstance = base.ConnectionOptions.UserInstance;
			sqlLogin.hostName = base.ConnectionOptions.ObtainWorkstationId();
			sqlLogin.userName = base.ConnectionOptions.UserID;
			sqlLogin.password = base.ConnectionOptions.Password;
			sqlLogin.applicationName = base.ConnectionOptions.ApplicationName;
			sqlLogin.language = this._currentLanguage;
			if (!sqlLogin.userInstance)
			{
				sqlLogin.database = base.CurrentDatabase;
				sqlLogin.attachDBFilename = base.ConnectionOptions.AttachDBFilename;
			}
			sqlLogin.serverName = server.UserServerName;
			sqlLogin.useReplication = base.ConnectionOptions.Replication;
			sqlLogin.useSSPI = base.ConnectionOptions.IntegratedSecurity;
			sqlLogin.packetSize = this._currentPacketSize;
			sqlLogin.newPassword = newPassword;
			sqlLogin.readOnlyIntent = (base.ConnectionOptions.ApplicationIntent == ApplicationIntent.ReadOnly);
			sqlLogin.credential = this._credential;
			if (newSecurePassword != null)
			{
				sqlLogin.newSecurePassword = newSecurePassword;
			}
			TdsEnums.FeatureExtension featureExtension = TdsEnums.FeatureExtension.None;
			if (base.ConnectionOptions.ConnectRetryCount > 0)
			{
				featureExtension |= TdsEnums.FeatureExtension.SessionRecovery;
				this._sessionRecoveryRequested = true;
			}
			if (this._accessTokenInBytes != null)
			{
				featureExtension |= TdsEnums.FeatureExtension.FedAuth;
				this._fedAuthFeatureExtensionData = new FederatedAuthenticationFeatureExtensionData?(new FederatedAuthenticationFeatureExtensionData
				{
					libraryType = TdsEnums.FedAuthLibrary.SecurityToken,
					fedAuthRequiredPreLoginResponse = this._fedAuthRequired,
					accessToken = this._accessTokenInBytes
				});
				this._federatedAuthenticationRequested = true;
			}
			featureExtension |= TdsEnums.FeatureExtension.GlobalTransactions;
			this._parser.TdsLogin(sqlLogin, featureExtension, this._recoverySessionData, this._fedAuthFeatureExtensionData);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00075959 File Offset: 0x00073B59
		private void LoginFailure()
		{
			if (this._parser != null)
			{
				this._parser.Disconnect();
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00075970 File Offset: 0x00073B70
		private void OpenLoginEnlist(TimeoutTimer timeout, SqlConnectionString connectionOptions, SqlCredential credential, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance)
		{
			ServerInfo serverInfo = new ServerInfo(connectionOptions);
			bool flag;
			string failoverPartner;
			if (this.PoolGroupProviderInfo != null)
			{
				flag = this.PoolGroupProviderInfo.UseFailoverPartner;
				failoverPartner = this.PoolGroupProviderInfo.FailoverPartner;
			}
			else
			{
				flag = false;
				failoverPartner = base.ConnectionOptions.FailoverPartner;
			}
			this._timeoutErrorInternal.SetInternalSourceType(flag ? SqlConnectionInternalSourceType.Failover : SqlConnectionInternalSourceType.Principle);
			bool flag2 = !string.IsNullOrEmpty(failoverPartner);
			try
			{
				this._timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
				if (flag2)
				{
					this._timeoutErrorInternal.SetFailoverScenario(true);
					this.LoginWithFailover(flag, serverInfo, failoverPartner, newPassword, newSecurePassword, redirectedUserInstance, connectionOptions, credential, timeout);
				}
				else
				{
					this._timeoutErrorInternal.SetFailoverScenario(false);
					this.LoginNoFailover(serverInfo, newPassword, newSecurePassword, redirectedUserInstance, connectionOptions, credential, timeout);
				}
				this._timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.PostLogin);
			}
			catch (Exception e)
			{
				if (ADP.IsCatchableExceptionType(e))
				{
					this.LoginFailure();
				}
				throw;
			}
			this._timeoutErrorInternal.SetAllCompleteMarker();
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00075A54 File Offset: 0x00073C54
		private bool IsDoNotRetryConnectError(SqlException exc)
		{
			return 18456 == exc.Number || 18488 == exc.Number || 1346 == exc.Number || exc._doNotReconnect;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00075A88 File Offset: 0x00073C88
		private void LoginNoFailover(ServerInfo serverInfo, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance, SqlConnectionString connectionOptions, SqlCredential credential, TimeoutTimer timeout)
		{
			int num = 0;
			ServerInfo serverInfo2 = serverInfo;
			int num2 = 100;
			this.ResolveExtendedServerName(serverInfo, !redirectedUserInstance, connectionOptions);
			long num3 = 0L;
			if (connectionOptions.MultiSubnetFailover)
			{
				if (timeout.IsInfinite)
				{
					num3 = 1200L;
				}
				else
				{
					num3 = checked((long)(unchecked(0.08f * (float)timeout.MillisecondsRemaining)));
				}
			}
			int num4 = 0;
			TimeoutTimer timeoutTimer = null;
			for (;;)
			{
				if (connectionOptions.MultiSubnetFailover)
				{
					num4++;
					long num5 = checked(num3 * unchecked((long)num4));
					long millisecondsRemaining = timeout.MillisecondsRemaining;
					if (num5 > millisecondsRemaining)
					{
						num5 = millisecondsRemaining;
					}
					timeoutTimer = TimeoutTimer.StartMillisecondsTimeout(num5);
				}
				if (this._parser != null)
				{
					this._parser.Disconnect();
				}
				this._parser = new TdsParser(base.ConnectionOptions.MARS, base.ConnectionOptions.Asynchronous);
				try
				{
					this.AttemptOneLogin(serverInfo, newPassword, newSecurePassword, !connectionOptions.MultiSubnetFailover, connectionOptions.MultiSubnetFailover ? timeoutTimer : timeout, false);
					if (connectionOptions.MultiSubnetFailover && this.ServerProvidedFailOverPartner != null)
					{
						throw SQL.MultiSubnetFailoverWithFailoverPartner(true, this);
					}
					if (this._routingInfo == null)
					{
						goto IL_271;
					}
					if (num > 0)
					{
						throw SQL.ROR_RecursiveRoutingNotSupported(this);
					}
					if (timeout.IsExpired)
					{
						throw SQL.ROR_TimeoutAfterRoutingInfo(this);
					}
					serverInfo = new ServerInfo(base.ConnectionOptions, this._routingInfo, serverInfo.ResolvedServerName);
					this._timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.RoutingDestination);
					this._originalClientConnectionId = this._clientConnectionId;
					this._routingDestination = serverInfo.UserServerName;
					this._currentPacketSize = base.ConnectionOptions.PacketSize;
					this._currentLanguage = (this._originalLanguage = base.ConnectionOptions.CurrentLanguage);
					base.CurrentDatabase = (this._originalDatabase = base.ConnectionOptions.InitialCatalog);
					this._currentFailoverPartner = null;
					this._instanceName = string.Empty;
					num++;
					continue;
				}
				catch (SqlException exc)
				{
					if (this._parser == null || this._parser.State != TdsParserState.Closed || this.IsDoNotRetryConnectError(exc) || timeout.IsExpired)
					{
						throw;
					}
					if (timeout.MillisecondsRemaining <= (long)num2)
					{
						throw;
					}
				}
				if (this.ServerProvidedFailOverPartner != null)
				{
					break;
				}
				Thread.Sleep(num2);
				num2 = ((num2 < 500) ? (num2 * 2) : 1000);
			}
			if (connectionOptions.MultiSubnetFailover)
			{
				throw SQL.MultiSubnetFailoverWithFailoverPartner(true, this);
			}
			this._timeoutErrorInternal.ResetAndRestartPhase();
			this._timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
			this._timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.Failover);
			this._timeoutErrorInternal.SetFailoverScenario(true);
			this.LoginWithFailover(true, serverInfo, this.ServerProvidedFailOverPartner, newPassword, newSecurePassword, redirectedUserInstance, connectionOptions, credential, timeout);
			return;
			IL_271:
			if (this.PoolGroupProviderInfo != null)
			{
				this.PoolGroupProviderInfo.FailoverCheck(this, false, connectionOptions, this.ServerProvidedFailOverPartner);
			}
			base.CurrentDataSource = serverInfo2.UserServerName;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00075D4C File Offset: 0x00073F4C
		private void LoginWithFailover(bool useFailoverHost, ServerInfo primaryServerInfo, string failoverHost, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance, SqlConnectionString connectionOptions, SqlCredential credential, TimeoutTimer timeout)
		{
			int num = 100;
			ServerInfo serverInfo = new ServerInfo(connectionOptions, failoverHost);
			this.ResolveExtendedServerName(primaryServerInfo, !redirectedUserInstance, connectionOptions);
			if (this.ServerProvidedFailOverPartner == null)
			{
				this.ResolveExtendedServerName(serverInfo, !redirectedUserInstance && failoverHost != primaryServerInfo.UserServerName, connectionOptions);
			}
			long num2;
			int num3;
			checked
			{
				if (timeout.IsInfinite)
				{
					num2 = (long)(unchecked(0.08f * (float)ADP.TimerFromSeconds(15)));
				}
				else
				{
					num2 = (long)(unchecked(0.08f * (float)timeout.MillisecondsRemaining));
				}
				num3 = 0;
			}
			for (;;)
			{
				long num4 = checked(num2 * unchecked((long)(checked(num3 / 2 + 1))));
				long millisecondsRemaining = timeout.MillisecondsRemaining;
				if (num4 > millisecondsRemaining)
				{
					num4 = millisecondsRemaining;
				}
				TimeoutTimer timeout2 = TimeoutTimer.StartMillisecondsTimeout(num4);
				if (this._parser != null)
				{
					this._parser.Disconnect();
				}
				this._parser = new TdsParser(base.ConnectionOptions.MARS, base.ConnectionOptions.Asynchronous);
				ServerInfo serverInfo2;
				if (useFailoverHost)
				{
					if (this.ServerProvidedFailOverPartner != null && serverInfo.ResolvedServerName != this.ServerProvidedFailOverPartner)
					{
						serverInfo.SetDerivedNames(string.Empty, this.ServerProvidedFailOverPartner);
					}
					serverInfo2 = serverInfo;
					this._timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.Failover);
				}
				else
				{
					serverInfo2 = primaryServerInfo;
					this._timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.Principle);
				}
				try
				{
					this.AttemptOneLogin(serverInfo2, newPassword, newSecurePassword, false, timeout2, true);
					if (this._routingInfo != null)
					{
						throw SQL.ROR_UnexpectedRoutingInfo(this);
					}
					break;
				}
				catch (SqlException exc)
				{
					if (this.IsDoNotRetryConnectError(exc) || timeout.IsExpired)
					{
						throw;
					}
					if (base.IsConnectionDoomed)
					{
						throw;
					}
					if (1 == num3 % 2 && timeout.MillisecondsRemaining <= (long)num)
					{
						throw;
					}
				}
				if (1 == num3 % 2)
				{
					Thread.Sleep(num);
					num = ((num < 500) ? (num * 2) : 1000);
				}
				num3++;
				useFailoverHost = !useFailoverHost;
			}
			if (useFailoverHost && this.ServerProvidedFailOverPartner == null)
			{
				throw SQL.InvalidPartnerConfiguration(failoverHost, base.CurrentDatabase);
			}
			if (this.PoolGroupProviderInfo != null)
			{
				this.PoolGroupProviderInfo.FailoverCheck(this, useFailoverHost, connectionOptions, this.ServerProvidedFailOverPartner);
			}
			base.CurrentDataSource = (useFailoverHost ? failoverHost : primaryServerInfo.UserServerName);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00075F4C File Offset: 0x0007414C
		private void ResolveExtendedServerName(ServerInfo serverInfo, bool aliasLookup, SqlConnectionString options)
		{
			if (serverInfo.ExtendedServerName == null)
			{
				string userServerName = serverInfo.UserServerName;
				string userProtocol = serverInfo.UserProtocol;
				serverInfo.SetDerivedNames(userProtocol, userServerName);
			}
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00075F78 File Offset: 0x00074178
		private void AttemptOneLogin(ServerInfo serverInfo, string newPassword, SecureString newSecurePassword, bool ignoreSniOpenTimeout, TimeoutTimer timeout, bool withFailover = false)
		{
			this._routingInfo = null;
			this._parser._physicalStateObj.SniContext = SniContext.Snix_Connect;
			this._parser.Connect(serverInfo, this, ignoreSniOpenTimeout, timeout.LegacyTimerExpire, base.ConnectionOptions.Encrypt, base.ConnectionOptions.TrustServerCertificate, base.ConnectionOptions.IntegratedSecurity, withFailover);
			this._timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.ConsumePreLoginHandshake);
			this._timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.LoginBegin);
			this._parser._physicalStateObj.SniContext = SniContext.Snix_Login;
			this.Login(serverInfo, timeout, newPassword, newSecurePassword);
			this._timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.ProcessConnectionAuth);
			this._timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PostLogin);
			this.CompleteLogin(!base.ConnectionOptions.Pooling);
			this._timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.PostLogin);
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00076042 File Offset: 0x00074242
		protected override object ObtainAdditionalLocksForClose()
		{
			bool flag = !this.ThreadHasParserLockForClose;
			if (flag)
			{
				this._parserLock.Wait(false);
				this.ThreadHasParserLockForClose = true;
			}
			return flag;
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00076068 File Offset: 0x00074268
		protected override void ReleaseAdditionalLocksForClose(object lockToken)
		{
			if ((bool)lockToken)
			{
				this.ThreadHasParserLockForClose = false;
				this._parserLock.Release();
			}
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00076084 File Offset: 0x00074284
		internal bool GetSessionAndReconnectIfNeeded(SqlConnection parent, int timeout = 0)
		{
			if (this.ThreadHasParserLockForClose)
			{
				return false;
			}
			this._parserLock.Wait(false);
			this.ThreadHasParserLockForClose = true;
			bool releaseConnectionLock = true;
			bool result;
			try
			{
				Task task = parent.ValidateAndReconnect(delegate
				{
					this.ThreadHasParserLockForClose = false;
					this._parserLock.Release();
					releaseConnectionLock = false;
				}, timeout);
				if (task != null)
				{
					AsyncHelper.WaitForCompletion(task, timeout, null, true);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			finally
			{
				if (releaseConnectionLock)
				{
					this.ThreadHasParserLockForClose = false;
					this._parserLock.Release();
				}
			}
			return result;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00076118 File Offset: 0x00074318
		internal void BreakConnection()
		{
			SqlConnection connection = base.Connection;
			base.DoomThisConnection();
			if (connection != null)
			{
				connection.Close();
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x0007613B File Offset: 0x0007433B
		internal bool IgnoreEnvChange
		{
			get
			{
				return this._routingInfo != null;
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00076148 File Offset: 0x00074348
		internal void OnEnvChange(SqlEnvChange rec)
		{
			switch (rec.type)
			{
			case 1:
				if (!this._fConnectionOpen && this._recoverySessionData == null)
				{
					this._originalDatabase = rec.newValue;
				}
				base.CurrentDatabase = rec.newValue;
				return;
			case 2:
				if (!this._fConnectionOpen && this._recoverySessionData == null)
				{
					this._originalLanguage = rec.newValue;
				}
				this._currentLanguage = rec.newValue;
				return;
			case 3:
			case 5:
			case 6:
			case 8:
			case 9:
			case 10:
			case 11:
			case 12:
			case 14:
			case 16:
			case 17:
				break;
			case 4:
				this._currentPacketSize = int.Parse(rec.newValue, CultureInfo.InvariantCulture);
				return;
			case 7:
				if (this._currentSessionData != null)
				{
					this._currentSessionData._collation = rec.newCollation;
					return;
				}
				break;
			case 13:
				if (base.ConnectionOptions.ApplicationIntent == ApplicationIntent.ReadOnly)
				{
					throw SQL.ROR_FailoverNotSupportedServer(this);
				}
				this._currentFailoverPartner = rec.newValue;
				return;
			case 15:
				base.PromotedDTCToken = rec.newBinValue;
				return;
			case 18:
				if (this._currentSessionData != null)
				{
					this._currentSessionData.Reset();
					return;
				}
				break;
			case 19:
				this._instanceName = rec.newValue;
				return;
			case 20:
				if (string.IsNullOrEmpty(rec.newRoutingInfo.ServerName) || rec.newRoutingInfo.Protocol != 0 || rec.newRoutingInfo.Port == 0)
				{
					throw SQL.ROR_InvalidRoutingInfo(this);
				}
				this._routingInfo = rec.newRoutingInfo;
				break;
			default:
				return;
			}
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x000762CC File Offset: 0x000744CC
		internal void OnLoginAck(SqlLoginAck rec)
		{
			this._loginAck = rec;
			if (this._recoverySessionData != null && this._recoverySessionData._tdsVersion != rec.tdsVersion)
			{
				throw SQL.CR_TDSVersionNotPreserved(this);
			}
			if (this._currentSessionData != null)
			{
				this._currentSessionData._tdsVersion = rec.tdsVersion;
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0007631C File Offset: 0x0007451C
		internal void OnFeatureExtAck(int featureId, byte[] data)
		{
			if (this._routingInfo != null)
			{
				return;
			}
			switch (featureId)
			{
			case 1:
			{
				if (!this._sessionRecoveryRequested)
				{
					throw SQL.ParsingError();
				}
				this._sessionRecoveryAcknowledged = true;
				int i = 0;
				while (i < data.Length)
				{
					byte b = data[i];
					i++;
					byte b2 = data[i];
					i++;
					int num;
					if (b2 == 255)
					{
						num = BitConverter.ToInt32(data, i);
						i += 4;
					}
					else
					{
						num = (int)b2;
					}
					byte[] array = new byte[num];
					Buffer.BlockCopy(data, i, array, 0, num);
					i += num;
					if (this._recoverySessionData == null)
					{
						this._currentSessionData._initialState[(int)b] = array;
					}
					else
					{
						this._currentSessionData._delta[(int)b] = new SessionStateRecord
						{
							_data = array,
							_dataLength = num,
							_recoverable = true,
							_version = 0U
						};
						this._currentSessionData._deltaDirty = true;
					}
				}
				return;
			}
			case 2:
				if (!this._federatedAuthenticationRequested)
				{
					throw SQL.ParsingErrorFeatureId(ParsingErrorState.UnrequestedFeatureAckReceived, featureId);
				}
				if (this._fedAuthFeatureExtensionData.Value.libraryType != TdsEnums.FedAuthLibrary.SecurityToken)
				{
					throw SQL.ParsingErrorLibraryType(ParsingErrorState.FedAuthFeatureAckUnknownLibraryType, (int)this._fedAuthFeatureExtensionData.Value.libraryType);
				}
				if (data.Length != 0)
				{
					throw SQL.ParsingError(ParsingErrorState.FedAuthFeatureAckContainsExtraData);
				}
				this._federatedAuthenticationAcknowledged = true;
				return;
			case 5:
				if (data.Length < 1)
				{
					throw SQL.ParsingError();
				}
				base.IsGlobalTransaction = true;
				if (1 == data[0])
				{
					base.IsGlobalTransactionsEnabledForServer = true;
					return;
				}
				return;
			}
			throw SQL.ParsingError();
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x00076483 File Offset: 0x00074683
		// (set) Token: 0x06001977 RID: 6519 RVA: 0x00076497 File Offset: 0x00074697
		internal bool ThreadHasParserLockForClose
		{
			get
			{
				return this._threadIdOwningParserLock == Thread.CurrentThread.ManagedThreadId;
			}
			set
			{
				if (value)
				{
					this._threadIdOwningParserLock = Thread.CurrentThread.ManagedThreadId;
					return;
				}
				if (this._threadIdOwningParserLock == Thread.CurrentThread.ManagedThreadId)
				{
					this._threadIdOwningParserLock = -1;
				}
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x000764C6 File Offset: 0x000746C6
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return base.TryOpenConnectionInternal(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x000764D4 File Offset: 0x000746D4
		// Note: this type is marked as 'beforefieldinit'.
		static SqlInternalConnectionTds()
		{
		}

		// Token: 0x04001062 RID: 4194
		private readonly SqlConnectionPoolGroupProviderInfo _poolGroupProviderInfo;

		// Token: 0x04001063 RID: 4195
		private TdsParser _parser;

		// Token: 0x04001064 RID: 4196
		private SqlLoginAck _loginAck;

		// Token: 0x04001065 RID: 4197
		private SqlCredential _credential;

		// Token: 0x04001066 RID: 4198
		private FederatedAuthenticationFeatureExtensionData? _fedAuthFeatureExtensionData;

		// Token: 0x04001067 RID: 4199
		private bool _sessionRecoveryRequested;

		// Token: 0x04001068 RID: 4200
		internal bool _sessionRecoveryAcknowledged;

		// Token: 0x04001069 RID: 4201
		internal SessionData _currentSessionData;

		// Token: 0x0400106A RID: 4202
		private SessionData _recoverySessionData;

		// Token: 0x0400106B RID: 4203
		internal bool _fedAuthRequired;

		// Token: 0x0400106C RID: 4204
		internal bool _federatedAuthenticationRequested;

		// Token: 0x0400106D RID: 4205
		internal bool _federatedAuthenticationAcknowledged;

		// Token: 0x0400106E RID: 4206
		internal byte[] _accessTokenInBytes;

		// Token: 0x0400106F RID: 4207
		private static readonly HashSet<int> s_transientErrors = new HashSet<int>
		{
			4060,
			10928,
			10929,
			40197,
			40501,
			40613
		};

		// Token: 0x04001070 RID: 4208
		private bool _fConnectionOpen;

		// Token: 0x04001071 RID: 4209
		private bool _fResetConnection;

		// Token: 0x04001072 RID: 4210
		private string _originalDatabase;

		// Token: 0x04001073 RID: 4211
		private string _currentFailoverPartner;

		// Token: 0x04001074 RID: 4212
		private string _originalLanguage;

		// Token: 0x04001075 RID: 4213
		private string _currentLanguage;

		// Token: 0x04001076 RID: 4214
		private int _currentPacketSize;

		// Token: 0x04001077 RID: 4215
		private int _asyncCommandCount;

		// Token: 0x04001078 RID: 4216
		private string _instanceName = string.Empty;

		// Token: 0x04001079 RID: 4217
		private DbConnectionPoolIdentity _identity;

		// Token: 0x0400107A RID: 4218
		internal SqlInternalConnectionTds.SyncAsyncLock _parserLock = new SqlInternalConnectionTds.SyncAsyncLock();

		// Token: 0x0400107B RID: 4219
		private int _threadIdOwningParserLock = -1;

		// Token: 0x0400107C RID: 4220
		private SqlConnectionTimeoutErrorInternal _timeoutErrorInternal;

		// Token: 0x0400107D RID: 4221
		internal Guid _clientConnectionId = Guid.Empty;

		// Token: 0x0400107E RID: 4222
		private RoutingInfo _routingInfo;

		// Token: 0x0400107F RID: 4223
		private Guid _originalClientConnectionId = Guid.Empty;

		// Token: 0x04001080 RID: 4224
		private string _routingDestination;

		// Token: 0x04001081 RID: 4225
		private readonly TimeoutTimer _timeout;

		// Token: 0x0200020A RID: 522
		internal class SyncAsyncLock
		{
			// Token: 0x0600197A RID: 6522 RVA: 0x00076534 File Offset: 0x00074734
			internal void Wait(bool canReleaseFromAnyThread)
			{
				Monitor.Enter(this._semaphore);
				if (canReleaseFromAnyThread || this._semaphore.CurrentCount == 0)
				{
					this._semaphore.Wait();
					if (canReleaseFromAnyThread)
					{
						Monitor.Exit(this._semaphore);
						return;
					}
					this._semaphore.Release();
				}
			}

			// Token: 0x0600197B RID: 6523 RVA: 0x00076584 File Offset: 0x00074784
			internal void Wait(bool canReleaseFromAnyThread, int timeout, ref bool lockTaken)
			{
				lockTaken = false;
				bool flag = false;
				try
				{
					Monitor.TryEnter(this._semaphore, timeout, ref flag);
					if (flag)
					{
						if (canReleaseFromAnyThread || this._semaphore.CurrentCount == 0)
						{
							if (this._semaphore.Wait(timeout))
							{
								if (canReleaseFromAnyThread)
								{
									Monitor.Exit(this._semaphore);
									flag = false;
								}
								else
								{
									this._semaphore.Release();
								}
								lockTaken = true;
							}
						}
						else
						{
							lockTaken = true;
						}
					}
				}
				finally
				{
					if (!lockTaken && flag)
					{
						Monitor.Exit(this._semaphore);
					}
				}
			}

			// Token: 0x0600197C RID: 6524 RVA: 0x00076614 File Offset: 0x00074814
			internal void Release()
			{
				if (this._semaphore.CurrentCount == 0)
				{
					this._semaphore.Release();
					return;
				}
				Monitor.Exit(this._semaphore);
			}

			// Token: 0x170004B2 RID: 1202
			// (get) Token: 0x0600197D RID: 6525 RVA: 0x0007663B File Offset: 0x0007483B
			internal bool CanBeReleasedFromAnyThread
			{
				get
				{
					return this._semaphore.CurrentCount == 0;
				}
			}

			// Token: 0x0600197E RID: 6526 RVA: 0x0007664B File Offset: 0x0007484B
			internal bool ThreadMayHaveLock()
			{
				return Monitor.IsEntered(this._semaphore) || this._semaphore.CurrentCount == 0;
			}

			// Token: 0x0600197F RID: 6527 RVA: 0x0007666A File Offset: 0x0007486A
			public SyncAsyncLock()
			{
			}

			// Token: 0x04001082 RID: 4226
			private SemaphoreSlim _semaphore = new SemaphoreSlim(1);
		}

		// Token: 0x0200020B RID: 523
		[CompilerGenerated]
		private sealed class <>c__DisplayClass88_0
		{
			// Token: 0x06001980 RID: 6528 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass88_0()
			{
			}

			// Token: 0x06001981 RID: 6529 RVA: 0x0007667E File Offset: 0x0007487E
			internal void <ExecuteTransactionYukon>b__0()
			{
				this.<>4__this.ThreadHasParserLockForClose = false;
				this.<>4__this._parserLock.Release();
				this.releaseConnectionLock = false;
			}

			// Token: 0x04001083 RID: 4227
			public SqlInternalConnectionTds <>4__this;

			// Token: 0x04001084 RID: 4228
			public bool releaseConnectionLock;
		}

		// Token: 0x0200020C RID: 524
		[CompilerGenerated]
		private sealed class <>c__DisplayClass103_0
		{
			// Token: 0x06001982 RID: 6530 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass103_0()
			{
			}

			// Token: 0x06001983 RID: 6531 RVA: 0x000766A3 File Offset: 0x000748A3
			internal void <GetSessionAndReconnectIfNeeded>b__0()
			{
				this.<>4__this.ThreadHasParserLockForClose = false;
				this.<>4__this._parserLock.Release();
				this.releaseConnectionLock = false;
			}

			// Token: 0x04001085 RID: 4229
			public SqlInternalConnectionTds <>4__this;

			// Token: 0x04001086 RID: 4230
			public bool releaseConnectionLock;
		}
	}
}
