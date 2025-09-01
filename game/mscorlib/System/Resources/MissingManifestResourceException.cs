using System;
using System.Runtime.Serialization;

namespace System.Resources
{
	/// <summary>The exception that is thrown if the main assembly does not contain the resources for the neutral culture, and an appropriate satellite assembly is missing.</summary>
	// Token: 0x0200085A RID: 2138
	[Serializable]
	public class MissingManifestResourceException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class with default properties.</summary>
		// Token: 0x06004736 RID: 18230 RVA: 0x000E8047 File Offset: 0x000E6247
		public MissingManifestResourceException() : base("Unable to find manifest resource.")
		{
			base.HResult = -2146233038;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class with the specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06004737 RID: 18231 RVA: 0x000E805F File Offset: 0x000E625F
		public MissingManifestResourceException(string message) : base(message)
		{
			base.HResult = -2146233038;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06004738 RID: 18232 RVA: 0x000E8073 File Offset: 0x000E6273
		public MissingManifestResourceException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233038;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination of the exception.</param>
		// Token: 0x06004739 RID: 18233 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected MissingManifestResourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
