using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Represents the statement of a <see cref="T:System.Security.Policy.CodeGroup" /> describing the permissions and other information that apply to code with a particular set of evidence. This class cannot be inherited.</summary>
	// Token: 0x0200041D RID: 1053
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyStatement : ISecurityEncodable, ISecurityPolicyEncodable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyStatement" /> class with the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.PermissionSet" /> with which to initialize the new instance.</param>
		// Token: 0x06002B0A RID: 11018 RVA: 0x0009BFC8 File Offset: 0x0009A1C8
		public PolicyStatement(PermissionSet permSet) : this(permSet, PolicyStatementAttribute.Nothing)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyStatement" /> class with the specified <see cref="T:System.Security.PermissionSet" /> and attributes.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.PermissionSet" /> with which to initialize the new instance.</param>
		/// <param name="attributes">A bitwise combination of the <see cref="T:System.Security.Policy.PolicyStatementAttribute" /> values.</param>
		// Token: 0x06002B0B RID: 11019 RVA: 0x0009BFD2 File Offset: 0x0009A1D2
		public PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes)
		{
			if (permSet != null)
			{
				this.perms = permSet.Copy();
				this.perms.SetReadOnly(true);
			}
			this.attrs = attributes;
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.PermissionSet" /> of the policy statement.</summary>
		/// <returns>The <see cref="T:System.Security.PermissionSet" /> of the policy statement.</returns>
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x0009BFFC File Offset: 0x0009A1FC
		// (set) Token: 0x06002B0D RID: 11021 RVA: 0x0009C024 File Offset: 0x0009A224
		public PermissionSet PermissionSet
		{
			get
			{
				if (this.perms == null)
				{
					this.perms = new PermissionSet(PermissionState.None);
					this.perms.SetReadOnly(true);
				}
				return this.perms;
			}
			set
			{
				this.perms = value;
			}
		}

		/// <summary>Gets or sets the attributes of the policy statement.</summary>
		/// <returns>The attributes of the policy statement.</returns>
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x0009C02D File Offset: 0x0009A22D
		// (set) Token: 0x06002B0F RID: 11023 RVA: 0x0009C035 File Offset: 0x0009A235
		public PolicyStatementAttribute Attributes
		{
			get
			{
				return this.attrs;
			}
			set
			{
				if (value <= PolicyStatementAttribute.All)
				{
					this.attrs = value;
					return;
				}
				throw new ArgumentException(string.Format(Locale.GetText("Invalid value for {0}."), "PolicyStatementAttribute"));
			}
		}

		/// <summary>Gets a string representation of the attributes of the policy statement.</summary>
		/// <returns>A text string representing the attributes of the policy statement.</returns>
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x0009C05C File Offset: 0x0009A25C
		public string AttributeString
		{
			get
			{
				switch (this.attrs)
				{
				case PolicyStatementAttribute.Exclusive:
					return "Exclusive";
				case PolicyStatementAttribute.LevelFinal:
					return "LevelFinal";
				case PolicyStatementAttribute.All:
					return "Exclusive LevelFinal";
				default:
					return string.Empty;
				}
			}
		}

		/// <summary>Creates an equivalent copy of the current policy statement.</summary>
		/// <returns>A new copy of the <see cref="T:System.Security.Policy.PolicyStatement" /> with <see cref="P:System.Security.Policy.PolicyStatement.PermissionSet" /> and <see cref="P:System.Security.Policy.PolicyStatement.Attributes" /> identical to those of the current <see cref="T:System.Security.Policy.PolicyStatement" />.</returns>
		// Token: 0x06002B11 RID: 11025 RVA: 0x0009C09D File Offset: 0x0009A29D
		public PolicyStatement Copy()
		{
			return new PolicyStatement(this.perms, this.attrs);
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid <see cref="T:System.Security.Policy.PolicyStatement" /> encoding.</exception>
		// Token: 0x06002B12 RID: 11026 RVA: 0x0009C0B0 File Offset: 0x0009A2B0
		public void FromXml(SecurityElement et)
		{
			this.FromXml(et, null);
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for lookup of <see cref="T:System.Security.NamedPermissionSet" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid <see cref="T:System.Security.Policy.PolicyStatement" /> encoding.</exception>
		// Token: 0x06002B13 RID: 11027 RVA: 0x0009C0BC File Offset: 0x0009A2BC
		[SecuritySafeCritical]
		public void FromXml(SecurityElement et, PolicyLevel level)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (et.Tag != "PolicyStatement")
			{
				throw new ArgumentException(Locale.GetText("Invalid tag."));
			}
			string text = et.Attribute("Attributes");
			if (text != null)
			{
				this.attrs = (PolicyStatementAttribute)Enum.Parse(typeof(PolicyStatementAttribute), text);
			}
			SecurityElement et2 = et.SearchForChildByTag("PermissionSet");
			this.PermissionSet.FromXml(et2);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002B14 RID: 11028 RVA: 0x0009C13B File Offset: 0x0009A33B
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for lookup of <see cref="T:System.Security.NamedPermissionSet" /> values.</param>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002B15 RID: 11029 RVA: 0x0009C144 File Offset: 0x0009A344
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("PolicyStatement");
			securityElement.AddAttribute("version", "1");
			if (this.attrs != PolicyStatementAttribute.Nothing)
			{
				securityElement.AddAttribute("Attributes", this.attrs.ToString());
			}
			securityElement.AddChild(this.PermissionSet.ToXml());
			return securityElement;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Policy.PolicyStatement" /> object is equal to the current <see cref="T:System.Security.Policy.PolicyStatement" />.</summary>
		/// <param name="obj">The <see cref="T:System.Security.Policy.PolicyStatement" /> object to compare with the current <see cref="T:System.Security.Policy.PolicyStatement" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Security.Policy.PolicyStatement" /> is equal to the current <see cref="T:System.Security.Policy.PolicyStatement" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B16 RID: 11030 RVA: 0x0009C1A4 File Offset: 0x0009A3A4
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PolicyStatement policyStatement = obj as PolicyStatement;
			return policyStatement != null && this.PermissionSet.Equals(obj) && this.attrs == policyStatement.attrs;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.Policy.PolicyStatement" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Policy.PolicyStatement" /> object.</returns>
		// Token: 0x06002B17 RID: 11031 RVA: 0x0009C1E0 File Offset: 0x0009A3E0
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.PermissionSet.GetHashCode() ^ (int)this.attrs;
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000985A0 File Offset: 0x000967A0
		internal static PolicyStatement Empty()
		{
			return new PolicyStatement(new PermissionSet(PermissionState.None));
		}

		// Token: 0x04001FB0 RID: 8112
		private PermissionSet perms;

		// Token: 0x04001FB1 RID: 8113
		private PolicyStatementAttribute attrs;
	}
}
