using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO.IsolatedStorage
{
	/// <summary>The exception that is thrown when an operation in isolated storage fails.</summary>
	// Token: 0x02000B72 RID: 2930
	[ComVisible(true)]
	[Serializable]
	public class IsolatedStorageException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with default properties.</summary>
		// Token: 0x06006A89 RID: 27273 RVA: 0x0016C97C File Offset: 0x0016AB7C
		public IsolatedStorageException() : base(Locale.GetText("An Isolated storage operation failed."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06006A8A RID: 27274 RVA: 0x000328A2 File Offset: 0x00030AA2
		public IsolatedStorageException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06006A8B RID: 27275 RVA: 0x000328AB File Offset: 0x00030AAB
		public IsolatedStorageException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06006A8C RID: 27276 RVA: 0x00020FA7 File Offset: 0x0001F1A7
		protected IsolatedStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
