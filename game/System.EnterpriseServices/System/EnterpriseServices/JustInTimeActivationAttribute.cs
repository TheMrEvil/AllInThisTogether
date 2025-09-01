using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Turns just-in-time (JIT) activation on or off. This class cannot be inherited.</summary>
	// Token: 0x02000032 RID: 50
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class JustInTimeActivationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.JustInTimeActivationAttribute" /> class. The default constructor enables just-in-time (JIT) activation.</summary>
		// Token: 0x06000098 RID: 152 RVA: 0x0000239C File Offset: 0x0000059C
		public JustInTimeActivationAttribute() : this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.JustInTimeActivationAttribute" /> class, optionally allowing the disabling of just-in-time (JIT) activation by passing <see langword="false" /> as the parameter.</summary>
		/// <param name="val">
		///   <see langword="true" /> to enable JIT activation; otherwise, <see langword="false" />.</param>
		// Token: 0x06000099 RID: 153 RVA: 0x000023A5 File Offset: 0x000005A5
		public JustInTimeActivationAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets the value of the <see cref="T:System.EnterpriseServices.JustInTimeActivationAttribute" /> setting.</summary>
		/// <returns>
		///   <see langword="true" /> if JIT activation is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000023B4 File Offset: 0x000005B4
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x0400006C RID: 108
		private bool val;
	}
}
