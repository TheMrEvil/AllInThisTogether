using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Provides an exception that is thrown when an invalid type is used with a <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
	// Token: 0x020001D2 RID: 466
	[Serializable]
	public class SettingsPropertyWrongTypeException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class.</summary>
		// Token: 0x06000C43 RID: 3139 RVA: 0x0000DC12 File Offset: 0x0000BE12
		public SettingsPropertyWrongTypeException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameter.</summary>
		/// <param name="message">A string containing an exception message.</param>
		// Token: 0x06000C44 RID: 3140 RVA: 0x0000DC2F File Offset: 0x0000BE2F
		public SettingsPropertyWrongTypeException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameters.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination of the serialized stream.</param>
		// Token: 0x06000C45 RID: 3141 RVA: 0x0002C42D File Offset: 0x0002A62D
		protected SettingsPropertyWrongTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameters.</summary>
		/// <param name="message">A string containing an exception message.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06000C46 RID: 3142 RVA: 0x000320E8 File Offset: 0x000302E8
		public SettingsPropertyWrongTypeException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
