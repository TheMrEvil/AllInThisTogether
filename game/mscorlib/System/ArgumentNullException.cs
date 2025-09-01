using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a null reference (<see langword="Nothing" /> in Visual Basic) is passed to a method that does not accept it as a valid argument.</summary>
	// Token: 0x020000F4 RID: 244
	[Serializable]
	public class ArgumentNullException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class.</summary>
		// Token: 0x06000707 RID: 1799 RVA: 0x000210B4 File Offset: 0x0001F2B4
		public ArgumentNullException() : base("Value cannot be null.")
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception.</param>
		// Token: 0x06000708 RID: 1800 RVA: 0x000210CC File Offset: 0x0001F2CC
		public ArgumentNullException(string paramName) : base("Value cannot be null.", paramName)
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with a specified error message and the exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for this exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x06000709 RID: 1801 RVA: 0x000210E5 File Offset: 0x0001F2E5
		public ArgumentNullException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes an instance of the <see cref="T:System.ArgumentNullException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception.</param>
		/// <param name="message">A message that describes the error.</param>
		// Token: 0x0600070A RID: 1802 RVA: 0x000210FA File Offset: 0x0001F2FA
		public ArgumentNullException(string paramName, string message) : base(message, paramName)
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">An object that describes the source or destination of the serialized data.</param>
		// Token: 0x0600070B RID: 1803 RVA: 0x0002110F File Offset: 0x0001F30F
		protected ArgumentNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
