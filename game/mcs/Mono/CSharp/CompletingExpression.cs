using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000138 RID: 312
	public abstract class CompletingExpression : ExpressionStatement
	{
		// Token: 0x06000FCF RID: 4047 RVA: 0x00041000 File Offset: 0x0003F200
		public static void AppendResults(List<string> results, string prefix, IEnumerable<string> names)
		{
			foreach (string text in names)
			{
				if (text != null && (prefix == null || text.StartsWith(prefix)) && !results.Contains(text))
				{
					if (prefix != null)
					{
						results.Add(text.Substring(prefix.Length));
					}
					else
					{
						results.Add(text);
					}
				}
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000055E7 File Offset: 0x000037E7
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			return null;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void EmitStatement(EmitContext ec)
		{
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0000AF70 File Offset: 0x00009170
		public override void Emit(EmitContext ec)
		{
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00041078 File Offset: 0x0003F278
		protected CompletingExpression()
		{
		}
	}
}
