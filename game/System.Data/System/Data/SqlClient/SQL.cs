using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace System.Data.SqlClient
{
	// Token: 0x02000238 RID: 568
	internal static class SQL
	{
		// Token: 0x06001B1A RID: 6938 RVA: 0x0007C8FB File Offset: 0x0007AAFB
		internal static Exception CannotGetDTCAddress()
		{
			return ADP.InvalidOperation(SR.GetString("Unable to get the address of the distributed transaction coordinator for the server, from the server.  Is DTC enabled on the server?"));
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0007C90C File Offset: 0x0007AB0C
		internal static Exception InvalidInternalPacketSize(string str)
		{
			return ADP.ArgumentOutOfRange(str);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0007C914 File Offset: 0x0007AB14
		internal static Exception InvalidPacketSize()
		{
			return ADP.ArgumentOutOfRange(SR.GetString("Invalid Packet Size."));
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0007C925 File Offset: 0x0007AB25
		internal static Exception InvalidPacketSizeValue()
		{
			return ADP.Argument(SR.GetString("Invalid 'Packet Size'.  The value must be an integer >= 512 and <= 32768."));
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0007C936 File Offset: 0x0007AB36
		internal static Exception InvalidSSPIPacketSize()
		{
			return ADP.Argument(SR.GetString("Invalid SSPI packet size."));
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0007C947 File Offset: 0x0007AB47
		internal static Exception NullEmptyTransactionName()
		{
			return ADP.Argument(SR.GetString("Invalid transaction or invalid name for a point at which to save within the transaction."));
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0007C958 File Offset: 0x0007AB58
		internal static Exception UserInstanceFailoverNotCompatible()
		{
			return ADP.Argument(SR.GetString("User Instance and Failover are not compatible options.  Please choose only one of the two in the connection string."));
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0007C96C File Offset: 0x0007AB6C
		internal static Exception ParsingErrorLibraryType(ParsingErrorState state, int libraryType)
		{
			string name = "Internal connection fatal error. Error state: {0}, Authentication Library Type: {1}.";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = libraryType;
			return ADP.InvalidOperation(SR.GetString(name, array));
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0007C9A8 File Offset: 0x0007ABA8
		internal static Exception InvalidSQLServerVersionUnknown()
		{
			return ADP.DataAdapter(SR.GetString("Unsupported SQL Server version.  The .Net Framework SqlClient Data Provider can only be used with SQL Server versions 7.0 and later."));
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0007C9B9 File Offset: 0x0007ABB9
		internal static Exception SynchronousCallMayNotPend()
		{
			return new Exception(SR.GetString("Internal Error"));
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0007C9CA File Offset: 0x0007ABCA
		internal static Exception ConnectionLockedForBcpEvent()
		{
			return ADP.InvalidOperation(SR.GetString("The connection cannot be used because there is an ongoing operation that must be finished."));
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0007C9DB File Offset: 0x0007ABDB
		internal static Exception InstanceFailure()
		{
			return ADP.InvalidOperation(SR.GetString("Instance failure."));
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0007C9EC File Offset: 0x0007ABEC
		internal static Exception ChangePasswordArgumentMissing(string argumentName)
		{
			return ADP.ArgumentNull(SR.GetString("The '{0}' argument must not be null or empty.", new object[]
			{
				argumentName
			}));
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0007CA07 File Offset: 0x0007AC07
		internal static Exception ChangePasswordConflictsWithSSPI()
		{
			return ADP.Argument(SR.GetString("ChangePassword can only be used with SQL authentication, not with integrated security."));
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0007CA18 File Offset: 0x0007AC18
		internal static Exception ChangePasswordRequiresYukon()
		{
			return ADP.InvalidOperation(SR.GetString("ChangePassword requires SQL Server 9.0 or later."));
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0007CA29 File Offset: 0x0007AC29
		internal static Exception ChangePasswordUseOfUnallowedKey(string key)
		{
			return ADP.InvalidOperation(SR.GetString("The keyword '{0}' must not be specified in the connectionString argument to ChangePassword.", new object[]
			{
				key
			}));
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0007CA44 File Offset: 0x0007AC44
		internal static Exception GlobalTransactionsNotEnabled()
		{
			return ADP.InvalidOperation(SR.GetString("Global Transactions are not enabled for this Azure SQL Database. Please contact Azure SQL Database support for assistance."));
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0007CA55 File Offset: 0x0007AC55
		internal static Exception UnknownSysTxIsolationLevel(IsolationLevel isolationLevel)
		{
			return ADP.InvalidOperation(SR.GetString("Unrecognized System.Transactions.IsolationLevel enumeration value: {0}.", new object[]
			{
				isolationLevel.ToString()
			}));
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0007CA7C File Offset: 0x0007AC7C
		internal static Exception InvalidPartnerConfiguration(string server, string database)
		{
			return ADP.InvalidOperation(SR.GetString("Server {0}, database {1} is not configured for database mirroring.", new object[]
			{
				server,
				database
			}));
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0007CA9B File Offset: 0x0007AC9B
		internal static Exception MARSUnspportedOnConnection()
		{
			return ADP.InvalidOperation(SR.GetString("The connection does not support MultipleActiveResultSets."));
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0007CAAC File Offset: 0x0007ACAC
		internal static Exception CannotModifyPropertyAsyncOperationInProgress([CallerMemberName] string property = "")
		{
			return ADP.InvalidOperation(SR.GetString("{0} cannot be changed while async operation is in progress.", new object[]
			{
				property
			}));
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0007CAC7 File Offset: 0x0007ACC7
		internal static Exception NonLocalSSEInstance()
		{
			return ADP.NotSupported(SR.GetString("SSE Instance re-direction is not supported for non-local user instances."));
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0007CAD8 File Offset: 0x0007ACD8
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(SR.GetString("The {0} enumeration value, {1}, is not supported by the .Net Framework SqlClient Data Provider.", new object[]
			{
				type.Name,
				value.ToString(CultureInfo.InvariantCulture)
			}), type.Name);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0007CB0D File Offset: 0x0007AD0D
		internal static ArgumentOutOfRangeException NotSupportedCommandType(CommandType value)
		{
			return SQL.NotSupportedEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0007CB1F File Offset: 0x0007AD1F
		internal static ArgumentOutOfRangeException NotSupportedIsolationLevel(IsolationLevel value)
		{
			return SQL.NotSupportedEnumerationValue(typeof(IsolationLevel), (int)value);
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0007CB31 File Offset: 0x0007AD31
		internal static Exception OperationCancelled()
		{
			return ADP.InvalidOperation(SR.GetString("Operation cancelled by user."));
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0007CB42 File Offset: 0x0007AD42
		internal static Exception PendingBeginXXXExists()
		{
			return ADP.InvalidOperation(SR.GetString("The command execution cannot proceed due to a pending asynchronous operation already in progress."));
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0007CB53 File Offset: 0x0007AD53
		internal static ArgumentOutOfRangeException InvalidSqlDependencyTimeout(string param)
		{
			return ADP.ArgumentOutOfRange(SR.GetString("Timeout specified is invalid. Timeout cannot be < 0."), param);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0007CB65 File Offset: 0x0007AD65
		internal static Exception NonXmlResult()
		{
			return ADP.InvalidOperation(SR.GetString("Invalid command sent to ExecuteXmlReader.  The command must return an Xml result."));
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0007CB76 File Offset: 0x0007AD76
		internal static Exception InvalidUdt3PartNameFormat()
		{
			return ADP.Argument(SR.GetString("Invalid 3 part name format for UdtTypeName."));
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0007CB87 File Offset: 0x0007AD87
		internal static Exception InvalidParameterTypeNameFormat()
		{
			return ADP.Argument(SR.GetString("Invalid 3 part name format for TypeName."));
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0007CB98 File Offset: 0x0007AD98
		internal static Exception InvalidParameterNameLength(string value)
		{
			return ADP.Argument(SR.GetString("The length of the parameter '{0}' exceeds the limit of 128 characters.", new object[]
			{
				value
			}));
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0007CBB3 File Offset: 0x0007ADB3
		internal static Exception PrecisionValueOutOfRange(byte precision)
		{
			return ADP.Argument(SR.GetString("Precision value '{0}' is either less than 0 or greater than the maximum allowed precision of 38.", new object[]
			{
				precision.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0007CBD9 File Offset: 0x0007ADD9
		internal static Exception ScaleValueOutOfRange(byte scale)
		{
			return ADP.Argument(SR.GetString("Scale value '{0}' is either less than 0 or greater than the maximum allowed scale of 38.", new object[]
			{
				scale.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0007CBFF File Offset: 0x0007ADFF
		internal static Exception TimeScaleValueOutOfRange(byte scale)
		{
			return ADP.Argument(SR.GetString("Scale value '{0}' is either less than 0 or greater than the maximum allowed scale of 7.", new object[]
			{
				scale.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0007CC25 File Offset: 0x0007AE25
		internal static Exception InvalidSqlDbType(SqlDbType value)
		{
			return ADP.InvalidEnumerationValue(typeof(SqlDbType), (int)value);
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x0007CC37 File Offset: 0x0007AE37
		internal static Exception UnsupportedTVPOutputParameter(ParameterDirection direction, string paramName)
		{
			return ADP.NotSupported(SR.GetString("ParameterDirection '{0}' specified for parameter '{1}' is not supported. Table-valued parameters only support ParameterDirection.Input.", new object[]
			{
				direction.ToString(),
				paramName
			}));
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0007CC62 File Offset: 0x0007AE62
		internal static Exception DBNullNotSupportedForTVPValues(string paramName)
		{
			return ADP.NotSupported(SR.GetString("DBNull value for parameter '{0}' is not supported. Table-valued parameters cannot be DBNull.", new object[]
			{
				paramName
			}));
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x0007CC7D File Offset: 0x0007AE7D
		internal static Exception UnexpectedTypeNameForNonStructParams(string paramName)
		{
			return ADP.NotSupported(SR.GetString("TypeName specified for parameter '{0}'.  TypeName must only be set for Structured parameters.", new object[]
			{
				paramName
			}));
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0007CC98 File Offset: 0x0007AE98
		internal static Exception ParameterInvalidVariant(string paramName)
		{
			return ADP.InvalidOperation(SR.GetString("Parameter '{0}' exceeds the size limit for the sql_variant datatype.", new object[]
			{
				paramName
			}));
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0007CCB3 File Offset: 0x0007AEB3
		internal static Exception MustSetTypeNameForParam(string paramType, string paramName)
		{
			return ADP.Argument(SR.GetString("The {0} type parameter '{1}' must have a valid type name.", new object[]
			{
				paramType,
				paramName
			}));
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0007CCD2 File Offset: 0x0007AED2
		internal static Exception NullSchemaTableDataTypeNotSupported(string columnName)
		{
			return ADP.Argument(SR.GetString("DateType column for field '{0}' in schema table is null.  DataType must be non-null.", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0007CCED File Offset: 0x0007AEED
		internal static Exception InvalidSchemaTableOrdinals()
		{
			return ADP.Argument(SR.GetString("Invalid column ordinals in schema table.  ColumnOrdinals, if present, must not have duplicates or gaps."));
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0007CCFE File Offset: 0x0007AEFE
		internal static Exception EnumeratedRecordMetaDataChanged(string fieldName, int recordNumber)
		{
			return ADP.Argument(SR.GetString("Metadata for field '{0}' of record '{1}' did not match the original record's metadata.", new object[]
			{
				fieldName,
				recordNumber
			}));
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0007CD22 File Offset: 0x0007AF22
		internal static Exception EnumeratedRecordFieldCountChanged(int recordNumber)
		{
			return ADP.Argument(SR.GetString("Number of fields in record '{0}' does not match the number in the original record.", new object[]
			{
				recordNumber
			}));
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0007CD42 File Offset: 0x0007AF42
		internal static Exception InvalidTDSVersion()
		{
			return ADP.InvalidOperation(SR.GetString("The SQL Server instance returned an invalid or unsupported protocol version during login negotiation."));
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0007CD53 File Offset: 0x0007AF53
		internal static Exception ParsingError()
		{
			return ADP.InvalidOperation(SR.GetString("Internal connection fatal error."));
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0007CD64 File Offset: 0x0007AF64
		internal static Exception ParsingError(ParsingErrorState state)
		{
			string name = "Internal connection fatal error. Error state: {0}.";
			object[] array = new object[1];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			return ADP.InvalidOperation(SR.GetString(name, array));
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0007CD98 File Offset: 0x0007AF98
		internal static Exception ParsingErrorValue(ParsingErrorState state, int value)
		{
			string name = "Internal connection fatal error. Error state: {0}, Value: {1}.";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = value;
			return ADP.InvalidOperation(SR.GetString(name, array));
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0007CDD4 File Offset: 0x0007AFD4
		internal static Exception ParsingErrorFeatureId(ParsingErrorState state, int featureId)
		{
			string name = "Internal connection fatal error. Error state: {0}, Feature Id: {1}.";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)state;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = featureId;
			return ADP.InvalidOperation(SR.GetString(name, array));
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0007CE10 File Offset: 0x0007B010
		internal static Exception MoneyOverflow(string moneyValue)
		{
			return ADP.Overflow(SR.GetString("SqlDbType.SmallMoney overflow.  Value '{0}' is out of range.  Must be between -214,748.3648 and 214,748.3647.", new object[]
			{
				moneyValue
			}));
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0007CE2B File Offset: 0x0007B02B
		internal static Exception SmallDateTimeOverflow(string datetime)
		{
			return ADP.Overflow(SR.GetString("SqlDbType.SmallDateTime overflow.  Value '{0}' is out of range.  Must be between 1/1/1900 12:00:00 AM and 6/6/2079 11:59:59 PM.", new object[]
			{
				datetime
			}));
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0007CE46 File Offset: 0x0007B046
		internal static Exception SNIPacketAllocationFailure()
		{
			return ADP.InvalidOperation(SR.GetString("Memory allocation for internal connection failed."));
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0007CE57 File Offset: 0x0007B057
		internal static Exception TimeOverflow(string time)
		{
			return ADP.Overflow(SR.GetString("SqlDbType.Time overflow.  Value '{0}' is out of range.  Must be between 00:00:00.0000000 and 23:59:59.9999999.", new object[]
			{
				time
			}));
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0007CE72 File Offset: 0x0007B072
		internal static Exception InvalidRead()
		{
			return ADP.InvalidOperation(SR.GetString("Invalid attempt to read when no data is present."));
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0007CE83 File Offset: 0x0007B083
		internal static Exception NonBlobColumn(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetBytes on column '{0}'.  The GetBytes function can only be used on columns of type Text, NText, or Image.", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0007CE9E File Offset: 0x0007B09E
		internal static Exception NonCharColumn(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetChars on column '{0}'.  The GetChars function can only be used on columns of type Text, NText, Xml, VarChar or NVarChar.", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0007CEB9 File Offset: 0x0007B0B9
		internal static Exception StreamNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetStream on column '{0}'. The GetStream function can only be used on columns of type Binary, Image, Udt or VarBinary.", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0007CED4 File Offset: 0x0007B0D4
		internal static Exception TextReaderNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetTextReader on column '{0}'. The GetTextReader function can only be used on columns of type Char, NChar, NText, NVarChar, Text or VarChar.", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0007CEEF File Offset: 0x0007B0EF
		internal static Exception XmlReaderNotSupportOnColumnType(string columnName)
		{
			return ADP.InvalidCast(SR.GetString("Invalid attempt to GetXmlReader on column '{0}'. The GetXmlReader function can only be used on columns of type Xml.", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0007CF0A File Offset: 0x0007B10A
		internal static Exception UDTUnexpectedResult(string exceptionText)
		{
			return ADP.TypeLoad(SR.GetString("unexpected error encountered in SqlClient data provider. {0}", new object[]
			{
				exceptionText
			}));
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0007CF25 File Offset: 0x0007B125
		internal static Exception SqlCommandHasExistingSqlNotificationRequest()
		{
			return ADP.InvalidOperation(SR.GetString("This SqlCommand object is already associated with another SqlDependency object."));
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0007CF36 File Offset: 0x0007B136
		internal static Exception SqlDepDefaultOptionsButNoStart()
		{
			return ADP.InvalidOperation(SR.GetString("When using SqlDependency without providing an options value, SqlDependency.Start() must be called prior to execution of a command added to the SqlDependency instance."));
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0007CF47 File Offset: 0x0007B147
		internal static Exception SqlDependencyDatabaseBrokerDisabled()
		{
			return ADP.InvalidOperation(SR.GetString("The SQL Server Service Broker for the current database is not enabled, and as a result query notifications are not supported.  Please enable the Service Broker for this database if you wish to use notifications."));
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x0007CF58 File Offset: 0x0007B158
		internal static Exception SqlDependencyEventNoDuplicate()
		{
			return ADP.InvalidOperation(SR.GetString("SqlDependency.OnChange does not support multiple event registrations for the same delegate."));
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x0007CF69 File Offset: 0x0007B169
		internal static Exception SqlDependencyDuplicateStart()
		{
			return ADP.InvalidOperation(SR.GetString("SqlDependency does not support calling Start() with different connection strings having the same server, user, and database in the same app domain."));
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x0007CF7A File Offset: 0x0007B17A
		internal static Exception SqlDependencyIdMismatch()
		{
			return ADP.InvalidOperation(SR.GetString("No SqlDependency exists for the key."));
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0007CF8B File Offset: 0x0007B18B
		internal static Exception SqlDependencyNoMatchingServerStart()
		{
			return ADP.InvalidOperation(SR.GetString("When using SqlDependency without providing an options value, SqlDependency.Start() must be called for each server that is being executed against."));
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0007CF9C File Offset: 0x0007B19C
		internal static Exception SqlDependencyNoMatchingServerDatabaseStart()
		{
			return ADP.InvalidOperation(SR.GetString("SqlDependency.Start has been called for the server the command is executing against more than once, but there is no matching server/user/database Start() call for current command."));
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0007CFAD File Offset: 0x0007B1AD
		internal static TransactionPromotionException PromotionFailed(Exception inner)
		{
			TransactionPromotionException ex = new TransactionPromotionException(SR.GetString("Failure while attempting to promote transaction."), inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0007CFC5 File Offset: 0x0007B1C5
		internal static Exception UnexpectedUdtTypeNameForNonUdtParams()
		{
			return ADP.Argument(SR.GetString("UdtTypeName property must be set only for UDT parameters."));
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0007CFD6 File Offset: 0x0007B1D6
		internal static Exception MustSetUdtTypeNameForUdtParams()
		{
			return ADP.Argument(SR.GetString("UdtTypeName property must be set for UDT parameters."));
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0007CFE7 File Offset: 0x0007B1E7
		internal static Exception UDTInvalidSqlType(string typeName)
		{
			return ADP.Argument(SR.GetString("Specified type is not registered on the target server. {0}.", new object[]
			{
				typeName
			}));
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0007D002 File Offset: 0x0007B202
		internal static Exception InvalidSqlDbTypeForConstructor(SqlDbType type)
		{
			return ADP.Argument(SR.GetString("The dbType {0} is invalid for this constructor.", new object[]
			{
				type.ToString()
			}));
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0007D029 File Offset: 0x0007B229
		internal static Exception NameTooLong(string parameterName)
		{
			return ADP.Argument(SR.GetString("The name is too long."), parameterName);
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0007D03B File Offset: 0x0007B23B
		internal static Exception InvalidSortOrder(SortOrder order)
		{
			return ADP.InvalidEnumerationValue(typeof(SortOrder), (int)order);
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0007D04D File Offset: 0x0007B24D
		internal static Exception MustSpecifyBothSortOrderAndOrdinal(SortOrder order, int ordinal)
		{
			return ADP.InvalidOperation(SR.GetString("The sort order and ordinal must either both be specified, or neither should be specified (SortOrder.Unspecified and -1).  The values given were: order = {0}, ordinal = {1}.", new object[]
			{
				order.ToString(),
				ordinal
			}));
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0007D07D File Offset: 0x0007B27D
		internal static Exception UnsupportedColumnTypeForSqlProvider(string columnName, string typeName)
		{
			return ADP.Argument(SR.GetString("The type of column '{0}' is not supported.  The type is '{1}'", new object[]
			{
				columnName,
				typeName
			}));
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0007D09C File Offset: 0x0007B29C
		internal static Exception InvalidColumnMaxLength(string columnName, long maxLength)
		{
			return ADP.Argument(SR.GetString("The size of column '{0}' is not supported. The size is {1}.", new object[]
			{
				columnName,
				maxLength
			}));
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0007D0C0 File Offset: 0x0007B2C0
		internal static Exception InvalidColumnPrecScale()
		{
			return ADP.Argument(SR.GetString("Invalid numeric precision/scale."));
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0007D0D1 File Offset: 0x0007B2D1
		internal static Exception NotEnoughColumnsInStructuredType()
		{
			return ADP.Argument(SR.GetString("There are not enough fields in the Structured type.  Structured types must have at least one field."));
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0007D0E2 File Offset: 0x0007B2E2
		internal static Exception DuplicateSortOrdinal(int sortOrdinal)
		{
			return ADP.InvalidOperation(SR.GetString("The sort ordinal {0} was specified twice.", new object[]
			{
				sortOrdinal
			}));
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x0007D102 File Offset: 0x0007B302
		internal static Exception MissingSortOrdinal(int sortOrdinal)
		{
			return ADP.InvalidOperation(SR.GetString("The sort ordinal {0} was not specified.", new object[]
			{
				sortOrdinal
			}));
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x0007D122 File Offset: 0x0007B322
		internal static Exception SortOrdinalGreaterThanFieldCount(int columnOrdinal, int sortOrdinal)
		{
			return ADP.InvalidOperation(SR.GetString("The sort ordinal {0} on field {1} exceeds the total number of fields.", new object[]
			{
				sortOrdinal,
				columnOrdinal
			}));
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0007D14B File Offset: 0x0007B34B
		internal static Exception IEnumerableOfSqlDataRecordHasNoRows()
		{
			return ADP.Argument(SR.GetString("There are no records in the SqlDataRecord enumeration. To send a table-valued parameter with no rows, use a null reference for the value instead."));
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0007D15C File Offset: 0x0007B35C
		internal static Exception BulkLoadMappingInaccessible()
		{
			return ADP.InvalidOperation(SR.GetString("The mapped collection is in use and cannot be accessed at this time;"));
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x0007D16D File Offset: 0x0007B36D
		internal static Exception BulkLoadMappingsNamesOrOrdinalsOnly()
		{
			return ADP.InvalidOperation(SR.GetString("Mappings must be either all name or all ordinal based."));
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0007D17E File Offset: 0x0007B37E
		internal static Exception BulkLoadCannotConvertValue(Type sourcetype, MetaType metatype, Exception e)
		{
			return ADP.InvalidOperation(SR.GetString("The given value of type {0} from the data source cannot be converted to type {1} of the specified target column.", new object[]
			{
				sourcetype.Name,
				metatype.TypeName
			}), e);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0007D1A8 File Offset: 0x0007B3A8
		internal static Exception BulkLoadNonMatchingColumnMapping()
		{
			return ADP.InvalidOperation(SR.GetString("The given ColumnMapping does not match up with any column in the source or destination."));
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0007D1B9 File Offset: 0x0007B3B9
		internal static Exception BulkLoadNonMatchingColumnName(string columnName)
		{
			return SQL.BulkLoadNonMatchingColumnName(columnName, null);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0007D1C2 File Offset: 0x0007B3C2
		internal static Exception BulkLoadNonMatchingColumnName(string columnName, Exception e)
		{
			return ADP.InvalidOperation(SR.GetString("The given ColumnName '{0}' does not match up with any column in data source.", new object[]
			{
				columnName
			}), e);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0007D1DE File Offset: 0x0007B3DE
		internal static Exception BulkLoadStringTooLong()
		{
			return ADP.InvalidOperation(SR.GetString("String or binary data would be truncated."));
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0007D1EF File Offset: 0x0007B3EF
		internal static Exception BulkLoadInvalidVariantValue()
		{
			return ADP.InvalidOperation(SR.GetString("Value cannot be converted to SqlVariant."));
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0007D200 File Offset: 0x0007B400
		internal static Exception BulkLoadInvalidTimeout(int timeout)
		{
			return ADP.Argument(SR.GetString("Timeout Value '{0}' is less than 0.", new object[]
			{
				timeout.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0007D226 File Offset: 0x0007B426
		internal static Exception BulkLoadExistingTransaction()
		{
			return ADP.InvalidOperation(SR.GetString("Unexpected existing transaction."));
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0007D237 File Offset: 0x0007B437
		internal static Exception BulkLoadNoCollation()
		{
			return ADP.InvalidOperation(SR.GetString("Failed to obtain column collation information for the destination table. If the table is not in the current database the name must be qualified using the database name (e.g. [mydb]..[mytable](e.g. [mydb]..[mytable]); this also applies to temporary-tables (e.g. #mytable would be specified as tempdb..#mytable)."));
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0007D248 File Offset: 0x0007B448
		internal static Exception BulkLoadConflictingTransactionOption()
		{
			return ADP.Argument(SR.GetString("Must not specify SqlBulkCopyOption.UseInternalTransaction and pass an external Transaction at the same time."));
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0007D259 File Offset: 0x0007B459
		internal static Exception BulkLoadLcidMismatch(int sourceLcid, string sourceColumnName, int destinationLcid, string destinationColumnName)
		{
			return ADP.InvalidOperation(SR.GetString("The locale id '{0}' of the source column '{1}' and the locale id '{2}' of the destination column '{3}' do not match.", new object[]
			{
				sourceLcid,
				sourceColumnName,
				destinationLcid,
				destinationColumnName
			}));
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0007D28A File Offset: 0x0007B48A
		internal static Exception InvalidOperationInsideEvent()
		{
			return ADP.InvalidOperation(SR.GetString("Function must not be called during event."));
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0007D29B File Offset: 0x0007B49B
		internal static Exception BulkLoadMissingDestinationTable()
		{
			return ADP.InvalidOperation(SR.GetString("The DestinationTableName property must be set before calling this method."));
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0007D2AC File Offset: 0x0007B4AC
		internal static Exception BulkLoadInvalidDestinationTable(string tableName, Exception inner)
		{
			return ADP.InvalidOperation(SR.GetString("Cannot access destination table '{0}'.", new object[]
			{
				tableName
			}), inner);
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0007D2C8 File Offset: 0x0007B4C8
		internal static Exception BulkLoadBulkLoadNotAllowDBNull(string columnName)
		{
			return ADP.InvalidOperation(SR.GetString("Column '{0}' does not allow DBNull.Value.", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0007D2E3 File Offset: 0x0007B4E3
		internal static Exception BulkLoadPendingOperation()
		{
			return ADP.InvalidOperation(SR.GetString("Attempt to invoke bulk copy on an object that has a pending operation."));
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0007D2F4 File Offset: 0x0007B4F4
		internal static Exception InvalidTableDerivedPrecisionForTvp(string columnName, byte precision)
		{
			return ADP.InvalidOperation(SR.GetString("Precision '{0}' required to send all values in column '{1}' exceeds the maximum supported precision '{2}'. The values must all fit in a single precision.", new object[]
			{
				precision,
				columnName,
				SqlDecimal.MaxPrecision
			}));
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0007D325 File Offset: 0x0007B525
		internal static Exception ConnectionDoomed()
		{
			return ADP.InvalidOperation(SR.GetString("The requested operation cannot be completed because the connection has been broken."));
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0007D336 File Offset: 0x0007B536
		internal static Exception OpenResultCountExceeded()
		{
			return ADP.InvalidOperation(SR.GetString("Open result count exceeded."));
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0007D347 File Offset: 0x0007B547
		internal static Exception UnsupportedSysTxForGlobalTransactions()
		{
			return ADP.InvalidOperation(SR.GetString("The currently loaded System.Transactions.dll does not support Global Transactions."));
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x0007D358 File Offset: 0x0007B558
		internal static Exception MultiSubnetFailoverWithFailoverPartner(bool serverProvidedFailoverPartner, SqlInternalConnectionTds internalConnection)
		{
			string @string = SR.GetString("Connecting to a mirrored SQL Server instance using the MultiSubnetFailover connection option is not supported.");
			if (serverProvidedFailoverPartner)
			{
				SqlException ex = SqlException.CreateException(new SqlErrorCollection
				{
					new SqlError(0, 0, 20, null, @string, "", 0, null)
				}, null, internalConnection, null);
				ex._doNotReconnect = true;
				return ex;
			}
			return ADP.Argument(@string);
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x0007D3A6 File Offset: 0x0007B5A6
		internal static Exception MultiSubnetFailoverWithMoreThan64IPs()
		{
			return ADP.InvalidOperation(SQL.GetSNIErrorMessage(47));
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x0007D3B4 File Offset: 0x0007B5B4
		internal static Exception MultiSubnetFailoverWithInstanceSpecified()
		{
			return ADP.Argument(SQL.GetSNIErrorMessage(48));
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x0007D3C2 File Offset: 0x0007B5C2
		internal static Exception MultiSubnetFailoverWithNonTcpProtocol()
		{
			return ADP.Argument(SQL.GetSNIErrorMessage(49));
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0007D3D0 File Offset: 0x0007B5D0
		internal static Exception ROR_FailoverNotSupportedConnString()
		{
			return ADP.Argument(SR.GetString("Connecting to a mirrored SQL Server instance using the ApplicationIntent ReadOnly connection option is not supported."));
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x0007D3E4 File Offset: 0x0007B5E4
		internal static Exception ROR_FailoverNotSupportedServer(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Connecting to a mirrored SQL Server instance using the ApplicationIntent ReadOnly connection option is not supported."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x0007D428 File Offset: 0x0007B628
		internal static Exception ROR_RecursiveRoutingNotSupported(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Two or more redirections have occurred. Only one redirection per login is allowed."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x0007D46C File Offset: 0x0007B66C
		internal static Exception ROR_UnexpectedRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Unexpected routing information received."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0007D4B0 File Offset: 0x0007B6B0
		internal static Exception ROR_InvalidRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Invalid routing information received."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0007D4F4 File Offset: 0x0007B6F4
		internal static Exception ROR_TimeoutAfterRoutingInfo(SqlInternalConnectionTds internalConnection)
		{
			SqlException ex = SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("Server provided routing information, but timeout already expired."), "", 0, null)
			}, null, internalConnection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0007D538 File Offset: 0x0007B738
		internal static SqlException CR_ReconnectTimeout()
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(-2, 0, 11, null, SQLMessage.Timeout(), "", 0, 258U, null)
			}, "");
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0007D578 File Offset: 0x0007B778
		internal static SqlException CR_ReconnectionCancelled()
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, SQLMessage.OperationCancelled(), "", 0, null)
			}, "");
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0007D5B0 File Offset: 0x0007B7B0
		internal static Exception CR_NextAttemptWillExceedQueryTimeout(SqlException innerException, Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, SR.GetString("Next reconnection attempt will exceed query timeout. Reconnection was terminated."), "", 0, null)
			}, "", connectionId, innerException);
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0007D5F0 File Offset: 0x0007B7F0
		internal static Exception CR_EncryptionChanged(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The server did not preserve SSL encryption during a recovery attempt, connection recovery is not possible."), "", 0, null)
			}, "", internalConnection, null);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0007D630 File Offset: 0x0007B830
		internal static SqlException CR_AllAttemptsFailed(SqlException innerException, Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 11, null, SR.GetString("The connection is broken and recovery is not possible.  The client driver attempted to recover the connection one or more times and all attempts failed.  Increase the value of ConnectRetryCount to increase the number of recovery attempts."), "", 0, null)
			}, "", connectionId, innerException);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0007D670 File Offset: 0x0007B870
		internal static SqlException CR_NoCRAckAtReconnection(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The server did not acknowledge a recovery attempt, connection recovery is not possible."), "", 0, null)
			}, "", internalConnection, null);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0007D6B0 File Offset: 0x0007B8B0
		internal static SqlException CR_TDSVersionNotPreserved(SqlInternalConnectionTds internalConnection)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The server did not preserve the exact client TDS version requested during a recovery attempt, connection recovery is not possible."), "", 0, null)
			}, "", internalConnection, null);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0007D6F0 File Offset: 0x0007B8F0
		internal static SqlException CR_UnrecoverableServer(Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The connection is broken and recovery is not possible.  The connection is marked by the server as unrecoverable.  No attempt was made to restore the connection."), "", 0, null)
			}, "", connectionId, null);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0007D730 File Offset: 0x0007B930
		internal static SqlException CR_UnrecoverableClient(Guid connectionId)
		{
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The connection is broken and recovery is not possible.  The connection is marked by the client driver as unrecoverable.  No attempt was made to restore the connection."), "", 0, null)
			}, "", connectionId, null);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0007D76F File Offset: 0x0007B96F
		internal static Exception StreamWriteNotSupported()
		{
			return ADP.NotSupported(SR.GetString("The Stream does not support writing."));
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0007D780 File Offset: 0x0007B980
		internal static Exception StreamReadNotSupported()
		{
			return ADP.NotSupported(SR.GetString("The Stream does not support reading."));
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0007D791 File Offset: 0x0007B991
		internal static Exception StreamSeekNotSupported()
		{
			return ADP.NotSupported(SR.GetString("The Stream does not support seeking."));
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0007D7A2 File Offset: 0x0007B9A2
		internal static SqlNullValueException SqlNullValue()
		{
			return new SqlNullValueException();
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0007D7A9 File Offset: 0x0007B9A9
		internal static Exception SubclassMustOverride()
		{
			return ADP.InvalidOperation(SR.GetString("Subclass did not override a required method."));
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0007D7BA File Offset: 0x0007B9BA
		internal static Exception UnsupportedKeyword(string keyword)
		{
			return ADP.NotSupported(SR.GetString("The keyword '{0}' is not supported on this platform.", new object[]
			{
				keyword
			}));
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0007D7D5 File Offset: 0x0007B9D5
		internal static Exception NetworkLibraryKeywordNotSupported()
		{
			return ADP.NotSupported(SR.GetString("The keyword 'Network Library' is not supported on this platform, prefix the 'Data Source' with the protocol desired instead ('tcp:' for a TCP connection, or 'np:' for a Named Pipe connection)."));
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0007D7E8 File Offset: 0x0007B9E8
		internal static Exception UnsupportedFeatureAndToken(SqlInternalConnectionTds internalConnection, string token)
		{
			NotSupportedException innerException = ADP.NotSupported(SR.GetString("Received an unsupported token '{0}' while reading data from the server.", new object[]
			{
				token
			}));
			return SqlException.CreateException(new SqlErrorCollection
			{
				new SqlError(0, 0, 20, null, SR.GetString("The server is attempting to use a feature that is not supported on this platform."), "", 0, null)
			}, "", internalConnection, innerException);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0007D841 File Offset: 0x0007BA41
		internal static Exception BatchedUpdatesNotAvailableOnContextConnection()
		{
			return ADP.InvalidOperation(SR.GetString("Batching updates is not supported on the context connection."));
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0007D852 File Offset: 0x0007BA52
		internal static string GetSNIErrorMessage(int sniError)
		{
			string text = string.Format(null, "SNI_ERROR_{0}", sniError);
			return SR.GetResourceString(text, text);
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0007D86B File Offset: 0x0007BA6B
		// Note: this type is marked as 'beforefieldinit'.
		static SQL()
		{
		}

		// Token: 0x0400115A RID: 4442
		internal static readonly byte[] AttentionHeader = new byte[]
		{
			6,
			1,
			0,
			8,
			0,
			0,
			0,
			0
		};

		// Token: 0x0400115B RID: 4443
		internal const int SqlDependencyTimeoutDefault = 0;

		// Token: 0x0400115C RID: 4444
		internal const int SqlDependencyServerTimeout = 432000;

		// Token: 0x0400115D RID: 4445
		internal const string SqlNotificationServiceDefault = "SqlQueryNotificationService";

		// Token: 0x0400115E RID: 4446
		internal const string SqlNotificationStoredProcedureDefault = "SqlQueryNotificationStoredProcedure";
	}
}
