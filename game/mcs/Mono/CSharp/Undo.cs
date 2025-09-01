using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000176 RID: 374
	public class Undo
	{
		// Token: 0x060011ED RID: 4589 RVA: 0x0004A668 File Offset: 0x00048868
		public void AddTypeContainer(TypeContainer current_container, TypeDefinition tc)
		{
			if (current_container == tc)
			{
				Console.Error.WriteLine("Internal error: inserting container into itself");
				return;
			}
			if (this.undo_actions == null)
			{
				this.undo_actions = new List<Action>();
			}
			if (current_container.Containers != null)
			{
				TypeContainer existing = current_container.Containers.FirstOrDefault((TypeContainer l) => l.MemberName.Basename == tc.MemberName.Basename);
				if (existing != null)
				{
					current_container.RemoveContainer(existing);
					this.undo_actions.Add(delegate
					{
						current_container.AddTypeContainer(existing);
					});
				}
			}
			this.undo_actions.Add(delegate
			{
				current_container.RemoveContainer(tc);
			});
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0004A74C File Offset: 0x0004894C
		public void ExecuteUndo()
		{
			if (this.undo_actions == null)
			{
				return;
			}
			foreach (Action action in this.undo_actions)
			{
				action();
			}
			this.undo_actions = null;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00002CCC File Offset: 0x00000ECC
		public Undo()
		{
		}

		// Token: 0x040007A6 RID: 1958
		private List<Action> undo_actions;

		// Token: 0x02000395 RID: 917
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x060026CA RID: 9930 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x060026CB RID: 9931 RVA: 0x000B70A4 File Offset: 0x000B52A4
			internal bool <AddTypeContainer>b__0(TypeContainer l)
			{
				return l.MemberName.Basename == this.tc.MemberName.Basename;
			}

			// Token: 0x060026CC RID: 9932 RVA: 0x000B70C6 File Offset: 0x000B52C6
			internal void <AddTypeContainer>b__2()
			{
				this.current_container.RemoveContainer(this.tc);
			}

			// Token: 0x04000F97 RID: 3991
			public TypeDefinition tc;

			// Token: 0x04000F98 RID: 3992
			public TypeContainer current_container;
		}

		// Token: 0x02000396 RID: 918
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_1
		{
			// Token: 0x060026CD RID: 9933 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass1_1()
			{
			}

			// Token: 0x060026CE RID: 9934 RVA: 0x000B70D9 File Offset: 0x000B52D9
			internal void <AddTypeContainer>b__1()
			{
				this.CS$<>8__locals1.current_container.AddTypeContainer(this.existing);
			}

			// Token: 0x04000F99 RID: 3993
			public TypeContainer existing;

			// Token: 0x04000F9A RID: 3994
			public Undo.<>c__DisplayClass1_0 CS$<>8__locals1;
		}
	}
}
