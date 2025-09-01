using System;

namespace Mono.CSharp
{
	// Token: 0x02000114 RID: 276
	public class RuntimeExplicitAssign : Assign
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x0003288F File Offset: 0x00030A8F
		public RuntimeExplicitAssign(Expression target, Expression source) : base(target, source, target.Location)
		{
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003289F File Offset: 0x00030A9F
		protected override Expression ResolveConversions(ResolveContext ec)
		{
			this.source = EmptyCast.Create(this.source, this.target.Type);
			return this;
		}
	}
}
