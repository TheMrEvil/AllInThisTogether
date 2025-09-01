using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace System.Data.Common
{
	// Token: 0x02000367 RID: 871
	internal static class ADP
	{
		// Token: 0x060027D3 RID: 10195 RVA: 0x000B1544 File Offset: 0x000AF744
		internal static Timer UnsafeCreateTimer(TimerCallback callback, object state, int dueTime, int period)
		{
			bool flag = false;
			Timer result;
			try
			{
				if (!ExecutionContext.IsFlowSuppressed())
				{
					ExecutionContext.SuppressFlow();
					flag = true;
				}
				result = new Timer(callback, state, dueTime, period);
			}
			finally
			{
				if (flag)
				{
					ExecutionContext.RestoreFlow();
				}
			}
			return result;
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060027D4 RID: 10196 RVA: 0x000B1588 File Offset: 0x000AF788
		internal static Task<bool> TrueTask
		{
			get
			{
				Task<bool> result;
				if ((result = ADP._trueTask) == null)
				{
					result = (ADP._trueTask = Task.FromResult<bool>(true));
				}
				return result;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060027D5 RID: 10197 RVA: 0x000B159F File Offset: 0x000AF79F
		internal static Task<bool> FalseTask
		{
			get
			{
				Task<bool> result;
				if ((result = ADP._falseTask) == null)
				{
					result = (ADP._falseTask = Task.FromResult<bool>(false));
				}
				return result;
			}
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x0001A853 File Offset: 0x00018A53
		private static void TraceException(string trace, Exception e)
		{
			if (e != null)
			{
				DataCommonEventSource.Log.Trace<Exception>(trace, e);
			}
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000B15B6 File Offset: 0x000AF7B6
		internal static void TraceExceptionAsReturnValue(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|THROW> '{0}'", e);
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000B15C3 File Offset: 0x000AF7C3
		internal static void TraceExceptionWithoutRethrow(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|CATCH> '%ls'\n", e);
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x000B15D0 File Offset: 0x000AF7D0
		internal static ArgumentException Argument(string error)
		{
			ArgumentException ex = new ArgumentException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000B15DE File Offset: 0x000AF7DE
		internal static ArgumentException Argument(string error, Exception inner)
		{
			ArgumentException ex = new ArgumentException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x000B15ED File Offset: 0x000AF7ED
		internal static ArgumentException Argument(string error, string parameter)
		{
			ArgumentException ex = new ArgumentException(error, parameter);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x000B15FC File Offset: 0x000AF7FC
		internal static ArgumentNullException ArgumentNull(string parameter)
		{
			ArgumentNullException ex = new ArgumentNullException(parameter);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x000B160A File Offset: 0x000AF80A
		internal static ArgumentNullException ArgumentNull(string parameter, string error)
		{
			ArgumentNullException ex = new ArgumentNullException(parameter, error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x000B1619 File Offset: 0x000AF819
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000B1627 File Offset: 0x000AF827
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName, message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000B1636 File Offset: 0x000AF836
		internal static IndexOutOfRangeException IndexOutOfRange(string error)
		{
			IndexOutOfRangeException ex = new IndexOutOfRangeException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000B1644 File Offset: 0x000AF844
		internal static InvalidCastException InvalidCast(string error)
		{
			return ADP.InvalidCast(error, null);
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000B164D File Offset: 0x000AF84D
		internal static InvalidCastException InvalidCast(string error, Exception inner)
		{
			InvalidCastException ex = new InvalidCastException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000B165C File Offset: 0x000AF85C
		internal static InvalidOperationException InvalidOperation(string error)
		{
			InvalidOperationException ex = new InvalidOperationException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000B166A File Offset: 0x000AF86A
		internal static NotSupportedException NotSupported()
		{
			NotSupportedException ex = new NotSupportedException();
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000B1677 File Offset: 0x000AF877
		internal static NotSupportedException NotSupported(string error)
		{
			NotSupportedException ex = new NotSupportedException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000B1688 File Offset: 0x000AF888
		internal static bool RemoveStringQuotes(string quotePrefix, string quoteSuffix, string quotedString, out string unquotedString)
		{
			int num = (quotePrefix != null) ? quotePrefix.Length : 0;
			int num2 = (quoteSuffix != null) ? quoteSuffix.Length : 0;
			if (num2 + num == 0)
			{
				unquotedString = quotedString;
				return true;
			}
			if (quotedString == null)
			{
				unquotedString = quotedString;
				return false;
			}
			int length = quotedString.Length;
			if (length < num + num2)
			{
				unquotedString = quotedString;
				return false;
			}
			if (num > 0 && !quotedString.StartsWith(quotePrefix, StringComparison.Ordinal))
			{
				unquotedString = quotedString;
				return false;
			}
			if (num2 > 0)
			{
				if (!quotedString.EndsWith(quoteSuffix, StringComparison.Ordinal))
				{
					unquotedString = quotedString;
					return false;
				}
				unquotedString = quotedString.Substring(num, length - (num + num2)).Replace(quoteSuffix + quoteSuffix, quoteSuffix);
			}
			else
			{
				unquotedString = quotedString.Substring(num, length - num);
			}
			return true;
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000B1723 File Offset: 0x000AF923
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, string value, string method)
		{
			return ADP.ArgumentOutOfRange(SR.Format("The {0} enumeration value, {1}, is not supported by the {2} method.", type.Name, value, method), type.Name);
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000B1742 File Offset: 0x000AF942
		internal static InvalidOperationException DataAdapter(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x000B1742 File Offset: 0x000AF942
		private static InvalidOperationException Provider(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000B174A File Offset: 0x000AF94A
		internal static ArgumentException InvalidMultipartName(string property, string value)
		{
			ArgumentException ex = new ArgumentException(SR.Format("{0} \"{1}\".", property, value));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000B1763 File Offset: 0x000AF963
		internal static ArgumentException InvalidMultipartNameIncorrectUsageOfQuotes(string property, string value)
		{
			ArgumentException ex = new ArgumentException(SR.Format("{0} \"{1}\", incorrect usage of quotes.", property, value));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000B177C File Offset: 0x000AF97C
		internal static ArgumentException InvalidMultipartNameToManyParts(string property, string value, int limit)
		{
			ArgumentException ex = new ArgumentException(SR.Format("{0} \"{1}\", the current limit of \"{2}\" is insufficient.", property, value, limit));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x000B179B File Offset: 0x000AF99B
		internal static void CheckArgumentNull(object value, string parameterName)
		{
			if (value == null)
			{
				throw ADP.ArgumentNull(parameterName);
			}
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x000B17A8 File Offset: 0x000AF9A8
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ADP.s_stackOverflowType && type != ADP.s_outOfMemoryType && type != ADP.s_threadAbortType && type != ADP.s_nullReferenceType && type != ADP.s_accessViolationType && !ADP.s_securityType.IsAssignableFrom(type);
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x000B1810 File Offset: 0x000AFA10
		internal static bool IsCatchableOrSecurityExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ADP.s_stackOverflowType && type != ADP.s_outOfMemoryType && type != ADP.s_threadAbortType && type != ADP.s_nullReferenceType && type != ADP.s_accessViolationType;
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000B1865 File Offset: 0x000AFA65
		internal static ArgumentOutOfRangeException InvalidEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(SR.Format("The {0} enumeration value, {1}, is invalid.", type.Name, value.ToString(CultureInfo.InvariantCulture)), type.Name);
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000B188E File Offset: 0x000AFA8E
		internal static ArgumentException ConnectionStringSyntax(int index)
		{
			return ADP.Argument(SR.Format("Format of the initialization string does not conform to specification starting at index {0}.", index));
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000B18A5 File Offset: 0x000AFAA5
		internal static ArgumentException KeywordNotSupported(string keyword)
		{
			return ADP.Argument(SR.Format("Keyword not supported: '{0}'.", keyword));
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000B18B7 File Offset: 0x000AFAB7
		internal static ArgumentException ConvertFailed(Type fromType, Type toType, Exception innerException)
		{
			return ADP.Argument(SR.Format(" Cannot convert object of type '{0}' to object of type '{1}'.", fromType.FullName, toType.FullName), innerException);
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000B18D5 File Offset: 0x000AFAD5
		internal static Exception InvalidConnectionOptionValue(string key)
		{
			return ADP.InvalidConnectionOptionValue(key, null);
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000B18DE File Offset: 0x000AFADE
		internal static Exception InvalidConnectionOptionValue(string key, Exception inner)
		{
			return ADP.Argument(SR.Format("Invalid value for key '{0}'.", key), inner);
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000B18F1 File Offset: 0x000AFAF1
		internal static ArgumentException CollectionRemoveInvalidObject(Type itemType, ICollection collection)
		{
			return ADP.Argument(SR.Format("Attempted to remove an {0} that is not contained by this {1}.", itemType.Name, collection.GetType().Name));
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000B1913 File Offset: 0x000AFB13
		internal static ArgumentNullException CollectionNullValue(string parameter, Type collection, Type itemType)
		{
			return ADP.ArgumentNull(parameter, SR.Format("The {0} only accepts non-null {1} type objects.", collection.Name, itemType.Name));
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000B1931 File Offset: 0x000AFB31
		internal static IndexOutOfRangeException CollectionIndexInt32(int index, Type collection, int count)
		{
			return ADP.IndexOutOfRange(SR.Format("Invalid index {0} for this {1} with Count={2}.", index.ToString(CultureInfo.InvariantCulture), collection.Name, count.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000B1960 File Offset: 0x000AFB60
		internal static IndexOutOfRangeException CollectionIndexString(Type itemType, string propertyName, string propertyValue, Type collection)
		{
			return ADP.IndexOutOfRange(SR.Format("An {0} with {1} '{2}' is not contained by this {3}.", new object[]
			{
				itemType.Name,
				propertyName,
				propertyValue,
				collection.Name
			}));
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000B1991 File Offset: 0x000AFB91
		internal static InvalidCastException CollectionInvalidType(Type collection, Type itemType, object invalidValue)
		{
			return ADP.InvalidCast(SR.Format("The {0} only accepts non-null {1} type objects, not {2} objects.", collection.Name, itemType.Name, invalidValue.GetType().Name));
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000B19BC File Offset: 0x000AFBBC
		private static string ConnectionStateMsg(ConnectionState state)
		{
			switch (state)
			{
			case ConnectionState.Closed:
				break;
			case ConnectionState.Open:
				return "The connection's current state is open.";
			case ConnectionState.Connecting:
				return "The connection's current state is connecting.";
			case ConnectionState.Open | ConnectionState.Connecting:
			case ConnectionState.Executing:
				goto IL_46;
			case ConnectionState.Open | ConnectionState.Executing:
				return "The connection's current state is executing.";
			default:
				if (state == (ConnectionState.Open | ConnectionState.Fetching))
				{
					return "The connection's current state is fetching.";
				}
				if (state != (ConnectionState.Connecting | ConnectionState.Broken))
				{
					goto IL_46;
				}
				break;
			}
			return "The connection's current state is closed.";
			IL_46:
			return SR.Format("The connection's current state: {0}.", state.ToString());
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000B1A26 File Offset: 0x000AFC26
		internal static Exception StreamClosed([CallerMemberName] string method = "")
		{
			return ADP.InvalidOperation(SR.Format("Invalid attempt to {0} when stream is closed.", method));
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x000B1A38 File Offset: 0x000AFC38
		internal static string BuildQuotedString(string quotePrefix, string quoteSuffix, string unQuotedString)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(quotePrefix))
			{
				stringBuilder.Append(quotePrefix);
			}
			if (!string.IsNullOrEmpty(quoteSuffix))
			{
				stringBuilder.Append(unQuotedString.Replace(quoteSuffix, quoteSuffix + quoteSuffix));
				stringBuilder.Append(quoteSuffix);
			}
			else
			{
				stringBuilder.Append(unQuotedString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x000B1A90 File Offset: 0x000AFC90
		internal static ArgumentException ParametersIsNotParent(Type parameterType, ICollection collection)
		{
			return ADP.Argument(SR.Format("The {0} is already contained by another {1}.", parameterType.Name, collection.GetType().Name));
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x000B1A90 File Offset: 0x000AFC90
		internal static ArgumentException ParametersIsParent(Type parameterType, ICollection collection)
		{
			return ADP.Argument(SR.Format("The {0} is already contained by another {1}.", parameterType.Name, collection.GetType().Name));
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x000B1AB2 File Offset: 0x000AFCB2
		internal static Exception InternalError(ADP.InternalErrorCode internalError)
		{
			return ADP.InvalidOperation(SR.Format("Internal .Net Framework Data Provider error {0}.", (int)internalError));
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000B1AC9 File Offset: 0x000AFCC9
		internal static Exception DataReaderClosed([CallerMemberName] string method = "")
		{
			return ADP.InvalidOperation(SR.Format("Invalid attempt to call {0} when reader is closed.", method));
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000B1ADB File Offset: 0x000AFCDB
		internal static ArgumentOutOfRangeException InvalidSourceBufferIndex(int maxLen, long srcOffset, string parameterName)
		{
			return ADP.ArgumentOutOfRange(SR.Format("Invalid source buffer (size of {0}) offset: {1}", maxLen.ToString(CultureInfo.InvariantCulture), srcOffset.ToString(CultureInfo.InvariantCulture)), parameterName);
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000B1B05 File Offset: 0x000AFD05
		internal static ArgumentOutOfRangeException InvalidDestinationBufferIndex(int maxLen, int dstOffset, string parameterName)
		{
			return ADP.ArgumentOutOfRange(SR.Format("Invalid destination buffer (size of {0}) offset: {1}", maxLen.ToString(CultureInfo.InvariantCulture), dstOffset.ToString(CultureInfo.InvariantCulture)), parameterName);
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000B1B2F File Offset: 0x000AFD2F
		internal static IndexOutOfRangeException InvalidBufferSizeOrIndex(int numBytes, int bufferIndex)
		{
			return ADP.IndexOutOfRange(SR.Format("Buffer offset '{1}' plus the bytes available '{0}' is greater than the length of the passed in buffer.", numBytes.ToString(CultureInfo.InvariantCulture), bufferIndex.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000B1B58 File Offset: 0x000AFD58
		internal static Exception InvalidDataLength(long length)
		{
			return ADP.IndexOutOfRange(SR.Format("Data length '{0}' is less than 0.", length.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000B1B75 File Offset: 0x000AFD75
		internal static bool CompareInsensitiveInvariant(string strvalue, string strconst)
		{
			return CultureInfo.InvariantCulture.CompareInfo.Compare(strvalue, strconst, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000B1B8C File Offset: 0x000AFD8C
		internal static int DstCompare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x000B1BA1 File Offset: 0x000AFDA1
		internal static bool IsEmptyArray(string[] array)
		{
			return array == null || array.Length == 0;
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x000B1BB0 File Offset: 0x000AFDB0
		internal static bool IsNull(object value)
		{
			if (value == null || DBNull.Value == value)
			{
				return true;
			}
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000B1BDC File Offset: 0x000AFDDC
		internal static Exception InvalidSeekOrigin(string parameterName)
		{
			return ADP.ArgumentOutOfRange("Specified SeekOrigin value is invalid.", parameterName);
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000B1BE9 File Offset: 0x000AFDE9
		internal static void SetCurrentTransaction(Transaction transaction)
		{
			Transaction.Current = transaction;
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000B1BF1 File Offset: 0x000AFDF1
		internal static Task<T> CreatedTaskWithCancellation<T>()
		{
			return Task.FromCanceled<T>(new CancellationToken(true));
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000B1BFE File Offset: 0x000AFDFE
		internal static void TraceExceptionForCapture(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|CATCH> '{0}'", e);
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000B1C0B File Offset: 0x000AFE0B
		internal static DataException Data(string message)
		{
			DataException ex = new DataException(message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000B1C19 File Offset: 0x000AFE19
		internal static void CheckArgumentLength(string value, string parameterName)
		{
			ADP.CheckArgumentNull(value, parameterName);
			if (value.Length == 0)
			{
				throw ADP.Argument(SR.Format("Expecting non-empty string for '{0}' parameter.", parameterName));
			}
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000B1C3B File Offset: 0x000AFE3B
		internal static void CheckArgumentLength(Array value, string parameterName)
		{
			ADP.CheckArgumentNull(value, parameterName);
			if (value.Length == 0)
			{
				throw ADP.Argument(SR.Format("Expecting non-empty array for '{0}' parameter.", parameterName));
			}
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000B1C5D File Offset: 0x000AFE5D
		internal static ArgumentOutOfRangeException InvalidAcceptRejectRule(AcceptRejectRule value)
		{
			return ADP.InvalidEnumerationValue(typeof(AcceptRejectRule), (int)value);
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000B1C6F File Offset: 0x000AFE6F
		internal static ArgumentOutOfRangeException InvalidCatalogLocation(CatalogLocation value)
		{
			return ADP.InvalidEnumerationValue(typeof(CatalogLocation), (int)value);
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x000B1C81 File Offset: 0x000AFE81
		internal static ArgumentOutOfRangeException InvalidConflictOptions(ConflictOption value)
		{
			return ADP.InvalidEnumerationValue(typeof(ConflictOption), (int)value);
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x000B1C93 File Offset: 0x000AFE93
		internal static ArgumentOutOfRangeException InvalidDataRowState(DataRowState value)
		{
			return ADP.InvalidEnumerationValue(typeof(DataRowState), (int)value);
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x000B1CA5 File Offset: 0x000AFEA5
		internal static ArgumentOutOfRangeException InvalidKeyRestrictionBehavior(KeyRestrictionBehavior value)
		{
			return ADP.InvalidEnumerationValue(typeof(KeyRestrictionBehavior), (int)value);
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000B1CB7 File Offset: 0x000AFEB7
		internal static ArgumentOutOfRangeException InvalidLoadOption(LoadOption value)
		{
			return ADP.InvalidEnumerationValue(typeof(LoadOption), (int)value);
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x000B1CC9 File Offset: 0x000AFEC9
		internal static ArgumentOutOfRangeException InvalidMissingMappingAction(MissingMappingAction value)
		{
			return ADP.InvalidEnumerationValue(typeof(MissingMappingAction), (int)value);
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000B1CDB File Offset: 0x000AFEDB
		internal static ArgumentOutOfRangeException InvalidMissingSchemaAction(MissingSchemaAction value)
		{
			return ADP.InvalidEnumerationValue(typeof(MissingSchemaAction), (int)value);
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000B1CED File Offset: 0x000AFEED
		internal static ArgumentOutOfRangeException InvalidRule(Rule value)
		{
			return ADP.InvalidEnumerationValue(typeof(Rule), (int)value);
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000B1CFF File Offset: 0x000AFEFF
		internal static ArgumentOutOfRangeException InvalidSchemaType(SchemaType value)
		{
			return ADP.InvalidEnumerationValue(typeof(SchemaType), (int)value);
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000B1D11 File Offset: 0x000AFF11
		internal static ArgumentOutOfRangeException InvalidStatementType(StatementType value)
		{
			return ADP.InvalidEnumerationValue(typeof(StatementType), (int)value);
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000B1D23 File Offset: 0x000AFF23
		internal static ArgumentOutOfRangeException InvalidUpdateStatus(UpdateStatus value)
		{
			return ADP.InvalidEnumerationValue(typeof(UpdateStatus), (int)value);
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000B1D35 File Offset: 0x000AFF35
		internal static ArgumentOutOfRangeException NotSupportedStatementType(StatementType value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(StatementType), value.ToString(), method);
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000B1D54 File Offset: 0x000AFF54
		internal static ArgumentException InvalidKeyname(string parameterName)
		{
			return ADP.Argument("Invalid keyword, contain one or more of 'no characters', 'control characters', 'leading or trailing whitespace' or 'leading semicolons'.", parameterName);
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x000B1D61 File Offset: 0x000AFF61
		internal static ArgumentException InvalidValue(string parameterName)
		{
			return ADP.Argument("The value contains embedded nulls (\\\\u0000).", parameterName);
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000B1D6E File Offset: 0x000AFF6E
		internal static Exception WrongType(Type got, Type expected)
		{
			return ADP.Argument(SR.Format("Expecting argument of type {1}, but received type {0}.", got.ToString(), expected.ToString()));
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000B1D8B File Offset: 0x000AFF8B
		internal static Exception CollectionUniqueValue(Type itemType, string propertyName, string propertyValue)
		{
			return ADP.Argument(SR.Format("The {0}.{1} is required to be unique, '{2}' already exists in the collection.", itemType.Name, propertyName, propertyValue));
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000B1DA4 File Offset: 0x000AFFA4
		internal static InvalidOperationException MissingSelectCommand(string method)
		{
			return ADP.Provider(SR.Format("The SelectCommand property has not been initialized before calling '{0}'.", method));
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000B1742 File Offset: 0x000AF942
		private static InvalidOperationException DataMapping(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000B1DB6 File Offset: 0x000AFFB6
		internal static InvalidOperationException ColumnSchemaExpression(string srcColumn, string cacheColumn)
		{
			return ADP.DataMapping(SR.Format("The column mapping from SourceColumn '{0}' failed because the DataColumn '{1}' is a computed column.", srcColumn, cacheColumn));
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000B1DC9 File Offset: 0x000AFFC9
		internal static InvalidOperationException ColumnSchemaMismatch(string srcColumn, Type srcType, DataColumn column)
		{
			return ADP.DataMapping(SR.Format("Inconvertible type mismatch between SourceColumn '{0}' of {1} and the DataColumn '{2}' of {3}.", new object[]
			{
				srcColumn,
				srcType.Name,
				column.ColumnName,
				column.DataType.Name
			}));
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x000B1E04 File Offset: 0x000B0004
		internal static InvalidOperationException ColumnSchemaMissing(string cacheColumn, string tableName, string srcColumn)
		{
			if (string.IsNullOrEmpty(tableName))
			{
				return ADP.InvalidOperation(SR.Format("Missing the DataColumn '{0}' for the SourceColumn '{2}'.", cacheColumn, tableName, srcColumn));
			}
			return ADP.DataMapping(SR.Format("Missing the DataColumn '{0}' in the DataTable '{1}' for the SourceColumn '{2}'.", cacheColumn, tableName, srcColumn));
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x000B1E33 File Offset: 0x000B0033
		internal static InvalidOperationException MissingColumnMapping(string srcColumn)
		{
			return ADP.DataMapping(SR.Format("Missing SourceColumn mapping for '{0}'.", srcColumn));
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x000B1E45 File Offset: 0x000B0045
		internal static InvalidOperationException MissingTableSchema(string cacheTable, string srcTable)
		{
			return ADP.DataMapping(SR.Format("Missing the '{0}' DataTable for the '{1}' SourceTable.", cacheTable, srcTable));
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x000B1E58 File Offset: 0x000B0058
		internal static InvalidOperationException MissingTableMapping(string srcTable)
		{
			return ADP.DataMapping(SR.Format("Missing SourceTable mapping: '{0}'", srcTable));
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000B1E6A File Offset: 0x000B006A
		internal static InvalidOperationException MissingTableMappingDestination(string dstTable)
		{
			return ADP.DataMapping(SR.Format("Missing TableMapping when TableMapping.DataSetTable='{0}'.", dstTable));
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000B1E7C File Offset: 0x000B007C
		internal static Exception InvalidSourceColumn(string parameter)
		{
			return ADP.Argument("SourceColumn is required to be a non-empty string.", parameter);
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000B1E89 File Offset: 0x000B0089
		internal static Exception ColumnsAddNullAttempt(string parameter)
		{
			return ADP.CollectionNullValue(parameter, typeof(DataColumnMappingCollection), typeof(DataColumnMapping));
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x000B1EA5 File Offset: 0x000B00A5
		internal static Exception ColumnsDataSetColumn(string cacheColumn)
		{
			return ADP.CollectionIndexString(typeof(DataColumnMapping), "DataSetColumn", cacheColumn, typeof(DataColumnMappingCollection));
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x000B1EC6 File Offset: 0x000B00C6
		internal static Exception ColumnsIndexInt32(int index, IColumnMappingCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x000B1EDA File Offset: 0x000B00DA
		internal static Exception ColumnsIndexSource(string srcColumn)
		{
			return ADP.CollectionIndexString(typeof(DataColumnMapping), "SourceColumn", srcColumn, typeof(DataColumnMappingCollection));
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x000B1EFB File Offset: 0x000B00FB
		internal static Exception ColumnsIsNotParent(ICollection collection)
		{
			return ADP.ParametersIsNotParent(typeof(DataColumnMapping), collection);
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x000B1F0D File Offset: 0x000B010D
		internal static Exception ColumnsIsParent(ICollection collection)
		{
			return ADP.ParametersIsParent(typeof(DataColumnMapping), collection);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000B1F1F File Offset: 0x000B011F
		internal static Exception ColumnsUniqueSourceColumn(string srcColumn)
		{
			return ADP.CollectionUniqueValue(typeof(DataColumnMapping), "SourceColumn", srcColumn);
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x000B1F36 File Offset: 0x000B0136
		internal static Exception NotADataColumnMapping(object value)
		{
			return ADP.CollectionInvalidType(typeof(DataColumnMappingCollection), typeof(DataColumnMapping), value);
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x000B1F52 File Offset: 0x000B0152
		internal static Exception InvalidSourceTable(string parameter)
		{
			return ADP.Argument("SourceTable is required to be a non-empty string", parameter);
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x000B1F5F File Offset: 0x000B015F
		internal static Exception TablesAddNullAttempt(string parameter)
		{
			return ADP.CollectionNullValue(parameter, typeof(DataTableMappingCollection), typeof(DataTableMapping));
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x000B1F7B File Offset: 0x000B017B
		internal static Exception TablesDataSetTable(string cacheTable)
		{
			return ADP.CollectionIndexString(typeof(DataTableMapping), "DataSetTable", cacheTable, typeof(DataTableMappingCollection));
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x000B1EC6 File Offset: 0x000B00C6
		internal static Exception TablesIndexInt32(int index, ITableMappingCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000B1F9C File Offset: 0x000B019C
		internal static Exception TablesIsNotParent(ICollection collection)
		{
			return ADP.ParametersIsNotParent(typeof(DataTableMapping), collection);
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000B1FAE File Offset: 0x000B01AE
		internal static Exception TablesIsParent(ICollection collection)
		{
			return ADP.ParametersIsParent(typeof(DataTableMapping), collection);
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x000B1FC0 File Offset: 0x000B01C0
		internal static Exception TablesSourceIndex(string srcTable)
		{
			return ADP.CollectionIndexString(typeof(DataTableMapping), "SourceTable", srcTable, typeof(DataTableMappingCollection));
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x000B1FE1 File Offset: 0x000B01E1
		internal static Exception TablesUniqueSourceTable(string srcTable)
		{
			return ADP.CollectionUniqueValue(typeof(DataTableMapping), "SourceTable", srcTable);
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000B1FF8 File Offset: 0x000B01F8
		internal static Exception NotADataTableMapping(object value)
		{
			return ADP.CollectionInvalidType(typeof(DataTableMappingCollection), typeof(DataTableMapping), value);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x000B2014 File Offset: 0x000B0214
		internal static InvalidOperationException UpdateConnectionRequired(StatementType statementType, bool isRowUpdatingCommand)
		{
			string error;
			if (!isRowUpdatingCommand)
			{
				switch (statementType)
				{
				case StatementType.Insert:
					error = "Update requires the InsertCommand to have a connection object. The Connection property of the InsertCommand has not been initialized.";
					goto IL_4A;
				case StatementType.Update:
					error = "Update requires the UpdateCommand to have a connection object. The Connection property of the UpdateCommand has not been initialized.";
					goto IL_4A;
				case StatementType.Delete:
					error = "Update requires the DeleteCommand to have a connection object. The Connection property of the DeleteCommand has not been initialized.";
					goto IL_4A;
				}
				throw ADP.InvalidStatementType(statementType);
			}
			error = "Update requires the command clone to have a connection object. The Connection property of the command clone has not been initialized.";
			IL_4A:
			return ADP.InvalidOperation(error);
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000B2071 File Offset: 0x000B0271
		internal static InvalidOperationException ConnectionRequired_Res(string method)
		{
			return ADP.InvalidOperation("ADP_ConnectionRequired_" + method);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x000B2084 File Offset: 0x000B0284
		internal static InvalidOperationException UpdateOpenConnectionRequired(StatementType statementType, bool isRowUpdatingCommand, ConnectionState state)
		{
			string resourceFormat;
			if (isRowUpdatingCommand)
			{
				resourceFormat = "Update requires the updating command to have an open connection object. {1}";
			}
			else
			{
				switch (statementType)
				{
				case StatementType.Insert:
					resourceFormat = "Update requires the {0}Command to have an open connection object. {1}";
					break;
				case StatementType.Update:
					resourceFormat = "Update requires the {0}Command to have an open connection object. {1}";
					break;
				case StatementType.Delete:
					resourceFormat = "Update requires the {0}Command to have an open connection object. {1}";
					break;
				default:
					throw ADP.InvalidStatementType(statementType);
				}
			}
			return ADP.InvalidOperation(SR.Format(resourceFormat, ADP.ConnectionStateMsg(state)));
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000B20E2 File Offset: 0x000B02E2
		internal static ArgumentException UnwantedStatementType(StatementType statementType)
		{
			return ADP.Argument(SR.Format("The StatementType {0} is not expected here.", statementType.ToString()));
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000B2100 File Offset: 0x000B0300
		internal static Exception FillSchemaRequiresSourceTableName(string parameter)
		{
			return ADP.Argument("FillSchema: expected a non-empty string for the SourceTable name.", parameter);
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000B210D File Offset: 0x000B030D
		internal static Exception InvalidMaxRecords(string parameter, int max)
		{
			return ADP.Argument(SR.Format("The MaxRecords value of {0} is invalid; the value must be >= 0.", max.ToString(CultureInfo.InvariantCulture)), parameter);
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000B212B File Offset: 0x000B032B
		internal static Exception InvalidStartRecord(string parameter, int start)
		{
			return ADP.Argument(SR.Format("The StartRecord value of {0} is invalid; the value must be >= 0.", start.ToString(CultureInfo.InvariantCulture)), parameter);
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000B2149 File Offset: 0x000B0349
		internal static Exception FillRequires(string parameter)
		{
			return ADP.ArgumentNull(parameter);
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000B2151 File Offset: 0x000B0351
		internal static Exception FillRequiresSourceTableName(string parameter)
		{
			return ADP.Argument("Fill: expected a non-empty string for the SourceTable name.", parameter);
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000B215E File Offset: 0x000B035E
		internal static Exception FillChapterAutoIncrement()
		{
			return ADP.InvalidOperation("Hierarchical chapter columns must map to an AutoIncrement DataColumn.");
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000B216A File Offset: 0x000B036A
		internal static InvalidOperationException MissingDataReaderFieldType(int index)
		{
			return ADP.DataAdapter(SR.Format("DataReader.GetFieldType({0}) returned null.", index));
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000B2181 File Offset: 0x000B0381
		internal static InvalidOperationException OnlyOneTableForStartRecordOrMaxRecords()
		{
			return ADP.DataAdapter("Only specify one item in the dataTables array when using non-zero values for startRecords or maxRecords.");
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000B2149 File Offset: 0x000B0349
		internal static ArgumentNullException UpdateRequiresNonNullDataSet(string parameter)
		{
			return ADP.ArgumentNull(parameter);
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x000B218D File Offset: 0x000B038D
		internal static InvalidOperationException UpdateRequiresSourceTable(string defaultSrcTableName)
		{
			return ADP.InvalidOperation(SR.Format("Update unable to find TableMapping['{0}'] or DataTable '{0}'.", defaultSrcTableName));
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x000B219F File Offset: 0x000B039F
		internal static InvalidOperationException UpdateRequiresSourceTableName(string srcTable)
		{
			return ADP.InvalidOperation(SR.Format("Update: expected a non-empty SourceTable name.", srcTable));
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000B2149 File Offset: 0x000B0349
		internal static ArgumentNullException UpdateRequiresDataTable(string parameter)
		{
			return ADP.ArgumentNull(parameter);
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000B21B4 File Offset: 0x000B03B4
		internal static Exception UpdateConcurrencyViolation(StatementType statementType, int affected, int expected, DataRow[] dataRows)
		{
			string resourceFormat;
			switch (statementType)
			{
			case StatementType.Update:
				resourceFormat = "Concurrency violation: the UpdateCommand affected {0} of the expected {1} records.";
				break;
			case StatementType.Delete:
				resourceFormat = "Concurrency violation: the DeleteCommand affected {0} of the expected {1} records.";
				break;
			case StatementType.Batch:
				resourceFormat = "Concurrency violation: the batched command affected {0} of the expected {1} records.";
				break;
			default:
				throw ADP.InvalidStatementType(statementType);
			}
			DBConcurrencyException ex = new DBConcurrencyException(SR.Format(resourceFormat, affected.ToString(CultureInfo.InvariantCulture), expected.ToString(CultureInfo.InvariantCulture)), null, dataRows);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000B2224 File Offset: 0x000B0424
		internal static InvalidOperationException UpdateRequiresCommand(StatementType statementType, bool isRowUpdatingCommand)
		{
			string error;
			if (isRowUpdatingCommand)
			{
				error = "Update requires the command clone to be valid.";
			}
			else
			{
				switch (statementType)
				{
				case StatementType.Select:
					error = "Auto SQL generation during Update requires a valid SelectCommand.";
					break;
				case StatementType.Insert:
					error = "Update requires a valid InsertCommand when passed DataRow collection with new rows.";
					break;
				case StatementType.Update:
					error = "Update requires a valid UpdateCommand when passed DataRow collection with modified rows.";
					break;
				case StatementType.Delete:
					error = "Update requires a valid DeleteCommand when passed DataRow collection with deleted rows.";
					break;
				default:
					throw ADP.InvalidStatementType(statementType);
				}
			}
			return ADP.InvalidOperation(error);
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000B2281 File Offset: 0x000B0481
		internal static ArgumentException UpdateMismatchRowTable(int i)
		{
			return ADP.Argument(SR.Format("DataRow[{0}] is from a different DataTable than DataRow[0].", i.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000B229E File Offset: 0x000B049E
		internal static DataException RowUpdatedErrors()
		{
			return ADP.Data("RowUpdatedEvent: Errors occurred; no additional is information available.");
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000B22AA File Offset: 0x000B04AA
		internal static DataException RowUpdatingErrors()
		{
			return ADP.Data("RowUpdatingEvent: Errors occurred; no additional is information available.");
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000B22B6 File Offset: 0x000B04B6
		internal static InvalidOperationException ResultsNotAllowedDuringBatch()
		{
			return ADP.DataAdapter("When batching, the command's UpdatedRowSource property value of UpdateRowSource.FirstReturnedRecord or UpdateRowSource.Both is invalid.");
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000B22C2 File Offset: 0x000B04C2
		internal static InvalidOperationException DynamicSQLJoinUnsupported()
		{
			return ADP.InvalidOperation("Dynamic SQL generation is not supported against multiple base tables.");
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000B22CE File Offset: 0x000B04CE
		internal static InvalidOperationException DynamicSQLNoTableInfo()
		{
			return ADP.InvalidOperation("Dynamic SQL generation is not supported against a SelectCommand that does not return any base table information.");
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x000B22DA File Offset: 0x000B04DA
		internal static InvalidOperationException DynamicSQLNoKeyInfoDelete()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the DeleteCommand is not supported against a SelectCommand that does not return any key column information.");
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000B22E6 File Offset: 0x000B04E6
		internal static InvalidOperationException DynamicSQLNoKeyInfoUpdate()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the UpdateCommand is not supported against a SelectCommand that does not return any key column information.");
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000B22F2 File Offset: 0x000B04F2
		internal static InvalidOperationException DynamicSQLNoKeyInfoRowVersionDelete()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the DeleteCommand is not supported against a SelectCommand that does not contain a row version column.");
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000B22FE File Offset: 0x000B04FE
		internal static InvalidOperationException DynamicSQLNoKeyInfoRowVersionUpdate()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the UpdateCommand is not supported against a SelectCommand that does not contain a row version column.");
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000B230A File Offset: 0x000B050A
		internal static InvalidOperationException DynamicSQLNestedQuote(string name, string quote)
		{
			return ADP.InvalidOperation(SR.Format("Dynamic SQL generation not supported against table names '{0}' that contain the QuotePrefix or QuoteSuffix character '{1}'.", name, quote));
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000B231D File Offset: 0x000B051D
		internal static InvalidOperationException NoQuoteChange()
		{
			return ADP.InvalidOperation("The QuotePrefix and QuoteSuffix properties cannot be changed once an Insert, Update, or Delete command has been generated.");
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000B2329 File Offset: 0x000B0529
		internal static InvalidOperationException MissingSourceCommand()
		{
			return ADP.InvalidOperation("The DataAdapter.SelectCommand property needs to be initialized.");
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x000B2335 File Offset: 0x000B0535
		internal static InvalidOperationException MissingSourceCommandConnection()
		{
			return ADP.InvalidOperation("The DataAdapter.SelectCommand.Connection property needs to be initialized;");
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000B2344 File Offset: 0x000B0544
		internal static DataRow[] SelectAdapterRows(DataTable dataTable, bool sorted)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			DataRowCollection rows = dataTable.Rows;
			foreach (object obj in rows)
			{
				DataRowState rowState = ((DataRow)obj).RowState;
				if (rowState != DataRowState.Added)
				{
					if (rowState != DataRowState.Deleted)
					{
						if (rowState == DataRowState.Modified)
						{
							num3++;
						}
					}
					else
					{
						num2++;
					}
				}
				else
				{
					num++;
				}
			}
			DataRow[] array = new DataRow[num + num2 + num3];
			if (sorted)
			{
				num3 = num + num2;
				num2 = num;
				num = 0;
				using (IEnumerator enumerator = rows.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						DataRow dataRow = (DataRow)obj2;
						DataRowState rowState = dataRow.RowState;
						if (rowState != DataRowState.Added)
						{
							if (rowState != DataRowState.Deleted)
							{
								if (rowState == DataRowState.Modified)
								{
									array[num3++] = dataRow;
								}
							}
							else
							{
								array[num2++] = dataRow;
							}
						}
						else
						{
							array[num++] = dataRow;
						}
					}
					return array;
				}
			}
			int num4 = 0;
			foreach (object obj3 in rows)
			{
				DataRow dataRow2 = (DataRow)obj3;
				if ((dataRow2.RowState & (DataRowState.Added | DataRowState.Deleted | DataRowState.Modified)) != (DataRowState)0)
				{
					array[num4++] = dataRow2;
					if (num4 == array.Length)
					{
						break;
					}
				}
			}
			return array;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000B24D0 File Offset: 0x000B06D0
		internal static void BuildSchemaTableInfoTableNames(string[] columnNameArray)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(columnNameArray.Length);
			int num = columnNameArray.Length;
			int num2 = columnNameArray.Length - 1;
			while (0 <= num2)
			{
				string text = columnNameArray[num2];
				if (text != null && 0 < text.Length)
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					int val;
					if (dictionary.TryGetValue(text, out val))
					{
						num = Math.Min(num, val);
					}
					dictionary[text] = num2;
				}
				else
				{
					columnNameArray[num2] = string.Empty;
					num = num2;
				}
				num2--;
			}
			int uniqueIndex = 1;
			for (int i = num; i < columnNameArray.Length; i++)
			{
				string text2 = columnNameArray[i];
				if (text2.Length == 0)
				{
					columnNameArray[i] = "Column";
					uniqueIndex = ADP.GenerateUniqueName(dictionary, ref columnNameArray[i], i, uniqueIndex);
				}
				else
				{
					text2 = text2.ToLower(CultureInfo.InvariantCulture);
					if (i != dictionary[text2])
					{
						ADP.GenerateUniqueName(dictionary, ref columnNameArray[i], i, 1);
					}
				}
			}
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000B25B4 File Offset: 0x000B07B4
		private static int GenerateUniqueName(Dictionary<string, int> hash, ref string columnName, int index, int uniqueIndex)
		{
			string text;
			for (;;)
			{
				text = columnName + uniqueIndex.ToString(CultureInfo.InvariantCulture);
				string key = text.ToLower(CultureInfo.InvariantCulture);
				if (hash.TryAdd(key, index))
				{
					break;
				}
				uniqueIndex++;
			}
			columnName = text;
			return uniqueIndex;
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000B25F8 File Offset: 0x000B07F8
		internal static int SrcCompare(string strA, string strB)
		{
			if (!(strA == strB))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000B2608 File Offset: 0x000B0808
		internal static Exception ExceptionWithStackTrace(Exception e)
		{
			try
			{
				throw e;
			}
			catch (Exception result)
			{
			}
			Exception result;
			return result;
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000B262C File Offset: 0x000B082C
		internal static IndexOutOfRangeException IndexOutOfRange(int value)
		{
			return new IndexOutOfRangeException(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000B263F File Offset: 0x000B083F
		internal static IndexOutOfRangeException IndexOutOfRange()
		{
			return new IndexOutOfRangeException();
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000B2646 File Offset: 0x000B0846
		internal static TimeoutException TimeoutException(string error)
		{
			return new TimeoutException(error);
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000B264E File Offset: 0x000B084E
		internal static InvalidOperationException InvalidOperation(string error, Exception inner)
		{
			return new InvalidOperationException(error, inner);
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000B2657 File Offset: 0x000B0857
		internal static OverflowException Overflow(string error)
		{
			return ADP.Overflow(error, null);
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000B2660 File Offset: 0x000B0860
		internal static OverflowException Overflow(string error, Exception inner)
		{
			return new OverflowException(error, inner);
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000B2669 File Offset: 0x000B0869
		internal static TypeLoadException TypeLoad(string error)
		{
			TypeLoadException ex = new TypeLoadException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000B2677 File Offset: 0x000B0877
		internal static PlatformNotSupportedException DbTypeNotSupported(string dbType)
		{
			return new PlatformNotSupportedException(SR.GetString("Type {0} is not supported on this platform.", new object[]
			{
				dbType
			}));
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000B2692 File Offset: 0x000B0892
		internal static InvalidCastException InvalidCast()
		{
			return new InvalidCastException();
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000B2699 File Offset: 0x000B0899
		internal static IOException IO(string error)
		{
			return new IOException(error);
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x000B26A1 File Offset: 0x000B08A1
		internal static IOException IO(string error, Exception inner)
		{
			return new IOException(error, inner);
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000B26AA File Offset: 0x000B08AA
		internal static ObjectDisposedException ObjectDisposed(object instance)
		{
			return new ObjectDisposedException(instance.GetType().Name);
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000B26BC File Offset: 0x000B08BC
		internal static Exception DataTableDoesNotExist(string collectionName)
		{
			return ADP.Argument(SR.GetString("The collection '{0}' is missing from the metadata XML.", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000B26D7 File Offset: 0x000B08D7
		internal static InvalidOperationException MethodCalledTwice(string method)
		{
			return new InvalidOperationException(SR.GetString("The method '{0}' cannot be called more than once for the same execution.", new object[]
			{
				method
			}));
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000B26F2 File Offset: 0x000B08F2
		internal static ArgumentOutOfRangeException InvalidCommandType(CommandType value)
		{
			return ADP.InvalidEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x000B2704 File Offset: 0x000B0904
		internal static ArgumentOutOfRangeException InvalidIsolationLevel(IsolationLevel value)
		{
			return ADP.InvalidEnumerationValue(typeof(IsolationLevel), (int)value);
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000B2716 File Offset: 0x000B0916
		internal static ArgumentOutOfRangeException InvalidParameterDirection(ParameterDirection value)
		{
			return ADP.InvalidEnumerationValue(typeof(ParameterDirection), (int)value);
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000B2728 File Offset: 0x000B0928
		internal static Exception TooManyRestrictions(string collectionName)
		{
			return ADP.Argument(SR.GetString("More restrictions were provided than the requested schema ('{0}') supports.", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000B2743 File Offset: 0x000B0943
		internal static ArgumentOutOfRangeException InvalidUpdateRowSource(UpdateRowSource value)
		{
			return ADP.InvalidEnumerationValue(typeof(UpdateRowSource), (int)value);
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000B2755 File Offset: 0x000B0955
		internal static ArgumentException InvalidMinMaxPoolSizeValues()
		{
			return ADP.Argument(SR.GetString("Invalid min or max pool size values, min pool size cannot be greater than the max pool size."));
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x000B2766 File Offset: 0x000B0966
		internal static InvalidOperationException NoConnectionString()
		{
			return ADP.InvalidOperation(SR.GetString("The ConnectionString property has not been initialized."));
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000B2777 File Offset: 0x000B0977
		internal static Exception MethodNotImplemented([CallerMemberName] string methodName = "")
		{
			return NotImplemented.ByDesignWithMessage(methodName);
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000B277F File Offset: 0x000B097F
		internal static Exception QueryFailed(string collectionName, Exception e)
		{
			return ADP.InvalidOperation(SR.GetString("Unable to build the '{0}' collection because execution of the SQL query failed. See the inner exception for details.", new object[]
			{
				collectionName
			}), e);
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000B279B File Offset: 0x000B099B
		internal static Exception InvalidConnectionOptionValueLength(string key, int limit)
		{
			return ADP.Argument(SR.GetString("The value's length for key '{0}' exceeds it's limit of '{1}'.", new object[]
			{
				key,
				limit
			}));
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000B27BF File Offset: 0x000B09BF
		internal static Exception MissingConnectionOptionValue(string key, string requiredAdditionalKey)
		{
			return ADP.Argument(SR.GetString("Use of key '{0}' requires the key '{1}' to be present.", new object[]
			{
				key,
				requiredAdditionalKey
			}));
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000B27DE File Offset: 0x000B09DE
		internal static Exception PooledOpenTimeout()
		{
			return ADP.InvalidOperation(SR.GetString("Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached."));
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000B27EF File Offset: 0x000B09EF
		internal static Exception NonPooledOpenTimeout()
		{
			return ADP.TimeoutException(SR.GetString("Timeout attempting to open the connection.  The time period elapsed prior to attempting to open the connection has been exceeded.  This may have occurred because of too many simultaneous non-pooled connection attempts."));
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000B2800 File Offset: 0x000B0A00
		internal static InvalidOperationException TransactionConnectionMismatch()
		{
			return ADP.Provider(SR.GetString("The transaction is either not associated with the current connection or has been completed."));
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000B2811 File Offset: 0x000B0A11
		internal static InvalidOperationException TransactionRequired(string method)
		{
			return ADP.Provider(SR.GetString("{0} requires the command to have a transaction when the connection assigned to the command is in a pending local transaction.  The Transaction property of the command has not been initialized.", new object[]
			{
				method
			}));
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x000B282C File Offset: 0x000B0A2C
		internal static Exception CommandTextRequired(string method)
		{
			return ADP.InvalidOperation(SR.GetString("{0}: CommandText property has not been initialized", new object[]
			{
				method
			}));
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000B2847 File Offset: 0x000B0A47
		internal static Exception NoColumns()
		{
			return ADP.Argument(SR.GetString("The schema table contains no columns."));
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000B2858 File Offset: 0x000B0A58
		internal static InvalidOperationException ConnectionRequired(string method)
		{
			return ADP.InvalidOperation(SR.GetString("{0}: Connection property has not been initialized.", new object[]
			{
				method
			}));
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000B2873 File Offset: 0x000B0A73
		internal static InvalidOperationException OpenConnectionRequired(string method, ConnectionState state)
		{
			return ADP.InvalidOperation(SR.GetString("{0} requires an open and available Connection. {1}", new object[]
			{
				method,
				ADP.ConnectionStateMsg(state)
			}));
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000B2897 File Offset: 0x000B0A97
		internal static Exception OpenReaderExists()
		{
			return ADP.OpenReaderExists(null);
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000B289F File Offset: 0x000B0A9F
		internal static Exception OpenReaderExists(Exception e)
		{
			return ADP.InvalidOperation(SR.GetString("There is already an open DataReader associated with this Command which must be closed first."), e);
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000B28B1 File Offset: 0x000B0AB1
		internal static Exception NonSeqByteAccess(long badIndex, long currIndex, string method)
		{
			return ADP.InvalidOperation(SR.GetString("Invalid {2} attempt at dataIndex '{0}'.  With CommandBehavior.SequentialAccess, you may only read from dataIndex '{1}' or greater.", new object[]
			{
				badIndex.ToString(CultureInfo.InvariantCulture),
				currIndex.ToString(CultureInfo.InvariantCulture),
				method
			}));
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000B28EA File Offset: 0x000B0AEA
		internal static Exception InvalidXml()
		{
			return ADP.Argument(SR.GetString("The metadata XML is invalid."));
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x000B28FB File Offset: 0x000B0AFB
		internal static Exception NegativeParameter(string parameterName)
		{
			return ADP.InvalidOperation(SR.GetString("Invalid value for argument '{0}'. The value must be greater than or equal to 0.", new object[]
			{
				parameterName
			}));
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000B2916 File Offset: 0x000B0B16
		internal static Exception InvalidXmlMissingColumn(string collectionName, string columnName)
		{
			return ADP.Argument(SR.GetString("The metadata XML is invalid. The {0} collection must contain a {1} column and it must be a string column.", new object[]
			{
				collectionName,
				columnName
			}));
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000B2935 File Offset: 0x000B0B35
		internal static Exception InvalidMetaDataValue()
		{
			return ADP.Argument(SR.GetString("Invalid value for this metadata."));
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000B2946 File Offset: 0x000B0B46
		internal static InvalidOperationException NonSequentialColumnAccess(int badCol, int currCol)
		{
			return ADP.InvalidOperation(SR.GetString("Invalid attempt to read from column ordinal '{0}'.  With CommandBehavior.SequentialAccess, you may only read from column ordinal '{1}' or greater.", new object[]
			{
				badCol.ToString(CultureInfo.InvariantCulture),
				currCol.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x000B297B File Offset: 0x000B0B7B
		internal static Exception InvalidXmlInvalidValue(string collectionName, string columnName)
		{
			return ADP.Argument(SR.GetString("The metadata XML is invalid. The {1} column of the {0} collection must contain a non-empty string.", new object[]
			{
				collectionName,
				columnName
			}));
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000B299A File Offset: 0x000B0B9A
		internal static Exception CollectionNameIsNotUnique(string collectionName)
		{
			return ADP.Argument(SR.GetString("There are multiple collections named '{0}'.", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000B29B5 File Offset: 0x000B0BB5
		internal static Exception InvalidCommandTimeout(int value, [CallerMemberName] string property = "")
		{
			return ADP.Argument(SR.GetString("Invalid CommandTimeout value {0}; the value must be >= 0.", new object[]
			{
				value.ToString(CultureInfo.InvariantCulture)
			}), property);
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000B29DC File Offset: 0x000B0BDC
		internal static Exception UninitializedParameterSize(int index, Type dataType)
		{
			return ADP.InvalidOperation(SR.GetString("{1}[{0}]: the Size property has an invalid size of 0.", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				dataType.Name
			}));
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000B2A0B File Offset: 0x000B0C0B
		internal static Exception UnableToBuildCollection(string collectionName)
		{
			return ADP.Argument(SR.GetString("Unable to build schema collection '{0}';", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000B2A26 File Offset: 0x000B0C26
		internal static Exception PrepareParameterType(DbCommand cmd)
		{
			return ADP.InvalidOperation(SR.GetString("{0}.Prepare method requires all parameters to have an explicitly set type.", new object[]
			{
				cmd.GetType().Name
			}));
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000B2A4B File Offset: 0x000B0C4B
		internal static Exception UndefinedCollection(string collectionName)
		{
			return ADP.Argument(SR.GetString("The requested collection ({0}) is not defined.", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000B2A66 File Offset: 0x000B0C66
		internal static Exception UnsupportedVersion(string collectionName)
		{
			return ADP.Argument(SR.GetString(" requested collection ({0}) is not supported by this version of the provider.", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000B2A81 File Offset: 0x000B0C81
		internal static Exception AmbigousCollectionName(string collectionName)
		{
			return ADP.Argument(SR.GetString("The collection name '{0}' matches at least two collections with the same name but with different case, but does not match any of them exactly.", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000B2A9C File Offset: 0x000B0C9C
		internal static Exception PrepareParameterSize(DbCommand cmd)
		{
			return ADP.InvalidOperation(SR.GetString("{0}.Prepare method requires all variable length parameters to have an explicitly set non-zero Size.", new object[]
			{
				cmd.GetType().Name
			}));
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000B2AC1 File Offset: 0x000B0CC1
		internal static Exception PrepareParameterScale(DbCommand cmd, string type)
		{
			return ADP.InvalidOperation(SR.GetString("{0}.Prepare method requires parameters of type '{1}' have an explicitly set Precision and Scale.", new object[]
			{
				cmd.GetType().Name,
				type
			}));
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000B2AEA File Offset: 0x000B0CEA
		internal static Exception MissingDataSourceInformationColumn()
		{
			return ADP.Argument(SR.GetString("One of the required DataSourceInformation tables columns is missing."));
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000B2AFB File Offset: 0x000B0CFB
		internal static Exception IncorrectNumberOfDataSourceInformationRows()
		{
			return ADP.Argument(SR.GetString("The DataSourceInformation table must contain exactly one row."));
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000B2B0C File Offset: 0x000B0D0C
		internal static Exception MismatchedAsyncResult(string expectedMethod, string gotMethod)
		{
			return ADP.InvalidOperation(SR.GetString("Mismatched end method call for asyncResult.  Expected call to {0} but {1} was called instead.", new object[]
			{
				expectedMethod,
				gotMethod
			}));
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000B2B2B File Offset: 0x000B0D2B
		internal static Exception ClosedConnectionError()
		{
			return ADP.InvalidOperation(SR.GetString("Invalid operation. The connection is closed."));
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000B2B3C File Offset: 0x000B0D3C
		internal static Exception ConnectionAlreadyOpen(ConnectionState state)
		{
			return ADP.InvalidOperation(SR.GetString("The connection was not closed. {0}", new object[]
			{
				ADP.ConnectionStateMsg(state)
			}));
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000B2B5C File Offset: 0x000B0D5C
		internal static Exception TransactionPresent()
		{
			return ADP.InvalidOperation(SR.GetString("Connection currently has transaction enlisted.  Finish current transaction and retry."));
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000B2B6D File Offset: 0x000B0D6D
		internal static Exception LocalTransactionPresent()
		{
			return ADP.InvalidOperation(SR.GetString("Cannot enlist in the transaction because a local transaction is in progress on the connection.  Finish local transaction and retry."));
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000B2B7E File Offset: 0x000B0D7E
		internal static Exception OpenConnectionPropertySet(string property, ConnectionState state)
		{
			return ADP.InvalidOperation(SR.GetString("Not allowed to change the '{0}' property. {1}", new object[]
			{
				property,
				ADP.ConnectionStateMsg(state)
			}));
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000B2BA2 File Offset: 0x000B0DA2
		internal static Exception EmptyDatabaseName()
		{
			return ADP.Argument(SR.GetString("Database cannot be null, the empty string, or string of only whitespace."));
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000B2BB3 File Offset: 0x000B0DB3
		internal static Exception MissingRestrictionColumn()
		{
			return ADP.Argument(SR.GetString("One or more of the required columns of the restrictions collection is missing."));
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000B2BC4 File Offset: 0x000B0DC4
		internal static Exception InternalConnectionError(ADP.ConnectionError internalError)
		{
			return ADP.InvalidOperation(SR.GetString("Internal DbConnection Error: {0}", new object[]
			{
				(int)internalError
			}));
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000B2BE4 File Offset: 0x000B0DE4
		internal static Exception InvalidConnectRetryCountValue()
		{
			return ADP.Argument(SR.GetString("Invalid ConnectRetryCount value (should be 0-255)."));
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000B2BF5 File Offset: 0x000B0DF5
		internal static Exception MissingRestrictionRow()
		{
			return ADP.Argument(SR.GetString("A restriction exists for which there is no matching row in the restrictions collection."));
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x000B2C06 File Offset: 0x000B0E06
		internal static Exception InvalidConnectRetryIntervalValue()
		{
			return ADP.Argument(SR.GetString("Invalid ConnectRetryInterval value (should be 1-60)."));
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000B2C17 File Offset: 0x000B0E17
		internal static InvalidOperationException AsyncOperationPending()
		{
			return ADP.InvalidOperation(SR.GetString("Can not start another operation while there is an asynchronous operation pending."));
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000B2C28 File Offset: 0x000B0E28
		internal static IOException ErrorReadingFromStream(Exception internalException)
		{
			return ADP.IO(SR.GetString("An error occurred while reading."), internalException);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x000B2C3A File Offset: 0x000B0E3A
		internal static ArgumentException InvalidDataType(TypeCode typecode)
		{
			return ADP.Argument(SR.GetString("The parameter data type of {0} is invalid.", new object[]
			{
				typecode.ToString()
			}));
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x000B2C61 File Offset: 0x000B0E61
		internal static ArgumentException UnknownDataType(Type dataType)
		{
			return ADP.Argument(SR.GetString("No mapping exists from object type {0} to a known managed provider native type.", new object[]
			{
				dataType.FullName
			}));
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x000B2C81 File Offset: 0x000B0E81
		internal static ArgumentException DbTypeNotSupported(DbType type, Type enumtype)
		{
			return ADP.Argument(SR.GetString("No mapping exists from DbType {0} to a known {1}.", new object[]
			{
				type.ToString(),
				enumtype.Name
			}));
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x000B2CB4 File Offset: 0x000B0EB4
		internal static ArgumentException UnknownDataTypeCode(Type dataType, TypeCode typeCode)
		{
			string name = "Unable to handle an unknown TypeCode {0} returned by Type {1}.";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)typeCode;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = dataType.FullName;
			return ADP.Argument(SR.GetString(name, array));
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000B2CF0 File Offset: 0x000B0EF0
		internal static ArgumentException InvalidOffsetValue(int value)
		{
			return ADP.Argument(SR.GetString("Invalid parameter Offset value '{0}'. The value must be greater than or equal to 0.", new object[]
			{
				value.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x000B2D16 File Offset: 0x000B0F16
		internal static ArgumentException InvalidSizeValue(int value)
		{
			return ADP.Argument(SR.GetString("Invalid parameter Size value '{0}'. The value must be greater than or equal to 0.", new object[]
			{
				value.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000B2D3C File Offset: 0x000B0F3C
		internal static ArgumentException ParameterValueOutOfRange(decimal value)
		{
			return ADP.Argument(SR.GetString("Parameter value '{0}' is out of range.", new object[]
			{
				value.ToString(null)
			}));
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x000B2D5E File Offset: 0x000B0F5E
		internal static ArgumentException ParameterValueOutOfRange(SqlDecimal value)
		{
			return ADP.Argument(SR.GetString("Parameter value '{0}' is out of range.", new object[]
			{
				value.ToString()
			}));
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x000B2D85 File Offset: 0x000B0F85
		internal static ArgumentException VersionDoesNotSupportDataType(string typeName)
		{
			return ADP.Argument(SR.GetString("The version of SQL Server in use does not support datatype '{0}'.", new object[]
			{
				typeName
			}));
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x000B2DA0 File Offset: 0x000B0FA0
		internal static Exception ParameterConversionFailed(object value, Type destType, Exception inner)
		{
			string @string = SR.GetString("Failed to convert parameter value from a {0} to a {1}.", new object[]
			{
				value.GetType().Name,
				destType.Name
			});
			Exception result;
			if (inner is ArgumentException)
			{
				result = new ArgumentException(@string, inner);
			}
			else if (inner is FormatException)
			{
				result = new FormatException(@string, inner);
			}
			else if (inner is InvalidCastException)
			{
				result = new InvalidCastException(@string, inner);
			}
			else if (inner is OverflowException)
			{
				result = new OverflowException(@string, inner);
			}
			else
			{
				result = inner;
			}
			return result;
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x000B2E20 File Offset: 0x000B1020
		internal static Exception ParametersMappingIndex(int index, DbParameterCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x000B2E34 File Offset: 0x000B1034
		internal static Exception ParametersSourceIndex(string parameterName, DbParameterCollection collection, Type parameterType)
		{
			return ADP.CollectionIndexString(parameterType, "ParameterName", parameterName, collection.GetType());
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x000B2E48 File Offset: 0x000B1048
		internal static Exception ParameterNull(string parameter, DbParameterCollection collection, Type parameterType)
		{
			return ADP.CollectionNullValue(parameter, collection.GetType(), parameterType);
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x00010C60 File Offset: 0x0000EE60
		internal static Exception UndefinedPopulationMechanism(string populationMechanism)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x000B2E57 File Offset: 0x000B1057
		internal static Exception InvalidParameterType(DbParameterCollection collection, Type parameterType, object invalidValue)
		{
			return ADP.CollectionInvalidType(collection.GetType(), parameterType, invalidValue);
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000B2E66 File Offset: 0x000B1066
		internal static Exception ParallelTransactionsNotSupported(DbConnection obj)
		{
			return ADP.InvalidOperation(SR.GetString("{0} does not support parallel transactions.", new object[]
			{
				obj.GetType().Name
			}));
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000B2E8B File Offset: 0x000B108B
		internal static Exception TransactionZombied(DbTransaction obj)
		{
			return ADP.InvalidOperation(SR.GetString("This {0} has completed; it is no longer usable.", new object[]
			{
				obj.GetType().Name
			}));
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000B2EB0 File Offset: 0x000B10B0
		internal static Delegate FindBuilder(MulticastDelegate mcd)
		{
			if (mcd != null)
			{
				foreach (Delegate @delegate in mcd.GetInvocationList())
				{
					if (@delegate.Target is DbCommandBuilder)
					{
						return @delegate;
					}
				}
			}
			return null;
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x000B2EEC File Offset: 0x000B10EC
		internal static void TimerCurrent(out long ticks)
		{
			ticks = DateTime.UtcNow.ToFileTimeUtc();
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000B2F08 File Offset: 0x000B1108
		internal static long TimerCurrent()
		{
			return DateTime.UtcNow.ToFileTimeUtc();
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000B2F22 File Offset: 0x000B1122
		internal static long TimerFromSeconds(int seconds)
		{
			return checked(unchecked((long)seconds) * 10000000L);
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x000B2F2D File Offset: 0x000B112D
		internal static long TimerFromMilliseconds(long milliseconds)
		{
			return checked(milliseconds * 10000L);
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000B2F37 File Offset: 0x000B1137
		internal static bool TimerHasExpired(long timerExpire)
		{
			return ADP.TimerCurrent() > timerExpire;
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000B2F44 File Offset: 0x000B1144
		internal static long TimerRemaining(long timerExpire)
		{
			long num = ADP.TimerCurrent();
			return checked(timerExpire - num);
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000B2F5A File Offset: 0x000B115A
		internal static long TimerRemainingMilliseconds(long timerExpire)
		{
			return ADP.TimerToMilliseconds(ADP.TimerRemaining(timerExpire));
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000B2F67 File Offset: 0x000B1167
		internal static long TimerRemainingSeconds(long timerExpire)
		{
			return ADP.TimerToSeconds(ADP.TimerRemaining(timerExpire));
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000B2F74 File Offset: 0x000B1174
		internal static long TimerToMilliseconds(long timerValue)
		{
			return timerValue / 10000L;
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x000B2F7E File Offset: 0x000B117E
		private static long TimerToSeconds(long timerValue)
		{
			return timerValue / 10000000L;
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x000B2F88 File Offset: 0x000B1188
		internal static string MachineName()
		{
			return Environment.MachineName;
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000B2F8F File Offset: 0x000B118F
		internal static Transaction GetCurrentTransaction()
		{
			return Transaction.Current;
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x000B2F96 File Offset: 0x000B1196
		internal static bool IsDirection(DbParameter value, ParameterDirection condition)
		{
			return condition == (condition & value.Direction);
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000B2FA4 File Offset: 0x000B11A4
		internal static void IsNullOrSqlType(object value, out bool isNull, out bool isSqlType)
		{
			if (value == null || value == DBNull.Value)
			{
				isNull = true;
				isSqlType = false;
				return;
			}
			INullable nullable = value as INullable;
			if (nullable != null)
			{
				isNull = nullable.IsNull;
				isSqlType = (value is SqlBinary || value is SqlBoolean || value is SqlByte || value is SqlBytes || value is SqlChars || value is SqlDateTime || value is SqlDecimal || value is SqlDouble || value is SqlGuid || value is SqlInt16 || value is SqlInt32 || value is SqlInt64 || value is SqlMoney || value is SqlSingle || value is SqlString);
				return;
			}
			isNull = false;
			isSqlType = false;
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000B305D File Offset: 0x000B125D
		internal static Version GetAssemblyVersion()
		{
			if (ADP.s_systemDataVersion == null)
			{
				ADP.s_systemDataVersion = new Version("4.6.57.0");
			}
			return ADP.s_systemDataVersion;
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000B3080 File Offset: 0x000B1280
		internal static bool IsAzureSqlServerEndpoint(string dataSource)
		{
			int i = dataSource.LastIndexOf(',');
			if (i >= 0)
			{
				dataSource = dataSource.Substring(0, i);
			}
			i = dataSource.LastIndexOf('\\');
			if (i >= 0)
			{
				dataSource = dataSource.Substring(0, i);
			}
			dataSource = dataSource.Trim();
			for (i = 0; i < ADP.AzureSqlServerEndpoints.Length; i++)
			{
				if (dataSource.EndsWith(ADP.AzureSqlServerEndpoints[i], StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000B30E8 File Offset: 0x000B12E8
		internal static ArgumentOutOfRangeException InvalidDataRowVersion(DataRowVersion value)
		{
			return ADP.InvalidEnumerationValue(typeof(DataRowVersion), (int)value);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000B30FA File Offset: 0x000B12FA
		internal static ArgumentException SingleValuedProperty(string propertyName, string value)
		{
			ArgumentException ex = new ArgumentException(SR.GetString("The only acceptable value for the property '{0}' is '{1}'.", new object[]
			{
				propertyName,
				value
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000B311F File Offset: 0x000B131F
		internal static ArgumentException DoubleValuedProperty(string propertyName, string value1, string value2)
		{
			ArgumentException ex = new ArgumentException(SR.GetString("The acceptable values for the property '{0}' are '{1}' or '{2}'.", new object[]
			{
				propertyName,
				value1,
				value2
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x000B3148 File Offset: 0x000B1348
		internal static ArgumentException InvalidPrefixSuffix()
		{
			ArgumentException ex = new ArgumentException(SR.GetString("Specified QuotePrefix and QuoteSuffix values do not match."));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000B315F File Offset: 0x000B135F
		internal static ArgumentOutOfRangeException InvalidCommandBehavior(CommandBehavior value)
		{
			return ADP.InvalidEnumerationValue(typeof(CommandBehavior), (int)value);
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x000B3171 File Offset: 0x000B1371
		internal static void ValidateCommandBehavior(CommandBehavior value)
		{
			if (value < CommandBehavior.Default || (CommandBehavior.SingleResult | CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo | CommandBehavior.SingleRow | CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection) < value)
			{
				throw ADP.InvalidCommandBehavior(value);
			}
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000B3183 File Offset: 0x000B1383
		internal static ArgumentOutOfRangeException NotSupportedCommandBehavior(CommandBehavior value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(CommandBehavior), value.ToString(), method);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000B31A2 File Offset: 0x000B13A2
		internal static ArgumentException BadParameterName(string parameterName)
		{
			ArgumentException ex = new ArgumentException(SR.GetString("Specified parameter name '{0}' is not valid.", new object[]
			{
				parameterName
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000B31C4 File Offset: 0x000B13C4
		internal static Exception DeriveParametersNotSupported(IDbCommand value)
		{
			return ADP.DataAdapter(SR.GetString("{0} DeriveParameters only supports CommandType.StoredProcedure, not CommandType. {1}.", new object[]
			{
				value.GetType().Name,
				value.CommandType.ToString()
			}));
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000B320B File Offset: 0x000B140B
		internal static Exception NoStoredProcedureExists(string sproc)
		{
			return ADP.InvalidOperation(SR.GetString("The stored procedure '{0}' doesn't exist.", new object[]
			{
				sproc
			}));
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000B3226 File Offset: 0x000B1426
		internal static InvalidOperationException TransactionCompletedButNotDisposed()
		{
			return ADP.Provider(SR.GetString("The transaction associated with the current connection has completed but has not been disposed.  The transaction must be disposed before the connection can be used to execute SQL statements."));
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000B3237 File Offset: 0x000B1437
		internal static ArgumentOutOfRangeException InvalidUserDefinedTypeSerializationFormat(Format value)
		{
			return ADP.InvalidEnumerationValue(typeof(Format), (int)value);
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000B3249 File Offset: 0x000B1449
		internal static ArgumentOutOfRangeException NotSupportedUserDefinedTypeSerializationFormat(Format value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(Format), value.ToString(), method);
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000B3268 File Offset: 0x000B1468
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName, object value)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName, value, message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000B3278 File Offset: 0x000B1478
		internal static ArgumentException InvalidArgumentLength(string argumentName, int limit)
		{
			return ADP.Argument(SR.GetString("The length of argument '{0}' exceeds its limit of '{1}'.", new object[]
			{
				argumentName,
				limit
			}));
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000B329C File Offset: 0x000B149C
		internal static ArgumentException MustBeReadOnly(string argumentName)
		{
			return ADP.Argument(SR.GetString("{0} must be marked as read only.", new object[]
			{
				argumentName
			}));
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000B32B7 File Offset: 0x000B14B7
		internal static InvalidOperationException InvalidMixedUsageOfSecureAndClearCredential()
		{
			return ADP.InvalidOperation(SR.GetString("Cannot use Credential with UserID, UID, Password, or PWD connection string keywords."));
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000B32C8 File Offset: 0x000B14C8
		internal static ArgumentException InvalidMixedArgumentOfSecureAndClearCredential()
		{
			return ADP.Argument(SR.GetString("Cannot use Credential with UserID, UID, Password, or PWD connection string keywords."));
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000B32D9 File Offset: 0x000B14D9
		internal static InvalidOperationException InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity()
		{
			return ADP.InvalidOperation(SR.GetString("Cannot use Credential with Integrated Security connection string keyword."));
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000B32EA File Offset: 0x000B14EA
		internal static ArgumentException InvalidMixedArgumentOfSecureCredentialAndIntegratedSecurity()
		{
			return ADP.Argument(SR.GetString("Cannot use Credential with Integrated Security connection string keyword."));
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000B32FB File Offset: 0x000B14FB
		internal static InvalidOperationException InvalidMixedUsageOfAccessTokenAndIntegratedSecurity()
		{
			return ADP.InvalidOperation(SR.GetString("Cannot set the AccessToken property if the 'Integrated Security' connection string keyword has been set to 'true' or 'SSPI'."));
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000B330C File Offset: 0x000B150C
		internal static InvalidOperationException InvalidMixedUsageOfAccessTokenAndUserIDPassword()
		{
			return ADP.InvalidOperation(SR.GetString("Cannot set the AccessToken property if 'UserID', 'UID', 'Password', or 'PWD' has been specified in connection string."));
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000B331D File Offset: 0x000B151D
		internal static Exception InvalidMixedUsageOfCredentialAndAccessToken()
		{
			return ADP.InvalidOperation(SR.GetString("Cannot set the Credential property if the AccessToken property is already set."));
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x00006D64 File Offset: 0x00004F64
		internal static bool NeedManualEnlistment()
		{
			return false;
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000B332E File Offset: 0x000B152E
		internal static bool IsEmpty(string str)
		{
			return string.IsNullOrEmpty(str);
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000B3336 File Offset: 0x000B1536
		internal static Exception DatabaseNameTooLong()
		{
			return ADP.Argument(SR.GetString("The argument is too long."));
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x0008DC8E File Offset: 0x0008BE8E
		internal static int StringLength(string inputString)
		{
			if (inputString == null)
			{
				return 0;
			}
			return inputString.Length;
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000B3347 File Offset: 0x000B1547
		internal static Exception NumericToDecimalOverflow()
		{
			return ADP.InvalidCast(SR.GetString("The numerical value is too large to fit into a 96 bit decimal."));
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000B3358 File Offset: 0x000B1558
		internal static Exception OdbcNoTypesFromProvider()
		{
			return ADP.InvalidOperation(SR.GetString("The ODBC provider did not return results from SQLGETTYPEINFO."));
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000B3369 File Offset: 0x000B1569
		internal static ArgumentException InvalidRestrictionValue(string collectionName, string restrictionName, string restrictionValue)
		{
			return ADP.Argument(SR.GetString("'{2}' is not a valid value for the '{1}' restriction of the '{0}' schema collection.", new object[]
			{
				collectionName,
				restrictionName,
				restrictionValue
			}));
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000B338C File Offset: 0x000B158C
		internal static Exception DataReaderNoData()
		{
			return ADP.InvalidOperation(SR.GetString("No data exists for the row/column."));
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000B339D File Offset: 0x000B159D
		internal static Exception ConnectionIsDisabled(Exception InnerException)
		{
			return ADP.InvalidOperation(SR.GetString("The connection has been disabled."), InnerException);
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000B33AF File Offset: 0x000B15AF
		internal static Exception OffsetOutOfRangeException()
		{
			return ADP.InvalidOperation(SR.GetString("Offset must refer to a location within the value."));
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000B33C0 File Offset: 0x000B15C0
		internal static InvalidOperationException QuotePrefixNotSet(string method)
		{
			return ADP.InvalidOperation(Res.GetString("{0} requires open connection when the quote prefix has not been set.", new object[]
			{
				method
			}));
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000B33DB File Offset: 0x000B15DB
		internal static string GetFullPath(string filename)
		{
			return Path.GetFullPath(filename);
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000B33E3 File Offset: 0x000B15E3
		internal static InvalidOperationException InvalidDataDirectory()
		{
			return ADP.InvalidOperation(SR.GetString("The DataDirectory substitute is not a string."));
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000B33F4 File Offset: 0x000B15F4
		internal static void EscapeSpecialCharacters(string unescapedString, StringBuilder escapedString)
		{
			foreach (char value in unescapedString)
			{
				if (".$^{[(|)*+?\\]".IndexOf(value) >= 0)
				{
					escapedString.Append("\\");
				}
				escapedString.Append(value);
			}
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000B343E File Offset: 0x000B163E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal static IntPtr IntPtrOffset(IntPtr pbase, int offset)
		{
			checked
			{
				if (4 == ADP.PtrSize)
				{
					return (IntPtr)(pbase.ToInt32() + offset);
				}
				return (IntPtr)(pbase.ToInt64() + unchecked((long)offset));
			}
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000B3466 File Offset: 0x000B1666
		internal static Exception InvalidXMLBadVersion()
		{
			return ADP.Argument(Res.GetString("Invalid Xml; can only parse elements of version one."));
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000B3477 File Offset: 0x000B1677
		internal static Exception NotAPermissionElement()
		{
			return ADP.Argument(Res.GetString("Given security element is not a permission element."));
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000B3488 File Offset: 0x000B1688
		internal static Exception PermissionTypeMismatch()
		{
			return ADP.Argument(Res.GetString("Type mismatch."));
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000B3499 File Offset: 0x000B1699
		internal static ArgumentOutOfRangeException InvalidPermissionState(PermissionState value)
		{
			return ADP.InvalidEnumerationValue(typeof(PermissionState), (int)value);
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000B34AB File Offset: 0x000B16AB
		internal static ConfigurationException Configuration(string message)
		{
			ConfigurationErrorsException ex = new ConfigurationErrorsException(message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000B34B9 File Offset: 0x000B16B9
		internal static ConfigurationException Configuration(string message, XmlNode node)
		{
			ConfigurationErrorsException ex = new ConfigurationErrorsException(message, node);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000B34C8 File Offset: 0x000B16C8
		internal static ArgumentException ConfigProviderNotFound()
		{
			return ADP.Argument(Res.GetString("Unable to find the requested .Net Framework Data Provider.  It may not be installed."));
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000B34D9 File Offset: 0x000B16D9
		internal static InvalidOperationException ConfigProviderInvalid()
		{
			return ADP.InvalidOperation(Res.GetString("The requested .Net Framework Data Provider's implementation does not have an Instance field of a System.Data.Common.DbProviderFactory derived type."));
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000B34EA File Offset: 0x000B16EA
		internal static ConfigurationException ConfigProviderNotInstalled()
		{
			return ADP.Configuration(Res.GetString("Failed to find or load the registered .Net Framework Data Provider."));
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000B34FB File Offset: 0x000B16FB
		internal static ConfigurationException ConfigProviderMissing()
		{
			return ADP.Configuration(Res.GetString("The missing .Net Framework Data Provider's assembly qualified name is required."));
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x000B350C File Offset: 0x000B170C
		internal static ConfigurationException ConfigBaseNoChildNodes(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Child nodes not allowed."), node);
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000B351E File Offset: 0x000B171E
		internal static ConfigurationException ConfigBaseElementsOnly(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Only elements allowed."), node);
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x000B3530 File Offset: 0x000B1730
		internal static ConfigurationException ConfigUnrecognizedAttributes(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Unrecognized attribute '{0}'.", new object[]
			{
				node.Attributes[0].Name
			}), node);
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000B355C File Offset: 0x000B175C
		internal static ConfigurationException ConfigUnrecognizedElement(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Unrecognized element."), node);
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000B356E File Offset: 0x000B176E
		internal static ConfigurationException ConfigSectionsUnique(string sectionName)
		{
			return ADP.Configuration(Res.GetString("The '{0}' section can only appear once per config file.", new object[]
			{
				sectionName
			}));
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000B3589 File Offset: 0x000B1789
		internal static ConfigurationException ConfigRequiredAttributeMissing(string name, XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Required attribute '{0}' not found.", new object[]
			{
				name
			}), node);
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000B35A5 File Offset: 0x000B17A5
		internal static ConfigurationException ConfigRequiredAttributeEmpty(string name, XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Required attribute '{0}' cannot be empty.", new object[]
			{
				name
			}), node);
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000B35C1 File Offset: 0x000B17C1
		internal static Exception OleDb()
		{
			return new NotImplementedException("OleDb is not implemented.");
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x000B35D0 File Offset: 0x000B17D0
		// Note: this type is marked as 'beforefieldinit'.
		static ADP()
		{
		}

		// Token: 0x040019DF RID: 6623
		private static Task<bool> _trueTask;

		// Token: 0x040019E0 RID: 6624
		private static Task<bool> _falseTask;

		// Token: 0x040019E1 RID: 6625
		internal const CompareOptions DefaultCompareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x040019E2 RID: 6626
		internal const int DefaultConnectionTimeout = 15;

		// Token: 0x040019E3 RID: 6627
		private static readonly Type s_stackOverflowType = typeof(StackOverflowException);

		// Token: 0x040019E4 RID: 6628
		private static readonly Type s_outOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x040019E5 RID: 6629
		private static readonly Type s_threadAbortType = typeof(ThreadAbortException);

		// Token: 0x040019E6 RID: 6630
		private static readonly Type s_nullReferenceType = typeof(NullReferenceException);

		// Token: 0x040019E7 RID: 6631
		private static readonly Type s_accessViolationType = typeof(AccessViolationException);

		// Token: 0x040019E8 RID: 6632
		private static readonly Type s_securityType = typeof(SecurityException);

		// Token: 0x040019E9 RID: 6633
		internal const string ConnectionString = "ConnectionString";

		// Token: 0x040019EA RID: 6634
		internal const string DataSetColumn = "DataSetColumn";

		// Token: 0x040019EB RID: 6635
		internal const string DataSetTable = "DataSetTable";

		// Token: 0x040019EC RID: 6636
		internal const string Fill = "Fill";

		// Token: 0x040019ED RID: 6637
		internal const string FillSchema = "FillSchema";

		// Token: 0x040019EE RID: 6638
		internal const string SourceColumn = "SourceColumn";

		// Token: 0x040019EF RID: 6639
		internal const string SourceTable = "SourceTable";

		// Token: 0x040019F0 RID: 6640
		internal const string Parameter = "Parameter";

		// Token: 0x040019F1 RID: 6641
		internal const string ParameterName = "ParameterName";

		// Token: 0x040019F2 RID: 6642
		internal const string ParameterSetPosition = "set_Position";

		// Token: 0x040019F3 RID: 6643
		internal const int DefaultCommandTimeout = 30;

		// Token: 0x040019F4 RID: 6644
		internal const float FailoverTimeoutStep = 0.08f;

		// Token: 0x040019F5 RID: 6645
		internal static readonly string StrEmpty = "";

		// Token: 0x040019F6 RID: 6646
		internal const int CharSize = 2;

		// Token: 0x040019F7 RID: 6647
		private static Version s_systemDataVersion;

		// Token: 0x040019F8 RID: 6648
		internal static readonly string[] AzureSqlServerEndpoints = new string[]
		{
			SR.GetString(".database.windows.net"),
			SR.GetString(".database.cloudapi.de"),
			SR.GetString(".database.usgovcloudapi.net"),
			SR.GetString(".database.chinacloudapi.cn")
		};

		// Token: 0x040019F9 RID: 6649
		internal const int DecimalMaxPrecision = 29;

		// Token: 0x040019FA RID: 6650
		internal const int DecimalMaxPrecision28 = 28;

		// Token: 0x040019FB RID: 6651
		internal static readonly IntPtr PtrZero = new IntPtr(0);

		// Token: 0x040019FC RID: 6652
		internal static readonly int PtrSize = IntPtr.Size;

		// Token: 0x040019FD RID: 6653
		internal const string BeginTransaction = "BeginTransaction";

		// Token: 0x040019FE RID: 6654
		internal const string ChangeDatabase = "ChangeDatabase";

		// Token: 0x040019FF RID: 6655
		internal const string CommitTransaction = "CommitTransaction";

		// Token: 0x04001A00 RID: 6656
		internal const string CommandTimeout = "CommandTimeout";

		// Token: 0x04001A01 RID: 6657
		internal const string DeriveParameters = "DeriveParameters";

		// Token: 0x04001A02 RID: 6658
		internal const string ExecuteReader = "ExecuteReader";

		// Token: 0x04001A03 RID: 6659
		internal const string ExecuteNonQuery = "ExecuteNonQuery";

		// Token: 0x04001A04 RID: 6660
		internal const string ExecuteScalar = "ExecuteScalar";

		// Token: 0x04001A05 RID: 6661
		internal const string GetSchema = "GetSchema";

		// Token: 0x04001A06 RID: 6662
		internal const string GetSchemaTable = "GetSchemaTable";

		// Token: 0x04001A07 RID: 6663
		internal const string Prepare = "Prepare";

		// Token: 0x04001A08 RID: 6664
		internal const string RollbackTransaction = "RollbackTransaction";

		// Token: 0x04001A09 RID: 6665
		internal const string QuoteIdentifier = "QuoteIdentifier";

		// Token: 0x04001A0A RID: 6666
		internal const string UnquoteIdentifier = "UnquoteIdentifier";

		// Token: 0x02000368 RID: 872
		internal enum InternalErrorCode
		{
			// Token: 0x04001A0C RID: 6668
			UnpooledObjectHasOwner,
			// Token: 0x04001A0D RID: 6669
			UnpooledObjectHasWrongOwner,
			// Token: 0x04001A0E RID: 6670
			PushingObjectSecondTime,
			// Token: 0x04001A0F RID: 6671
			PooledObjectHasOwner,
			// Token: 0x04001A10 RID: 6672
			PooledObjectInPoolMoreThanOnce,
			// Token: 0x04001A11 RID: 6673
			CreateObjectReturnedNull,
			// Token: 0x04001A12 RID: 6674
			NewObjectCannotBePooled,
			// Token: 0x04001A13 RID: 6675
			NonPooledObjectUsedMoreThanOnce,
			// Token: 0x04001A14 RID: 6676
			AttemptingToPoolOnRestrictedToken,
			// Token: 0x04001A15 RID: 6677
			ConvertSidToStringSidWReturnedNull = 10,
			// Token: 0x04001A16 RID: 6678
			AttemptingToConstructReferenceCollectionOnStaticObject = 12,
			// Token: 0x04001A17 RID: 6679
			AttemptingToEnlistTwice,
			// Token: 0x04001A18 RID: 6680
			CreateReferenceCollectionReturnedNull,
			// Token: 0x04001A19 RID: 6681
			PooledObjectWithoutPool,
			// Token: 0x04001A1A RID: 6682
			UnexpectedWaitAnyResult,
			// Token: 0x04001A1B RID: 6683
			SynchronousConnectReturnedPending,
			// Token: 0x04001A1C RID: 6684
			CompletedConnectReturnedPending,
			// Token: 0x04001A1D RID: 6685
			NameValuePairNext = 20,
			// Token: 0x04001A1E RID: 6686
			InvalidParserState1,
			// Token: 0x04001A1F RID: 6687
			InvalidParserState2,
			// Token: 0x04001A20 RID: 6688
			InvalidParserState3,
			// Token: 0x04001A21 RID: 6689
			InvalidBuffer = 30,
			// Token: 0x04001A22 RID: 6690
			UnimplementedSMIMethod = 40,
			// Token: 0x04001A23 RID: 6691
			InvalidSmiCall,
			// Token: 0x04001A24 RID: 6692
			SqlDependencyObtainProcessDispatcherFailureObjectHandle = 50,
			// Token: 0x04001A25 RID: 6693
			SqlDependencyProcessDispatcherFailureCreateInstance,
			// Token: 0x04001A26 RID: 6694
			SqlDependencyProcessDispatcherFailureAppDomain,
			// Token: 0x04001A27 RID: 6695
			SqlDependencyCommandHashIsNotAssociatedWithNotification,
			// Token: 0x04001A28 RID: 6696
			UnknownTransactionFailure = 60
		}

		// Token: 0x02000369 RID: 873
		internal enum ConnectionError
		{
			// Token: 0x04001A2A RID: 6698
			BeginGetConnectionReturnsNull,
			// Token: 0x04001A2B RID: 6699
			GetConnectionReturnsNull,
			// Token: 0x04001A2C RID: 6700
			ConnectionOptionsMissing,
			// Token: 0x04001A2D RID: 6701
			CouldNotSwitchToClosedPreviouslyOpenedState
		}
	}
}
