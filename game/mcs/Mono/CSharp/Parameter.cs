using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x0200026C RID: 620
	public class Parameter : ParameterBase, IParameterData, ILocalVariable
	{
		// Token: 0x06001E60 RID: 7776 RVA: 0x000958F9 File Offset: 0x00093AF9
		public Parameter(FullNamedExpression type, string name, Parameter.Modifier mod, Attributes attrs, Location loc)
		{
			this.name = name;
			this.modFlags = mod;
			this.loc = loc;
			this.texpr = type;
			this.attributes = attrs;
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x00095926 File Offset: 0x00093B26
		public Expression DefaultExpression
		{
			get
			{
				return this.default_expr;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x0009592E File Offset: 0x00093B2E
		// (set) Token: 0x06001E63 RID: 7779 RVA: 0x0009593B File Offset: 0x00093B3B
		public DefaultParameterValueExpression DefaultValue
		{
			get
			{
				return this.default_expr as DefaultParameterValueExpression;
			}
			set
			{
				this.default_expr = value;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x00095944 File Offset: 0x00093B44
		Expression IParameterData.DefaultValue
		{
			get
			{
				DefaultParameterValueExpression defaultParameterValueExpression = this.default_expr as DefaultParameterValueExpression;
				if (defaultParameterValueExpression != null)
				{
					return defaultParameterValueExpression.Child;
				}
				return this.default_expr;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x0009596D File Offset: 0x00093B6D
		private bool HasOptionalExpression
		{
			get
			{
				return this.default_expr is DefaultParameterValueExpression;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x0009597D File Offset: 0x00093B7D
		public Location Location
		{
			get
			{
				return this.loc;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x00095985 File Offset: 0x00093B85
		public Parameter.Modifier ParameterModifier
		{
			get
			{
				return this.modFlags;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x000958F1 File Offset: 0x00093AF1
		// (set) Token: 0x06001E69 RID: 7785 RVA: 0x00095840 File Offset: 0x00093A40
		public TypeSpec Type
		{
			get
			{
				return this.parameter_type;
			}
			set
			{
				this.parameter_type = value;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x0009598D File Offset: 0x00093B8D
		public FullNamedExpression TypeExpression
		{
			get
			{
				return this.texpr;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x00095995 File Offset: 0x00093B95
		public override string[] ValidAttributeTargets
		{
			get
			{
				return Parameter.attribute_targets;
			}
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0009599C File Offset: 0x00093B9C
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Type == pa.In && this.ModFlags == Parameter.Modifier.OUT)
			{
				a.Report.Error(36, a.Location, "An out parameter cannot have the `In' attribute");
				return;
			}
			if (a.Type == pa.ParamArray)
			{
				a.Report.Error(674, a.Location, "Do not use `System.ParamArrayAttribute'. Use the `params' keyword instead");
				return;
			}
			if (a.Type == pa.Out && (this.ModFlags & Parameter.Modifier.REF) != Parameter.Modifier.NONE && !base.OptAttributes.Contains(pa.In))
			{
				a.Report.Error(662, a.Location, "Cannot specify only `Out' attribute on a ref parameter. Use both `In' and `Out' attributes or neither");
				return;
			}
			if (a.Type == pa.CLSCompliant)
			{
				a.Report.Warning(3022, 1, a.Location, "CLSCompliant attribute has no meaning when applied to parameters. Try putting it on the method instead");
			}
			else if (a.Type == pa.DefaultParameterValue || a.Type == pa.OptionalParameter)
			{
				if (this.HasOptionalExpression)
				{
					a.Report.Error(1745, a.Location, "Cannot specify `{0}' attribute on optional parameter `{1}'", a.Type.GetSignatureForError().Replace("Attribute", ""), this.Name);
				}
				if (a.Type == pa.DefaultParameterValue)
				{
					return;
				}
			}
			else if (a.Type == pa.CallerMemberNameAttribute)
			{
				if ((this.modFlags & Parameter.Modifier.CallerMemberName) == Parameter.Modifier.NONE)
				{
					a.Report.Error(4022, a.Location, "The CallerMemberName attribute can only be applied to parameters with default value");
				}
			}
			else if (a.Type == pa.CallerLineNumberAttribute)
			{
				if ((this.modFlags & Parameter.Modifier.CallerLineNumber) == Parameter.Modifier.NONE)
				{
					a.Report.Error(4020, a.Location, "The CallerLineNumber attribute can only be applied to parameters with default value");
				}
			}
			else if (a.Type == pa.CallerFilePathAttribute && (this.modFlags & Parameter.Modifier.CallerFilePath) == Parameter.Modifier.NONE)
			{
				a.Report.Error(4021, a.Location, "The CallerFilePath attribute can only be applied to parameters with default value");
			}
			base.ApplyAttributeBuilder(a, ctor, cdata, pa);
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x00095BD5 File Offset: 0x00093DD5
		public virtual bool CheckAccessibility(InterfaceMemberBase member)
		{
			return this.parameter_type == null || member.IsAccessibleAs(this.parameter_type);
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x00095BF0 File Offset: 0x00093DF0
		private bool IsValidCallerContext(MemberCore memberContext)
		{
			Method method = memberContext as Method;
			return method == null || !method.IsPartialImplementation;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x00095C14 File Offset: 0x00093E14
		public virtual TypeSpec Resolve(IMemberContext rc, int index)
		{
			if (this.parameter_type != null)
			{
				return this.parameter_type;
			}
			if (this.attributes != null)
			{
				this.attributes.AttachTo(this, rc);
			}
			this.parameter_type = this.texpr.ResolveAsType(rc, false);
			if (this.parameter_type == null)
			{
				return null;
			}
			this.idx = index;
			if ((this.modFlags & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE && this.parameter_type.IsSpecialRuntimeType)
			{
				rc.Module.Compiler.Report.Error(1601, this.Location, "Method or delegate parameter cannot be of type `{0}'", this.GetSignatureForError());
				return null;
			}
			VarianceDecl.CheckTypeVariance(this.parameter_type, ((this.modFlags & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE) ? Variance.None : Variance.Contravariant, rc);
			if (this.parameter_type.IsStatic)
			{
				rc.Module.Compiler.Report.Error(721, this.Location, "`{0}': static types cannot be used as parameters", this.texpr.GetSignatureForError());
				return this.parameter_type;
			}
			if ((this.modFlags & Parameter.Modifier.This) != Parameter.Modifier.NONE && (this.parameter_type.IsPointer || this.parameter_type.BuiltinType == BuiltinTypeSpec.Type.Dynamic))
			{
				rc.Module.Compiler.Report.Error(1103, this.Location, "The extension method cannot be of type `{0}'", this.parameter_type.GetSignatureForError());
			}
			return this.parameter_type;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x00095D68 File Offset: 0x00093F68
		private void ResolveCallerAttributes(ResolveContext rc)
		{
			PredefinedAttributes predefinedAttributes = rc.Module.PredefinedAttributes;
			Attribute attribute = null;
			Attribute attribute2 = null;
			foreach (Attribute attribute3 in this.attributes.Attrs)
			{
				TypeSpec typeSpec = attribute3.ResolveTypeForComparison();
				if (typeSpec != null)
				{
					if (typeSpec == predefinedAttributes.CallerMemberNameAttribute)
					{
						TypeSpec typeSpec2 = rc.BuiltinTypes.String;
						if (typeSpec2 != this.parameter_type && !Convert.ImplicitReferenceConversionExists(typeSpec2, this.parameter_type))
						{
							rc.Report.Error(4019, attribute3.Location, "The CallerMemberName attribute cannot be applied because there is no standard conversion from `{0}' to `{1}'", typeSpec2.GetSignatureForError(), this.parameter_type.GetSignatureForError());
						}
						if (!this.IsValidCallerContext(rc.CurrentMemberDefinition))
						{
							rc.Report.Warning(4026, 1, attribute3.Location, "The CallerMemberName applied to parameter `{0}' will have no effect because it applies to a member that is used in context that do not allow optional arguments", this.name);
						}
						this.modFlags |= Parameter.Modifier.CallerMemberName;
						attribute = attribute3;
					}
					else if (typeSpec == predefinedAttributes.CallerLineNumberAttribute)
					{
						TypeSpec typeSpec2 = rc.BuiltinTypes.Int;
						if (typeSpec2 != this.parameter_type && !Convert.ImplicitStandardConversionExists(new IntConstant(typeSpec2, 2147483647, Location.Null), this.parameter_type))
						{
							rc.Report.Error(4017, attribute3.Location, "The CallerLineNumberAttribute attribute cannot be applied because there is no standard conversion from `{0}' to `{1}'", typeSpec2.GetSignatureForError(), this.parameter_type.GetSignatureForError());
						}
						if (!this.IsValidCallerContext(rc.CurrentMemberDefinition))
						{
							rc.Report.Warning(4024, 1, attribute3.Location, "The CallerLineNumberAttribute applied to parameter `{0}' will have no effect because it applies to a member that is used in context that do not allow optional arguments", this.name);
						}
						this.modFlags |= Parameter.Modifier.CallerLineNumber;
					}
					else if (typeSpec == predefinedAttributes.CallerFilePathAttribute)
					{
						TypeSpec typeSpec2 = rc.BuiltinTypes.String;
						if (typeSpec2 != this.parameter_type && !Convert.ImplicitReferenceConversionExists(typeSpec2, this.parameter_type))
						{
							rc.Report.Error(4018, attribute3.Location, "The CallerFilePath attribute cannot be applied because there is no standard conversion from `{0}' to `{1}'", typeSpec2.GetSignatureForError(), this.parameter_type.GetSignatureForError());
						}
						if (!this.IsValidCallerContext(rc.CurrentMemberDefinition))
						{
							rc.Report.Warning(4025, 1, attribute3.Location, "The CallerFilePath applied to parameter `{0}' will have no effect because it applies to a member that is used in context that do not allow optional arguments", this.name);
						}
						this.modFlags |= Parameter.Modifier.CallerFilePath;
						attribute2 = attribute3;
					}
				}
			}
			if ((this.modFlags & Parameter.Modifier.CallerLineNumber) != Parameter.Modifier.NONE)
			{
				if (attribute != null)
				{
					rc.Report.Warning(7081, 1, attribute.Location, "The CallerMemberNameAttribute applied to parameter `{0}' will have no effect. It is overridden by the CallerLineNumberAttribute", this.Name);
				}
				if (attribute2 != null)
				{
					rc.Report.Warning(7082, 1, attribute2.Location, "The CallerFilePathAttribute applied to parameter `{0}' will have no effect. It is overridden by the CallerLineNumberAttribute", this.name);
				}
			}
			if ((this.modFlags & Parameter.Modifier.CallerMemberName) != Parameter.Modifier.NONE && attribute2 != null)
			{
				rc.Report.Warning(7080, 1, attribute2.Location, "The CallerMemberNameAttribute applied to parameter `{0}' will have no effect. It is overridden by the CallerFilePathAttribute", this.name);
			}
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x00096078 File Offset: 0x00094278
		public void ResolveDefaultValue(ResolveContext rc)
		{
			if (this.default_expr != null)
			{
				((DefaultParameterValueExpression)this.default_expr).Resolve(rc, this);
				if (this.attributes != null)
				{
					this.ResolveCallerAttributes(rc);
				}
				return;
			}
			if (this.attributes == null)
			{
				return;
			}
			PredefinedAttributes predefinedAttributes = rc.Module.PredefinedAttributes;
			Attribute attribute = this.attributes.Search(predefinedAttributes.DefaultParameterValue);
			if (attribute == null)
			{
				if (this.attributes.Search(predefinedAttributes.OptionalParameter) != null)
				{
					this.default_expr = EmptyExpression.MissingValue;
				}
				return;
			}
			if (attribute.Resolve() == null)
			{
				return;
			}
			Expression parameterDefaultValue = attribute.GetParameterDefaultValue();
			if (parameterDefaultValue == null)
			{
				return;
			}
			ResolveContext resolveContext = attribute.CreateResolveContext();
			this.default_expr = parameterDefaultValue.Resolve(resolveContext);
			if (this.default_expr is BoxedCast)
			{
				this.default_expr = ((BoxedCast)this.default_expr).Child;
			}
			if (!(this.default_expr is Constant))
			{
				if (this.parameter_type.BuiltinType == BuiltinTypeSpec.Type.Object)
				{
					rc.Report.Error(1910, this.default_expr.Location, "Argument of type `{0}' is not applicable for the DefaultParameterValue attribute", this.default_expr.Type.GetSignatureForError());
				}
				else
				{
					rc.Report.Error(1909, this.default_expr.Location, "The DefaultParameterValue attribute is not applicable on parameters of type `{0}'", this.default_expr.Type.GetSignatureForError());
				}
				this.default_expr = null;
				return;
			}
			if (TypeSpecComparer.IsEqual(this.default_expr.Type, this.parameter_type) || (this.default_expr is NullConstant && TypeSpec.IsReferenceType(this.parameter_type) && !this.parameter_type.IsGenericParameter) || this.parameter_type.BuiltinType == BuiltinTypeSpec.Type.Object || this.parameter_type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				return;
			}
			Expression expression = Convert.ImplicitUserConversion(resolveContext, this.default_expr, this.parameter_type, this.loc);
			if (expression != null && TypeSpecComparer.IsEqual(expression.Type, this.parameter_type))
			{
				return;
			}
			rc.Report.Error(1908, this.default_expr.Location, "The type of the default value should match the type of the parameter");
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x0009627D File Offset: 0x0009447D
		public bool HasDefaultValue
		{
			get
			{
				return this.default_expr != null;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x00096288 File Offset: 0x00094488
		public bool HasExtensionMethodModifier
		{
			get
			{
				return (this.modFlags & Parameter.Modifier.This) > Parameter.Modifier.NONE;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x00096295 File Offset: 0x00094495
		// (set) Token: 0x06001E75 RID: 7797 RVA: 0x0009629D File Offset: 0x0009449D
		public HoistedParameter HoistedVariant
		{
			get
			{
				return this.hoisted_variant;
			}
			set
			{
				this.hoisted_variant = value;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001E76 RID: 7798 RVA: 0x000962A6 File Offset: 0x000944A6
		public Parameter.Modifier ModFlags
		{
			get
			{
				return this.modFlags & ~Parameter.Modifier.This;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x000962B4 File Offset: 0x000944B4
		// (set) Token: 0x06001E78 RID: 7800 RVA: 0x000962BC File Offset: 0x000944BC
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x000962C5 File Offset: 0x000944C5
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.Parameter;
			}
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x000962CC File Offset: 0x000944CC
		public void Error_DuplicateName(Report r)
		{
			r.Error(100, this.Location, "The parameter name `{0}' is a duplicate", this.Name);
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000962E8 File Offset: 0x000944E8
		public virtual string GetSignatureForError()
		{
			string signatureForError;
			if (this.parameter_type != null)
			{
				signatureForError = this.parameter_type.GetSignatureForError();
			}
			else
			{
				signatureForError = this.texpr.GetSignatureForError();
			}
			string modifierSignature = Parameter.GetModifierSignature(this.modFlags);
			if (modifierSignature.Length > 0)
			{
				return modifierSignature + " " + signatureForError;
			}
			return signatureForError;
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x0009633A File Offset: 0x0009453A
		public static string GetModifierSignature(Parameter.Modifier mod)
		{
			switch (mod)
			{
			case Parameter.Modifier.PARAMS:
				return "params";
			case Parameter.Modifier.REF:
				return "ref";
			case Parameter.Modifier.PARAMS | Parameter.Modifier.REF:
				break;
			case Parameter.Modifier.OUT:
				return "out";
			default:
				if (mod == Parameter.Modifier.This)
				{
					return "this";
				}
				break;
			}
			return "";
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x00096378 File Offset: 0x00094578
		public void IsClsCompliant(IMemberContext ctx)
		{
			if (this.parameter_type.IsCLSCompliant())
			{
				return;
			}
			ctx.Module.Compiler.Report.Warning(3001, 1, this.Location, "Argument type `{0}' is not CLS-compliant", this.parameter_type.GetSignatureForError());
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x000963C4 File Offset: 0x000945C4
		public virtual void ApplyAttributes(MethodBuilder mb, ConstructorBuilder cb, int index, PredefinedAttributes pa)
		{
			if (this.builder != null)
			{
				throw new InternalErrorException("builder already exists");
			}
			ParameterAttributes parameterAttributes = AParametersCollection.GetParameterAttribute(this.modFlags);
			if (this.HasOptionalExpression)
			{
				parameterAttributes |= ParameterAttributes.Optional;
			}
			if (mb == null)
			{
				this.builder = cb.DefineParameter(index, parameterAttributes, this.Name);
			}
			else
			{
				this.builder = mb.DefineParameter(index, parameterAttributes, this.Name);
			}
			if (base.OptAttributes != null)
			{
				base.OptAttributes.Emit();
			}
			if (this.HasDefaultValue && this.default_expr.Type != null)
			{
				DefaultParameterValueExpression defaultValue = this.DefaultValue;
				Constant constant = (defaultValue != null) ? (defaultValue.Child as Constant) : (this.default_expr as Constant);
				if (constant != null)
				{
					if (constant.Type.BuiltinType == BuiltinTypeSpec.Type.Decimal)
					{
						pa.DecimalConstant.EmitAttribute(this.builder, (decimal)constant.GetValue(), constant.Location);
					}
					else
					{
						this.builder.SetConstant(constant.GetValue());
					}
				}
				else if (this.default_expr.Type.IsStruct || this.default_expr.Type.IsGenericParameter)
				{
					this.builder.SetConstant(null);
				}
			}
			if (this.parameter_type != null)
			{
				if (this.parameter_type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					pa.Dynamic.EmitAttribute(this.builder);
					return;
				}
				if (this.parameter_type.HasDynamicElement)
				{
					pa.Dynamic.EmitAttribute(this.builder, this.parameter_type, this.Location);
				}
			}
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0009654C File Offset: 0x0009474C
		public Parameter Clone()
		{
			Parameter parameter = (Parameter)base.MemberwiseClone();
			if (this.attributes != null)
			{
				parameter.attributes = this.attributes.Clone();
			}
			return parameter;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00096580 File Offset: 0x00094780
		public ExpressionStatement CreateExpressionTreeVariable(BlockContext ec)
		{
			if ((this.modFlags & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE)
			{
				ec.Report.Error(1951, this.Location, "An expression tree parameter cannot use `ref' or `out' modifier");
			}
			this.expr_tree_variable = TemporaryVariableReference.Create(Parameter.ResolveParameterExpressionType(ec, this.Location).Type, ec.CurrentBlock.ParametersBlock, this.Location);
			this.expr_tree_variable = (TemporaryVariableReference)this.expr_tree_variable.Resolve(ec);
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(new TypeOf(this.parameter_type, this.Location)));
			arguments.Add(new Argument(new StringConstant(ec.BuiltinTypes, this.Name, this.Location)));
			return new SimpleAssign(this.ExpressionTreeVariableReference(), Expression.CreateExpressionFactoryCall(ec, "Parameter", null, arguments, this.Location));
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00096659 File Offset: 0x00094859
		public void Emit(EmitContext ec)
		{
			ec.EmitArgumentLoad(this.idx);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00096667 File Offset: 0x00094867
		public void EmitAssign(EmitContext ec)
		{
			ec.EmitArgumentStore(this.idx);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x00096675 File Offset: 0x00094875
		public void EmitAddressOf(EmitContext ec)
		{
			if ((this.ModFlags & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE)
			{
				ec.EmitArgumentLoad(this.idx);
				return;
			}
			ec.EmitArgumentAddress(this.idx);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x0009669A File Offset: 0x0009489A
		public TemporaryVariableReference ExpressionTreeVariableReference()
		{
			return this.expr_tree_variable;
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x000966A2 File Offset: 0x000948A2
		public static TypeExpr ResolveParameterExpressionType(IMemberContext ec, Location location)
		{
			return new TypeExpression(ec.Module.PredefinedTypes.ParameterExpression.Resolve(), location);
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x000966BF File Offset: 0x000948BF
		public void SetIndex(int index)
		{
			this.idx = index;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x000966C8 File Offset: 0x000948C8
		public void Warning_UselessOptionalParameter(Report Report)
		{
			Report.Warning(1066, 1, this.Location, "The default value specified for optional parameter `{0}' will never be used", this.Name);
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x000966E7 File Offset: 0x000948E7
		// Note: this type is marked as 'beforefieldinit'.
		static Parameter()
		{
		}

		// Token: 0x04000B40 RID: 2880
		private static readonly string[] attribute_targets = new string[]
		{
			"param"
		};

		// Token: 0x04000B41 RID: 2881
		private FullNamedExpression texpr;

		// Token: 0x04000B42 RID: 2882
		private Parameter.Modifier modFlags;

		// Token: 0x04000B43 RID: 2883
		private string name;

		// Token: 0x04000B44 RID: 2884
		private Expression default_expr;

		// Token: 0x04000B45 RID: 2885
		protected TypeSpec parameter_type;

		// Token: 0x04000B46 RID: 2886
		private readonly Location loc;

		// Token: 0x04000B47 RID: 2887
		protected int idx;

		// Token: 0x04000B48 RID: 2888
		public bool HasAddressTaken;

		// Token: 0x04000B49 RID: 2889
		private TemporaryVariableReference expr_tree_variable;

		// Token: 0x04000B4A RID: 2890
		private HoistedParameter hoisted_variant;

		// Token: 0x020003D6 RID: 982
		[Flags]
		public enum Modifier : byte
		{
			// Token: 0x040010F2 RID: 4338
			NONE = 0,
			// Token: 0x040010F3 RID: 4339
			PARAMS = 1,
			// Token: 0x040010F4 RID: 4340
			REF = 2,
			// Token: 0x040010F5 RID: 4341
			OUT = 4,
			// Token: 0x040010F6 RID: 4342
			This = 8,
			// Token: 0x040010F7 RID: 4343
			CallerMemberName = 16,
			// Token: 0x040010F8 RID: 4344
			CallerLineNumber = 32,
			// Token: 0x040010F9 RID: 4345
			CallerFilePath = 64,
			// Token: 0x040010FA RID: 4346
			RefOutMask = 6,
			// Token: 0x040010FB RID: 4347
			ModifierMask = 15,
			// Token: 0x040010FC RID: 4348
			CallerMask = 112
		}
	}
}
