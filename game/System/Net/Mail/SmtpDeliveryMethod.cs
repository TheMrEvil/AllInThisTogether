using System;

namespace System.Net.Mail
{
	/// <summary>Specifies how email messages are delivered.</summary>
	// Token: 0x02000838 RID: 2104
	public enum SmtpDeliveryMethod
	{
		/// <summary>Email is sent through the network to an SMTP server.</summary>
		// Token: 0x040028A1 RID: 10401
		Network,
		/// <summary>Email is copied to the directory specified by the <see cref="P:System.Net.Mail.SmtpClient.PickupDirectoryLocation" /> property for delivery by an external application.</summary>
		// Token: 0x040028A2 RID: 10402
		SpecifiedPickupDirectory,
		/// <summary>Email is copied to the pickup directory used by a local Internet Information Services (IIS) for delivery.</summary>
		// Token: 0x040028A3 RID: 10403
		PickupDirectoryFromIis
	}
}
