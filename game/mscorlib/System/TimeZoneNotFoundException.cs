using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a time zone cannot be found.</summary>
	// Token: 0x02000194 RID: 404
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class TimeZoneNotFoundException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TimeZoneNotFoundException" /> class with a system-supplied message.</summary>
		// Token: 0x0600102B RID: 4139 RVA: 0x00004B05 File Offset: 0x00002D05
		public TimeZoneNotFoundException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeZoneNotFoundException" /> class with the specified message string.</summary>
		/// <param name="message">A string that describes the exception.</param>
		// Token: 0x0600102C RID: 4140 RVA: 0x000328A2 File Offset: 0x00030AA2
		public TimeZoneNotFoundException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeZoneNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A string that describes the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x0600102D RID: 4141 RVA: 0x000328AB File Offset: 0x00030AAB
		public TimeZoneNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeZoneNotFoundException" /> class from serialized data.</summary>
		/// <param name="info">The object that contains the serialized data.</param>
		/// <param name="context">The stream that contains the serialized data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="context" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600102E RID: 4142 RVA: 0x00020FA7 File Offset: 0x0001F1A7
		protected TimeZoneNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
