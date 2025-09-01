using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the base attribute class for declarative security from which <see cref="T:System.Security.Permissions.CodeAccessSecurityAttribute" /> is derived.</summary>
	// Token: 0x02000456 RID: 1110
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public abstract class SecurityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Permissions.SecurityAttribute" /> with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002D0D RID: 11533 RVA: 0x000A1AEF File Offset: 0x0009FCEF
		protected SecurityAttribute(SecurityAction action)
		{
			this.Action = action;
		}

		/// <summary>When overridden in a derived class, creates a permission object that can then be serialized into binary form and persistently stored along with the <see cref="T:System.Security.Permissions.SecurityAction" /> in an assembly's metadata.</summary>
		/// <returns>A serializable permission object.</returns>
		// Token: 0x06002D0E RID: 11534
		public abstract IPermission CreatePermission();

		/// <summary>Gets or sets a value indicating whether full (unrestricted) permission to the resource protected by the attribute is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if full permission to the protected resource is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002D0F RID: 11535 RVA: 0x000A1AFE File Offset: 0x0009FCFE
		// (set) Token: 0x06002D10 RID: 11536 RVA: 0x000A1B06 File Offset: 0x0009FD06
		public bool Unrestricted
		{
			get
			{
				return this.m_Unrestricted;
			}
			set
			{
				this.m_Unrestricted = value;
			}
		}

		/// <summary>Gets or sets a security action.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</returns>
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002D11 RID: 11537 RVA: 0x000A1B0F File Offset: 0x0009FD0F
		// (set) Token: 0x06002D12 RID: 11538 RVA: 0x000A1B17 File Offset: 0x0009FD17
		public SecurityAction Action
		{
			get
			{
				return this.m_Action;
			}
			set
			{
				this.m_Action = value;
			}
		}

		// Token: 0x04002093 RID: 8339
		private SecurityAction m_Action;

		// Token: 0x04002094 RID: 8340
		private bool m_Unrestricted;
	}
}
