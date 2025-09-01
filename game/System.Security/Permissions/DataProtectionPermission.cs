using System;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access encrypted data and memory. This class cannot be inherited.</summary>
	// Token: 0x02000009 RID: 9
	[Serializable]
	public sealed class DataProtectionPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.DataProtectionPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value.</exception>
		// Token: 0x0600000B RID: 11 RVA: 0x0000215D File Offset: 0x0000035D
		public DataProtectionPermission(PermissionState state)
		{
			if (PermissionHelper.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._flags = DataProtectionPermissionFlags.AllFlags;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.DataProtectionPermission" /> class with the specified permission flags.</summary>
		/// <param name="flag">A bitwise combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flag" /> is not a valid combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values.</exception>
		// Token: 0x0600000C RID: 12 RVA: 0x00002177 File Offset: 0x00000377
		public DataProtectionPermission(DataProtectionPermissionFlags flag)
		{
			this.Flags = flag;
		}

		/// <summary>Gets or sets the data and memory protection flags.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value is not a valid combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values.</exception>
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002186 File Offset: 0x00000386
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000218E File Offset: 0x0000038E
		public DataProtectionPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & ~(DataProtectionPermissionFlags.ProtectData | DataProtectionPermissionFlags.UnprotectData | DataProtectionPermissionFlags.ProtectMemory | DataProtectionPermissionFlags.UnprotectMemory)) != DataProtectionPermissionFlags.NoFlags)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "DataProtectionPermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600000F RID: 15 RVA: 0x000021BD File Offset: 0x000003BD
		public bool IsUnrestricted()
		{
			return this._flags == DataProtectionPermissionFlags.AllFlags;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06000010 RID: 16 RVA: 0x000021C9 File Offset: 0x000003C9
		public override IPermission Copy()
		{
			return new DataProtectionPermission(this._flags);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and does not specify a permission of the same type as the current permission.</exception>
		// Token: 0x06000011 RID: 17 RVA: 0x000021D8 File Offset: 0x000003D8
		public override IPermission Intersect(IPermission target)
		{
			DataProtectionPermission dataProtectionPermission = this.Cast(target);
			if (dataProtectionPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted() && dataProtectionPermission.IsUnrestricted())
			{
				return new DataProtectionPermission(PermissionState.Unrestricted);
			}
			if (this.IsUnrestricted())
			{
				return dataProtectionPermission.Copy();
			}
			if (dataProtectionPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			return new DataProtectionPermission(this._flags & dataProtectionPermission._flags);
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and does not specify a permission of the same type as the current permission.</exception>
		// Token: 0x06000012 RID: 18 RVA: 0x0000223C File Offset: 0x0000043C
		public override IPermission Union(IPermission target)
		{
			DataProtectionPermission dataProtectionPermission = this.Cast(target);
			if (dataProtectionPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || dataProtectionPermission.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new DataProtectionPermission(this._flags | dataProtectionPermission._flags);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and does not specify a permission of the same type as the current permission.</exception>
		// Token: 0x06000013 RID: 19 RVA: 0x00002284 File Offset: 0x00000484
		public override bool IsSubsetOf(IPermission target)
		{
			DataProtectionPermission dataProtectionPermission = this.Cast(target);
			if (dataProtectionPermission == null)
			{
				return this._flags == DataProtectionPermissionFlags.NoFlags;
			}
			return dataProtectionPermission.IsUnrestricted() || (!this.IsUnrestricted() && (this._flags & ~dataProtectionPermission._flags) == DataProtectionPermissionFlags.NoFlags);
		}

		/// <summary>Reconstructs a permission with a specific state from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding used to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element.  
		/// -or-  
		/// The version number of <paramref name="securityElement" /> is not supported.</exception>
		// Token: 0x06000014 RID: 20 RVA: 0x000022CB File Offset: 0x000004CB
		public override void FromXml(SecurityElement securityElement)
		{
			PermissionHelper.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			this._flags = (DataProtectionPermissionFlags)Enum.Parse(typeof(DataProtectionPermissionFlags), securityElement.Attribute("Flags"));
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including state information.</returns>
		// Token: 0x06000015 RID: 21 RVA: 0x00002300 File Offset: 0x00000500
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(typeof(DataProtectionPermission), 1);
			securityElement.AddAttribute("Flags", this._flags.ToString());
			return securityElement;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000232E File Offset: 0x0000052E
		private DataProtectionPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			DataProtectionPermission dataProtectionPermission = target as DataProtectionPermission;
			if (dataProtectionPermission == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(DataProtectionPermission));
			}
			return dataProtectionPermission;
		}

		// Token: 0x04000076 RID: 118
		private const int version = 1;

		// Token: 0x04000077 RID: 119
		private DataProtectionPermissionFlags _flags;
	}
}
