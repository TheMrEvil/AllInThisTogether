using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace Mono.CSharp
{
	// Token: 0x02000193 RID: 403
	public class Delegate : TypeDefinition, IParametersMember, IInterfaceMemberSpec
	{
		// Token: 0x060015B7 RID: 5559 RVA: 0x00067B78 File Offset: 0x00065D78
		public Delegate(TypeContainer parent, FullNamedExpression type, Modifiers mod_flags, MemberName name, ParametersCompiled param_list, Attributes attrs) : base(parent, name, attrs, MemberKind.Delegate)
		{
			this.ReturnType = type;
			base.ModFlags = ModifiersExtensions.Check(Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.UNSAFE, mod_flags, base.IsTopLevel ? Modifiers.INTERNAL : Modifiers.PRIVATE, name.Location, base.Report);
			this.parameters = param_list;
			this.spec = new TypeSpec(this.Kind, null, this, null, base.ModFlags | Modifiers.SEALED);
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x00067BEB File Offset: 0x00065DEB
		public TypeSpec MemberType
		{
			get
			{
				return this.ReturnType.Type;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00067BF8 File Offset: 0x00065DF8
		public AParametersCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00067C00 File Offset: 0x00065E00
		public FullNamedExpression TypExpression
		{
			get
			{
				return this.ReturnType;
			}
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00067C08 File Offset: 0x00065E08
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00067C14 File Offset: 0x00065E14
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Target == AttributeTargets.ReturnValue)
			{
				if (this.return_attributes == null)
				{
					this.return_attributes = new ReturnParameter(this, this.InvokeBuilder.MethodBuilder, base.Location);
				}
				this.return_attributes.ApplyAttributeBuilder(a, ctor, cdata, pa);
				return;
			}
			if (a.IsValidSecurityAttribute())
			{
				a.ExtractSecurityPermissionSet(ctor, ref this.declarative_security);
				return;
			}
			base.ApplyAttributeBuilder(a, ctor, cdata, pa);
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00067C85 File Offset: 0x00065E85
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.Delegate;
			}
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00067C8C File Offset: 0x00065E8C
		protected override bool DoDefineMembers()
		{
			BuiltinTypes builtinTypes = this.Compiler.BuiltinTypes;
			ParametersCompiled args = ParametersCompiled.CreateFullyResolved(new Parameter[]
			{
				new Parameter(new TypeExpression(builtinTypes.Object, base.Location), "object", Parameter.Modifier.NONE, null, base.Location),
				new Parameter(new TypeExpression(builtinTypes.IntPtr, base.Location), "method", Parameter.Modifier.NONE, null, base.Location)
			}, new BuiltinTypeSpec[]
			{
				builtinTypes.Object,
				builtinTypes.IntPtr
			});
			this.Constructor = new Constructor(this, Constructor.ConstructorName, Modifiers.PUBLIC, null, args, base.Location);
			this.Constructor.Define();
			ParametersCompiled parametersCompiled = this.parameters;
			if (!parametersCompiled.Resolve(this))
			{
				return false;
			}
			foreach (TypeSpec typeSpec in parametersCompiled.Types)
			{
				if (!base.IsAccessibleAs(typeSpec))
				{
					base.Report.SymbolRelatedToPreviousError(typeSpec);
					base.Report.Error(59, base.Location, "Inconsistent accessibility: parameter type `{0}' is less accessible than delegate `{1}'", typeSpec.GetSignatureForError(), this.GetSignatureForError());
				}
			}
			TypeSpec typeSpec2 = this.ReturnType.ResolveAsType(this, false);
			if (typeSpec2 == null)
			{
				return false;
			}
			if (!base.IsAccessibleAs(typeSpec2))
			{
				base.Report.SymbolRelatedToPreviousError(typeSpec2);
				base.Report.Error(58, base.Location, string.Concat(new string[]
				{
					"Inconsistent accessibility: return type `",
					typeSpec2.GetSignatureForError(),
					"' is less accessible than delegate `",
					this.GetSignatureForError(),
					"'"
				}));
				return false;
			}
			base.CheckProtectedModifier();
			if (this.Compiler.Settings.StdLib && typeSpec2.IsSpecialRuntimeType)
			{
				Method.Error1599(base.Location, typeSpec2, base.Report);
				return false;
			}
			VarianceDecl.CheckTypeVariance(typeSpec2, Variance.Covariant, this);
			TypeExpression typeExpression = new TypeExpression(typeSpec2, base.Location);
			this.InvokeBuilder = new Method(this, typeExpression, Modifiers.PUBLIC | Modifiers.VIRTUAL, new MemberName(Delegate.InvokeMethodName), parametersCompiled, null);
			this.InvokeBuilder.Define();
			if (!base.IsCompilerGenerated)
			{
				this.DefineAsyncMethods(typeExpression);
			}
			return true;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00067EA4 File Offset: 0x000660A4
		private void DefineAsyncMethods(TypeExpression returnType)
		{
			PredefinedType iasyncResult = this.Module.PredefinedTypes.IAsyncResult;
			PredefinedType asyncCallback = this.Module.PredefinedTypes.AsyncCallback;
			if (!iasyncResult.Define() || !asyncCallback.Define())
			{
				return;
			}
			ParametersCompiled userParams;
			if (this.Parameters.Count == 0)
			{
				userParams = ParametersCompiled.EmptyReadOnlyParameters;
			}
			else
			{
				Parameter[] array = new Parameter[this.Parameters.Count];
				for (int i = 0; i < array.Length; i++)
				{
					Parameter parameter = this.parameters[i];
					array[i] = new Parameter(new TypeExpression(this.parameters.Types[i], base.Location), parameter.Name, parameter.ModFlags & Parameter.Modifier.RefOutMask, (parameter.OptAttributes == null) ? null : parameter.OptAttributes.Clone(), base.Location);
				}
				userParams = new ParametersCompiled(array);
			}
			userParams = ParametersCompiled.MergeGenerated(this.Compiler, userParams, false, new Parameter[]
			{
				new Parameter(new TypeExpression(asyncCallback.TypeSpec, base.Location), "callback", Parameter.Modifier.NONE, null, base.Location),
				new Parameter(new TypeExpression(this.Compiler.BuiltinTypes.Object, base.Location), "object", Parameter.Modifier.NONE, null, base.Location)
			}, new TypeSpec[]
			{
				asyncCallback.TypeSpec,
				this.Compiler.BuiltinTypes.Object
			});
			this.BeginInvokeBuilder = new Method(this, new TypeExpression(iasyncResult.TypeSpec, base.Location), Modifiers.PUBLIC | Modifiers.VIRTUAL, new MemberName("BeginInvoke"), userParams, null);
			this.BeginInvokeBuilder.Define();
			int num = 0;
			IParameterData[] fixedParameters = this.Parameters.FixedParameters;
			for (int j = 0; j < fixedParameters.Length; j++)
			{
				if ((((Parameter)fixedParameters[j]).ModFlags & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE)
				{
					num++;
				}
			}
			ParametersCompiled userParams2;
			if (num > 0)
			{
				Parameter[] array2 = new Parameter[num];
				int num2 = 0;
				for (int k = 0; k < this.Parameters.FixedParameters.Length; k++)
				{
					Parameter parameter2 = this.parameters[k];
					if ((parameter2.ModFlags & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE)
					{
						array2[num2++] = new Parameter(new TypeExpression(parameter2.Type, base.Location), parameter2.Name, parameter2.ModFlags & Parameter.Modifier.RefOutMask, (parameter2.OptAttributes == null) ? null : parameter2.OptAttributes.Clone(), base.Location);
					}
				}
				userParams2 = new ParametersCompiled(array2);
			}
			else
			{
				userParams2 = ParametersCompiled.EmptyReadOnlyParameters;
			}
			userParams2 = ParametersCompiled.MergeGenerated(this.Compiler, userParams2, false, new Parameter(new TypeExpression(iasyncResult.TypeSpec, base.Location), "result", Parameter.Modifier.NONE, null, base.Location), iasyncResult.TypeSpec);
			this.EndInvokeBuilder = new Method(this, returnType, Modifiers.PUBLIC | Modifiers.VIRTUAL, new MemberName("EndInvoke"), userParams2, null);
			this.EndInvokeBuilder.Define();
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00068195 File Offset: 0x00066395
		public override void PrepareEmit()
		{
			if ((this.caching_flags & MemberCore.Flags.CloseTypeCreated) != (MemberCore.Flags)0)
			{
				return;
			}
			this.InvokeBuilder.PrepareEmit();
			if (this.BeginInvokeBuilder != null)
			{
				this.BeginInvokeBuilder.PrepareEmit();
				this.EndInvokeBuilder.PrepareEmit();
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000681CC File Offset: 0x000663CC
		public override void Emit()
		{
			base.Emit();
			if (this.declarative_security != null)
			{
				foreach (KeyValuePair<SecurityAction, PermissionSet> keyValuePair in this.declarative_security)
				{
					this.TypeBuilder.AddDeclarativeSecurity(keyValuePair.Key, keyValuePair.Value);
				}
			}
			if (this.ReturnType.Type != null)
			{
				if (this.ReturnType.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					this.return_attributes = new ReturnParameter(this, this.InvokeBuilder.MethodBuilder, base.Location);
					this.Module.PredefinedAttributes.Dynamic.EmitAttribute(this.return_attributes.Builder);
				}
				else if (this.ReturnType.Type.HasDynamicElement)
				{
					this.return_attributes = new ReturnParameter(this, this.InvokeBuilder.MethodBuilder, base.Location);
					this.Module.PredefinedAttributes.Dynamic.EmitAttribute(this.return_attributes.Builder, this.ReturnType.Type, base.Location);
				}
				ConstraintChecker.Check(this, this.ReturnType.Type, this.ReturnType.Location);
			}
			this.Constructor.ParameterInfo.ApplyAttributes(this, this.Constructor.ConstructorBuilder);
			this.Constructor.ConstructorBuilder.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
			this.parameters.CheckConstraints(this);
			this.parameters.ApplyAttributes(this, this.InvokeBuilder.MethodBuilder);
			this.InvokeBuilder.MethodBuilder.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
			if (this.BeginInvokeBuilder != null)
			{
				this.BeginInvokeBuilder.ParameterInfo.ApplyAttributes(this, this.BeginInvokeBuilder.MethodBuilder);
				this.EndInvokeBuilder.ParameterInfo.ApplyAttributes(this, this.EndInvokeBuilder.MethodBuilder);
				this.BeginInvokeBuilder.MethodBuilder.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
				this.EndInvokeBuilder.MethodBuilder.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
			}
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000683E4 File Offset: 0x000665E4
		protected override TypeSpec[] ResolveBaseTypes(out FullNamedExpression base_class)
		{
			this.base_type = this.Compiler.BuiltinTypes.MulticastDelegate;
			base_class = null;
			return null;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x00068400 File Offset: 0x00066600
		protected override TypeAttributes TypeAttr
		{
			get
			{
				return base.TypeAttr | TypeAttributes.NotPublic | TypeAttributes.Sealed;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00068410 File Offset: 0x00066610
		public override string[] ValidAttributeTargets
		{
			get
			{
				return Delegate.attribute_targets;
			}
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00068418 File Offset: 0x00066618
		protected override bool VerifyClsCompliance()
		{
			if (!base.VerifyClsCompliance())
			{
				return false;
			}
			this.parameters.VerifyClsCompliance(this);
			if (!this.InvokeBuilder.MemberType.IsCLSCompliant())
			{
				base.Report.Warning(3002, 1, base.Location, "Return type of `{0}' is not CLS-compliant", this.GetSignatureForError());
			}
			return true;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00068470 File Offset: 0x00066670
		public static MethodSpec GetConstructor(TypeSpec delType)
		{
			return (MethodSpec)MemberCache.FindMember(delType, MemberFilter.Constructor(null), BindingRestriction.DeclaredOnly);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00068484 File Offset: 0x00066684
		public static MethodSpec GetInvokeMethod(TypeSpec delType)
		{
			return (MethodSpec)MemberCache.FindMember(delType, MemberFilter.Method(Delegate.InvokeMethodName, 0, null, null), BindingRestriction.DeclaredOnly);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0006849F File Offset: 0x0006669F
		public static AParametersCollection GetParameters(TypeSpec delType)
		{
			return Delegate.GetInvokeMethod(delType).Parameters;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x000684AC File Offset: 0x000666AC
		public static bool IsTypeCovariant(ResolveContext rc, TypeSpec a, TypeSpec b)
		{
			if (a == b)
			{
				return true;
			}
			if (rc.Module.Compiler.Settings.Version == LanguageVersion.ISO_1)
			{
				return false;
			}
			if (a.IsGenericParameter && b.IsGenericParameter)
			{
				return a == b;
			}
			return Convert.ImplicitReferenceConversionExists(a, b);
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x000684EA File Offset: 0x000666EA
		public static string FullDelegateDesc(MethodSpec invoke_method)
		{
			return TypeManager.GetFullNameSignature(invoke_method).Replace(".Invoke", "");
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x00068501 File Offset: 0x00066701
		// (set) Token: 0x060015CC RID: 5580 RVA: 0x00068509 File Offset: 0x00066709
		public Expression InstanceExpression
		{
			get
			{
				return this.instance_expr;
			}
			set
			{
				this.instance_expr = value;
			}
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00068512 File Offset: 0x00066712
		// Note: this type is marked as 'beforefieldinit'.
		static Delegate()
		{
		}

		// Token: 0x04000912 RID: 2322
		private FullNamedExpression ReturnType;

		// Token: 0x04000913 RID: 2323
		private readonly ParametersCompiled parameters;

		// Token: 0x04000914 RID: 2324
		private Constructor Constructor;

		// Token: 0x04000915 RID: 2325
		private Method InvokeBuilder;

		// Token: 0x04000916 RID: 2326
		private Method BeginInvokeBuilder;

		// Token: 0x04000917 RID: 2327
		private Method EndInvokeBuilder;

		// Token: 0x04000918 RID: 2328
		private static readonly string[] attribute_targets = new string[]
		{
			"type",
			"return"
		};

		// Token: 0x04000919 RID: 2329
		public static readonly string InvokeMethodName = "Invoke";

		// Token: 0x0400091A RID: 2330
		private Expression instance_expr;

		// Token: 0x0400091B RID: 2331
		private ReturnParameter return_attributes;

		// Token: 0x0400091C RID: 2332
		private Dictionary<SecurityAction, PermissionSet> declarative_security;

		// Token: 0x0400091D RID: 2333
		private const Modifiers MethodModifiers = Modifiers.PUBLIC | Modifiers.VIRTUAL;

		// Token: 0x0400091E RID: 2334
		private const Modifiers AllowedModifiers = Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.UNSAFE;
	}
}
