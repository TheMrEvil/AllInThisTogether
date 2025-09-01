using System;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when a floating-point value is positive infinity, negative infinity, or Not-a-Number (NaN).</summary>
	// Token: 0x0200015F RID: 351
	[Serializable]
	public class NotFiniteNumberException : ArithmeticException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class.</summary>
		// Token: 0x06000DD3 RID: 3539 RVA: 0x00035E7D File Offset: 0x0003407D
		public NotFiniteNumberException() : base("Arg_NotFiniteNumberException = Number encountered was not a finite quantity.")
		{
			this._offendingNumber = 0.0;
			base.HResult = -2146233048;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with the invalid number.</summary>
		/// <param name="offendingNumber">The value of the argument that caused the exception.</param>
		// Token: 0x06000DD4 RID: 3540 RVA: 0x00035EA4 File Offset: 0x000340A4
		public NotFiniteNumberException(double offendingNumber)
		{
			this._offendingNumber = offendingNumber;
			base.HResult = -2146233048;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000DD5 RID: 3541 RVA: 0x00035EBE File Offset: 0x000340BE
		public NotFiniteNumberException(string message) : base(message)
		{
			this._offendingNumber = 0.0;
			base.HResult = -2146233048;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message and the invalid number.</summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="offendingNumber">The value of the argument that caused the exception.</param>
		// Token: 0x06000DD6 RID: 3542 RVA: 0x00035EE1 File Offset: 0x000340E1
		public NotFiniteNumberException(string message, double offendingNumber) : base(message)
		{
			this._offendingNumber = offendingNumber;
			base.HResult = -2146233048;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message and a reference to the inner exception that is root cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000DD7 RID: 3543 RVA: 0x00035EFC File Offset: 0x000340FC
		public NotFiniteNumberException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233048;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with a specified error message, the invalid number, and a reference to the inner exception that is root cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="offendingNumber">The value of the argument that caused the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000DD8 RID: 3544 RVA: 0x00035F11 File Offset: 0x00034111
		public NotFiniteNumberException(string message, double offendingNumber, Exception innerException) : base(message, innerException)
		{
			this._offendingNumber = offendingNumber;
			base.HResult = -2146233048;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotFiniteNumberException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000DD9 RID: 3545 RVA: 0x00035F2D File Offset: 0x0003412D
		protected NotFiniteNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._offendingNumber = (double)info.GetInt32("OffendingNumber");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the invalid number and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is <see langword="null" />.</exception>
		// Token: 0x06000DDA RID: 3546 RVA: 0x00035F49 File Offset: 0x00034149
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("OffendingNumber", this._offendingNumber, typeof(int));
		}

		/// <summary>Gets the invalid number that is a positive infinity, a negative infinity, or Not-a-Number (NaN).</summary>
		/// <returns>The invalid number.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00035F73 File Offset: 0x00034173
		public double OffendingNumber
		{
			get
			{
				return this._offendingNumber;
			}
		}

		// Token: 0x04001284 RID: 4740
		private double _offendingNumber;
	}
}
