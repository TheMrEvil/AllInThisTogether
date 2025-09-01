using System;

namespace QFSW.QC.Suggestors
{
	// Token: 0x02000005 RID: 5
	public struct CollapsedCommand
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000025DF File Offset: 0x000007DF
		public CollapsedCommand(CommandData command)
		{
			this.Command = command;
			this.NumOptionalParams = 0;
		}

		// Token: 0x0400000A RID: 10
		public CommandData Command;

		// Token: 0x0400000B RID: 11
		public int NumOptionalParams;
	}
}
