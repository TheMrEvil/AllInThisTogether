using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The base exception type for all COM interop exceptions and structured exception handling (SEH) exceptions.</summary>
	// Token: 0x020006C4 RID: 1732
	[Serializable]
	public class ExternalException : SystemException
	{
		/// <summary>Initializes a new instance of the <see langword="ExternalException" /> class with default properties.</summary>
		// Token: 0x06003FD0 RID: 16336 RVA: 0x000DFD54 File Offset: 0x000DDF54
		public ExternalException() : base("External component has thrown an exception.")
		{
			base.HResult = -2147467259;
		}

		/// <summary>Initializes a new instance of the <see langword="ExternalException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that specifies the reason for the exception.</param>
		// Token: 0x06003FD1 RID: 16337 RVA: 0x000DFD6C File Offset: 0x000DDF6C
		public ExternalException(string message) : base(message)
		{
			base.HResult = -2147467259;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ExternalException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06003FD2 RID: 16338 RVA: 0x000DFD80 File Offset: 0x000DDF80
		public ExternalException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2147467259;
		}

		/// <summary>Initializes a new instance of the <see langword="ExternalException" /> class with a specified error message and the HRESULT of the error.</summary>
		/// <param name="message">The error message that specifies the reason for the exception.</param>
		/// <param name="errorCode">The HRESULT of the error.</param>
		// Token: 0x06003FD3 RID: 16339 RVA: 0x00032810 File Offset: 0x00030A10
		public ExternalException(string message, int errorCode) : base(message)
		{
			base.HResult = errorCode;
		}

		/// <summary>Initializes a new instance of the <see langword="ExternalException" /> class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06003FD4 RID: 16340 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected ExternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Gets the <see langword="HRESULT" /> of the error.</summary>
		/// <returns>The <see langword="HRESULT" /> of the error.</returns>
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x000DFD95 File Offset: 0x000DDF95
		public virtual int ErrorCode
		{
			get
			{
				return base.HResult;
			}
		}

		/// <summary>Returns a string that contains the HRESULT of the error.</summary>
		/// <returns>A string that represents the HRESULT.</returns>
		// Token: 0x06003FD6 RID: 16342 RVA: 0x000DFDA0 File Offset: 0x000DDFA0
		public override string ToString()
		{
			string message = this.Message;
			string text = base.GetType().ToString() + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (!string.IsNullOrEmpty(message))
			{
				text = text + ": " + message;
			}
			Exception innerException = base.InnerException;
			if (innerException != null)
			{
				text = text + " ---> " + innerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			return text;
		}
	}
}
