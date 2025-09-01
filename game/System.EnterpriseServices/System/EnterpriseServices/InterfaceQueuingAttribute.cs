using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables queuing support for the marked interface. This class cannot be inherited.</summary>
	// Token: 0x02000031 RID: 49
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
	[ComVisible(false)]
	public sealed class InterfaceQueuingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.InterfaceQueuingAttribute" /> class setting the <see cref="P:System.EnterpriseServices.InterfaceQueuingAttribute.Enabled" /> and <see cref="P:System.EnterpriseServices.InterfaceQueuingAttribute.Interface" /> properties to their default values.</summary>
		// Token: 0x06000092 RID: 146 RVA: 0x0000235B File Offset: 0x0000055B
		public InterfaceQueuingAttribute() : this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.InterfaceQueuingAttribute" /> class, optionally disabling queuing support.</summary>
		/// <param name="enabled">
		///   <see langword="true" /> to enable queuing support; otherwise, <see langword="false" />.</param>
		// Token: 0x06000093 RID: 147 RVA: 0x00002364 File Offset: 0x00000564
		public InterfaceQueuingAttribute(bool enabled)
		{
			this.enabled = enabled;
			this.interfaceName = null;
		}

		/// <summary>Gets or sets a value indicating whether queuing support is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if queuing support is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000237A File Offset: 0x0000057A
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00002382 File Offset: 0x00000582
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

		/// <summary>Gets or sets the name of the interface on which queuing is enabled.</summary>
		/// <returns>The name of the interface on which queuing is enabled.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000238B File Offset: 0x0000058B
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00002393 File Offset: 0x00000593
		public string Interface
		{
			get
			{
				return this.interfaceName;
			}
			set
			{
				this.interfaceName = value;
			}
		}

		// Token: 0x0400006A RID: 106
		private bool enabled;

		// Token: 0x0400006B RID: 107
		private string interfaceName;
	}
}
