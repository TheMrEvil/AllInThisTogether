using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Contains the <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" /> that were originally imported for this method from the COM type library.</summary>
	// Token: 0x020006FA RID: 1786
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibFuncAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="TypeLibFuncAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for the attributed method as found in the type library it was imported from.</param>
		// Token: 0x06004085 RID: 16517 RVA: 0x000E1144 File Offset: 0x000DF344
		public TypeLibFuncAttribute(TypeLibFuncFlags flags)
		{
			this._val = flags;
		}

		/// <summary>Initializes a new instance of the <see langword="TypeLibFuncAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for the attributed method as found in the type library it was imported from.</param>
		// Token: 0x06004086 RID: 16518 RVA: 0x000E1144 File Offset: 0x000DF344
		public TypeLibFuncAttribute(short flags)
		{
			this._val = (TypeLibFuncFlags)flags;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for this method.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for this method.</returns>
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06004087 RID: 16519 RVA: 0x000E1153 File Offset: 0x000DF353
		public TypeLibFuncFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A71 RID: 10865
		internal TypeLibFuncFlags _val;
	}
}
