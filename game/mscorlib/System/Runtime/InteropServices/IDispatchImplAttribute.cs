using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates which <see langword="IDispatch" /> implementation the common language runtime uses when exposing dual interfaces and dispinterfaces to COM.</summary>
	// Token: 0x020006F3 RID: 1779
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	[Obsolete("This attribute is deprecated and will be removed in a future version.", false)]
	public sealed class IDispatchImplAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="IDispatchImplAttribute" /> class with specified <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value.</summary>
		/// <param name="implType">Indicates which <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> enumeration will be used.</param>
		// Token: 0x06004078 RID: 16504 RVA: 0x000E1013 File Offset: 0x000DF213
		public IDispatchImplAttribute(IDispatchImplType implType)
		{
			this._val = implType;
		}

		/// <summary>Initializes a new instance of the <see langword="IDispatchImplAttribute" /> class with specified <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value.</summary>
		/// <param name="implType">Indicates which <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> enumeration will be used.</param>
		// Token: 0x06004079 RID: 16505 RVA: 0x000E1013 File Offset: 0x000DF213
		public IDispatchImplAttribute(short implType)
		{
			this._val = (IDispatchImplType)implType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value used by the class.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.IDispatchImplType" /> value used by the class.</returns>
		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x000E1022 File Offset: 0x000DF222
		public IDispatchImplType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A43 RID: 10819
		internal IDispatchImplType _val;
	}
}
