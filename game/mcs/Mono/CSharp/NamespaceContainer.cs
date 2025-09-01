using System;
using System.Collections.Generic;
using System.Linq;

namespace Mono.CSharp
{
	// Token: 0x0200025D RID: 605
	public class NamespaceContainer : TypeContainer, IMemberContext, IModuleContext
	{
		// Token: 0x06001DED RID: 7661 RVA: 0x00092685 File Offset: 0x00090885
		public NamespaceContainer(MemberName name, NamespaceContainer parent) : base(parent, name, null, MemberKind.Namespace)
		{
			this.Parent = parent;
			this.ns = parent.NS.AddNamespace(name);
			this.containers = new List<TypeContainer>();
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x000926B9 File Offset: 0x000908B9
		protected NamespaceContainer(ModuleContainer parent) : base(parent, null, null, MemberKind.Namespace)
		{
			this.ns = parent.GlobalRootNamespace;
			this.containers = new List<TypeContainer>(2);
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0000225C File Offset: 0x0000045C
		public override AttributeTargets AttributeTargets
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x0000225C File Offset: 0x0000045C
		public override string DocCommentHeader
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x000926E1 File Offset: 0x000908E1
		public Namespace NS
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x000926E9 File Offset: 0x000908E9
		public List<UsingClause> Usings
		{
			get
			{
				return this.clauses;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x0000225C File Offset: 0x0000045C
		public override string[] ValidAttributeTargets
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000926F4 File Offset: 0x000908F4
		public void AddUsing(UsingClause un)
		{
			if (this.DeclarationFound)
			{
				this.Compiler.Report.Error(1529, un.Location, "A using clause must precede all other namespace elements except extern alias declarations");
			}
			if (this.clauses == null)
			{
				this.clauses = new List<UsingClause>();
			}
			this.clauses.Add(un);
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00092748 File Offset: 0x00090948
		public void AddUsing(UsingAliasNamespace un)
		{
			if (this.DeclarationFound)
			{
				this.Compiler.Report.Error(1529, un.Location, "A using clause must precede all other namespace elements except extern alias declarations");
			}
			this.AddAlias(un);
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x0009277C File Offset: 0x0009097C
		private void AddAlias(UsingAliasNamespace un)
		{
			if (this.clauses == null)
			{
				this.clauses = new List<UsingClause>();
			}
			else
			{
				foreach (UsingClause usingClause in this.clauses)
				{
					UsingAliasNamespace usingAliasNamespace = usingClause as UsingAliasNamespace;
					if (usingAliasNamespace != null && usingAliasNamespace.Alias.Value == un.Alias.Value)
					{
						this.Compiler.Report.SymbolRelatedToPreviousError(usingAliasNamespace.Location, "");
						this.Compiler.Report.Error(1537, un.Location, "The using alias `{0}' appeared previously in this namespace", un.Alias.Value);
					}
				}
			}
			this.clauses.Add(un);
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x00092858 File Offset: 0x00090A58
		public override void AddPartial(TypeDefinition next_part)
		{
			TypeSpec typeSpec = this.ns.LookupType(this, next_part.MemberName.Name, next_part.MemberName.Arity, LookupMode.Probing, Location.Null);
			TypeDefinition existing = (typeSpec != null) ? (typeSpec.MemberDefinition as TypeDefinition) : null;
			base.AddPartial(next_part, existing);
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000928A8 File Offset: 0x00090AA8
		public override void AddTypeContainer(TypeContainer tc)
		{
			MemberName memberName = tc.MemberName;
			string text = memberName.Basename;
			while (memberName.Left != null)
			{
				memberName = memberName.Left;
				text = memberName.Name;
			}
			TypeContainer typeContainer = (this.Parent == null) ? this.Module : this;
			MemberCore memberCore;
			if (typeContainer.DefinedNames.TryGetValue(text, out memberCore))
			{
				if (tc is NamespaceContainer && memberCore is NamespaceContainer)
				{
					this.AddTypeContainerMember(tc);
					return;
				}
				base.Report.SymbolRelatedToPreviousError(memberCore);
				if ((memberCore.ModFlags & Modifiers.PARTIAL) != (Modifiers)0 && (tc is ClassOrStruct || tc is Interface))
				{
					base.Error_MissingPartialModifier(tc);
				}
				else
				{
					base.Report.Error(101, tc.Location, "The namespace `{0}' already contains a definition for `{1}'", this.GetSignatureForError(), memberName.GetSignatureForError());
				}
			}
			else
			{
				typeContainer.DefinedNames.Add(text, tc);
				TypeDefinition partialContainer = tc.PartialContainer;
				if (partialContainer != null)
				{
					IList<TypeSpec> allTypes = this.ns.GetAllTypes(text);
					if (allTypes != null)
					{
						foreach (TypeSpec typeSpec in allTypes)
						{
							if (typeSpec.Arity == memberName.Arity)
							{
								memberCore = (MemberCore)typeSpec.MemberDefinition;
								break;
							}
						}
					}
					if (memberCore != null)
					{
						base.Report.SymbolRelatedToPreviousError(memberCore);
						base.Report.Error(101, tc.Location, "The namespace `{0}' already contains a definition for `{1}'", this.GetSignatureForError(), memberName.GetSignatureForError());
					}
					else
					{
						this.ns.AddType(this.Module, partialContainer.Definition);
					}
				}
			}
			base.AddTypeContainer(tc);
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x0000225C File Offset: 0x0000045C
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00092A50 File Offset: 0x00090C50
		public override void EmitContainer()
		{
			this.VerifyClsCompliance();
			base.EmitContainer();
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00092A60 File Offset: 0x00090C60
		public ExtensionMethodCandidates LookupExtensionMethod(IMemberContext invocationContext, string name, int arity, int position)
		{
			NamespaceContainer namespaceContainer = this;
			ExtensionMethodCandidates extensionMethodCandidates;
			List<MethodSpec> list;
			for (;;)
			{
				extensionMethodCandidates = namespaceContainer.LookupExtensionMethodCandidates(invocationContext, name, arity, ref position);
				if (extensionMethodCandidates != null || namespaceContainer.MemberName == null)
				{
					break;
				}
				Namespace parent = namespaceContainer.ns.Parent;
				MemberName left = namespaceContainer.MemberName.Left;
				int num = position - 2;
				while (num-- > 0)
				{
					left = left.Left;
					parent = parent.Parent;
				}
				while (left != null)
				{
					position++;
					list = parent.LookupExtensionMethod(invocationContext, name, arity);
					if (list != null)
					{
						goto Block_3;
					}
					left = left.Left;
					parent = parent.Parent;
				}
				position = 0;
				namespaceContainer = namespaceContainer.Parent;
				if (namespaceContainer == null)
				{
					goto Block_5;
				}
			}
			return extensionMethodCandidates;
			Block_3:
			return new ExtensionMethodCandidates(invocationContext, list, namespaceContainer, position);
			Block_5:
			return null;
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x00092B08 File Offset: 0x00090D08
		private ExtensionMethodCandidates LookupExtensionMethodCandidates(IMemberContext invocationContext, string name, int arity, ref int position)
		{
			List<MethodSpec> list = null;
			if (position == 0)
			{
				position++;
				list = this.ns.LookupExtensionMethod(invocationContext, name, arity);
				if (list != null)
				{
					return new ExtensionMethodCandidates(invocationContext, list, this, position);
				}
			}
			if (position == 1)
			{
				position++;
				Namespace[] array = this.namespace_using_table;
				for (int i = 0; i < array.Length; i++)
				{
					List<MethodSpec> list2 = array[i].LookupExtensionMethod(invocationContext, name, arity);
					if (list2 != null)
					{
						if (list == null)
						{
							list = list2;
						}
						else
						{
							list.AddRange(list2);
						}
					}
				}
				if (this.types_using_table != null)
				{
					TypeSpec[] array2 = this.types_using_table;
					for (int i = 0; i < array2.Length; i++)
					{
						List<MethodSpec> list3 = array2[i].MemberCache.FindExtensionMethods(invocationContext, name, arity);
						if (list3 != null)
						{
							if (list == null)
							{
								list = list3;
							}
							else
							{
								list.AddRange(list3);
							}
						}
					}
				}
				if (list != null)
				{
					return new ExtensionMethodCandidates(invocationContext, list, this, position);
				}
			}
			return null;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x00092BDC File Offset: 0x00090DDC
		public override FullNamedExpression LookupNamespaceOrType(string name, int arity, LookupMode mode, Location loc)
		{
			for (NamespaceContainer namespaceContainer = this; namespaceContainer != null; namespaceContainer = namespaceContainer.Parent)
			{
				FullNamedExpression fullNamedExpression = namespaceContainer.Lookup(name, arity, mode, loc);
				if (fullNamedExpression != null || namespaceContainer.MemberName == null)
				{
					return fullNamedExpression;
				}
				Namespace parent = namespaceContainer.ns.Parent;
				MemberName left = namespaceContainer.MemberName.Left;
				while (left != null)
				{
					fullNamedExpression = parent.LookupTypeOrNamespace(this, name, arity, mode, loc);
					if (fullNamedExpression != null)
					{
						return fullNamedExpression;
					}
					left = left.Left;
					parent = parent.Parent;
				}
			}
			return null;
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x00092C50 File Offset: 0x00090E50
		public override void GetCompletionStartingWith(string prefix, List<string> results)
		{
			if (this.Usings == null)
			{
				return;
			}
			foreach (UsingClause usingClause in this.Usings)
			{
				if (usingClause.Alias == null)
				{
					string name = usingClause.NamespaceExpression.Name;
					if (name.StartsWith(prefix))
					{
						results.Add(name);
					}
				}
			}
			IEnumerable<string> enumerable = Enumerable.Empty<string>();
			foreach (Namespace @namespace in this.namespace_using_table)
			{
				if (prefix.StartsWith(@namespace.Name))
				{
					int num = prefix.LastIndexOf('.');
					if (num != -1)
					{
						string prefix2 = prefix.Substring(num + 1);
						enumerable = enumerable.Concat(@namespace.CompletionGetTypesStartingWith(prefix2));
					}
				}
				enumerable = enumerable.Concat(@namespace.CompletionGetTypesStartingWith(prefix));
			}
			results.AddRange(enumerable);
			base.GetCompletionStartingWith(prefix, results);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x00092D4C File Offset: 0x00090F4C
		public FullNamedExpression LookupExternAlias(string name)
		{
			if (this.aliases == null)
			{
				return null;
			}
			UsingAliasNamespace usingAliasNamespace;
			if (this.aliases.TryGetValue(name, out usingAliasNamespace) && usingAliasNamespace is UsingExternAlias)
			{
				return usingAliasNamespace.ResolvedExpression;
			}
			return null;
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x00092D84 File Offset: 0x00090F84
		public override FullNamedExpression LookupNamespaceAlias(string name)
		{
			for (NamespaceContainer namespaceContainer = this; namespaceContainer != null; namespaceContainer = namespaceContainer.Parent)
			{
				UsingAliasNamespace usingAliasNamespace;
				if (namespaceContainer.aliases != null && namespaceContainer.aliases.TryGetValue(name, out usingAliasNamespace))
				{
					if (usingAliasNamespace.ResolvedExpression == null)
					{
						usingAliasNamespace.Define(namespaceContainer);
					}
					return usingAliasNamespace.ResolvedExpression;
				}
			}
			return null;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00092DD0 File Offset: 0x00090FD0
		private FullNamedExpression Lookup(string name, int arity, LookupMode mode, Location loc)
		{
			FullNamedExpression fullNamedExpression = this.ns.LookupTypeOrNamespace(this, name, arity, mode, loc);
			UsingAliasNamespace usingAliasNamespace;
			if (this.aliases != null && arity == 0 && this.aliases.TryGetValue(name, out usingAliasNamespace))
			{
				if (fullNamedExpression != null && mode != LookupMode.Probing)
				{
					this.Compiler.Report.SymbolRelatedToPreviousError(usingAliasNamespace.Location, null);
					this.Compiler.Report.Error(576, loc, "Namespace `{0}' contains a definition with same name as alias `{1}'", this.GetSignatureForError(), name);
				}
				if (usingAliasNamespace.ResolvedExpression == null)
				{
					usingAliasNamespace.Define(this);
				}
				return usingAliasNamespace.ResolvedExpression;
			}
			if (fullNamedExpression != null)
			{
				return fullNamedExpression;
			}
			if (this.namespace_using_table == null)
			{
				this.DoDefineNamespace();
			}
			FullNamedExpression fullNamedExpression2 = null;
			Namespace[] array = this.namespace_using_table;
			for (int i = 0; i < array.Length; i++)
			{
				TypeSpec typeSpec = array[i].LookupType(this, name, arity, mode, loc);
				if (typeSpec != null)
				{
					fullNamedExpression = new TypeExpression(typeSpec, loc);
					if (fullNamedExpression2 == null)
					{
						fullNamedExpression2 = fullNamedExpression;
					}
					else
					{
						TypeExpr typeExpr = fullNamedExpression as TypeExpr;
						TypeExpr typeExpr2 = fullNamedExpression2 as TypeExpr;
						if (typeExpr != null && typeExpr2 == null)
						{
							fullNamedExpression2 = fullNamedExpression;
						}
						else if (typeExpr != null)
						{
							TypeSpec typeSpec2 = Namespace.IsImportedTypeOverride(this.Module, typeExpr2.Type, typeExpr.Type);
							if (typeSpec2 == null)
							{
								if (mode == LookupMode.Normal)
								{
									this.Error_AmbiguousReference(name, typeExpr2, typeExpr, loc);
								}
								return fullNamedExpression2;
							}
							if (typeSpec2 == typeExpr.Type)
							{
								fullNamedExpression2 = typeExpr;
							}
						}
					}
				}
			}
			if (this.types_using_table != null)
			{
				TypeSpec[] array2 = this.types_using_table;
				for (int i = 0; i < array2.Length; i++)
				{
					IList<MemberSpec> list = MemberCache.FindMembers(array2[i], name, true);
					if (list != null)
					{
						foreach (MemberSpec memberSpec in list)
						{
							if (arity <= 0 || memberSpec.Arity == arity)
							{
								if ((memberSpec.Kind & MemberKind.NestedMask) == (MemberKind)0)
								{
									if ((memberSpec.Modifiers & Modifiers.STATIC) != (Modifiers)0 && (memberSpec.Modifiers & Modifiers.METHOD_EXTENSION) == (Modifiers)0)
									{
										if (mode == LookupMode.Normal)
										{
											throw new NotImplementedException();
										}
										return null;
									}
								}
								else
								{
									fullNamedExpression = new TypeExpression((TypeSpec)memberSpec, loc);
									if (fullNamedExpression2 == null)
									{
										fullNamedExpression2 = fullNamedExpression;
									}
									else if (mode == LookupMode.Normal)
									{
										this.Error_AmbiguousReference(name, fullNamedExpression2, fullNamedExpression, loc);
									}
								}
							}
						}
					}
				}
			}
			return fullNamedExpression2;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x00093008 File Offset: 0x00091208
		private void Error_AmbiguousReference(string name, FullNamedExpression a, FullNamedExpression b, Location loc)
		{
			Report report = this.Compiler.Report;
			report.SymbolRelatedToPreviousError(a.Type);
			report.SymbolRelatedToPreviousError(b.Type);
			report.Error(104, loc, "`{0}' is an ambiguous reference between `{1}' and `{2}'", new string[]
			{
				name,
				a.GetSignatureForError(),
				b.GetSignatureForError()
			});
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x00093064 File Offset: 0x00091264
		public static Expression LookupStaticUsings(IMemberContext mc, string name, int arity, Location loc)
		{
			for (MemberCore memberCore = mc.CurrentMemberDefinition; memberCore != null; memberCore = memberCore.Parent)
			{
				NamespaceContainer namespaceContainer = memberCore as NamespaceContainer;
				if (namespaceContainer != null)
				{
					List<MemberSpec> list = null;
					if (namespaceContainer.types_using_table != null)
					{
						TypeSpec[] array = namespaceContainer.types_using_table;
						for (int i = 0; i < array.Length; i++)
						{
							IList<MemberSpec> list2 = MemberCache.FindMembers(array[i], name, true);
							if (list2 != null)
							{
								foreach (MemberSpec memberSpec in list2)
								{
									if (((memberSpec.Kind & MemberKind.NestedMask) != (MemberKind)0 || ((memberSpec.Modifiers & Modifiers.STATIC) != (Modifiers)0 && (memberSpec.Modifiers & Modifiers.METHOD_EXTENSION) == (Modifiers)0)) && (arity <= 0 || memberSpec.Arity == arity))
									{
										if (list == null)
										{
											list = new List<MemberSpec>();
										}
										list.Add(memberSpec);
									}
								}
							}
						}
					}
					if (list != null)
					{
						Expression expression = Expression.MemberLookupToExpression(mc, list, false, null, name, arity, Expression.MemberLookupRestrictions.None, loc);
						if (expression != null)
						{
							return expression;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x00093178 File Offset: 0x00091378
		protected override void DefineNamespace()
		{
			if (this.namespace_using_table == null)
			{
				this.DoDefineNamespace();
			}
			base.DefineNamespace();
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x00093190 File Offset: 0x00091390
		private void DoDefineNamespace()
		{
			this.namespace_using_table = NamespaceContainer.empty_namespaces;
			if (this.clauses != null)
			{
				List<Namespace> list = null;
				List<TypeSpec> list2 = null;
				bool flag = false;
				for (int i = 0; i < this.clauses.Count; i++)
				{
					UsingClause usingClause = this.clauses[i];
					if (usingClause.Alias != null)
					{
						if (this.aliases == null)
						{
							this.aliases = new Dictionary<string, UsingAliasNamespace>();
						}
						if (usingClause is UsingExternAlias)
						{
							usingClause.Define(this);
							if (usingClause.ResolvedExpression != null)
							{
								this.aliases.Add(usingClause.Alias.Value, (UsingExternAlias)usingClause);
							}
							this.clauses.RemoveAt(i--);
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						usingClause.Define(this);
						if (usingClause.ResolvedExpression == null)
						{
							this.clauses.RemoveAt(i--);
						}
						else
						{
							NamespaceExpression namespaceExpression = usingClause.ResolvedExpression as NamespaceExpression;
							if (namespaceExpression == null)
							{
								TypeSpec type = usingClause.ResolvedExpression.Type;
								if (list2 == null)
								{
									list2 = new List<TypeSpec>();
								}
								if (list2.Contains(type))
								{
									this.Warning_DuplicateEntry(usingClause);
								}
								else
								{
									list2.Add(type);
								}
							}
							else
							{
								if (list == null)
								{
									list = new List<Namespace>();
								}
								if (list.Contains(namespaceExpression.Namespace))
								{
									this.clauses.RemoveAt(i--);
									this.Warning_DuplicateEntry(usingClause);
								}
								else
								{
									list.Add(namespaceExpression.Namespace);
								}
							}
						}
					}
				}
				this.namespace_using_table = ((list == null) ? new Namespace[0] : list.ToArray());
				if (list2 != null)
				{
					this.types_using_table = list2.ToArray();
				}
				if (flag)
				{
					for (int j = 0; j < this.clauses.Count; j++)
					{
						UsingClause usingClause2 = this.clauses[j];
						if (usingClause2.Alias != null)
						{
							this.aliases.Add(usingClause2.Alias.Value, (UsingAliasNamespace)usingClause2);
						}
					}
				}
			}
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00093378 File Offset: 0x00091578
		protected override void DoDefineContainer()
		{
			base.DoDefineContainer();
			if (this.clauses != null)
			{
				for (int i = 0; i < this.clauses.Count; i++)
				{
					UsingClause usingClause = this.clauses[i];
					if (usingClause.Alias != null && usingClause.ResolvedExpression == null)
					{
						usingClause.Define(this);
					}
				}
			}
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x000933CD File Offset: 0x000915CD
		public void EnableRedefinition()
		{
			this.is_defined = false;
			this.namespace_using_table = null;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000933E0 File Offset: 0x000915E0
		public override void GenerateDocComment(DocumentationBuilder builder)
		{
			if (this.containers != null)
			{
				foreach (TypeContainer typeContainer in this.containers)
				{
					typeContainer.GenerateDocComment(builder);
				}
			}
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0009343C File Offset: 0x0009163C
		public override string GetSignatureForError()
		{
			if (base.MemberName != null)
			{
				return base.GetSignatureForError();
			}
			return "global::";
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00093452 File Offset: 0x00091652
		public override void RemoveContainer(TypeContainer cont)
		{
			base.RemoveContainer(cont);
			this.NS.RemoveContainer(cont);
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x00093467 File Offset: 0x00091667
		protected override bool VerifyClsCompliance()
		{
			if (this.Module.IsClsComplianceRequired())
			{
				if (base.MemberName != null && base.MemberName.Name[0] == '_')
				{
					base.Warning_IdentifierNotCompliant();
				}
				this.ns.VerifyClsCompliance();
				return true;
			}
			return false;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x000934A7 File Offset: 0x000916A7
		private void Warning_DuplicateEntry(UsingClause entry)
		{
			this.Compiler.Report.Warning(105, 3, entry.Location, "The using directive for `{0}' appeared previously in this namespace", entry.ResolvedExpression.GetSignatureForError());
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x000934D2 File Offset: 0x000916D2
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000934DB File Offset: 0x000916DB
		// Note: this type is marked as 'beforefieldinit'.
		static NamespaceContainer()
		{
		}

		// Token: 0x04000B21 RID: 2849
		private static readonly Namespace[] empty_namespaces = new Namespace[0];

		// Token: 0x04000B22 RID: 2850
		private readonly Namespace ns;

		// Token: 0x04000B23 RID: 2851
		public new readonly NamespaceContainer Parent;

		// Token: 0x04000B24 RID: 2852
		private List<UsingClause> clauses;

		// Token: 0x04000B25 RID: 2853
		public bool DeclarationFound;

		// Token: 0x04000B26 RID: 2854
		private Namespace[] namespace_using_table;

		// Token: 0x04000B27 RID: 2855
		private TypeSpec[] types_using_table;

		// Token: 0x04000B28 RID: 2856
		private Dictionary<string, UsingAliasNamespace> aliases;
	}
}
