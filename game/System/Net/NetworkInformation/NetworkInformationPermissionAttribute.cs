using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.NetworkInformation
{
	/// <summary>Allows security actions for <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> to be applied to code using declarative security.</summary>
	// Token: 0x020006F6 RID: 1782
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class NetworkInformationPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationPermissionAttribute" /> class.</summary>
		/// <param name="action">A <see cref="T:System.Security.Permissions.SecurityAction" /> value that specifies the permission behavior.</param>
		// Token: 0x06003948 RID: 14664 RVA: 0x000A97B6 File Offset: 0x000A79B6
		public NetworkInformationPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the network information access level.</summary>
		/// <returns>A string that specifies the access level.</returns>
		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06003949 RID: 14665 RVA: 0x000C830B File Offset: 0x000C650B
		// (set) Token: 0x0600394A RID: 14666 RVA: 0x000C8313 File Offset: 0x000C6513
		public string Access
		{
			get
			{
				return this.access;
			}
			set
			{
				this.access = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> object.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x0600394B RID: 14667 RVA: 0x000C831C File Offset: 0x000C651C
		public override IPermission CreatePermission()
		{
			NetworkInformationPermission networkInformationPermission;
			if (base.Unrestricted)
			{
				networkInformationPermission = new NetworkInformationPermission(PermissionState.Unrestricted);
			}
			else
			{
				networkInformationPermission = new NetworkInformationPermission(PermissionState.None);
				if (this.access != null)
				{
					if (string.Compare(this.access, "Read", StringComparison.OrdinalIgnoreCase) == 0)
					{
						networkInformationPermission.AddPermission(NetworkInformationAccess.Read);
					}
					else if (string.Compare(this.access, "Ping", StringComparison.OrdinalIgnoreCase) == 0)
					{
						networkInformationPermission.AddPermission(NetworkInformationAccess.Ping);
					}
					else
					{
						if (string.Compare(this.access, "None", StringComparison.OrdinalIgnoreCase) != 0)
						{
							throw new ArgumentException(SR.GetString("The parameter value '{0}={1}' is invalid.", new object[]
							{
								"Access",
								this.access
							}));
						}
						networkInformationPermission.AddPermission(NetworkInformationAccess.None);
					}
				}
			}
			return networkInformationPermission;
		}

		// Token: 0x04002195 RID: 8597
		private const string strAccess = "Access";

		// Token: 0x04002196 RID: 8598
		private string access;
	}
}
