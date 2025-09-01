using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when binding to a member results in more than one member matching the binding criteria. This class cannot be inherited.</summary>
	// Token: 0x0200087C RID: 2172
	[Serializable]
	public sealed class AmbiguousMatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with an empty message string and the root cause exception set to <see langword="null" />.</summary>
		// Token: 0x0600484A RID: 18506 RVA: 0x000EDF94 File Offset: 0x000EC194
		public AmbiguousMatchException() : base("Ambiguous match found.")
		{
			base.HResult = -2147475171;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with its message string set to the given message and the root cause exception set to <see langword="null" />.</summary>
		/// <param name="message">A string indicating the reason this exception was thrown.</param>
		// Token: 0x0600484B RID: 18507 RVA: 0x000EDFAC File Offset: 0x000EC1AC
		public AmbiguousMatchException(string message) : base(message)
		{
			base.HResult = -2147475171;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600484C RID: 18508 RVA: 0x000EDFC0 File Offset: 0x000EC1C0
		public AmbiguousMatchException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147475171;
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x00020A65 File Offset: 0x0001EC65
		internal AmbiguousMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
