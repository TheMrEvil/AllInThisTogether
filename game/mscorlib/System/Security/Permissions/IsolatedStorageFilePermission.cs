using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the allowed usage of a private virtual file system. This class cannot be inherited.</summary>
	// Token: 0x02000441 RID: 1089
	[ComVisible(true)]
	[Serializable]
	public sealed class IsolatedStorageFilePermission : IsolatedStoragePermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" /> class with either fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002C32 RID: 11314 RVA: 0x0009F0D8 File Offset: 0x0009D2D8
		public IsolatedStorageFilePermission(PermissionState state) : base(state)
		{
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002C33 RID: 11315 RVA: 0x0009F0E4 File Offset: 0x0009D2E4
		public override IPermission Copy()
		{
			return new IsolatedStorageFilePermission(PermissionState.None)
			{
				m_userQuota = this.m_userQuota,
				m_machineQuota = this.m_machineQuota,
				m_expirationDays = this.m_expirationDays,
				m_permanentData = this.m_permanentData,
				m_allowed = this.m_allowed
			};
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission object. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002C34 RID: 11316 RVA: 0x0009F134 File Offset: 0x0009D334
		public override IPermission Intersect(IPermission target)
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission = this.Cast(target);
			if (isolatedStorageFilePermission == null)
			{
				return null;
			}
			if (base.IsEmpty() && isolatedStorageFilePermission.IsEmpty())
			{
				return null;
			}
			return new IsolatedStorageFilePermission(PermissionState.None)
			{
				m_userQuota = ((this.m_userQuota < isolatedStorageFilePermission.m_userQuota) ? this.m_userQuota : isolatedStorageFilePermission.m_userQuota),
				m_machineQuota = ((this.m_machineQuota < isolatedStorageFilePermission.m_machineQuota) ? this.m_machineQuota : isolatedStorageFilePermission.m_machineQuota),
				m_expirationDays = ((this.m_expirationDays < isolatedStorageFilePermission.m_expirationDays) ? this.m_expirationDays : isolatedStorageFilePermission.m_expirationDays),
				m_permanentData = (this.m_permanentData && isolatedStorageFilePermission.m_permanentData),
				UsageAllowed = ((this.m_allowed < isolatedStorageFilePermission.m_allowed) ? this.m_allowed : isolatedStorageFilePermission.m_allowed)
			};
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002C35 RID: 11317 RVA: 0x0009F208 File Offset: 0x0009D408
		public override bool IsSubsetOf(IPermission target)
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission = this.Cast(target);
			if (isolatedStorageFilePermission == null)
			{
				return base.IsEmpty();
			}
			return isolatedStorageFilePermission.IsUnrestricted() || (this.m_userQuota <= isolatedStorageFilePermission.m_userQuota && this.m_machineQuota <= isolatedStorageFilePermission.m_machineQuota && this.m_expirationDays <= isolatedStorageFilePermission.m_expirationDays && this.m_permanentData == isolatedStorageFilePermission.m_permanentData && this.m_allowed <= isolatedStorageFilePermission.m_allowed);
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002C36 RID: 11318 RVA: 0x0009F284 File Offset: 0x0009D484
		public override IPermission Union(IPermission target)
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission = this.Cast(target);
			if (isolatedStorageFilePermission == null)
			{
				return this.Copy();
			}
			return new IsolatedStorageFilePermission(PermissionState.None)
			{
				m_userQuota = ((this.m_userQuota > isolatedStorageFilePermission.m_userQuota) ? this.m_userQuota : isolatedStorageFilePermission.m_userQuota),
				m_machineQuota = ((this.m_machineQuota > isolatedStorageFilePermission.m_machineQuota) ? this.m_machineQuota : isolatedStorageFilePermission.m_machineQuota),
				m_expirationDays = ((this.m_expirationDays > isolatedStorageFilePermission.m_expirationDays) ? this.m_expirationDays : isolatedStorageFilePermission.m_expirationDays),
				m_permanentData = (this.m_permanentData || isolatedStorageFilePermission.m_permanentData),
				UsageAllowed = ((this.m_allowed > isolatedStorageFilePermission.m_allowed) ? this.m_allowed : isolatedStorageFilePermission.m_allowed)
			};
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002C37 RID: 11319 RVA: 0x0009F348 File Offset: 0x0009D548
		[MonoTODO("(2.0) new override - something must have been added ???")]
		[ComVisible(false)]
		public override SecurityElement ToXml()
		{
			return base.ToXml();
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000221D2 File Offset: 0x000203D2
		int IBuiltInPermission.GetTokenIndex()
		{
			return 3;
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x0009F350 File Offset: 0x0009D550
		private IsolatedStorageFilePermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			IsolatedStorageFilePermission isolatedStorageFilePermission = target as IsolatedStorageFilePermission;
			if (isolatedStorageFilePermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(IsolatedStorageFilePermission));
			}
			return isolatedStorageFilePermission;
		}
	}
}
