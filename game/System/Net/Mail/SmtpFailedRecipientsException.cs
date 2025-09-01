using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>The exception that is thrown when email is sent using an <see cref="T:System.Net.Mail.SmtpClient" /> and cannot be delivered to all recipients.</summary>
	// Token: 0x0200083B RID: 2107
	[Serializable]
	public class SmtpFailedRecipientsException : SmtpFailedRecipientException, ISerializable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class.</summary>
		// Token: 0x0600431F RID: 17183 RVA: 0x000E9F85 File Offset: 0x000E8185
		public SmtpFailedRecipientsException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" />.</summary>
		/// <param name="message">The exception message.</param>
		// Token: 0x06004320 RID: 17184 RVA: 0x000E9F8D File Offset: 0x000E818D
		public SmtpFailedRecipientsException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" /> and inner <see cref="T:System.Exception" />.</summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerException">The inner exception.</param>
		// Token: 0x06004321 RID: 17185 RVA: 0x000E9F96 File Offset: 0x000E8196
		public SmtpFailedRecipientsException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" /> and array of type <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerExceptions">The array of recipients with delivery errors.</param>
		// Token: 0x06004322 RID: 17186 RVA: 0x000E9FA0 File Offset: 0x000E81A0
		public SmtpFailedRecipientsException(string message, SmtpFailedRecipientException[] innerExceptions) : base(message)
		{
			this.innerExceptions = innerExceptions;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> instance.</param>
		// Token: 0x06004323 RID: 17187 RVA: 0x000E9FB0 File Offset: 0x000E81B0
		protected SmtpFailedRecipientsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.innerExceptions = (SmtpFailedRecipientException[])info.GetValue("innerExceptions", typeof(SmtpFailedRecipientException[]));
		}

		/// <summary>Gets one or more <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />s that indicate the email recipients with SMTP delivery errors.</summary>
		/// <returns>An array of type <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> that lists the recipients with delivery errors.</returns>
		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06004324 RID: 17188 RVA: 0x000E9FE8 File Offset: 0x000E81E8
		public SmtpFailedRecipientException[] InnerExceptions
		{
			get
			{
				return this.innerExceptions;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used.</param>
		// Token: 0x06004325 RID: 17189 RVA: 0x000E9FF0 File Offset: 0x000E81F0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (serializationInfo == null)
			{
				throw new ArgumentNullException("serializationInfo");
			}
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("innerExceptions", this.innerExceptions);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />.</param>
		// Token: 0x06004326 RID: 17190 RVA: 0x000A7812 File Offset: 0x000A5A12
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		// Token: 0x040028A6 RID: 10406
		private SmtpFailedRecipientException[] innerExceptions;
	}
}
