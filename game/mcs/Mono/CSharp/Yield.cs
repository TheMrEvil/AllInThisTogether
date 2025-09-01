using System;

namespace Mono.CSharp
{
	// Token: 0x0200022E RID: 558
	public class Yield : YieldStatement<Iterator>
	{
		// Token: 0x06001C3F RID: 7231 RVA: 0x000893DF File Offset: 0x000875DF
		public Yield(Expression expr, Location loc) : base(expr, loc)
		{
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x000893EC File Offset: 0x000875EC
		public static bool CheckContext(BlockContext bc, Location loc)
		{
			if (!bc.CurrentAnonymousMethod.IsIterator)
			{
				bc.Report.Error(1621, loc, "The yield statement cannot be used inside anonymous method blocks");
				return false;
			}
			if (bc.HasSet(ResolveContext.Options.FinallyScope))
			{
				bc.Report.Error(1625, loc, "Cannot yield in the body of a finally clause");
				return false;
			}
			return true;
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00089444 File Offset: 0x00087644
		public override bool Resolve(BlockContext bc)
		{
			if (!Yield.CheckContext(bc, this.loc))
			{
				return false;
			}
			if (bc.HasAny(ResolveContext.Options.TryWithCatchScope))
			{
				bc.Report.Error(1626, this.loc, "Cannot yield a value in the body of a try block with a catch clause");
			}
			if (bc.HasSet(ResolveContext.Options.CatchScope))
			{
				bc.Report.Error(1631, this.loc, "Cannot yield a value in the body of a catch clause");
			}
			if (!base.Resolve(bc))
			{
				return false;
			}
			TypeSpec originalIteratorType = bc.CurrentIterator.OriginalIteratorType;
			if (this.expr.Type != originalIteratorType)
			{
				this.expr = Convert.ImplicitConversionRequired(bc, this.expr, originalIteratorType, this.loc);
				if (this.expr == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000894F6 File Offset: 0x000876F6
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
