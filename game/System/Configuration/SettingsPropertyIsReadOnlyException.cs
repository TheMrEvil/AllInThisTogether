using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Provides an exception for read-only <see cref="T:System.Configuration.SettingsProperty" /> objects.</summary>
	// Token: 0x020001CE RID: 462
	[Serializable]
	public class SettingsPropertyIsReadOnlyException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class.</summary>
		// Token: 0x06000C1F RID: 3103 RVA: 0x0000DC12 File Offset: 0x0000BE12
		public SettingsPropertyIsReadOnlyException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class based on a supplied parameter.</summary>
		/// <param name="message">A string containing an exception message.</param>
		// Token: 0x06000C20 RID: 3104 RVA: 0x0000DC2F File Offset: 0x0000BE2F
		public SettingsPropertyIsReadOnlyException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class based on the supplied parameters.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination of the serialized stream.</param>
		// Token: 0x06000C21 RID: 3105 RVA: 0x0002C42D File Offset: 0x0002A62D
		protected SettingsPropertyIsReadOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class based on supplied parameters.</summary>
		/// <param name="message">A string containing an exception message.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06000C22 RID: 3106 RVA: 0x000320E8 File Offset: 0x000302E8
		public SettingsPropertyIsReadOnlyException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
