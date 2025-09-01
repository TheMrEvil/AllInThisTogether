using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Allows security policy to be defined by the union of the policy statement of a code group and that of the first child code group that matches. This class cannot be inherited.</summary>
	// Token: 0x0200040F RID: 1039
	[ComVisible(true)]
	[Serializable]
	public sealed class FirstMatchCodeGroup : CodeGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.FirstMatchCodeGroup" /> class.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies policy.</param>
		/// <param name="policy">The policy statement for the code group in the form of a permission set and attributes to grant code that matches the membership condition.</param>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid.  
		///  -or-  
		///  The type of the <paramref name="policy" /> parameter is not valid.</exception>
		// Token: 0x06002A82 RID: 10882 RVA: 0x00099EAE File Offset: 0x000980AE
		public FirstMatchCodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy) : base(membershipCondition, policy)
		{
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x00099BD7 File Offset: 0x00097DD7
		internal FirstMatchCodeGroup(SecurityElement e, PolicyLevel level) : base(e, level)
		{
		}

		/// <summary>Gets the merge logic.</summary>
		/// <returns>The string "First Match".</returns>
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x00099EB8 File Offset: 0x000980B8
		public override string MergeLogic
		{
			get
			{
				return "First Match";
			}
		}

		/// <summary>Makes a deep copy of the code group.</summary>
		/// <returns>An equivalent copy of the code group, including its membership conditions and child code groups.</returns>
		// Token: 0x06002A85 RID: 10885 RVA: 0x00099EC0 File Offset: 0x000980C0
		public override CodeGroup Copy()
		{
			FirstMatchCodeGroup firstMatchCodeGroup = this.CopyNoChildren();
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				firstMatchCodeGroup.AddChild(codeGroup.Copy());
			}
			return firstMatchCodeGroup;
		}

		/// <summary>Resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <param name="evidence">The evidence for the assembly.</param>
		/// <returns>A policy statement consisting of the permissions granted by the code group with optional attributes, or <see langword="null" /> if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">More than one code group (including the parent code group and any child code groups) is marked <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />.</exception>
		// Token: 0x06002A86 RID: 10886 RVA: 0x00099F28 File Offset: 0x00098128
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
			foreach (object obj in base.Children)
			{
				PolicyStatement policyStatement = ((CodeGroup)obj).Resolve(evidence);
				if (policyStatement != null)
				{
					return policyStatement;
				}
			}
			return base.PolicyStatement;
		}

		/// <summary>Resolves matching code groups.</summary>
		/// <param name="evidence">The evidence for the assembly.</param>
		/// <returns>A <see cref="T:System.Security.Policy.CodeGroup" /> that is the root of the tree of matching code groups.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002A87 RID: 10887 RVA: 0x00099FB0 File Offset: 0x000981B0
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
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				if (codeGroup.Resolve(evidence) != null)
				{
					return codeGroup.Copy();
				}
			}
			return this.CopyNoChildren();
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x0009A03C File Offset: 0x0009823C
		private FirstMatchCodeGroup CopyNoChildren()
		{
			return new FirstMatchCodeGroup(base.MembershipCondition, base.PolicyStatement)
			{
				Name = base.Name,
				Description = base.Description
			};
		}
	}
}
