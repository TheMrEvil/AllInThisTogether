using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how to marshal the data between managed and unmanaged code.</summary>
	// Token: 0x0200074C RID: 1868
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class MarshalAsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value.</summary>
		/// <param name="unmanagedType">The value the data is to be marshaled as.</param>
		// Token: 0x06004234 RID: 16948 RVA: 0x000E3A6A File Offset: 0x000E1C6A
		public MarshalAsAttribute(short unmanagedType)
		{
			this.utype = (UnmanagedType)unmanagedType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> enumeration member.</summary>
		/// <param name="unmanagedType">The value the data is to be marshaled as.</param>
		// Token: 0x06004235 RID: 16949 RVA: 0x000E3A6A File Offset: 0x000E1C6A
		public MarshalAsAttribute(UnmanagedType unmanagedType)
		{
			this.utype = unmanagedType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value the data is to be marshaled as.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value the data is to be marshaled as.</returns>
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x000E3A79 File Offset: 0x000E1C79
		public UnmanagedType Value
		{
			get
			{
				return this.utype;
			}
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x000E3A81 File Offset: 0x000E1C81
		internal MarshalAsAttribute Copy()
		{
			return (MarshalAsAttribute)base.MemberwiseClone();
		}

		/// <summary>Provides additional information to a custom marshaler.</summary>
		// Token: 0x04002BD4 RID: 11220
		public string MarshalCookie;

		/// <summary>Specifies the fully qualified name of a custom marshaler.</summary>
		// Token: 0x04002BD5 RID: 11221
		[ComVisible(true)]
		public string MarshalType;

		/// <summary>Implements <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalType" /> as a type.</summary>
		// Token: 0x04002BD6 RID: 11222
		[ComVisible(true)]
		[PreserveDependency("GetCustomMarshalerInstance", "System.Runtime.InteropServices.Marshal")]
		public Type MarshalTypeRef;

		/// <summary>Indicates the user-defined element type of the <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.</summary>
		// Token: 0x04002BD7 RID: 11223
		public Type SafeArrayUserDefinedSubType;

		// Token: 0x04002BD8 RID: 11224
		private UnmanagedType utype;

		/// <summary>Specifies the element type of the unmanaged <see cref="F:System.Runtime.InteropServices.UnmanagedType.LPArray" /> or <see cref="F:System.Runtime.InteropServices.UnmanagedType.ByValArray" />.</summary>
		// Token: 0x04002BD9 RID: 11225
		public UnmanagedType ArraySubType;

		/// <summary>Indicates the element type of the <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.</summary>
		// Token: 0x04002BDA RID: 11226
		public VarEnum SafeArraySubType;

		/// <summary>Indicates the number of elements in the fixed-length array or the number of characters (not bytes) in a string to import.</summary>
		// Token: 0x04002BDB RID: 11227
		public int SizeConst;

		/// <summary>Specifies the parameter index of the unmanaged <see langword="iid_is" /> attribute used by COM.</summary>
		// Token: 0x04002BDC RID: 11228
		public int IidParameterIndex;

		/// <summary>Indicates the zero-based parameter that contains the count of array elements, similar to <see langword="size_is" /> in COM.</summary>
		// Token: 0x04002BDD RID: 11229
		public short SizeParamIndex;
	}
}
