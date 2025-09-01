using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Xml;
using Unity;

namespace System.Security.Policy
{
	/// <summary>Represents the security policy levels for the common language runtime. This class cannot be inherited.</summary>
	// Token: 0x0200041C RID: 1052
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyLevel
	{
		// Token: 0x06002AE8 RID: 10984 RVA: 0x0009AEDE File Offset: 0x000990DE
		internal PolicyLevel(string label, PolicyLevelType type)
		{
			this.label = label;
			this._type = type;
			this.full_trust_assemblies = new ArrayList();
			this.named_permission_sets = new ArrayList();
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x0009AF0C File Offset: 0x0009910C
		internal void LoadFromFile(string filename)
		{
			try
			{
				if (!File.Exists(filename))
				{
					string text = filename + ".default";
					if (File.Exists(text))
					{
						File.Copy(text, filename);
					}
				}
				if (File.Exists(filename))
				{
					using (StreamReader streamReader = File.OpenText(filename))
					{
						this.xml = this.FromString(streamReader.ReadToEnd());
					}
					try
					{
						SecurityManager.ResolvingPolicyLevel = this;
						this.FromXml(this.xml);
						goto IL_8A;
					}
					finally
					{
						SecurityManager.ResolvingPolicyLevel = this;
					}
				}
				this.CreateDefaultFullTrustAssemblies();
				this.CreateDefaultNamedPermissionSets();
				this.CreateDefaultLevel(this._type);
				this.Save();
				IL_8A:;
			}
			catch
			{
			}
			finally
			{
				this._location = filename;
			}
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x0009AFE4 File Offset: 0x000991E4
		internal void LoadFromString(string xml)
		{
			this.FromXml(this.FromString(xml));
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x0009AFF4 File Offset: 0x000991F4
		private SecurityElement FromString(string xml)
		{
			SecurityParser securityParser = new SecurityParser();
			securityParser.LoadXml(xml);
			SecurityElement securityElement = securityParser.ToXml();
			if (securityElement.Tag != "configuration")
			{
				throw new ArgumentException(Locale.GetText("missing <configuration> root element"));
			}
			SecurityElement securityElement2 = (SecurityElement)securityElement.Children[0];
			if (securityElement2.Tag != "mscorlib")
			{
				throw new ArgumentException(Locale.GetText("missing <mscorlib> tag"));
			}
			SecurityElement securityElement3 = (SecurityElement)securityElement2.Children[0];
			if (securityElement3.Tag != "security")
			{
				throw new ArgumentException(Locale.GetText("missing <security> tag"));
			}
			SecurityElement securityElement4 = (SecurityElement)securityElement3.Children[0];
			if (securityElement4.Tag != "policy")
			{
				throw new ArgumentException(Locale.GetText("missing <policy> tag"));
			}
			return (SecurityElement)securityElement4.Children[0];
		}

		/// <summary>Gets a list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies used to evaluate security policy.</summary>
		/// <returns>A list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies used to evaluate security policy. These assemblies are granted full trust during security policy evaluation of assemblies not in the list.</returns>
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x0009B0DA File Offset: 0x000992DA
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public IList FullTrustAssemblies
		{
			get
			{
				return this.full_trust_assemblies;
			}
		}

		/// <summary>Gets a descriptive label for the policy level.</summary>
		/// <returns>The label associated with the policy level.</returns>
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x0009B0E2 File Offset: 0x000992E2
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		/// <summary>Gets a list of named permission sets defined for the policy level.</summary>
		/// <returns>A list of named permission sets defined for the policy level.</returns>
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x0009B0EA File Offset: 0x000992EA
		public IList NamedPermissionSets
		{
			get
			{
				return this.named_permission_sets;
			}
		}

		/// <summary>Gets or sets the root code group for the policy level.</summary>
		/// <returns>The <see cref="T:System.Security.Policy.CodeGroup" /> that is the root of the tree of policy level code groups.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for <see cref="P:System.Security.Policy.PolicyLevel.RootCodeGroup" /> is <see langword="null" />.</exception>
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x0009B0F2 File Offset: 0x000992F2
		// (set) Token: 0x06002AF0 RID: 10992 RVA: 0x0009B0FA File Offset: 0x000992FA
		public CodeGroup RootCodeGroup
		{
			get
			{
				return this.root_code_group;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.root_code_group = value;
			}
		}

		/// <summary>Gets the path where the policy file is stored.</summary>
		/// <returns>The path where the policy file is stored, or <see langword="null" /> if the <see cref="T:System.Security.Policy.PolicyLevel" /> does not have a storage location.</returns>
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x0009B111 File Offset: 0x00099311
		public string StoreLocation
		{
			get
			{
				return this._location;
			}
		}

		/// <summary>Gets the type of the policy level.</summary>
		/// <returns>One of the <see cref="T:System.Security.PolicyLevelType" /> values.</returns>
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x0009B119 File Offset: 0x00099319
		[ComVisible(false)]
		public PolicyLevelType Type
		{
			get
			{
				return this._type;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> corresponding to the specified <see cref="T:System.Security.Policy.StrongName" /> to the list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies that should not be evaluated.</summary>
		/// <param name="sn">The <see cref="T:System.Security.Policy.StrongName" /> used to create the <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> to add to the list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies that should not be evaluated.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sn" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Policy.StrongName" /> specified by the <paramref name="sn" /> parameter already has full trust.</exception>
		// Token: 0x06002AF3 RID: 10995 RVA: 0x0009B124 File Offset: 0x00099324
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public void AddFullTrustAssembly(StrongName sn)
		{
			if (sn == null)
			{
				throw new ArgumentNullException("sn");
			}
			StrongNameMembershipCondition snMC = new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version);
			this.AddFullTrustAssembly(snMC);
		}

		/// <summary>Adds the specified <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> to the list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies that should not be evaluated.</summary>
		/// <param name="snMC">The <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> to add to the list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies that should not be evaluated.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="snMC" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> specified by the <paramref name="snMC" /> parameter already has full trust.</exception>
		// Token: 0x06002AF4 RID: 10996 RVA: 0x0009B160 File Offset: 0x00099360
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public void AddFullTrustAssembly(StrongNameMembershipCondition snMC)
		{
			if (snMC == null)
			{
				throw new ArgumentNullException("snMC");
			}
			using (IEnumerator enumerator = this.full_trust_assemblies.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((StrongNameMembershipCondition)enumerator.Current).Equals(snMC))
					{
						throw new ArgumentException(Locale.GetText("sn already has full trust."));
					}
				}
			}
			this.full_trust_assemblies.Add(snMC);
		}

		/// <summary>Adds a <see cref="T:System.Security.NamedPermissionSet" /> to the current policy level.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.NamedPermissionSet" /> to add to the current policy level.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="permSet" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="permSet" /> parameter has the same name as an existing <see cref="T:System.Security.NamedPermissionSet" /> in the <see cref="T:System.Security.Policy.PolicyLevel" />.</exception>
		// Token: 0x06002AF5 RID: 10997 RVA: 0x0009B1E4 File Offset: 0x000993E4
		public void AddNamedPermissionSet(NamedPermissionSet permSet)
		{
			if (permSet == null)
			{
				throw new ArgumentNullException("permSet");
			}
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				if (permSet.Name == namedPermissionSet.Name)
				{
					throw new ArgumentException(Locale.GetText("This NamedPermissionSet is the same an existing NamedPermissionSet."));
				}
			}
			this.named_permission_sets.Add(permSet.Copy());
		}

		/// <summary>Replaces a <see cref="T:System.Security.NamedPermissionSet" /> in the current policy level with the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="name">The name of the <see cref="T:System.Security.NamedPermissionSet" /> to replace.</param>
		/// <param name="pSet">The <see cref="T:System.Security.PermissionSet" /> that replaces the <see cref="T:System.Security.NamedPermissionSet" /> specified by the <paramref name="name" /> parameter.</param>
		/// <returns>A copy of the <see cref="T:System.Security.NamedPermissionSet" /> that was replaced.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="pSet" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is equal to the name of a reserved permission set.  
		///  -or-  
		///  The <see cref="T:System.Security.PermissionSet" /> specified by the <paramref name="pSet" /> parameter cannot be found.</exception>
		// Token: 0x06002AF6 RID: 10998 RVA: 0x0009B27C File Offset: 0x0009947C
		public NamedPermissionSet ChangeNamedPermissionSet(string name, PermissionSet pSet)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (pSet == null)
			{
				throw new ArgumentNullException("pSet");
			}
			if (DefaultPolicies.ReservedNames.IsReserved(name))
			{
				throw new ArgumentException(Locale.GetText("Reserved name"));
			}
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				if (name == namedPermissionSet.Name)
				{
					this.named_permission_sets.Remove(namedPermissionSet);
					this.AddNamedPermissionSet(new NamedPermissionSet(name, pSet));
					return namedPermissionSet;
				}
			}
			throw new ArgumentException(Locale.GetText("PermissionSet not found"));
		}

		/// <summary>Creates a new policy level for use at the application domain policy level.</summary>
		/// <returns>The newly created <see cref="T:System.Security.Policy.PolicyLevel" />.</returns>
		// Token: 0x06002AF7 RID: 10999 RVA: 0x0009B340 File Offset: 0x00099540
		public static PolicyLevel CreateAppDomainLevel()
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup(new AllMembershipCondition(), new PolicyStatement(DefaultPolicies.FullTrust));
			unionCodeGroup.Name = "All_Code";
			PolicyLevel policyLevel = new PolicyLevel("AppDomain", PolicyLevelType.AppDomain);
			policyLevel.RootCodeGroup = unionCodeGroup;
			policyLevel.Reset();
			return policyLevel;
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.SecurityElement" /> specified by the <paramref name="e" /> parameter is invalid.</exception>
		// Token: 0x06002AF8 RID: 11000 RVA: 0x0009B388 File Offset: 0x00099588
		public void FromXml(SecurityElement e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			SecurityElement securityElement = e.SearchForChildByTag("SecurityClasses");
			if (securityElement != null && securityElement.Children != null && securityElement.Children.Count > 0)
			{
				this.fullNames = new Hashtable(securityElement.Children.Count);
				foreach (object obj in securityElement.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					this.fullNames.Add(securityElement2.Attributes["Name"], securityElement2.Attributes["Description"]);
				}
			}
			SecurityElement securityElement3 = e.SearchForChildByTag("FullTrustAssemblies");
			if (securityElement3 != null && securityElement3.Children != null && securityElement3.Children.Count > 0)
			{
				this.full_trust_assemblies.Clear();
				foreach (object obj2 in securityElement3.Children)
				{
					SecurityElement securityElement4 = (SecurityElement)obj2;
					if (securityElement4.Tag != "IMembershipCondition")
					{
						throw new ArgumentException(Locale.GetText("Invalid XML"));
					}
					if (securityElement4.Attribute("class").IndexOf("StrongNameMembershipCondition") < 0)
					{
						throw new ArgumentException(Locale.GetText("Invalid XML - must be StrongNameMembershipCondition"));
					}
					this.full_trust_assemblies.Add(new StrongNameMembershipCondition(securityElement4));
				}
			}
			SecurityElement securityElement5 = e.SearchForChildByTag("CodeGroup");
			if (securityElement5 != null && securityElement5.Children != null && securityElement5.Children.Count > 0)
			{
				this.root_code_group = CodeGroup.CreateFromXml(securityElement5, this);
				SecurityElement securityElement6 = e.SearchForChildByTag("NamedPermissionSets");
				if (securityElement6 != null && securityElement6.Children != null && securityElement6.Children.Count > 0)
				{
					this.named_permission_sets.Clear();
					foreach (object obj3 in securityElement6.Children)
					{
						SecurityElement et = (SecurityElement)obj3;
						NamedPermissionSet namedPermissionSet = new NamedPermissionSet();
						namedPermissionSet.Resolver = this;
						namedPermissionSet.FromXml(et);
						this.named_permission_sets.Add(namedPermissionSet);
					}
				}
				return;
			}
			throw new ArgumentException(Locale.GetText("Missing Root CodeGroup"));
		}

		/// <summary>Returns the <see cref="T:System.Security.NamedPermissionSet" /> in the current policy level with the specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.Security.NamedPermissionSet" /> to find.</param>
		/// <returns>The <see cref="T:System.Security.NamedPermissionSet" /> in the current policy level with the specified name, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002AF9 RID: 11001 RVA: 0x0009B628 File Offset: 0x00099828
		public NamedPermissionSet GetNamedPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				if (namedPermissionSet.Name == name)
				{
					return (NamedPermissionSet)namedPermissionSet.Copy();
				}
			}
			return null;
		}

		/// <summary>Replaces the configuration file for this <see cref="T:System.Security.Policy.PolicyLevel" /> with the last backup (reflecting the state of policy prior to the last time it was saved) and returns it to the state of the last save.</summary>
		/// <exception cref="T:System.Security.Policy.PolicyException">The policy level does not have a valid configuration file.</exception>
		// Token: 0x06002AFA RID: 11002 RVA: 0x0009B6A8 File Offset: 0x000998A8
		public void Recover()
		{
			if (this._location == null)
			{
				throw new PolicyException(Locale.GetText("Only file based policies may be recovered."));
			}
			string text = this._location + ".backup";
			if (!File.Exists(text))
			{
				throw new PolicyException(Locale.GetText("No policy backup exists."));
			}
			try
			{
				File.Copy(text, this._location, true);
			}
			catch (Exception exception)
			{
				throw new PolicyException(Locale.GetText("Couldn't replace the policy file with it's backup."), exception);
			}
		}

		/// <summary>Removes an assembly with the specified <see cref="T:System.Security.Policy.StrongName" /> from the list of assemblies the policy level uses to evaluate policy.</summary>
		/// <param name="sn">The <see cref="T:System.Security.Policy.StrongName" /> of the assembly to remove from the list of assemblies used to evaluate policy.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sn" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The assembly with the <see cref="T:System.Security.Policy.StrongName" /> specified by the <paramref name="sn" /> parameter does not have full trust.</exception>
		// Token: 0x06002AFB RID: 11003 RVA: 0x0009B728 File Offset: 0x00099928
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public void RemoveFullTrustAssembly(StrongName sn)
		{
			if (sn == null)
			{
				throw new ArgumentNullException("sn");
			}
			StrongNameMembershipCondition snMC = new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version);
			this.RemoveFullTrustAssembly(snMC);
		}

		/// <summary>Removes an assembly with the specified <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> from the list of assemblies the policy level uses to evaluate policy.</summary>
		/// <param name="snMC">The <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> of the assembly to remove from the list of assemblies used to evaluate policy.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="snMC" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> specified by the <paramref name="snMC" /> parameter does not have full trust.</exception>
		// Token: 0x06002AFC RID: 11004 RVA: 0x0009B762 File Offset: 0x00099962
		[Obsolete("All GACed assemblies are now fully trusted and all permissions now succeed on fully trusted code.")]
		public void RemoveFullTrustAssembly(StrongNameMembershipCondition snMC)
		{
			if (snMC == null)
			{
				throw new ArgumentNullException("snMC");
			}
			if (((IList)this.full_trust_assemblies).Contains(snMC))
			{
				((IList)this.full_trust_assemblies).Remove(snMC);
				return;
			}
			throw new ArgumentException(Locale.GetText("sn does not have full trust."));
		}

		/// <summary>Removes the specified <see cref="T:System.Security.NamedPermissionSet" /> from the current policy level.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.NamedPermissionSet" /> to remove from the current policy level.</param>
		/// <returns>The <see cref="T:System.Security.NamedPermissionSet" /> that was removed.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.NamedPermissionSet" /> specified by the <paramref name="permSet" /> parameter was not found.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="permSet" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002AFD RID: 11005 RVA: 0x0009B79C File Offset: 0x0009999C
		public NamedPermissionSet RemoveNamedPermissionSet(NamedPermissionSet permSet)
		{
			if (permSet == null)
			{
				throw new ArgumentNullException("permSet");
			}
			return this.RemoveNamedPermissionSet(permSet.Name);
		}

		/// <summary>Removes the <see cref="T:System.Security.NamedPermissionSet" /> with the specified name from the current policy level.</summary>
		/// <param name="name">The name of the <see cref="T:System.Security.NamedPermissionSet" /> to remove.</param>
		/// <returns>The <see cref="T:System.Security.NamedPermissionSet" /> that was removed.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is equal to the name of a reserved permission set.  
		///  -or-  
		///  A <see cref="T:System.Security.NamedPermissionSet" /> with the specified name cannot be found.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002AFE RID: 11006 RVA: 0x0009B7B8 File Offset: 0x000999B8
		public NamedPermissionSet RemoveNamedPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (DefaultPolicies.ReservedNames.IsReserved(name))
			{
				throw new ArgumentException(Locale.GetText("Reserved name"));
			}
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				if (name == namedPermissionSet.Name)
				{
					this.named_permission_sets.Remove(namedPermissionSet);
					return namedPermissionSet;
				}
			}
			throw new ArgumentException(string.Format(Locale.GetText("Name '{0}' cannot be found."), name), "name");
		}

		/// <summary>Returns the current policy level to the default state.</summary>
		// Token: 0x06002AFF RID: 11007 RVA: 0x0009B86C File Offset: 0x00099A6C
		public void Reset()
		{
			if (this.fullNames != null)
			{
				this.fullNames.Clear();
			}
			if (this._type != PolicyLevelType.AppDomain)
			{
				this.full_trust_assemblies.Clear();
				this.named_permission_sets.Clear();
				if (this._location != null && File.Exists(this._location))
				{
					try
					{
						File.Delete(this._location);
					}
					catch
					{
					}
				}
				this.LoadFromFile(this._location);
				return;
			}
			this.CreateDefaultFullTrustAssemblies();
			this.CreateDefaultNamedPermissionSets();
		}

		/// <summary>Resolves policy based on evidence for the policy level, and returns the resulting <see cref="T:System.Security.Policy.PolicyStatement" />.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> used to resolve the <see cref="T:System.Security.Policy.PolicyLevel" />.</param>
		/// <returns>The resulting <see cref="T:System.Security.Policy.PolicyStatement" />.</returns>
		/// <exception cref="T:System.Security.Policy.PolicyException">The policy level contains multiple matching code groups marked as exclusive.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B00 RID: 11008 RVA: 0x0009B8FC File Offset: 0x00099AFC
		public PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			PolicyStatement policyStatement = this.root_code_group.Resolve(evidence);
			if (policyStatement == null)
			{
				return PolicyStatement.Empty();
			}
			return policyStatement;
		}

		/// <summary>Resolves policy at the policy level and returns the root of a code group tree that matches the evidence.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> used to resolve policy.</param>
		/// <returns>A <see cref="T:System.Security.Policy.CodeGroup" /> representing the root of a tree of code groups matching the specified evidence.</returns>
		/// <exception cref="T:System.Security.Policy.PolicyException">The policy level contains multiple matching code groups marked as exclusive.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B01 RID: 11009 RVA: 0x0009B930 File Offset: 0x00099B30
		public CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			CodeGroup codeGroup = this.root_code_group.ResolveMatchingCodeGroups(evidence);
			if (codeGroup == null)
			{
				return null;
			}
			return codeGroup;
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002B02 RID: 11010 RVA: 0x0009B960 File Offset: 0x00099B60
		public SecurityElement ToXml()
		{
			Hashtable hashtable = new Hashtable();
			if (this.full_trust_assemblies.Count > 0 && !hashtable.Contains("StrongNameMembershipCondition"))
			{
				hashtable.Add("StrongNameMembershipCondition", typeof(StrongNameMembershipCondition).FullName);
			}
			SecurityElement securityElement = new SecurityElement("NamedPermissionSets");
			foreach (object obj in this.named_permission_sets)
			{
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
				SecurityElement securityElement2 = namedPermissionSet.ToXml();
				object key = securityElement2.Attributes["class"];
				if (!hashtable.Contains(key))
				{
					hashtable.Add(key, namedPermissionSet.GetType().FullName);
				}
				securityElement.AddChild(securityElement2);
			}
			SecurityElement securityElement3 = new SecurityElement("FullTrustAssemblies");
			foreach (object obj2 in this.full_trust_assemblies)
			{
				StrongNameMembershipCondition strongNameMembershipCondition = (StrongNameMembershipCondition)obj2;
				securityElement3.AddChild(strongNameMembershipCondition.ToXml(this));
			}
			SecurityElement securityElement4 = new SecurityElement("SecurityClasses");
			if (hashtable.Count > 0)
			{
				foreach (object obj3 in hashtable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
					SecurityElement securityElement5 = new SecurityElement("SecurityClass");
					securityElement5.AddAttribute("Name", (string)dictionaryEntry.Key);
					securityElement5.AddAttribute("Description", (string)dictionaryEntry.Value);
					securityElement4.AddChild(securityElement5);
				}
			}
			SecurityElement securityElement6 = new SecurityElement(typeof(PolicyLevel).Name);
			securityElement6.AddAttribute("version", "1");
			securityElement6.AddChild(securityElement4);
			securityElement6.AddChild(securityElement);
			if (this.root_code_group != null)
			{
				securityElement6.AddChild(this.root_code_group.ToXml(this));
			}
			securityElement6.AddChild(securityElement3);
			return securityElement6;
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x0009BB9C File Offset: 0x00099D9C
		internal void Save()
		{
			if (this._type == PolicyLevelType.AppDomain)
			{
				throw new PolicyException(Locale.GetText("Can't save AppDomain PolicyLevel"));
			}
			if (this._location != null)
			{
				try
				{
					if (File.Exists(this._location))
					{
						File.Copy(this._location, this._location + ".backup", true);
					}
				}
				catch (Exception)
				{
				}
				finally
				{
					using (StreamWriter streamWriter = new StreamWriter(this._location))
					{
						streamWriter.Write(this.ToXml().ToString());
						streamWriter.Close();
					}
				}
			}
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x0009BC50 File Offset: 0x00099E50
		internal void CreateDefaultLevel(PolicyLevelType type)
		{
			PolicyStatement policy = new PolicyStatement(DefaultPolicies.FullTrust);
			switch (type)
			{
			case PolicyLevelType.User:
			case PolicyLevelType.Enterprise:
			case PolicyLevelType.AppDomain:
				this.root_code_group = new UnionCodeGroup(new AllMembershipCondition(), policy);
				this.root_code_group.Name = "All_Code";
				return;
			case PolicyLevelType.Machine:
			{
				PolicyStatement policy2 = new PolicyStatement(DefaultPolicies.Nothing);
				this.root_code_group = new UnionCodeGroup(new AllMembershipCondition(), policy2);
				this.root_code_group.Name = "All_Code";
				UnionCodeGroup unionCodeGroup = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.MyComputer), policy);
				unionCodeGroup.Name = "My_Computer_Zone";
				this.root_code_group.AddChild(unionCodeGroup);
				UnionCodeGroup unionCodeGroup2 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Intranet), new PolicyStatement(DefaultPolicies.LocalIntranet));
				unionCodeGroup2.Name = "LocalIntranet_Zone";
				this.root_code_group.AddChild(unionCodeGroup2);
				PolicyStatement policy3 = new PolicyStatement(DefaultPolicies.Internet);
				UnionCodeGroup unionCodeGroup3 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Internet), policy3);
				unionCodeGroup3.Name = "Internet_Zone";
				this.root_code_group.AddChild(unionCodeGroup3);
				UnionCodeGroup unionCodeGroup4 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Untrusted), policy2);
				unionCodeGroup4.Name = "Restricted_Zone";
				this.root_code_group.AddChild(unionCodeGroup4);
				UnionCodeGroup unionCodeGroup5 = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Trusted), policy3);
				unionCodeGroup5.Name = "Trusted_Zone";
				this.root_code_group.AddChild(unionCodeGroup5);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x0009BDA4 File Offset: 0x00099FA4
		internal void CreateDefaultFullTrustAssemblies()
		{
			this.full_trust_assemblies.Clear();
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("mscorlib", DefaultPolicies.Key.Ecma));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System", DefaultPolicies.Key.Ecma));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.Data", DefaultPolicies.Key.Ecma));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.DirectoryServices", DefaultPolicies.Key.MsFinal));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.Drawing", DefaultPolicies.Key.MsFinal));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.Messaging", DefaultPolicies.Key.MsFinal));
			this.full_trust_assemblies.Add(DefaultPolicies.FullTrustMembership("System.ServiceProcess", DefaultPolicies.Key.MsFinal));
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x0009BE60 File Offset: 0x0009A060
		internal void CreateDefaultNamedPermissionSets()
		{
			this.named_permission_sets.Clear();
			try
			{
				SecurityManager.ResolvingPolicyLevel = this;
				this.named_permission_sets.Add(DefaultPolicies.LocalIntranet);
				this.named_permission_sets.Add(DefaultPolicies.Internet);
				this.named_permission_sets.Add(DefaultPolicies.SkipVerification);
				this.named_permission_sets.Add(DefaultPolicies.Execution);
				this.named_permission_sets.Add(DefaultPolicies.Nothing);
				this.named_permission_sets.Add(DefaultPolicies.Everything);
				this.named_permission_sets.Add(DefaultPolicies.FullTrust);
			}
			finally
			{
				SecurityManager.ResolvingPolicyLevel = null;
			}
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x0009BF10 File Offset: 0x0009A110
		internal string ResolveClassName(string className)
		{
			if (this.fullNames != null)
			{
				object obj = this.fullNames[className];
				if (obj != null)
				{
					return (string)obj;
				}
			}
			return className;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x0009BF40 File Offset: 0x0009A140
		internal bool IsFullTrustAssembly(Assembly a)
		{
			AssemblyName name = a.GetName();
			StrongNameMembershipCondition obj = new StrongNameMembershipCondition(new StrongNamePublicKeyBlob(name.GetPublicKey()), name.Name, name.Version);
			using (IEnumerator enumerator = this.full_trust_assemblies.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((StrongNameMembershipCondition)enumerator.Current).Equals(obj))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000173AD File Offset: 0x000155AD
		internal PolicyLevel()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001FA8 RID: 8104
		private string label;

		// Token: 0x04001FA9 RID: 8105
		private CodeGroup root_code_group;

		// Token: 0x04001FAA RID: 8106
		private ArrayList full_trust_assemblies;

		// Token: 0x04001FAB RID: 8107
		private ArrayList named_permission_sets;

		// Token: 0x04001FAC RID: 8108
		private string _location;

		// Token: 0x04001FAD RID: 8109
		private PolicyLevelType _type;

		// Token: 0x04001FAE RID: 8110
		private Hashtable fullNames;

		// Token: 0x04001FAF RID: 8111
		private SecurityElement xml;
	}
}
