using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x02000132 RID: 306
	public abstract class InterfaceMemberBase : MemberBase
	{
		// Token: 0x06000F50 RID: 3920 RVA: 0x0003E7A1 File Offset: 0x0003C9A1
		protected InterfaceMemberBase(TypeDefinition parent, FullNamedExpression type, Modifiers mod, Modifiers allowed_mod, MemberName name, Attributes attrs) : base(parent, type, mod, allowed_mod, Modifiers.PRIVATE, name, attrs)
		{
			this.IsInterface = (parent.Kind == MemberKind.Interface);
			this.IsExplicitImpl = (base.MemberName.ExplicitInterface != null);
			this.explicit_mod_flags = mod;
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000F51 RID: 3921
		public abstract Variance ExpectedMemberTypeVariance { get; }

		// Token: 0x06000F52 RID: 3922 RVA: 0x0003E7E4 File Offset: 0x0003C9E4
		protected override bool CheckBase()
		{
			if (!base.CheckBase())
			{
				return false;
			}
			if ((this.caching_flags & MemberCore.Flags.MethodOverloadsExist) != (MemberCore.Flags)0)
			{
				this.CheckForDuplications();
			}
			if (this.IsExplicitImpl)
			{
				return true;
			}
			if (this.Parent.BaseType == null)
			{
				return true;
			}
			bool flag = false;
			MemberSpec memberSpec2;
			MemberSpec memberSpec = this.FindBaseMember(out memberSpec2, ref flag);
			if ((base.ModFlags & Modifiers.OVERRIDE) == (Modifiers)0)
			{
				if (memberSpec == null && memberSpec2 != null && (!(memberSpec2 is IParametersMember) || !(this is IParametersMember)))
				{
					memberSpec = memberSpec2;
				}
				if (memberSpec == null)
				{
					if ((base.ModFlags & Modifiers.NEW) != (Modifiers)0 && memberSpec == null)
					{
						base.Report.Warning(109, 4, base.Location, "The member `{0}' does not hide an inherited member. The new keyword is not required", this.GetSignatureForError());
					}
				}
				else
				{
					if ((base.ModFlags & Modifiers.NEW) == (Modifiers)0)
					{
						base.ModFlags |= Modifiers.NEW;
						if (!base.IsCompilerGenerated)
						{
							base.Report.SymbolRelatedToPreviousError(memberSpec);
							if ((memberSpec.Kind & MemberKind.NestedMask) == (MemberKind)0 && !this.IsInterface && (memberSpec.Modifiers & (Modifiers.ABSTRACT | Modifiers.VIRTUAL | Modifiers.OVERRIDE)) != (Modifiers)0)
							{
								base.Report.Warning(114, 2, base.Location, "`{0}' hides inherited member `{1}'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword", this.GetSignatureForError(), memberSpec.GetSignatureForError());
							}
							else
							{
								base.Report.Warning(108, 2, base.Location, "`{0}' hides inherited member `{1}'. Use the new keyword if hiding was intended", this.GetSignatureForError(), memberSpec.GetSignatureForError());
							}
						}
					}
					if (!this.IsInterface && memberSpec.IsAbstract && !flag && !base.IsStatic)
					{
						MemberKind kind = memberSpec.Kind;
						if (kind <= MemberKind.Method)
						{
							if (kind != MemberKind.Event && kind != MemberKind.Method)
							{
								return true;
							}
						}
						else if (kind != MemberKind.Property && kind != MemberKind.Indexer)
						{
							return true;
						}
						base.Report.SymbolRelatedToPreviousError(memberSpec);
						base.Report.Error(533, base.Location, "`{0}' hides inherited abstract member `{1}'", this.GetSignatureForError(), memberSpec.GetSignatureForError());
					}
				}
				return true;
			}
			if (memberSpec == null)
			{
				if (memberSpec2 == null)
				{
					if (this is Method && ((Method)this).ParameterInfo.IsEmpty && base.MemberName.Name == Destructor.MetadataName && base.MemberName.Arity == 0)
					{
						base.Report.Error(249, base.Location, "Do not override `{0}'. Use destructor syntax instead", "object.Finalize()");
					}
					else
					{
						base.Report.Error(115, base.Location, "`{0}' is marked as an override but no suitable {1} found to override", this.GetSignatureForError(), ATypeNameExpression.GetMemberType(this));
					}
				}
				else
				{
					base.Report.SymbolRelatedToPreviousError(memberSpec2);
					if (this is Event)
					{
						base.Report.Error(72, base.Location, "`{0}': cannot override because `{1}' is not an event", this.GetSignatureForError(), TypeManager.GetFullNameSignature(memberSpec2));
					}
					else if (this is PropertyBase)
					{
						base.Report.Error(544, base.Location, "`{0}': cannot override because `{1}' is not a property", this.GetSignatureForError(), TypeManager.GetFullNameSignature(memberSpec2));
					}
					else
					{
						base.Report.Error(505, base.Location, "`{0}': cannot override because `{1}' is not a method", this.GetSignatureForError(), TypeManager.GetFullNameSignature(memberSpec2));
					}
				}
				return false;
			}
			if (memberSpec2 != null)
			{
				base.Report.SymbolRelatedToPreviousError(memberSpec2);
				base.Report.SymbolRelatedToPreviousError(memberSpec);
				MemberSpec member = MemberCache.GetMember<MemberSpec>(memberSpec.DeclaringType.GetDefinition(), memberSpec);
				MemberSpec member2 = MemberCache.GetMember<MemberSpec>(memberSpec2.DeclaringType.GetDefinition(), memberSpec2);
				base.Report.Error(462, base.Location, "`{0}' cannot override inherited members `{1}' and `{2}' because they have the same signature when used in type `{3}'", new string[]
				{
					this.GetSignatureForError(),
					member.GetSignatureForError(),
					member2.GetSignatureForError(),
					this.Parent.GetSignatureForError()
				});
			}
			if (!this.CheckOverrideAgainstBase(memberSpec))
			{
				return false;
			}
			if (memberSpec.GetAttributeObsolete() != null)
			{
				if (base.OptAttributes == null || !base.OptAttributes.Contains(this.Module.PredefinedAttributes.Obsolete))
				{
					base.Report.SymbolRelatedToPreviousError(memberSpec);
					base.Report.Warning(672, 1, base.Location, "Member `{0}' overrides obsolete member `{1}'. Add the Obsolete attribute to `{0}'", this.GetSignatureForError(), memberSpec.GetSignatureForError());
				}
			}
			else if (base.OptAttributes != null && base.OptAttributes.Contains(this.Module.PredefinedAttributes.Obsolete))
			{
				base.Report.SymbolRelatedToPreviousError(memberSpec);
				base.Report.Warning(809, 1, base.Location, "Obsolete member `{0}' overrides non-obsolete member `{1}'", this.GetSignatureForError(), memberSpec.GetSignatureForError());
			}
			this.base_method = (memberSpec as MethodSpec);
			return true;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0003EC41 File Offset: 0x0003CE41
		protected virtual bool CheckForDuplications()
		{
			return this.Parent.MemberCache.CheckExistingMembersOverloads(this, ParametersCompiled.EmptyReadOnlyParameters);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0003EC5C File Offset: 0x0003CE5C
		protected virtual bool CheckOverrideAgainstBase(MemberSpec base_member)
		{
			bool result = true;
			if ((base_member.Modifiers & (Modifiers.ABSTRACT | Modifiers.VIRTUAL | Modifiers.OVERRIDE)) == (Modifiers)0)
			{
				base.Report.SymbolRelatedToPreviousError(base_member);
				base.Report.Error(506, base.Location, "`{0}': cannot override inherited member `{1}' because it is not marked virtual, abstract or override", this.GetSignatureForError(), TypeManager.CSharpSignature(base_member));
				result = false;
			}
			if ((base_member.Modifiers & Modifiers.SEALED) != (Modifiers)0)
			{
				base.Report.SymbolRelatedToPreviousError(base_member);
				base.Report.Error(239, base.Location, "`{0}': cannot override inherited member `{1}' because it is sealed", this.GetSignatureForError(), TypeManager.CSharpSignature(base_member));
				result = false;
			}
			TypeSpec memberType = ((IInterfaceMemberSpec)base_member).MemberType;
			if (!TypeSpecComparer.Override.IsEqual(base.MemberType, memberType))
			{
				base.Report.SymbolRelatedToPreviousError(base_member);
				if (this is PropertyBasedMember)
				{
					base.Report.Error(1715, base.Location, "`{0}': type must be `{1}' to match overridden member `{2}'", new string[]
					{
						this.GetSignatureForError(),
						memberType.GetSignatureForError(),
						base_member.GetSignatureForError()
					});
				}
				else
				{
					base.Report.Error(508, base.Location, "`{0}': return type must be `{1}' to match overridden member `{2}'", new string[]
					{
						this.GetSignatureForError(),
						memberType.GetSignatureForError(),
						base_member.GetSignatureForError()
					});
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0003ED9C File Offset: 0x0003CF9C
		protected static bool CheckAccessModifiers(MemberCore this_member, MemberSpec base_member)
		{
			Modifiers modifiers = this_member.ModFlags & Modifiers.AccessibilityMask;
			Modifiers modifiers2 = base_member.Modifiers & Modifiers.AccessibilityMask;
			if ((modifiers2 & (Modifiers.PROTECTED | Modifiers.INTERNAL)) != (Modifiers.PROTECTED | Modifiers.INTERNAL))
			{
				return modifiers == modifiers2;
			}
			if ((modifiers & Modifiers.PROTECTED) == (Modifiers)0)
			{
				return false;
			}
			if ((modifiers & Modifiers.INTERNAL) != (Modifiers)0)
			{
				return base_member.DeclaringType.MemberDefinition.IsInternalAsPublic(this_member.Module.DeclaringAssembly);
			}
			return !base_member.DeclaringType.MemberDefinition.IsInternalAsPublic(this_member.Module.DeclaringAssembly);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0003EE14 File Offset: 0x0003D014
		public override bool Define()
		{
			if (this.IsInterface)
			{
				base.ModFlags = (Modifiers.PUBLIC | Modifiers.ABSTRACT | Modifiers.VIRTUAL | (base.ModFlags & (Modifiers.NEW | Modifiers.UNSAFE)));
				this.flags = (MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask | MethodAttributes.Abstract);
			}
			else
			{
				this.Parent.PartialContainer.MethodModifiersValid(this);
				this.flags = ModifiersExtensions.MethodAttr(base.ModFlags);
			}
			if (this.IsExplicitImpl)
			{
				this.InterfaceType = base.MemberName.ExplicitInterface.ResolveAsType(this.Parent, false);
				if (this.InterfaceType == null)
				{
					return false;
				}
				if ((base.ModFlags & Modifiers.PARTIAL) != (Modifiers)0)
				{
					base.Report.Error(754, base.Location, "A partial method `{0}' cannot explicitly implement an interface", this.GetSignatureForError());
				}
				if (!this.InterfaceType.IsInterface)
				{
					base.Report.SymbolRelatedToPreviousError(this.InterfaceType);
					base.Report.Error(538, base.Location, "The type `{0}' in explicit interface declaration is not an interface", this.InterfaceType.GetSignatureForError());
				}
				else
				{
					this.Parent.PartialContainer.VerifyImplements(this);
				}
				Modifiers modifiers = Modifiers.AllowedExplicitImplFlags;
				if (this is Method)
				{
					modifiers |= Modifiers.ASYNC;
				}
				ModifiersExtensions.Check(modifiers, this.explicit_mod_flags, (Modifiers)0, base.Location, base.Report);
			}
			return base.Define();
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0003EF60 File Offset: 0x0003D160
		protected bool DefineParameters(ParametersCompiled parameters)
		{
			if (!parameters.Resolve(this))
			{
				return false;
			}
			bool flag = false;
			for (int i = 0; i < parameters.Count; i++)
			{
				Parameter parameter = parameters[i];
				if (parameter.HasDefaultValue && (this.IsExplicitImpl || this is Operator || (this is Indexer && parameters.Count == 1)))
				{
					parameter.Warning_UselessOptionalParameter(base.Report);
				}
				if (!parameter.CheckAccessibility(this))
				{
					TypeSpec typeSpec = parameters.Types[i];
					base.Report.SymbolRelatedToPreviousError(typeSpec);
					if (this is Indexer)
					{
						base.Report.Error(55, base.Location, "Inconsistent accessibility: parameter type `{0}' is less accessible than indexer `{1}'", typeSpec.GetSignatureForError(), this.GetSignatureForError());
					}
					else if (this is Operator)
					{
						base.Report.Error(57, base.Location, "Inconsistent accessibility: parameter type `{0}' is less accessible than operator `{1}'", typeSpec.GetSignatureForError(), this.GetSignatureForError());
					}
					else
					{
						base.Report.Error(51, base.Location, "Inconsistent accessibility: parameter type `{0}' is less accessible than method `{1}'", typeSpec.GetSignatureForError(), this.GetSignatureForError());
					}
					flag = true;
				}
			}
			return !flag;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0003F075 File Offset: 0x0003D275
		protected override void DoMemberTypeDependentChecks()
		{
			base.DoMemberTypeDependentChecks();
			VarianceDecl.CheckTypeVariance(base.MemberType, this.ExpectedMemberTypeVariance, this);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0003F090 File Offset: 0x0003D290
		public override void Emit()
		{
			if ((base.ModFlags & Modifiers.EXTERN) != (Modifiers)0 && !this.is_external_implementation && (base.OptAttributes == null || !base.OptAttributes.HasResolveError()))
			{
				if (this is Constructor)
				{
					base.Report.Warning(824, 1, base.Location, "Constructor `{0}' is marked `external' but has no external implementation specified", this.GetSignatureForError());
				}
				else
				{
					base.Report.Warning(626, 1, base.Location, "`{0}' is marked as an external but has no DllImport attribute. Consider adding a DllImport attribute to specify the external implementation", this.GetSignatureForError());
				}
			}
			base.Emit();
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0003F11C File Offset: 0x0003D31C
		public override bool EnableOverloadChecks(MemberCore overload)
		{
			InterfaceMemberBase interfaceMemberBase = overload as InterfaceMemberBase;
			if (interfaceMemberBase != null && interfaceMemberBase.IsExplicitImpl)
			{
				if (this.IsExplicitImpl)
				{
					this.caching_flags |= MemberCore.Flags.MethodOverloadsExist;
				}
				return true;
			}
			return this.IsExplicitImpl;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0003F160 File Offset: 0x0003D360
		protected void Error_CannotChangeAccessModifiers(MemberCore member, MemberSpec base_member)
		{
			Modifiers modifiers = base_member.Modifiers;
			if ((modifiers & Modifiers.AccessibilityMask) == (Modifiers.PROTECTED | Modifiers.INTERNAL) && !base_member.DeclaringType.MemberDefinition.IsInternalAsPublic(member.Module.DeclaringAssembly))
			{
				modifiers = Modifiers.PROTECTED;
			}
			base.Report.SymbolRelatedToPreviousError(base_member);
			base.Report.Error(507, member.Location, "`{0}': cannot change access modifiers when overriding `{1}' inherited member `{2}'", new string[]
			{
				member.GetSignatureForError(),
				ModifiersExtensions.AccessibilityName(modifiers),
				base_member.GetSignatureForError()
			});
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0003F1E3 File Offset: 0x0003D3E3
		protected void Error_StaticReturnType()
		{
			base.Report.Error(722, base.Location, "`{0}': static types cannot be used as return types", base.MemberType.GetSignatureForError());
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0003F20B File Offset: 0x0003D40B
		protected virtual MemberSpec FindBaseMember(out MemberSpec bestCandidate, ref bool overrides)
		{
			return MemberCache.FindBaseMember(this, out bestCandidate, ref overrides);
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0003F215 File Offset: 0x0003D415
		public string ShortName
		{
			get
			{
				return base.MemberName.Name;
			}
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0003F222 File Offset: 0x0003D422
		public string GetFullName(MemberName name)
		{
			return this.GetFullName(name.Name);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0003F230 File Offset: 0x0003D430
		public string GetFullName(string name)
		{
			if (!this.IsExplicitImpl)
			{
				return name;
			}
			return this.InterfaceType.GetSignatureForError() + "." + name;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0003F254 File Offset: 0x0003D454
		public override string GetSignatureForDocumentation()
		{
			if (this.IsExplicitImpl)
			{
				return string.Concat(new string[]
				{
					this.Parent.GetSignatureForDocumentation(),
					".",
					this.InterfaceType.GetSignatureForDocumentation(true),
					"#",
					this.ShortName
				});
			}
			return this.Parent.GetSignatureForDocumentation() + "." + this.ShortName;
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0003F2C6 File Offset: 0x0003D4C6
		public override bool IsUsed
		{
			get
			{
				return this.IsExplicitImpl || base.IsUsed;
			}
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003F2D8 File Offset: 0x0003D4D8
		public override void SetConstraints(List<Constraints> constraints_list)
		{
			if ((base.ModFlags & Modifiers.OVERRIDE) != (Modifiers)0 || this.IsExplicitImpl)
			{
				base.Report.Error(460, base.Location, "`{0}': Cannot specify constraints for overrides and explicit interface implementation methods", this.GetSignatureForError());
			}
			base.SetConstraints(constraints_list);
		}

		// Token: 0x04000701 RID: 1793
		protected const Modifiers AllowedModifiersClass = Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.ABSTRACT | Modifiers.SEALED | Modifiers.STATIC | Modifiers.VIRTUAL | Modifiers.OVERRIDE | Modifiers.EXTERN | Modifiers.UNSAFE;

		// Token: 0x04000702 RID: 1794
		protected const Modifiers AllowedModifiersStruct = Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.STATIC | Modifiers.OVERRIDE | Modifiers.EXTERN | Modifiers.UNSAFE;

		// Token: 0x04000703 RID: 1795
		protected const Modifiers AllowedModifiersInterface = Modifiers.NEW | Modifiers.UNSAFE;

		// Token: 0x04000704 RID: 1796
		public bool IsInterface;

		// Token: 0x04000705 RID: 1797
		public readonly bool IsExplicitImpl;

		// Token: 0x04000706 RID: 1798
		protected bool is_external_implementation;

		// Token: 0x04000707 RID: 1799
		public TypeSpec InterfaceType;

		// Token: 0x04000708 RID: 1800
		protected MethodSpec base_method;

		// Token: 0x04000709 RID: 1801
		private readonly Modifiers explicit_mod_flags;

		// Token: 0x0400070A RID: 1802
		public MethodAttributes flags;
	}
}
