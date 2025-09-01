using System;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.DataProtectionPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class DataProtectionPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.DataProtectionPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06000017 RID: 23 RVA: 0x0000234E File Offset: 0x0000054E
		public DataProtectionPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the data protection permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.DataProtectionPermissionFlags.NoFlags" />.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002357 File Offset: 0x00000557
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000235F File Offset: 0x0000055F
		public DataProtectionPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & DataProtectionPermissionFlags.AllFlags) != value)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid flags {0}"), value), "DataProtectionPermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether data can be encrypted using the <see cref="T:System.Security.Cryptography.ProtectedData" /> class.</summary>
		/// <returns>
		///   <see langword="true" /> if data can be encrypted; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000238F File Offset: 0x0000058F
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000239C File Offset: 0x0000059C
		public bool ProtectData
		{
			get
			{
				return (this._flags & DataProtectionPermissionFlags.ProtectData) > DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= DataProtectionPermissionFlags.ProtectData;
					return;
				}
				this._flags &= ~DataProtectionPermissionFlags.ProtectData;
			}
		}

		/// <summary>Gets or sets a value indicating whether data can be unencrypted using the <see cref="T:System.Security.Cryptography.ProtectedData" /> class.</summary>
		/// <returns>
		///   <see langword="true" /> if data can be unencrypted; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023BF File Offset: 0x000005BF
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000023CC File Offset: 0x000005CC
		public bool UnprotectData
		{
			get
			{
				return (this._flags & DataProtectionPermissionFlags.UnprotectData) > DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= DataProtectionPermissionFlags.UnprotectData;
					return;
				}
				this._flags &= ~DataProtectionPermissionFlags.UnprotectData;
			}
		}

		/// <summary>Gets or sets a value indicating whether memory can be encrypted using the <see cref="T:System.Security.Cryptography.ProtectedMemory" /> class.</summary>
		/// <returns>
		///   <see langword="true" /> if memory can be encrypted; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000023EF File Offset: 0x000005EF
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000023FC File Offset: 0x000005FC
		public bool ProtectMemory
		{
			get
			{
				return (this._flags & DataProtectionPermissionFlags.ProtectMemory) > DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= DataProtectionPermissionFlags.ProtectMemory;
					return;
				}
				this._flags &= ~DataProtectionPermissionFlags.ProtectMemory;
			}
		}

		/// <summary>Gets or sets a value indicating whether memory can be unencrypted using the <see cref="T:System.Security.Cryptography.ProtectedMemory" /> class.</summary>
		/// <returns>
		///   <see langword="true" /> if memory can be unencrypted; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000241F File Offset: 0x0000061F
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000242C File Offset: 0x0000062C
		public bool UnprotectMemory
		{
			get
			{
				return (this._flags & DataProtectionPermissionFlags.UnprotectMemory) > DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= DataProtectionPermissionFlags.UnprotectMemory;
					return;
				}
				this._flags &= ~DataProtectionPermissionFlags.UnprotectMemory;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.DataProtectionPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.DataProtectionPermission" /> that corresponds to the attribute.</returns>
		// Token: 0x06000022 RID: 34 RVA: 0x00002450 File Offset: 0x00000650
		public override IPermission CreatePermission()
		{
			DataProtectionPermission result;
			if (base.Unrestricted)
			{
				result = new DataProtectionPermission(PermissionState.Unrestricted);
			}
			else
			{
				result = new DataProtectionPermission(this._flags);
			}
			return result;
		}

		// Token: 0x04000078 RID: 120
		private DataProtectionPermissionFlags _flags;
	}
}
