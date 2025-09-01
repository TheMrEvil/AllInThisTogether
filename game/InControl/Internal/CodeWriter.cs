using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InControl.Internal
{
	// Token: 0x02000076 RID: 118
	public class CodeWriter
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x00014ED3 File Offset: 0x000130D3
		public CodeWriter()
		{
			this.indent = 0;
			this.stringBuilder = new StringBuilder(4096);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00014EF2 File Offset: 0x000130F2
		public void IncreaseIndent()
		{
			this.indent++;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00014F02 File Offset: 0x00013102
		public void DecreaseIndent()
		{
			this.indent--;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00014F12 File Offset: 0x00013112
		public void Append(string code)
		{
			this.Append(false, code);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00014F1C File Offset: 0x0001311C
		public void Append(bool trim, string code)
		{
			if (trim)
			{
				code = code.Trim();
			}
			string[] array = Regex.Split(code, "\\r?\\n|\\n");
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				string text = array[i];
				if (!text.All(new Func<char, bool>(char.IsWhiteSpace)))
				{
					this.stringBuilder.Append('\t', this.indent);
					this.stringBuilder.Append(text);
				}
				if (i < num - 1)
				{
					this.stringBuilder.Append('\n');
				}
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00014F9D File Offset: 0x0001319D
		public void AppendLine(string code)
		{
			this.Append(code);
			this.stringBuilder.Append('\n');
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00014FB4 File Offset: 0x000131B4
		public void AppendLine(int count)
		{
			this.stringBuilder.Append('\n', count);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00014FC5 File Offset: 0x000131C5
		public void AppendFormat(string format, params object[] args)
		{
			this.Append(string.Format(format, args));
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00014FD4 File Offset: 0x000131D4
		public void AppendLineFormat(string format, params object[] args)
		{
			this.AppendLine(string.Format(format, args));
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00014FE3 File Offset: 0x000131E3
		public override string ToString()
		{
			return this.stringBuilder.ToString();
		}

		// Token: 0x0400042F RID: 1071
		private const char newLine = '\n';

		// Token: 0x04000430 RID: 1072
		private int indent;

		// Token: 0x04000431 RID: 1073
		private readonly StringBuilder stringBuilder;
	}
}
