using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the type of class interface to be generated for a class exposed to COM, if an interface is generated at all.</summary>
	// Token: 0x020006EA RID: 1770
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ClassInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> enumeration member.</summary>
		/// <param name="classInterfaceType">One of the <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> values that describes the type of interface that is generated for a class.</param>
		// Token: 0x06004069 RID: 16489 RVA: 0x000E0F84 File Offset: 0x000DF184
		public ClassInterfaceAttribute(ClassInterfaceType classInterfaceType)
		{
			this._val = classInterfaceType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> enumeration value.</summary>
		/// <param name="classInterfaceType">Describes the type of interface that is generated for a class.</param>
		// Token: 0x0600406A RID: 16490 RVA: 0x000E0F84 File Offset: 0x000DF184
		public ClassInterfaceAttribute(short classInterfaceType)
		{
			this._val = (ClassInterfaceType)classInterfaceType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> value that describes which type of interface should be generated for the class.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> value that describes which type of interface should be generated for the class.</returns>
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x0600406B RID: 16491 RVA: 0x000E0F93 File Offset: 0x000DF193
		public ClassInterfaceType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A39 RID: 10809
		internal ClassInterfaceType _val;
	}
}
