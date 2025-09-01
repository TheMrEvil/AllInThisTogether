using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an invalid Uniform Resource Identifier (URI) is detected.</summary>
	// Token: 0x02000154 RID: 340
	[Serializable]
	public class UriFormatException : FormatException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UriFormatException" /> class.</summary>
		// Token: 0x06000979 RID: 2425 RVA: 0x00029735 File Offset: 0x00027935
		public UriFormatException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriFormatException" /> class with the specified message.</summary>
		/// <param name="textString">The error message string.</param>
		// Token: 0x0600097A RID: 2426 RVA: 0x0002973D File Offset: 0x0002793D
		public UriFormatException(string textString) : base(textString)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriFormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="textString">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="e">The exception that is the cause of the current exception. If the <c>innerException</c> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600097B RID: 2427 RVA: 0x00029746 File Offset: 0x00027946
		public UriFormatException(string textString, Exception e) : base(textString, e)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriFormatException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information that is required to serialize the new <see cref="T:System.UriFormatException" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.UriFormatException" />.</param>
		// Token: 0x0600097C RID: 2428 RVA: 0x00029750 File Offset: 0x00027950
		protected UriFormatException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize the <see cref="T:System.UriFormatException" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that will hold the serialized data for the <see cref="T:System.UriFormatException" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream that is associated with the new <see cref="T:System.UriFormatException" />.</param>
		// Token: 0x0600097D RID: 2429 RVA: 0x0002975A File Offset: 0x0002795A
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}
