using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls the marshaling behavior of a delegate signature passed as an unmanaged function pointer to or from unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x020006E2 RID: 1762
	[AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	public sealed class UnmanagedFunctionPointerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute" /> class with the specified calling convention.</summary>
		/// <param name="callingConvention">The specified calling convention.</param>
		// Token: 0x0600405B RID: 16475 RVA: 0x000E0F02 File Offset: 0x000DF102
		public UnmanagedFunctionPointerAttribute(CallingConvention callingConvention)
		{
			this.m_callingConvention = callingConvention;
		}

		/// <summary>Gets the value of the calling convention.</summary>
		/// <returns>The value of the calling convention specified by the <see cref="M:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute.#ctor(System.Runtime.InteropServices.CallingConvention)" /> constructor.</returns>
		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x0600405C RID: 16476 RVA: 0x000E0F11 File Offset: 0x000DF111
		public CallingConvention CallingConvention
		{
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x04002A26 RID: 10790
		private CallingConvention m_callingConvention;

		/// <summary>Indicates how to marshal string parameters to the method, and controls name mangling.</summary>
		// Token: 0x04002A27 RID: 10791
		public CharSet CharSet;

		/// <summary>Enables or disables best-fit mapping behavior when converting Unicode characters to ANSI characters.</summary>
		// Token: 0x04002A28 RID: 10792
		public bool BestFitMapping;

		/// <summary>Enables or disables the throwing of an exception on an unmappable Unicode character that is converted to an ANSI "?" character.</summary>
		// Token: 0x04002A29 RID: 10793
		public bool ThrowOnUnmappableChar;

		/// <summary>Indicates whether the callee calls the <see langword="SetLastError" /> Win32 API function before returning from the attributed method.</summary>
		// Token: 0x04002A2A RID: 10794
		public bool SetLastError;
	}
}
