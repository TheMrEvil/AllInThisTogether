using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Enables Compensating Resource Manger (CRM) on the tagged application.</summary>
	// Token: 0x0200006F RID: 111
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(false)]
	[ProgId("System.EnterpriseServices.Crm.ApplicationCrmEnabledAttribute")]
	public sealed class ApplicationCrmEnabledAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ApplicationCrmEnabledAttribute" /> class, setting the <see cref="P:System.EnterpriseServices.CompensatingResourceManager.ApplicationCrmEnabledAttribute.Value" /> property to <see langword="true" />.</summary>
		// Token: 0x060001AC RID: 428 RVA: 0x000026ED File Offset: 0x000008ED
		public ApplicationCrmEnabledAttribute()
		{
			this.val = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ApplicationCrmEnabledAttribute" /> class, optionally setting the <see cref="P:System.EnterpriseServices.CompensatingResourceManager.ApplicationCrmEnabledAttribute.Value" /> property to <see langword="false" />.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable Compensating Resource Manager (CRM); otherwise, <see langword="false" />.</param>
		// Token: 0x060001AD RID: 429 RVA: 0x000026FC File Offset: 0x000008FC
		public ApplicationCrmEnabledAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Enables or disables Compensating Resource Manager (CRM) on the tagged application.</summary>
		/// <returns>
		///   <see langword="true" /> if CRM is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000270B File Offset: 0x0000090B
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x040000B7 RID: 183
		private bool val;
	}
}
