using System;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002C5 RID: 709
	public class Using : TryFinallyBlock
	{
		// Token: 0x06002229 RID: 8745 RVA: 0x000A79FD File Offset: 0x000A5BFD
		public Using(Using.VariableDeclaration decl, Statement stmt, Location loc) : base(stmt, loc)
		{
			this.decl = decl;
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000A7A0E File Offset: 0x000A5C0E
		public Using(Expression expr, Statement stmt, Location loc) : base(stmt, loc)
		{
			this.decl = new Using.VariableDeclaration(expr);
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x000A7A24 File Offset: 0x000A5C24
		public Expression Expr
		{
			get
			{
				if (this.decl.Variable != null)
				{
					return null;
				}
				return this.decl.Initializer;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x000A7A40 File Offset: 0x000A5C40
		public BlockVariable Variables
		{
			get
			{
				return this.decl;
			}
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000A7A48 File Offset: 0x000A5C48
		public override void Emit(EmitContext ec)
		{
			this.DoEmit(ec);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000A7A51 File Offset: 0x000A5C51
		protected override void EmitTryBodyPrepare(EmitContext ec)
		{
			this.decl.Emit(ec);
			base.EmitTryBodyPrepare(ec);
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x000A7A66 File Offset: 0x000A5C66
		protected override void EmitTryBody(EmitContext ec)
		{
			this.stmt.Emit(ec);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x000A7A74 File Offset: 0x000A5C74
		public override void EmitFinallyBody(EmitContext ec)
		{
			this.decl.EmitDispose(ec);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000A7A82 File Offset: 0x000A5C82
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			this.decl.FlowAnalysis(fc);
			return this.stmt.FlowAnalysis(fc);
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x000A7A9D File Offset: 0x000A5C9D
		public override Reachability MarkReachable(Reachability rc)
		{
			this.decl.MarkReachable(rc);
			return base.MarkReachable(rc);
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000A7AB4 File Offset: 0x000A5CB4
		public override bool Resolve(BlockContext ec)
		{
			bool isLockedByStatement = false;
			VariableReference variableReference;
			using (ec.Set(ResolveContext.Options.UsingInitializerScope))
			{
				if (this.decl.Variable == null)
				{
					variableReference = (this.decl.ResolveExpression(ec) as VariableReference);
					if (variableReference != null)
					{
						isLockedByStatement = variableReference.IsLockedByStatement;
						variableReference.IsLockedByStatement = true;
					}
				}
				else
				{
					if (this.decl.IsNested)
					{
						this.decl.ResolveDeclaratorInitializer(ec);
					}
					else
					{
						if (!this.decl.Resolve(ec))
						{
							return false;
						}
						if (this.decl.Declarators != null)
						{
							this.stmt = this.decl.RewriteUsingDeclarators(ec, this.stmt);
						}
					}
					variableReference = null;
				}
			}
			bool result = base.Resolve(ec);
			if (variableReference != null)
			{
				variableReference.IsLockedByStatement = isLockedByStatement;
			}
			return result;
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000A7B88 File Offset: 0x000A5D88
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			Using @using = (Using)t;
			@using.decl = (Using.VariableDeclaration)this.decl.Clone(clonectx);
			@using.stmt = this.stmt.Clone(clonectx);
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x000A7BB8 File Offset: 0x000A5DB8
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C9B RID: 3227
		private Using.VariableDeclaration decl;

		// Token: 0x020003FD RID: 1021
		public class VariableDeclaration : BlockVariable
		{
			// Token: 0x06002811 RID: 10257 RVA: 0x000A1C89 File Offset: 0x0009FE89
			public VariableDeclaration(FullNamedExpression type, LocalVariable li) : base(type, li)
			{
			}

			// Token: 0x06002812 RID: 10258 RVA: 0x000BDD10 File Offset: 0x000BBF10
			public VariableDeclaration(LocalVariable li, Location loc) : base(li)
			{
				this.reachable = true;
				this.loc = loc;
			}

			// Token: 0x06002813 RID: 10259 RVA: 0x000BDD27 File Offset: 0x000BBF27
			public VariableDeclaration(Expression expr) : base(null)
			{
				this.loc = expr.Location;
				base.Initializer = expr;
			}

			// Token: 0x1700090B RID: 2315
			// (get) Token: 0x06002814 RID: 10260 RVA: 0x000BDD43 File Offset: 0x000BBF43
			// (set) Token: 0x06002815 RID: 10261 RVA: 0x000BDD4B File Offset: 0x000BBF4B
			public bool IsNested
			{
				[CompilerGenerated]
				get
				{
					return this.<IsNested>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<IsNested>k__BackingField = value;
				}
			}

			// Token: 0x06002816 RID: 10262 RVA: 0x000BDD54 File Offset: 0x000BBF54
			public void EmitDispose(EmitContext ec)
			{
				this.dispose_call.Emit(ec);
			}

			// Token: 0x06002817 RID: 10263 RVA: 0x000BDD62 File Offset: 0x000BBF62
			public override bool Resolve(BlockContext bc)
			{
				return this.IsNested || base.Resolve(bc, false);
			}

			// Token: 0x06002818 RID: 10264 RVA: 0x000BDD78 File Offset: 0x000BBF78
			public Expression ResolveExpression(BlockContext bc)
			{
				Expression expression = base.Initializer.Resolve(bc);
				if (expression == null)
				{
					return null;
				}
				this.li = LocalVariable.CreateCompilerGenerated(expression.Type, bc.CurrentBlock, this.loc);
				base.Initializer = this.ResolveInitializer(bc, base.Variable, expression);
				return expression;
			}

			// Token: 0x06002819 RID: 10265 RVA: 0x000BDDCC File Offset: 0x000BBFCC
			protected override Expression ResolveInitializer(BlockContext bc, LocalVariable li, Expression initializer)
			{
				if (li.Type.BuiltinType != BuiltinTypeSpec.Type.Dynamic)
				{
					if (li == base.Variable)
					{
						this.CheckIDiposableConversion(bc, li, initializer);
						this.dispose_call = this.CreateDisposeCall(bc, li);
						this.dispose_call.Resolve(bc);
					}
					return base.ResolveInitializer(bc, li, initializer);
				}
				initializer = initializer.Resolve(bc);
				if (initializer == null)
				{
					return null;
				}
				Arguments arguments = new Arguments(1);
				arguments.Add(new Argument(initializer));
				initializer = new DynamicConversion(bc.BuiltinTypes.IDisposable, CSharpBinderFlags.None, arguments, initializer.Location).Resolve(bc);
				if (initializer == null)
				{
					return null;
				}
				LocalVariable localVariable = LocalVariable.CreateCompilerGenerated(initializer.Type, bc.CurrentBlock, this.loc);
				this.dispose_call = this.CreateDisposeCall(bc, localVariable);
				this.dispose_call.Resolve(bc);
				return base.ResolveInitializer(bc, li, new SimpleAssign(localVariable.CreateReferenceExpression(bc, this.loc), initializer, this.loc));
			}

			// Token: 0x0600281A RID: 10266 RVA: 0x000BDEBC File Offset: 0x000BC0BC
			protected virtual void CheckIDiposableConversion(BlockContext bc, LocalVariable li, Expression initializer)
			{
				TypeSpec type = li.Type;
				if (type.BuiltinType == BuiltinTypeSpec.Type.IDisposable || Using.VariableDeclaration.CanConvertToIDisposable(bc, type))
				{
					return;
				}
				if (type.IsNullableType)
				{
					return;
				}
				if (type != InternalType.ErrorType)
				{
					bc.Report.SymbolRelatedToPreviousError(type);
					Location loc = (this.type_expr == null) ? initializer.Location : this.type_expr.Location;
					bc.Report.Error(1674, loc, "`{0}': type used in a using statement must be implicitly convertible to `System.IDisposable'", type.GetSignatureForError());
				}
			}

			// Token: 0x0600281B RID: 10267 RVA: 0x000BDF3C File Offset: 0x000BC13C
			private static bool CanConvertToIDisposable(BlockContext bc, TypeSpec type)
			{
				BuiltinTypeSpec idisposable = bc.BuiltinTypes.IDisposable;
				TypeParameterSpec typeParameterSpec = type as TypeParameterSpec;
				if (typeParameterSpec != null)
				{
					return Convert.ImplicitTypeParameterConversion(null, typeParameterSpec, idisposable) != null;
				}
				return type.ImplementsInterface(idisposable, false);
			}

			// Token: 0x0600281C RID: 10268 RVA: 0x000BDF74 File Offset: 0x000BC174
			protected virtual Statement CreateDisposeCall(BlockContext bc, LocalVariable lv)
			{
				Expression expression = lv.CreateReferenceExpression(bc, lv.Location);
				TypeSpec type = lv.Type;
				Location location = lv.Location;
				BuiltinTypeSpec idisposable = bc.BuiltinTypes.IDisposable;
				MethodGroupExpr methodGroupExpr = MethodGroupExpr.CreatePredefined(bc.Module.PredefinedMembers.IDisposableDispose.Resolve(location), idisposable, location);
				methodGroupExpr.InstanceExpression = (type.IsNullableType ? new Cast(new TypeExpression(idisposable, location), expression, location).Resolve(bc) : expression);
				Statement statement = new StatementExpression(new Invocation(methodGroupExpr, null), Location.Null);
				if (!TypeSpec.IsValueType(type) || type.IsNullableType)
				{
					Expression bool_expr = new Binary(Binary.Operator.Inequality, expression, new NullLiteral(location));
					Statement statement2 = statement;
					statement = new If(bool_expr, statement2, statement2.loc);
				}
				return statement;
			}

			// Token: 0x0600281D RID: 10269 RVA: 0x000BE02F File Offset: 0x000BC22F
			public void ResolveDeclaratorInitializer(BlockContext bc)
			{
				base.Initializer = base.ResolveInitializer(bc, base.Variable, base.Initializer);
			}

			// Token: 0x0600281E RID: 10270 RVA: 0x000BE04C File Offset: 0x000BC24C
			public Statement RewriteUsingDeclarators(BlockContext bc, Statement stmt)
			{
				for (int i = this.declarators.Count - 1; i >= 0; i--)
				{
					BlockVariableDeclarator blockVariableDeclarator = this.declarators[i];
					Using.VariableDeclaration variableDeclaration = new Using.VariableDeclaration(blockVariableDeclarator.Variable, blockVariableDeclarator.Variable.Location);
					variableDeclaration.Initializer = blockVariableDeclarator.Initializer;
					variableDeclaration.IsNested = true;
					variableDeclaration.dispose_call = this.CreateDisposeCall(bc, blockVariableDeclarator.Variable);
					variableDeclaration.dispose_call.Resolve(bc);
					stmt = new Using(variableDeclaration, stmt, blockVariableDeclarator.Variable.Location);
				}
				this.declarators = null;
				return stmt;
			}

			// Token: 0x0600281F RID: 10271 RVA: 0x000A1C80 File Offset: 0x0009FE80
			public override object Accept(StructuralVisitor visitor)
			{
				return visitor.Visit(this);
			}

			// Token: 0x04001158 RID: 4440
			private Statement dispose_call;

			// Token: 0x04001159 RID: 4441
			[CompilerGenerated]
			private bool <IsNested>k__BackingField;
		}
	}
}
