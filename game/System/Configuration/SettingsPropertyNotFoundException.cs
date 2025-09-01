using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Provides an exception for <see cref="T:System.Configuration.SettingsProperty" /> objects that are not found.</summary>
	// Token: 0x020001CF RID: 463
	[Serializable]
	public class SettingsPropertyNotFoundException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyNotFoundException" /> class.</summary>
		// Token: 0x06000C23 RID: 3107 RVA: 0x0000DC12 File Offset: 0x0000BE12
		public SettingsPropertyNotFoundException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyNotFoundException" /> class, based on a supplied parameter.</summary>
		/// <param name="message">A string containing an exception message.</param>
		// Token: 0x06000C24 RID: 3108 RVA: 0x0000DC2F File Offset: 0x0000BE2F
		public SettingsPropertyNotFoundException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyNotFoundException" /> class, based on supplied parameters.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination of the serialized stream.</param>
		// Token: 0x06000C25 RID: 3109 RVA: 0x0002C42D File Offset: 0x0002A62D
		protected SettingsPropertyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyNotFoundException" /> class, based on supplied parameters.</summary>
		/// <param name="message">A string containing an exception message.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06000C26 RID: 3110 RVA: 0x000320E8 File Offset: 0x000302E8
		public SettingsPropertyNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
