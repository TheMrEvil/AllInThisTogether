using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x02000120 RID: 288
	public class Attribute
	{
		// Token: 0x06000E0F RID: 3599 RVA: 0x000346A5 File Offset: 0x000328A5
		public Attribute(string target, ATypeNameExpression expr, Arguments[] args, Location loc, bool nameEscaped)
		{
			this.expression = expr;
			if (args != null)
			{
				this.pos_args = args[0];
				this.named_args = args[1];
			}
			this.loc = loc;
			this.ExplicitTarget = target;
			this.nameEscaped = nameEscaped;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x000346E0 File Offset: 0x000328E0
		public Location Location
		{
			get
			{
				return this.loc;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x000346E8 File Offset: 0x000328E8
		public Arguments NamedArguments
		{
			get
			{
				return this.named_args;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x000346F0 File Offset: 0x000328F0
		public Arguments PositionalArguments
		{
			get
			{
				return this.pos_args;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x000346F8 File Offset: 0x000328F8
		public bool ResolveError
		{
			get
			{
				return this.resolve_error;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00034700 File Offset: 0x00032900
		public ATypeNameExpression TypeExpression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00034708 File Offset: 0x00032908
		private void AddModuleCharSet(ResolveContext rc)
		{
			if (this.HasField("CharSet"))
			{
				return;
			}
			if (!rc.Module.PredefinedTypes.CharSet.Define())
			{
				return;
			}
			if (this.NamedArguments == null)
			{
				this.named_args = new Arguments(1);
			}
			Constant expr = Constant.CreateConstantFromValue(rc.Module.PredefinedTypes.CharSet.TypeSpec, rc.Module.DefaultCharSet, this.Location);
			this.NamedArguments.Add(new NamedArgument("CharSet", this.loc, expr));
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0003479C File Offset: 0x0003299C
		public Attribute Clone()
		{
			return new Attribute(this.ExplicitTarget, this.expression, null, this.loc, this.nameEscaped)
			{
				pos_args = this.pos_args,
				named_args = this.NamedArguments
			};
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x000347D4 File Offset: 0x000329D4
		public void AttachTo(Attributable target, IMemberContext context)
		{
			if (this.targets == null)
			{
				this.targets = new Attributable[]
				{
					target
				};
				this.context = context;
				return;
			}
			if (context is NamespaceContainer)
			{
				this.targets[0] = target;
				this.context = context;
				return;
			}
			Attributable[] array = new Attributable[this.targets.Length + 1];
			this.targets.CopyTo(array, 0);
			array[this.targets.Length] = target;
			this.targets = array;
			target.OptAttributes = null;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00034850 File Offset: 0x00032A50
		public ResolveContext CreateResolveContext()
		{
			return new ResolveContext(this.context, ResolveContext.Options.ConstantScope);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00034862 File Offset: 0x00032A62
		private static void Error_InvalidNamedArgument(ResolveContext rc, NamedArgument name)
		{
			rc.Report.Error(617, name.Location, "`{0}' is not a valid named attribute argument. Named attribute arguments must be fields which are not readonly, static, const or read-write properties which are public and not static", name.Name);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00034885 File Offset: 0x00032A85
		private static void Error_InvalidNamedArgumentType(ResolveContext rc, NamedArgument name)
		{
			rc.Report.Error(655, name.Location, "`{0}' is not a valid named attribute argument because it is not a valid attribute parameter type", name.Name);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x000348A8 File Offset: 0x00032AA8
		public static void Error_AttributeArgumentIsDynamic(IMemberContext context, Location loc)
		{
			context.Module.Compiler.Report.Error(1982, loc, "An attribute argument cannot be dynamic expression");
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x000348CA File Offset: 0x00032ACA
		public void Error_MissingGuidAttribute()
		{
			this.Report.Error(596, this.Location, "The Guid attribute must be specified with the ComImport attribute");
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000348E7 File Offset: 0x00032AE7
		public void Error_MisusedExtensionAttribute()
		{
			this.Report.Error(1112, this.Location, "Do not use `{0}' directly. Use parameter modifier `this' instead", this.GetSignatureForError());
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003490A File Offset: 0x00032B0A
		public void Error_MisusedDynamicAttribute()
		{
			this.Report.Error(1970, this.loc, "Do not use `{0}' directly. Use `dynamic' keyword instead", this.GetSignatureForError());
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0003492D File Offset: 0x00032B2D
		private void Error_AttributeEmitError(string inner)
		{
			this.Report.Error(647, this.Location, "Error during emitting `{0}' attribute. The reason is `{1}'", this.Type.GetSignatureForError(), inner);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00034956 File Offset: 0x00032B56
		public void Error_InvalidArgumentValue(TypeSpec attributeType)
		{
			this.Report.Error(591, this.Location, "Invalid value for argument to `{0}' attribute", attributeType.GetSignatureForError());
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00034979 File Offset: 0x00032B79
		public void Error_InvalidSecurityParent()
		{
			this.Report.Error(7070, this.Location, "Security attribute `{0}' is not valid on this declaration type. Security attributes are only valid on assembly, type and method declarations", this.Type.GetSignatureForError());
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x000349A1 File Offset: 0x00032BA1
		private Attributable Owner
		{
			get
			{
				return this.targets[0];
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x000349AC File Offset: 0x00032BAC
		private void ResolveAttributeType(bool comparisonOnly)
		{
			SessionReportPrinter sessionReportPrinter = new SessionReportPrinter();
			SessionReportPrinter sessionReportPrinter2 = null;
			ReportPrinter reportPrinter = this.Report.SetPrinter(sessionReportPrinter);
			bool flag = false;
			bool flag2 = false;
			ATypeNameExpression atypeNameExpression = null;
			TypeSpec typeSpec;
			TypeSpec typeSpec2;
			try
			{
				typeSpec = this.expression.ResolveAsType(this.context, false);
				sessionReportPrinter.EndSession();
				if (typeSpec != null && sessionReportPrinter.ErrorsCount == 0)
				{
					flag = typeSpec.IsAttribute;
				}
				if (this.nameEscaped)
				{
					typeSpec2 = null;
				}
				else
				{
					atypeNameExpression = (ATypeNameExpression)this.expression.Clone(null);
					ATypeNameExpression atypeNameExpression2 = atypeNameExpression;
					atypeNameExpression2.Name += "Attribute";
					sessionReportPrinter2 = new SessionReportPrinter();
					this.Report.SetPrinter(sessionReportPrinter2);
					typeSpec2 = atypeNameExpression.ResolveAsType(this.context, false);
					sessionReportPrinter2.EndSession();
					if (typeSpec2 != null && sessionReportPrinter2.ErrorsCount == 0)
					{
						flag2 = typeSpec2.IsAttribute;
					}
					sessionReportPrinter2.EndSession();
				}
			}
			finally
			{
				this.context.Module.Compiler.Report.SetPrinter(reportPrinter);
			}
			if (flag && flag2 && typeSpec != typeSpec2)
			{
				if (!comparisonOnly)
				{
					this.Report.Error(1614, this.Location, "`{0}' is ambiguous between `{1}' and `{2}'. Use either `@{0}' or `{0}Attribute'", new string[]
					{
						this.GetSignatureForError(),
						this.expression.GetSignatureForError(),
						atypeNameExpression.GetSignatureForError()
					});
					this.resolve_error = true;
				}
				return;
			}
			if (flag)
			{
				this.Type = typeSpec;
				return;
			}
			if (flag2)
			{
				this.Type = typeSpec2;
				return;
			}
			if (comparisonOnly)
			{
				return;
			}
			this.resolve_error = true;
			if (typeSpec != null)
			{
				if (sessionReportPrinter.IsEmpty)
				{
					this.Report.SymbolRelatedToPreviousError(typeSpec);
					this.Report.Error(616, this.Location, "`{0}': is not an attribute class", typeSpec.GetSignatureForError());
					return;
				}
				sessionReportPrinter.Merge(reportPrinter);
				return;
			}
			else
			{
				if (typeSpec2 == null)
				{
					sessionReportPrinter.Merge(reportPrinter);
					return;
				}
				if (sessionReportPrinter2.IsEmpty)
				{
					this.Report.SymbolRelatedToPreviousError(typeSpec2);
					this.Report.Error(616, this.Location, "`{0}': is not an attribute class", typeSpec2.GetSignatureForError());
					return;
				}
				sessionReportPrinter2.Merge(reportPrinter);
				return;
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00034BC8 File Offset: 0x00032DC8
		public TypeSpec ResolveTypeForComparison()
		{
			if (this.Type == null && !this.resolve_error)
			{
				this.ResolveAttributeType(true);
			}
			return this.Type;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00034BE7 File Offset: 0x00032DE7
		public string GetSignatureForError()
		{
			if (this.Type != null)
			{
				return this.Type.GetSignatureForError();
			}
			return this.expression.GetSignatureForError();
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00034C08 File Offset: 0x00032E08
		public bool HasSecurityAttribute
		{
			get
			{
				PredefinedAttribute security = this.context.Module.PredefinedAttributes.Security;
				return security.IsDefined && TypeSpec.IsBaseClass(this.Type, security.TypeSpec, false);
			}
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00034C47 File Offset: 0x00032E47
		public bool IsValidSecurityAttribute()
		{
			return this.HasSecurityAttribute && this.IsSecurityActionValid();
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00034C5C File Offset: 0x00032E5C
		private static bool IsValidMethodImplOption(int value)
		{
			MethodImplOptions methodImplOptions = MethodImplOptions.AggressiveInlining;
			foreach (object obj in Enum.GetValues(typeof(MethodImplOptions)))
			{
				MethodImplOptions methodImplOptions2 = (MethodImplOptions)obj;
				methodImplOptions |= methodImplOptions2;
			}
			return (value | (int)methodImplOptions) == (int)methodImplOptions;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00034CC8 File Offset: 0x00032EC8
		public static bool IsValidArgumentType(TypeSpec t)
		{
			if (t.IsArray)
			{
				ArrayContainer arrayContainer = (ArrayContainer)t;
				if (arrayContainer.Rank > 1)
				{
					return false;
				}
				t = arrayContainer.Element;
			}
			switch (t.BuiltinType)
			{
			case BuiltinTypeSpec.Type.FirstPrimitive:
			case BuiltinTypeSpec.Type.Byte:
			case BuiltinTypeSpec.Type.SByte:
			case BuiltinTypeSpec.Type.Char:
			case BuiltinTypeSpec.Type.Short:
			case BuiltinTypeSpec.Type.UShort:
			case BuiltinTypeSpec.Type.Int:
			case BuiltinTypeSpec.Type.UInt:
			case BuiltinTypeSpec.Type.Long:
			case BuiltinTypeSpec.Type.ULong:
			case BuiltinTypeSpec.Type.Float:
			case BuiltinTypeSpec.Type.Double:
			case BuiltinTypeSpec.Type.Object:
			case BuiltinTypeSpec.Type.Dynamic:
			case BuiltinTypeSpec.Type.String:
			case BuiltinTypeSpec.Type.Type:
				return true;
			}
			return t.IsEnum;
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00034D5C File Offset: 0x00032F5C
		public string Name
		{
			get
			{
				return this.expression.Name;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00034D69 File Offset: 0x00032F69
		public Report Report
		{
			get
			{
				return this.context.Module.Compiler.Report;
			}
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00034D80 File Offset: 0x00032F80
		public MethodSpec Resolve()
		{
			if (this.resolve_error)
			{
				return null;
			}
			this.resolve_error = true;
			this.arg_resolved = true;
			if (this.Type == null)
			{
				this.ResolveAttributeType(false);
				if (this.Type == null)
				{
					return null;
				}
			}
			if (this.Type.IsAbstract)
			{
				this.Report.Error(653, this.Location, "Cannot apply attribute class `{0}' because it is abstract", this.GetSignatureForError());
				return null;
			}
			ObsoleteAttribute attributeObsolete = this.Type.GetAttributeObsolete();
			if (attributeObsolete != null)
			{
				AttributeTester.Report_ObsoleteMessage(attributeObsolete, this.Type.GetSignatureForError(), this.Location, this.Report);
			}
			ResolveContext resolveContext = null;
			MethodSpec methodSpec;
			if (this.pos_args != null || !this.context.Module.AttributeConstructorCache.TryGetValue(this.Type, out methodSpec))
			{
				resolveContext = this.CreateResolveContext();
				methodSpec = this.ResolveConstructor(resolveContext);
				if (methodSpec == null)
				{
					return null;
				}
				if (this.pos_args == null && methodSpec.Parameters.IsEmpty)
				{
					this.context.Module.AttributeConstructorCache.Add(this.Type, methodSpec);
				}
			}
			ModuleContainer module = this.context.Module;
			if ((this.Type == module.PredefinedAttributes.DllImport || this.Type == module.PredefinedAttributes.UnmanagedFunctionPointer) && module.HasDefaultCharSet)
			{
				if (resolveContext == null)
				{
					resolveContext = this.CreateResolveContext();
				}
				this.AddModuleCharSet(resolveContext);
			}
			if (this.NamedArguments != null)
			{
				if (resolveContext == null)
				{
					resolveContext = this.CreateResolveContext();
				}
				if (!this.ResolveNamedArguments(resolveContext))
				{
					return null;
				}
			}
			this.resolve_error = false;
			return methodSpec;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00034F04 File Offset: 0x00033104
		private MethodSpec ResolveConstructor(ResolveContext ec)
		{
			if (this.pos_args != null)
			{
				bool flag;
				this.pos_args.Resolve(ec, out flag);
				if (flag)
				{
					Attribute.Error_AttributeArgumentIsDynamic(ec.MemberContext, this.loc);
					return null;
				}
			}
			return Expression.ConstructorLookup(ec, this.Type, ref this.pos_args, this.loc);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00034F58 File Offset: 0x00033158
		private bool ResolveNamedArguments(ResolveContext ec)
		{
			int count = this.NamedArguments.Count;
			List<string> list = new List<string>(count);
			this.named_values = new List<KeyValuePair<MemberExpr, NamedArgument>>(count);
			foreach (Argument argument in this.NamedArguments)
			{
				NamedArgument namedArgument = (NamedArgument)argument;
				string name = namedArgument.Name;
				if (list.Contains(name))
				{
					ec.Report.Error(643, namedArgument.Location, "Duplicate named attribute `{0}' argument", name);
				}
				else
				{
					list.Add(name);
					namedArgument.Resolve(ec);
					Expression expression = Expression.MemberLookup(ec, false, this.Type, name, 0, Expression.MemberLookupRestrictions.ExactArity, this.loc);
					if (expression == null)
					{
						expression = Expression.MemberLookup(ec, true, this.Type, name, 0, Expression.MemberLookupRestrictions.ExactArity, this.loc);
						if (expression != null)
						{
							Expression.ErrorIsInaccesible(ec, expression.GetSignatureForError(), this.loc);
							return false;
						}
					}
					if (expression == null)
					{
						Expression.Error_TypeDoesNotContainDefinition(ec, this.Location, this.Type, name);
						return false;
					}
					if (!(expression is PropertyExpr) && !(expression is FieldExpr))
					{
						Attribute.Error_InvalidNamedArgument(ec, namedArgument);
						return false;
					}
					ObsoleteAttribute attributeObsolete;
					if (expression is PropertyExpr)
					{
						PropertySpec propertyInfo = ((PropertyExpr)expression).PropertyInfo;
						if (!propertyInfo.HasSet || !propertyInfo.HasGet || propertyInfo.IsStatic || !propertyInfo.Get.IsPublic || !propertyInfo.Set.IsPublic)
						{
							ec.Report.SymbolRelatedToPreviousError(propertyInfo);
							Attribute.Error_InvalidNamedArgument(ec, namedArgument);
							return false;
						}
						if (!Attribute.IsValidArgumentType(expression.Type))
						{
							ec.Report.SymbolRelatedToPreviousError(propertyInfo);
							Attribute.Error_InvalidNamedArgumentType(ec, namedArgument);
							return false;
						}
						attributeObsolete = propertyInfo.GetAttributeObsolete();
						propertyInfo.MemberDefinition.SetIsAssigned();
					}
					else
					{
						FieldSpec spec = ((FieldExpr)expression).Spec;
						if (spec.IsReadOnly || spec.IsStatic || !spec.IsPublic)
						{
							Attribute.Error_InvalidNamedArgument(ec, namedArgument);
							return false;
						}
						if (!Attribute.IsValidArgumentType(expression.Type))
						{
							ec.Report.SymbolRelatedToPreviousError(spec);
							Attribute.Error_InvalidNamedArgumentType(ec, namedArgument);
							return false;
						}
						attributeObsolete = spec.GetAttributeObsolete();
						spec.MemberDefinition.SetIsAssigned();
					}
					if (attributeObsolete != null && !this.context.IsObsolete)
					{
						AttributeTester.Report_ObsoleteMessage(attributeObsolete, expression.GetSignatureForError(), expression.Location, this.Report);
					}
					if (namedArgument.Type != expression.Type)
					{
						namedArgument.Expr = Convert.ImplicitConversionRequired(ec, namedArgument.Expr, expression.Type, namedArgument.Expr.Location);
					}
					if (namedArgument.Expr != null)
					{
						this.named_values.Add(new KeyValuePair<MemberExpr, NamedArgument>((MemberExpr)expression, namedArgument));
					}
				}
			}
			return true;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003525C File Offset: 0x0003345C
		public string GetValidTargets()
		{
			StringBuilder stringBuilder = new StringBuilder();
			AttributeTargets validOn = this.Type.GetAttributeUsage(this.context.Module.PredefinedAttributes.AttributeUsage).ValidOn;
			if ((validOn & AttributeTargets.Assembly) != (AttributeTargets)0)
			{
				stringBuilder.Append("assembly, ");
			}
			if ((validOn & AttributeTargets.Module) != (AttributeTargets)0)
			{
				stringBuilder.Append("module, ");
			}
			if ((validOn & AttributeTargets.Class) != (AttributeTargets)0)
			{
				stringBuilder.Append("class, ");
			}
			if ((validOn & AttributeTargets.Struct) != (AttributeTargets)0)
			{
				stringBuilder.Append("struct, ");
			}
			if ((validOn & AttributeTargets.Enum) != (AttributeTargets)0)
			{
				stringBuilder.Append("enum, ");
			}
			if ((validOn & AttributeTargets.Constructor) != (AttributeTargets)0)
			{
				stringBuilder.Append("constructor, ");
			}
			if ((validOn & AttributeTargets.Method) != (AttributeTargets)0)
			{
				stringBuilder.Append("method, ");
			}
			if ((validOn & AttributeTargets.Property) != (AttributeTargets)0)
			{
				stringBuilder.Append("property, indexer, ");
			}
			if ((validOn & AttributeTargets.Field) != (AttributeTargets)0)
			{
				stringBuilder.Append("field, ");
			}
			if ((validOn & AttributeTargets.Event) != (AttributeTargets)0)
			{
				stringBuilder.Append("event, ");
			}
			if ((validOn & AttributeTargets.Interface) != (AttributeTargets)0)
			{
				stringBuilder.Append("interface, ");
			}
			if ((validOn & AttributeTargets.Parameter) != (AttributeTargets)0)
			{
				stringBuilder.Append("parameter, ");
			}
			if ((validOn & AttributeTargets.Delegate) != (AttributeTargets)0)
			{
				stringBuilder.Append("delegate, ");
			}
			if ((validOn & AttributeTargets.ReturnValue) != (AttributeTargets)0)
			{
				stringBuilder.Append("return, ");
			}
			if ((validOn & AttributeTargets.GenericParameter) != (AttributeTargets)0)
			{
				stringBuilder.Append("type parameter, ");
			}
			StringBuilder stringBuilder2 = stringBuilder;
			return stringBuilder2.Remove(stringBuilder2.Length - 2, 2).ToString();
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x000353CC File Offset: 0x000335CC
		public AttributeUsageAttribute GetAttributeUsageAttribute()
		{
			if (!this.arg_resolved)
			{
				this.Resolve();
			}
			if (this.resolve_error)
			{
				return Attribute.DefaultUsageAttribute;
			}
			AttributeUsageAttribute attributeUsageAttribute = new AttributeUsageAttribute((AttributeTargets)((Constant)this.pos_args[0].Expr).GetValue());
			BoolConstant boolConstant = this.GetNamedValue("AllowMultiple") as BoolConstant;
			if (boolConstant != null)
			{
				attributeUsageAttribute.AllowMultiple = boolConstant.Value;
			}
			boolConstant = (this.GetNamedValue("Inherited") as BoolConstant);
			if (boolConstant != null)
			{
				attributeUsageAttribute.Inherited = boolConstant.Value;
			}
			return attributeUsageAttribute;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00035460 File Offset: 0x00033660
		public string GetIndexerAttributeValue()
		{
			if (!this.arg_resolved)
			{
				this.Resolve();
			}
			if (this.resolve_error || this.pos_args.Count != 1 || !(this.pos_args[0].Expr is Constant))
			{
				return null;
			}
			return ((Constant)this.pos_args[0].Expr).GetValue() as string;
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x000354CC File Offset: 0x000336CC
		public string GetConditionalAttributeValue()
		{
			if (!this.arg_resolved)
			{
				this.Resolve();
			}
			if (this.resolve_error)
			{
				return null;
			}
			return ((Constant)this.pos_args[0].Expr).GetValue() as string;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00035508 File Offset: 0x00033708
		public ObsoleteAttribute GetObsoleteAttribute()
		{
			if (!this.arg_resolved)
			{
				Class @class = this.Type.MemberDefinition as Class;
				if (@class != null && !@class.HasMembersDefined)
				{
					@class.Define();
				}
				this.Resolve();
			}
			if (this.resolve_error)
			{
				return null;
			}
			if (this.pos_args == null)
			{
				return new ObsoleteAttribute();
			}
			string message = ((Constant)this.pos_args[0].Expr).GetValue() as string;
			if (this.pos_args.Count == 1)
			{
				return new ObsoleteAttribute(message);
			}
			return new ObsoleteAttribute(message, ((BoolConstant)this.pos_args[1].Expr).Value);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000355B6 File Offset: 0x000337B6
		public bool GetClsCompliantAttributeValue()
		{
			if (!this.arg_resolved)
			{
				this.Resolve();
			}
			return !this.resolve_error && ((BoolConstant)this.pos_args[0].Expr).Value;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x000355EC File Offset: 0x000337EC
		public TypeSpec GetCoClassAttributeValue()
		{
			if (!this.arg_resolved)
			{
				this.Resolve();
			}
			if (this.resolve_error)
			{
				return null;
			}
			return this.GetArgumentType();
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00035610 File Offset: 0x00033810
		public bool CheckTarget()
		{
			string[] validAttributeTargets = this.Owner.ValidAttributeTargets;
			if (this.ExplicitTarget == null || this.ExplicitTarget == validAttributeTargets[0])
			{
				this.Target = this.Owner.AttributeTargets;
				return true;
			}
			if (!Array.Exists<string>(validAttributeTargets, (string i) => i == this.ExplicitTarget))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in validAttributeTargets)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(", ");
				}
				StringBuilder stringBuilder2 = stringBuilder;
				stringBuilder2.Remove(stringBuilder2.Length - 2, 2);
				this.Report.Warning(657, 1, this.Location, "`{0}' is not a valid attribute location for this declaration. Valid attribute locations for this declaration are `{1}'. All attributes in this section will be ignored", this.ExplicitTarget, stringBuilder.ToString());
				return false;
			}
			string explicitTarget = this.ExplicitTarget;
			if (explicitTarget == "return")
			{
				this.Target = AttributeTargets.ReturnValue;
				return true;
			}
			if (explicitTarget == "param")
			{
				this.Target = AttributeTargets.Parameter;
				return true;
			}
			if (explicitTarget == "field")
			{
				this.Target = AttributeTargets.Field;
				return true;
			}
			if (explicitTarget == "method")
			{
				this.Target = AttributeTargets.Method;
				return true;
			}
			if (explicitTarget == "property")
			{
				this.Target = AttributeTargets.Property;
				return true;
			}
			if (!(explicitTarget == "module"))
			{
				throw new InternalErrorException("Unknown explicit target: " + this.ExplicitTarget);
			}
			this.Target = AttributeTargets.Module;
			return true;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00035790 File Offset: 0x00033990
		private bool IsSecurityActionValid()
		{
			Constant constant = null;
			SecurityAction? securityActionValue = this.GetSecurityActionValue(ref constant);
			bool flag = this.Target == AttributeTargets.Assembly || this.Target == AttributeTargets.Module;
			if (securityActionValue != null)
			{
				switch (securityActionValue.GetValueOrDefault())
				{
				case SecurityAction.Demand:
				case SecurityAction.Assert:
				case SecurityAction.Deny:
				case SecurityAction.PermitOnly:
				case SecurityAction.LinkDemand:
				case SecurityAction.InheritanceDemand:
					if (!flag)
					{
						return true;
					}
					break;
				case SecurityAction.RequestMinimum:
				case SecurityAction.RequestOptional:
				case SecurityAction.RequestRefuse:
					if (flag)
					{
						return true;
					}
					break;
				default:
					this.Report.Error(7049, constant.Location, "Security attribute `{0}' has an invalid SecurityAction value `{1}'", this.Type.GetSignatureForError(), constant.GetValueAsLiteral());
					return false;
				}
				AttributeTargets target = this.Target;
				if (target == AttributeTargets.Assembly)
				{
					this.Report.Error(7050, constant.Location, "SecurityAction value `{0}' is invalid for security attributes applied to an assembly", constant.GetSignatureForError());
				}
				else
				{
					this.Report.Error(7051, constant.Location, "SecurityAction value `{0}' is invalid for security attributes applied to a type or a method", constant.GetSignatureForError());
				}
				return false;
			}
			this.Report.Error(7048, this.loc, "First argument of a security attribute `{0}' must be a valid SecurityAction", this.Type.GetSignatureForError());
			return false;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000358B0 File Offset: 0x00033AB0
		private SecurityAction? GetSecurityActionValue(ref Constant value)
		{
			if (this.pos_args != null)
			{
				value = (Constant)this.pos_args[0].Expr;
				return new SecurityAction?((SecurityAction)value.GetValue());
			}
			PredefinedAttributes predefinedAttributes = this.context.Module.PredefinedAttributes;
			if (this.Type == predefinedAttributes.HostProtection.TypeSpec)
			{
				value = new IntConstant(this.context.Module.Compiler.BuiltinTypes, 6, this.loc);
				return new SecurityAction?(SecurityAction.LinkDemand);
			}
			return null;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0000225C File Offset: 0x0000045C
		public void ExtractSecurityPermissionSet(MethodSpec ctor, ref Dictionary<SecurityAction, PermissionSet> permissions)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00035948 File Offset: 0x00033B48
		public Constant GetNamedValue(string name)
		{
			if (this.named_values == null)
			{
				return null;
			}
			for (int i = 0; i < this.named_values.Count; i++)
			{
				if (this.named_values[i].Value.Name == name)
				{
					return this.named_values[i].Value.Expr as Constant;
				}
			}
			return null;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x000359B6 File Offset: 0x00033BB6
		public CharSet GetCharSetValue()
		{
			return (CharSet)Enum.Parse(typeof(CharSet), ((Constant)this.pos_args[0].Expr).GetValue().ToString());
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x000359EC File Offset: 0x00033BEC
		public bool HasField(string fieldName)
		{
			if (this.named_values == null)
			{
				return false;
			}
			foreach (KeyValuePair<MemberExpr, NamedArgument> keyValuePair in this.named_values)
			{
				if (keyValuePair.Value.Name == fieldName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00035A60 File Offset: 0x00033C60
		public bool IsInternalCall()
		{
			return (this.GetMethodImplOptions() & MethodImplOptions.InternalCall) > (MethodImplOptions)0;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00035A74 File Offset: 0x00033C74
		public MethodImplOptions GetMethodImplOptions()
		{
			MethodImplOptions result = (MethodImplOptions)0;
			if (this.pos_args.Count == 1)
			{
				result = (MethodImplOptions)Enum.Parse(typeof(MethodImplOptions), ((Constant)this.pos_args[0].Expr).GetValue().ToString());
			}
			else if (this.HasField("Value"))
			{
				Constant namedValue = this.GetNamedValue("Value");
				result = (MethodImplOptions)Enum.Parse(typeof(MethodImplOptions), namedValue.GetValue().ToString());
			}
			return result;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00035B04 File Offset: 0x00033D04
		public bool IsExplicitLayoutKind()
		{
			return this.pos_args != null && this.pos_args.Count == 1 && (LayoutKind)Enum.Parse(typeof(LayoutKind), ((Constant)this.pos_args[0].Expr).GetValue().ToString()) == LayoutKind.Explicit;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00035B60 File Offset: 0x00033D60
		public Expression GetParameterDefaultValue()
		{
			if (this.pos_args == null)
			{
				return null;
			}
			return this.pos_args[0].Expr;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00035B80 File Offset: 0x00033D80
		public override bool Equals(object obj)
		{
			Attribute attribute = obj as Attribute;
			return attribute != null && this.Type == attribute.Type && this.Target == attribute.Target;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00035BB7 File Offset: 0x00033DB7
		public override int GetHashCode()
		{
			return this.Type.GetHashCode() ^ this.Target.GetHashCode();
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00035BD8 File Offset: 0x00033DD8
		public void Emit(Dictionary<Attribute, List<Attribute>> allEmitted)
		{
			MethodSpec methodSpec = this.Resolve();
			if (methodSpec == null)
			{
				return;
			}
			PredefinedAttributes predefinedAttributes = this.context.Module.PredefinedAttributes;
			AttributeUsageAttribute attributeUsage = this.Type.GetAttributeUsage(predefinedAttributes.AttributeUsage);
			if ((attributeUsage.ValidOn & this.Target) == (AttributeTargets)0)
			{
				this.Report.Error(592, this.Location, "The attribute `{0}' is not valid on this declaration type. It is valid on `{1}' declarations only", this.GetSignatureForError(), this.GetValidTargets());
				return;
			}
			byte[] cdata;
			if (this.pos_args == null && this.named_values == null)
			{
				cdata = AttributeEncoder.Empty;
			}
			else
			{
				AttributeEncoder attributeEncoder = new AttributeEncoder();
				if (this.pos_args != null)
				{
					TypeSpec[] types = methodSpec.Parameters.Types;
					for (int i = 0; i < this.pos_args.Count; i++)
					{
						TypeSpec typeSpec = types[i];
						Expression expr = this.pos_args[i].Expr;
						if (i == 0)
						{
							if ((this.Type == predefinedAttributes.IndexerName || this.Type == predefinedAttributes.Conditional) && expr is Constant)
							{
								string s = ((Constant)expr).GetValue() as string;
								if (!Tokenizer.IsValidIdentifier(s) || (this.Type == predefinedAttributes.IndexerName && Tokenizer.IsKeyword(s)))
								{
									this.context.Module.Compiler.Report.Error(633, expr.Location, "The argument to the `{0}' attribute must be a valid identifier", this.GetSignatureForError());
									return;
								}
							}
							else
							{
								if (this.Type == predefinedAttributes.Guid)
								{
									string value = ((StringConstant)expr).Value;
									try
									{
										new Guid(value);
										goto IL_33B;
									}
									catch
									{
										this.Error_InvalidArgumentValue(this.Type);
										return;
									}
								}
								if (this.Type == predefinedAttributes.AttributeUsage)
								{
									if (((IntConstant)((EnumConstant)expr).Child).Value == 0)
									{
										this.Error_InvalidArgumentValue(this.Type);
									}
								}
								else if (this.Type == predefinedAttributes.MarshalAs)
								{
									if (this.pos_args.Count == 1 && (UnmanagedType)Enum.Parse(typeof(UnmanagedType), ((Constant)this.pos_args[0].Expr).GetValue().ToString()) == UnmanagedType.ByValArray && !(this.Owner is FieldBase))
									{
										this.Report.Error(7055, this.pos_args[0].Expr.Location, "Unmanaged type `ByValArray' is only valid for fields");
									}
								}
								else if (this.Type == predefinedAttributes.DllImport)
								{
									if (this.pos_args.Count == 1 && this.pos_args[0].Expr is Constant && string.IsNullOrEmpty(((Constant)this.pos_args[0].Expr).GetValue() as string))
									{
										this.Error_InvalidArgumentValue(this.Type);
									}
								}
								else if (this.Type == predefinedAttributes.MethodImpl && this.pos_args.Count == 1 && !Attribute.IsValidMethodImplOption((int)((Constant)expr).GetValueAsLong()))
								{
									this.Error_InvalidArgumentValue(this.Type);
								}
							}
						}
						IL_33B:
						Expression expression = expr;
						IMemberContext rc = this.context;
						AttributeEncoder enc = attributeEncoder;
						TypeSpec typeSpec2 = typeSpec;
						expression.EncodeAttributeValue(rc, enc, typeSpec2, typeSpec2);
					}
				}
				if (this.named_values != null)
				{
					attributeEncoder.Encode((ushort)this.named_values.Count);
					using (List<KeyValuePair<MemberExpr, NamedArgument>>.Enumerator enumerator = this.named_values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<MemberExpr, NamedArgument> keyValuePair = enumerator.Current;
							if (keyValuePair.Key is FieldExpr)
							{
								attributeEncoder.Encode(83);
							}
							else
							{
								attributeEncoder.Encode(84);
							}
							attributeEncoder.Encode(keyValuePair.Key.Type);
							attributeEncoder.Encode(keyValuePair.Value.Name);
							keyValuePair.Value.Expr.EncodeAttributeValue(this.context, attributeEncoder, keyValuePair.Key.Type, keyValuePair.Key.Type);
						}
						goto IL_43B;
					}
				}
				attributeEncoder.EncodeEmptyNamedArguments();
				IL_43B:
				cdata = attributeEncoder.ToArray();
			}
			if (!methodSpec.DeclaringType.IsConditionallyExcluded(this.context))
			{
				try
				{
					Attributable[] array = this.targets;
					for (int j = 0; j < array.Length; j++)
					{
						array[j].ApplyAttributeBuilder(this, methodSpec, cdata, predefinedAttributes);
					}
				}
				catch (Exception ex)
				{
					if (ex is BadImageFormatException && this.Report.Errors > 0)
					{
						return;
					}
					this.Error_AttributeEmitError(ex.Message);
					return;
				}
			}
			if (!attributeUsage.AllowMultiple && allEmitted != null)
			{
				if (allEmitted.ContainsKey(this))
				{
					List<Attribute> list = allEmitted[this];
					if (list == null)
					{
						list = new List<Attribute>(2);
						allEmitted[this] = list;
					}
					list.Add(this);
				}
				else
				{
					allEmitted.Add(this, null);
				}
			}
			if (!this.context.Module.Compiler.Settings.VerifyClsCompliance)
			{
				return;
			}
			if (this.Owner.IsClsComplianceRequired())
			{
				if (this.pos_args != null)
				{
					this.pos_args.CheckArrayAsAttribute(this.context.Module.Compiler);
				}
				if (this.NamedArguments == null)
				{
					return;
				}
				this.NamedArguments.CheckArrayAsAttribute(this.context.Module.Compiler);
			}
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00036174 File Offset: 0x00034374
		private Expression GetValue()
		{
			if (this.pos_args == null || this.pos_args.Count < 1)
			{
				return null;
			}
			return this.pos_args[0].Expr;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000361A0 File Offset: 0x000343A0
		public string GetString()
		{
			Expression value = this.GetValue();
			if (value is StringConstant)
			{
				return ((StringConstant)value).Value;
			}
			return null;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x000361CC File Offset: 0x000343CC
		public bool GetBoolean()
		{
			Expression value = this.GetValue();
			return value is BoolConstant && ((BoolConstant)value).Value;
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x000361F8 File Offset: 0x000343F8
		public TypeSpec GetArgumentType()
		{
			TypeOf typeOf = this.GetValue() as TypeOf;
			if (typeOf == null)
			{
				return null;
			}
			return typeOf.TypeArgument;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003621C File Offset: 0x0003441C
		// Note: this type is marked as 'beforefieldinit'.
		static Attribute()
		{
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00036238 File Offset: 0x00034438
		[CompilerGenerated]
		private bool <CheckTarget>b__63_0(string i)
		{
			return i == this.ExplicitTarget;
		}

		// Token: 0x04000682 RID: 1666
		public readonly string ExplicitTarget;

		// Token: 0x04000683 RID: 1667
		public AttributeTargets Target;

		// Token: 0x04000684 RID: 1668
		private readonly ATypeNameExpression expression;

		// Token: 0x04000685 RID: 1669
		private Arguments pos_args;

		// Token: 0x04000686 RID: 1670
		private Arguments named_args;

		// Token: 0x04000687 RID: 1671
		private bool resolve_error;

		// Token: 0x04000688 RID: 1672
		private bool arg_resolved;

		// Token: 0x04000689 RID: 1673
		private readonly bool nameEscaped;

		// Token: 0x0400068A RID: 1674
		private readonly Location loc;

		// Token: 0x0400068B RID: 1675
		public TypeSpec Type;

		// Token: 0x0400068C RID: 1676
		private Attributable[] targets;

		// Token: 0x0400068D RID: 1677
		private IMemberContext context;

		// Token: 0x0400068E RID: 1678
		public static readonly AttributeUsageAttribute DefaultUsageAttribute = new AttributeUsageAttribute(AttributeTargets.All);

		// Token: 0x0400068F RID: 1679
		public static readonly object[] EmptyObject = new object[0];

		// Token: 0x04000690 RID: 1680
		private List<KeyValuePair<MemberExpr, NamedArgument>> named_values;
	}
}
