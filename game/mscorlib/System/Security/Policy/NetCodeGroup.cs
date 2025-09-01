using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Grants Web permission to the site from which the assembly was downloaded. This class cannot be inherited.</summary>
	// Token: 0x02000419 RID: 1049
	[ComVisible(true)]
	[Serializable]
	public sealed class NetCodeGroup : CodeGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.NetCodeGroup" /> class.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies code access security policy.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="membershipCondition" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid.</exception>
		// Token: 0x06002ACA RID: 10954 RVA: 0x0009A83E File Offset: 0x00098A3E
		public NetCodeGroup(IMembershipCondition membershipCondition) : base(membershipCondition, null)
		{
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x0009A853 File Offset: 0x00098A53
		internal NetCodeGroup(SecurityElement e, PolicyLevel level) : base(e, level)
		{
		}

		/// <summary>Gets a string representation of the attributes of the policy statement for the code group.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x0000AF5E File Offset: 0x0000915E
		public override string AttributeString
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the logic to use for merging groups.</summary>
		/// <returns>The string "Union".</returns>
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x00099C70 File Offset: 0x00097E70
		public override string MergeLogic
		{
			get
			{
				return "Union";
			}
		}

		/// <summary>Gets the name of the <see cref="T:System.Security.NamedPermissionSet" /> for the code group.</summary>
		/// <returns>Always the string "Same site Web."</returns>
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06002ACE RID: 10958 RVA: 0x0009A868 File Offset: 0x00098A68
		public override string PermissionSetName
		{
			get
			{
				return "Same site Web";
			}
		}

		/// <summary>Adds the specified connection access to the current code group.</summary>
		/// <param name="originScheme">A <see cref="T:System.String" /> containing the scheme to match against the code's scheme.</param>
		/// <param name="connectAccess">A <see cref="T:System.Security.Policy.CodeConnectAccess" /> that specifies the scheme and port code can use to connect back to its origin server.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="originScheme" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="originScheme" /> contains characters that are not permitted in schemes.  
		/// -or-  
		/// <paramref name="originScheme" /> = <see cref="F:System.Security.Policy.NetCodeGroup.AbsentOriginScheme" /> and <paramref name="connectAccess" /> specifies <see cref="F:System.Security.Policy.CodeConnectAccess.OriginScheme" /> as its scheme.</exception>
		// Token: 0x06002ACF RID: 10959 RVA: 0x0009A870 File Offset: 0x00098A70
		[MonoTODO("(2.0) missing validations")]
		public void AddConnectAccess(string originScheme, CodeConnectAccess connectAccess)
		{
			if (originScheme == null)
			{
				throw new ArgumentException("originScheme");
			}
			if (originScheme == NetCodeGroup.AbsentOriginScheme && connectAccess.Scheme == CodeConnectAccess.OriginScheme)
			{
				throw new ArgumentOutOfRangeException("connectAccess", Locale.GetText("Schema == CodeConnectAccess.OriginScheme"));
			}
			if (this._rules.ContainsKey(originScheme))
			{
				if (connectAccess != null)
				{
					CodeConnectAccess[] array = (CodeConnectAccess[])this._rules[originScheme];
					CodeConnectAccess[] array2 = new CodeConnectAccess[array.Length + 1];
					Array.Copy(array, 0, array2, 0, array.Length);
					array2[array.Length] = connectAccess;
					this._rules[originScheme] = array2;
					return;
				}
			}
			else
			{
				CodeConnectAccess[] value = new CodeConnectAccess[]
				{
					connectAccess
				};
				this._rules.Add(originScheme, value);
			}
		}

		/// <summary>Makes a deep copy of the current code group.</summary>
		/// <returns>An equivalent copy of the current code group, including its membership conditions and child code groups.</returns>
		// Token: 0x06002AD0 RID: 10960 RVA: 0x0009A928 File Offset: 0x00098B28
		public override CodeGroup Copy()
		{
			NetCodeGroup netCodeGroup = new NetCodeGroup(base.MembershipCondition);
			netCodeGroup.Name = base.Name;
			netCodeGroup.Description = base.Description;
			netCodeGroup.PolicyStatement = base.PolicyStatement;
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				netCodeGroup.AddChild(codeGroup.Copy());
			}
			return netCodeGroup;
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x0009A9B8 File Offset: 0x00098BB8
		private bool Equals(CodeConnectAccess[] rules1, CodeConnectAccess[] rules2)
		{
			for (int i = 0; i < rules1.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < rules2.Length; j++)
				{
					if (rules1[i].Equals(rules2[j]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group.</summary>
		/// <param name="o">The <see cref="T:System.Security.Policy.NetCodeGroup" /> object to compare with the current code group.</param>
		/// <returns>
		///   <see langword="true" /> if the specified code group is equivalent to the current code group; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002AD2 RID: 10962 RVA: 0x0009A9FC File Offset: 0x00098BFC
		public override bool Equals(object o)
		{
			if (!base.Equals(o))
			{
				return false;
			}
			NetCodeGroup netCodeGroup = o as NetCodeGroup;
			if (netCodeGroup == null)
			{
				return false;
			}
			foreach (object obj in this._rules)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				CodeConnectAccess[] array = (CodeConnectAccess[])netCodeGroup._rules[dictionaryEntry.Key];
				bool flag;
				if (array != null)
				{
					flag = this.Equals((CodeConnectAccess[])dictionaryEntry.Value, array);
				}
				else
				{
					flag = (dictionaryEntry.Value == null);
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the connection access information for the current code group.</summary>
		/// <returns>A <see cref="T:System.Collections.DictionaryEntry" /> array containing connection access information.</returns>
		// Token: 0x06002AD3 RID: 10963 RVA: 0x0009AAB8 File Offset: 0x00098CB8
		public DictionaryEntry[] GetConnectAccessRules()
		{
			DictionaryEntry[] array = new DictionaryEntry[this._rules.Count];
			this._rules.CopyTo(array, 0);
			return array;
		}

		/// <summary>Gets the hash code of the current code group.</summary>
		/// <returns>The hash code of the current code group.</returns>
		// Token: 0x06002AD4 RID: 10964 RVA: 0x0009AAE4 File Offset: 0x00098CE4
		public override int GetHashCode()
		{
			if (this._hashcode == 0)
			{
				this._hashcode = base.GetHashCode();
				foreach (object obj in this._rules)
				{
					CodeConnectAccess[] array = (CodeConnectAccess[])((DictionaryEntry)obj).Value;
					if (array != null)
					{
						foreach (CodeConnectAccess codeConnectAccess in array)
						{
							this._hashcode ^= codeConnectAccess.GetHashCode();
						}
					}
				}
			}
			return this._hashcode;
		}

		/// <summary>Resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> for the assembly.</param>
		/// <returns>A <see cref="T:System.Security.Policy.PolicyStatement" /> that consists of the permissions granted by the code group with optional attributes, or <see langword="null" /> if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">More than one code group (including the parent code group and any child code groups) is marked <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />.</exception>
		// Token: 0x06002AD5 RID: 10965 RVA: 0x0009AB94 File Offset: 0x00098D94
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
			PolicyStatement policyStatement2 = base.PolicyStatement.Copy();
			policyStatement2.PermissionSet = permissionSet;
			return policyStatement2;
		}

		/// <summary>Removes all connection access information for the current code group.</summary>
		// Token: 0x06002AD6 RID: 10966 RVA: 0x0009AC60 File Offset: 0x00098E60
		public void ResetConnectAccess()
		{
			this._rules.Clear();
		}

		/// <summary>Resolves matching code groups.</summary>
		/// <param name="evidence">The evidence for the assembly.</param>
		/// <returns>The complete set of code groups that were matched by the evidence.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002AD7 RID: 10967 RVA: 0x0009AC70 File Offset: 0x00098E70
		public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			CodeGroup codeGroup = null;
			if (base.MembershipCondition.Check(evidence))
			{
				codeGroup = this.Copy();
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup2 = ((CodeGroup)obj).ResolveMatchingCodeGroups(evidence);
					if (codeGroup2 != null)
					{
						codeGroup.AddChild(codeGroup2);
					}
				}
			}
			return codeGroup;
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x0009ACF8 File Offset: 0x00098EF8
		[MonoTODO("(2.0) Add new stuff (CodeConnectAccess) into XML")]
		protected override void CreateXml(SecurityElement element, PolicyLevel level)
		{
			base.CreateXml(element, level);
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x0009AD02 File Offset: 0x00098F02
		[MonoTODO("(2.0) Parse new stuff (CodeConnectAccess) from XML")]
		protected override void ParseXml(SecurityElement e, PolicyLevel level)
		{
			base.ParseXml(e, level);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x0009AD0C File Offset: 0x00098F0C
		// Note: this type is marked as 'beforefieldinit'.
		static NetCodeGroup()
		{
		}

		/// <summary>Contains a value used to specify connection access for code with an unknown or unrecognized origin scheme.</summary>
		// Token: 0x04001FA1 RID: 8097
		public static readonly string AbsentOriginScheme = string.Empty;

		/// <summary>Contains a value used to specify any other unspecified origin scheme.</summary>
		// Token: 0x04001FA2 RID: 8098
		public static readonly string AnyOtherOriginScheme = "*";

		// Token: 0x04001FA3 RID: 8099
		private Hashtable _rules = new Hashtable();

		// Token: 0x04001FA4 RID: 8100
		private int _hashcode;
	}
}
