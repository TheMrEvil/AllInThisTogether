using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.CSharp
{
	// Token: 0x0200012C RID: 300
	public abstract class TypeContainer : MemberCore
	{
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x00039E3C File Offset: 0x0003803C
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x00039E44 File Offset: 0x00038044
		public int CounterAnonymousMethods
		{
			[CompilerGenerated]
			get
			{
				return this.<CounterAnonymousMethods>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CounterAnonymousMethods>k__BackingField = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00039E4D File Offset: 0x0003804D
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x00039E55 File Offset: 0x00038055
		public int CounterAnonymousContainers
		{
			[CompilerGenerated]
			get
			{
				return this.<CounterAnonymousContainers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CounterAnonymousContainers>k__BackingField = value;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00039E5E File Offset: 0x0003805E
		// (set) Token: 0x06000E9C RID: 3740 RVA: 0x00039E66 File Offset: 0x00038066
		public int CounterSwitchTypes
		{
			[CompilerGenerated]
			get
			{
				return this.<CounterSwitchTypes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CounterSwitchTypes>k__BackingField = value;
			}
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00039E6F File Offset: 0x0003806F
		protected TypeContainer(TypeContainer parent, MemberName name, Attributes attrs, MemberKind kind) : base(parent, name, attrs)
		{
			this.Kind = kind;
			this.defined_names = new Dictionary<string, MemberCore>();
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x000055E7 File Offset: 0x000037E7
		public override TypeSpec CurrentType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x00039E8D File Offset: 0x0003808D
		public Dictionary<string, MemberCore> DefinedNames
		{
			get
			{
				return this.defined_names;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00039E95 File Offset: 0x00038095
		// (set) Token: 0x06000EA1 RID: 3745 RVA: 0x00039E9D File Offset: 0x0003809D
		public TypeDefinition PartialContainer
		{
			get
			{
				return this.main_container;
			}
			protected set
			{
				this.main_container = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x00039EA6 File Offset: 0x000380A6
		public IList<TypeContainer> Containers
		{
			get
			{
				return this.containers;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00039EAE File Offset: 0x000380AE
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x00039EB6 File Offset: 0x000380B6
		public Attributes UnattachedAttributes
		{
			[CompilerGenerated]
			get
			{
				return this.<UnattachedAttributes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnattachedAttributes>k__BackingField = value;
			}
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00039EBF File Offset: 0x000380BF
		public void AddCompilerGeneratedClass(CompilerGeneratedContainer c)
		{
			this.AddTypeContainerMember(c);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00039EC8 File Offset: 0x000380C8
		public virtual void AddPartial(TypeDefinition next_part)
		{
			MemberCore memberCore;
			(this.PartialContainer ?? this).defined_names.TryGetValue(next_part.MemberName.Basename, out memberCore);
			this.AddPartial(next_part, memberCore as TypeDefinition);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00039F08 File Offset: 0x00038108
		protected void AddPartial(TypeDefinition next_part, TypeDefinition existing)
		{
			next_part.ModFlags |= Modifiers.PARTIAL;
			if (existing == null)
			{
				this.AddTypeContainer(next_part);
				return;
			}
			if ((existing.ModFlags & Modifiers.PARTIAL) != (Modifiers)0)
			{
				if (existing.Kind != next_part.Kind)
				{
					base.Report.SymbolRelatedToPreviousError(existing);
					base.Report.Error(261, next_part.Location, "Partial declarations of `{0}' must be all classes, all structs or all interfaces", next_part.GetSignatureForError());
				}
				if ((existing.ModFlags & Modifiers.AccessibilityMask) != (next_part.ModFlags & Modifiers.AccessibilityMask) && (existing.ModFlags & Modifiers.DEFAULT_ACCESS_MODIFIER) == (Modifiers)0 && (next_part.ModFlags & Modifiers.DEFAULT_ACCESS_MODIFIER) == (Modifiers)0)
				{
					base.Report.SymbolRelatedToPreviousError(existing);
					base.Report.Error(262, next_part.Location, "Partial declarations of `{0}' have conflicting accessibility modifiers", next_part.GetSignatureForError());
				}
				TypeParameters currentTypeParameters = existing.CurrentTypeParameters;
				if (currentTypeParameters != null)
				{
					for (int i = 0; i < currentTypeParameters.Count; i++)
					{
						TypeParameter typeParameter = next_part.MemberName.TypeParameters[i];
						if (currentTypeParameters[i].MemberName.Name != typeParameter.MemberName.Name)
						{
							base.Report.SymbolRelatedToPreviousError(existing.Location, "");
							base.Report.Error(264, next_part.Location, "Partial declarations of `{0}' must have the same type parameter names in the same order", next_part.GetSignatureForError());
							break;
						}
						if (currentTypeParameters[i].Variance != typeParameter.Variance)
						{
							base.Report.SymbolRelatedToPreviousError(existing.Location, "");
							base.Report.Error(1067, next_part.Location, "Partial declarations of `{0}' must have the same type parameter variance modifiers", next_part.GetSignatureForError());
							break;
						}
					}
				}
				if ((next_part.ModFlags & Modifiers.DEFAULT_ACCESS_MODIFIER) != (Modifiers)0)
				{
					existing.ModFlags |= (next_part.ModFlags & ~(Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.DEFAULT_ACCESS_MODIFIER));
				}
				else if ((existing.ModFlags & Modifiers.DEFAULT_ACCESS_MODIFIER) != (Modifiers)0)
				{
					existing.ModFlags &= ~(Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.DEFAULT_ACCESS_MODIFIER);
					existing.ModFlags |= next_part.ModFlags;
				}
				else
				{
					existing.ModFlags |= next_part.ModFlags;
				}
				existing.Definition.Modifiers = existing.ModFlags;
				if (next_part.attributes != null)
				{
					if (existing.attributes == null)
					{
						existing.attributes = next_part.attributes;
					}
					else
					{
						existing.attributes.AddAttributes(next_part.attributes.Attrs);
					}
				}
				next_part.PartialContainer = existing;
				existing.AddPartialPart(next_part);
				this.AddTypeContainerMember(next_part);
				return;
			}
			if (existing.Kind != next_part.Kind)
			{
				this.AddTypeContainer(next_part);
				return;
			}
			base.Report.SymbolRelatedToPreviousError(next_part);
			this.Error_MissingPartialModifier(existing);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003A1AC File Offset: 0x000383AC
		public virtual void AddTypeContainer(TypeContainer tc)
		{
			this.AddTypeContainerMember(tc);
			TypeParameters typeParameters = tc.MemberName.TypeParameters;
			if (typeParameters != null && tc.PartialContainer != null)
			{
				TypeDefinition typeDefinition = (TypeDefinition)tc;
				for (int i = 0; i < typeParameters.Count; i++)
				{
					TypeParameter typeParameter = typeParameters[i];
					if (typeParameter.MemberName != null)
					{
						typeDefinition.AddNameToContainer(typeParameter, typeParameter.Name);
					}
				}
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003A20C File Offset: 0x0003840C
		protected virtual void AddTypeContainerMember(TypeContainer tc)
		{
			this.containers.Add(tc);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003A21C File Offset: 0x0003841C
		public virtual void CloseContainer()
		{
			if (this.containers != null)
			{
				foreach (TypeContainer typeContainer in this.containers)
				{
					typeContainer.CloseContainer();
				}
			}
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003A274 File Offset: 0x00038474
		public virtual void CreateMetadataName(StringBuilder sb)
		{
			if (this.Parent != null && this.Parent.MemberName != null)
			{
				this.Parent.CreateMetadataName(sb);
			}
			base.MemberName.CreateMetadataName(sb);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003A2A4 File Offset: 0x000384A4
		public virtual bool CreateContainer()
		{
			if (this.containers != null)
			{
				foreach (TypeContainer typeContainer in this.containers)
				{
					typeContainer.CreateContainer();
				}
			}
			return true;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003A300 File Offset: 0x00038500
		public override bool Define()
		{
			if (this.containers != null)
			{
				foreach (TypeContainer typeContainer in this.containers)
				{
					typeContainer.Define();
				}
			}
			if (this.Module.Evaluator == null)
			{
				this.defined_names = null;
			}
			else
			{
				this.defined_names.Clear();
			}
			return true;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003A37C File Offset: 0x0003857C
		public virtual void PrepareEmit()
		{
			if (this.containers != null)
			{
				foreach (TypeContainer typeContainer in this.containers)
				{
					try
					{
						typeContainer.PrepareEmit();
					}
					catch (Exception e)
					{
						if (base.MemberName == MemberName.Null)
						{
							throw;
						}
						throw new InternalErrorException(typeContainer, e);
					}
				}
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003A3FC File Offset: 0x000385FC
		public virtual bool DefineContainer()
		{
			if (this.is_defined)
			{
				return true;
			}
			this.is_defined = true;
			this.DoDefineContainer();
			if (this.containers != null)
			{
				foreach (TypeContainer typeContainer in this.containers)
				{
					try
					{
						typeContainer.DefineContainer();
					}
					catch (Exception e)
					{
						if (base.MemberName == MemberName.Null)
						{
							throw;
						}
						throw new InternalErrorException(typeContainer, e);
					}
				}
			}
			return true;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003A498 File Offset: 0x00038698
		public virtual void ExpandBaseInterfaces()
		{
			if (this.containers != null)
			{
				foreach (TypeContainer typeContainer in this.containers)
				{
					typeContainer.ExpandBaseInterfaces();
				}
			}
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003A4F0 File Offset: 0x000386F0
		protected virtual void DefineNamespace()
		{
			if (this.containers != null)
			{
				foreach (TypeContainer typeContainer in this.containers)
				{
					try
					{
						typeContainer.DefineNamespace();
					}
					catch (Exception e)
					{
						throw new InternalErrorException(typeContainer, e);
					}
				}
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0000AF70 File Offset: 0x00009170
		protected virtual void DoDefineContainer()
		{
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003A564 File Offset: 0x00038764
		public virtual void EmitContainer()
		{
			if (this.containers != null)
			{
				for (int i = 0; i < this.containers.Count; i++)
				{
					this.containers[i].EmitContainer();
				}
			}
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003A5A0 File Offset: 0x000387A0
		protected void Error_MissingPartialModifier(MemberCore type)
		{
			base.Report.Error(260, type.Location, "Missing partial modifier on declaration of type `{0}'. Another partial declaration of this type exists", type.GetSignatureForError());
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003A5C4 File Offset: 0x000387C4
		public override string GetSignatureForDocumentation()
		{
			if (this.Parent != null && this.Parent.MemberName != null)
			{
				return this.Parent.GetSignatureForDocumentation() + "." + base.MemberName.GetSignatureForDocumentation();
			}
			return base.MemberName.GetSignatureForDocumentation();
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003A614 File Offset: 0x00038814
		public override string GetSignatureForError()
		{
			if (this.Parent != null && this.Parent.MemberName != null)
			{
				return this.Parent.GetSignatureForError() + "." + base.MemberName.GetSignatureForError();
			}
			return base.MemberName.GetSignatureForError();
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0003A664 File Offset: 0x00038864
		public virtual string GetSignatureForMetadata()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.CreateMetadataName(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0003A684 File Offset: 0x00038884
		public virtual void RemoveContainer(TypeContainer cont)
		{
			if (this.containers != null)
			{
				this.containers.Remove(cont);
			}
			((this.Parent == this.Module) ? this.Module : this).defined_names.Remove(cont.MemberName.Basename);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0003A6D4 File Offset: 0x000388D4
		public virtual void VerifyMembers()
		{
			if (this.containers != null)
			{
				foreach (TypeContainer typeContainer in this.containers)
				{
					typeContainer.VerifyMembers();
				}
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0003A72C File Offset: 0x0003892C
		public override void WriteDebugSymbol(MonoSymbolFile file)
		{
			if (this.containers != null)
			{
				foreach (TypeContainer typeContainer in this.containers)
				{
					typeContainer.WriteDebugSymbol(file);
				}
			}
		}

		// Token: 0x040006CB RID: 1739
		public readonly MemberKind Kind;

		// Token: 0x040006CC RID: 1740
		protected List<TypeContainer> containers;

		// Token: 0x040006CD RID: 1741
		private TypeDefinition main_container;

		// Token: 0x040006CE RID: 1742
		protected Dictionary<string, MemberCore> defined_names;

		// Token: 0x040006CF RID: 1743
		protected bool is_defined;

		// Token: 0x040006D0 RID: 1744
		[CompilerGenerated]
		private int <CounterAnonymousMethods>k__BackingField;

		// Token: 0x040006D1 RID: 1745
		[CompilerGenerated]
		private int <CounterAnonymousContainers>k__BackingField;

		// Token: 0x040006D2 RID: 1746
		[CompilerGenerated]
		private int <CounterSwitchTypes>k__BackingField;

		// Token: 0x040006D3 RID: 1747
		[CompilerGenerated]
		private Attributes <UnattachedAttributes>k__BackingField;
	}
}
