using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates whether a managed interface is dual, dispatch-only, or <see langword="IUnknown" /> -only when exposed to COM.</summary>
	// Token: 0x020006E7 RID: 1767
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class InterfaceTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InterfaceTypeAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> enumeration member.</summary>
		/// <param name="interfaceType">One of the <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> values that describes how the interface should be exposed to COM clients.</param>
		// Token: 0x06004064 RID: 16484 RVA: 0x000E0F56 File Offset: 0x000DF156
		public InterfaceTypeAttribute(ComInterfaceType interfaceType)
		{
			this._val = interfaceType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InterfaceTypeAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> enumeration member.</summary>
		/// <param name="interfaceType">Describes how the interface should be exposed to COM clients.</param>
		// Token: 0x06004065 RID: 16485 RVA: 0x000E0F56 File Offset: 0x000DF156
		public InterfaceTypeAttribute(short interfaceType)
		{
			this._val = (ComInterfaceType)interfaceType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> value that describes how the interface should be exposed to COM.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> value that describes how the interface should be exposed to COM.</returns>
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06004066 RID: 16486 RVA: 0x000E0F65 File Offset: 0x000DF165
		public ComInterfaceType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A33 RID: 10803
		internal ComInterfaceType _val;
	}
}
