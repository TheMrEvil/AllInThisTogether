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
	// Token: 0x0200024B RID: 587
	public abstract class MethodOrOperator : MethodCore, IMethodData, IMemberContext, IModuleContext, IMethodDefinition, IMemberDefinition
	{
		// Token: 0x06001D26 RID: 7462 RVA: 0x0008DAFA File Offset: 0x0008BCFA
		protected MethodOrOperator(TypeDefinition parent, FullNamedExpression type, Modifiers mod, Modifiers allowed_mod, MemberName name, Attributes attrs, ParametersCompiled parameters) : base(parent, type, mod, allowed_mod, name, attrs, parameters)
		{
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x0008DB10 File Offset: 0x0008BD10
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Target == AttributeTargets.ReturnValue)
			{
				if (this.return_attributes == null)
				{
					this.return_attributes = new ReturnParameter(this, this.MethodBuilder, base.Location);
				}
				this.return_attributes.ApplyAttributeBuilder(a, ctor, cdata, pa);
				return;
			}
			if (a.Type == pa.MethodImpl)
			{
				if ((base.ModFlags & Modifiers.ASYNC) != (Modifiers)0 && (a.GetMethodImplOptions() & MethodImplOptions.Synchronized) != (MethodImplOptions)0)
				{
					base.Report.Error(4015, a.Location, "`{0}': Async methods cannot use `MethodImplOptions.Synchronized'", this.GetSignatureForError());
				}
				this.is_external_implementation = a.IsInternalCall();
			}
			else if (a.Type == pa.DllImport)
			{
				if ((base.ModFlags & (Modifiers.STATIC | Modifiers.EXTERN)) != (Modifiers.STATIC | Modifiers.EXTERN))
				{
					base.Report.Error(601, a.Location, "The DllImport attribute must be specified on a method marked `static' and `extern'");
				}
				if (base.MemberName.IsGeneric || this.Parent.IsGenericOrParentIsGeneric)
				{
					base.Report.Error(7042, a.Location, "The DllImport attribute cannot be applied to a method that is generic or contained in a generic type");
				}
				this.is_external_implementation = true;
			}
			if (a.IsValidSecurityAttribute())
			{
				a.ExtractSecurityPermissionSet(ctor, ref this.declarative_security);
				return;
			}
			if (this.MethodBuilder != null)
			{
				this.MethodBuilder.SetCustomAttribute((ConstructorInfo)ctor.GetMetaInfo(), cdata);
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x0008DC6A File Offset: 0x0008BE6A
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.Method;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x0008DC6E File Offset: 0x0008BE6E
		MethodBase IMethodDefinition.Metadata
		{
			get
			{
				return this.MethodData.MethodBuilder;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x0008DC6E File Offset: 0x0008BE6E
		public MethodBuilder MethodBuilder
		{
			get
			{
				return this.MethodData.MethodBuilder;
			}
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0008DC7B File Offset: 0x0008BE7B
		protected override bool CheckForDuplications()
		{
			return this.Parent.MemberCache.CheckExistingMembersOverloads(this, this.parameters);
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x0008DC94 File Offset: 0x0008BE94
		public virtual EmitContext CreateEmitContext(ILGenerator ig, SourceMethodBuilder sourceMethod)
		{
			return new EmitContext(this, ig, base.MemberType, sourceMethod);
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0008DCA4 File Offset: 0x0008BEA4
		public override bool Define()
		{
			if (!base.Define())
			{
				return false;
			}
			if (!this.CheckBase())
			{
				return false;
			}
			MemberKind kind;
			if (this is Operator)
			{
				kind = MemberKind.Operator;
			}
			else if (this is Destructor)
			{
				kind = MemberKind.Destructor;
			}
			else
			{
				kind = MemberKind.Method;
			}
			string exlicitName;
			if (this.IsPartialDefinition)
			{
				this.caching_flags &= ~MemberCore.Flags.Excluded_Undetected;
				this.caching_flags |= MemberCore.Flags.Excluded;
				if ((this.caching_flags & MemberCore.Flags.PartialDefinitionExists) != (MemberCore.Flags)0)
				{
					return true;
				}
				if (this.IsExplicitImpl)
				{
					return true;
				}
				exlicitName = null;
			}
			else
			{
				this.MethodData = new MethodData(this, base.ModFlags, this.flags, this, this.base_method);
				if (!this.MethodData.Define(this.Parent.PartialContainer, base.GetFullName(base.MemberName)))
				{
					return false;
				}
				exlicitName = this.MethodData.MetadataName;
			}
			this.spec = new MethodSpec(kind, this.Parent.Definition, this, this.ReturnType, this.parameters, base.ModFlags);
			if (base.MemberName.Arity > 0)
			{
				this.spec.IsGeneric = true;
			}
			this.Parent.MemberCache.AddMember(this, exlicitName, this.spec);
			return true;
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0008DDDC File Offset: 0x0008BFDC
		protected override void DoMemberTypeIndependentChecks()
		{
			base.DoMemberTypeIndependentChecks();
			base.CheckAbstractAndExtern(this.block != null);
			if ((base.ModFlags & Modifiers.PARTIAL) != (Modifiers)0)
			{
				for (int i = 0; i < this.parameters.Count; i++)
				{
					IParameterData parameterData = this.parameters.FixedParameters[i];
					if ((parameterData.ModFlags & Parameter.Modifier.OUT) != Parameter.Modifier.NONE)
					{
						base.Report.Error(752, base.Location, "`{0}': A partial method parameters cannot use `out' modifier", this.GetSignatureForError());
					}
					if (parameterData.HasDefaultValue && this.IsPartialImplementation)
					{
						((Parameter)parameterData).Warning_UselessOptionalParameter(base.Report);
					}
				}
			}
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0008DE7D File Offset: 0x0008C07D
		protected override void DoMemberTypeDependentChecks()
		{
			base.DoMemberTypeDependentChecks();
			if (base.MemberType.IsStatic)
			{
				base.Error_StaticReturnType();
			}
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x0008DE98 File Offset: 0x0008C098
		public override void Emit()
		{
			if ((base.ModFlags & Modifiers.COMPILER_GENERATED) != (Modifiers)0 && !this.Parent.IsCompilerGenerated)
			{
				this.Module.PredefinedAttributes.CompilerGenerated.EmitAttribute(this.MethodBuilder);
			}
			if ((base.ModFlags & Modifiers.DEBUGGER_HIDDEN) != (Modifiers)0)
			{
				this.Module.PredefinedAttributes.DebuggerHidden.EmitAttribute(this.MethodBuilder);
			}
			if ((base.ModFlags & Modifiers.DEBUGGER_STEP_THROUGH) != (Modifiers)0)
			{
				this.Module.PredefinedAttributes.DebuggerStepThrough.EmitAttribute(this.MethodBuilder);
			}
			if (this.ReturnType.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				this.return_attributes = new ReturnParameter(this, this.MethodBuilder, base.Location);
				this.Module.PredefinedAttributes.Dynamic.EmitAttribute(this.return_attributes.Builder);
			}
			else if (this.ReturnType.HasDynamicElement)
			{
				this.return_attributes = new ReturnParameter(this, this.MethodBuilder, base.Location);
				this.Module.PredefinedAttributes.Dynamic.EmitAttribute(this.return_attributes.Builder, this.ReturnType, base.Location);
			}
			if (base.OptAttributes != null)
			{
				base.OptAttributes.Emit();
			}
			if (this.declarative_security != null)
			{
				foreach (KeyValuePair<SecurityAction, PermissionSet> keyValuePair in this.declarative_security)
				{
					this.MethodBuilder.AddDeclarativeSecurity(keyValuePair.Key, keyValuePair.Value);
				}
			}
			if (this.type_expr != null)
			{
				ConstraintChecker.Check(this, this.member_type, this.type_expr.Location);
			}
			base.Emit();
			if (this.MethodData != null)
			{
				this.MethodData.Emit(this.Parent);
			}
			if (this.block != null && this.block.StateMachine is AsyncTaskStorey)
			{
				this.Module.PredefinedAttributes.AsyncStateMachine.EmitAttribute(this.MethodBuilder, this.block.StateMachine);
			}
			if ((base.ModFlags & Modifiers.PARTIAL) == (Modifiers)0)
			{
				base.Block = null;
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0008E0D0 File Offset: 0x0008C2D0
		protected void Error_ConditionalAttributeIsNotValid()
		{
			base.Report.Error(577, base.Location, "Conditional not valid on `{0}' because it is a constructor, destructor, operator or explicit interface implementation", this.GetSignatureForError());
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x0008E0F3 File Offset: 0x0008C2F3
		public bool IsPartialDefinition
		{
			get
			{
				return (base.ModFlags & Modifiers.PARTIAL) != (Modifiers)0 && base.Block == null;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x0008E10E File Offset: 0x0008C30E
		public bool IsPartialImplementation
		{
			get
			{
				return (base.ModFlags & Modifiers.PARTIAL) != (Modifiers)0 && base.Block != null;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x0008E129 File Offset: 0x0008C329
		public override string[] ValidAttributeTargets
		{
			get
			{
				return MethodOrOperator.attribute_targets;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x000022F4 File Offset: 0x000004F4
		bool IMethodData.IsAccessor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x0008E130 File Offset: 0x0008C330
		public TypeSpec ReturnType
		{
			get
			{
				return base.MemberType;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0008E138 File Offset: 0x0008C338
		public MemberName MethodName
		{
			get
			{
				return base.MemberName;
			}
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0008E140 File Offset: 0x0008C340
		public override string[] ConditionalConditions()
		{
			if ((this.caching_flags & (MemberCore.Flags.Excluded_Undetected | MemberCore.Flags.Excluded)) == (MemberCore.Flags)0)
			{
				return null;
			}
			if ((base.ModFlags & Modifiers.PARTIAL) != (Modifiers)0 && (this.caching_flags & MemberCore.Flags.Excluded) != (MemberCore.Flags)0)
			{
				return new string[0];
			}
			this.caching_flags &= ~MemberCore.Flags.Excluded_Undetected;
			string[] array2;
			if (this.base_method == null)
			{
				if (base.OptAttributes == null)
				{
					return null;
				}
				Attribute[] array = base.OptAttributes.SearchMulti(this.Module.PredefinedAttributes.Conditional);
				if (array == null)
				{
					return null;
				}
				array2 = new string[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = array[i].GetConditionalAttributeValue();
				}
			}
			else
			{
				array2 = this.base_method.MemberDefinition.ConditionalConditions();
			}
			if (array2 != null)
			{
				this.caching_flags |= MemberCore.Flags.Excluded;
			}
			return array2;
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0008E210 File Offset: 0x0008C410
		public override void PrepareEmit()
		{
			base.PrepareEmit();
			MethodBuilder methodBuilder = this.MethodData.DefineMethodBuilder(this.Parent);
			if (this.CurrentTypeParameters != null)
			{
				string[] array = new string[this.CurrentTypeParameters.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.CurrentTypeParameters[i].Name;
				}
				GenericTypeParameterBuilder[] array2 = this.MethodBuilder.DefineGenericParameters(array);
				for (int j = 0; j < this.CurrentTypeParameters.Count; j++)
				{
					this.CurrentTypeParameters[j].Define(array2[j]);
				}
			}
			methodBuilder.SetParameters(this.parameters.GetMetaInfo());
			methodBuilder.SetReturnType(this.ReturnType.GetMetaInfo());
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0008E2CF File Offset: 0x0008C4CF
		public override void WriteDebugSymbol(MonoSymbolFile file)
		{
			if (this.MethodData != null && !this.IsPartialDefinition)
			{
				this.MethodData.WriteDebugSymbol(file);
			}
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x0008E2ED File Offset: 0x0008C4ED
		// Note: this type is marked as 'beforefieldinit'.
		static MethodOrOperator()
		{
		}

		// Token: 0x04000AD1 RID: 2769
		private ReturnParameter return_attributes;

		// Token: 0x04000AD2 RID: 2770
		private Dictionary<SecurityAction, PermissionSet> declarative_security;

		// Token: 0x04000AD3 RID: 2771
		protected MethodData MethodData;

		// Token: 0x04000AD4 RID: 2772
		private static readonly string[] attribute_targets = new string[]
		{
			"method",
			"return"
		};
	}
}
