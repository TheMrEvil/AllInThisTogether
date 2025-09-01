using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a <see langword="VT_ERROR" />.</summary>
	// Token: 0x020006D6 RID: 1750
	public sealed class ErrorWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> class with the HRESULT of the error.</summary>
		/// <param name="errorCode">The HRESULT of the error.</param>
		// Token: 0x0600402E RID: 16430 RVA: 0x000E0C69 File Offset: 0x000DEE69
		public ErrorWrapper(int errorCode)
		{
			this.m_ErrorCode = errorCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> class with an object containing the HRESULT of the error.</summary>
		/// <param name="errorCode">The object containing the HRESULT of the error.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="errorCode" /> parameter is not an <see cref="T:System.Int32" /> type.</exception>
		// Token: 0x0600402F RID: 16431 RVA: 0x000E0C78 File Offset: 0x000DEE78
		public ErrorWrapper(object errorCode)
		{
			if (!(errorCode is int))
			{
				throw new ArgumentException("Object must be of type Int32.", "errorCode");
			}
			this.m_ErrorCode = (int)errorCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> class with the HRESULT that corresponds to the exception supplied.</summary>
		/// <param name="e">The exception to be converted to an error code.</param>
		// Token: 0x06004030 RID: 16432 RVA: 0x000E0CA4 File Offset: 0x000DEEA4
		public ErrorWrapper(Exception e)
		{
			this.m_ErrorCode = Marshal.GetHRForException(e);
		}

		/// <summary>Gets the error code of the wrapper.</summary>
		/// <returns>The HRESULT of the error.</returns>
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06004031 RID: 16433 RVA: 0x000E0CB8 File Offset: 0x000DEEB8
		public int ErrorCode
		{
			get
			{
				return this.m_ErrorCode;
			}
		}

		// Token: 0x04002A20 RID: 10784
		private int m_ErrorCode;
	}
}
