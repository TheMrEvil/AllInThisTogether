using System;
using System.IO;

namespace System.Xml.Serialization
{
	// Token: 0x02000314 RID: 788
	internal class IndentedWriter
	{
		// Token: 0x060020AC RID: 8364 RVA: 0x000D16EE File Offset: 0x000CF8EE
		internal IndentedWriter(TextWriter writer, bool compact)
		{
			this.writer = writer;
			this.compact = compact;
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x000D1704 File Offset: 0x000CF904
		// (set) Token: 0x060020AE RID: 8366 RVA: 0x000D170C File Offset: 0x000CF90C
		internal int Indent
		{
			get
			{
				return this.indentLevel;
			}
			set
			{
				this.indentLevel = value;
			}
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000D1715 File Offset: 0x000CF915
		internal void Write(string s)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.Write(s);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000D1731 File Offset: 0x000CF931
		internal void Write(char c)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.Write(c);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x000D174D File Offset: 0x000CF94D
		internal void WriteLine(string s)
		{
			if (this.needIndent)
			{
				this.WriteIndent();
			}
			this.writer.WriteLine(s);
			this.needIndent = true;
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000D1770 File Offset: 0x000CF970
		internal void WriteLine()
		{
			this.writer.WriteLine();
			this.needIndent = true;
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000D1784 File Offset: 0x000CF984
		internal void WriteIndent()
		{
			this.needIndent = false;
			if (!this.compact)
			{
				for (int i = 0; i < this.indentLevel; i++)
				{
					this.writer.Write("    ");
				}
			}
		}

		// Token: 0x04001B3F RID: 6975
		private TextWriter writer;

		// Token: 0x04001B40 RID: 6976
		private bool needIndent;

		// Token: 0x04001B41 RID: 6977
		private int indentLevel;

		// Token: 0x04001B42 RID: 6978
		private bool compact;
	}
}
