using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200013B RID: 315
	public class CompletionElementInitializer : CompletingExpression
	{
		// Token: 0x06000FDE RID: 4062 RVA: 0x0004137E File Offset: 0x0003F57E
		public CompletionElementInitializer(string partial_name, Location l)
		{
			this.partial_name = partial_name;
			this.loc = l;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x00041394 File Offset: 0x0003F594
		protected override Expression DoResolve(ResolveContext ec)
		{
			List<string> list = (from l in MemberCache.GetCompletitionMembers(ec, ec.CurrentInitializerVariable.Type, this.partial_name)
			where (l.Kind & (MemberKind.Field | MemberKind.Property)) > (MemberKind)0
			select l.Name).ToList<string>();
			if (this.partial_name != null)
			{
				List<string> list2 = new List<string>();
				CompletingExpression.AppendResults(list2, this.partial_name, list);
				list = list2;
			}
			throw new CompletionResult((this.partial_name == null) ? "" : this.partial_name, list.Distinct<string>().ToArray<string>());
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
		}

		// Token: 0x0400072F RID: 1839
		private string partial_name;

		// Token: 0x02000389 RID: 905
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060026BB RID: 9915 RVA: 0x000B6E87 File Offset: 0x000B5087
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060026BC RID: 9916 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c()
			{
			}

			// Token: 0x060026BD RID: 9917 RVA: 0x000B6E93 File Offset: 0x000B5093
			internal bool <DoResolve>b__2_0(MemberSpec l)
			{
				return (l.Kind & (MemberKind.Field | MemberKind.Property)) > (MemberKind)0;
			}

			// Token: 0x060026BE RID: 9918 RVA: 0x000B6E73 File Offset: 0x000B5073
			internal string <DoResolve>b__2_1(MemberSpec l)
			{
				return l.Name;
			}

			// Token: 0x04000F5C RID: 3932
			public static readonly CompletionElementInitializer.<>c <>9 = new CompletionElementInitializer.<>c();

			// Token: 0x04000F5D RID: 3933
			public static Func<MemberSpec, bool> <>9__2_0;

			// Token: 0x04000F5E RID: 3934
			public static Func<MemberSpec, string> <>9__2_1;
		}
	}
}
