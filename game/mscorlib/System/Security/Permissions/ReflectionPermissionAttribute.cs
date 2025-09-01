using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.ReflectionPermission" /> to be applied to code using declarative security.</summary>
	// Token: 0x02000452 RID: 1106
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ReflectionPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002CDB RID: 11483 RVA: 0x0009DCDC File Offset: 0x0009BEDC
		public ReflectionPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the current allowed uses of reflection.</summary>
		/// <returns>One or more of the <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> values combined using a bitwise OR.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> for the valid values.</exception>
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x000A0F64 File Offset: 0x0009F164
		// (set) Token: 0x06002CDD RID: 11485 RVA: 0x000A0F6C File Offset: 0x0009F16C
		public ReflectionPermissionFlag Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
				this.memberAccess = ((this.flags & ReflectionPermissionFlag.MemberAccess) == ReflectionPermissionFlag.MemberAccess);
				this.reflectionEmit = ((this.flags & ReflectionPermissionFlag.ReflectionEmit) == ReflectionPermissionFlag.ReflectionEmit);
				this.typeInfo = ((this.flags & ReflectionPermissionFlag.TypeInformation) == ReflectionPermissionFlag.TypeInformation);
			}
		}

		/// <summary>Gets or sets a value that indicates whether invocation of operations on non-public members is allowed.</summary>
		/// <returns>
		///   <see langword="true" /> if invocation of operations on non-public members is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x000A0FA8 File Offset: 0x0009F1A8
		// (set) Token: 0x06002CDF RID: 11487 RVA: 0x000A0FB0 File Offset: 0x0009F1B0
		public bool MemberAccess
		{
			get
			{
				return this.memberAccess;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.MemberAccess;
				}
				else
				{
					this.flags -= 2;
				}
				this.memberAccess = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether use of certain features in <see cref="N:System.Reflection.Emit" />, such as emitting debug symbols, is allowed.</summary>
		/// <returns>
		///   <see langword="true" /> if use of the affected features is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000A0FDA File Offset: 0x0009F1DA
		// (set) Token: 0x06002CE1 RID: 11489 RVA: 0x000A0FE2 File Offset: 0x0009F1E2
		[Obsolete]
		public bool ReflectionEmit
		{
			get
			{
				return this.reflectionEmit;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.ReflectionEmit;
				}
				else
				{
					this.flags -= 4;
				}
				this.reflectionEmit = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether restricted invocation of non-public members is allowed. Restricted invocation means that the grant set of the assembly that contains the non-public member that is being invoked must be equal to, or a subset of, the grant set of the invoking assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if restricted invocation of non-public members is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x000A100C File Offset: 0x0009F20C
		// (set) Token: 0x06002CE3 RID: 11491 RVA: 0x000A1019 File Offset: 0x0009F219
		public bool RestrictedMemberAccess
		{
			get
			{
				return (this.flags & ReflectionPermissionFlag.RestrictedMemberAccess) == ReflectionPermissionFlag.RestrictedMemberAccess;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.RestrictedMemberAccess;
					return;
				}
				this.flags -= 8;
			}
		}

		/// <summary>Gets or sets a value that indicates whether reflection on members that are not visible is allowed.</summary>
		/// <returns>
		///   <see langword="true" /> if reflection on members that are not visible is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x000A103B File Offset: 0x0009F23B
		// (set) Token: 0x06002CE5 RID: 11493 RVA: 0x000A1043 File Offset: 0x0009F243
		[Obsolete("not enforced in 2.0+")]
		public bool TypeInformation
		{
			get
			{
				return this.typeInfo;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.TypeInformation;
				}
				else
				{
					this.flags--;
				}
				this.typeInfo = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.ReflectionPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.ReflectionPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002CE6 RID: 11494 RVA: 0x000A1070 File Offset: 0x0009F270
		public override IPermission CreatePermission()
		{
			ReflectionPermission result;
			if (base.Unrestricted)
			{
				result = new ReflectionPermission(PermissionState.Unrestricted);
			}
			else
			{
				result = new ReflectionPermission(this.flags);
			}
			return result;
		}

		// Token: 0x0400207B RID: 8315
		private ReflectionPermissionFlag flags;

		// Token: 0x0400207C RID: 8316
		private bool memberAccess;

		// Token: 0x0400207D RID: 8317
		private bool reflectionEmit;

		// Token: 0x0400207E RID: 8318
		private bool typeInfo;
	}
}
