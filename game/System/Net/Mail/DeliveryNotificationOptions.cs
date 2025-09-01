using System;

namespace System.Net.Mail
{
	/// <summary>Describes the delivery notification options for email.</summary>
	// Token: 0x02000828 RID: 2088
	[Flags]
	public enum DeliveryNotificationOptions
	{
		/// <summary>No notification information will be sent. The mail server will utilize its configured behavior to determine whether it should generate a delivery notification.</summary>
		// Token: 0x0400284C RID: 10316
		None = 0,
		/// <summary>Notify if the delivery is successful.</summary>
		// Token: 0x0400284D RID: 10317
		OnSuccess = 1,
		/// <summary>Notify if the delivery is unsuccessful.</summary>
		// Token: 0x0400284E RID: 10318
		OnFailure = 2,
		/// <summary>Notify if the delivery is delayed.</summary>
		// Token: 0x0400284F RID: 10319
		Delay = 4,
		/// <summary>A notification should not be generated under any circumstances.</summary>
		// Token: 0x04002850 RID: 10320
		Never = 134217728
	}
}
