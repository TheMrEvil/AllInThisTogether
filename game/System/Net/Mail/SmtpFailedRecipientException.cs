using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Represents the exception that is thrown when the <see cref="T:System.Net.Mail.SmtpClient" /> is not able to complete a <see cref="Overload:System.Net.Mail.SmtpClient.Send" /> or <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> operation to a particular recipient.</summary>
	// Token: 0x0200083A RID: 2106
	[Serializable]
	public class SmtpFailedRecipientException : SmtpException, ISerializable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class.</summary>
		// Token: 0x06004315 RID: 17173 RVA: 0x000E9EDE File Offset: 0x000E80DE
		public SmtpFailedRecipientException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class with the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains the error message.</param>
		// Token: 0x06004316 RID: 17174 RVA: 0x000E9EE6 File Offset: 0x000E80E6
		public SmtpFailedRecipientException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream that is associated with the new instance.</param>
		// Token: 0x06004317 RID: 17175 RVA: 0x000E9EEF File Offset: 0x000E80EF
		protected SmtpFailedRecipientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.failedRecipient = info.GetString("failedRecipient");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class with the specified status code and email address.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		/// <param name="failedRecipient">A <see cref="T:System.String" /> that contains the email address.</param>
		// Token: 0x06004318 RID: 17176 RVA: 0x000E9F18 File Offset: 0x000E8118
		public SmtpFailedRecipientException(SmtpStatusCode statusCode, string failedRecipient) : base(statusCode)
		{
			this.failedRecipient = failedRecipient;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06004319 RID: 17177 RVA: 0x000E9F28 File Offset: 0x000E8128
		public SmtpFailedRecipientException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message, email address, and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		/// <param name="failedRecipient">A <see cref="T:System.String" /> that contains the email address.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x0600431A RID: 17178 RVA: 0x000E9F32 File Offset: 0x000E8132
		public SmtpFailedRecipientException(string message, string failedRecipient, Exception innerException) : base(message, innerException)
		{
			this.failedRecipient = failedRecipient;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class with the specified status code, email address, and server response.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		/// <param name="failedRecipient">A <see cref="T:System.String" /> that contains the email address.</param>
		/// <param name="serverResponse">A <see cref="T:System.String" /> that contains the server response.</param>
		// Token: 0x0600431B RID: 17179 RVA: 0x000E9F43 File Offset: 0x000E8143
		public SmtpFailedRecipientException(SmtpStatusCode statusCode, string failedRecipient, string serverResponse) : base(statusCode, serverResponse)
		{
			this.failedRecipient = failedRecipient;
		}

		/// <summary>Indicates the email address with delivery difficulties.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the email address.</returns>
		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x000E9F54 File Offset: 0x000E8154
		public string FailedRecipient
		{
			get
			{
				return this.failedRecipient;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x0600431D RID: 17181 RVA: 0x000E9F5C File Offset: 0x000E815C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (serializationInfo == null)
			{
				throw new ArgumentNullException("serializationInfo");
			}
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("failedRecipient", this.failedRecipient);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance, which holds the serialized data for the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> instance that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</param>
		// Token: 0x0600431E RID: 17182 RVA: 0x000A7812 File Offset: 0x000A5A12
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x040028A5 RID: 10405
		private string failedRecipient;
	}
}
