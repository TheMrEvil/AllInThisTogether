using System;

namespace System.Data.SqlClient
{
	/// <summary>Describes the different notification types that can be received by an <see cref="T:System.Data.SqlClient.OnChangeEventHandler" /> event handler through the <see cref="T:System.Data.SqlClient.SqlNotificationEventArgs" /> parameter.</summary>
	// Token: 0x02000215 RID: 533
	public enum SqlNotificationType
	{
		/// <summary>Data on the server being monitored changed. Use the <see cref="T:System.Data.SqlClient.SqlNotificationInfo" /> item to determine the details of the change.</summary>
		// Token: 0x040010CB RID: 4299
		Change,
		/// <summary>There was a failure to create a notification subscription. Use the <see cref="T:System.Data.SqlClient.SqlNotificationEventArgs" /> object's <see cref="T:System.Data.SqlClient.SqlNotificationInfo" /> item to determine the cause of the failure.</summary>
		// Token: 0x040010CC RID: 4300
		Subscribe,
		/// <summary>Used when the type option sent by the server was not recognized by the client.</summary>
		// Token: 0x040010CD RID: 4301
		Unknown = -1
	}
}
