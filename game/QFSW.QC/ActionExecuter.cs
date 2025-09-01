using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace QFSW.QC
{
	// Token: 0x02000003 RID: 3
	public static class ActionExecuter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static ActionState Execute(this IEnumerator<ICommandAction> action, ActionContext context)
		{
			ActionExecuter.<>c__DisplayClass0_0 CS$<>8__locals1;
			CS$<>8__locals1.action = action;
			CS$<>8__locals1.context = context;
			CS$<>8__locals1.state = ActionState.Running;
			CS$<>8__locals1.idle = false;
			while (!CS$<>8__locals1.idle)
			{
				if (CS$<>8__locals1.action.Current == null)
				{
					ActionExecuter.<Execute>g__MoveNext|0_0(ref CS$<>8__locals1);
				}
				else if (CS$<>8__locals1.action.Current.IsFinished)
				{
					CS$<>8__locals1.action.Current.Finalize(CS$<>8__locals1.context);
					ActionExecuter.<Execute>g__MoveNext|0_0(ref CS$<>8__locals1);
				}
				else
				{
					CS$<>8__locals1.idle = true;
				}
			}
			return CS$<>8__locals1.state;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020DC File Offset: 0x000002DC
		[CompilerGenerated]
		internal static void <Execute>g__MoveNext|0_0(ref ActionExecuter.<>c__DisplayClass0_0 A_0)
		{
			if (A_0.action.MoveNext())
			{
				ICommandAction commandAction = A_0.action.Current;
				if (commandAction != null)
				{
					commandAction.Start(A_0.context);
				}
				ICommandAction commandAction2 = A_0.action.Current;
				A_0.idle = (commandAction2 != null && commandAction2.StartsIdle);
				return;
			}
			A_0.idle = true;
			A_0.state = ActionState.Complete;
			A_0.action.Dispose();
		}

		// Token: 0x02000080 RID: 128
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass0_0
		{
			// Token: 0x0400016A RID: 362
			public IEnumerator<ICommandAction> action;

			// Token: 0x0400016B RID: 363
			public ActionContext context;

			// Token: 0x0400016C RID: 364
			public bool idle;

			// Token: 0x0400016D RID: 365
			public ActionState state;
		}
	}
}
