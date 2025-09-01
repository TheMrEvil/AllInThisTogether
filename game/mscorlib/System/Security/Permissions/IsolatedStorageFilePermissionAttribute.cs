using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000442 RID: 1090
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class IsolatedStorageFilePermissionAttribute : IsolatedStoragePermissionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.IsolatedStorageFilePermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002C3A RID: 11322 RVA: 0x0009F370 File Offset: 0x0009D570
		public IsolatedStorageFilePermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" />.</summary>
		/// <returns>An <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002C3B RID: 11323 RVA: 0x0009F37C File Offset: 0x0009D57C
		public override IPermission CreatePermission()
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission;
			if (base.Unrestricted)
			{
				isolatedStorageFilePermission = new IsolatedStorageFilePermission(PermissionState.Unrestricted);
			}
			else
			{
				isolatedStorageFilePermission = new IsolatedStorageFilePermission(PermissionState.None);
				isolatedStorageFilePermission.UsageAllowed = base.UsageAllowed;
				isolatedStorageFilePermission.UserQuota = base.UserQuota;
			}
			return isolatedStorageFilePermission;
		}
	}
}
