using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000139 RID: 313
	public class CompletionSimpleName : CompletingExpression
	{
		// Token: 0x06000FD5 RID: 4053 RVA: 0x00041080 File Offset: 0x0003F280
		public CompletionSimpleName(string prefix, Location l)
		{
			this.loc = l;
			this.Prefix = prefix;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00041098 File Offset: 0x0003F298
		protected override Expression DoResolve(ResolveContext ec)
		{
			List<string> list = new List<string>();
			ec.CurrentMemberDefinition.GetCompletionStartingWith(this.Prefix, list);
			throw new CompletionResult(this.Prefix, (from l in list.Distinct<string>()
			select l.Substring(this.Prefix.Length)).ToArray<string>());
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000410E4 File Offset: 0x0003F2E4
		[CompilerGenerated]
		private string <DoResolve>b__2_0(string l)
		{
			return l.Substring(this.Prefix.Length);
		}

		// Token: 0x0400072B RID: 1835
		public string Prefix;
	}
}
