using System;

namespace System.Net.Mail
{
	/// <summary>The delivery format to use for sending outgoing email using the Simple Mail Transport Protocol (SMTP).</summary>
	// Token: 0x02000837 RID: 2103
	public enum SmtpDeliveryFormat
	{
		/// <summary>A delivery format using 7-bit ASCII.  
		///  The traditional delivery format used in the Simple Mail Transport Protocol (SMTP) for mail messages.</summary>
		// Token: 0x0400289E RID: 10398
		SevenBit,
		/// <summary>A delivery format where non-ASCII characters in the envelope and header fields used in the Simple Mail Transport Protocol (SMTP) for mail messages are encoded with UTF-8 characters.  
		///  The extensions to support international email are defined in IETF RFC 6530, 6531, and 6532.</summary>
		// Token: 0x0400289F RID: 10399
		International
	}
}
