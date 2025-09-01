using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables access to ASP intrinsic values from <see cref="M:System.EnterpriseServices.ContextUtil.GetNamedProperty(System.String)" />. This class cannot be inherited.</summary>
	// Token: 0x02000020 RID: 32
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class IISIntrinsicsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.IISIntrinsicsAttribute" /> class, enabling access to the ASP intrinsic values.</summary>
		// Token: 0x0600006F RID: 111 RVA: 0x00002335 File Offset: 0x00000535
		public IISIntrinsicsAttribute()
		{
			this.val = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.IISIntrinsicsAttribute" /> class, optionally disabling access to the ASP intrinsic values.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable access to the ASP intrinsic values; otherwise, <see langword="false" />.</param>
		// Token: 0x06000070 RID: 112 RVA: 0x00002344 File Offset: 0x00000544
		public IISIntrinsicsAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets a value that indicates whether access to the ASP intrinsic values is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if access is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002353 File Offset: 0x00000553
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x04000055 RID: 85
		private bool val;
	}
}
