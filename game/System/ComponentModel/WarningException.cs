using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Specifies an exception that is handled as a warning instead of an error.</summary>
	// Token: 0x020003FA RID: 1018
	[Serializable]
	public class WarningException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class.</summary>
		// Token: 0x0600212C RID: 8492 RVA: 0x00072085 File Offset: 0x00070285
		public WarningException() : this(null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message and no Help file.</summary>
		/// <param name="message">The message to display to the end user.</param>
		// Token: 0x0600212D RID: 8493 RVA: 0x00072090 File Offset: 0x00070290
		public WarningException(string message) : this(message, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message, and with access to the specified Help file.</summary>
		/// <param name="message">The message to display to the end user.</param>
		/// <param name="helpUrl">The Help file to display if the user requests help.</param>
		// Token: 0x0600212E RID: 8494 RVA: 0x0007209B File Offset: 0x0007029B
		public WarningException(string message, string helpUrl) : this(message, helpUrl, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified detailed description and the specified exception.</summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x0600212F RID: 8495 RVA: 0x0002F191 File Offset: 0x0002D391
		public WarningException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message, and with access to the specified Help file and topic.</summary>
		/// <param name="message">The message to display to the end user.</param>
		/// <param name="helpUrl">The Help file to display if the user requests help.</param>
		/// <param name="helpTopic">The Help topic to display if the user requests help.</param>
		// Token: 0x06002130 RID: 8496 RVA: 0x000720A6 File Offset: 0x000702A6
		public WarningException(string message, string helpUrl, string helpTopic) : base(message)
		{
			this.HelpUrl = helpUrl;
			this.HelpTopic = helpTopic;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06002131 RID: 8497 RVA: 0x000720C0 File Offset: 0x000702C0
		protected WarningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.HelpUrl = (string)info.GetValue("helpUrl", typeof(string));
			this.HelpTopic = (string)info.GetValue("helpTopic", typeof(string));
		}

		/// <summary>Gets the Help file associated with the warning.</summary>
		/// <returns>The Help file associated with the warning.</returns>
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x00072115 File Offset: 0x00070315
		public string HelpUrl
		{
			[CompilerGenerated]
			get
			{
				return this.<HelpUrl>k__BackingField;
			}
		}

		/// <summary>Gets the Help topic associated with the warning.</summary>
		/// <returns>The Help topic associated with the warning.</returns>
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x0007211D File Offset: 0x0007031D
		public string HelpTopic
		{
			[CompilerGenerated]
			get
			{
				return this.<HelpTopic>k__BackingField;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the parameter name and additional exception information.</summary>
		/// <param name="info">Stores the data that was being used to serialize or deserialize the object that the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> was serializing or deserializing.</param>
		/// <param name="context">Describes the source and destination of the stream that generated the exception, as well as a means for serialization to retain that context and an additional caller-defined context.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06002134 RID: 8500 RVA: 0x00072125 File Offset: 0x00070325
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("helpUrl", this.HelpUrl);
			info.AddValue("helpTopic", this.HelpTopic);
		}

		// Token: 0x04000FF8 RID: 4088
		[CompilerGenerated]
		private readonly string <HelpUrl>k__BackingField;

		// Token: 0x04000FF9 RID: 4089
		[CompilerGenerated]
		private readonly string <HelpTopic>k__BackingField;
	}
}
