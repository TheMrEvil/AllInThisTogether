using System;

namespace Mono.CSharp
{
	// Token: 0x020002AF RID: 687
	public interface INamedBlockVariable
	{
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060020E6 RID: 8422
		Block Block { get; }

		// Token: 0x060020E7 RID: 8423
		Expression CreateReferenceExpression(ResolveContext rc, Location loc);

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060020E8 RID: 8424
		bool IsDeclared { get; }

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060020E9 RID: 8425
		bool IsParameter { get; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060020EA RID: 8426
		Location Location { get; }
	}
}
