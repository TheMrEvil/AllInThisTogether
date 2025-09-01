using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for strong names. This class cannot be inherited.</summary>
	// Token: 0x0200045C RID: 1116
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002D55 RID: 11605 RVA: 0x000A24CE File Offset: 0x000A06CE
		public StrongNameIdentityPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this._list = new ArrayList();
			this._list.Add(StrongNameIdentityPermission.SNIP.CreateDefault());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> class for the specified strong name identity.</summary>
		/// <param name="blob">The public key defining the strong name identity namespace.</param>
		/// <param name="name">The simple name part of the strong name identity. This corresponds to the name of the assembly.</param>
		/// <param name="version">The version number of the identity.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="blob" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string ("").</exception>
		// Token: 0x06002D56 RID: 11606 RVA: 0x000A2504 File Offset: 0x000A0704
		public StrongNameIdentityPermission(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (name != null && name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this._state = PermissionState.None;
			this._list = new ArrayList();
			this._list.Add(new StrongNameIdentityPermission.SNIP(blob, name, version));
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000A2568 File Offset: 0x000A0768
		internal StrongNameIdentityPermission(StrongNameIdentityPermission snip)
		{
			this._state = snip._state;
			this._list = new ArrayList(snip._list.Count);
			foreach (object obj in snip._list)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)obj;
				this._list.Add(new StrongNameIdentityPermission.SNIP(snip2.PublicKey, snip2.Name, snip2.AssemblyVersion));
			}
		}

		/// <summary>Gets or sets the simple name portion of the strong name identity.</summary>
		/// <returns>The simple name of the identity.</returns>
		/// <exception cref="T:System.ArgumentException">The value is an empty string ("").</exception>
		/// <exception cref="T:System.NotSupportedException">The property value cannot be retrieved because it contains an ambiguous identity.</exception>
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x000A260C File Offset: 0x000A080C
		// (set) Token: 0x06002D59 RID: 11609 RVA: 0x000A2638 File Offset: 0x000A0838
		public string Name
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).Name;
			}
			set
			{
				if (value != null && value.Length == 0)
				{
					throw new ArgumentException("name");
				}
				if (this._list.Count > 1)
				{
					this.ResetToDefault();
				}
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)this._list[0];
				snip.Name = value;
				this._list[0] = snip;
			}
		}

		/// <summary>Gets or sets the public key blob that defines the strong name identity namespace.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> that contains the public key of the identity, or <see langword="null" /> if there is no key.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is set to <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The property value cannot be retrieved because it contains an ambiguous identity.</exception>
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x000A269B File Offset: 0x000A089B
		// (set) Token: 0x06002D5B RID: 11611 RVA: 0x000A26C8 File Offset: 0x000A08C8
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).PublicKey;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this._list.Count > 1)
				{
					this.ResetToDefault();
				}
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)this._list[0];
				snip.PublicKey = value;
				this._list[0] = snip;
			}
		}

		/// <summary>Gets or sets the version number of the identity.</summary>
		/// <returns>The version of the identity.</returns>
		/// <exception cref="T:System.NotSupportedException">The property value cannot be retrieved because it contains an ambiguous identity.</exception>
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000A2723 File Offset: 0x000A0923
		// (set) Token: 0x06002D5D RID: 11613 RVA: 0x000A2750 File Offset: 0x000A0950
		public Version Version
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).AssemblyVersion;
			}
			set
			{
				if (this._list.Count > 1)
				{
					this.ResetToDefault();
				}
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)this._list[0];
				snip.AssemblyVersion = value;
				this._list[0] = snip;
			}
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000A279D File Offset: 0x000A099D
		internal void ResetToDefault()
		{
			this._list.Clear();
			this._list.Add(StrongNameIdentityPermission.SNIP.CreateDefault());
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002D5F RID: 11615 RVA: 0x000A27C0 File Offset: 0x000A09C0
		public override IPermission Copy()
		{
			if (this.IsEmpty())
			{
				return new StrongNameIdentityPermission(PermissionState.None);
			}
			return new StrongNameIdentityPermission(this);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="e" /> parameter's version number is not valid.</exception>
		// Token: 0x06002D60 RID: 11616 RVA: 0x000A27D8 File Offset: 0x000A09D8
		public override void FromXml(SecurityElement e)
		{
			CodeAccessPermission.CheckSecurityElement(e, "e", 1, 1);
			this._list.Clear();
			if (e.Children != null && e.Children.Count > 0)
			{
				using (IEnumerator enumerator = e.Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SecurityElement se = (SecurityElement)obj;
						this._list.Add(this.FromSecurityElement(se));
					}
					return;
				}
			}
			this._list.Add(this.FromSecurityElement(e));
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000A288C File Offset: 0x000A0A8C
		private StrongNameIdentityPermission.SNIP FromSecurityElement(SecurityElement se)
		{
			string name = se.Attribute("Name");
			StrongNamePublicKeyBlob pk = StrongNamePublicKeyBlob.FromString(se.Attribute("PublicKeyBlob"));
			string text = se.Attribute("AssemblyVersion");
			Version version = (text == null) ? null : new Version(text);
			return new StrongNameIdentityPermission.SNIP(pk, name, version);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission, or <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002D62 RID: 11618 RVA: 0x000A28D8 File Offset: 0x000A0AD8
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
			if (strongNameIdentityPermission == null)
			{
				throw new ArgumentException(Locale.GetText("Wrong permission type."));
			}
			if (this.IsEmpty() || strongNameIdentityPermission.IsEmpty())
			{
				return null;
			}
			if (!this.Match(strongNameIdentityPermission.Name))
			{
				return null;
			}
			string name = (this.Name.Length < strongNameIdentityPermission.Name.Length) ? this.Name : strongNameIdentityPermission.Name;
			if (!this.Version.Equals(strongNameIdentityPermission.Version))
			{
				return null;
			}
			if (!this.PublicKey.Equals(strongNameIdentityPermission.PublicKey))
			{
				return null;
			}
			return new StrongNameIdentityPermission(this.PublicKey, name, this.Version);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002D63 RID: 11619 RVA: 0x000A298C File Offset: 0x000A0B8C
		public override bool IsSubsetOf(IPermission target)
		{
			StrongNameIdentityPermission strongNameIdentityPermission = this.Cast(target);
			if (strongNameIdentityPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.IsEmpty())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return strongNameIdentityPermission.IsUnrestricted();
			}
			if (strongNameIdentityPermission.IsUnrestricted())
			{
				return true;
			}
			foreach (object obj in this._list)
			{
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
				foreach (object obj2 in strongNameIdentityPermission._list)
				{
					StrongNameIdentityPermission.SNIP target2 = (StrongNameIdentityPermission.SNIP)obj2;
					if (!snip.IsSubsetOf(target2))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002D64 RID: 11620 RVA: 0x000A2A70 File Offset: 0x000A0C70
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._list.Count > 1)
			{
				using (IEnumerator enumerator = this._list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
						SecurityElement securityElement2 = new SecurityElement("StrongName");
						this.ToSecurityElement(securityElement2, snip);
						securityElement.AddChild(securityElement2);
					}
					return securityElement;
				}
			}
			if (this._list.Count == 1)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)this._list[0];
				if (!this.IsEmpty(snip2))
				{
					this.ToSecurityElement(securityElement, snip2);
				}
			}
			return securityElement;
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000A2B2C File Offset: 0x000A0D2C
		private void ToSecurityElement(SecurityElement se, StrongNameIdentityPermission.SNIP snip)
		{
			if (snip.PublicKey != null)
			{
				se.AddAttribute("PublicKeyBlob", snip.PublicKey.ToString());
			}
			if (snip.Name != null)
			{
				se.AddAttribute("Name", snip.Name);
			}
			if (snip.AssemblyVersion != null)
			{
				se.AddAttribute("AssemblyVersion", snip.AssemblyVersion.ToString());
			}
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.  
		///  -or-  
		///  The two permissions are not equal and one is a subset of the other.</exception>
		// Token: 0x06002D66 RID: 11622 RVA: 0x000A2B94 File Offset: 0x000A0D94
		public override IPermission Union(IPermission target)
		{
			StrongNameIdentityPermission strongNameIdentityPermission = this.Cast(target);
			if (strongNameIdentityPermission == null || strongNameIdentityPermission.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return strongNameIdentityPermission.Copy();
			}
			StrongNameIdentityPermission strongNameIdentityPermission2 = (StrongNameIdentityPermission)this.Copy();
			foreach (object obj in strongNameIdentityPermission._list)
			{
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
				if (!this.IsEmpty(snip) && !this.Contains(snip))
				{
					strongNameIdentityPermission2._list.Add(snip);
				}
			}
			return strongNameIdentityPermission2;
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x00048419 File Offset: 0x00046619
		int IBuiltInPermission.GetTokenIndex()
		{
			return 12;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000A2C44 File Offset: 0x000A0E44
		private bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000A2C50 File Offset: 0x000A0E50
		private bool Contains(StrongNameIdentityPermission.SNIP snip)
		{
			foreach (object obj in this._list)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)obj;
				bool flag = (snip2.PublicKey == null && snip.PublicKey == null) || (snip2.PublicKey != null && snip2.PublicKey.Equals(snip.PublicKey));
				bool flag2 = snip2.IsNameSubsetOf(snip.Name);
				bool flag3 = (snip2.AssemblyVersion == null && snip.AssemblyVersion == null) || (snip2.AssemblyVersion != null && snip2.AssemblyVersion.Equals(snip.AssemblyVersion));
				if (flag && flag2 && flag3)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000A2D3C File Offset: 0x000A0F3C
		private bool IsEmpty(StrongNameIdentityPermission.SNIP snip)
		{
			return this.PublicKey == null && (this.Name == null || this.Name.Length <= 0) && (this.Version == null || StrongNameIdentityPermission.defaultVersion.Equals(this.Version));
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000A2D8C File Offset: 0x000A0F8C
		private bool IsEmpty()
		{
			return !this.IsUnrestricted() && this._list.Count <= 1 && this.PublicKey == null && (this.Name == null || this.Name.Length <= 0) && (this.Version == null || StrongNameIdentityPermission.defaultVersion.Equals(this.Version));
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000A2DF3 File Offset: 0x000A0FF3
		private StrongNameIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
			if (strongNameIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(StrongNameIdentityPermission));
			}
			return strongNameIdentityPermission;
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000A2E14 File Offset: 0x000A1014
		private bool Match(string target)
		{
			if (this.Name == null || target == null)
			{
				return false;
			}
			int num = this.Name.LastIndexOf('*');
			int num2 = target.LastIndexOf('*');
			int length;
			if (num == -1 && num2 == -1)
			{
				length = Math.Max(this.Name.Length, target.Length);
			}
			else if (num == -1)
			{
				length = num2;
			}
			else if (num2 == -1)
			{
				length = num;
			}
			else
			{
				length = Math.Min(num, num2);
			}
			return string.Compare(this.Name, 0, target, 0, length, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000A2E9C File Offset: 0x000A109C
		// Note: this type is marked as 'beforefieldinit'.
		static StrongNameIdentityPermission()
		{
		}

		// Token: 0x040020AD RID: 8365
		private const int version = 1;

		// Token: 0x040020AE RID: 8366
		private static Version defaultVersion = new Version(0, 0);

		// Token: 0x040020AF RID: 8367
		private PermissionState _state;

		// Token: 0x040020B0 RID: 8368
		private ArrayList _list;

		// Token: 0x0200045D RID: 1117
		private struct SNIP
		{
			// Token: 0x06002D6F RID: 11631 RVA: 0x000A2EAA File Offset: 0x000A10AA
			internal SNIP(StrongNamePublicKeyBlob pk, string name, Version version)
			{
				this.PublicKey = pk;
				this.Name = name;
				this.AssemblyVersion = version;
			}

			// Token: 0x06002D70 RID: 11632 RVA: 0x000A2EC1 File Offset: 0x000A10C1
			internal static StrongNameIdentityPermission.SNIP CreateDefault()
			{
				return new StrongNameIdentityPermission.SNIP(null, string.Empty, (Version)StrongNameIdentityPermission.defaultVersion.Clone());
			}

			// Token: 0x06002D71 RID: 11633 RVA: 0x000A2EE0 File Offset: 0x000A10E0
			internal bool IsNameSubsetOf(string target)
			{
				if (this.Name == null)
				{
					return target == null;
				}
				if (target == null)
				{
					return true;
				}
				int num = this.Name.LastIndexOf('*');
				if (num == 0)
				{
					return true;
				}
				if (num == -1)
				{
					num = this.Name.Length;
				}
				return string.Compare(this.Name, 0, target, 0, num, true, CultureInfo.InvariantCulture) == 0;
			}

			// Token: 0x06002D72 RID: 11634 RVA: 0x000A2F3C File Offset: 0x000A113C
			internal bool IsSubsetOf(StrongNameIdentityPermission.SNIP target)
			{
				return (this.PublicKey != null && this.PublicKey.Equals(target.PublicKey)) || (this.IsNameSubsetOf(target.Name) && (!(this.AssemblyVersion != null) || this.AssemblyVersion.Equals(target.AssemblyVersion)) && this.PublicKey == null && target.PublicKey == null);
			}

			// Token: 0x040020B1 RID: 8369
			public StrongNamePublicKeyBlob PublicKey;

			// Token: 0x040020B2 RID: 8370
			public string Name;

			// Token: 0x040020B3 RID: 8371
			public Version AssemblyVersion;
		}
	}
}
