using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception that is thrown by the marshaler when it encounters a <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> it does not support.</summary>
	// Token: 0x020006C7 RID: 1735
	[Serializable]
	public class MarshalDirectiveException : SystemException
	{
		/// <summary>Initializes a new instance of the <see langword="MarshalDirectiveException" /> class with default properties.</summary>
		// Token: 0x06003FDC RID: 16348 RVA: 0x000DFE53 File Offset: 0x000DE053
		public MarshalDirectiveException() : base("Marshaling directives are invalid.")
		{
			base.HResult = -2146233035;
		}

		/// <summary>Initializes a new instance of the <see langword="MarshalDirectiveException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that specifies the reason for the exception.</param>
		// Token: 0x06003FDD RID: 16349 RVA: 0x000DFE6B File Offset: 0x000DE06B
		public MarshalDirectiveException(string message) : base(message)
		{
			base.HResult = -2146233035;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.MarshalDirectiveException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06003FDE RID: 16350 RVA: 0x000DFE7F File Offset: 0x000DE07F
		public MarshalDirectiveException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233035;
		}

		/// <summary>Initializes a new instance of the <see langword="MarshalDirectiveException" /> class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06003FDF RID: 16351 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected MarshalDirectiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
