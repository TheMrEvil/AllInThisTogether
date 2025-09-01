using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x020002C6 RID: 710
	public class Foreach : LoopStatement
	{
		// Token: 0x06002236 RID: 8758 RVA: 0x000A7BC1 File Offset: 0x000A5DC1
		public Foreach(Expression type, LocalVariable var, Expression expr, Statement stmt, Block body, Location l) : base(stmt)
		{
			this.type = type;
			this.variable = var;
			this.expr = expr;
			this.body = body;
			this.loc = l;
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x000A7BF0 File Offset: 0x000A5DF0
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x000A7BF8 File Offset: 0x000A5DF8
		public Expression TypeExpression
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x000A7C00 File Offset: 0x000A5E00
		public LocalVariable Variable
		{
			get
			{
				return this.variable;
			}
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000A7C08 File Offset: 0x000A5E08
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			this.body.MarkReachable(rc);
			return rc;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000A7C20 File Offset: 0x000A5E20
		public override bool Resolve(BlockContext ec)
		{
			this.expr = this.expr.Resolve(ec);
			if (this.expr == null)
			{
				return false;
			}
			if (this.expr.IsNull)
			{
				ec.Report.Error(186, this.loc, "Use of null is not valid in this context");
				return false;
			}
			this.body.AddStatement(base.Statement);
			if (this.expr.Type.BuiltinType == BuiltinTypeSpec.Type.String)
			{
				base.Statement = new Foreach.ArrayForeach(this, 1);
			}
			else if (this.expr.Type is ArrayContainer)
			{
				base.Statement = new Foreach.ArrayForeach(this, ((ArrayContainer)this.expr.Type).Rank);
			}
			else
			{
				if (this.expr.eclass == ExprClass.MethodGroup || this.expr is AnonymousMethodExpression)
				{
					ec.Report.Error(446, this.expr.Location, "Foreach statement cannot operate on a `{0}'", this.expr.ExprClassName);
					return false;
				}
				base.Statement = new Foreach.CollectionForeach(this, this.variable, this.expr);
			}
			return base.Resolve(ec);
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000A7D48 File Offset: 0x000A5F48
		protected override void DoEmit(EmitContext ec)
		{
			Label loopBegin = ec.LoopBegin;
			Label loopEnd = ec.LoopEnd;
			ec.LoopBegin = ec.DefineLabel();
			ec.LoopEnd = ec.DefineLabel();
			if (!(base.Statement is Block))
			{
				ec.BeginCompilerScope();
			}
			this.variable.CreateBuilder(ec);
			base.Statement.Emit(ec);
			if (!(base.Statement is Block))
			{
				ec.EndScope();
			}
			ec.LoopBegin = loopBegin;
			ec.LoopEnd = loopEnd;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000A7DC8 File Offset: 0x000A5FC8
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			this.expr.FlowAnalysis(fc);
			DefiniteAssignmentBitSet definiteAssignment = fc.BranchDefiniteAssignment();
			this.body.FlowAnalysis(fc);
			fc.DefiniteAssignment = definiteAssignment;
			return false;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000A7E00 File Offset: 0x000A6000
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			Foreach @foreach = (Foreach)t;
			@foreach.type = this.type.Clone(clonectx);
			@foreach.expr = this.expr.Clone(clonectx);
			@foreach.body = (Block)this.body.Clone(clonectx);
			@foreach.Statement = base.Statement.Clone(clonectx);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000A7E5F File Offset: 0x000A605F
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C9C RID: 3228
		private Expression type;

		// Token: 0x04000C9D RID: 3229
		private LocalVariable variable;

		// Token: 0x04000C9E RID: 3230
		private Expression expr;

		// Token: 0x04000C9F RID: 3231
		private Block body;

		// Token: 0x020003FE RID: 1022
		private abstract class IteratorStatement : Statement
		{
			// Token: 0x06002820 RID: 10272 RVA: 0x000BE0E2 File Offset: 0x000BC2E2
			protected IteratorStatement(Foreach @foreach)
			{
				this.for_each = @foreach;
				this.loc = @foreach.expr.Location;
			}

			// Token: 0x06002821 RID: 10273 RVA: 0x00023DF4 File Offset: 0x00021FF4
			protected override void CloneTo(CloneContext clonectx, Statement target)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06002822 RID: 10274 RVA: 0x000BE102 File Offset: 0x000BC302
			public override void Emit(EmitContext ec)
			{
				if (ec.EmitAccurateDebugInfo)
				{
					ec.Emit(OpCodes.Nop);
				}
				base.Emit(ec);
			}

			// Token: 0x06002823 RID: 10275 RVA: 0x00023DF4 File Offset: 0x00021FF4
			protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400115A RID: 4442
			protected readonly Foreach for_each;
		}

		// Token: 0x020003FF RID: 1023
		private sealed class ArrayForeach : Foreach.IteratorStatement
		{
			// Token: 0x06002824 RID: 10276 RVA: 0x000BE11E File Offset: 0x000BC31E
			public ArrayForeach(Foreach @foreach, int rank) : base(@foreach)
			{
				this.counter = new StatementExpression[rank];
				this.variables = new TemporaryVariableReference[rank];
				this.length_exprs = new Expression[rank];
				if (rank > 1)
				{
					this.lengths = new TemporaryVariableReference[rank];
				}
			}

			// Token: 0x06002825 RID: 10277 RVA: 0x000BE15C File Offset: 0x000BC35C
			public override bool Resolve(BlockContext ec)
			{
				Block block = this.for_each.variable.Block;
				this.copy = TemporaryVariableReference.Create(this.for_each.expr.Type, block, this.loc);
				this.copy.Resolve(ec);
				int num = this.length_exprs.Length;
				Arguments arguments = new Arguments(num);
				for (int i = 0; i < num; i++)
				{
					TemporaryVariableReference temporaryVariableReference = TemporaryVariableReference.Create(ec.BuiltinTypes.Int, block, this.loc);
					this.variables[i] = temporaryVariableReference;
					this.counter[i] = new StatementExpression(new UnaryMutator(UnaryMutator.Mode.IsPost, temporaryVariableReference, Location.Null));
					this.counter[i].Resolve(ec);
					if (num == 1)
					{
						this.length_exprs[i] = new MemberAccess(this.copy, "Length").Resolve(ec);
					}
					else
					{
						this.lengths[i] = TemporaryVariableReference.Create(ec.BuiltinTypes.Int, block, this.loc);
						this.lengths[i].Resolve(ec);
						Arguments arguments2 = new Arguments(1);
						arguments2.Add(new Argument(new IntConstant(ec.BuiltinTypes, i, this.loc)));
						this.length_exprs[i] = new Invocation(new MemberAccess(this.copy, "GetLength"), arguments2).Resolve(ec);
					}
					arguments.Add(new Argument(temporaryVariableReference));
				}
				Expression expression = new ElementAccess(this.copy, arguments, this.loc).Resolve(ec);
				if (expression == null)
				{
					return false;
				}
				TypeSpec typeSpec;
				if (this.for_each.type is VarExpr)
				{
					typeSpec = expression.Type;
				}
				else
				{
					typeSpec = this.for_each.type.ResolveAsType(ec, false);
					if (typeSpec == null)
					{
						return false;
					}
					expression = Convert.ExplicitConversion(ec, expression, typeSpec, this.loc);
					if (expression == null)
					{
						return false;
					}
				}
				this.for_each.variable.Type = typeSpec;
				Expression expression2 = new LocalVariableReference(this.for_each.variable, this.loc).Resolve(ec);
				if (expression2 == null)
				{
					return false;
				}
				this.for_each.body.AddScopeStatement(new StatementExpression(new CompilerAssign(expression2, expression, Location.Null), this.for_each.type.Location));
				return this.for_each.body.Resolve(ec);
			}

			// Token: 0x06002826 RID: 10278 RVA: 0x000BE3B0 File Offset: 0x000BC5B0
			protected override void DoEmit(EmitContext ec)
			{
				this.copy.EmitAssign(ec, this.for_each.expr);
				int num = this.length_exprs.Length;
				Label[] array = new Label[num];
				Label[] array2 = new Label[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = ec.DefineLabel();
					array2[i] = ec.DefineLabel();
					if (this.lengths != null)
					{
						this.lengths[i].EmitAssign(ec, this.length_exprs[i]);
					}
				}
				IntConstant source = new IntConstant(ec.BuiltinTypes, 0, this.loc);
				for (int j = 0; j < num; j++)
				{
					this.variables[j].EmitAssign(ec, source);
					ec.Emit(OpCodes.Br, array[j]);
					ec.MarkLabel(array2[j]);
				}
				this.for_each.body.Emit(ec);
				ec.MarkLabel(ec.LoopBegin);
				ec.Mark(this.for_each.expr.Location);
				for (int k = num - 1; k >= 0; k--)
				{
					this.counter[k].Emit(ec);
					ec.MarkLabel(array[k]);
					this.variables[k].Emit(ec);
					if (this.lengths != null)
					{
						this.lengths[k].Emit(ec);
					}
					else
					{
						this.length_exprs[k].Emit(ec);
					}
					ec.Emit(OpCodes.Blt, array2[k]);
				}
				ec.MarkLabel(ec.LoopEnd);
			}

			// Token: 0x0400115B RID: 4443
			private TemporaryVariableReference[] lengths;

			// Token: 0x0400115C RID: 4444
			private Expression[] length_exprs;

			// Token: 0x0400115D RID: 4445
			private StatementExpression[] counter;

			// Token: 0x0400115E RID: 4446
			private TemporaryVariableReference[] variables;

			// Token: 0x0400115F RID: 4447
			private TemporaryVariableReference copy;
		}

		// Token: 0x02000400 RID: 1024
		private sealed class CollectionForeach : Foreach.IteratorStatement, OverloadResolver.IErrorHandler
		{
			// Token: 0x06002827 RID: 10279 RVA: 0x000BE544 File Offset: 0x000BC744
			public CollectionForeach(Foreach @foreach, LocalVariable var, Expression expr) : base(@foreach)
			{
				this.variable = var;
				this.expr = expr;
			}

			// Token: 0x06002828 RID: 10280 RVA: 0x000BE55B File Offset: 0x000BC75B
			private void Error_WrongEnumerator(ResolveContext rc, MethodSpec enumerator)
			{
				rc.Report.SymbolRelatedToPreviousError(enumerator);
				rc.Report.Error(202, this.loc, "foreach statement requires that the return type `{0}' of `{1}' must have a suitable public MoveNext method and public Current property", enumerator.ReturnType.GetSignatureForError(), enumerator.GetSignatureForError());
			}

			// Token: 0x06002829 RID: 10281 RVA: 0x000BE598 File Offset: 0x000BC798
			private MethodGroupExpr ResolveGetEnumerator(ResolveContext rc)
			{
				MethodGroupExpr methodGroupExpr = Expression.MemberLookup(rc, false, this.expr.Type, "GetEnumerator", 0, Expression.MemberLookupRestrictions.ExactArity, this.loc) as MethodGroupExpr;
				if (methodGroupExpr != null)
				{
					methodGroupExpr.InstanceExpression = this.expr;
					Arguments arguments = new Arguments(0);
					methodGroupExpr = methodGroupExpr.OverloadResolve(rc, ref arguments, this, OverloadResolver.Restrictions.ProbingOnly | OverloadResolver.Restrictions.GetEnumeratorLookup);
					if (this.ambiguous_getenumerator_name)
					{
						methodGroupExpr = null;
					}
					if (methodGroupExpr != null && !methodGroupExpr.BestCandidate.IsStatic && methodGroupExpr.BestCandidate.IsPublic)
					{
						return methodGroupExpr;
					}
				}
				TypeSpec type = this.expr.Type;
				PredefinedMember<MethodSpec> predefinedMember = null;
				PredefinedType predefinedType = rc.Module.PredefinedTypes.IEnumerableGeneric;
				if (!predefinedType.Define())
				{
					predefinedType = null;
				}
				IList<TypeSpec> interfaces = type.Interfaces;
				if (interfaces != null)
				{
					foreach (TypeSpec typeSpec in interfaces)
					{
						if (predefinedType != null && typeSpec.MemberDefinition == predefinedType.TypeSpec.MemberDefinition)
						{
							if (predefinedMember != null && predefinedMember != rc.Module.PredefinedMembers.IEnumerableGetEnumerator)
							{
								rc.Report.SymbolRelatedToPreviousError(this.expr.Type);
								rc.Report.Error(1640, this.loc, "foreach statement cannot operate on variables of type `{0}' because it contains multiple implementation of `{1}'. Try casting to a specific implementation", this.expr.Type.GetSignatureForError(), predefinedType.TypeSpec.GetSignatureForError());
								return null;
							}
							predefinedMember = new PredefinedMember<MethodSpec>(rc.Module, typeSpec, MemberFilter.Method("GetEnumerator", 0, ParametersCompiled.EmptyReadOnlyParameters, null));
						}
						else if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.IEnumerable && predefinedMember == null)
						{
							predefinedMember = rc.Module.PredefinedMembers.IEnumerableGetEnumerator;
						}
					}
				}
				if (predefinedMember == null)
				{
					if (this.expr.Type != InternalType.ErrorType)
					{
						rc.Report.Error(1579, this.loc, "foreach statement cannot operate on variables of type `{0}' because it does not contain a definition for `{1}' or is inaccessible", this.expr.Type.GetSignatureForError(), "GetEnumerator");
					}
					return null;
				}
				MethodSpec methodSpec = predefinedMember.Resolve(this.loc);
				if (methodSpec == null)
				{
					return null;
				}
				methodGroupExpr = MethodGroupExpr.CreatePredefined(methodSpec, this.expr.Type, this.loc);
				methodGroupExpr.InstanceExpression = this.expr;
				return methodGroupExpr;
			}

			// Token: 0x0600282A RID: 10282 RVA: 0x000BE7D8 File Offset: 0x000BC9D8
			private MethodGroupExpr ResolveMoveNext(ResolveContext rc, MethodSpec enumerator)
			{
				MethodSpec methodSpec = MemberCache.FindMember(enumerator.ReturnType, MemberFilter.Method("MoveNext", 0, ParametersCompiled.EmptyReadOnlyParameters, rc.BuiltinTypes.Bool), BindingRestriction.InstanceOnly) as MethodSpec;
				if (methodSpec == null || !methodSpec.IsPublic)
				{
					this.Error_WrongEnumerator(rc, enumerator);
					return null;
				}
				return MethodGroupExpr.CreatePredefined(methodSpec, enumerator.ReturnType, this.expr.Location);
			}

			// Token: 0x0600282B RID: 10283 RVA: 0x000BE840 File Offset: 0x000BCA40
			private PropertySpec ResolveCurrent(ResolveContext rc, MethodSpec enumerator)
			{
				PropertySpec propertySpec = MemberCache.FindMember(enumerator.ReturnType, MemberFilter.Property("Current", null), BindingRestriction.InstanceOnly) as PropertySpec;
				if (propertySpec == null || !propertySpec.IsPublic)
				{
					this.Error_WrongEnumerator(rc, enumerator);
					return null;
				}
				return propertySpec;
			}

			// Token: 0x0600282C RID: 10284 RVA: 0x000BE880 File Offset: 0x000BCA80
			public override bool Resolve(BlockContext ec)
			{
				bool flag = this.expr.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic;
				if (flag)
				{
					this.expr = Convert.ImplicitConversionRequired(ec, this.expr, ec.BuiltinTypes.IEnumerable, this.loc);
				}
				else if (this.expr.Type.IsNullableType)
				{
					this.expr = new UnwrapCall(this.expr).Resolve(ec);
				}
				MethodGroupExpr methodGroupExpr = this.ResolveGetEnumerator(ec);
				if (methodGroupExpr == null)
				{
					return false;
				}
				MethodSpec bestCandidate = methodGroupExpr.BestCandidate;
				this.enumerator_variable = TemporaryVariableReference.Create(bestCandidate.ReturnType, this.variable.Block, this.loc);
				this.enumerator_variable.Resolve(ec);
				MethodGroupExpr methodGroupExpr2 = this.ResolveMoveNext(ec, bestCandidate);
				if (methodGroupExpr2 == null)
				{
					return false;
				}
				methodGroupExpr2.InstanceExpression = this.enumerator_variable;
				PropertySpec propertySpec = this.ResolveCurrent(ec, bestCandidate);
				if (propertySpec == null)
				{
					return false;
				}
				Expression expression = new PropertyExpr(propertySpec, this.loc)
				{
					InstanceExpression = this.enumerator_variable
				}.Resolve(ec);
				if (expression == null)
				{
					return false;
				}
				if (this.for_each.type is VarExpr)
				{
					if (flag)
					{
						this.variable.Type = ec.BuiltinTypes.Dynamic;
					}
					else
					{
						this.variable.Type = expression.Type;
					}
				}
				else
				{
					if (flag)
					{
						expression = EmptyCast.Create(expression, ec.BuiltinTypes.Dynamic);
					}
					this.variable.Type = this.for_each.type.ResolveAsType(ec, false);
					if (this.variable.Type == null)
					{
						return false;
					}
					expression = Convert.ExplicitConversion(ec, expression, this.variable.Type, this.loc);
					if (expression == null)
					{
						return false;
					}
				}
				Expression expression2 = new LocalVariableReference(this.variable, this.loc).Resolve(ec);
				if (expression2 == null)
				{
					return false;
				}
				this.for_each.body.AddScopeStatement(new StatementExpression(new CompilerAssign(expression2, expression, Location.Null), this.for_each.type.Location));
				Invocation.Predefined predefined = new Invocation.Predefined(methodGroupExpr, null);
				this.statement = new While(new BooleanExpression(new Invocation(methodGroupExpr2, null)), this.for_each.body, Location.Null);
				TypeSpec type = this.enumerator_variable.Type;
				if (!type.ImplementsInterface(ec.BuiltinTypes.IDisposable, false))
				{
					if (!type.IsSealed && !TypeSpec.IsValueType(type))
					{
						this.statement = new Using(new Foreach.CollectionForeach.RuntimeDispose(this.enumerator_variable.LocalInfo, Location.Null)
						{
							Initializer = predefined
						}, this.statement, Location.Null);
					}
					else
					{
						this.init = new SimpleAssign(this.enumerator_variable, predefined, Location.Null);
						this.init.Resolve(ec);
					}
				}
				else
				{
					this.statement = new Using(new Using.VariableDeclaration(this.enumerator_variable.LocalInfo, Location.Null)
					{
						Initializer = predefined
					}, this.statement, Location.Null);
				}
				return this.statement.Resolve(ec);
			}

			// Token: 0x0600282D RID: 10285 RVA: 0x000BEB87 File Offset: 0x000BCD87
			protected override void DoEmit(EmitContext ec)
			{
				this.enumerator_variable.LocalInfo.CreateBuilder(ec);
				if (this.init != null)
				{
					this.init.EmitStatement(ec);
				}
				this.statement.Emit(ec);
			}

			// Token: 0x0600282E RID: 10286 RVA: 0x000BEBBC File Offset: 0x000BCDBC
			bool OverloadResolver.IErrorHandler.AmbiguousCandidates(ResolveContext ec, MemberSpec best, MemberSpec ambiguous)
			{
				ec.Report.SymbolRelatedToPreviousError(best);
				ec.Report.Warning(278, 2, this.expr.Location, "`{0}' contains ambiguous implementation of `{1}' pattern. Method `{2}' is ambiguous with method `{3}'", new object[]
				{
					this.expr.Type.GetSignatureForError(),
					"enumerable",
					best.GetSignatureForError(),
					ambiguous.GetSignatureForError()
				});
				this.ambiguous_getenumerator_name = true;
				return true;
			}

			// Token: 0x0600282F RID: 10287 RVA: 0x000022F4 File Offset: 0x000004F4
			bool OverloadResolver.IErrorHandler.ArgumentMismatch(ResolveContext rc, MemberSpec best, Argument arg, int index)
			{
				return false;
			}

			// Token: 0x06002830 RID: 10288 RVA: 0x000022F4 File Offset: 0x000004F4
			bool OverloadResolver.IErrorHandler.NoArgumentMatch(ResolveContext rc, MemberSpec best)
			{
				return false;
			}

			// Token: 0x06002831 RID: 10289 RVA: 0x000022F4 File Offset: 0x000004F4
			bool OverloadResolver.IErrorHandler.TypeInferenceFailed(ResolveContext rc, MemberSpec best)
			{
				return false;
			}

			// Token: 0x04001160 RID: 4448
			private LocalVariable variable;

			// Token: 0x04001161 RID: 4449
			private Expression expr;

			// Token: 0x04001162 RID: 4450
			private Statement statement;

			// Token: 0x04001163 RID: 4451
			private ExpressionStatement init;

			// Token: 0x04001164 RID: 4452
			private TemporaryVariableReference enumerator_variable;

			// Token: 0x04001165 RID: 4453
			private bool ambiguous_getenumerator_name;

			// Token: 0x02000423 RID: 1059
			private class RuntimeDispose : Using.VariableDeclaration
			{
				// Token: 0x06002867 RID: 10343 RVA: 0x000BF7D7 File Offset: 0x000BD9D7
				public RuntimeDispose(LocalVariable lv, Location loc) : base(lv, loc)
				{
					this.reachable = true;
				}

				// Token: 0x06002868 RID: 10344 RVA: 0x0000AF70 File Offset: 0x00009170
				protected override void CheckIDiposableConversion(BlockContext bc, LocalVariable li, Expression initializer)
				{
				}

				// Token: 0x06002869 RID: 10345 RVA: 0x000BF7E8 File Offset: 0x000BD9E8
				protected override Statement CreateDisposeCall(BlockContext bc, LocalVariable lv)
				{
					BuiltinTypeSpec idisposable = bc.BuiltinTypes.IDisposable;
					LocalVariable localVariable = LocalVariable.CreateCompilerGenerated(idisposable, bc.CurrentBlock, this.loc);
					Expression bool_expr = new Binary(Binary.Operator.Inequality, new CompilerAssign(localVariable.CreateReferenceExpression(bc, this.loc), new As(lv.CreateReferenceExpression(bc, this.loc), new TypeExpression(localVariable.Type, this.loc), this.loc), this.loc), new NullLiteral(this.loc));
					MethodGroupExpr methodGroupExpr = MethodGroupExpr.CreatePredefined(bc.Module.PredefinedMembers.IDisposableDispose.Resolve(this.loc), idisposable, this.loc);
					methodGroupExpr.InstanceExpression = localVariable.CreateReferenceExpression(bc, this.loc);
					Statement true_statement = new StatementExpression(new Invocation(methodGroupExpr, null));
					return new If(bool_expr, true_statement, this.loc);
				}
			}
		}
	}
}
