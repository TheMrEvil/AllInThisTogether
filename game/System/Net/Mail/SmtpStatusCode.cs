using System;

namespace System.Net.Mail
{
	/// <summary>Specifies the outcome of sending email by using the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
	// Token: 0x0200083E RID: 2110
	public enum SmtpStatusCode
	{
		/// <summary>The commands were sent in the incorrect sequence.</summary>
		// Token: 0x040028AC RID: 10412
		BadCommandSequence = 503,
		/// <summary>The specified user is not local, but the receiving SMTP service accepted the message and attempted to deliver it. This status code is defined in RFC 1123, which is available at https://www.ietf.org.</summary>
		// Token: 0x040028AD RID: 10413
		CannotVerifyUserWillAttemptDelivery = 252,
		/// <summary>The client was not authenticated or is not allowed to send mail using the specified SMTP host.</summary>
		// Token: 0x040028AE RID: 10414
		ClientNotPermitted = 454,
		/// <summary>The SMTP service does not implement the specified command.</summary>
		// Token: 0x040028AF RID: 10415
		CommandNotImplemented = 502,
		/// <summary>The SMTP service does not implement the specified command parameter.</summary>
		// Token: 0x040028B0 RID: 10416
		CommandParameterNotImplemented = 504,
		/// <summary>The SMTP service does not recognize the specified command.</summary>
		// Token: 0x040028B1 RID: 10417
		CommandUnrecognized = 500,
		/// <summary>The message is too large to be stored in the destination mailbox.</summary>
		// Token: 0x040028B2 RID: 10418
		ExceededStorageAllocation = 552,
		/// <summary>The transaction could not occur. You receive this error when the specified SMTP host cannot be found.</summary>
		// Token: 0x040028B3 RID: 10419
		GeneralFailure = -1,
		/// <summary>A Help message was returned by the service.</summary>
		// Token: 0x040028B4 RID: 10420
		HelpMessage = 214,
		/// <summary>The SMTP service does not have sufficient storage to complete the request.</summary>
		// Token: 0x040028B5 RID: 10421
		InsufficientStorage = 452,
		/// <summary>The SMTP service cannot complete the request. This error can occur if the client's IP address cannot be resolved (that is, a reverse lookup failed). You can also receive this error if the client domain has been identified as an open relay or source for unsolicited email (spam). For details, see RFC 2505, which is available at https://www.ietf.org.</summary>
		// Token: 0x040028B6 RID: 10422
		LocalErrorInProcessing = 451,
		/// <summary>The destination mailbox is in use.</summary>
		// Token: 0x040028B7 RID: 10423
		MailboxBusy = 450,
		/// <summary>The syntax used to specify the destination mailbox is incorrect.</summary>
		// Token: 0x040028B8 RID: 10424
		MailboxNameNotAllowed = 553,
		/// <summary>The destination mailbox was not found or could not be accessed.</summary>
		// Token: 0x040028B9 RID: 10425
		MailboxUnavailable = 550,
		/// <summary>The email was successfully sent to the SMTP service.</summary>
		// Token: 0x040028BA RID: 10426
		Ok = 250,
		/// <summary>The SMTP service is closing the transmission channel.</summary>
		// Token: 0x040028BB RID: 10427
		ServiceClosingTransmissionChannel = 221,
		/// <summary>The SMTP service is not available; the server is closing the transmission channel.</summary>
		// Token: 0x040028BC RID: 10428
		ServiceNotAvailable = 421,
		/// <summary>The SMTP service is ready.</summary>
		// Token: 0x040028BD RID: 10429
		ServiceReady = 220,
		/// <summary>The SMTP service is ready to receive the email content.</summary>
		// Token: 0x040028BE RID: 10430
		StartMailInput = 354,
		/// <summary>The syntax used to specify a command or parameter is incorrect.</summary>
		// Token: 0x040028BF RID: 10431
		SyntaxError = 501,
		/// <summary>A system status or system Help reply.</summary>
		// Token: 0x040028C0 RID: 10432
		SystemStatus = 211,
		/// <summary>The transaction failed.</summary>
		// Token: 0x040028C1 RID: 10433
		TransactionFailed = 554,
		/// <summary>The user mailbox is not located on the receiving server. You should resend using the supplied address information.</summary>
		// Token: 0x040028C2 RID: 10434
		UserNotLocalTryAlternatePath = 551,
		/// <summary>The user mailbox is not located on the receiving server; the server forwards the email.</summary>
		// Token: 0x040028C3 RID: 10435
		UserNotLocalWillForward = 251,
		/// <summary>The SMTP server is configured to accept only TLS connections, and the SMTP client is attempting to connect by using a non-TLS connection. The solution is for the user to set EnableSsl=true on the SMTP Client.</summary>
		// Token: 0x040028C4 RID: 10436
		MustIssueStartTlsFirst = 530
	}
}
