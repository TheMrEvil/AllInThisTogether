using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001F7 RID: 503
	public class SizeOf : Expression
	{
		// Token: 0x06001A24 RID: 6692 RVA: 0x00080257 File Offset: 0x0007E457
		public SizeOf(Expression queried_type, Location l)
		{
			this.texpr = queried_type;
			this.loc = l;
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsSideEffectFree
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x0008026D File Offset: 0x0007E46D
		public Expression TypeExpression
		{
			get
			{
				return this.texpr;
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00075589 File Offset: 0x00073789
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			base.Error_PointerInsideExpressionTree(ec);
			return null;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x00080278 File Offset: 0x0007E478
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.type_queried = this.texpr.ResolveAsType(ec, false);
			if (this.type_queried == null)
			{
				return null;
			}
			if (this.type_queried.IsEnum)
			{
				this.type_queried = EnumSpec.GetUnderlyingType(this.type_queried);
			}
			int size = BuiltinTypeSpec.GetSize(this.type_queried);
			if (size > 0)
			{
				return new IntConstant(ec.BuiltinTypes, size, this.loc);
			}
			if (!TypeManager.VerifyUnmanaged(ec.Module, this.type_queried, this.loc))
			{
				return null;
			}
			if (!ec.IsUnsafe)
			{
				ec.Report.Error(233, this.loc, "`{0}' does not have a predefined size, therefore sizeof can only be used in an unsafe context (consider using System.Runtime.InteropServices.Marshal.SizeOf)", this.type_queried.GetSignatureForError());
			}
			this.type = ec.BuiltinTypes.Int;
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00080345 File Offset: 0x0007E545
		public override void Emit(EmitContext ec)
		{
			ec.Emit(OpCodes.Sizeof, this.type_queried);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00080358 File Offset: 0x0007E558
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009E4 RID: 2532
		private readonly Expression texpr;

		// Token: 0x040009E5 RID: 2533
		private TypeSpec type_queried;
	}
}
