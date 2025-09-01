using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000245 RID: 581
	internal sealed class ClearDebugInfoExpression : DebugInfoExpression
	{
		// Token: 0x06000FD7 RID: 4055 RVA: 0x00035966 File Offset: 0x00033B66
		internal ClearDebugInfoExpression(SymbolDocumentInfo document) : base(document)
		{
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00007E1D File Offset: 0x0000601D
		public override bool IsClear
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0003596F File Offset: 0x00033B6F
		public override int StartLine
		{
			get
			{
				return 16707566;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x000023D1 File Offset: 0x000005D1
		public override int StartColumn
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0003596F File Offset: 0x00033B6F
		public override int EndLine
		{
			get
			{
				return 16707566;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x000023D1 File Offset: 0x000005D1
		public override int EndColumn
		{
			get
			{
				return 0;
			}
		}
	}
}
