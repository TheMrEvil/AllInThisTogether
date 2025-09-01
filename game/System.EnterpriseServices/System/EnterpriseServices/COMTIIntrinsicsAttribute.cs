using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables you to pass context properties from the COM Transaction Integrator (COMTI) into the COM+ context.</summary>
	// Token: 0x02000016 RID: 22
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class COMTIIntrinsicsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.COMTIIntrinsicsAttribute" /> class, setting the <see cref="P:System.EnterpriseServices.COMTIIntrinsicsAttribute.Value" /> property to <see langword="true" />.</summary>
		// Token: 0x0600003B RID: 59 RVA: 0x000021E8 File Offset: 0x000003E8
		public COMTIIntrinsicsAttribute()
		{
			this.val = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.COMTIIntrinsicsAttribute" /> class, enabling the setting of the <see cref="P:System.EnterpriseServices.COMTIIntrinsicsAttribute.Value" /> property.</summary>
		/// <param name="val">
		///   <see langword="true" /> if the COMTI context properties are passed into the COM+ context; otherwise, <see langword="false" />.</param>
		// Token: 0x0600003C RID: 60 RVA: 0x000021F7 File Offset: 0x000003F7
		public COMTIIntrinsicsAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets a value indicating whether the COM Transaction Integrator (COMTI) context properties are passed into the COM+ context.</summary>
		/// <returns>
		///   <see langword="true" /> if the COMTI context properties are passed into the COM+ context; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002206 File Offset: 0x00000406
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x0400004A RID: 74
		private bool val;
	}
}
