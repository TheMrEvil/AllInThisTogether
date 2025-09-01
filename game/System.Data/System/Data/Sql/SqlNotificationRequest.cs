using System;
using System.Data.Common;

namespace System.Data.Sql
{
	/// <summary>Represents a request for notification for a given command.</summary>
	// Token: 0x02000176 RID: 374
	public sealed class SqlNotificationRequest
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.Sql.SqlNotificationRequest" /> class with default values.</summary>
		// Token: 0x060013DF RID: 5087 RVA: 0x0005AE4C File Offset: 0x0005904C
		public SqlNotificationRequest() : this(null, null, 0)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Data.Sql.SqlNotificationRequest" /> class with a user-defined string that identifies a particular notification request, the name of a predefined SQL Server 2005 Service Broker service name, and the time-out period, measured in seconds.</summary>
		/// <param name="userData">A string that contains an application-specific identifier for this notification. It is not used by the notifications infrastructure, but it allows you to associate notifications with the application state. The value indicated in this parameter is included in the Service Broker queue message.</param>
		/// <param name="options">A string that contains the Service Broker service name where notification messages are posted, and it must include a database name or a Service Broker instance GUID that restricts the scope of the service name lookup to a particular database.  
		///  For more information about the format of the <paramref name="options" /> parameter, see <see cref="P:System.Data.Sql.SqlNotificationRequest.Options" />.</param>
		/// <param name="timeout">The time, in seconds, to wait for a notification message.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="options" /> parameter is NULL.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="options" /> or <paramref name="userData" /> parameter is longer than <see langword="uint16.MaxValue" /> or the value in the <paramref name="timeout" /> parameter is less than zero.</exception>
		// Token: 0x060013E0 RID: 5088 RVA: 0x0005AE57 File Offset: 0x00059057
		public SqlNotificationRequest(string userData, string options, int timeout)
		{
			this.UserData = userData;
			this.Timeout = timeout;
			this.Options = options;
		}

		/// <summary>Gets or sets the SQL Server Service Broker service name where notification messages are posted.</summary>
		/// <returns>
		///   <see langword="string" /> that contains the SQL Server 2005 Service Broker service name where notification messages are posted and the database or service broker instance GUID to scope the server name lookup.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is NULL.</exception>
		/// <exception cref="T:System.ArgumentException">The value is longer than <see langword="uint16.MaxValue" />.</exception>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0005AE74 File Offset: 0x00059074
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x0005AE7C File Offset: 0x0005907C
		public string Options
		{
			get
			{
				return this._options;
			}
			set
			{
				if (value != null && 65535 < value.Length)
				{
					throw ADP.ArgumentOutOfRange(string.Empty, "Options");
				}
				this._options = value;
			}
		}

		/// <summary>Gets or sets a value that specifies how long SQL Server waits for a change to occur before the operation times out.</summary>
		/// <returns>A signed integer value that specifies, in seconds, how long SQL Server waits for a change to occur before the operation times out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than zero.</exception>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0005AEA5 File Offset: 0x000590A5
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x0005AEAD File Offset: 0x000590AD
		public int Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				if (0 > value)
				{
					throw ADP.ArgumentOutOfRange(string.Empty, "Timeout");
				}
				this._timeout = value;
			}
		}

		/// <summary>Gets or sets an application-specific identifier for this notification.</summary>
		/// <returns>A <see langword="string" /> value of the application-specific identifier for this notification.</returns>
		/// <exception cref="T:System.ArgumentException">The value is longer than <see langword="uint16.MaxValue" />.</exception>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0005AECA File Offset: 0x000590CA
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x0005AED2 File Offset: 0x000590D2
		public string UserData
		{
			get
			{
				return this._userData;
			}
			set
			{
				if (value != null && 65535 < value.Length)
				{
					throw ADP.ArgumentOutOfRange(string.Empty, "UserData");
				}
				this._userData = value;
			}
		}

		// Token: 0x04000C27 RID: 3111
		private string _userData;

		// Token: 0x04000C28 RID: 3112
		private string _options;

		// Token: 0x04000C29 RID: 3113
		private int _timeout;
	}
}
