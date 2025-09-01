using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.EnvironmentPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000435 RID: 1077
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class EnvironmentPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.SecurityAction" />.</exception>
		// Token: 0x06002BAB RID: 11179 RVA: 0x0009DCDC File Offset: 0x0009BEDC
		public EnvironmentPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Sets full access for the environment variables specified by the string value.</summary>
		/// <returns>A list of environment variables for full access.</returns>
		/// <exception cref="T:System.NotSupportedException">The get method is not supported for this property.</exception>
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x0009DCE5 File Offset: 0x0009BEE5
		// (set) Token: 0x06002BAD RID: 11181 RVA: 0x0009DCF1 File Offset: 0x0009BEF1
		public string All
		{
			get
			{
				throw new NotSupportedException("All");
			}
			set
			{
				this.read = value;
				this.write = value;
			}
		}

		/// <summary>Gets or sets read access for the environment variables specified by the string value.</summary>
		/// <returns>A list of environment variables for read access.</returns>
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06002BAE RID: 11182 RVA: 0x0009DD01 File Offset: 0x0009BF01
		// (set) Token: 0x06002BAF RID: 11183 RVA: 0x0009DD09 File Offset: 0x0009BF09
		public string Read
		{
			get
			{
				return this.read;
			}
			set
			{
				this.read = value;
			}
		}

		/// <summary>Gets or sets write access for the environment variables specified by the string value.</summary>
		/// <returns>A list of environment variables for write access.</returns>
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x0009DD12 File Offset: 0x0009BF12
		// (set) Token: 0x06002BB1 RID: 11185 RVA: 0x0009DD1A File Offset: 0x0009BF1A
		public string Write
		{
			get
			{
				return this.write;
			}
			set
			{
				this.write = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.EnvironmentPermission" />.</summary>
		/// <returns>An <see cref="T:System.Security.Permissions.EnvironmentPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002BB2 RID: 11186 RVA: 0x0009DD24 File Offset: 0x0009BF24
		public override IPermission CreatePermission()
		{
			EnvironmentPermission environmentPermission;
			if (base.Unrestricted)
			{
				environmentPermission = new EnvironmentPermission(PermissionState.Unrestricted);
			}
			else
			{
				environmentPermission = new EnvironmentPermission(PermissionState.None);
				if (this.read != null)
				{
					environmentPermission.AddPathList(EnvironmentPermissionAccess.Read, this.read);
				}
				if (this.write != null)
				{
					environmentPermission.AddPathList(EnvironmentPermissionAccess.Write, this.write);
				}
			}
			return environmentPermission;
		}

		// Token: 0x0400200D RID: 8205
		private string read;

		// Token: 0x0400200E RID: 8206
		private string write;
	}
}
