using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a method attempts to use a type that it does not have access to.</summary>
	// Token: 0x020001A2 RID: 418
	[Serializable]
	public class TypeAccessException : TypeLoadException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TypeAccessException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x060011E5 RID: 4581 RVA: 0x00047C3F File Offset: 0x00045E3F
		public TypeAccessException() : base("Attempt to access the type failed.")
		{
			base.HResult = -2146233021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeAccessException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor must ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060011E6 RID: 4582 RVA: 0x00047C57 File Offset: 0x00045E57
		public TypeAccessException(string message) : base(message)
		{
			base.HResult = -2146233021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor must ensure that this string has been localized for the current system culture.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060011E7 RID: 4583 RVA: 0x00047C6B File Offset: 0x00045E6B
		public TypeAccessException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060011E8 RID: 4584 RVA: 0x0002824E File Offset: 0x0002644E
		protected TypeAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
