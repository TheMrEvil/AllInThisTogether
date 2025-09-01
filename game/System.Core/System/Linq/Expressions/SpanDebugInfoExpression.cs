using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000244 RID: 580
	internal sealed class SpanDebugInfoExpression : DebugInfoExpression
	{
		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003591F File Offset: 0x00033B1F
		internal SpanDebugInfoExpression(SymbolDocumentInfo document, int startLine, int startColumn, int endLine, int endColumn) : base(document)
		{
			this._startLine = startLine;
			this._startColumn = startColumn;
			this._endLine = endLine;
			this._endColumn = endColumn;
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00035946 File Offset: 0x00033B46
		public override int StartLine
		{
			get
			{
				return this._startLine;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0003594E File Offset: 0x00033B4E
		public override int StartColumn
		{
			get
			{
				return this._startColumn;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00035956 File Offset: 0x00033B56
		public override int EndLine
		{
			get
			{
				return this._endLine;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0003595E File Offset: 0x00033B5E
		public override int EndColumn
		{
			get
			{
				return this._endColumn;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x000023D1 File Offset: 0x000005D1
		public override bool IsClear
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00035916 File Offset: 0x00033B16
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitDebugInfo(this);
		}

		// Token: 0x0400096A RID: 2410
		private readonly int _startLine;

		// Token: 0x0400096B RID: 2411
		private readonly int _startColumn;

		// Token: 0x0400096C RID: 2412
		private readonly int _endLine;

		// Token: 0x0400096D RID: 2413
		private readonly int _endColumn;
	}
}
