using System;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents a SQL client logger.</summary>
	// Token: 0x020003F0 RID: 1008
	public class SqlClientLogger
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientLogger" /> class.</summary>
		// Token: 0x06002F94 RID: 12180 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public SqlClientLogger()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value that indicates whether bid tracing is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if bid tracing is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002F95 RID: 12181 RVA: 0x000CBAE8 File Offset: 0x000C9CE8
		public bool IsLoggingEnabled
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Logs the specified message if <paramref name="value" /> is <see langword="false" />.</summary>
		/// <param name="value">
		///   <see langword="false" /> to log the message; otherwise, <see langword="true" />.</param>
		/// <param name="type">The type to be logged.</param>
		/// <param name="method">The logging method.</param>
		/// <param name="message">The message to be logged.</param>
		/// <returns>
		///   <see langword="true" /> if the message is not logged; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002F96 RID: 12182 RVA: 0x000CBB04 File Offset: 0x000C9D04
		public bool LogAssert(bool value, string type, string method, string message)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Logs an error through a specified method of the current instance type.</summary>
		/// <param name="type">The type to be logged.</param>
		/// <param name="method">The logging method.</param>
		/// <param name="message">The message to be logged.</param>
		// Token: 0x06002F97 RID: 12183 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public void LogError(string type, string method, string message)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Logs information through a specified method of the current instance type.</summary>
		/// <param name="type">The type to be logged.</param>
		/// <param name="method">The logging method.</param>
		/// <param name="message">The message to be logged.</param>
		// Token: 0x06002F98 RID: 12184 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public void LogInfo(string type, string method, string message)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
