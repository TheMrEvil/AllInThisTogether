using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables COM+ object construction support. This class cannot be inherited.</summary>
	// Token: 0x02000018 RID: 24
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ConstructionEnabledAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ConstructionEnabledAttribute" /> class and initializes the default settings for <see cref="P:System.EnterpriseServices.ConstructionEnabledAttribute.Enabled" /> and <see cref="P:System.EnterpriseServices.ConstructionEnabledAttribute.Default" />.</summary>
		// Token: 0x06000041 RID: 65 RVA: 0x00002234 File Offset: 0x00000434
		public ConstructionEnabledAttribute()
		{
			this.def = string.Empty;
			this.enabled = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ConstructionEnabledAttribute" /> class, setting <see cref="P:System.EnterpriseServices.ConstructionEnabledAttribute.Enabled" /> to the specified value.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable COM+ object construction support; otherwise, <see langword="false" />.</param>
		// Token: 0x06000042 RID: 66 RVA: 0x0000224E File Offset: 0x0000044E
		public ConstructionEnabledAttribute(bool val)
		{
			this.def = string.Empty;
			this.enabled = val;
		}

		/// <summary>Gets or sets a default value for the constructor string.</summary>
		/// <returns>The value to be used for the default constructor string. The default is an empty string ("").</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002268 File Offset: 0x00000468
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002270 File Offset: 0x00000470
		public string Default
		{
			get
			{
				return this.def;
			}
			set
			{
				this.def = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether COM+ object construction support is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if COM+ object construction support is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002279 File Offset: 0x00000479
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002281 File Offset: 0x00000481
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x0400004C RID: 76
		private string def;

		// Token: 0x0400004D RID: 77
		private bool enabled;
	}
}
