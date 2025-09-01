using System;

namespace System.Runtime.Serialization
{
	/// <summary>The exception that is thrown when the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> or <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> encounters an invalid data contract during serialization and deserialization.</summary>
	// Token: 0x020000EA RID: 234
	[Serializable]
	public class InvalidDataContractException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.InvalidDataContractException" /> class.</summary>
		// Token: 0x06000D52 RID: 3410 RVA: 0x0003543B File Offset: 0x0003363B
		public InvalidDataContractException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.InvalidDataContractException" /> class with the specified error message.</summary>
		/// <param name="message">A description of the error.</param>
		// Token: 0x06000D53 RID: 3411 RVA: 0x00035443 File Offset: 0x00033643
		public InvalidDataContractException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.InvalidDataContractException" /> class with the specified error message and inner exception.</summary>
		/// <param name="message">A description of the error.</param>
		/// <param name="innerException">The original <see cref="T:System.Exception" />.</param>
		// Token: 0x06000D54 RID: 3412 RVA: 0x0003544C File Offset: 0x0003364C
		public InvalidDataContractException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.InvalidDataContractException" /> class with the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains data needed to serialize and deserialize an object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies user context during serialization and deserialization.</param>
		// Token: 0x06000D55 RID: 3413 RVA: 0x00035456 File Offset: 0x00033656
		protected InvalidDataContractException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
