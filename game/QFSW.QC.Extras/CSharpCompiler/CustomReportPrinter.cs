using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Mono.CSharp;

namespace CSharpCompiler
{
	// Token: 0x02000005 RID: 5
	public class CustomReportPrinter : ReportPrinter
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002AE9 File Offset: 0x00000CE9
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002AF1 File Offset: 0x00000CF1
		public new int ErrorsCount
		{
			[CompilerGenerated]
			get
			{
				return this.<ErrorsCount>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ErrorsCount>k__BackingField = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002AFA File Offset: 0x00000CFA
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002B02 File Offset: 0x00000D02
		public new int WarningsCount
		{
			[CompilerGenerated]
			get
			{
				return this.<WarningsCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<WarningsCount>k__BackingField = value;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B0B File Offset: 0x00000D0B
		public CustomReportPrinter(CompilerResults compilerResults)
		{
			this.compilerResults = compilerResults;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B1C File Offset: 0x00000D1C
		public override void Print(AbstractMessage msg, bool showFullPath)
		{
			if (msg.IsWarning)
			{
				int num = this.WarningsCount + 1;
				this.WarningsCount = num;
			}
			else
			{
				int num = this.ErrorsCount + 1;
				this.ErrorsCount = num;
			}
			this.compilerResults.Errors.Add(new CompilerError
			{
				IsWarning = msg.IsWarning,
				Column = msg.Location.Column,
				Line = msg.Location.Row,
				ErrorNumber = msg.Code.ToString(),
				ErrorText = msg.Text,
				FileName = (showFullPath ? msg.Location.SourceFile.FullPathName : msg.Location.SourceFile.Name)
			});
		}

		// Token: 0x04000005 RID: 5
		private readonly CompilerResults compilerResults;

		// Token: 0x04000006 RID: 6
		[CompilerGenerated]
		private int <ErrorsCount>k__BackingField;

		// Token: 0x04000007 RID: 7
		[CompilerGenerated]
		private int <WarningsCount>k__BackingField;
	}
}
