using System;

namespace System.Net.Mail
{
	/// <summary>Specifies the level of access allowed to a Simple Mail Transport Protocol (SMTP) server.</summary>
	// Token: 0x0200082E RID: 2094
	public enum SmtpAccess
	{
		/// <summary>No access to an SMTP host.</summary>
		// Token: 0x0400286A RID: 10346
		None,
		/// <summary>Connection to an SMTP host on the default port (port 25).</summary>
		// Token: 0x0400286B RID: 10347
		Connect,
		/// <summary>Connection to an SMTP host on any port.</summary>
		// Token: 0x0400286C RID: 10348
		ConnectToUnrestrictedPort
	}
}
