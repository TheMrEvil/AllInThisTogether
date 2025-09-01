using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Data.SqlClient
{
	// Token: 0x020001AE RID: 430
	internal static class SqlClientDiagnosticListenerExtensions
	{
		// Token: 0x06001522 RID: 5410 RVA: 0x00060830 File Offset: 0x0005EA30
		public static Guid WriteCommandBefore(this DiagnosticListener @this, SqlCommand sqlCommand, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteCommandBefore"))
			{
				Guid guid = Guid.NewGuid();
				string s = "System.Data.SqlClient.WriteCommandBefore";
				Guid operationId = guid;
				SqlConnection connection = sqlCommand.Connection;
				@this.Write(s, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = ((connection != null) ? new Guid?(connection.ClientConnectionId) : null),
					Command = sqlCommand
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0006088C File Offset: 0x0005EA8C
		public static void WriteCommandAfter(this DiagnosticListener @this, Guid operationId, SqlCommand sqlCommand, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteCommandAfter"))
			{
				string s = "System.Data.SqlClient.WriteCommandAfter";
				SqlConnection connection = sqlCommand.Connection;
				Guid? connectionId = (connection != null) ? new Guid?(connection.ClientConnectionId) : null;
				SqlStatistics statistics = sqlCommand.Statistics;
				@this.Write(s, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = connectionId,
					Command = sqlCommand,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x000608F0 File Offset: 0x0005EAF0
		public static void WriteCommandError(this DiagnosticListener @this, Guid operationId, SqlCommand sqlCommand, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteCommandError"))
			{
				string s = "System.Data.SqlClient.WriteCommandError";
				SqlConnection connection = sqlCommand.Connection;
				@this.Write(s, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = ((connection != null) ? new Guid?(connection.ClientConnectionId) : null),
					Command = sqlCommand,
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x00060944 File Offset: 0x0005EB44
		public static Guid WriteConnectionOpenBefore(this DiagnosticListener @this, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionOpenBefore"))
			{
				Guid guid = Guid.NewGuid();
				@this.Write("System.Data.SqlClient.WriteConnectionOpenBefore", new
				{
					OperationId = guid,
					Operation = operation,
					Connection = sqlConnection,
					Timestamp = Stopwatch.GetTimestamp()
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00060983 File Offset: 0x0005EB83
		public static void WriteConnectionOpenAfter(this DiagnosticListener @this, Guid operationId, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionOpenAfter"))
			{
				string s = "System.Data.SqlClient.WriteConnectionOpenAfter";
				Guid clientConnectionId = sqlConnection.ClientConnectionId;
				SqlStatistics statistics = sqlConnection.Statistics;
				@this.Write(s, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = clientConnectionId,
					Connection = sqlConnection,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x000609C2 File Offset: 0x0005EBC2
		public static void WriteConnectionOpenError(this DiagnosticListener @this, Guid operationId, SqlConnection sqlConnection, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionOpenError"))
			{
				@this.Write("System.Data.SqlClient.WriteConnectionOpenError", new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = sqlConnection.ClientConnectionId,
					Connection = sqlConnection,
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x000609F4 File Offset: 0x0005EBF4
		public static Guid WriteConnectionCloseBefore(this DiagnosticListener @this, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionCloseBefore"))
			{
				Guid guid = Guid.NewGuid();
				string s = "System.Data.SqlClient.WriteConnectionCloseBefore";
				Guid operationId = guid;
				Guid clientConnectionId = sqlConnection.ClientConnectionId;
				SqlStatistics statistics = sqlConnection.Statistics;
				@this.Write(s, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = clientConnectionId,
					Connection = sqlConnection,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Timestamp = Stopwatch.GetTimestamp()
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00060A4B File Offset: 0x0005EC4B
		public static void WriteConnectionCloseAfter(this DiagnosticListener @this, Guid operationId, Guid clientConnectionId, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionCloseAfter"))
			{
				string s = "System.Data.SqlClient.WriteConnectionCloseAfter";
				SqlStatistics statistics = sqlConnection.Statistics;
				@this.Write(s, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = clientConnectionId,
					Connection = sqlConnection,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00060A88 File Offset: 0x0005EC88
		public static void WriteConnectionCloseError(this DiagnosticListener @this, Guid operationId, Guid clientConnectionId, SqlConnection sqlConnection, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionCloseError"))
			{
				string s = "System.Data.SqlClient.WriteConnectionCloseError";
				SqlStatistics statistics = sqlConnection.Statistics;
				@this.Write(s, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = clientConnectionId,
					Connection = sqlConnection,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00060AD0 File Offset: 0x0005ECD0
		public static Guid WriteTransactionCommitBefore(this DiagnosticListener @this, IsolationLevel isolationLevel, SqlConnection connection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionCommitBefore"))
			{
				Guid guid = Guid.NewGuid();
				@this.Write("System.Data.SqlClient.WriteTransactionCommitBefore", new
				{
					OperationId = guid,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					Timestamp = Stopwatch.GetTimestamp()
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00060B10 File Offset: 0x0005ED10
		public static void WriteTransactionCommitAfter(this DiagnosticListener @this, Guid operationId, IsolationLevel isolationLevel, SqlConnection connection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionCommitAfter"))
			{
				@this.Write("System.Data.SqlClient.WriteTransactionCommitAfter", new
				{
					OperationId = operationId,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x00060B39 File Offset: 0x0005ED39
		public static void WriteTransactionCommitError(this DiagnosticListener @this, Guid operationId, IsolationLevel isolationLevel, SqlConnection connection, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionCommitError"))
			{
				@this.Write("System.Data.SqlClient.WriteTransactionCommitError", new
				{
					OperationId = operationId,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00060B64 File Offset: 0x0005ED64
		public static Guid WriteTransactionRollbackBefore(this DiagnosticListener @this, IsolationLevel isolationLevel, SqlConnection connection, string transactionName, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionRollbackBefore"))
			{
				Guid guid = Guid.NewGuid();
				@this.Write("System.Data.SqlClient.WriteTransactionRollbackBefore", new
				{
					OperationId = guid,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					TransactionName = transactionName,
					Timestamp = Stopwatch.GetTimestamp()
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00060BA6 File Offset: 0x0005EDA6
		public static void WriteTransactionRollbackAfter(this DiagnosticListener @this, Guid operationId, IsolationLevel isolationLevel, SqlConnection connection, string transactionName, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionRollbackAfter"))
			{
				@this.Write("System.Data.SqlClient.WriteTransactionRollbackAfter", new
				{
					OperationId = operationId,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					TransactionName = transactionName,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00060BD4 File Offset: 0x0005EDD4
		public static void WriteTransactionRollbackError(this DiagnosticListener @this, Guid operationId, IsolationLevel isolationLevel, SqlConnection connection, string transactionName, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionRollbackError"))
			{
				@this.Write("System.Data.SqlClient.WriteTransactionRollbackError", new
				{
					OperationId = operationId,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					TransactionName = transactionName,
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x04000D71 RID: 3441
		public const string DiagnosticListenerName = "SqlClientDiagnosticListener";

		// Token: 0x04000D72 RID: 3442
		private const string SqlClientPrefix = "System.Data.SqlClient.";

		// Token: 0x04000D73 RID: 3443
		public const string SqlBeforeExecuteCommand = "System.Data.SqlClient.WriteCommandBefore";

		// Token: 0x04000D74 RID: 3444
		public const string SqlAfterExecuteCommand = "System.Data.SqlClient.WriteCommandAfter";

		// Token: 0x04000D75 RID: 3445
		public const string SqlErrorExecuteCommand = "System.Data.SqlClient.WriteCommandError";

		// Token: 0x04000D76 RID: 3446
		public const string SqlBeforeOpenConnection = "System.Data.SqlClient.WriteConnectionOpenBefore";

		// Token: 0x04000D77 RID: 3447
		public const string SqlAfterOpenConnection = "System.Data.SqlClient.WriteConnectionOpenAfter";

		// Token: 0x04000D78 RID: 3448
		public const string SqlErrorOpenConnection = "System.Data.SqlClient.WriteConnectionOpenError";

		// Token: 0x04000D79 RID: 3449
		public const string SqlBeforeCloseConnection = "System.Data.SqlClient.WriteConnectionCloseBefore";

		// Token: 0x04000D7A RID: 3450
		public const string SqlAfterCloseConnection = "System.Data.SqlClient.WriteConnectionCloseAfter";

		// Token: 0x04000D7B RID: 3451
		public const string SqlErrorCloseConnection = "System.Data.SqlClient.WriteConnectionCloseError";

		// Token: 0x04000D7C RID: 3452
		public const string SqlBeforeCommitTransaction = "System.Data.SqlClient.WriteTransactionCommitBefore";

		// Token: 0x04000D7D RID: 3453
		public const string SqlAfterCommitTransaction = "System.Data.SqlClient.WriteTransactionCommitAfter";

		// Token: 0x04000D7E RID: 3454
		public const string SqlErrorCommitTransaction = "System.Data.SqlClient.WriteTransactionCommitError";

		// Token: 0x04000D7F RID: 3455
		public const string SqlBeforeRollbackTransaction = "System.Data.SqlClient.WriteTransactionRollbackBefore";

		// Token: 0x04000D80 RID: 3456
		public const string SqlAfterRollbackTransaction = "System.Data.SqlClient.WriteTransactionRollbackAfter";

		// Token: 0x04000D81 RID: 3457
		public const string SqlErrorRollbackTransaction = "System.Data.SqlClient.WriteTransactionRollbackError";
	}
}
