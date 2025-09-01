using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000161 RID: 353
	public class RuntimeValueExpression : Expression, IDynamicAssign, IAssignMethod, IMemoryLocation
	{
		// Token: 0x0600116F RID: 4463 RVA: 0x00047D8C File Offset: 0x00045F8C
		public RuntimeValueExpression(RuntimeValueExpression.DynamicMetaObject obj, TypeSpec type)
		{
			this.obj = obj;
			this.type = type;
			this.eclass = ExprClass.Variable;
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x00047DA9 File Offset: 0x00045FA9
		// (set) Token: 0x06001171 RID: 4465 RVA: 0x00047DB1 File Offset: 0x00045FB1
		public bool IsSuggestionOnly
		{
			[CompilerGenerated]
			get
			{
				return this.<IsSuggestionOnly>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsSuggestionOnly>k__BackingField = value;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x00047DBA File Offset: 0x00045FBA
		public RuntimeValueExpression.DynamicMetaObject MetaObject
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public void AddressOf(EmitContext ec, AddressOp mode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0000225C File Offset: 0x0000045C
		public override bool ContainsEmitWithAwait()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0000225C File Offset: 0x0000045C
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00005936 File Offset: 0x00003B36
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00005936 File Offset: 0x00003B36
		public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
		{
			return this;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override void Emit(EmitContext ec)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public void Emit(EmitContext ec, bool leave_copy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public Expression MakeAssignExpression(BuilderContext ctx, Expression source)
		{
			return this.obj.Expression;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00047DCF File Offset: 0x00045FCF
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return Expression.Convert(this.obj.Expression, this.type.GetMetaInfo());
		}

		// Token: 0x0400077D RID: 1917
		private readonly RuntimeValueExpression.DynamicMetaObject obj;

		// Token: 0x0400077E RID: 1918
		[CompilerGenerated]
		private bool <IsSuggestionOnly>k__BackingField;

		// Token: 0x0200038F RID: 911
		public class DynamicMetaObject
		{
			// Token: 0x060026C5 RID: 9925 RVA: 0x00002CCC File Offset: 0x00000ECC
			public DynamicMetaObject()
			{
			}

			// Token: 0x04000F88 RID: 3976
			public TypeSpec RuntimeType;

			// Token: 0x04000F89 RID: 3977
			public TypeSpec LimitType;

			// Token: 0x04000F8A RID: 3978
			public Expression Expression;
		}
	}
}
