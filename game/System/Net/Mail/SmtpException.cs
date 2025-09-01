using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Represents the exception that is thrown when the <see cref="T:System.Net.Mail.SmtpClient" /> is not able to complete a <see cref="Overload:System.Net.Mail.SmtpClient.Send" /> or <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> operation.</summary>
	// Token: 0x02000839 RID: 2105
	[Serializable]
	public class SmtpException : Exception, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class.</summary>
		// Token: 0x0600430B RID: 17163 RVA: 0x000E9DE6 File Offset: 0x000E7FE6
		public SmtpException() : this(SmtpStatusCode.GeneralFailure)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified status code.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		// Token: 0x0600430C RID: 17164 RVA: 0x000E9DEF File Offset: 0x000E7FEF
		public SmtpException(SmtpStatusCode statusCode) : this(statusCode, "Syntax error, command unrecognized.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		// Token: 0x0600430D RID: 17165 RVA: 0x000E9DFD File Offset: 0x000E7FFD
		public SmtpException(string message) : this(SmtpStatusCode.GeneralFailure, message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpException" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream associated with the new instance.</param>
		// Token: 0x0600430E RID: 17166 RVA: 0x000E9E08 File Offset: 0x000E8008
		protected SmtpException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
			try
			{
				this.statusCode = (SmtpStatusCode)serializationInfo.GetValue("Status", typeof(int));
			}
			catch (SerializationException)
			{
				this.statusCode = (SmtpStatusCode)serializationInfo.GetValue("statusCode", typeof(SmtpStatusCode));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified status code and error message.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		// Token: 0x0600430F RID: 17167 RVA: 0x000E9E74 File Offset: 0x000E8074
		public SmtpException(SmtpStatusCode statusCode, string message) : base(message)
		{
			this.statusCode = statusCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06004310 RID: 17168 RVA: 0x000E9E84 File Offset: 0x000E8084
		public SmtpException(string message, Exception innerException) : base(message, innerException)
		{
			this.statusCode = SmtpStatusCode.GeneralFailure;
		}

		/// <summary>Gets the status code returned by an SMTP server when an email message is transmitted.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value that indicates the error that occurred.</returns>
		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06004311 RID: 17169 RVA: 0x000E9E95 File Offset: 0x000E8095
		// (set) Token: 0x06004312 RID: 17170 RVA: 0x000E9E9D File Offset: 0x000E809D
		public SmtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				this.statusCode = value;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.Mail.SmtpException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06004313 RID: 17171 RVA: 0x000E9EA6 File Offset: 0x000E80A6
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (serializationInfo == null)
			{
				throw new ArgumentNullException("serializationInfo");
			}
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("Status", this.statusCode, typeof(int));
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.Mail.SmtpException" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" />, which holds the serialized data for the <see cref="T:System.Net.Mail.SmtpException" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream associated with the new <see cref="T:System.Net.Mail.SmtpException" />.</param>
		// Token: 0x06004314 RID: 17172 RVA: 0x000A7812 File Offset: 0x000A5A12
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		// Token: 0x040028A4 RID: 10404
		private SmtpStatusCode statusCode;
	}
}
