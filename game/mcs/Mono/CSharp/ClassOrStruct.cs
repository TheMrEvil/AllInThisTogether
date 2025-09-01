using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace Mono.CSharp
{
	// Token: 0x0200012E RID: 302
	public abstract class ClassOrStruct : TypeDefinition
	{
		// Token: 0x06000F2C RID: 3884 RVA: 0x0003D65D File Offset: 0x0003B85D
		protected ClassOrStruct(TypeContainer parent, MemberName name, Attributes attrs, MemberKind kind) : base(parent, name, attrs, kind)
		{
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x0003D66A File Offset: 0x0003B86A
		// (set) Token: 0x06000F2E RID: 3886 RVA: 0x0003D672 File Offset: 0x0003B872
		public ToplevelBlock PrimaryConstructorBlock
		{
			[CompilerGenerated]
			get
			{
				return this.<PrimaryConstructorBlock>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PrimaryConstructorBlock>k__BackingField = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0003D67C File Offset: 0x0003B87C
		protected override TypeAttributes TypeAttr
		{
			get
			{
				TypeAttributes typeAttributes = base.TypeAttr;
				if (!this.has_static_constructor)
				{
					typeAttributes |= TypeAttributes.BeforeFieldInit;
				}
				if (this.Kind == MemberKind.Class)
				{
					typeAttributes |= TypeAttributes.NotPublic;
					if (base.IsStatic)
					{
						typeAttributes |= (TypeAttributes.Abstract | TypeAttributes.Sealed);
					}
				}
				else
				{
					typeAttributes |= TypeAttributes.SequentialLayout;
				}
				return typeAttributes;
			}
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0003D6C8 File Offset: 0x0003B8C8
		public override void AddNameToContainer(MemberCore symbol, string name)
		{
			if (!(symbol is Constructor) && symbol.MemberName.Name == base.MemberName.Name)
			{
				if (symbol is TypeParameter)
				{
					base.Report.Error(694, symbol.Location, "Type parameter `{0}' has same name as containing type, or method", symbol.GetSignatureForError());
					return;
				}
				InterfaceMemberBase interfaceMemberBase = symbol as InterfaceMemberBase;
				if (interfaceMemberBase == null || !interfaceMemberBase.IsExplicitImpl)
				{
					base.Report.SymbolRelatedToPreviousError(this);
					base.Report.Error(542, symbol.Location, "`{0}': member names cannot be the same as their enclosing type", symbol.GetSignatureForError());
					return;
				}
			}
			base.AddNameToContainer(symbol, name);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0003D770 File Offset: 0x0003B970
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.IsValidSecurityAttribute())
			{
				a.ExtractSecurityPermissionSet(ctor, ref this.declarative_security);
				return;
			}
			if (a.Type == pa.StructLayout)
			{
				base.PartialContainer.HasStructLayout = true;
				if (a.IsExplicitLayoutKind())
				{
					base.PartialContainer.HasExplicitLayout = true;
				}
			}
			if (a.Type == pa.Dynamic)
			{
				a.Error_MisusedDynamicAttribute();
				return;
			}
			base.ApplyAttributeBuilder(a, ctor, cdata, pa);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0003D7F0 File Offset: 0x0003B9F0
		protected virtual Constructor DefineDefaultConstructor(bool is_static)
		{
			Modifiers mod;
			ParametersCompiled parametersCompiled;
			if (is_static)
			{
				mod = (Modifiers.PRIVATE | Modifiers.STATIC);
				parametersCompiled = ParametersCompiled.EmptyReadOnlyParameters;
			}
			else
			{
				mod = (((base.ModFlags & Modifiers.ABSTRACT) != (Modifiers)0) ? Modifiers.PROTECTED : Modifiers.PUBLIC);
				parametersCompiled = (base.PrimaryConstructorParameters ?? ParametersCompiled.EmptyReadOnlyParameters);
			}
			Constructor constructor = new Constructor(this, base.MemberName.Name, mod, null, parametersCompiled, base.Location);
			if (this.Kind == MemberKind.Class)
			{
				constructor.Initializer = new GeneratedBaseInitializer(base.Location, base.PrimaryConstructorBaseArguments);
			}
			if (base.PrimaryConstructorParameters != null && !is_static)
			{
				constructor.IsPrimaryConstructor = true;
				constructor.caching_flags |= MemberCore.Flags.MethodOverloadsExist;
			}
			base.AddConstructor(constructor, true);
			if (this.PrimaryConstructorBlock == null)
			{
				constructor.Block = new ToplevelBlock(this.Compiler, parametersCompiled, base.Location, (Block.Flags)0)
				{
					IsCompilerGenerated = true
				};
			}
			else
			{
				constructor.Block = this.PrimaryConstructorBlock;
			}
			return constructor;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0003D8D4 File Offset: 0x0003BAD4
		protected override bool DoDefineMembers()
		{
			base.CheckProtectedModifier();
			if (base.PrimaryConstructorParameters != null)
			{
				foreach (Parameter parameter in base.PrimaryConstructorParameters.FixedParameters)
				{
					if (parameter.Name == base.MemberName.Name)
					{
						base.Report.Error(8039, parameter.Location, "Primary constructor of type `{0}' has parameter of same name as containing type", this.GetSignatureForError());
					}
					if (this.CurrentTypeParameters != null)
					{
						for (int j = 0; j < this.CurrentTypeParameters.Count; j++)
						{
							TypeParameter typeParameter = this.CurrentTypeParameters[j];
							if (parameter.Name == typeParameter.Name)
							{
								base.Report.Error(8038, parameter.Location, "Primary constructor of type `{0}' has parameter of same name as type parameter `{1}'", this.GetSignatureForError(), parameter.GetSignatureForError());
							}
						}
					}
				}
			}
			base.DoDefineMembers();
			return true;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0003D9C4 File Offset: 0x0003BBC4
		public override void Emit()
		{
			if (!this.has_static_constructor && base.HasStaticFieldInitializer)
			{
				this.DefineDefaultConstructor(true).Define();
			}
			base.Emit();
			if (this.declarative_security != null)
			{
				foreach (KeyValuePair<SecurityAction, PermissionSet> keyValuePair in this.declarative_security)
				{
					this.TypeBuilder.AddDeclarativeSecurity(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x040006F7 RID: 1783
		public const TypeAttributes StaticClassAttribute = TypeAttributes.Abstract | TypeAttributes.Sealed;

		// Token: 0x040006F8 RID: 1784
		private Dictionary<SecurityAction, PermissionSet> declarative_security;

		// Token: 0x040006F9 RID: 1785
		protected Constructor generated_primary_constructor;

		// Token: 0x040006FA RID: 1786
		[CompilerGenerated]
		private ToplevelBlock <PrimaryConstructorBlock>k__BackingField;
	}
}
