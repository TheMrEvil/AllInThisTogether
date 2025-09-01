using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.ProviderBase;
using System.EnterpriseServices;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Transactions;
using Unity;

namespace System.Data.Odbc
{
	/// <summary>Represents an open connection to a data source.</summary>
	// Token: 0x020002D8 RID: 728
	public sealed class OdbcConnection : DbConnection, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnection" /> class with the specified connection string.</summary>
		/// <param name="connectionString">The connection used to open the data source.</param>
		// Token: 0x06001FC9 RID: 8137 RVA: 0x00095722 File Offset: 0x00093922
		public OdbcConnection(string connectionString) : this()
		{
			this.ConnectionString = connectionString;
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x00095731 File Offset: 0x00093931
		private OdbcConnection(OdbcConnection connection) : this()
		{
			this.CopyFrom(connection);
			this._connectionTimeout = connection._connectionTimeout;
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x0009574C File Offset: 0x0009394C
		// (set) Token: 0x06001FCC RID: 8140 RVA: 0x00095754 File Offset: 0x00093954
		internal OdbcConnectionHandle ConnectionHandle
		{
			get
			{
				return this._connectionHandle;
			}
			set
			{
				this._connectionHandle = value;
			}
		}

		/// <summary>Gets or sets the string used to open a data source.</summary>
		/// <returns>The ODBC driver connection string that includes settings, such as the data source name, needed to establish the initial connection. The default value is an empty string (""). The maximum length is 1024 characters.</returns>
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x0009575D File Offset: 0x0009395D
		// (set) Token: 0x06001FCE RID: 8142 RVA: 0x00095765 File Offset: 0x00093965
		public override string ConnectionString
		{
			get
			{
				return this.ConnectionString_Get();
			}
			set
			{
				this.ConnectionString_Set(value);
			}
		}

		/// <summary>Gets or sets the time to wait while trying to establish a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time in seconds to wait for a connection to open. The default value is 15 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is less than 0.</exception>
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x0009576E File Offset: 0x0009396E
		// (set) Token: 0x06001FD0 RID: 8144 RVA: 0x00095776 File Offset: 0x00093976
		[DefaultValue(15)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int ConnectionTimeout
		{
			get
			{
				return this._connectionTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw ODBC.NegativeArgument();
				}
				if (this.IsOpen)
				{
					throw ODBC.CantSetPropertyOnOpenConnection();
				}
				this._connectionTimeout = value;
			}
		}

		/// <summary>Gets the name of the current database or the database to be used after a connection is opened.</summary>
		/// <returns>The name of the current database. The default value is an empty string ("") until the connection is opened.</returns>
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x00095797 File Offset: 0x00093997
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Database
		{
			get
			{
				if (this.IsOpen && !this.ProviderInfo.NoCurrentCatalog)
				{
					return this.GetConnectAttrString(ODBC32.SQL_ATTR.CURRENT_CATALOG);
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the server name or file name of the data source.</summary>
		/// <returns>The server name or file name of the data source. The default value is an empty string ("") until the connection is opened.</returns>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x000957BC File Offset: 0x000939BC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string DataSource
		{
			get
			{
				if (this.IsOpen)
				{
					return this.GetInfoStringUnhandled(ODBC32.SQL_INFO.SERVER_NAME, true);
				}
				return string.Empty;
			}
		}

		/// <summary>Gets a string that contains the version of the server to which the client is connected.</summary>
		/// <returns>The version of the connected server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection is closed.</exception>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x000957D5 File Offset: 0x000939D5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string ServerVersion
		{
			get
			{
				return this.InnerConnection.ServerVersion;
			}
		}

		/// <summary>Gets the current state of the connection.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Data.ConnectionState" /> values. The default is <see langword="Closed" />.</returns>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x000957E2 File Offset: 0x000939E2
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ConnectionState State
		{
			get
			{
				return this.InnerConnection.State;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x000957EF File Offset: 0x000939EF
		internal OdbcConnectionPoolGroupProviderInfo ProviderInfo
		{
			get
			{
				return (OdbcConnectionPoolGroupProviderInfo)this.PoolGroup.ProviderInfo;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x00095801 File Offset: 0x00093A01
		internal ConnectionState InternalState
		{
			get
			{
				return this.State | this._extraState;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x00095810 File Offset: 0x00093A10
		internal bool IsOpen
		{
			get
			{
				return this.InnerConnection is OdbcConnectionOpen;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x00095820 File Offset: 0x00093A20
		// (set) Token: 0x06001FD9 RID: 8153 RVA: 0x00095849 File Offset: 0x00093A49
		internal OdbcTransaction LocalTransaction
		{
			get
			{
				OdbcTransaction result = null;
				if (this._weakTransaction != null)
				{
					result = (OdbcTransaction)this._weakTransaction.Target;
				}
				return result;
			}
			set
			{
				this._weakTransaction = null;
				if (value != null)
				{
					this._weakTransaction = new WeakReference(value);
				}
			}
		}

		/// <summary>Gets the name of the ODBC driver specified for the current connection.</summary>
		/// <returns>The name of the ODBC driver. This typically is the DLL name (for example, Sqlsrv32.dll). The default value is an empty string ("") until the connection is opened.</returns>
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x00095861 File Offset: 0x00093A61
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string Driver
		{
			get
			{
				if (this.IsOpen)
				{
					if (this.ProviderInfo.DriverName == null)
					{
						this.ProviderInfo.DriverName = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.DRIVER_NAME);
					}
					return this.ProviderInfo.DriverName;
				}
				return ADP.StrEmpty;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x0009589C File Offset: 0x00093A9C
		internal bool IsV3Driver
		{
			get
			{
				if (this.ProviderInfo.DriverVersion == null)
				{
					this.ProviderInfo.DriverVersion = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.DRIVER_ODBC_VER);
					if (this.ProviderInfo.DriverVersion != null && this.ProviderInfo.DriverVersion.Length >= 2)
					{
						try
						{
							this.ProviderInfo.IsV3Driver = (int.Parse(this.ProviderInfo.DriverVersion.Substring(0, 2), CultureInfo.InvariantCulture) >= 3);
							goto IL_95;
						}
						catch (FormatException e)
						{
							this.ProviderInfo.IsV3Driver = false;
							ADP.TraceExceptionWithoutRethrow(e);
							goto IL_95;
						}
					}
					this.ProviderInfo.DriverVersion = "";
				}
				IL_95:
				return this.ProviderInfo.IsV3Driver;
			}
		}

		/// <summary>Occurs when the ODBC driver sends a warning or an informational message.</summary>
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001FDC RID: 8156 RVA: 0x0009595C File Offset: 0x00093B5C
		// (remove) Token: 0x06001FDD RID: 8157 RVA: 0x00095975 File Offset: 0x00093B75
		public event OdbcInfoMessageEventHandler InfoMessage
		{
			add
			{
				this._infoMessageEventHandler = (OdbcInfoMessageEventHandler)Delegate.Combine(this._infoMessageEventHandler, value);
			}
			remove
			{
				this._infoMessageEventHandler = (OdbcInfoMessageEventHandler)Delegate.Remove(this._infoMessageEventHandler, value);
			}
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x00095990 File Offset: 0x00093B90
		internal char EscapeChar(string method)
		{
			this.CheckState(method);
			if (!this.ProviderInfo.HasEscapeChar)
			{
				string infoStringUnhandled = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.SEARCH_PATTERN_ESCAPE);
				this.ProviderInfo.EscapeChar = ((infoStringUnhandled.Length == 1) ? infoStringUnhandled[0] : this.QuoteChar(method)[0]);
			}
			return this.ProviderInfo.EscapeChar;
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x000959F0 File Offset: 0x00093BF0
		internal string QuoteChar(string method)
		{
			this.CheckState(method);
			if (!this.ProviderInfo.HasQuoteChar)
			{
				string infoStringUnhandled = this.GetInfoStringUnhandled(ODBC32.SQL_INFO.IDENTIFIER_QUOTE_CHAR);
				this.ProviderInfo.QuoteChar = ((1 == infoStringUnhandled.Length) ? infoStringUnhandled : "\0");
			}
			return this.ProviderInfo.QuoteChar;
		}

		/// <summary>Starts a transaction at the data source.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.InvalidOperationException">A transaction is currently active. Parallel transactions are not supported.</exception>
		// Token: 0x06001FE0 RID: 8160 RVA: 0x00095A41 File Offset: 0x00093C41
		public new OdbcTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.Unspecified);
		}

		/// <summary>Starts a transaction at the data source with the specified <see cref="T:System.Data.IsolationLevel" /> value.</summary>
		/// <param name="isolevel">The transaction isolation level for this connection. If you do not specify an isolation level, the default isolation level for the driver is used.</param>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.InvalidOperationException">A transaction is currently active. Parallel transactions are not supported.</exception>
		// Token: 0x06001FE1 RID: 8161 RVA: 0x00095A4A File Offset: 0x00093C4A
		public new OdbcTransaction BeginTransaction(IsolationLevel isolevel)
		{
			return (OdbcTransaction)this.InnerConnection.BeginTransaction(isolevel);
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x00095A60 File Offset: 0x00093C60
		private void RollbackDeadTransaction()
		{
			WeakReference weakTransaction = this._weakTransaction;
			if (weakTransaction != null && !weakTransaction.IsAlive)
			{
				this._weakTransaction = null;
				this.ConnectionHandle.CompleteTransaction(1);
			}
		}

		/// <summary>Changes the current database associated with an open <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <param name="value">The database name.</param>
		/// <exception cref="T:System.ArgumentException">The database name is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The connection is not open.</exception>
		/// <exception cref="T:System.Data.Odbc.OdbcException">Cannot change the database.</exception>
		// Token: 0x06001FE3 RID: 8163 RVA: 0x00095A93 File Offset: 0x00093C93
		public override void ChangeDatabase(string value)
		{
			this.InnerConnection.ChangeDatabase(value);
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x00095AA4 File Offset: 0x00093CA4
		internal void CheckState(string method)
		{
			ConnectionState internalState = this.InternalState;
			if (ConnectionState.Open != internalState)
			{
				throw ADP.OpenConnectionRequired(method, internalState);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06001FE5 RID: 8165 RVA: 0x00095AC4 File Offset: 0x00093CC4
		object ICloneable.Clone()
		{
			return new OdbcConnection(this);
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x00095ACC File Offset: 0x00093CCC
		internal bool ConnectionIsAlive(Exception innerException)
		{
			if (this.IsOpen)
			{
				if (!this.ProviderInfo.NoConnectionDead)
				{
					int connectAttr = this.GetConnectAttr(ODBC32.SQL_ATTR.CONNECTION_DEAD, ODBC32.HANDLER.IGNORE);
					if (1 == connectAttr)
					{
						this.Close();
						throw ADP.ConnectionIsDisabled(innerException);
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Creates and returns an <see cref="T:System.Data.Odbc.OdbcCommand" /> object associated with the <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> object.</returns>
		// Token: 0x06001FE7 RID: 8167 RVA: 0x00095B0F File Offset: 0x00093D0F
		public new OdbcCommand CreateCommand()
		{
			return new OdbcCommand(string.Empty, this);
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x00095B1C File Offset: 0x00093D1C
		internal OdbcStatementHandle CreateStatementHandle()
		{
			return new OdbcStatementHandle(this.ConnectionHandle);
		}

		/// <summary>Closes the connection to the data source.</summary>
		// Token: 0x06001FE9 RID: 8169 RVA: 0x00095B2C File Offset: 0x00093D2C
		public override void Close()
		{
			this.InnerConnection.CloseConnection(this, this.ConnectionFactory);
			OdbcConnectionHandle connectionHandle = this._connectionHandle;
			if (connectionHandle != null)
			{
				this._connectionHandle = null;
				WeakReference weakTransaction = this._weakTransaction;
				if (weakTransaction != null)
				{
					this._weakTransaction = null;
					IDisposable disposable = weakTransaction.Target as OdbcTransaction;
					if (disposable != null && weakTransaction.IsAlive)
					{
						disposable.Dispose();
					}
				}
				connectionHandle.Dispose();
			}
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x00007EED File Offset: 0x000060ED
		private void DisposeMe(bool disposing)
		{
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x00095B90 File Offset: 0x00093D90
		internal string GetConnectAttrString(ODBC32.SQL_ATTR attribute)
		{
			string result = "";
			int num = 0;
			byte[] array = new byte[100];
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				ODBC32.RetCode connectionAttribute = connectionHandle.GetConnectionAttribute(attribute, array, out num);
				if (array.Length + 2 <= num)
				{
					array = new byte[num + 2];
					connectionAttribute = connectionHandle.GetConnectionAttribute(attribute, array, out num);
				}
				if (connectionAttribute == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == connectionAttribute)
				{
					result = (BitConverter.IsLittleEndian ? Encoding.Unicode : Encoding.BigEndianUnicode).GetString(array, 0, Math.Min(num, array.Length));
				}
				else if (connectionAttribute == ODBC32.RetCode.ERROR)
				{
					string diagSqlState = this.GetDiagSqlState();
					if ("HYC00" == diagSqlState || "HY092" == diagSqlState || "IM001" == diagSqlState)
					{
						this.FlagUnsupportedConnectAttr(attribute);
					}
				}
			}
			return result;
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x00095C54 File Offset: 0x00093E54
		internal int GetConnectAttr(ODBC32.SQL_ATTR attribute, ODBC32.HANDLER handler)
		{
			int result = -1;
			int num = 0;
			byte[] array = new byte[4];
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				ODBC32.RetCode connectionAttribute = connectionHandle.GetConnectionAttribute(attribute, array, out num);
				if (connectionAttribute == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == connectionAttribute)
				{
					result = BitConverter.ToInt32(array, 0);
				}
				else
				{
					if (connectionAttribute == ODBC32.RetCode.ERROR)
					{
						string diagSqlState = this.GetDiagSqlState();
						if ("HYC00" == diagSqlState || "HY092" == diagSqlState || "IM001" == diagSqlState)
						{
							this.FlagUnsupportedConnectAttr(attribute);
						}
					}
					if (handler == ODBC32.HANDLER.THROW)
					{
						this.HandleError(connectionHandle, connectionAttribute);
					}
				}
			}
			return result;
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x00095CE4 File Offset: 0x00093EE4
		private string GetDiagSqlState()
		{
			string result;
			this.ConnectionHandle.GetDiagnosticField(out result);
			return result;
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x00095D00 File Offset: 0x00093F00
		internal ODBC32.RetCode GetInfoInt16Unhandled(ODBC32.SQL_INFO info, out short resultValue)
		{
			byte[] array = new byte[2];
			ODBC32.RetCode info2 = this.ConnectionHandle.GetInfo1(info, array);
			resultValue = BitConverter.ToInt16(array, 0);
			return info2;
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x00095D2C File Offset: 0x00093F2C
		internal ODBC32.RetCode GetInfoInt32Unhandled(ODBC32.SQL_INFO info, out int resultValue)
		{
			byte[] array = new byte[4];
			ODBC32.RetCode info2 = this.ConnectionHandle.GetInfo1(info, array);
			resultValue = BitConverter.ToInt32(array, 0);
			return info2;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x00095D58 File Offset: 0x00093F58
		private int GetInfoInt32Unhandled(ODBC32.SQL_INFO infotype)
		{
			byte[] array = new byte[4];
			this.ConnectionHandle.GetInfo1(infotype, array);
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x00095D81 File Offset: 0x00093F81
		internal string GetInfoStringUnhandled(ODBC32.SQL_INFO info)
		{
			return this.GetInfoStringUnhandled(info, false);
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x00095D8C File Offset: 0x00093F8C
		private string GetInfoStringUnhandled(ODBC32.SQL_INFO info, bool handleError)
		{
			string result = null;
			short num = 0;
			byte[] array = new byte[100];
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				ODBC32.RetCode info2 = connectionHandle.GetInfo2(info, array, out num);
				if (array.Length < (int)(num - 2))
				{
					array = new byte[(int)(num + 2)];
					info2 = connectionHandle.GetInfo2(info, array, out num);
				}
				if (info2 == ODBC32.RetCode.SUCCESS || info2 == ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					result = (BitConverter.IsLittleEndian ? Encoding.Unicode : Encoding.BigEndianUnicode).GetString(array, 0, Math.Min((int)num, array.Length));
				}
				else if (handleError)
				{
					this.HandleError(this.ConnectionHandle, info2);
				}
			}
			else if (handleError)
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x00095E24 File Offset: 0x00094024
		internal Exception HandleErrorNoThrow(OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			if (retcode != ODBC32.RetCode.SUCCESS)
			{
				if (retcode != ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					OdbcException ex = OdbcException.CreateException(ODBC32.GetDiagErrors(null, hrHandle, retcode), retcode);
					if (ex != null)
					{
						ex.Errors.SetSource(this.Driver);
					}
					this.ConnectionIsAlive(ex);
					return ex;
				}
				if (this._infoMessageEventHandler != null)
				{
					OdbcErrorCollection diagErrors = ODBC32.GetDiagErrors(null, hrHandle, retcode);
					diagErrors.SetSource(this.Driver);
					this.OnInfoMessage(new OdbcInfoMessageEventArgs(diagErrors));
				}
			}
			return null;
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x00095E94 File Offset: 0x00094094
		internal void HandleError(OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			Exception ex = this.HandleErrorNoThrow(hrHandle, retcode);
			if (retcode > ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				throw ex;
			}
		}

		/// <summary>Opens a connection to a data source with the property settings specified by the <see cref="P:System.Data.Odbc.OdbcConnection.ConnectionString" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The functionality of this method is unsupported in the base class and must be implemented in a derived class instead.</exception>
		// Token: 0x06001FF5 RID: 8181 RVA: 0x00095EB0 File Offset: 0x000940B0
		public override void Open()
		{
			try
			{
				this.InnerConnection.OpenConnection(this, this.ConnectionFactory);
			}
			catch (DllNotFoundException ex) when (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				throw new DllNotFoundException("Dependency unixODBC with minimum version 2.3.1 is required." + Environment.NewLine + ex.Message);
			}
			if (ADP.NeedManualEnlistment())
			{
				this.EnlistTransaction(Transaction.Current);
			}
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x00095F30 File Offset: 0x00094130
		private void OnInfoMessage(OdbcInfoMessageEventArgs args)
		{
			if (this._infoMessageEventHandler != null)
			{
				try
				{
					this._infoMessageEventHandler(this, args);
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableOrSecurityExceptionType(e))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(e);
				}
			}
		}

		/// <summary>Indicates that the ODBC Driver Manager environment handle can be released when the last underlying connection is released.</summary>
		// Token: 0x06001FF7 RID: 8183 RVA: 0x00095F78 File Offset: 0x00094178
		public static void ReleaseObjectPool()
		{
			OdbcEnvironment.ReleaseObjectPool();
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x00095F80 File Offset: 0x00094180
		internal OdbcTransaction SetStateExecuting(string method, OdbcTransaction transaction)
		{
			if (this._weakTransaction != null)
			{
				OdbcTransaction odbcTransaction = this._weakTransaction.Target as OdbcTransaction;
				if (transaction != odbcTransaction)
				{
					if (transaction == null)
					{
						throw ADP.TransactionRequired(method);
					}
					if (this != transaction.Connection)
					{
						throw ADP.TransactionConnectionMismatch();
					}
					transaction = null;
				}
			}
			else if (transaction != null)
			{
				if (transaction.Connection != null)
				{
					throw ADP.TransactionConnectionMismatch();
				}
				transaction = null;
			}
			ConnectionState internalState = this.InternalState;
			if (ConnectionState.Open != internalState)
			{
				this.NotifyWeakReference(1);
				internalState = this.InternalState;
				if (ConnectionState.Open != internalState)
				{
					if ((ConnectionState.Fetching & internalState) != ConnectionState.Closed)
					{
						throw ADP.OpenReaderExists();
					}
					throw ADP.OpenConnectionRequired(method, internalState);
				}
			}
			return transaction;
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00096010 File Offset: 0x00094210
		internal void SetSupportedType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			switch (sqltype)
			{
			case ODBC32.SQL_TYPE.WLONGVARCHAR:
				sql_CVT = ODBC32.SQL_CVT.WLONGVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WVARCHAR:
				sql_CVT = ODBC32.SQL_CVT.WVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WCHAR:
				sql_CVT = ODBC32.SQL_CVT.WCHAR;
				break;
			default:
				if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
				{
					return;
				}
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
				break;
			}
			this.ProviderInfo.TestedSQLTypes |= (int)sql_CVT;
			this.ProviderInfo.SupportedSQLTypes |= (int)sql_CVT;
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x0009607C File Offset: 0x0009427C
		internal void FlagRestrictedSqlBindType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
			{
				if (sqltype != ODBC32.SQL_TYPE.DECIMAL)
				{
					return;
				}
				sql_CVT = ODBC32.SQL_CVT.DECIMAL;
			}
			else
			{
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
			}
			this.ProviderInfo.RestrictedSQLBindTypes |= (int)sql_CVT;
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x000960AB File Offset: 0x000942AB
		internal void FlagUnsupportedConnectAttr(ODBC32.SQL_ATTR Attribute)
		{
			if (Attribute == ODBC32.SQL_ATTR.CURRENT_CATALOG)
			{
				this.ProviderInfo.NoCurrentCatalog = true;
				return;
			}
			if (Attribute != ODBC32.SQL_ATTR.CONNECTION_DEAD)
			{
				return;
			}
			this.ProviderInfo.NoConnectionDead = true;
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x000960D4 File Offset: 0x000942D4
		internal void FlagUnsupportedStmtAttr(ODBC32.SQL_ATTR Attribute)
		{
			if (Attribute == ODBC32.SQL_ATTR.QUERY_TIMEOUT)
			{
				this.ProviderInfo.NoQueryTimeout = true;
				return;
			}
			if (Attribute == ODBC32.SQL_ATTR.SQL_COPT_SS_TXN_ISOLATION)
			{
				this.ProviderInfo.NoSqlSoptSSHiddenColumns = true;
				return;
			}
			if (Attribute != (ODBC32.SQL_ATTR)1228)
			{
				return;
			}
			this.ProviderInfo.NoSqlSoptSSNoBrowseTable = true;
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x00096110 File Offset: 0x00094310
		internal void FlagUnsupportedColAttr(ODBC32.SQL_DESC v3FieldId, ODBC32.SQL_COLUMN v2FieldId)
		{
			if (this.IsV3Driver && v3FieldId == (ODBC32.SQL_DESC)1212)
			{
				this.ProviderInfo.NoSqlCASSColumnKey = true;
			}
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x00096130 File Offset: 0x00094330
		internal bool SQLGetFunctions(ODBC32.SQL_API odbcFunction)
		{
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			if (connectionHandle != null)
			{
				short result;
				ODBC32.RetCode functions = connectionHandle.GetFunctions(odbcFunction, out result);
				if (functions != ODBC32.RetCode.SUCCESS)
				{
					this.HandleError(connectionHandle, functions);
				}
				return result != 0;
			}
			throw ODBC.ConnectionClosed();
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x0009616C File Offset: 0x0009436C
		internal bool TestTypeSupport(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CONVERT infotype;
			ODBC32.SQL_CVT sql_CVT;
			switch (sqltype)
			{
			case ODBC32.SQL_TYPE.WLONGVARCHAR:
				infotype = ODBC32.SQL_CONVERT.LONGVARCHAR;
				sql_CVT = ODBC32.SQL_CVT.WLONGVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WVARCHAR:
				infotype = ODBC32.SQL_CONVERT.VARCHAR;
				sql_CVT = ODBC32.SQL_CVT.WVARCHAR;
				break;
			case ODBC32.SQL_TYPE.WCHAR:
				infotype = ODBC32.SQL_CONVERT.CHAR;
				sql_CVT = ODBC32.SQL_CVT.WCHAR;
				break;
			default:
				if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
				{
					return false;
				}
				infotype = ODBC32.SQL_CONVERT.NUMERIC;
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
				break;
			}
			if ((this.ProviderInfo.TestedSQLTypes & (int)sql_CVT) == 0)
			{
				int num = this.GetInfoInt32Unhandled((ODBC32.SQL_INFO)infotype);
				num &= (int)sql_CVT;
				this.ProviderInfo.TestedSQLTypes |= (int)sql_CVT;
				this.ProviderInfo.SupportedSQLTypes |= num;
			}
			return (this.ProviderInfo.SupportedSQLTypes & (int)sql_CVT) != 0;
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x00096210 File Offset: 0x00094410
		internal bool TestRestrictedSqlBindType(ODBC32.SQL_TYPE sqltype)
		{
			ODBC32.SQL_CVT sql_CVT;
			if (sqltype != ODBC32.SQL_TYPE.NUMERIC)
			{
				if (sqltype != ODBC32.SQL_TYPE.DECIMAL)
				{
					return false;
				}
				sql_CVT = ODBC32.SQL_CVT.DECIMAL;
			}
			else
			{
				sql_CVT = ODBC32.SQL_CVT.NUMERIC;
			}
			return (this.ProviderInfo.RestrictedSQLBindTypes & (int)sql_CVT) != 0;
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x00096241 File Offset: 0x00094441
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			DbTransaction result = this.InnerConnection.BeginTransaction(isolationLevel);
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x00096258 File Offset: 0x00094458
		internal OdbcTransaction Open_BeginTransaction(IsolationLevel isolevel)
		{
			this.CheckState("BeginTransaction");
			this.RollbackDeadTransaction();
			if (this._weakTransaction != null && this._weakTransaction.IsAlive)
			{
				throw ADP.ParallelTransactionsNotSupported(this);
			}
			if (isolevel <= IsolationLevel.ReadUncommitted)
			{
				if (isolevel == IsolationLevel.Unspecified)
				{
					goto IL_82;
				}
				if (isolevel == IsolationLevel.Chaos)
				{
					throw ODBC.NotSupportedIsolationLevel(isolevel);
				}
				if (isolevel == IsolationLevel.ReadUncommitted)
				{
					goto IL_82;
				}
			}
			else if (isolevel <= IsolationLevel.RepeatableRead)
			{
				if (isolevel == IsolationLevel.ReadCommitted || isolevel == IsolationLevel.RepeatableRead)
				{
					goto IL_82;
				}
			}
			else if (isolevel == IsolationLevel.Serializable || isolevel == IsolationLevel.Snapshot)
			{
				goto IL_82;
			}
			throw ADP.InvalidIsolationLevel(isolevel);
			IL_82:
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			ODBC32.RetCode retCode = connectionHandle.BeginTransaction(ref isolevel);
			if (retCode == ODBC32.RetCode.ERROR)
			{
				this.HandleError(connectionHandle, retCode);
			}
			OdbcTransaction odbcTransaction = new OdbcTransaction(this, isolevel, connectionHandle);
			this._weakTransaction = new WeakReference(odbcTransaction);
			return odbcTransaction;
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x0009631C File Offset: 0x0009451C
		internal void Open_ChangeDatabase(string value)
		{
			this.CheckState("ChangeDatabase");
			if (value == null || value.Trim().Length == 0)
			{
				throw ADP.EmptyDatabaseName();
			}
			if (1024 < value.Length * 2 + 2)
			{
				throw ADP.DatabaseNameTooLong();
			}
			this.RollbackDeadTransaction();
			OdbcConnectionHandle connectionHandle = this.ConnectionHandle;
			ODBC32.RetCode retCode = connectionHandle.SetConnectionAttribute3(ODBC32.SQL_ATTR.CURRENT_CATALOG, value, checked(value.Length * 2));
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				this.HandleError(connectionHandle, retCode);
			}
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x0009638B File Offset: 0x0009458B
		internal string Open_GetServerVersion()
		{
			return this.GetInfoStringUnhandled(ODBC32.SQL_INFO.DBMS_VER, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnection" /> class.</summary>
		// Token: 0x06002005 RID: 8197 RVA: 0x00096396 File Offset: 0x00094596
		public OdbcConnection()
		{
			GC.SuppressFinalize(this);
			this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x000963B8 File Offset: 0x000945B8
		private void CopyFrom(OdbcConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			this._userConnectionOptions = connection.UserConnectionOptions;
			this._poolGroup = connection.PoolGroup;
			if (DbConnectionClosedNeverOpened.SingletonInstance == connection._innerConnection)
			{
				this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				return;
			}
			this._innerConnection = DbConnectionClosedPreviouslyOpened.SingletonInstance;
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x0009640C File Offset: 0x0009460C
		internal int CloseCount
		{
			get
			{
				return this._closeCount;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x00096414 File Offset: 0x00094614
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return OdbcConnection.s_connectionFactory;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x0009641C File Offset: 0x0009461C
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				DbConnectionPoolGroup poolGroup = this.PoolGroup;
				if (poolGroup == null)
				{
					return null;
				}
				return poolGroup.ConnectionOptions;
			}
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0009643C File Offset: 0x0009463C
		private string ConnectionString_Get()
		{
			bool shouldHidePassword = this.InnerConnection.ShouldHidePassword;
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
			if (userConnectionOptions == null)
			{
				return "";
			}
			return userConnectionOptions.UsersConnectionString(shouldHidePassword);
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x0009646C File Offset: 0x0009466C
		private void ConnectionString_Set(string value)
		{
			DbConnectionPoolKey key = new DbConnectionPoolKey(value);
			this.ConnectionString_Set(key);
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x00096488 File Offset: 0x00094688
		private void ConnectionString_Set(DbConnectionPoolKey key)
		{
			DbConnectionOptions userConnectionOptions = null;
			DbConnectionPoolGroup connectionPoolGroup = this.ConnectionFactory.GetConnectionPoolGroup(key, null, ref userConnectionOptions);
			DbConnectionInternal innerConnection = this.InnerConnection;
			bool flag = innerConnection.AllowSetConnectionString;
			if (flag)
			{
				flag = this.SetInnerConnectionFrom(DbConnectionClosedBusy.SingletonInstance, innerConnection);
				if (flag)
				{
					this._userConnectionOptions = userConnectionOptions;
					this._poolGroup = connectionPoolGroup;
					this._innerConnection = DbConnectionClosedNeverOpened.SingletonInstance;
				}
			}
			if (!flag)
			{
				throw ADP.OpenConnectionPropertySet("ConnectionString", innerConnection.State);
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x000964F5 File Offset: 0x000946F5
		internal DbConnectionInternal InnerConnection
		{
			get
			{
				return this._innerConnection;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x000964FD File Offset: 0x000946FD
		// (set) Token: 0x0600200F RID: 8207 RVA: 0x00096505 File Offset: 0x00094705
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._poolGroup;
			}
			set
			{
				this._poolGroup = value;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x0009650E File Offset: 0x0009470E
		internal DbConnectionOptions UserConnectionOptions
		{
			get
			{
				return this._userConnectionOptions;
			}
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00096518 File Offset: 0x00094718
		internal void Abort(Exception e)
		{
			DbConnectionInternal innerConnection = this._innerConnection;
			if (ConnectionState.Open == innerConnection.State)
			{
				Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, DbConnectionClosedPreviouslyOpened.SingletonInstance, innerConnection);
				innerConnection.DoomThisConnection();
			}
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0009654D File Offset: 0x0009474D
		internal void AddWeakReference(object value, int tag)
		{
			this.InnerConnection.AddWeakReference(value, tag);
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x0009655C File Offset: 0x0009475C
		protected override DbCommand CreateDbCommand()
		{
			DbCommand dbCommand = this.ConnectionFactory.ProviderFactory.CreateCommand();
			dbCommand.Connection = this;
			return dbCommand;
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x00096575 File Offset: 0x00094775
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._userConnectionOptions = null;
				this._poolGroup = null;
				this.Close();
			}
			this.DisposeMe(disposing);
			base.Dispose(disposing);
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		// Token: 0x06002015 RID: 8213 RVA: 0x00066C88 File Offset: 0x00064E88
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Odbc.OdbcConnection" /> using the specified name for the schema name.</summary>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		// Token: 0x06002016 RID: 8214 RVA: 0x00066C96 File Offset: 0x00064E96
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Odbc.OdbcConnection" /> using the specified string for the schema name and the specified string array for the restriction values.</summary>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <param name="restrictionValues">Specifies a set of restriction values for the requested schema.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		// Token: 0x06002017 RID: 8215 RVA: 0x0009659C File Offset: 0x0009479C
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			return this.InnerConnection.GetSchema(this.ConnectionFactory, this.PoolGroup, this, collectionName, restrictionValues);
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x000965B8 File Offset: 0x000947B8
		internal void NotifyWeakReference(int message)
		{
			this.InnerConnection.NotifyWeakReference(message);
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x000965C8 File Offset: 0x000947C8
		internal void PermissionDemand()
		{
			DbConnectionPoolGroup poolGroup = this.PoolGroup;
			DbConnectionOptions dbConnectionOptions = (poolGroup != null) ? poolGroup.ConnectionOptions : null;
			if (dbConnectionOptions == null || dbConnectionOptions.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			DbConnectionOptions userConnectionOptions = this.UserConnectionOptions;
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00096601 File Offset: 0x00094801
		internal void RemoveWeakReference(object value)
		{
			this.InnerConnection.RemoveWeakReference(value);
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00096610 File Offset: 0x00094810
		internal void SetInnerConnectionEvent(DbConnectionInternal to)
		{
			ConnectionState connectionState = this._innerConnection.State & ConnectionState.Open;
			ConnectionState connectionState2 = to.State & ConnectionState.Open;
			if (connectionState != connectionState2 && connectionState2 == ConnectionState.Closed)
			{
				this._closeCount++;
			}
			this._innerConnection = to;
			if (connectionState == ConnectionState.Closed && ConnectionState.Open == connectionState2)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeOpen);
				return;
			}
			if (ConnectionState.Open == connectionState && connectionState2 == ConnectionState.Closed)
			{
				this.OnStateChange(DbConnectionInternal.StateChangeClosed);
				return;
			}
			if (connectionState != connectionState2)
			{
				this.OnStateChange(new StateChangeEventArgs(connectionState, connectionState2));
			}
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x00096687 File Offset: 0x00094887
		internal bool SetInnerConnectionFrom(DbConnectionInternal to, DbConnectionInternal from)
		{
			return from == Interlocked.CompareExchange<DbConnectionInternal>(ref this._innerConnection, to, from);
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x00096699 File Offset: 0x00094899
		internal void SetInnerConnectionTo(DbConnectionInternal to)
		{
			this._innerConnection = to;
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x000966A2 File Offset: 0x000948A2
		// Note: this type is marked as 'beforefieldinit'.
		static OdbcConnection()
		{
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.EnterpriseServices.ITransaction" /> in which to enlist.</param>
		// Token: 0x0600201F RID: 8223 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001754 RID: 5972
		private int _connectionTimeout = 15;

		// Token: 0x04001755 RID: 5973
		private OdbcInfoMessageEventHandler _infoMessageEventHandler;

		// Token: 0x04001756 RID: 5974
		private WeakReference _weakTransaction;

		// Token: 0x04001757 RID: 5975
		private OdbcConnectionHandle _connectionHandle;

		// Token: 0x04001758 RID: 5976
		private ConnectionState _extraState;

		// Token: 0x04001759 RID: 5977
		private static readonly DbConnectionFactory s_connectionFactory = OdbcConnectionFactory.SingletonInstance;

		// Token: 0x0400175A RID: 5978
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x0400175B RID: 5979
		private DbConnectionPoolGroup _poolGroup;

		// Token: 0x0400175C RID: 5980
		private DbConnectionInternal _innerConnection;

		// Token: 0x0400175D RID: 5981
		private int _closeCount;
	}
}
