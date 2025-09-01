using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>Represents structured exception handling (SEH) errors.</summary>
	// Token: 0x020006DC RID: 1756
	[Serializable]
	public class SEHException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SEHException" /> class.</summary>
		// Token: 0x06004041 RID: 16449 RVA: 0x000E0D42 File Offset: 0x000DEF42
		public SEHException()
		{
			base.HResult = -2147467259;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SEHException" /> class with a specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception.</param>
		// Token: 0x06004042 RID: 16450 RVA: 0x000E0D55 File Offset: 0x000DEF55
		public SEHException(string message) : base(message)
		{
			base.HResult = -2147467259;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SEHException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06004043 RID: 16451 RVA: 0x000E0D69 File Offset: 0x000DEF69
		public SEHException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147467259;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SEHException" /> class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06004044 RID: 16452 RVA: 0x000E0B57 File Offset: 0x000DED57
		protected SEHException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Indicates whether the exception can be recovered from, and whether the code can continue from the point at which the exception was thrown.</summary>
		/// <returns>Always <see langword="false" />, because resumable exceptions are not implemented.</returns>
		// Token: 0x06004045 RID: 16453 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool CanResume()
		{
			return false;
		}
	}
}
