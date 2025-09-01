using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Marks the attributed method as an <see langword="AutoComplete" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(false)]
	public sealed class AutoCompleteAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.AutoCompleteAttribute" /> class, specifying that the application should automatically call <see cref="M:System.EnterpriseServices.ContextUtil.SetComplete" /> if the transaction completes successfully.</summary>
		// Token: 0x06000035 RID: 53 RVA: 0x000021BA File Offset: 0x000003BA
		public AutoCompleteAttribute()
		{
			this.val = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.AutoCompleteAttribute" /> class, specifying whether COM+ <see langword="AutoComplete" /> is enabled.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable <see langword="AutoComplete" /> in the COM+ object; otherwise, <see langword="false" />.</param>
		// Token: 0x06000036 RID: 54 RVA: 0x000021C9 File Offset: 0x000003C9
		public AutoCompleteAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets a value indicating the setting of the <see langword="AutoComplete" /> option in COM+.</summary>
		/// <returns>
		///   <see langword="true" /> if <see langword="AutoComplete" /> is enabled in COM+; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000021D8 File Offset: 0x000003D8
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x04000045 RID: 69
		private bool val;
	}
}
