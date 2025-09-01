using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for the Web site from which the code originates. This class cannot be inherited.</summary>
	// Token: 0x0200045A RID: 1114
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002D41 RID: 11585 RVA: 0x0009EC34 File Offset: 0x0009CE34
		public SiteIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> class to represent the specified site identity.</summary>
		/// <param name="site">The site name or wildcard expression.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="site" /> parameter is not a valid string, or does not match a valid wildcard site name.</exception>
		// Token: 0x06002D42 RID: 11586 RVA: 0x000A20D2 File Offset: 0x000A02D2
		public SiteIdentityPermission(string site)
		{
			this.Site = site;
		}

		/// <summary>Gets or sets the current site.</summary>
		/// <returns>The current site.</returns>
		/// <exception cref="T:System.NotSupportedException">The site identity cannot be retrieved because it has an ambiguous identity.</exception>
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002D43 RID: 11587 RVA: 0x000A20E1 File Offset: 0x000A02E1
		// (set) Token: 0x06002D44 RID: 11588 RVA: 0x000A20FC File Offset: 0x000A02FC
		public string Site
		{
			get
			{
				if (this.IsEmpty())
				{
					throw new NullReferenceException("No site.");
				}
				return this._site;
			}
			set
			{
				if (!this.IsValid(value))
				{
					throw new ArgumentException("Invalid site.");
				}
				this._site = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002D45 RID: 11589 RVA: 0x000A2119 File Offset: 0x000A0319
		public override IPermission Copy()
		{
			if (this.IsEmpty())
			{
				return new SiteIdentityPermission(PermissionState.None);
			}
			return new SiteIdentityPermission(this._site);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="esd" /> parameter's version number is not valid.</exception>
		// Token: 0x06002D46 RID: 11590 RVA: 0x000A2138 File Offset: 0x000A0338
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attribute("Site");
			if (text != null)
			{
				this.Site = text;
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002D47 RID: 11591 RVA: 0x000A216C File Offset: 0x000A036C
		public override IPermission Intersect(IPermission target)
		{
			SiteIdentityPermission siteIdentityPermission = this.Cast(target);
			if (siteIdentityPermission == null || this.IsEmpty())
			{
				return null;
			}
			if (this.Match(siteIdentityPermission._site))
			{
				return new SiteIdentityPermission((this._site.Length > siteIdentityPermission._site.Length) ? this._site : siteIdentityPermission._site);
			}
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002D48 RID: 11592 RVA: 0x000A21CC File Offset: 0x000A03CC
		public override bool IsSubsetOf(IPermission target)
		{
			SiteIdentityPermission siteIdentityPermission = this.Cast(target);
			if (siteIdentityPermission == null)
			{
				return this.IsEmpty();
			}
			if (this._site == null && siteIdentityPermission._site == null)
			{
				return true;
			}
			if (this._site == null || siteIdentityPermission._site == null)
			{
				return false;
			}
			int num = siteIdentityPermission._site.IndexOf('*');
			if (num == -1)
			{
				return this._site == siteIdentityPermission._site;
			}
			return this._site.EndsWith(siteIdentityPermission._site.Substring(num + 1));
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002D49 RID: 11593 RVA: 0x000A224C File Offset: 0x000A044C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._site != null)
			{
				securityElement.AddAttribute("Site", this._site);
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.  
		///  -or-  
		///  The permissions are not equal and one is not a subset of the other.</exception>
		// Token: 0x06002D4A RID: 11594 RVA: 0x000A227C File Offset: 0x000A047C
		public override IPermission Union(IPermission target)
		{
			SiteIdentityPermission siteIdentityPermission = this.Cast(target);
			if (siteIdentityPermission == null || siteIdentityPermission.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return siteIdentityPermission.Copy();
			}
			if (this.Match(siteIdentityPermission._site))
			{
				return new SiteIdentityPermission((this._site.Length < siteIdentityPermission._site.Length) ? this._site : siteIdentityPermission._site);
			}
			throw new ArgumentException(Locale.GetText("Cannot union two different sites."), "target");
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x00032730 File Offset: 0x00030930
		int IBuiltInPermission.GetTokenIndex()
		{
			return 11;
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000A2300 File Offset: 0x000A0500
		private bool IsEmpty()
		{
			return this._site == null;
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000A230B File Offset: 0x000A050B
		private SiteIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
			if (siteIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(SiteIdentityPermission));
			}
			return siteIdentityPermission;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000A232C File Offset: 0x000A052C
		private bool IsValid(string s)
		{
			if (s == null || s.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < s.Length; i++)
			{
				ushort num = (ushort)s[i];
				if (num < 33 || num > 126)
				{
					return false;
				}
				if (num == 42 && s.Length > 1 && (i > 0 || s[i + 1] != '.'))
				{
					return false;
				}
				if (!SiteIdentityPermission.valid[(int)(num - 33)])
				{
					return false;
				}
			}
			return s.Length != 1 || s[0] != '.';
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000A23B4 File Offset: 0x000A05B4
		private bool Match(string target)
		{
			if (this._site == null || target == null)
			{
				return false;
			}
			int num = this._site.IndexOf('*');
			int num2 = target.IndexOf('*');
			if (num == -1 && num2 == -1)
			{
				return this._site == target;
			}
			if (num == -1)
			{
				return this._site.EndsWith(target.Substring(num2 + 1));
			}
			if (num2 == -1)
			{
				return target.EndsWith(this._site.Substring(num + 1));
			}
			string text = this._site.Substring(num + 1);
			target = target.Substring(num2 + 1);
			if (text.Length > target.Length)
			{
				return text.EndsWith(target);
			}
			return target.EndsWith(text);
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000A2463 File Offset: 0x000A0663
		// Note: this type is marked as 'beforefieldinit'.
		static SiteIdentityPermission()
		{
		}

		// Token: 0x040020A9 RID: 8361
		private const int version = 1;

		// Token: 0x040020AA RID: 8362
		private string _site;

		// Token: 0x040020AB RID: 8363
		private static bool[] valid = new bool[]
		{
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			true,
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			false,
			true,
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			true,
			true
		};
	}
}
