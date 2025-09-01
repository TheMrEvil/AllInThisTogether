using System;

namespace Mono.CSharp
{
	// Token: 0x020000FD RID: 253
	public class NamedArgument : MovableArgument
	{
		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002CE5A File Offset: 0x0002B05A
		public NamedArgument(string name, Location loc, Expression expr) : this(name, loc, expr, Argument.AType.None)
		{
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002CE66 File Offset: 0x0002B066
		public NamedArgument(string name, Location loc, Expression expr, Argument.AType modifier) : base(expr, modifier)
		{
			this.Name = name;
			this.loc = loc;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002CE7F File Offset: 0x0002B07F
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			ec.Report.Error(853, this.loc, "An expression tree cannot contain named argument");
			return base.CreateExpressionTree(ec);
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0002CEA3 File Offset: 0x0002B0A3
		public Location Location
		{
			get
			{
				return this.loc;
			}
		}

		// Token: 0x04000618 RID: 1560
		public readonly string Name;

		// Token: 0x04000619 RID: 1561
		private readonly Location loc;
	}
}
