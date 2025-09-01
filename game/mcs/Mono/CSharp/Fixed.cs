using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002C1 RID: 705
	public class Fixed : Statement
	{
		// Token: 0x060021F5 RID: 8693 RVA: 0x000A6798 File Offset: 0x000A4998
		public Fixed(Fixed.VariableDeclaration decl, Statement stmt, Location l)
		{
			this.decl = decl;
			this.statement = stmt;
			this.loc = l;
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x000A67B5 File Offset: 0x000A49B5
		public Statement Statement
		{
			get
			{
				return this.statement;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x000A67BD File Offset: 0x000A49BD
		public BlockVariable Variables
		{
			get
			{
				return this.decl;
			}
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000A67C8 File Offset: 0x000A49C8
		public override bool Resolve(BlockContext bc)
		{
			using (bc.Set(ResolveContext.Options.FixedInitializerScope))
			{
				if (!this.decl.Resolve(bc))
				{
					return false;
				}
			}
			return this.statement.Resolve(bc);
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000A6824 File Offset: 0x000A4A24
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			this.decl.FlowAnalysis(fc);
			return this.statement.FlowAnalysis(fc);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000A6840 File Offset: 0x000A4A40
		protected override void DoEmit(EmitContext ec)
		{
			this.decl.Variable.CreateBuilder(ec);
			this.decl.Initializer.Emit(ec);
			if (this.decl.Declarators != null)
			{
				foreach (BlockVariableDeclarator blockVariableDeclarator in this.decl.Declarators)
				{
					blockVariableDeclarator.Variable.CreateBuilder(ec);
					blockVariableDeclarator.Initializer.Emit(ec);
				}
			}
			this.statement.Emit(ec);
			if (this.has_ret)
			{
				return;
			}
			((Fixed.Emitter)this.decl.Initializer).EmitExit(ec);
			if (this.decl.Declarators != null)
			{
				foreach (BlockVariableDeclarator blockVariableDeclarator2 in this.decl.Declarators)
				{
					((Fixed.Emitter)blockVariableDeclarator2.Initializer).EmitExit(ec);
				}
			}
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000A6960 File Offset: 0x000A4B60
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			this.decl.MarkReachable(rc);
			rc = this.statement.MarkReachable(rc);
			this.has_ret = rc.IsUnreachable;
			return rc;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000A6993 File Offset: 0x000A4B93
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			Fixed @fixed = (Fixed)t;
			@fixed.decl = (Fixed.VariableDeclaration)this.decl.Clone(clonectx);
			@fixed.statement = this.statement.Clone(clonectx);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000A69C3 File Offset: 0x000A4BC3
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C89 RID: 3209
		private Fixed.VariableDeclaration decl;

		// Token: 0x04000C8A RID: 3210
		private Statement statement;

		// Token: 0x04000C8B RID: 3211
		private bool has_ret;

		// Token: 0x020003F7 RID: 1015
		private abstract class Emitter : ShimExpression
		{
			// Token: 0x060027FB RID: 10235 RVA: 0x000BD7B8 File Offset: 0x000BB9B8
			protected Emitter(Expression expr, LocalVariable li) : base(expr)
			{
				this.vi = li;
			}

			// Token: 0x060027FC RID: 10236
			public abstract void EmitExit(EmitContext ec);

			// Token: 0x060027FD RID: 10237 RVA: 0x000BD7C8 File Offset: 0x000BB9C8
			public override void FlowAnalysis(FlowAnalysisContext fc)
			{
				this.expr.FlowAnalysis(fc);
			}

			// Token: 0x04001154 RID: 4436
			protected LocalVariable vi;
		}

		// Token: 0x020003F8 RID: 1016
		private class ExpressionEmitter : Fixed.Emitter
		{
			// Token: 0x060027FE RID: 10238 RVA: 0x000BD7D6 File Offset: 0x000BB9D6
			public ExpressionEmitter(Expression converted, LocalVariable li) : base(converted, li)
			{
			}

			// Token: 0x060027FF RID: 10239 RVA: 0x00023DF4 File Offset: 0x00021FF4
			protected override Expression DoResolve(ResolveContext rc)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06002800 RID: 10240 RVA: 0x000BD7E0 File Offset: 0x000BB9E0
			public override void Emit(EmitContext ec)
			{
				this.expr.Emit(ec);
				this.vi.EmitAssign(ec);
			}

			// Token: 0x06002801 RID: 10241 RVA: 0x000BD7FA File Offset: 0x000BB9FA
			public override void EmitExit(EmitContext ec)
			{
				ec.EmitInt(0);
				ec.Emit(OpCodes.Conv_U);
				this.vi.EmitAssign(ec);
			}
		}

		// Token: 0x020003F9 RID: 1017
		private class StringEmitter : Fixed.Emitter
		{
			// Token: 0x06002802 RID: 10242 RVA: 0x000BD7D6 File Offset: 0x000BB9D6
			public StringEmitter(Expression expr, LocalVariable li) : base(expr, li)
			{
			}

			// Token: 0x06002803 RID: 10243 RVA: 0x000BD81C File Offset: 0x000BBA1C
			protected override Expression DoResolve(ResolveContext rc)
			{
				this.pinned_string = new LocalVariable(this.vi.Block, "$pinned", LocalVariable.Flags.Used | LocalVariable.Flags.CompilerGenerated | LocalVariable.Flags.FixedVariable, this.vi.Location);
				this.pinned_string.Type = rc.BuiltinTypes.String;
				this.eclass = ExprClass.Variable;
				this.type = rc.BuiltinTypes.Int;
				return this;
			}

			// Token: 0x06002804 RID: 10244 RVA: 0x000BD880 File Offset: 0x000BBA80
			public override void Emit(EmitContext ec)
			{
				this.pinned_string.CreateBuilder(ec);
				this.expr.Emit(ec);
				this.pinned_string.EmitAssign(ec);
				this.pinned_string.Emit(ec);
				ec.Emit(OpCodes.Conv_I);
				PropertySpec propertySpec = ec.Module.PredefinedMembers.RuntimeHelpersOffsetToStringData.Resolve(this.loc);
				if (propertySpec == null)
				{
					return;
				}
				new PropertyExpr(propertySpec, this.pinned_string.Location).Resolve(new ResolveContext(ec.MemberContext)).Emit(ec);
				ec.Emit(OpCodes.Add);
				this.vi.EmitAssign(ec);
			}

			// Token: 0x06002805 RID: 10245 RVA: 0x000BD926 File Offset: 0x000BBB26
			public override void EmitExit(EmitContext ec)
			{
				ec.EmitNull();
				this.pinned_string.EmitAssign(ec);
			}

			// Token: 0x04001155 RID: 4437
			private LocalVariable pinned_string;
		}

		// Token: 0x020003FA RID: 1018
		public class VariableDeclaration : BlockVariable
		{
			// Token: 0x06002806 RID: 10246 RVA: 0x000A1C89 File Offset: 0x0009FE89
			public VariableDeclaration(FullNamedExpression type, LocalVariable li) : base(type, li)
			{
			}

			// Token: 0x06002807 RID: 10247 RVA: 0x000BD93C File Offset: 0x000BBB3C
			protected override Expression ResolveInitializer(BlockContext bc, LocalVariable li, Expression initializer)
			{
				if (!base.Variable.Type.IsPointer && li == base.Variable)
				{
					bc.Report.Error(209, base.TypeExpression.Location, "The type of locals declared in a fixed statement must be a pointer type");
					return null;
				}
				Expression expression = initializer.Resolve(bc);
				if (expression == null)
				{
					return null;
				}
				if (expression.Type.IsArray)
				{
					TypeSpec elementType = TypeManager.GetElementType(expression.Type);
					if (!TypeManager.VerifyUnmanaged(bc.Module, elementType, this.loc))
					{
						return null;
					}
					ArrayPtr arrayPtr = new ArrayPtr(expression, elementType, this.loc);
					Expression expression2 = Convert.ImplicitConversionRequired(bc, arrayPtr.Resolve(bc), li.Type, this.loc);
					if (expression2 == null)
					{
						return null;
					}
					expression2 = new Conditional(new BooleanExpression(new Binary(Binary.Operator.LogicalOr, new Binary(Binary.Operator.Equality, expression, new NullLiteral(this.loc)), new Binary(Binary.Operator.Equality, new MemberAccess(expression, "Length"), new IntConstant(bc.BuiltinTypes, 0, this.loc)))), new NullLiteral(this.loc), expression2, this.loc);
					expression2 = expression2.Resolve(bc);
					return new Fixed.ExpressionEmitter(expression2, li);
				}
				else
				{
					if (expression.Type.BuiltinType == BuiltinTypeSpec.Type.String)
					{
						return new Fixed.StringEmitter(expression, li).Resolve(bc);
					}
					if (expression is FixedBufferPtr)
					{
						return new Fixed.ExpressionEmitter(expression, li);
					}
					bool flag = true;
					Unary unary = expression as Unary;
					if (unary != null)
					{
						if (unary.Oper == Unary.Operator.AddressOf)
						{
							IVariableReference variableReference = unary.Expr as IVariableReference;
							if (variableReference == null || !variableReference.IsFixed)
							{
								flag = false;
							}
						}
					}
					else if (initializer is Cast)
					{
						bc.Report.Error(254, initializer.Location, "The right hand side of a fixed statement assignment may not be a cast expression");
						return null;
					}
					if (flag)
					{
						bc.Report.Error(213, this.loc, "You cannot use the fixed statement to take the address of an already fixed expression");
					}
					expression = Convert.ImplicitConversionRequired(bc, expression, li.Type, this.loc);
					return new Fixed.ExpressionEmitter(expression, li);
				}
			}
		}
	}
}
