using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error is made while using a network protocol.</summary>
	// Token: 0x02000600 RID: 1536
	[Serializable]
	public class ProtocolViolationException : InvalidOperationException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.ProtocolViolationException" /> class.</summary>
		// Token: 0x06003092 RID: 12434 RVA: 0x000A7186 File Offset: 0x000A5386
		public ProtocolViolationException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.ProtocolViolationException" /> class with the specified message.</summary>
		/// <param name="message">The error message string.</param>
		// Token: 0x06003093 RID: 12435 RVA: 0x000A718E File Offset: 0x000A538E
		public ProtocolViolationException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.ProtocolViolationException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information that is required to deserialize the <see cref="T:System.Net.ProtocolViolationException" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.ProtocolViolationException" />.</param>
		// Token: 0x06003094 RID: 12436 RVA: 0x000A7197 File Offset: 0x000A5397
		protected ProtocolViolationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.ProtocolViolationException" /> will be serialized.</param>
		/// <param name="streamingContext">The destination of the serialization.</param>
		// Token: 0x06003095 RID: 12437 RVA: 0x0002975A File Offset: 0x0002795A
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06003096 RID: 12438 RVA: 0x0002975A File Offset: 0x0002795A
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}
