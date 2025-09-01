using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables security checking on calls to a component. This class cannot be inherited.</summary>
	// Token: 0x02000017 RID: 23
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ComponentAccessControlAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ComponentAccessControlAttribute" /> class.</summary>
		// Token: 0x0600003E RID: 62 RVA: 0x0000220E File Offset: 0x0000040E
		public ComponentAccessControlAttribute()
		{
			this.val = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ComponentAccessControlAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ComponentAccessControlAttribute.Value" /> property to indicate whether to enable COM+ security configuration.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable security checking on calls to a component; otherwise, <see langword="false" />.</param>
		// Token: 0x0600003F RID: 63 RVA: 0x0000221D File Offset: 0x0000041D
		public ComponentAccessControlAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets a value indicating whether to enable security checking on calls to a component.</summary>
		/// <returns>
		///   <see langword="true" /> if security checking is to be enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000222C File Offset: 0x0000042C
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x0400004B RID: 75
		private bool val;
	}
}
