using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000108 RID: 264
	public abstract class HoistedVariable
	{
		// Token: 0x06000D39 RID: 3385 RVA: 0x000302F7 File Offset: 0x0002E4F7
		protected HoistedVariable(AnonymousMethodStorey storey, string name, TypeSpec type) : this(storey, storey.AddCapturedVariable(name, type))
		{
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00030308 File Offset: 0x0002E508
		protected HoistedVariable(AnonymousMethodStorey storey, Field field)
		{
			this.storey = storey;
			this.field = field;
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0003031E File Offset: 0x0002E51E
		public Field Field
		{
			get
			{
				return this.field;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00030326 File Offset: 0x0002E526
		public AnonymousMethodStorey Storey
		{
			get
			{
				return this.storey;
			}
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0003032E File Offset: 0x0002E52E
		public void AddressOf(EmitContext ec, AddressOp mode)
		{
			this.GetFieldExpression(ec).AddressOf(ec, mode);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0003033E File Offset: 0x0002E53E
		public Expression CreateExpressionTree()
		{
			return new HoistedVariable.ExpressionTreeVariableReference(this);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00030346 File Offset: 0x0002E546
		public void Emit(EmitContext ec)
		{
			this.GetFieldExpression(ec).Emit(ec);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00030355 File Offset: 0x0002E555
		public Expression EmitToField(EmitContext ec)
		{
			return this.GetFieldExpression(ec);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00030360 File Offset: 0x0002E560
		protected virtual FieldExpr GetFieldExpression(EmitContext ec)
		{
			if (ec.CurrentAnonymousMethod != null && ec.CurrentAnonymousMethod.Storey != null)
			{
				FieldExpr fieldExpr;
				if (this.cached_inner_access != null)
				{
					if (!this.cached_inner_access.TryGetValue(ec.CurrentAnonymousMethod, out fieldExpr))
					{
						fieldExpr = null;
					}
				}
				else
				{
					fieldExpr = null;
					this.cached_inner_access = new Dictionary<AnonymousExpression, FieldExpr>(4);
				}
				if (fieldExpr == null)
				{
					if (this.field.Parent.IsGenericOrParentIsGeneric)
					{
						fieldExpr = new FieldExpr(MemberCache.GetMember<FieldSpec>(this.field.Parent.CurrentType, this.field.Spec), this.field.Location);
					}
					else
					{
						fieldExpr = new FieldExpr(this.field, this.field.Location);
					}
					fieldExpr.InstanceExpression = this.storey.GetStoreyInstanceExpression(ec);
					this.cached_inner_access.Add(ec.CurrentAnonymousMethod, fieldExpr);
				}
				return fieldExpr;
			}
			if (this.cached_outer_access != null)
			{
				return this.cached_outer_access;
			}
			if (this.storey.Instance.Type.IsGenericOrParentIsGeneric)
			{
				FieldSpec member = MemberCache.GetMember<FieldSpec>(this.storey.Instance.Type, this.field.Spec);
				this.cached_outer_access = new FieldExpr(member, this.field.Location);
			}
			else
			{
				this.cached_outer_access = new FieldExpr(this.field, this.field.Location);
			}
			this.cached_outer_access.InstanceExpression = this.storey.GetStoreyInstanceExpression(ec);
			return this.cached_outer_access;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000304D5 File Offset: 0x0002E6D5
		public void Emit(EmitContext ec, bool leave_copy)
		{
			this.GetFieldExpression(ec).Emit(ec, leave_copy);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x000304E5 File Offset: 0x0002E6E5
		public void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound)
		{
			this.GetFieldExpression(ec).EmitAssign(ec, source, leave_copy, false);
		}

		// Token: 0x0400064C RID: 1612
		protected readonly AnonymousMethodStorey storey;

		// Token: 0x0400064D RID: 1613
		protected Field field;

		// Token: 0x0400064E RID: 1614
		private Dictionary<AnonymousExpression, FieldExpr> cached_inner_access;

		// Token: 0x0400064F RID: 1615
		private FieldExpr cached_outer_access;

		// Token: 0x0200037C RID: 892
		private class ExpressionTreeVariableReference : Expression
		{
			// Token: 0x06002689 RID: 9865 RVA: 0x000B69A0 File Offset: 0x000B4BA0
			public ExpressionTreeVariableReference(HoistedVariable hv)
			{
				this.hv = hv;
			}

			// Token: 0x0600268A RID: 9866 RVA: 0x000022F4 File Offset: 0x000004F4
			public override bool ContainsEmitWithAwait()
			{
				return false;
			}

			// Token: 0x0600268B RID: 9867 RVA: 0x000B69AF File Offset: 0x000B4BAF
			public override Expression CreateExpressionTree(ResolveContext ec)
			{
				return this.hv.CreateExpressionTree();
			}

			// Token: 0x0600268C RID: 9868 RVA: 0x000B69BC File Offset: 0x000B4BBC
			protected override Expression DoResolve(ResolveContext ec)
			{
				this.eclass = ExprClass.Value;
				this.type = ec.Module.PredefinedTypes.Expression.Resolve();
				return this;
			}

			// Token: 0x0600268D RID: 9869 RVA: 0x000B69E4 File Offset: 0x000B4BE4
			public override void Emit(EmitContext ec)
			{
				ResolveContext resolveContext = new ResolveContext(ec.MemberContext);
				Expression expression = this.hv.GetFieldExpression(ec).CreateExpressionTree(resolveContext, false);
				expression = expression.Resolve(resolveContext);
				if (expression != null)
				{
					expression.Emit(ec);
				}
			}

			// Token: 0x04000F48 RID: 3912
			private readonly HoistedVariable hv;
		}
	}
}
