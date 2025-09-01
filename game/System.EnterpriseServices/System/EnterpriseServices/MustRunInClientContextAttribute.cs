using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Forces the attributed object to be created in the context of the creator, if possible. This class cannot be inherited.</summary>
	// Token: 0x02000034 RID: 52
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class MustRunInClientContextAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.MustRunInClientContextAttribute" /> class, requiring creation of the object in the context of the creator.</summary>
		// Token: 0x0600009E RID: 158 RVA: 0x000023DC File Offset: 0x000005DC
		public MustRunInClientContextAttribute() : this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.MustRunInClientContextAttribute" /> class, optionally not creating the object in the context of the creator.</summary>
		/// <param name="val">
		///   <see langword="true" /> to create the object in the context of the creator; otherwise, <see langword="false" />.</param>
		// Token: 0x0600009F RID: 159 RVA: 0x000023E5 File Offset: 0x000005E5
		public MustRunInClientContextAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets a value that indicates whether the attributed object is to be created in the context of the creator.</summary>
		/// <returns>
		///   <see langword="true" /> if the object is to be created in the context of the creator; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000023F4 File Offset: 0x000005F4
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x0400006E RID: 110
		private bool val;
	}
}
