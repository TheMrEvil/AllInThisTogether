using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Sets the synchronization value of the component. This class cannot be inherited.</summary>
	// Token: 0x0200004E RID: 78
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class SynchronizationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SynchronizationAttribute" /> class with the default <see cref="T:System.EnterpriseServices.SynchronizationOption" />.</summary>
		// Token: 0x0600014B RID: 331 RVA: 0x00002644 File Offset: 0x00000844
		public SynchronizationAttribute() : this(SynchronizationOption.Required)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SynchronizationAttribute" /> class with the specified <see cref="T:System.EnterpriseServices.SynchronizationOption" />.</summary>
		/// <param name="val">One of the <see cref="T:System.EnterpriseServices.SynchronizationOption" /> values.</param>
		// Token: 0x0600014C RID: 332 RVA: 0x0000264D File Offset: 0x0000084D
		public SynchronizationAttribute(SynchronizationOption val)
		{
			this.val = val;
		}

		/// <summary>Gets the current setting of the <see cref="P:System.EnterpriseServices.SynchronizationAttribute.Value" /> property.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.SynchronizationOption" /> values. The default is <see langword="Required" />.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000265C File Offset: 0x0000085C
		public SynchronizationOption Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x0400008C RID: 140
		private SynchronizationOption val;
	}
}
