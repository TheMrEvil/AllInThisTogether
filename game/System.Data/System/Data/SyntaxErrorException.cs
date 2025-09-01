using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when the <see cref="P:System.Data.DataColumn.Expression" /> property of a <see cref="T:System.Data.DataColumn" /> contains a syntax error.</summary>
	// Token: 0x020000F3 RID: 243
	[Serializable]
	public class SyntaxErrorException : InvalidExpressionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SyntaxErrorException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object.</param>
		/// <param name="context">The source and destination of a specific serialized stream.</param>
		// Token: 0x06000E62 RID: 3682 RVA: 0x0003CC16 File Offset: 0x0003AE16
		protected SyntaxErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SyntaxErrorException" /> class.</summary>
		// Token: 0x06000E63 RID: 3683 RVA: 0x0003CC20 File Offset: 0x0003AE20
		public SyntaxErrorException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SyntaxErrorException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown.</param>
		// Token: 0x06000E64 RID: 3684 RVA: 0x0003CC28 File Offset: 0x0003AE28
		public SyntaxErrorException(string s) : base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SyntaxErrorException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x06000E65 RID: 3685 RVA: 0x0003CC31 File Offset: 0x0003AE31
		public SyntaxErrorException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
