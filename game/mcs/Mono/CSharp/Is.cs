using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x020001D1 RID: 465
	public class Is : Probe
	{
		// Token: 0x06001874 RID: 6260 RVA: 0x00075F1A File Offset: 0x0007411A
		public Is(Expression expr, Expression probe_type, Location l) : base(expr, probe_type, l)
		{
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x00075F25 File Offset: 0x00074125
		protected override string OperatorName
		{
			get
			{
				return "is";
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x00075F2C File Offset: 0x0007412C
		// (set) Token: 0x06001877 RID: 6263 RVA: 0x00075F34 File Offset: 0x00074134
		public LocalVariable Variable
		{
			[CompilerGenerated]
			get
			{
				return this.<Variable>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Variable>k__BackingField = value;
			}
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00075F40 File Offset: 0x00074140
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			if (this.Variable != null)
			{
				throw new NotSupportedException();
			}
			Arguments args = Arguments.CreateForExpressionTree(ec, null, new Expression[]
			{
				this.expr.CreateExpressionTree(ec),
				new TypeOf(this.probe_type_expr, this.loc)
			});
			return base.CreateExpressionFactoryCall(ec, "TypeIs", args);
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x00075F9C File Offset: 0x0007419C
		private Expression CreateConstantResult(ResolveContext rc, bool result)
		{
			if (result)
			{
				rc.Report.Warning(183, 1, this.loc, "The given expression is always of the provided (`{0}') type", this.probe_type_expr.GetSignatureForError());
			}
			else
			{
				rc.Report.Warning(184, 1, this.loc, "The given expression is never of the provided (`{0}') type", this.probe_type_expr.GetSignatureForError());
			}
			BoolConstant boolConstant = new BoolConstant(rc.BuiltinTypes, result, this.loc);
			if (!this.expr.IsSideEffectFree)
			{
				return new SideEffectConstant(boolConstant, this, this.loc);
			}
			return ReducedExpression.Create(boolConstant, this);
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00076034 File Offset: 0x00074234
		public override void Emit(EmitContext ec)
		{
			if (this.probe_type_expr != null)
			{
				this.EmitLoad(ec);
				if (this.expr_unwrap == null)
				{
					ec.EmitNull();
					ec.Emit(OpCodes.Cgt_Un);
				}
				return;
			}
			if (this.ProbeType is WildcardPattern)
			{
				this.expr.EmitSideEffect(ec);
				this.ProbeType.Emit(ec);
				return;
			}
			this.EmitPatternMatch(ec);
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00076097 File Offset: 0x00074297
		public override void EmitBranchable(EmitContext ec, Label target, bool on_true)
		{
			if (this.probe_type_expr == null)
			{
				this.EmitPatternMatch(ec);
			}
			else
			{
				this.EmitLoad(ec);
			}
			ec.Emit(on_true ? OpCodes.Brtrue : OpCodes.Brfalse, target);
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x000760C8 File Offset: 0x000742C8
		private void EmitPatternMatch(EmitContext ec)
		{
			Label label = ec.DefineLabel();
			Label label2 = ec.DefineLabel();
			if (this.expr_unwrap != null)
			{
				this.expr_unwrap.EmitCheck(ec);
				if (this.ProbeType.IsNull)
				{
					ec.EmitInt(0);
					ec.Emit(OpCodes.Ceq);
					return;
				}
				ec.Emit(OpCodes.Brfalse_S, label);
				this.expr_unwrap.Emit(ec);
				this.ProbeType.Emit(ec);
				ec.Emit(OpCodes.Ceq);
				ec.Emit(OpCodes.Br_S, label2);
				ec.MarkLabel(label);
				ec.EmitInt(0);
				ec.MarkLabel(label2);
				return;
			}
			else
			{
				if (this.number_args != null && this.number_args.Count == 3)
				{
					default(CallEmitter).Emit(ec, this.number_mg, this.number_args, this.loc);
					return;
				}
				TypeSpec type = this.ProbeType.Type;
				base.Expr.Emit(ec);
				ec.Emit(OpCodes.Isinst, type);
				ec.Emit(OpCodes.Dup);
				ec.Emit(OpCodes.Brfalse, label);
				bool flag = this.ProbeType is ComplexPatternExpression;
				Label recursivePatternLabel = ec.RecursivePatternLabel;
				if (flag)
				{
					ec.RecursivePatternLabel = ec.DefineLabel();
				}
				if (this.number_mg != null)
				{
					default(CallEmitter).Emit(ec, this.number_mg, this.number_args, this.loc);
				}
				else
				{
					if (TypeSpec.IsValueType(type))
					{
						ec.Emit(OpCodes.Unbox_Any, type);
					}
					this.ProbeType.Emit(ec);
					if (flag)
					{
						ec.EmitInt(1);
					}
					else
					{
						ec.Emit(OpCodes.Ceq);
					}
				}
				ec.Emit(OpCodes.Br_S, label2);
				ec.MarkLabel(label);
				ec.Emit(OpCodes.Pop);
				if (flag)
				{
					ec.MarkLabel(ec.RecursivePatternLabel);
				}
				ec.RecursivePatternLabel = recursivePatternLabel;
				ec.EmitInt(0);
				ec.MarkLabel(label2);
				return;
			}
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x000762A8 File Offset: 0x000744A8
		private void EmitLoad(EmitContext ec)
		{
			Label label = default(Label);
			if (this.expr_unwrap != null)
			{
				this.expr_unwrap.EmitCheck(ec);
				if (this.Variable == null)
				{
					return;
				}
				ec.Emit(OpCodes.Dup);
				label = ec.DefineLabel();
				ec.Emit(OpCodes.Brfalse_S, label);
				this.expr_unwrap.Emit(ec);
			}
			else
			{
				this.expr.Emit(ec);
				if (this.probe_type_expr.IsGenericParameter && TypeSpec.IsValueType(this.expr.Type))
				{
					ec.Emit(OpCodes.Box, this.expr.Type);
				}
				ec.Emit(OpCodes.Isinst, this.probe_type_expr);
			}
			if (this.Variable != null)
			{
				bool flag;
				if (this.probe_type_expr.IsGenericParameter || this.probe_type_expr.IsNullableType)
				{
					ec.Emit(OpCodes.Dup);
					ec.Emit(OpCodes.Unbox_Any, this.probe_type_expr);
					flag = true;
				}
				else
				{
					flag = false;
				}
				this.Variable.CreateBuilder(ec);
				this.Variable.EmitAssign(ec);
				if (this.expr_unwrap != null)
				{
					ec.MarkLabel(label);
					return;
				}
				if (!flag)
				{
					this.Variable.Emit(ec);
				}
			}
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x000763D4 File Offset: 0x000745D4
		protected override Expression DoResolve(ResolveContext rc)
		{
			if (base.ResolveCommon(rc) == null)
			{
				return null;
			}
			this.type = rc.BuiltinTypes.Bool;
			this.eclass = ExprClass.Value;
			if (this.probe_type_expr == null)
			{
				return this.ResolveMatchingExpression(rc);
			}
			Expression expression = this.ResolveResultExpression(rc);
			if (this.Variable != null)
			{
				if (expression is Constant)
				{
					throw new NotImplementedException("constant in type pattern matching");
				}
				this.Variable.Type = this.probe_type_expr;
				BlockContext blockContext = rc as BlockContext;
				if (blockContext != null)
				{
					this.Variable.PrepareAssignmentAnalysis(blockContext);
				}
			}
			return expression;
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0007645F File Offset: 0x0007465F
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			base.FlowAnalysis(fc);
			if (this.Variable != null)
			{
				fc.SetVariableAssigned(this.Variable.VariableInfo, true);
			}
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00076484 File Offset: 0x00074684
		protected override void ResolveProbeType(ResolveContext rc)
		{
			if (this.ProbeType is TypeExpr || rc.Module.Compiler.Settings.Version != LanguageVersion.Experimental)
			{
				base.ResolveProbeType(rc);
				return;
			}
			if (this.ProbeType is PatternExpression)
			{
				this.ProbeType.Resolve(rc);
				return;
			}
			SessionReportPrinter sessionReportPrinter = new SessionReportPrinter();
			ReportPrinter printer = rc.Report.SetPrinter(sessionReportPrinter);
			this.probe_type_expr = this.ProbeType.ResolveAsType(rc, false);
			sessionReportPrinter.EndSession();
			if (this.probe_type_expr != null)
			{
				sessionReportPrinter.Merge(rc.Report.Printer);
				rc.Report.SetPrinter(printer);
				return;
			}
			VarExpr varExpr = this.ProbeType as VarExpr;
			if (varExpr != null && varExpr.InferType(rc, this.expr))
			{
				this.probe_type_expr = varExpr.Type;
				rc.Report.SetPrinter(printer);
				return;
			}
			SessionReportPrinter sessionReportPrinter2 = new SessionReportPrinter();
			rc.Report.SetPrinter(sessionReportPrinter2);
			this.ProbeType = this.ProbeType.Resolve(rc);
			sessionReportPrinter2.EndSession();
			if (this.ProbeType != null)
			{
				sessionReportPrinter2.Merge(rc.Report.Printer);
			}
			else
			{
				sessionReportPrinter.Merge(rc.Report.Printer);
			}
			rc.Report.SetPrinter(printer);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x000765D4 File Offset: 0x000747D4
		private Expression ResolveMatchingExpression(ResolveContext rc)
		{
			Constant constant = this.ProbeType as Constant;
			if (constant != null)
			{
				if (!Convert.ImplicitConversionExists(rc, this.ProbeType, base.Expr.Type))
				{
					this.ProbeType.Error_ValueCannotBeConverted(rc, base.Expr.Type, false);
					return null;
				}
				if (constant.IsNull)
				{
					return new Binary(Binary.Operator.Equality, base.Expr, constant).Resolve(rc);
				}
				Constant constant2 = base.Expr as Constant;
				if (constant2 != null)
				{
					constant2 = ConstantFold.BinaryFold(rc, Binary.Operator.Equality, constant2, constant, this.loc);
					if (constant2 != null)
					{
						return constant2;
					}
				}
				if (base.Expr.Type.IsNullableType)
				{
					this.expr_unwrap = new Unwrap(base.Expr, true);
					this.expr_unwrap.Resolve(rc);
					this.ProbeType = Convert.ImplicitConversion(rc, this.ProbeType, this.expr_unwrap.Type, this.loc);
				}
				else
				{
					if (this.ProbeType.Type == base.Expr.Type)
					{
						return new Binary(Binary.Operator.Equality, base.Expr, constant, this.loc).Resolve(rc);
					}
					if (this.ProbeType.Type.IsEnum || (this.ProbeType.Type.BuiltinType >= BuiltinTypeSpec.Type.Byte && this.ProbeType.Type.BuiltinType <= BuiltinTypeSpec.Type.Decimal))
					{
						ModuleContainer.PatternMatchingHelper patternMatchingHelper = rc.Module.CreatePatterMatchingHelper();
						this.number_mg = patternMatchingHelper.NumberMatcher.Spec;
						this.number_args = new Arguments(3);
						if (!this.ProbeType.Type.IsEnum)
						{
							this.number_args.Add(new Argument(base.Expr));
						}
						this.number_args.Add(new Argument(Convert.ImplicitConversion(rc, this.ProbeType, rc.BuiltinTypes.Object, this.loc)));
						this.number_args.Add(new Argument(new BoolLiteral(rc.BuiltinTypes, this.ProbeType.Type.IsEnum, this.loc)));
					}
				}
				return this;
			}
			else
			{
				if (this.ProbeType is PatternExpression)
				{
					if (!(this.ProbeType is WildcardPattern) && !Convert.ImplicitConversionExists(rc, this.ProbeType, base.Expr.Type))
					{
						this.ProbeType.Error_ValueCannotBeConverted(rc, base.Expr.Type, false);
					}
					return this;
				}
				rc.Report.Error(150, this.ProbeType.Location, "A constant value is expected");
				return this;
			}
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0007685C File Offset: 0x00074A5C
		private Expression ResolveResultExpression(ResolveContext ec)
		{
			TypeSpec typeSpec = this.expr.Type;
			bool flag = false;
			if (this.expr.IsNull || this.expr.eclass == ExprClass.MethodGroup)
			{
				return this.CreateConstantResult(ec, false);
			}
			if (typeSpec.IsNullableType)
			{
				TypeSpec underlyingType = NullableInfo.GetUnderlyingType(typeSpec);
				if (!underlyingType.IsGenericParameter)
				{
					typeSpec = underlyingType;
					flag = true;
				}
			}
			TypeSpec typeSpec2 = this.probe_type_expr;
			bool flag2 = false;
			if (typeSpec2.IsNullableType)
			{
				TypeSpec underlyingType2 = NullableInfo.GetUnderlyingType(typeSpec2);
				if (!underlyingType2.IsGenericParameter)
				{
					typeSpec2 = underlyingType2;
					flag2 = true;
				}
			}
			if (typeSpec2.IsStruct)
			{
				if (typeSpec == typeSpec2)
				{
					if (flag && !flag2)
					{
						this.expr_unwrap = Unwrap.Create(this.expr, true);
						return this;
					}
					return this.CreateConstantResult(ec, true);
				}
				else
				{
					TypeParameterSpec typeParameterSpec = typeSpec as TypeParameterSpec;
					if (typeParameterSpec != null)
					{
						return this.ResolveGenericParameter(ec, typeSpec2, typeParameterSpec);
					}
					if (Convert.ExplicitReferenceConversionExists(typeSpec, typeSpec2))
					{
						return this;
					}
					if (typeSpec is InflatedTypeSpec && InflatedTypeSpec.ContainsTypeParameter(typeSpec))
					{
						return this;
					}
				}
			}
			else
			{
				TypeParameterSpec typeParameterSpec2 = typeSpec2 as TypeParameterSpec;
				if (typeParameterSpec2 != null)
				{
					return this.ResolveGenericParameter(ec, typeSpec, typeParameterSpec2);
				}
				if (typeSpec2.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					ec.Report.Warning(1981, 3, this.loc, "Using `{0}' to test compatibility with `{1}' is identical to testing compatibility with `object'", this.OperatorName, typeSpec2.GetSignatureForError());
				}
				if (TypeManager.IsGenericParameter(typeSpec))
				{
					return this.ResolveGenericParameter(ec, typeSpec2, (TypeParameterSpec)typeSpec);
				}
				if (TypeSpec.IsValueType(typeSpec))
				{
					if (Convert.ImplicitBoxingConversion(null, typeSpec, typeSpec2) != null)
					{
						if (flag && !flag2)
						{
							this.expr_unwrap = Unwrap.Create(this.expr, false);
							return this;
						}
						return this.CreateConstantResult(ec, true);
					}
				}
				else if (Convert.ImplicitReferenceConversionExists(typeSpec, typeSpec2))
				{
					Constant constant = this.expr as Constant;
					if (constant != null)
					{
						return this.CreateConstantResult(ec, !constant.IsNull);
					}
					if (typeSpec.MemberDefinition.IsImported && typeSpec.BuiltinType != BuiltinTypeSpec.Type.None && typeSpec.MemberDefinition.DeclaringAssembly != typeSpec2.MemberDefinition.DeclaringAssembly)
					{
						return this;
					}
					if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
					{
						return this;
					}
					return ReducedExpression.Create(new Binary(Binary.Operator.Inequality, this.expr, new NullLiteral(this.loc), Binary.State.UserOperatorsExcluded).Resolve(ec), this).Resolve(ec);
				}
				else
				{
					if (Convert.ExplicitReferenceConversionExists(typeSpec, typeSpec2))
					{
						return this;
					}
					if ((typeSpec is InflatedTypeSpec || typeSpec.IsArray) && InflatedTypeSpec.ContainsTypeParameter(typeSpec))
					{
						return this;
					}
				}
			}
			return this.CreateConstantResult(ec, false);
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00076AA8 File Offset: 0x00074CA8
		private Expression ResolveGenericParameter(ResolveContext ec, TypeSpec d, TypeParameterSpec t)
		{
			if (t.IsReferenceType && d.IsStruct)
			{
				return this.CreateConstantResult(ec, false);
			}
			if (this.expr.Type.IsGenericParameter)
			{
				if (this.expr.Type == d && TypeSpec.IsValueType(t) && TypeSpec.IsValueType(d))
				{
					return this.CreateConstantResult(ec, true);
				}
				this.expr = new BoxedCast(this.expr, d);
			}
			return this;
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00076B1A File Offset: 0x00074D1A
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009A3 RID: 2467
		private Unwrap expr_unwrap;

		// Token: 0x040009A4 RID: 2468
		private MethodSpec number_mg;

		// Token: 0x040009A5 RID: 2469
		private Arguments number_args;

		// Token: 0x040009A6 RID: 2470
		[CompilerGenerated]
		private LocalVariable <Variable>k__BackingField;
	}
}
