using System;
using System.Linq.Expressions;

namespace Mono.CSharp.Nullable
{
	// Token: 0x020002FD RID: 765
	public class Unwrap : Expression, IMemoryLocation
	{
		// Token: 0x0600244D RID: 9293 RVA: 0x000ADD41 File Offset: 0x000ABF41
		public Unwrap(Expression expr, bool useDefaultValue = true)
		{
			this.expr = expr;
			this.loc = expr.Location;
			this.useDefaultValue = useDefaultValue;
			this.type = NullableInfo.GetUnderlyingType(expr.Type);
			this.eclass = expr.eclass;
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000ADD80 File Offset: 0x000ABF80
		public override bool ContainsEmitWithAwait()
		{
			return this.expr.ContainsEmitWithAwait();
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000ADD90 File Offset: 0x000ABF90
		public static Expression Create(Expression expr)
		{
			Wrap wrap = expr as Wrap;
			if (wrap != null)
			{
				return wrap.Child;
			}
			return Unwrap.Create(expr, false);
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000ADDB8 File Offset: 0x000ABFB8
		public static Expression CreateUnwrapped(Expression expr)
		{
			Wrap wrap = expr as Wrap;
			if (wrap != null)
			{
				return wrap.Child;
			}
			return Unwrap.Create(expr, true);
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x000ADDDD File Offset: 0x000ABFDD
		public static Unwrap Create(Expression expr, bool useDefaultValue)
		{
			return new Unwrap(expr, useDefaultValue);
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000ADDE6 File Offset: 0x000ABFE6
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			return this.expr.CreateExpressionTree(ec);
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x00005936 File Offset: 0x00003B36
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000ADDF4 File Offset: 0x000ABFF4
		public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
		{
			this.expr = this.expr.DoResolveLValue(ec, right_side);
			return this;
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000ADE0C File Offset: 0x000AC00C
		public override void Emit(EmitContext ec)
		{
			this.Store(ec);
			CallEmitter callEmitter = default(CallEmitter);
			callEmitter.InstanceExpression = this;
			if (this.useDefaultValue)
			{
				callEmitter.EmitPredefined(ec, NullableInfo.GetGetValueOrDefault(this.expr.Type), null, false, null);
				return;
			}
			callEmitter.EmitPredefined(ec, NullableInfo.GetValue(this.expr.Type), null, false, null);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000ADE80 File Offset: 0x000AC080
		public void EmitCheck(EmitContext ec)
		{
			this.Store(ec);
			CallEmitter callEmitter = default(CallEmitter);
			callEmitter.InstanceExpression = this;
			callEmitter.EmitPredefined(ec, NullableInfo.GetHasValue(this.expr.Type), null, false, null);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000ADEC7 File Offset: 0x000AC0C7
		public override void EmitSideEffect(EmitContext ec)
		{
			this.expr.EmitSideEffect(ec);
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000ADED5 File Offset: 0x000AC0D5
		public override Expression EmitToField(EmitContext ec)
		{
			if (this.temp_field == null)
			{
				this.temp_field = this.expr.EmitToField(ec);
			}
			return this;
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000ADEF4 File Offset: 0x000AC0F4
		public override bool Equals(object obj)
		{
			Unwrap unwrap = obj as Unwrap;
			return unwrap != null && this.expr.Equals(unwrap.expr);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000ADF1E File Offset: 0x000AC11E
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.expr.FlowAnalysis(fc);
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x0600245B RID: 9307 RVA: 0x000ADF2C File Offset: 0x000AC12C
		public Expression Original
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000ADF34 File Offset: 0x000AC134
		public override int GetHashCode()
		{
			return this.expr.GetHashCode();
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x0600245D RID: 9309 RVA: 0x000ADF41 File Offset: 0x000AC141
		public override bool IsNull
		{
			get
			{
				return this.expr.IsNull;
			}
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000ADF4E File Offset: 0x000AC14E
		public void Store(EmitContext ec)
		{
			if (this.temp != null || this.temp_field != null)
			{
				return;
			}
			if (this.expr is VariableReference)
			{
				return;
			}
			this.expr.Emit(ec);
			this.LocalVariable.Store(ec);
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000ADF87 File Offset: 0x000AC187
		public void Load(EmitContext ec)
		{
			if (this.temp_field != null)
			{
				this.temp_field.Emit(ec);
				return;
			}
			if (this.expr is VariableReference)
			{
				this.expr.Emit(ec);
				return;
			}
			this.LocalVariable.Emit(ec);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000ADFC4 File Offset: 0x000AC1C4
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return this.expr.MakeExpression(ctx);
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000ADFD4 File Offset: 0x000AC1D4
		public void AddressOf(EmitContext ec, AddressOp mode)
		{
			IMemoryLocation memoryLocation;
			if (this.temp_field != null)
			{
				memoryLocation = (this.temp_field as IMemoryLocation);
				if (memoryLocation == null)
				{
					LocalTemporary localTemporary = new LocalTemporary(this.temp_field.Type);
					this.temp_field.Emit(ec);
					localTemporary.Store(ec);
					memoryLocation = localTemporary;
				}
			}
			else
			{
				memoryLocation = (this.expr as VariableReference);
			}
			if (memoryLocation != null)
			{
				memoryLocation.AddressOf(ec, mode);
				return;
			}
			this.LocalVariable.AddressOf(ec, mode);
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x000AE043 File Offset: 0x000AC243
		private LocalTemporary LocalVariable
		{
			get
			{
				if (this.temp == null && this.temp_field == null)
				{
					this.temp = new LocalTemporary(this.expr.Type);
				}
				return this.temp;
			}
		}

		// Token: 0x04000D8C RID: 3468
		private Expression expr;

		// Token: 0x04000D8D RID: 3469
		private LocalTemporary temp;

		// Token: 0x04000D8E RID: 3470
		private Expression temp_field;

		// Token: 0x04000D8F RID: 3471
		private readonly bool useDefaultValue;
	}
}
