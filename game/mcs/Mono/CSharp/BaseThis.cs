using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x02000200 RID: 512
	public class BaseThis : This
	{
		// Token: 0x06001A9E RID: 6814 RVA: 0x00082257 File Offset: 0x00080457
		public BaseThis(Location loc) : base(loc)
		{
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00082260 File Offset: 0x00080460
		public BaseThis(TypeSpec type, Location loc) : base(loc)
		{
			this.type = type;
			this.eclass = ExprClass.Variable;
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x00082277 File Offset: 0x00080477
		public override string Name
		{
			get
			{
				return "base";
			}
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0008227E File Offset: 0x0008047E
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			ec.Report.Error(831, this.loc, "An expression tree may not contain a base access");
			return base.CreateExpressionTree(ec);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x000822A4 File Offset: 0x000804A4
		public override void Emit(EmitContext ec)
		{
			base.Emit(ec);
			if (this.type == ec.Module.Compiler.BuiltinTypes.ValueType)
			{
				TypeSpec currentType = ec.CurrentType;
				ec.Emit(OpCodes.Ldobj, currentType);
				ec.Emit(OpCodes.Box, currentType);
			}
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000822F4 File Offset: 0x000804F4
		protected override void Error_ThisNotAvailable(ResolveContext ec)
		{
			if (ec.IsStatic)
			{
				ec.Report.Error(1511, this.loc, "Keyword `base' is not available in a static method");
				return;
			}
			ec.Report.Error(1512, this.loc, "Keyword `base' is not available in the current context");
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00082340 File Offset: 0x00080540
		public override void ResolveBase(ResolveContext ec)
		{
			base.ResolveBase(ec);
			this.type = ec.CurrentType.BaseType;
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0008235A File Offset: 0x0008055A
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
