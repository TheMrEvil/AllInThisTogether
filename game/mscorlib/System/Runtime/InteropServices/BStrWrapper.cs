using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Marshals data of type <see langword="VT_BSTR" /> from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x020006D2 RID: 1746
	public sealed class BStrWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.BStrWrapper" /> class with the specified <see cref="T:System.String" /> object.</summary>
		/// <param name="value">The object to wrap and marshal as <see langword="VT_BSTR" />.</param>
		// Token: 0x06004028 RID: 16424 RVA: 0x000E0BFB File Offset: 0x000DEDFB
		public BStrWrapper(string value)
		{
			this.m_WrappedObject = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.BStrWrapper" /> class with the specified <see cref="T:System.Object" /> object.</summary>
		/// <param name="value">The object to wrap and marshal as <see langword="VT_BSTR" />.</param>
		// Token: 0x06004029 RID: 16425 RVA: 0x000E0C0A File Offset: 0x000DEE0A
		public BStrWrapper(object value)
		{
			this.m_WrappedObject = (string)value;
		}

		/// <summary>Gets the wrapped <see cref="T:System.String" /> object to marshal as type <see langword="VT_BSTR" />.</summary>
		/// <returns>The object that is wrapped by <see cref="T:System.Runtime.InteropServices.BStrWrapper" />.</returns>
		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600402A RID: 16426 RVA: 0x000E0C1E File Offset: 0x000DEE1E
		public string WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002A17 RID: 10775
		private string m_WrappedObject;
	}
}
