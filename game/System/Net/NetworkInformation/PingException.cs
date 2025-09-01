using System;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	/// <summary>The exception that is thrown when a <see cref="Overload:System.Net.NetworkInformation.Ping.Send" /> or <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> method calls a method that throws an exception.</summary>
	// Token: 0x0200070D RID: 1805
	[Serializable]
	public class PingException : InvalidOperationException
	{
		// Token: 0x060039C6 RID: 14790 RVA: 0x000A7186 File Offset: 0x000A5386
		internal PingException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">The object that holds the serialized object data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the contextual information about the source or destination for this serialization.</param>
		// Token: 0x060039C7 RID: 14791 RVA: 0x000A7197 File Offset: 0x000A5397
		protected PingException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class using the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x060039C8 RID: 14792 RVA: 0x000A718E File Offset: 0x000A538E
		public PingException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class using the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		/// <param name="innerException">The exception that causes the current exception.</param>
		// Token: 0x060039C9 RID: 14793 RVA: 0x000C8EAF File Offset: 0x000C70AF
		public PingException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
