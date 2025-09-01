using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020002C2 RID: 706
	public class Catch : Statement
	{
		// Token: 0x060021FE RID: 8702 RVA: 0x000A69CC File Offset: 0x000A4BCC
		public Catch(ExplicitBlock block, Location loc)
		{
			this.block = block;
			this.loc = loc;
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x000A69E2 File Offset: 0x000A4BE2
		public ExplicitBlock Block
		{
			get
			{
				return this.block;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x000A69EA File Offset: 0x000A4BEA
		public TypeSpec CatchType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x000A69F2 File Offset: 0x000A4BF2
		// (set) Token: 0x06002202 RID: 8706 RVA: 0x000A69FA File Offset: 0x000A4BFA
		public Expression Filter
		{
			[CompilerGenerated]
			get
			{
				return this.<Filter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Filter>k__BackingField = value;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x000A6A03 File Offset: 0x000A4C03
		public bool IsGeneral
		{
			get
			{
				return this.type_expr == null;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x000A6A0E File Offset: 0x000A4C0E
		// (set) Token: 0x06002205 RID: 8709 RVA: 0x000A6A16 File Offset: 0x000A4C16
		public FullNamedExpression TypeExpression
		{
			get
			{
				return this.type_expr;
			}
			set
			{
				this.type_expr = value;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x000A6A1F File Offset: 0x000A4C1F
		// (set) Token: 0x06002207 RID: 8711 RVA: 0x000A6A27 File Offset: 0x000A4C27
		public LocalVariable Variable
		{
			get
			{
				return this.li;
			}
			set
			{
				this.li = value;
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000A6A30 File Offset: 0x000A4C30
		protected override void DoEmit(EmitContext ec)
		{
			if (this.Filter == null)
			{
				if (this.IsGeneral)
				{
					ec.BeginCatchBlock(ec.BuiltinTypes.Object);
				}
				else
				{
					ec.BeginCatchBlock(this.CatchType);
				}
				if (this.li == null)
				{
					ec.Emit(OpCodes.Pop);
				}
				if (this.Block.HasAwait)
				{
					if (this.li != null)
					{
						this.EmitCatchVariableStore(ec);
						return;
					}
				}
				else
				{
					this.Block.Emit(ec);
				}
				return;
			}
			ec.BeginExceptionFilterBlock();
			ec.Emit(OpCodes.Isinst, this.IsGeneral ? ec.BuiltinTypes.Object : this.CatchType);
			if (this.Block.HasAwait)
			{
				this.Block.EmitScopeInitialization(ec);
				return;
			}
			this.Block.Emit(ec);
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000A6AFC File Offset: 0x000A4CFC
		private void EmitCatchVariableStore(EmitContext ec)
		{
			this.li.CreateBuilder(ec);
			if (this.li.HoistedVariant != null)
			{
				this.hoisted_temp = new LocalTemporary(this.li.Type);
				this.hoisted_temp.Store(ec);
				this.assign.UpdateSource(this.hoisted_temp);
			}
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000A6B58 File Offset: 0x000A4D58
		public override bool Resolve(BlockContext bc)
		{
			bool result;
			using (bc.Set(ResolveContext.Options.CatchScope))
			{
				if (this.type_expr == null)
				{
					if (this.CreateExceptionVariable(bc.Module.Compiler.BuiltinTypes.Object))
					{
						if (!this.block.HasAwait || this.Filter != null)
						{
							this.block.AddScopeStatement(new Catch.CatchVariableStore(this));
						}
						Expression source = new EmptyExpression(this.li.Type);
						this.assign = new CompilerAssign(new LocalVariableReference(this.li, Location.Null), source, Location.Null);
						this.Block.AddScopeStatement(new StatementExpression(this.assign, Location.Null));
					}
				}
				else
				{
					this.type = this.type_expr.ResolveAsType(bc, false);
					if (this.type == null)
					{
						return false;
					}
					if (this.li == null)
					{
						this.CreateExceptionVariable(this.type);
					}
					if (this.type.BuiltinType != BuiltinTypeSpec.Type.Exception && !TypeSpec.IsBaseClass(this.type, bc.BuiltinTypes.Exception, false))
					{
						bc.Report.Error(155, this.loc, "The type caught or thrown must be derived from System.Exception");
					}
					else if (this.li != null)
					{
						this.li.Type = this.type;
						this.li.PrepareAssignmentAnalysis(bc);
						Expression expression = new EmptyExpression(this.li.Type);
						if (this.li.Type.IsGenericParameter)
						{
							expression = new UnboxCast(expression, this.li.Type);
						}
						if (!this.block.HasAwait || this.Filter != null)
						{
							this.block.AddScopeStatement(new Catch.CatchVariableStore(this));
						}
						this.assign = new CompilerAssign(new LocalVariableReference(this.li, Location.Null), expression, Location.Null);
						this.Block.AddScopeStatement(new StatementExpression(this.assign, Location.Null));
					}
				}
				if (this.Filter != null)
				{
					this.Block.AddScopeStatement(new Catch.FilterStatement(this));
				}
				this.Block.SetCatchBlock();
				result = this.Block.Resolve(bc);
			}
			return result;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x000A6DAC File Offset: 0x000A4FAC
		private bool CreateExceptionVariable(TypeSpec type)
		{
			if (!this.Block.HasAwait)
			{
				return false;
			}
			this.li = LocalVariable.CreateCompilerGenerated(type, this.block, Location.Null);
			return true;
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000A6DD5 File Offset: 0x000A4FD5
		protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
		{
			if (this.li != null && !this.li.IsCompilerGenerated)
			{
				fc.SetVariableAssigned(this.li.VariableInfo, true);
			}
			return this.block.FlowAnalysis(fc);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000A6E0C File Offset: 0x000A500C
		public override Reachability MarkReachable(Reachability rc)
		{
			base.MarkReachable(rc);
			Constant constant = this.Filter as Constant;
			if (constant != null && constant.IsDefaultValue)
			{
				return Reachability.CreateUnreachable();
			}
			return this.block.MarkReachable(rc);
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000A6E4C File Offset: 0x000A504C
		protected override void CloneTo(CloneContext clonectx, Statement t)
		{
			Catch @catch = (Catch)t;
			if (this.type_expr != null)
			{
				@catch.type_expr = (FullNamedExpression)this.type_expr.Clone(clonectx);
			}
			if (this.Filter != null)
			{
				@catch.Filter = this.Filter.Clone(clonectx);
			}
			@catch.block = (ExplicitBlock)clonectx.LookupBlock(this.block);
		}

		// Token: 0x04000C8C RID: 3212
		private ExplicitBlock block;

		// Token: 0x04000C8D RID: 3213
		private LocalVariable li;

		// Token: 0x04000C8E RID: 3214
		private FullNamedExpression type_expr;

		// Token: 0x04000C8F RID: 3215
		private CompilerAssign assign;

		// Token: 0x04000C90 RID: 3216
		private TypeSpec type;

		// Token: 0x04000C91 RID: 3217
		private LocalTemporary hoisted_temp;

		// Token: 0x04000C92 RID: 3218
		[CompilerGenerated]
		private Expression <Filter>k__BackingField;

		// Token: 0x020003FB RID: 1019
		private class CatchVariableStore : Statement
		{
			// Token: 0x06002808 RID: 10248 RVA: 0x000BDB2D File Offset: 0x000BBD2D
			public CatchVariableStore(Catch ctch)
			{
				this.ctch = ctch;
			}

			// Token: 0x06002809 RID: 10249 RVA: 0x0000AF70 File Offset: 0x00009170
			protected override void CloneTo(CloneContext clonectx, Statement target)
			{
			}

			// Token: 0x0600280A RID: 10250 RVA: 0x000BDB3C File Offset: 0x000BBD3C
			protected override void DoEmit(EmitContext ec)
			{
				this.ctch.EmitCatchVariableStore(ec);
			}

			// Token: 0x0600280B RID: 10251 RVA: 0x0000212D File Offset: 0x0000032D
			protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
			{
				return true;
			}

			// Token: 0x04001156 RID: 4438
			private readonly Catch ctch;
		}

		// Token: 0x020003FC RID: 1020
		private class FilterStatement : Statement
		{
			// Token: 0x0600280C RID: 10252 RVA: 0x000BDB4A File Offset: 0x000BBD4A
			public FilterStatement(Catch ctch)
			{
				this.ctch = ctch;
			}

			// Token: 0x0600280D RID: 10253 RVA: 0x0000AF70 File Offset: 0x00009170
			protected override void CloneTo(CloneContext clonectx, Statement target)
			{
			}

			// Token: 0x0600280E RID: 10254 RVA: 0x000BDB5C File Offset: 0x000BBD5C
			protected override void DoEmit(EmitContext ec)
			{
				if (this.ctch.li != null)
				{
					if (this.ctch.hoisted_temp != null)
					{
						this.ctch.hoisted_temp.Emit(ec);
					}
					else
					{
						this.ctch.li.Emit(ec);
					}
					if (!this.ctch.IsGeneral && this.ctch.type.Kind == MemberKind.TypeParameter)
					{
						ec.Emit(OpCodes.Box, this.ctch.type);
					}
				}
				Label label = ec.DefineLabel();
				Label label2 = ec.DefineLabel();
				ec.Emit(OpCodes.Brtrue_S, label);
				ec.EmitInt(0);
				ec.Emit(OpCodes.Br, label2);
				ec.MarkLabel(label);
				this.ctch.Filter.Emit(ec);
				ec.MarkLabel(label2);
				ec.Emit(OpCodes.Endfilter);
				ec.BeginFilterHandler();
				ec.Emit(OpCodes.Pop);
			}

			// Token: 0x0600280F RID: 10255 RVA: 0x000BDC49 File Offset: 0x000BBE49
			protected override bool DoFlowAnalysis(FlowAnalysisContext fc)
			{
				this.ctch.Filter.FlowAnalysis(fc);
				return true;
			}

			// Token: 0x06002810 RID: 10256 RVA: 0x000BDC60 File Offset: 0x000BBE60
			public override bool Resolve(BlockContext bc)
			{
				this.ctch.Filter = this.ctch.Filter.Resolve(bc);
				if (this.ctch.Filter != null)
				{
					if (this.ctch.Filter.ContainsEmitWithAwait())
					{
						bc.Report.Error(7094, this.ctch.Filter.Location, "The `await' operator cannot be used in the filter expression of a catch clause");
					}
					Constant constant = this.ctch.Filter as Constant;
					if (constant != null && !constant.IsDefaultValue)
					{
						bc.Report.Warning(7095, 1, this.ctch.Filter.Location, "Exception filter expression is a constant");
					}
				}
				return true;
			}

			// Token: 0x04001157 RID: 4439
			private readonly Catch ctch;
		}
	}
}
