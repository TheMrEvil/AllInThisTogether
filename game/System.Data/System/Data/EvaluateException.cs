using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when the <see cref="P:System.Data.DataColumn.Expression" /> property of a <see cref="T:System.Data.DataColumn" /> cannot be evaluated.</summary>
	// Token: 0x020000F2 RID: 242
	[Serializable]
	public class EvaluateException : InvalidExpressionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object.</param>
		/// <param name="context">The source and destination of a particular serialized stream.</param>
		// Token: 0x06000E5E RID: 3678 RVA: 0x0003CC16 File Offset: 0x0003AE16
		protected EvaluateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class.</summary>
		// Token: 0x06000E5F RID: 3679 RVA: 0x0003CC20 File Offset: 0x0003AE20
		public EvaluateException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown.</param>
		// Token: 0x06000E60 RID: 3680 RVA: 0x0003CC28 File Offset: 0x0003AE28
		public EvaluateException(string s) : base(s)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x06000E61 RID: 3681 RVA: 0x0003CC31 File Offset: 0x0003AE31
		public EvaluateException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
