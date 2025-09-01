using System;
using System.Collections.Generic;
using System.Diagnostics;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.CSharp
{
	// Token: 0x0200018D RID: 397
	[DebuggerDisplay("{GetSignatureForError()}")]
	public abstract class MemberCore : Attributable, IMemberContext, IModuleContext, IMemberDefinition
	{
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x00066A15 File Offset: 0x00064C15
		string IMemberDefinition.Name
		{
			get
			{
				return this.member_name.Name;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x00066A22 File Offset: 0x00064C22
		public MemberName MemberName
		{
			get
			{
				return this.member_name;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00066A47 File Offset: 0x00064C47
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x00066A2A File Offset: 0x00064C2A
		public Modifiers ModFlags
		{
			get
			{
				return this.mod_flags;
			}
			set
			{
				this.mod_flags = value;
				if ((value & Modifiers.COMPILER_GENERATED) != (Modifiers)0)
				{
					this.caching_flags = (MemberCore.Flags.IsUsed | MemberCore.Flags.IsAssigned);
				}
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00066A4F File Offset: 0x00064C4F
		public virtual ModuleContainer Module
		{
			get
			{
				return this.Parent.Module;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x00066A5C File Offset: 0x00064C5C
		public Location Location
		{
			get
			{
				return this.member_name.Location;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001561 RID: 5473
		public abstract string DocCommentHeader { get; }

		// Token: 0x06001562 RID: 5474 RVA: 0x00066A69 File Offset: 0x00064C69
		protected MemberCore(TypeContainer parent, MemberName name, Attributes attrs)
		{
			this.Parent = parent;
			this.member_name = name;
			this.caching_flags = (MemberCore.Flags.Obsolete_Undetected | MemberCore.Flags.ClsCompliance_Undetected | MemberCore.Flags.HasCompliantAttribute_Undetected | MemberCore.Flags.Excluded_Undetected);
			base.AddAttributes(attrs, this);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00066A92 File Offset: 0x00064C92
		protected virtual void SetMemberName(MemberName new_name)
		{
			this.member_name = new_name;
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0003A9F2 File Offset: 0x00038BF2
		public virtual void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00066A9C File Offset: 0x00064C9C
		protected bool CheckAbstractAndExtern(bool has_block)
		{
			if (this.Parent.PartialContainer.Kind == MemberKind.Interface)
			{
				return true;
			}
			if (has_block)
			{
				if ((this.ModFlags & Modifiers.EXTERN) != (Modifiers)0)
				{
					this.Report.Error(179, this.Location, "`{0}' cannot declare a body because it is marked extern", this.GetSignatureForError());
					return false;
				}
				if ((this.ModFlags & Modifiers.ABSTRACT) != (Modifiers)0)
				{
					this.Report.Error(500, this.Location, "`{0}' cannot declare a body because it is marked abstract", this.GetSignatureForError());
					return false;
				}
			}
			else if ((this.ModFlags & (Modifiers.ABSTRACT | Modifiers.EXTERN | Modifiers.PARTIAL)) == (Modifiers)0 && !(this.Parent is Delegate))
			{
				if (this.Compiler.Settings.Version >= LanguageVersion.V_3)
				{
					PropertyBase.PropertyMethod propertyMethod = this as PropertyBase.PropertyMethod;
					if (propertyMethod is Indexer.GetIndexerMethod || propertyMethod is Indexer.SetIndexerMethod)
					{
						propertyMethod = null;
					}
					if (propertyMethod != null && propertyMethod.Property.AccessorSecond == null)
					{
						this.Report.Error(840, this.Location, "`{0}' must have a body because it is not marked abstract or extern. The property can be automatically implemented when you define both accessors", this.GetSignatureForError());
						return false;
					}
				}
				this.Report.Error(501, this.Location, "`{0}' must have a body because it is not marked abstract, extern, or partial", this.GetSignatureForError());
				return false;
			}
			return true;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00066BCC File Offset: 0x00064DCC
		protected void CheckProtectedModifier()
		{
			if ((this.ModFlags & Modifiers.PROTECTED) == (Modifiers)0)
			{
				return;
			}
			if (this.Parent.PartialContainer.Kind == MemberKind.Struct)
			{
				this.Report.Error(666, this.Location, "`{0}': Structs cannot contain protected members", this.GetSignatureForError());
				return;
			}
			if ((this.Parent.ModFlags & Modifiers.STATIC) != (Modifiers)0)
			{
				this.Report.Error(1057, this.Location, "`{0}': Static classes cannot contain protected members", this.GetSignatureForError());
				return;
			}
			if ((this.Parent.ModFlags & Modifiers.SEALED) != (Modifiers)0 && (this.ModFlags & Modifiers.OVERRIDE) == (Modifiers)0 && !(this is Destructor))
			{
				this.Report.Warning(628, 4, this.Location, "`{0}': new protected member declared in sealed class", this.GetSignatureForError());
				return;
			}
		}

		// Token: 0x06001567 RID: 5479
		public abstract bool Define();

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0003B0F3 File Offset: 0x000392F3
		// (set) Token: 0x06001569 RID: 5481 RVA: 0x00066C9B File Offset: 0x00064E9B
		public virtual string DocComment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00066CA4 File Offset: 0x00064EA4
		public virtual string GetSignatureForError()
		{
			string signatureForError = this.Parent.GetSignatureForError();
			if (signatureForError == null)
			{
				return this.member_name.GetSignatureForError();
			}
			return signatureForError + "." + this.member_name.GetSignatureForError();
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00066CE2 File Offset: 0x00064EE2
		public virtual void Emit()
		{
			if (!this.Compiler.Settings.VerifyClsCompliance)
			{
				return;
			}
			this.VerifyClsCompliance();
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x00066CFE File Offset: 0x00064EFE
		// (set) Token: 0x0600156D RID: 5485 RVA: 0x00066D0F File Offset: 0x00064F0F
		public bool IsAvailableForReuse
		{
			get
			{
				return (this.caching_flags & MemberCore.Flags.CanBeReused) > (MemberCore.Flags)0;
			}
			set
			{
				this.caching_flags = (value ? (this.caching_flags | MemberCore.Flags.CanBeReused) : (this.caching_flags & ~MemberCore.Flags.CanBeReused));
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x00066D34 File Offset: 0x00064F34
		public bool IsCompilerGenerated
		{
			get
			{
				return (this.mod_flags & Modifiers.COMPILER_GENERATED) != (Modifiers)0 || (this.Parent != null && this.Parent.IsCompilerGenerated);
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x000022F4 File Offset: 0x000004F4
		public bool IsImported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x00066D5B File Offset: 0x00064F5B
		public virtual bool IsUsed
		{
			get
			{
				return (this.caching_flags & MemberCore.Flags.IsUsed) > (MemberCore.Flags)0;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x00066D6C File Offset: 0x00064F6C
		protected Report Report
		{
			get
			{
				return this.Compiler.Report;
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x00066D79 File Offset: 0x00064F79
		public void SetIsUsed()
		{
			this.caching_flags |= MemberCore.Flags.IsUsed;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00066D8D File Offset: 0x00064F8D
		public void SetIsAssigned()
		{
			this.caching_flags |= MemberCore.Flags.IsAssigned;
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00066DA4 File Offset: 0x00064FA4
		public virtual void SetConstraints(List<Constraints> constraints_list)
		{
			TypeParameters typeParameters = this.member_name.TypeParameters;
			if (typeParameters == null)
			{
				this.Report.Error(80, this.Location, "Constraints are not allowed on non-generic declarations");
				return;
			}
			foreach (Constraints constraints in constraints_list)
			{
				TypeParameter typeParameter = typeParameters.Find(constraints.TypeParameter.Value);
				if (typeParameter == null)
				{
					this.Report.Error(699, constraints.Location, "`{0}': A constraint references nonexistent type parameter `{1}'", this.GetSignatureForError(), constraints.TypeParameter.Value);
				}
				else
				{
					typeParameter.Constraints = constraints;
				}
			}
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00066E60 File Offset: 0x00065060
		public virtual ObsoleteAttribute GetAttributeObsolete()
		{
			if ((this.caching_flags & (MemberCore.Flags.Obsolete_Undetected | MemberCore.Flags.Obsolete)) == (MemberCore.Flags)0)
			{
				return null;
			}
			this.caching_flags &= ~MemberCore.Flags.Obsolete_Undetected;
			if (base.OptAttributes == null)
			{
				return null;
			}
			Attribute attribute = base.OptAttributes.Search(this.Module.PredefinedAttributes.Obsolete);
			if (attribute == null)
			{
				return null;
			}
			this.caching_flags |= MemberCore.Flags.Obsolete;
			ObsoleteAttribute obsoleteAttribute = attribute.GetObsoleteAttribute();
			if (obsoleteAttribute == null)
			{
				return null;
			}
			return obsoleteAttribute;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00066ED0 File Offset: 0x000650D0
		public virtual void CheckObsoleteness(Location loc)
		{
			ObsoleteAttribute attributeObsolete = this.GetAttributeObsolete();
			if (attributeObsolete != null)
			{
				AttributeTester.Report_ObsoleteMessage(attributeObsolete, this.GetSignatureForError(), loc, this.Report);
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00066EFC File Offset: 0x000650FC
		public bool IsAccessibleAs(TypeSpec p)
		{
			if ((this.mod_flags & Modifiers.PRIVATE) != (Modifiers)0)
			{
				return true;
			}
			while (TypeManager.HasElementType(p))
			{
				p = TypeManager.GetElementType(p);
			}
			if (p.IsGenericParameter)
			{
				return true;
			}
			while (p != null)
			{
				TypeSpec declaringType = p.DeclaringType;
				if (p.IsGeneric)
				{
					foreach (TypeSpec p2 in p.TypeArguments)
					{
						if (!this.IsAccessibleAs(p2))
						{
							return false;
						}
					}
				}
				Modifiers modifiers = p.Modifiers & Modifiers.AccessibilityMask;
				if (modifiers != Modifiers.PUBLIC)
				{
					bool flag = false;
					MemberCore memberCore = this;
					while (!flag && memberCore != null && memberCore.Parent != null)
					{
						Modifiers modifiers2 = memberCore.ModFlags & Modifiers.AccessibilityMask;
						if (modifiers <= Modifiers.PRIVATE)
						{
							if (modifiers == Modifiers.PROTECTED)
							{
								goto IL_D9;
							}
							if (modifiers != Modifiers.PRIVATE)
							{
								goto IL_20E;
							}
							if (modifiers2 == Modifiers.PRIVATE)
							{
								TypeContainer parent = memberCore.Parent;
								do
								{
									flag = (parent.CurrentType.MemberDefinition == declaringType.MemberDefinition);
									if (flag || parent.PartialContainer.IsTopLevel)
									{
										break;
									}
								}
								while ((parent = parent.Parent) != null);
							}
						}
						else if (modifiers != Modifiers.INTERNAL)
						{
							if (modifiers != (Modifiers.PROTECTED | Modifiers.INTERNAL))
							{
								goto IL_20E;
							}
							if (modifiers2 == Modifiers.INTERNAL)
							{
								flag = p.MemberDefinition.IsInternalAsPublic(memberCore.Module.DeclaringAssembly);
							}
							else if (modifiers2 == (Modifiers.PROTECTED | Modifiers.INTERNAL))
							{
								flag = (memberCore.Parent.PartialContainer.IsBaseTypeDefinition(declaringType) && p.MemberDefinition.IsInternalAsPublic(memberCore.Module.DeclaringAssembly));
							}
							else
							{
								if (modifiers2 == Modifiers.PROTECTED)
								{
									goto IL_D9;
								}
								if (modifiers2 == Modifiers.PRIVATE)
								{
									if (!p.MemberDefinition.IsInternalAsPublic(memberCore.Module.DeclaringAssembly))
									{
										goto IL_D9;
									}
									flag = true;
								}
							}
						}
						else if (modifiers2 == Modifiers.PRIVATE || modifiers2 == Modifiers.INTERNAL)
						{
							flag = p.MemberDefinition.IsInternalAsPublic(memberCore.Module.DeclaringAssembly);
						}
						IL_221:
						memberCore = memberCore.Parent;
						continue;
						IL_D9:
						if (modifiers2 == Modifiers.PROTECTED)
						{
							flag = memberCore.Parent.PartialContainer.IsBaseTypeDefinition(declaringType);
							goto IL_221;
						}
						if (modifiers2 == Modifiers.PRIVATE)
						{
							while (memberCore.Parent != null)
							{
								if (memberCore.Parent.PartialContainer == null)
								{
									break;
								}
								if (memberCore.Parent.PartialContainer.IsBaseTypeDefinition(declaringType))
								{
									flag = true;
									break;
								}
								memberCore = memberCore.Parent;
							}
							goto IL_221;
						}
						goto IL_221;
						IL_20E:
						throw new InternalErrorException(modifiers2.ToString());
					}
					if (!flag)
					{
						return false;
					}
				}
				p = declaringType;
			}
			return true;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00067158 File Offset: 0x00065358
		public override bool IsClsComplianceRequired()
		{
			if ((this.caching_flags & MemberCore.Flags.ClsCompliance_Undetected) == (MemberCore.Flags)0)
			{
				return (this.caching_flags & MemberCore.Flags.ClsCompliant) > (MemberCore.Flags)0;
			}
			this.caching_flags &= ~MemberCore.Flags.ClsCompliance_Undetected;
			if (this.HasClsCompliantAttribute)
			{
				if ((this.caching_flags & MemberCore.Flags.ClsCompliantAttributeFalse) != (MemberCore.Flags)0)
				{
					return false;
				}
				this.caching_flags |= MemberCore.Flags.ClsCompliant;
				return true;
			}
			else
			{
				if (this.Parent.IsClsComplianceRequired())
				{
					this.caching_flags |= MemberCore.Flags.ClsCompliant;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual string[] ConditionalConditions()
		{
			return null;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x000671D0 File Offset: 0x000653D0
		public bool IsExposedFromAssembly()
		{
			if ((this.ModFlags & (Modifiers.PROTECTED | Modifiers.PUBLIC)) == (Modifiers)0)
			{
				return this is NamespaceContainer;
			}
			for (TypeDefinition partialContainer = this.Parent.PartialContainer; partialContainer != null; partialContainer = partialContainer.Parent.PartialContainer)
			{
				if ((partialContainer.ModFlags & (Modifiers.PROTECTED | Modifiers.PUBLIC)) == (Modifiers)0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0006721C File Offset: 0x0006541C
		public ExtensionMethodCandidates LookupExtensionMethod(string name, int arity)
		{
			TypeContainer parent = this.Parent;
			NamespaceContainer namespaceContainer;
			for (;;)
			{
				namespaceContainer = (parent as NamespaceContainer);
				if (namespaceContainer != null)
				{
					break;
				}
				parent = parent.Parent;
				if (parent == null)
				{
					goto Block_2;
				}
			}
			return namespaceContainer.LookupExtensionMethod(this, name, arity, 0);
			Block_2:
			return null;
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00067250 File Offset: 0x00065450
		public virtual FullNamedExpression LookupNamespaceAlias(string name)
		{
			return this.Parent.LookupNamespaceAlias(name);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0006725E File Offset: 0x0006545E
		public virtual FullNamedExpression LookupNamespaceOrType(string name, int arity, LookupMode mode, Location loc)
		{
			return this.Parent.LookupNamespaceOrType(name, arity, mode, loc);
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00067270 File Offset: 0x00065470
		public bool? CLSAttributeValue
		{
			get
			{
				if ((this.caching_flags & MemberCore.Flags.HasCompliantAttribute_Undetected) != (MemberCore.Flags)0)
				{
					this.caching_flags &= ~MemberCore.Flags.HasCompliantAttribute_Undetected;
					if (base.OptAttributes != null)
					{
						Attribute attribute = base.OptAttributes.Search(this.Module.PredefinedAttributes.CLSCompliant);
						if (attribute != null)
						{
							this.caching_flags |= MemberCore.Flags.HasClsCompliantAttribute;
							if (attribute.GetClsCompliantAttributeValue())
							{
								return new bool?(true);
							}
							this.caching_flags |= MemberCore.Flags.ClsCompliantAttributeFalse;
							return new bool?(false);
						}
					}
					return null;
				}
				if ((this.caching_flags & MemberCore.Flags.HasClsCompliantAttribute) == (MemberCore.Flags)0)
				{
					return null;
				}
				return new bool?((this.caching_flags & MemberCore.Flags.ClsCompliantAttributeFalse) == (MemberCore.Flags)0);
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x00067328 File Offset: 0x00065528
		protected bool HasClsCompliantAttribute
		{
			get
			{
				return this.CLSAttributeValue != null;
			}
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool EnableOverloadChecks(MemberCore overload)
		{
			return false;
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00067344 File Offset: 0x00065544
		protected virtual bool VerifyClsCompliance()
		{
			if (this.HasClsCompliantAttribute)
			{
				if (!this.Module.DeclaringAssembly.HasCLSCompliantAttribute)
				{
					Attribute attribute = base.OptAttributes.Search(this.Module.PredefinedAttributes.CLSCompliant);
					if ((this.caching_flags & MemberCore.Flags.ClsCompliantAttributeFalse) != (MemberCore.Flags)0)
					{
						this.Report.Warning(3021, 2, attribute.Location, "`{0}' does not need a CLSCompliant attribute because the assembly is not marked as CLS-compliant", this.GetSignatureForError());
					}
					else
					{
						this.Report.Warning(3014, 1, attribute.Location, "`{0}' cannot be marked as CLS-compliant because the assembly is not marked as CLS-compliant", this.GetSignatureForError());
					}
					return false;
				}
				if (!this.IsExposedFromAssembly())
				{
					Attribute attribute2 = base.OptAttributes.Search(this.Module.PredefinedAttributes.CLSCompliant);
					this.Report.Warning(3019, 2, attribute2.Location, "CLS compliance checking will not be performed on `{0}' because it is not visible from outside this assembly", this.GetSignatureForError());
					return false;
				}
				if ((this.caching_flags & MemberCore.Flags.ClsCompliantAttributeFalse) != (MemberCore.Flags)0)
				{
					if (this.Parent is Interface && this.Parent.IsClsComplianceRequired())
					{
						this.Report.Warning(3010, 1, this.Location, "`{0}': CLS-compliant interfaces must have only CLS-compliant members", this.GetSignatureForError());
					}
					else if (this.Parent.Kind == MemberKind.Class && (this.ModFlags & Modifiers.ABSTRACT) != (Modifiers)0 && this.Parent.IsClsComplianceRequired())
					{
						this.Report.Warning(3011, 1, this.Location, "`{0}': only CLS-compliant members can be abstract", this.GetSignatureForError());
					}
					return false;
				}
				if (this.Parent.Kind != MemberKind.Namespace && this.Parent.Kind != (MemberKind)0 && !this.Parent.IsClsComplianceRequired())
				{
					Attribute attribute3 = base.OptAttributes.Search(this.Module.PredefinedAttributes.CLSCompliant);
					this.Report.Warning(3018, 1, attribute3.Location, "`{0}' cannot be marked as CLS-compliant because it is a member of non CLS-compliant type `{1}'", this.GetSignatureForError(), this.Parent.GetSignatureForError());
					return false;
				}
			}
			else
			{
				if (!this.IsExposedFromAssembly())
				{
					return false;
				}
				if (!this.Parent.IsClsComplianceRequired())
				{
					return false;
				}
			}
			if (this.member_name.Name[0] == '_')
			{
				this.Warning_IdentifierNotCompliant();
			}
			if (this.member_name.TypeParameters != null)
			{
				this.member_name.TypeParameters.VerifyClsCompliance();
			}
			return true;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0006758E File Offset: 0x0006578E
		protected void Warning_IdentifierNotCompliant()
		{
			this.Report.Warning(3008, 1, this.MemberName.Location, "Identifier `{0}' is not CLS-compliant", this.GetSignatureForError());
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0003F215 File Offset: 0x0003D415
		public virtual string GetCallerMemberName()
		{
			return this.MemberName.Name;
		}

		// Token: 0x06001584 RID: 5508
		public abstract string GetSignatureForDocumentation();

		// Token: 0x06001585 RID: 5509 RVA: 0x000675B7 File Offset: 0x000657B7
		public virtual void GetCompletionStartingWith(string prefix, List<string> results)
		{
			this.Parent.GetCompletionStartingWith(prefix, results);
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x000675C8 File Offset: 0x000657C8
		public virtual void GenerateDocComment(DocumentationBuilder builder)
		{
			if (this.DocComment == null)
			{
				if (this.IsExposedFromAssembly())
				{
					Constructor constructor = this as Constructor;
					if (constructor == null || !constructor.IsDefault())
					{
						this.Report.Warning(1591, 4, this.Location, "Missing XML comment for publicly visible type or member `{0}'", this.GetSignatureForError());
					}
				}
				return;
			}
			try
			{
				builder.GenerateDocumentationForMember(this);
			}
			catch (Exception e)
			{
				throw new InternalErrorException(this, e);
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0000AF70 File Offset: 0x00009170
		public virtual void WriteDebugSymbol(MonoSymbolFile file)
		{
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x00067640 File Offset: 0x00065840
		public virtual CompilerContext Compiler
		{
			get
			{
				return this.Module.Compiler;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x0006764D File Offset: 0x0006584D
		public virtual TypeSpec CurrentType
		{
			get
			{
				return this.Parent.CurrentType;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x00005936 File Offset: 0x00003B36
		public MemberCore CurrentMemberDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x000055E7 File Offset: 0x000037E7
		public virtual TypeParameters CurrentTypeParameters
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0006765A File Offset: 0x0006585A
		public bool IsObsolete
		{
			get
			{
				return this.GetAttributeObsolete() != null || (this.Parent != null && this.Parent.IsObsolete);
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x0006767B File Offset: 0x0006587B
		public bool IsUnsafe
		{
			get
			{
				return (this.ModFlags & Modifiers.UNSAFE) != (Modifiers)0 || (this.Parent != null && this.Parent.IsUnsafe);
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x000676A2 File Offset: 0x000658A2
		public bool IsStatic
		{
			get
			{
				return (this.ModFlags & Modifiers.STATIC) > (Modifiers)0;
			}
		}

		// Token: 0x04000907 RID: 2311
		private MemberName member_name;

		// Token: 0x04000908 RID: 2312
		private Modifiers mod_flags;

		// Token: 0x04000909 RID: 2313
		public TypeContainer Parent;

		// Token: 0x0400090A RID: 2314
		protected string comment;

		// Token: 0x0400090B RID: 2315
		public MemberCore.Flags caching_flags;

		// Token: 0x020003A1 RID: 929
		[Flags]
		public enum Flags
		{
			// Token: 0x04000FD7 RID: 4055
			Obsolete_Undetected = 1,
			// Token: 0x04000FD8 RID: 4056
			Obsolete = 2,
			// Token: 0x04000FD9 RID: 4057
			ClsCompliance_Undetected = 4,
			// Token: 0x04000FDA RID: 4058
			ClsCompliant = 8,
			// Token: 0x04000FDB RID: 4059
			CloseTypeCreated = 16,
			// Token: 0x04000FDC RID: 4060
			HasCompliantAttribute_Undetected = 32,
			// Token: 0x04000FDD RID: 4061
			HasClsCompliantAttribute = 64,
			// Token: 0x04000FDE RID: 4062
			ClsCompliantAttributeFalse = 128,
			// Token: 0x04000FDF RID: 4063
			Excluded_Undetected = 256,
			// Token: 0x04000FE0 RID: 4064
			Excluded = 512,
			// Token: 0x04000FE1 RID: 4065
			MethodOverloadsExist = 1024,
			// Token: 0x04000FE2 RID: 4066
			IsUsed = 2048,
			// Token: 0x04000FE3 RID: 4067
			IsAssigned = 4096,
			// Token: 0x04000FE4 RID: 4068
			HasExplicitLayout = 8192,
			// Token: 0x04000FE5 RID: 4069
			PartialDefinitionExists = 16384,
			// Token: 0x04000FE6 RID: 4070
			HasStructLayout = 32768,
			// Token: 0x04000FE7 RID: 4071
			HasInstanceConstructor = 65536,
			// Token: 0x04000FE8 RID: 4072
			HasUserOperators = 131072,
			// Token: 0x04000FE9 RID: 4073
			CanBeReused = 262144,
			// Token: 0x04000FEA RID: 4074
			InterfacesExpanded = 524288
		}
	}
}
