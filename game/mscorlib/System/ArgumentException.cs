using System;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when one of the arguments provided to a method is not valid.</summary>
	// Token: 0x020000F3 RID: 243
	[Serializable]
	public class ArgumentException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class.</summary>
		// Token: 0x060006FE RID: 1790 RVA: 0x00020FB1 File Offset: 0x0001F1B1
		public ArgumentException() : base("Value does not fall within the expected range.")
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x060006FF RID: 1791 RVA: 0x00020FC9 File Offset: 0x0001F1C9
		public ArgumentException(string message) : base(message)
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000700 RID: 1792 RVA: 0x00020FDD File Offset: 0x0001F1DD
		public ArgumentException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message, the parameter name, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="paramName">The name of the parameter that caused the current exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000701 RID: 1793 RVA: 0x00020FF2 File Offset: 0x0001F1F2
		public ArgumentException(string message, string paramName, Exception innerException) : base(message, innerException)
		{
			this._paramName = paramName;
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="paramName">The name of the parameter that caused the current exception.</param>
		// Token: 0x06000702 RID: 1794 RVA: 0x0002100E File Offset: 0x0001F20E
		public ArgumentException(string message, string paramName) : base(message)
		{
			this._paramName = paramName;
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000703 RID: 1795 RVA: 0x00021029 File Offset: 0x0001F229
		protected ArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._paramName = info.GetString("ParamName");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
		// Token: 0x06000704 RID: 1796 RVA: 0x00021044 File Offset: 0x0001F244
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ParamName", this._paramName, typeof(string));
		}

		/// <summary>Gets the error message and the parameter name, or only the error message if no parameter name is set.</summary>
		/// <returns>A text string describing the details of the exception. The value of this property takes one of two forms:  
		///   Condition  
		///
		///   Value  
		///
		///   The <paramref name="paramName" /> is a null reference (<see langword="Nothing" /> in Visual Basic) or of zero length.  
		///
		///   The <paramref name="message" /> string passed to the constructor.  
		///
		///   The <paramref name="paramName" /> is not null reference (<see langword="Nothing" /> in Visual Basic) and it has a length greater than zero.  
		///
		///   The <paramref name="message" /> string appended with the name of the invalid parameter.</returns>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0002106C File Offset: 0x0001F26C
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (!string.IsNullOrEmpty(this._paramName))
				{
					string str = SR.Format("Parameter name: {0}", this._paramName);
					return message + Environment.NewLine + str;
				}
				return message;
			}
		}

		/// <summary>Gets the name of the parameter that causes this exception.</summary>
		/// <returns>The parameter name.</returns>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x000210AC File Offset: 0x0001F2AC
		public virtual string ParamName
		{
			get
			{
				return this._paramName;
			}
		}

		// Token: 0x04001048 RID: 4168
		private string _paramName;
	}
}
