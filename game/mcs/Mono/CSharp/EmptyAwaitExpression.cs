using System;

namespace Mono.CSharp
{
	// Token: 0x02000202 RID: 514
	internal sealed class EmptyAwaitExpression : EmptyExpression
	{
		// Token: 0x06001AB0 RID: 6832 RVA: 0x00082415 File Offset: 0x00080615
		public EmptyAwaitExpression(TypeSpec type) : base(type)
		{
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool ContainsEmitWithAwait()
		{
			return true;
		}
	}
}
