using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Determines whether the component participates in load balancing, if the component load balancing service is installed and enabled on the server.</summary>
	// Token: 0x02000033 RID: 51
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class LoadBalancingSupportedAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.LoadBalancingSupportedAttribute" /> class, specifying load balancing support.</summary>
		// Token: 0x0600009B RID: 155 RVA: 0x000023BC File Offset: 0x000005BC
		public LoadBalancingSupportedAttribute() : this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.LoadBalancingSupportedAttribute" /> class, optionally disabling load balancing support.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable load balancing support; otherwise, <see langword="false" />.</param>
		// Token: 0x0600009C RID: 156 RVA: 0x000023C5 File Offset: 0x000005C5
		public LoadBalancingSupportedAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets a value that indicates whether load balancing support is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if load balancing support is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000023D4 File Offset: 0x000005D4
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x0400006D RID: 109
		private bool val;
	}
}
