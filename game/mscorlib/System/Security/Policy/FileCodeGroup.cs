using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Grants permission to manipulate files located in the code assemblies to code assemblies that match the membership condition. This class cannot be inherited.</summary>
	// Token: 0x0200040E RID: 1038
	[ComVisible(true)]
	[Serializable]
	public sealed class FileCodeGroup : CodeGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.FileCodeGroup" /> class.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies policy.</param>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values. This value is used to construct the <see cref="T:System.Security.Permissions.FileIOPermission" /> that is granted.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="membershipCondition" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid.  
		///  -or-  
		///  The type of the <paramref name="access" /> parameter is not valid.</exception>
		// Token: 0x06002A76 RID: 10870 RVA: 0x00099BC6 File Offset: 0x00097DC6
		public FileCodeGroup(IMembershipCondition membershipCondition, FileIOPermissionAccess access) : base(membershipCondition, null)
		{
			this.m_access = access;
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x00099BD7 File Offset: 0x00097DD7
		internal FileCodeGroup(SecurityElement e, PolicyLevel level) : base(e, level)
		{
		}

		/// <summary>Makes a deep copy of the current code group.</summary>
		/// <returns>An equivalent copy of the current code group, including its membership conditions and child code groups.</returns>
		// Token: 0x06002A78 RID: 10872 RVA: 0x00099BE4 File Offset: 0x00097DE4
		public override CodeGroup Copy()
		{
			FileCodeGroup fileCodeGroup = new FileCodeGroup(base.MembershipCondition, this.m_access);
			fileCodeGroup.Name = base.Name;
			fileCodeGroup.Description = base.Description;
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				fileCodeGroup.AddChild(codeGroup.Copy());
			}
			return fileCodeGroup;
		}

		/// <summary>Gets the merge logic.</summary>
		/// <returns>The string "Union".</returns>
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x00099C70 File Offset: 0x00097E70
		public override string MergeLogic
		{
			get
			{
				return "Union";
			}
		}

		/// <summary>Resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <param name="evidence">The evidence for the assembly.</param>
		/// <returns>A policy statement consisting of the permissions granted by the code group with optional attributes, or <see langword="null" /> if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">The current policy is <see langword="null" />.  
		///  -or-  
		///  More than one code group (including the parent code group and all child code groups) is marked <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />.</exception>
		// Token: 0x06002A7A RID: 10874 RVA: 0x00099C78 File Offset: 0x00097E78
		public override PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (!base.MembershipCondition.Check(evidence))
			{
				return null;
			}
			PermissionSet permissionSet = null;
			if (base.PolicyStatement == null)
			{
				permissionSet = new PermissionSet(PermissionState.None);
			}
			else
			{
				permissionSet = base.PolicyStatement.PermissionSet.Copy();
			}
			if (base.Children.Count > 0)
			{
				foreach (object obj in base.Children)
				{
					PolicyStatement policyStatement = ((CodeGroup)obj).Resolve(evidence);
					if (policyStatement != null)
					{
						permissionSet = permissionSet.Union(policyStatement.PermissionSet);
					}
				}
			}
			PolicyStatement policyStatement2;
			if (base.PolicyStatement != null)
			{
				policyStatement2 = base.PolicyStatement.Copy();
			}
			else
			{
				policyStatement2 = PolicyStatement.Empty();
			}
			policyStatement2.PermissionSet = permissionSet;
			return policyStatement2;
		}

		/// <summary>Resolves matching code groups.</summary>
		/// <param name="evidence">The evidence for the assembly.</param>
		/// <returns>A <see cref="T:System.Security.Policy.CodeGroup" /> that is the root of the tree of matching code groups.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002A7B RID: 10875 RVA: 0x00099D5C File Offset: 0x00097F5C
		public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (!base.MembershipCondition.Check(evidence))
			{
				return null;
			}
			FileCodeGroup fileCodeGroup = new FileCodeGroup(base.MembershipCondition, this.m_access);
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = ((CodeGroup)obj).ResolveMatchingCodeGroups(evidence);
				if (codeGroup != null)
				{
					fileCodeGroup.AddChild(codeGroup);
				}
			}
			return fileCodeGroup;
		}

		/// <summary>Gets a string representation of the attributes of the policy statement for the code group.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06002A7C RID: 10876 RVA: 0x0000AF5E File Offset: 0x0000915E
		public override string AttributeString
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the name of the named permission set for the code group.</summary>
		/// <returns>The concatenatation of the string "Same directory FileIO - " and the access type.</returns>
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x00099DF0 File Offset: 0x00097FF0
		public override string PermissionSetName
		{
			get
			{
				return "Same directory FileIO - " + this.m_access.ToString();
			}
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group.</summary>
		/// <param name="o">The code group to compare with the current code group.</param>
		/// <returns>
		///   <see langword="true" /> if the specified code group is equivalent to the current code group; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002A7E RID: 10878 RVA: 0x00099E0D File Offset: 0x0009800D
		public override bool Equals(object o)
		{
			return o is FileCodeGroup && this.m_access == ((FileCodeGroup)o).m_access && base.Equals((CodeGroup)o, false);
		}

		/// <summary>Gets the hash code of the current code group.</summary>
		/// <returns>The hash code of the current code group.</returns>
		// Token: 0x06002A7F RID: 10879 RVA: 0x00099E3B File Offset: 0x0009803B
		public override int GetHashCode()
		{
			return this.m_access.GetHashCode();
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x00099E50 File Offset: 0x00098050
		protected override void ParseXml(SecurityElement e, PolicyLevel level)
		{
			string text = e.Attribute("Access");
			if (text != null)
			{
				this.m_access = (FileIOPermissionAccess)Enum.Parse(typeof(FileIOPermissionAccess), text, true);
				return;
			}
			this.m_access = FileIOPermissionAccess.NoAccess;
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x00099E90 File Offset: 0x00098090
		protected override void CreateXml(SecurityElement element, PolicyLevel level)
		{
			element.AddAttribute("Access", this.m_access.ToString());
		}

		// Token: 0x04001F96 RID: 8086
		private FileIOPermissionAccess m_access;
	}
}
