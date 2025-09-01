using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Represents the abstract base class from which all implementations of code groups must derive.</summary>
	// Token: 0x02000407 RID: 1031
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeGroup
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Policy.CodeGroup" />.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies policy.</param>
		/// <param name="policy">The policy statement for the code group in the form of a permission set and attributes to grant code that matches the membership condition.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="membershipCondition" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid.  
		///  -or-  
		///  The type of the <paramref name="policy" /> parameter is not valid.</exception>
		// Token: 0x06002A23 RID: 10787 RVA: 0x00098A6A File Offset: 0x00096C6A
		protected CodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
		{
			if (membershipCondition == null)
			{
				throw new ArgumentNullException("membershipCondition");
			}
			if (policy != null)
			{
				this.m_policy = policy.Copy();
			}
			this.m_membershipCondition = membershipCondition.Copy();
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x00098AA6 File Offset: 0x00096CA6
		internal CodeGroup(SecurityElement e, PolicyLevel level)
		{
			this.FromXml(e, level);
		}

		/// <summary>When overridden in a derived class, makes a deep copy of the current code group.</summary>
		/// <returns>An equivalent copy of the current code group, including its membership conditions and child code groups.</returns>
		// Token: 0x06002A25 RID: 10789
		public abstract CodeGroup Copy();

		/// <summary>When overridden in a derived class, gets the merge logic for the code group.</summary>
		/// <returns>A description of the merge logic for the code group.</returns>
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06002A26 RID: 10790
		public abstract string MergeLogic { get; }

		/// <summary>When overridden in a derived class, resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <param name="evidence">The evidence for the assembly.</param>
		/// <returns>A policy statement that consists of the permissions granted by the code group with optional attributes, or <see langword="null" /> if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		// Token: 0x06002A27 RID: 10791
		public abstract PolicyStatement Resolve(Evidence evidence);

		/// <summary>When overridden in a derived class, resolves matching code groups.</summary>
		/// <param name="evidence">The evidence for the assembly.</param>
		/// <returns>A <see cref="T:System.Security.Policy.CodeGroup" /> that is the root of the tree of matching code groups.</returns>
		// Token: 0x06002A28 RID: 10792
		public abstract CodeGroup ResolveMatchingCodeGroups(Evidence evidence);

		/// <summary>Gets or sets the policy statement associated with the code group.</summary>
		/// <returns>The policy statement for the code group.</returns>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x00098AC1 File Offset: 0x00096CC1
		// (set) Token: 0x06002A2A RID: 10794 RVA: 0x00098AC9 File Offset: 0x00096CC9
		public PolicyStatement PolicyStatement
		{
			get
			{
				return this.m_policy;
			}
			set
			{
				this.m_policy = value;
			}
		}

		/// <summary>Gets or sets the description of the code group.</summary>
		/// <returns>The description of the code group.</returns>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x00098AD2 File Offset: 0x00096CD2
		// (set) Token: 0x06002A2C RID: 10796 RVA: 0x00098ADA File Offset: 0x00096CDA
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		/// <summary>Gets or sets the code group's membership condition.</summary>
		/// <returns>The membership condition that determines to which evidence the code group is applicable.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set this parameter to <see langword="null" />.</exception>
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x00098AE3 File Offset: 0x00096CE3
		// (set) Token: 0x06002A2E RID: 10798 RVA: 0x00098AEB File Offset: 0x00096CEB
		public IMembershipCondition MembershipCondition
		{
			get
			{
				return this.m_membershipCondition;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("value");
				}
				this.m_membershipCondition = value;
			}
		}

		/// <summary>Gets or sets the name of the code group.</summary>
		/// <returns>The name of the code group.</returns>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06002A2F RID: 10799 RVA: 0x00098B02 File Offset: 0x00096D02
		// (set) Token: 0x06002A30 RID: 10800 RVA: 0x00098B0A File Offset: 0x00096D0A
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		/// <summary>Gets or sets an ordered list of the child code groups of a code group.</summary>
		/// <returns>A list of child code groups.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set this property to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property with a list of children that are not <see cref="T:System.Security.Policy.CodeGroup" /> objects.</exception>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x00098B13 File Offset: 0x00096D13
		// (set) Token: 0x06002A32 RID: 10802 RVA: 0x00098B1B File Offset: 0x00096D1B
		public IList Children
		{
			get
			{
				return this.m_children;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_children = new ArrayList(value);
			}
		}

		/// <summary>Gets a string representation of the attributes of the policy statement for the code group.</summary>
		/// <returns>A string representation of the attributes of the policy statement for the code group.</returns>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06002A33 RID: 10803 RVA: 0x00098B37 File Offset: 0x00096D37
		public virtual string AttributeString
		{
			get
			{
				if (this.m_policy != null)
				{
					return this.m_policy.AttributeString;
				}
				return null;
			}
		}

		/// <summary>Gets the name of the named permission set for the code group.</summary>
		/// <returns>The name of a named permission set of the policy level.</returns>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06002A34 RID: 10804 RVA: 0x00098B4E File Offset: 0x00096D4E
		public virtual string PermissionSetName
		{
			get
			{
				if (this.m_policy == null)
				{
					return null;
				}
				if (this.m_policy.PermissionSet is NamedPermissionSet)
				{
					return ((NamedPermissionSet)this.m_policy.PermissionSet).Name;
				}
				return null;
			}
		}

		/// <summary>Adds a child code group to the current code group.</summary>
		/// <param name="group">The code group to be added as a child. This new child code group is added to the end of the list.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="group" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="group" /> parameter is not a valid code group.</exception>
		// Token: 0x06002A35 RID: 10805 RVA: 0x00098B83 File Offset: 0x00096D83
		public void AddChild(CodeGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.m_children.Add(group.Copy());
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group.</summary>
		/// <param name="o">The code group to compare with the current code group.</param>
		/// <returns>
		///   <see langword="true" /> if the specified code group is equivalent to the current code group; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002A36 RID: 10806 RVA: 0x00098BA8 File Offset: 0x00096DA8
		public override bool Equals(object o)
		{
			CodeGroup codeGroup = o as CodeGroup;
			return codeGroup != null && this.Equals(codeGroup, false);
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group, checking the child code groups as well, if specified.</summary>
		/// <param name="cg">The code group to compare with the current code group.</param>
		/// <param name="compareChildren">
		///   <see langword="true" /> to compare child code groups, as well; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified code group is equivalent to the current code group; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002A37 RID: 10807 RVA: 0x00098BCC File Offset: 0x00096DCC
		public bool Equals(CodeGroup cg, bool compareChildren)
		{
			if (cg.Name != this.Name)
			{
				return false;
			}
			if (cg.Description != this.Description)
			{
				return false;
			}
			if (!cg.MembershipCondition.Equals(this.m_membershipCondition))
			{
				return false;
			}
			if (compareChildren)
			{
				int count = cg.Children.Count;
				if (this.Children.Count != count)
				{
					return false;
				}
				for (int i = 0; i < count; i++)
				{
					if (!((CodeGroup)this.Children[i]).Equals((CodeGroup)cg.Children[i], false))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Removes the specified child code group.</summary>
		/// <param name="group">The code group to be removed as a child.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="group" /> parameter is not an immediate child code group of the current code group.</exception>
		// Token: 0x06002A38 RID: 10808 RVA: 0x00098C70 File Offset: 0x00096E70
		public void RemoveChild(CodeGroup group)
		{
			if (group != null)
			{
				this.m_children.Remove(group);
			}
		}

		/// <summary>Gets the hash code of the current code group.</summary>
		/// <returns>The hash code of the current code group.</returns>
		// Token: 0x06002A39 RID: 10809 RVA: 0x00098C84 File Offset: 0x00096E84
		public override int GetHashCode()
		{
			int num = this.m_membershipCondition.GetHashCode();
			if (this.m_policy != null)
			{
				num += this.m_policy.GetHashCode();
			}
			return num;
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002A3A RID: 10810 RVA: 0x00098CB4 File Offset: 0x00096EB4
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a given state and policy level from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The policy level within which the code group exists.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002A3B RID: 10811 RVA: 0x00098CC0 File Offset: 0x00096EC0
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			string text = e.Attribute("PermissionSetName");
			PermissionSet permissionSet;
			if (text != null && level != null)
			{
				permissionSet = level.GetNamedPermissionSet(text);
			}
			else
			{
				SecurityElement securityElement = e.SearchForChildByTag("PermissionSet");
				if (securityElement != null)
				{
					permissionSet = (PermissionSet)Activator.CreateInstance(Type.GetType(securityElement.Attribute("class")), true);
					permissionSet.FromXml(securityElement);
				}
				else
				{
					permissionSet = new PermissionSet(new PermissionSet(PermissionState.None));
				}
			}
			this.m_policy = new PolicyStatement(permissionSet);
			this.m_children.Clear();
			if (e.Children != null && e.Children.Count > 0)
			{
				foreach (object obj in e.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					if (securityElement2.Tag == "CodeGroup")
					{
						this.AddChild(CodeGroup.CreateFromXml(securityElement2, level));
					}
				}
			}
			this.m_membershipCondition = null;
			SecurityElement securityElement3 = e.SearchForChildByTag("IMembershipCondition");
			if (securityElement3 != null)
			{
				string text2 = securityElement3.Attribute("class");
				Type type = Type.GetType(text2);
				if (type == null)
				{
					type = Type.GetType("System.Security.Policy." + text2);
				}
				this.m_membershipCondition = (IMembershipCondition)Activator.CreateInstance(type, true);
				this.m_membershipCondition.FromXml(securityElement3, level);
			}
			this.m_name = e.Attribute("Name");
			this.m_description = e.Attribute("Description");
			this.ParseXml(e, level);
		}

		/// <summary>When overridden in a derived class, reconstructs properties and internal state specific to a derived code group from the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The policy level within which the code group exists.</param>
		// Token: 0x06002A3C RID: 10812 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void ParseXml(SecurityElement e, PolicyLevel level)
		{
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002A3D RID: 10813 RVA: 0x00098E68 File Offset: 0x00097068
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object, its current state, and the policy level within which the code exists.</summary>
		/// <param name="level">The policy level within which the code group exists.</param>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002A3E RID: 10814 RVA: 0x00098E74 File Offset: 0x00097074
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("CodeGroup");
			securityElement.AddAttribute("class", base.GetType().AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			if (this.Name != null)
			{
				securityElement.AddAttribute("Name", this.Name);
			}
			if (this.Description != null)
			{
				securityElement.AddAttribute("Description", this.Description);
			}
			if (this.MembershipCondition != null)
			{
				securityElement.AddChild(this.MembershipCondition.ToXml());
			}
			if (this.PolicyStatement != null && this.PolicyStatement.PermissionSet != null)
			{
				securityElement.AddChild(this.PolicyStatement.PermissionSet.ToXml());
			}
			foreach (object obj in this.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				securityElement.AddChild(codeGroup.ToXml());
			}
			this.CreateXml(securityElement, level);
			return securityElement;
		}

		/// <summary>When overridden in a derived class, serializes properties and internal state specific to a derived code group and adds the serialization to the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="element">The XML encoding to which to add the serialization.</param>
		/// <param name="level">The policy level within which the code group exists.</param>
		// Token: 0x06002A3F RID: 10815 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void CreateXml(SecurityElement element, PolicyLevel level)
		{
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x00098F84 File Offset: 0x00097184
		internal static CodeGroup CreateFromXml(SecurityElement se, PolicyLevel level)
		{
			string text = se.Attribute("class");
			string text2 = text;
			int num = text2.IndexOf(",");
			if (num > 0)
			{
				text2 = text2.Substring(0, num);
			}
			num = text2.LastIndexOf(".");
			if (num > 0)
			{
				text2 = text2.Substring(num + 1);
			}
			if (text2 == "FileCodeGroup")
			{
				return new FileCodeGroup(se, level);
			}
			if (text2 == "FirstMatchCodeGroup")
			{
				return new FirstMatchCodeGroup(se, level);
			}
			if (text2 == "NetCodeGroup")
			{
				return new NetCodeGroup(se, level);
			}
			if (!(text2 == "UnionCodeGroup"))
			{
				CodeGroup codeGroup = (CodeGroup)Activator.CreateInstance(Type.GetType(text), true);
				codeGroup.FromXml(se, level);
				return codeGroup;
			}
			return new UnionCodeGroup(se, level);
		}

		// Token: 0x04001F6A RID: 8042
		private PolicyStatement m_policy;

		// Token: 0x04001F6B RID: 8043
		private IMembershipCondition m_membershipCondition;

		// Token: 0x04001F6C RID: 8044
		private string m_description;

		// Token: 0x04001F6D RID: 8045
		private string m_name;

		// Token: 0x04001F6E RID: 8046
		private ArrayList m_children = new ArrayList();
	}
}
