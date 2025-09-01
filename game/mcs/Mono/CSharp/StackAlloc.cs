using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x0200020B RID: 523
	public class StackAlloc : Expression
	{
		// Token: 0x06001AE6 RID: 6886 RVA: 0x00082A21 File Offset: 0x00080C21
		public StackAlloc(Expression type, Expression count, Location l)
		{
			this.texpr = type;
			this.count = count;
			this.loc = l;
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x00082A3E File Offset: 0x00080C3E
		public Expression TypeExpression
		{
			get
			{
				return this.texpr;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x00082A46 File Offset: 0x00080C46
		public Expression CountExpression
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0006D4AD File Offset: 0x0006B6AD
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotSupportedException("ET");
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00082A50 File Offset: 0x00080C50
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.count = this.count.Resolve(ec);
			if (this.count == null)
			{
				return null;
			}
			if (this.count.Type.BuiltinType != BuiltinTypeSpec.Type.UInt)
			{
				this.count = Convert.ImplicitConversionRequired(ec, this.count, ec.BuiltinTypes.Int, this.loc);
				if (this.count == null)
				{
					return null;
				}
			}
			Constant constant = this.count as Constant;
			if (constant != null && constant.IsNegative)
			{
				ec.Report.Error(247, this.loc, "Cannot use a negative size with stackalloc");
			}
			if (ec.HasAny(ResolveContext.Options.CatchScope | ResolveContext.Options.FinallyScope))
			{
				ec.Report.Error(255, this.loc, "Cannot use stackalloc in finally or catch");
			}
			this.otype = this.texpr.ResolveAsType(ec, false);
			if (this.otype == null)
			{
				return null;
			}
			if (!TypeManager.VerifyUnmanaged(ec.Module, this.otype, this.loc))
			{
				return null;
			}
			this.type = PointerContainer.MakeType(ec.Module, this.otype);
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00082B68 File Offset: 0x00080D68
		public override void Emit(EmitContext ec)
		{
			int size = BuiltinTypeSpec.GetSize(this.otype);
			this.count.Emit(ec);
			if (size == 0)
			{
				ec.Emit(OpCodes.Sizeof, this.otype);
			}
			else
			{
				ec.EmitInt(size);
			}
			ec.Emit(OpCodes.Mul_Ovf_Un);
			ec.Emit(OpCodes.Localloc);
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00082BC0 File Offset: 0x00080DC0
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			StackAlloc stackAlloc = (StackAlloc)t;
			stackAlloc.count = this.count.Clone(clonectx);
			stackAlloc.texpr = this.texpr.Clone(clonectx);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00082BEB File Offset: 0x00080DEB
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000A0A RID: 2570
		private TypeSpec otype;

		// Token: 0x04000A0B RID: 2571
		private Expression texpr;

		// Token: 0x04000A0C RID: 2572
		private Expression count;
	}
}
