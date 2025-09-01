using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.NetworkInformation
{
	/// <summary>Controls access to network information and traffic statistics for the local computer. This class cannot be inherited.</summary>
	// Token: 0x020006F7 RID: 1783
	[Serializable]
	public sealed class NetworkInformationPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		// Token: 0x0600394C RID: 14668 RVA: 0x000C83CA File Offset: 0x000C65CA
		public NetworkInformationPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.access = (NetworkInformationAccess.Read | NetworkInformationAccess.Ping);
				this.unrestricted = true;
				return;
			}
			this.access = NetworkInformationAccess.None;
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x000C83EC File Offset: 0x000C65EC
		internal NetworkInformationPermission(bool unrestricted)
		{
			if (unrestricted)
			{
				this.access = (NetworkInformationAccess.Read | NetworkInformationAccess.Ping);
				unrestricted = true;
				return;
			}
			this.access = NetworkInformationAccess.None;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> class using the specified <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> value.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> values.</param>
		// Token: 0x0600394E RID: 14670 RVA: 0x000C8409 File Offset: 0x000C6609
		public NetworkInformationPermission(NetworkInformationAccess access)
		{
			this.access = access;
		}

		/// <summary>Gets the level of access to network information controlled by this permission.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> values.</returns>
		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600394F RID: 14671 RVA: 0x000C8418 File Offset: 0x000C6618
		public NetworkInformationAccess Access
		{
			get
			{
				return this.access;
			}
		}

		/// <summary>Adds the specified value to this permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> values.</param>
		// Token: 0x06003950 RID: 14672 RVA: 0x000C8420 File Offset: 0x000C6620
		public void AddPermission(NetworkInformationAccess access)
		{
			this.access |= access;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003951 RID: 14673 RVA: 0x000C8430 File Offset: 0x000C6630
		public bool IsUnrestricted()
		{
			return this.unrestricted;
		}

		/// <summary>Creates and returns an identical copy of this permission.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> that is identical to the current permission</returns>
		// Token: 0x06003952 RID: 14674 RVA: 0x000C8438 File Offset: 0x000C6638
		public override IPermission Copy()
		{
			if (this.unrestricted)
			{
				return new NetworkInformationPermission(true);
			}
			return new NetworkInformationPermission(this.access);
		}

		/// <summary>Creates a permission that is the union of this permission and the specified permission.</summary>
		/// <param name="target">A <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> permission to combine with the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		// Token: 0x06003953 RID: 14675 RVA: 0x000C8454 File Offset: 0x000C6654
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			NetworkInformationPermission networkInformationPermission = target as NetworkInformationPermission;
			if (networkInformationPermission == null)
			{
				throw new ArgumentException(SR.GetString("Cannot cast target permission type."), "target");
			}
			if (this.unrestricted || networkInformationPermission.IsUnrestricted())
			{
				return new NetworkInformationPermission(true);
			}
			return new NetworkInformationPermission(this.access | networkInformationPermission.access);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" /> that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty or <paramref name="target" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" />.</exception>
		// Token: 0x06003954 RID: 14676 RVA: 0x000C84B4 File Offset: 0x000C66B4
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			NetworkInformationPermission networkInformationPermission = target as NetworkInformationPermission;
			if (networkInformationPermission == null)
			{
				throw new ArgumentException(SR.GetString("Cannot cast target permission type."), "target");
			}
			if (this.unrestricted && networkInformationPermission.IsUnrestricted())
			{
				return new NetworkInformationPermission(true);
			}
			return new NetworkInformationPermission(this.access & networkInformationPermission.access);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> that is to be tested for the subset relationship. This permission must be of the same type as the current permission</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003955 RID: 14677 RVA: 0x000C8510 File Offset: 0x000C6710
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.access == NetworkInformationAccess.None;
			}
			NetworkInformationPermission networkInformationPermission = target as NetworkInformationPermission;
			if (networkInformationPermission == null)
			{
				throw new ArgumentException(SR.GetString("Cannot cast target permission type."), "target");
			}
			return (!this.unrestricted || networkInformationPermission.IsUnrestricted()) && (this.access & networkInformationPermission.access) == this.access;
		}

		/// <summary>Sets the state of this permission using the specified XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding to use to set the state of the current permission</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a permission encoding.  
		/// -or-  
		/// <paramref name="securityElement" /> is not an encoding of a <see cref="T:System.Net.NetworkInformation.NetworkInformationPermission" />.  
		/// -or-  
		/// <paramref name="securityElement" /> has invalid <see cref="T:System.Net.NetworkInformation.NetworkInformationAccess" /> values.</exception>
		// Token: 0x06003956 RID: 14678 RVA: 0x000C8574 File Offset: 0x000C6774
		public override void FromXml(SecurityElement securityElement)
		{
			this.access = NetworkInformationAccess.None;
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (!securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("Specified value does not contain 'IPermission' as its tag."), "securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				throw new ArgumentException(SR.GetString("Specified value does not contain a 'class' attribute."), "securityElement");
			}
			if (text.IndexOf(base.GetType().FullName) < 0)
			{
				throw new ArgumentException(SR.GetString("The value class attribute is not valid."), "securityElement");
			}
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.access = (NetworkInformationAccess.Read | NetworkInformationAccess.Ping);
				this.unrestricted = true;
				return;
			}
			if (securityElement.Children != null)
			{
				foreach (object obj in securityElement.Children)
				{
					text2 = ((SecurityElement)obj).Attribute("Access");
					if (string.Compare(text2, "Read", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.access |= NetworkInformationAccess.Read;
					}
					else if (string.Compare(text2, "Ping", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.access |= NetworkInformationAccess.Ping;
					}
				}
			}
		}

		/// <summary>Creates an XML encoding of the state of this permission.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding of the current permission.</returns>
		// Token: 0x06003957 RID: 14679 RVA: 0x000C86C8 File Offset: 0x000C68C8
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (this.unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
				return securityElement;
			}
			if ((this.access & NetworkInformationAccess.Read) > NetworkInformationAccess.None)
			{
				SecurityElement securityElement2 = new SecurityElement("NetworkInformationAccess");
				securityElement2.AddAttribute("Access", "Read");
				securityElement.AddChild(securityElement2);
			}
			if ((this.access & NetworkInformationAccess.Ping) > NetworkInformationAccess.None)
			{
				SecurityElement securityElement3 = new SecurityElement("NetworkInformationAccess");
				securityElement3.AddAttribute("Access", "Ping");
				securityElement.AddChild(securityElement3);
			}
			return securityElement;
		}

		// Token: 0x04002197 RID: 8599
		private NetworkInformationAccess access;

		// Token: 0x04002198 RID: 8600
		private bool unrestricted;
	}
}
