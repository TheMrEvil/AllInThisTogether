using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x020002B1 RID: 689
	public class BlockVariable : Statement
	{
		// Token: 0x060020F0 RID: 8432 RVA: 0x000A1614 File Offset: 0x0009F814
		public BlockVariable(FullNamedExpression type, LocalVariable li)
		{
			this.type_expr = type;
			this.li = li;
			this.loc = this.type_expr.Location;
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000A163B File Offset: 0x0009F83B
		protected BlockVariable(LocalVariable li)
		{
			this.li = li;
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060020F2 RID: 8434 RVA: 0x000A164A File Offset: 0x0009F84A
		public List<BlockVariableDeclarator> Declarators
		{
			get
			{
				return this.declarators;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x000A1652 File Offset: 0x0009F852
		// (set) Token: 0x060020F4 RID: 8436 RVA: 0x000A165A File Offset: 0x0009F85A
		public Expression Initializer
		{
			get
			{
				return this.initializer;
			}
			set
			{
				this.initializer = value;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x000A1663 File Offset: 0x0009F863
		public FullNamedExpression TypeExpression
		{
			get
			{
				return this.type_expr;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x000A166B File Offset: 0x0009F86B
		public LocalVariable Variable
		{
			get
			{
				return this.li;
			}
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000A1673 File Offset: 0x0009F873
		public void AddDeclarator(BlockVariableDeclarator decl)
		{
			if (this.declarators == null)
			{
				this.declarators = new List<BlockVariableDeclarator>();
			}
			this.declarators.Add(decl);
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x000A1694 File Offset: 0x0009F894
		private static void CreateEvaluatorVariable(BlockContext bc, LocalVariable li)
		{
			if (bc.Report.Errors != 0)
			{
				return;
			}
			TypeDefinition partialContainer = bc.CurrentMemberDefinition.Parent.PartialContainer;
			Field field = new Field(partialContainer, new TypeExpression(li.Type, li.Location), Modifiers.PUBLIC | Modifiers.STATIC, new MemberName(li.Name, li.Location), null);
			partialContainer.AddField(field);
			field.Define();
			li.HoistedVariant = new HoistedEvaluatorVariable(field);
			li.SetIsUsed();
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x000A170E File Offset: 0x0009F90E
		public override bool Resolve(BlockContext bc)
		{
			return this.Resolve(bc, true);
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x000A1718 File Offset: 0x0009F918
		public bool Resolve(BlockContext bc, bool resolveDeclaratorInitializers)
		{
			if (this.type == null && !this.li.IsCompilerGenerated)
			{
				VarExpr varExpr = this.type_expr as VarExpr;
				if (varExpr != null && !varExpr.IsPossibleType(bc))
				{
					if (bc.Module.Compiler.Settings.Version < LanguageVersion.V_3)
					{
						bc.Report.FeatureIsNotAvailable(bc.Module.Compiler, this.loc, "implicitly typed local variable");
					}
					if (this.li.IsFixed)
					{
						bc.Report.Error(821, this.loc, "A fixed statement cannot use an implicitly typed local variable");
						return false;
					}
					if (this.li.IsConstant)
					{
						bc.Report.Error(822, this.loc, "An implicitly typed local variable cannot be a constant");
						return false;
					}
					if (this.Initializer == null)
					{
						bc.Report.Error(818, this.loc, "An implicitly typed local variable declarator must include an initializer");
						return false;
					}
					if (this.declarators != null)
					{
						bc.Report.Error(819, this.loc, "An implicitly typed local variable declaration cannot include multiple declarators");
						this.declarators = null;
					}
					this.Initializer = this.Initializer.Resolve(bc);
					if (this.Initializer != null)
					{
						((VarExpr)this.type_expr).InferType(bc, this.Initializer);
						this.type = this.type_expr.Type;
					}
					else
					{
						this.type = InternalType.ErrorType;
					}
				}
				if (this.type == null)
				{
					this.type = this.type_expr.ResolveAsType(bc, false);
					if (this.type == null)
					{
						return false;
					}
					if (this.li.IsConstant && !this.type.IsConstantCompatible)
					{
						Const.Error_InvalidConstantType(this.type, this.loc, bc.Report);
					}
				}
				if (this.type.IsStatic)
				{
					FieldBase.Error_VariableOfStaticClass(this.loc, this.li.Name, this.type, bc.Report);
				}
				this.li.Type = this.type;
			}
			bool flag = bc.Module.Compiler.Settings.StatementMode && bc.CurrentBlock is ToplevelBlock;
			if (flag)
			{
				BlockVariable.CreateEvaluatorVariable(bc, this.li);
			}
			else if (this.type != InternalType.ErrorType)
			{
				this.li.PrepareAssignmentAnalysis(bc);
			}
			if (this.initializer != null)
			{
				this.initializer = this.ResolveInitializer(bc, this.li, this.initializer);
			}
			if (this.declarators != null)
			{
				foreach (BlockVariableDeclarator blockVariableDeclarator in this.declarators)
				{
					blockVariableDeclarator.Variable.Type = this.li.Type;
					if (flag)
					{
						BlockVariable.CreateEvaluatorVariable(bc, blockVariableDeclarator.Variable);
					}
					else if (this.type != InternalType.ErrorType)
					{
						blockVariableDeclarator.Variable.PrepareAssignmentAnalysis(bc);
					}
					if (blockVariableDeclarator.Initializer != null && resolveDeclaratorInitializers)
					{
						blockVariableDeclarator.Initializer = this.ResolveInitializer(bc, blockVariableDeclarator.Variable, blockVariableDeclarator.Initializer);
					}
				}
			}
			return true;
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x000A1A48 File Offset: 0x0009FC48
		protected virtual Expression ResolveInitializer(BlockContext bc, LocalVariable li, Expression initializer)
		{
			return new SimpleAssign(li.CreateReferenceExpression(bc, li.Location), initializer, li.Location).ResolveStatement(bc);
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000A1A6C File Offset: 0x0009FC6C
		protected override void DoEmit(EmitContext ec)
		{
			this.li.CreateBuilder(ec);
			if (this.Initializer != null && !base.IsUnreachable)
			{
				((ExpressionStatement)this.Initializer).EmitStatement(ec);
			}
			if (this.declarators != null)
			{
				foreach (BlockVariableDeclarator blockVariableDeclarator in this.declarators)
				{
					blockVariableDeclarator.Variable.CreateBuilder(ec);
					if (blockVariableDeclarator.Initializer != null && !base.IsUnreachable)
					{
						ec.Mark(blockVariableDeclarator.Variable.Location);
						((ExpressionStatement)blockVariableDeclarator.Initializer).EmitStatement(ec);
					}
				}
			}
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000A1B2C File Offset: 0x0009FD2C
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			if (this.Initializer != null)
			{
				this.Initializer.FlowAnalysis(fc);
			}
			if (this.declarators != null)
			{
				foreach (BlockVariableDeclarator blockVariableDeclarator in this.declarators)
				{
					if (blockVariableDeclarator.Initializer != null)
					{
						blockVariableDeclarator.Initializer.FlowAnalysis(fc);
					}
				}
			}
			return false;
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x000A1BAC File Offset: 0x0009FDAC
		public override Reachability MarkReachable(Reachability rc)
		{
			ExpressionStatement expressionStatement = this.initializer as ExpressionStatement;
			if (expressionStatement != null)
			{
				expressionStatement.MarkReachable(rc);
			}
			return base.MarkReachable(rc);
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000A1BD8 File Offset: 0x0009FDD8
		protected override void CloneTo(CloneContext clonectx, Statement target)
		{
			BlockVariable blockVariable = (BlockVariable)target;
			if (this.type_expr != null)
			{
				blockVariable.type_expr = (FullNamedExpression)this.type_expr.Clone(clonectx);
			}
			if (this.initializer != null)
			{
				blockVariable.initializer = this.initializer.Clone(clonectx);
			}
			if (this.declarators != null)
			{
				blockVariable.declarators = null;
				foreach (BlockVariableDeclarator blockVariableDeclarator in this.declarators)
				{
					blockVariable.AddDeclarator(blockVariableDeclarator.Clone(clonectx));
				}
			}
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000A1C80 File Offset: 0x0009FE80
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000C3D RID: 3133
		private Expression initializer;

		// Token: 0x04000C3E RID: 3134
		protected FullNamedExpression type_expr;

		// Token: 0x04000C3F RID: 3135
		protected LocalVariable li;

		// Token: 0x04000C40 RID: 3136
		protected List<BlockVariableDeclarator> declarators;

		// Token: 0x04000C41 RID: 3137
		private TypeSpec type;
	}
}
