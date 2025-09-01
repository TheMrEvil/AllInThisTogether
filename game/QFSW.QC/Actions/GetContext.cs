using System;

namespace QFSW.QC.Actions
{
	// Token: 0x02000072 RID: 114
	public class GetContext : ICommandAction
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000A6FC File Offset: 0x000088FC
		public bool IsFinished
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000A6FF File Offset: 0x000088FF
		public bool StartsIdle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A702 File Offset: 0x00008902
		public GetContext(Action<ActionContext> onContext)
		{
			this._onContext = onContext;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A711 File Offset: 0x00008911
		public void Start(ActionContext context)
		{
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A713 File Offset: 0x00008913
		public void Finalize(ActionContext context)
		{
			this._onContext(context);
		}

		// Token: 0x04000155 RID: 341
		private readonly Action<ActionContext> _onContext;
	}
}
