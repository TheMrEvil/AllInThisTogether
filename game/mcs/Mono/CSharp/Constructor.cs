using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.CSharp
{
	// Token: 0x02000251 RID: 593
	public class Constructor : MethodCore, IMethodData, IMemberContext, IModuleContext, IMethodDefinition, IMemberDefinition
	{
		// Token: 0x06001D5D RID: 7517 RVA: 0x0008F65C File Offset: 0x0008D85C
		public Constructor(TypeDefinition parent, string name, Modifiers mod, Attributes attrs, ParametersCompiled args, Location loc) : base(parent, null, mod, Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.STATIC | Modifiers.EXTERN | Modifiers.UNSAFE, new MemberName(name, loc), attrs, args)
		{
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x0008F678 File Offset: 0x0008D878
		public bool HasCompliantArgs
		{
			get
			{
				return this.has_compliant_args;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x0008F680 File Offset: 0x0008D880
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.Constructor;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x000022F4 File Offset: 0x000004F4
		bool IMethodData.IsAccessor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x0008F684 File Offset: 0x0008D884
		// (set) Token: 0x06001D62 RID: 7522 RVA: 0x0008F68C File Offset: 0x0008D88C
		public bool IsPrimaryConstructor
		{
			[CompilerGenerated]
			get
			{
				return this.<IsPrimaryConstructor>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsPrimaryConstructor>k__BackingField = value;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x0008F695 File Offset: 0x0008D895
		MethodBase IMethodDefinition.Metadata
		{
			get
			{
				return this.ConstructorBuilder;
			}
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0008F6A0 File Offset: 0x0008D8A0
		public bool IsDefault()
		{
			if ((base.ModFlags & Modifiers.STATIC) != (Modifiers)0)
			{
				return this.parameters.IsEmpty;
			}
			return this.parameters.IsEmpty && this.Initializer is ConstructorBaseInitializer && this.Initializer.Arguments == null;
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0008F6F1 File Offset: 0x0008D8F1
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0008F6FC File Offset: 0x0008D8FC
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.IsValidSecurityAttribute())
			{
				a.ExtractSecurityPermissionSet(ctor, ref this.declarative_security);
				return;
			}
			if (a.Type == pa.MethodImpl)
			{
				this.is_external_implementation = a.IsInternalCall();
			}
			this.ConstructorBuilder.SetCustomAttribute((ConstructorInfo)ctor.GetMetaInfo(), cdata);
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0008F758 File Offset: 0x0008D958
		protected override bool CheckBase()
		{
			if ((base.ModFlags & Modifiers.STATIC) != (Modifiers)0)
			{
				if ((this.caching_flags & MemberCore.Flags.MethodOverloadsExist) != (MemberCore.Flags)0)
				{
					this.Parent.MemberCache.CheckExistingMembersOverloads(this, this.parameters);
				}
				return true;
			}
			if (!base.DefineParameters(this.parameters))
			{
				return false;
			}
			if ((this.caching_flags & MemberCore.Flags.MethodOverloadsExist) != (MemberCore.Flags)0)
			{
				this.Parent.MemberCache.CheckExistingMembersOverloads(this, this.parameters);
			}
			base.CheckProtectedModifier();
			return true;
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x0008F7D8 File Offset: 0x0008D9D8
		public override bool Define()
		{
			if (this.ConstructorBuilder != null)
			{
				return true;
			}
			if (!base.CheckAbstractAndExtern(this.block != null))
			{
				return false;
			}
			if (!this.CheckBase())
			{
				return false;
			}
			if (this.Parent.PrimaryConstructorParameters != null && !this.IsPrimaryConstructor && !base.IsStatic)
			{
				if (this.Parent.Kind == MemberKind.Struct && this.Initializer is ConstructorThisInitializer && this.Initializer.Arguments == null)
				{
					base.Report.Error(8043, base.Location, "`{0}': Structs with primary constructor cannot specify default constructor initializer", this.GetSignatureForError());
				}
				else if (this.Initializer == null || this.Initializer is ConstructorBaseInitializer)
				{
					base.Report.Error(8037, base.Location, "`{0}': Instance constructor of type with primary constructor must specify `this' constructor initializer", this.GetSignatureForError());
				}
			}
			if ((base.ModFlags & Modifiers.EXTERN) != (Modifiers)0 && this.Initializer != null)
			{
				base.Report.Error(8091, base.Location, "`{0}': Contructors cannot be extern and have a constructor initializer", this.GetSignatureForError());
			}
			MethodAttributes attributes = ModifiersExtensions.MethodAttr(base.ModFlags) | MethodAttributes.RTSpecialName | MethodAttributes.SpecialName;
			this.ConstructorBuilder = this.Parent.TypeBuilder.DefineConstructor(attributes, base.CallingConventions, this.parameters.GetMetaInfo());
			this.spec = new MethodSpec(MemberKind.Constructor, this.Parent.Definition, this, this.Compiler.BuiltinTypes.Void, this.parameters, base.ModFlags);
			this.Parent.MemberCache.AddMember(this.spec);
			if (this.block != null)
			{
				if (this.block.IsIterator)
				{
					this.member_type = this.Compiler.BuiltinTypes.Void;
					Iterator.CreateIterator(this, this.Parent.PartialContainer, base.ModFlags);
				}
				if (this.Compiler.Settings.WriteMetadataOnly)
				{
					this.block = null;
				}
			}
			return true;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0008F9D4 File Offset: 0x0008DBD4
		public override void Emit()
		{
			if (this.Parent.PartialContainer.IsComImport)
			{
				if (!this.IsDefault())
				{
					base.Report.Error(669, base.Location, "`{0}': A class with the ComImport attribute cannot have a user-defined constructor", this.Parent.GetSignatureForError());
				}
				this.ConstructorBuilder.SetImplementationFlags(MethodImplAttributes.InternalCall);
				this.block = null;
			}
			if ((base.ModFlags & Modifiers.DEBUGGER_HIDDEN) != (Modifiers)0)
			{
				this.Module.PredefinedAttributes.DebuggerHidden.EmitAttribute(this.ConstructorBuilder);
			}
			if (base.OptAttributes != null)
			{
				base.OptAttributes.Emit();
			}
			base.Emit();
			this.parameters.ApplyAttributes(this, this.ConstructorBuilder);
			BlockContext blockContext = new BlockContext(this, this.block, this.Compiler.BuiltinTypes.Void);
			blockContext.Set(ResolveContext.Options.ConstructorScope);
			if (this.block != null)
			{
				if (!base.IsStatic && this.Initializer == null && this.Parent.PartialContainer.Kind == MemberKind.Struct)
				{
					this.block.AddThisVariable(blockContext);
				}
				if (!(this.Initializer is ConstructorThisInitializer))
				{
					this.Parent.PartialContainer.ResolveFieldInitializers(blockContext);
				}
				if (!base.IsStatic)
				{
					if (this.Initializer == null && this.Parent.PartialContainer.Kind == MemberKind.Class)
					{
						this.Initializer = new GeneratedBaseInitializer(base.Location, null);
					}
					if (this.Initializer != null)
					{
						this.block.AddScopeStatement(new StatementExpression(this.Initializer));
					}
				}
				if (this.block.Resolve(blockContext, this))
				{
					this.debug_builder = this.Parent.CreateMethodSymbolEntry();
					EmitContext emitContext = new EmitContext(this, this.ConstructorBuilder.GetILGenerator(), blockContext.ReturnType, this.debug_builder);
					emitContext.With(BuilderContext.Options.ConstructorScope, true);
					this.block.Emit(emitContext);
				}
			}
			if (this.declarative_security != null)
			{
				foreach (KeyValuePair<SecurityAction, PermissionSet> keyValuePair in this.declarative_security)
				{
					this.ConstructorBuilder.AddDeclarativeSecurity(keyValuePair.Key, keyValuePair.Value);
				}
			}
			this.block = null;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0008FC24 File Offset: 0x0008DE24
		protected override MemberSpec FindBaseMember(out MemberSpec bestCandidate, ref bool overrides)
		{
			bestCandidate = null;
			return null;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0008FC2A File Offset: 0x0008DE2A
		public override string GetCallerMemberName()
		{
			if (!base.IsStatic)
			{
				return Constructor.ConstructorName;
			}
			return Constructor.TypeConstructorName;
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0008FC3F File Offset: 0x0008DE3F
		public override string GetSignatureForDocumentation()
		{
			return this.Parent.GetSignatureForDocumentation() + ".#ctor" + this.parameters.GetSignatureForDocumentation();
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0008E45A File Offset: 0x0008C65A
		public override string GetSignatureForError()
		{
			return base.GetSignatureForError() + this.parameters.GetSignatureForError();
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x0008FC61 File Offset: 0x0008DE61
		public override string[] ValidAttributeTargets
		{
			get
			{
				return Constructor.attribute_targets;
			}
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0008FC68 File Offset: 0x0008DE68
		protected override bool VerifyClsCompliance()
		{
			if (!base.VerifyClsCompliance() || !base.IsExposedFromAssembly())
			{
				return false;
			}
			if (!this.parameters.IsEmpty && this.Parent.Definition.IsAttribute)
			{
				TypeSpec[] types = this.parameters.Types;
				for (int i = 0; i < types.Length; i++)
				{
					if (types[i].IsArray)
					{
						return true;
					}
				}
			}
			this.has_compliant_args = true;
			return true;
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0008FCD4 File Offset: 0x0008DED4
		public override void WriteDebugSymbol(MonoSymbolFile file)
		{
			if (this.debug_builder == null)
			{
				return;
			}
			int token = this.ConstructorBuilder.GetToken().Token;
			this.debug_builder.DefineMethod(file, token);
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x0008E138 File Offset: 0x0008C338
		public MemberName MethodName
		{
			get
			{
				return base.MemberName;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x0008E130 File Offset: 0x0008C330
		public TypeSpec ReturnType
		{
			get
			{
				return base.MemberType;
			}
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00023DF4 File Offset: 0x00021FF4
		EmitContext IMethodData.CreateEmitContext(ILGenerator ig, SourceMethodBuilder sourceMethod)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0008FD0B File Offset: 0x0008DF0B
		// Note: this type is marked as 'beforefieldinit'.
		static Constructor()
		{
		}

		// Token: 0x04000AD8 RID: 2776
		public ConstructorBuilder ConstructorBuilder;

		// Token: 0x04000AD9 RID: 2777
		public ConstructorInitializer Initializer;

		// Token: 0x04000ADA RID: 2778
		private Dictionary<SecurityAction, PermissionSet> declarative_security;

		// Token: 0x04000ADB RID: 2779
		private bool has_compliant_args;

		// Token: 0x04000ADC RID: 2780
		private SourceMethodBuilder debug_builder;

		// Token: 0x04000ADD RID: 2781
		public const Modifiers AllowedModifiers = Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.STATIC | Modifiers.EXTERN | Modifiers.UNSAFE;

		// Token: 0x04000ADE RID: 2782
		private static readonly string[] attribute_targets = new string[]
		{
			"method"
		};

		// Token: 0x04000ADF RID: 2783
		public static readonly string ConstructorName = ".ctor";

		// Token: 0x04000AE0 RID: 2784
		public static readonly string TypeConstructorName = ".cctor";

		// Token: 0x04000AE1 RID: 2785
		[CompilerGenerated]
		private bool <IsPrimaryConstructor>k__BackingField;
	}
}
