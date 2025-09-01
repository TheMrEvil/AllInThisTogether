using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to load a class fails due to the absence of an entry method.</summary>
	// Token: 0x02000114 RID: 276
	[Serializable]
	public class EntryPointNotFoundException : TypeLoadException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class.</summary>
		// Token: 0x06000ACB RID: 2763 RVA: 0x000287CC File Offset: 0x000269CC
		public EntryPointNotFoundException() : base("Entry point was not found.")
		{
			base.HResult = -2146233053;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000ACC RID: 2764 RVA: 0x000287E4 File Offset: 0x000269E4
		public EntryPointNotFoundException(string message) : base(message)
		{
			base.HResult = -2146233053;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000ACD RID: 2765 RVA: 0x000287F8 File Offset: 0x000269F8
		public EntryPointNotFoundException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233053;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EntryPointNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000ACE RID: 2766 RVA: 0x0002824E File Offset: 0x0002644E
		protected EntryPointNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
