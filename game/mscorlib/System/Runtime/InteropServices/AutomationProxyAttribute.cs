using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies whether the type should be marshaled using the Automation marshaler or a custom proxy and stub.</summary>
	// Token: 0x0200070A RID: 1802
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	public sealed class AutomationProxyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.AutomationProxyAttribute" /> class.</summary>
		/// <param name="val">
		///   <see langword="true" /> if the class should be marshaled using the Automation Marshaler; <see langword="false" /> if a proxy stub marshaler should be used.</param>
		// Token: 0x060040AF RID: 16559 RVA: 0x000E1523 File Offset: 0x000DF723
		public AutomationProxyAttribute(bool val)
		{
			this._val = val;
		}

		/// <summary>Gets a value indicating the type of marshaler to use.</summary>
		/// <returns>
		///   <see langword="true" /> if the class should be marshaled using the Automation Marshaler; <see langword="false" /> if a proxy stub marshaler should be used.</returns>
		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x060040B0 RID: 16560 RVA: 0x000E1532 File Offset: 0x000DF732
		public bool Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002AE1 RID: 10977
		internal bool _val;
	}
}
