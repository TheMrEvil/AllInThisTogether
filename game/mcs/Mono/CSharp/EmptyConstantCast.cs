using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001A9 RID: 425
	public class EmptyConstantCast : Constant
	{
		// Token: 0x06001690 RID: 5776 RVA: 0x0006C4BF File Offset: 0x0006A6BF
		public EmptyConstantCast(Constant child, TypeSpec type) : base(child.Location)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			this.child = child;
			this.eclass = child.eclass;
			this.type = type;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0006C4F5 File Offset: 0x0006A6F5
		public override Constant ConvertExplicitly(bool in_checked_context, TypeSpec target_type)
		{
			if (this.child.Type == target_type)
			{
				return this.child;
			}
			return this.child.ConvertExplicitly(in_checked_context, target_type);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0006C51C File Offset: 0x0006A71C
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments args = Arguments.CreateForExpressionTree(ec, null, new Expression[]
			{
				this.child.CreateExpressionTree(ec),
				new TypeOf(this.type, this.loc)
			});
			if (this.type.IsPointer)
			{
				base.Error_PointerInsideExpressionTree(ec);
			}
			return base.CreateExpressionFactoryCall(ec, "Convert", args);
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0006C57B File Offset: 0x0006A77B
		public override bool IsDefaultValue
		{
			get
			{
				return this.child.IsDefaultValue;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x0006C588 File Offset: 0x0006A788
		public override bool IsNegative
		{
			get
			{
				return this.child.IsNegative;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x0006C595 File Offset: 0x0006A795
		public override bool IsNull
		{
			get
			{
				return this.child.IsNull;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x0006C5A2 File Offset: 0x0006A7A2
		public override bool IsOneInteger
		{
			get
			{
				return this.child.IsOneInteger;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0006C5AF File Offset: 0x0006A7AF
		public override bool IsSideEffectFree
		{
			get
			{
				return this.child.IsSideEffectFree;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x0006C5BC File Offset: 0x0006A7BC
		public override bool IsZeroInteger
		{
			get
			{
				return this.child.IsZeroInteger;
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0006C5C9 File Offset: 0x0006A7C9
		public override void Emit(EmitContext ec)
		{
			this.child.Emit(ec);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0006C5D7 File Offset: 0x0006A7D7
		public override void EmitBranchable(EmitContext ec, Label label, bool on_true)
		{
			this.child.EmitBranchable(ec, label, on_true);
			if (TypeManager.IsGenericParameter(this.type) && this.child.IsNull)
			{
				ec.Emit(OpCodes.Unbox_Any, this.type);
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0006C612 File Offset: 0x0006A812
		public override void EmitSideEffect(EmitContext ec)
		{
			this.child.EmitSideEffect(ec);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0006C620 File Offset: 0x0006A820
		public override object GetValue()
		{
			return this.child.GetValue();
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0006C62D File Offset: 0x0006A82D
		public override string GetValueAsLiteral()
		{
			return this.child.GetValueAsLiteral();
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0006C63A File Offset: 0x0006A83A
		public override long GetValueAsLong()
		{
			return this.child.GetValueAsLong();
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0006C647 File Offset: 0x0006A847
		public override Constant ConvertImplicitly(TypeSpec target_type)
		{
			if (this.type == target_type)
			{
				return this;
			}
			if (!Convert.ImplicitStandardConversionExists(this, target_type))
			{
				return null;
			}
			return this.child.ConvertImplicitly(target_type);
		}

		// Token: 0x04000955 RID: 2389
		public readonly Constant child;
	}
}
