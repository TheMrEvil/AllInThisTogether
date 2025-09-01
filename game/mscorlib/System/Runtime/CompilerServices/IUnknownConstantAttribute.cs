using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that the default value for the attributed field or parameter is an instance of <see cref="T:System.Runtime.InteropServices.UnknownWrapper" />, where the <see cref="P:System.Runtime.InteropServices.UnknownWrapper.WrappedObject" /> is <see langword="null" />. This class cannot be inherited.</summary>
	// Token: 0x02000842 RID: 2114
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IUnknownConstantAttribute : CustomConstantAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.IUnknownConstantAttribute" /> class.</summary>
		// Token: 0x060046BD RID: 18109 RVA: 0x000E711A File Offset: 0x000E531A
		public IUnknownConstantAttribute()
		{
		}

		/// <summary>Gets the <see langword="IUnknown" /> constant stored in this attribute.</summary>
		/// <returns>The <see langword="IUnknown" /> constant stored in this attribute. Only <see langword="null" /> is allowed for an <see langword="IUnknown" /> constant value.</returns>
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x060046BE RID: 18110 RVA: 0x000E7159 File Offset: 0x000E5359
		public override object Value
		{
			get
			{
				return new UnknownWrapper(null);
			}
		}
	}
}
