using System;
using System.Data.SqlClient;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Allows managed stored procedures running in-process on a SQL Server database to return results back to the caller. This class cannot be inherited.</summary>
	// Token: 0x02000065 RID: 101
	public sealed class SqlPipe
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x00003D93 File Offset: 0x00001F93
		private SqlPipe()
		{
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:Microsoft.SqlServer.Server.SqlPipe" /> is in the mode of sending single result sets back to the client. This property is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsStart(Microsoft.SqlServer.Server.SqlDataRecord)" /> method has been called and the <see cref="T:Microsoft.SqlServer.Server.SqlPipe" /> is in the mode of sending single result sets back to the client; otherwise <see langword="false" />.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00006D64 File Offset: 0x00004F64
		public bool IsSendingResults
		{
			get
			{
				return false;
			}
		}

		/// <summary>Executes the command passed as a parameter and sends the results to the client.</summary>
		/// <param name="command">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object to be executed.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="command" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This method is not supported on commands bound to out-of-process connections.</exception>
		// Token: 0x060004AB RID: 1195 RVA: 0x00010C60 File Offset: 0x0000EE60
		public void ExecuteAndSend(SqlCommand command)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends a string message directly to the client or current output consumer.</summary>
		/// <param name="message">The message string to be sent to the client.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="message" /> is greater than 4,000 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="message" /> is <see langword="null" />.</exception>
		// Token: 0x060004AC RID: 1196 RVA: 0x00010C60 File Offset: 0x0000EE60
		public void Send(string message)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends a multirow result set directly to the client or current output consumer.</summary>
		/// <param name="reader">The multirow result set to be sent to the client: a <see cref="T:System.Data.SqlClient.SqlDataReader" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x060004AD RID: 1197 RVA: 0x00010C60 File Offset: 0x0000EE60
		public void Send(SqlDataReader reader)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends a single-row result set directly to the client or current output consumer.</summary>
		/// <param name="record">The single-row result set sent to the client: a <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="record" /> is <see langword="null" />.</exception>
		// Token: 0x060004AE RID: 1198 RVA: 0x00010C60 File Offset: 0x0000EE60
		public void Send(SqlDataRecord record)
		{
			throw new NotImplementedException();
		}

		/// <summary>Marks the beginning of a result set to be sent back to the client, and uses the record parameter to construct the metadata that describes the result set.</summary>
		/// <param name="record">A <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> object from which metadata is extracted and used to describe the result set.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="record" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="record" /> has no columns or has not been initialized.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method other than <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsRow(Microsoft.SqlServer.Server.SqlDataRecord)" /> or <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsEnd" /> was called after the <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsStart(Microsoft.SqlServer.Server.SqlDataRecord)" /> method.</exception>
		// Token: 0x060004AF RID: 1199 RVA: 0x00010C60 File Offset: 0x0000EE60
		public void SendResultsStart(SqlDataRecord record)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends a single row of data back to the client.</summary>
		/// <param name="record">A <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> object with the column values for the row to be sent to the client. The schema for the record must match the schema described by the metadata of the <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> passed to the <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsStart(Microsoft.SqlServer.Server.SqlDataRecord)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="record" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsStart(Microsoft.SqlServer.Server.SqlDataRecord)" /> method was not previously called.</exception>
		// Token: 0x060004B0 RID: 1200 RVA: 0x00010C60 File Offset: 0x0000EE60
		public void SendResultsRow(SqlDataRecord record)
		{
			throw new NotImplementedException();
		}

		/// <summary>Marks the end of a result set, and returns the <see cref="T:Microsoft.SqlServer.Server.SqlPipe" /> instance back to the initial state.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:Microsoft.SqlServer.Server.SqlPipe.SendResultsStart(Microsoft.SqlServer.Server.SqlDataRecord)" /> method was not previously called.</exception>
		// Token: 0x060004B1 RID: 1201 RVA: 0x00010C60 File Offset: 0x0000EE60
		public void SendResultsEnd()
		{
			throw new NotImplementedException();
		}
	}
}
