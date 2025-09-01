using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies a default interface to expose to COM. This class cannot be inherited.</summary>
	// Token: 0x020006E8 RID: 1768
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComDefaultInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComDefaultInterfaceAttribute" /> class with the specified <see cref="T:System.Type" /> object as the default interface exposed to COM.</summary>
		/// <param name="defaultInterface">A <see cref="T:System.Type" /> value indicating the default interface to expose to COM.</param>
		// Token: 0x06004067 RID: 16487 RVA: 0x000E0F6D File Offset: 0x000DF16D
		public ComDefaultInterfaceAttribute(Type defaultInterface)
		{
			this._val = defaultInterface;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object that specifies the default interface to expose to COM.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that specifies the default interface to expose to COM.</returns>
		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06004068 RID: 16488 RVA: 0x000E0F7C File Offset: 0x000DF17C
		public Type Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A34 RID: 10804
		internal Type _val;
	}
}
