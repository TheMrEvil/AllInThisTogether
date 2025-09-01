using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.GacIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200043C RID: 1084
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class GacIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.GacIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid <see cref="T:System.Security.Permissions.SecurityAction" /> value.</exception>
		// Token: 0x06002C0B RID: 11275 RVA: 0x0009DCDC File Offset: 0x0009BEDC
		public GacIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Creates a new <see cref="T:System.Security.Permissions.GacIdentityPermission" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.GacIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002C0C RID: 11276 RVA: 0x0009A06E File Offset: 0x0009826E
		public override IPermission CreatePermission()
		{
			return new GacIdentityPermission();
		}
	}
}
