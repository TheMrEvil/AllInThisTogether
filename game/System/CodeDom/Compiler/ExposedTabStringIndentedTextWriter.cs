using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200034E RID: 846
	internal sealed class ExposedTabStringIndentedTextWriter : IndentedTextWriter
	{
		// Token: 0x06001C08 RID: 7176 RVA: 0x0006683A File Offset: 0x00064A3A
		public ExposedTabStringIndentedTextWriter(TextWriter writer, string tabString) : base(writer, tabString)
		{
			this.TabString = (tabString ?? "    ");
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00066854 File Offset: 0x00064A54
		internal void InternalOutputTabs()
		{
			TextWriter innerWriter = base.InnerWriter;
			for (int i = 0; i < base.Indent; i++)
			{
				innerWriter.Write(this.TabString);
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x00066885 File Offset: 0x00064A85
		internal string TabString
		{
			[CompilerGenerated]
			get
			{
				return this.<TabString>k__BackingField;
			}
		}

		// Token: 0x04000E4C RID: 3660
		[CompilerGenerated]
		private readonly string <TabString>k__BackingField;
	}
}
