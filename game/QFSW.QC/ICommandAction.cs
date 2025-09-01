using System;

namespace QFSW.QC
{
	// Token: 0x02000005 RID: 5
	public interface ICommandAction
	{
		// Token: 0x06000003 RID: 3
		void Start(ActionContext context);

		// Token: 0x06000004 RID: 4
		void Finalize(ActionContext context);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5
		bool IsFinished { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6
		bool StartsIdle { get; }
	}
}
