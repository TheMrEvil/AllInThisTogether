using System;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020001FE RID: 510
	public class ArrayAccess : Expression, IDynamicAssign, IAssignMethod, IMemoryLocation
	{
		// Token: 0x06001A72 RID: 6770 RVA: 0x000816D1 File Offset: 0x0007F8D1
		public ArrayAccess(ElementAccess ea_data, Location l)
		{
			this.ea = ea_data;
			this.loc = l;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x000816E7 File Offset: 0x0007F8E7
		// (set) Token: 0x06001A74 RID: 6772 RVA: 0x000816EF File Offset: 0x0007F8EF
		public bool ConditionalAccess
		{
			[CompilerGenerated]
			get
			{
				return this.<ConditionalAccess>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ConditionalAccess>k__BackingField = value;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x000816F8 File Offset: 0x0007F8F8
		// (set) Token: 0x06001A76 RID: 6774 RVA: 0x00081700 File Offset: 0x0007F900
		public bool ConditionalAccessReceiver
		{
			[CompilerGenerated]
			get
			{
				return this.<ConditionalAccessReceiver>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ConditionalAccessReceiver>k__BackingField = value;
			}
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0008170C File Offset: 0x0007F90C
		public void AddressOf(EmitContext ec, AddressOp mode)
		{
			ArrayContainer arrayContainer = (ArrayContainer)this.ea.Expr.Type;
			if (this.has_await_args == null && ec.HasSet(BuilderContext.Options.AsyncBody) && this.ea.Arguments.ContainsEmitWithAwait())
			{
				this.LoadInstanceAndArguments(ec, false, true);
			}
			this.LoadInstanceAndArguments(ec, false, false);
			if (arrayContainer.Element.IsGenericParameter && mode == AddressOp.Load)
			{
				ec.Emit(OpCodes.Readonly);
			}
			ec.EmitArrayAddress(arrayContainer);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0008178D File Offset: 0x0007F98D
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			if (this.ConditionalAccess)
			{
				base.Error_NullShortCircuitInsideExpressionTree(ec);
			}
			return this.ea.CreateExpressionTree(ec);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000817AA File Offset: 0x0007F9AA
		public override bool ContainsEmitWithAwait()
		{
			return this.ea.ContainsEmitWithAwait();
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000817B7 File Offset: 0x0007F9B7
		public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
		{
			if (this.HasConditionalAccess())
			{
				base.Error_NullPropagatingLValue(ec);
			}
			return this.DoResolve(ec);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000817D0 File Offset: 0x0007F9D0
		protected override Expression DoResolve(ResolveContext ec)
		{
			bool flag;
			this.ea.Arguments.Resolve(ec, out flag);
			ArrayContainer arrayContainer = this.ea.Expr.Type as ArrayContainer;
			int count = this.ea.Arguments.Count;
			if (arrayContainer.Rank != count)
			{
				ec.Report.Error(22, this.ea.Location, "Wrong number of indexes `{0}' inside [], expected `{1}'", count.ToString(), arrayContainer.Rank.ToString());
				return null;
			}
			this.type = arrayContainer.Element;
			if (this.type.IsPointer && !ec.IsUnsafe)
			{
				Expression.UnsafeError(ec, this.ea.Location);
			}
			if (this.ConditionalAccessReceiver)
			{
				this.type = Expression.LiftMemberType(ec, this.type);
			}
			foreach (Argument argument in this.ea.Arguments)
			{
				NamedArgument namedArgument = argument as NamedArgument;
				if (namedArgument != null)
				{
					Expression.Error_NamedArgument(namedArgument, ec.Report);
				}
				argument.Expr = base.ConvertExpressionToArrayIndex(ec, argument.Expr, false);
			}
			this.eclass = ExprClass.Variable;
			return this;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00081920 File Offset: 0x0007FB20
		protected override void Error_NegativeArrayIndex(ResolveContext ec, Location loc)
		{
			ec.Report.Warning(251, 2, loc, "Indexing an array with a negative index (array indices always start at zero)");
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00081939 File Offset: 0x0007FB39
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.ea.FlowAnalysis(fc);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x00081947 File Offset: 0x0007FB47
		public override bool HasConditionalAccess()
		{
			return this.ConditionalAccess || this.ea.Expr.HasConditionalAccess();
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00081964 File Offset: 0x0007FB64
		private void LoadInstanceAndArguments(EmitContext ec, bool duplicateArguments, bool prepareAwait)
		{
			if (prepareAwait)
			{
				this.ea.Expr = this.ea.Expr.EmitToField(ec);
			}
			else
			{
				InstanceEmitter instanceEmitter = new InstanceEmitter(this.ea.Expr, false);
				instanceEmitter.Emit(ec, this.ConditionalAccess);
				if (duplicateArguments)
				{
					ec.Emit(OpCodes.Dup);
					LocalTemporary localTemporary = new LocalTemporary(this.ea.Expr.Type);
					localTemporary.Store(ec);
					this.ea.Expr = localTemporary;
				}
			}
			Arguments arguments = this.ea.Arguments.Emit(ec, duplicateArguments, prepareAwait);
			if (arguments != null)
			{
				this.ea.Arguments = arguments;
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00081A10 File Offset: 0x0007FC10
		public void Emit(EmitContext ec, bool leave_copy)
		{
			if (this.prepared)
			{
				ec.EmitLoadFromPtr(this.type);
			}
			else
			{
				if (this.has_await_args == null && ec.HasSet(BuilderContext.Options.AsyncBody) && this.ea.Arguments.ContainsEmitWithAwait())
				{
					this.LoadInstanceAndArguments(ec, false, true);
				}
				if (this.ConditionalAccessReceiver)
				{
					ec.ConditionalAccess = new ConditionalAccessContext(this.type, ec.DefineLabel());
				}
				ArrayContainer arrayContainer = (ArrayContainer)this.ea.Expr.Type;
				this.LoadInstanceAndArguments(ec, false, false);
				ec.EmitArrayLoad(arrayContainer);
				if (this.ConditionalAccessReceiver)
				{
					ec.CloseConditionalAccess((this.type.IsNullableType && this.type != arrayContainer.Element) ? this.type : null);
				}
			}
			if (leave_copy)
			{
				ec.Emit(OpCodes.Dup);
				this.temp = new LocalTemporary(this.type);
				this.temp.Store(ec);
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00081B0A File Offset: 0x0007FD0A
		public override void Emit(EmitContext ec)
		{
			this.Emit(ec, false);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00081B14 File Offset: 0x0007FD14
		public void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound)
		{
			ArrayContainer ac = (ArrayContainer)this.ea.Expr.Type;
			TypeSpec type = source.Type;
			this.has_await_args = new bool?(ec.HasSet(BuilderContext.Options.AsyncBody) && (this.ea.Arguments.ContainsEmitWithAwait() || source.ContainsEmitWithAwait()));
			if (type.IsStruct && ((isCompound && !(source is DynamicExpressionStatement)) || !BuiltinTypeSpec.IsPrimitiveType(type)))
			{
				this.LoadInstanceAndArguments(ec, false, this.has_await_args.Value);
				if (this.has_await_args.Value)
				{
					if (source.ContainsEmitWithAwait())
					{
						source = source.EmitToField(ec);
						isCompound = false;
						this.prepared = true;
					}
					this.LoadInstanceAndArguments(ec, isCompound, false);
				}
				else
				{
					this.prepared = true;
				}
				ec.EmitArrayAddress(ac);
				if (isCompound)
				{
					ec.Emit(OpCodes.Dup);
					this.prepared = true;
				}
			}
			else
			{
				this.LoadInstanceAndArguments(ec, isCompound, this.has_await_args.Value);
				if (this.has_await_args.Value)
				{
					if (source.ContainsEmitWithAwait())
					{
						source = source.EmitToField(ec);
					}
					this.LoadInstanceAndArguments(ec, false, false);
				}
			}
			source.Emit(ec);
			if (isCompound)
			{
				LocalTemporary localTemporary = this.ea.Expr as LocalTemporary;
				if (localTemporary != null)
				{
					localTemporary.Release(ec);
				}
			}
			if (leave_copy)
			{
				ec.Emit(OpCodes.Dup);
				this.temp = new LocalTemporary(this.type);
				this.temp.Store(ec);
			}
			if (this.prepared)
			{
				ec.EmitStoreFromPtr(type);
			}
			else
			{
				ec.EmitArrayStore(ac);
			}
			if (this.temp != null)
			{
				this.temp.Emit(ec);
				this.temp.Release(ec);
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00081CC1 File Offset: 0x0007FEC1
		public override Expression EmitToField(EmitContext ec)
		{
			this.ea.Expr = this.ea.Expr.EmitToField(ec);
			this.ea.Arguments = this.ea.Arguments.Emit(ec, false, true);
			return this;
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public Expression MakeAssignExpression(BuilderContext ctx, Expression source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00081CFE File Offset: 0x0007FEFE
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return Expression.ArrayIndex(this.ea.Expr.MakeExpression(ctx), this.MakeExpressionArguments(ctx));
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00081D20 File Offset: 0x0007FF20
		private Expression[] MakeExpressionArguments(BuilderContext ctx)
		{
			Expression[] result;
			using (ctx.With(BuilderContext.Options.CheckedScope, true))
			{
				result = Arguments.MakeExpression(this.ea.Arguments, ctx);
			}
			return result;
		}

		// Token: 0x040009EE RID: 2542
		private ElementAccess ea;

		// Token: 0x040009EF RID: 2543
		private LocalTemporary temp;

		// Token: 0x040009F0 RID: 2544
		private bool prepared;

		// Token: 0x040009F1 RID: 2545
		private bool? has_await_args;

		// Token: 0x040009F2 RID: 2546
		[CompilerGenerated]
		private bool <ConditionalAccess>k__BackingField;

		// Token: 0x040009F3 RID: 2547
		[CompilerGenerated]
		private bool <ConditionalAccessReceiver>k__BackingField;
	}
}
