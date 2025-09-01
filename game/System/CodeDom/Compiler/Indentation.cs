using System;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200034F RID: 847
	internal sealed class Indentation
	{
		// Token: 0x06001C0B RID: 7179 RVA: 0x0006688D File Offset: 0x00064A8D
		internal Indentation(ExposedTabStringIndentedTextWriter writer, int indent)
		{
			this._writer = writer;
			this._indent = indent;
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x000668A4 File Offset: 0x00064AA4
		internal string IndentationString
		{
			get
			{
				if (this._s == null)
				{
					string tabString = this._writer.TabString;
					switch (this._indent)
					{
					case 0:
						this._s = string.Empty;
						break;
					case 1:
						this._s = tabString;
						break;
					case 2:
						this._s = tabString + tabString;
						break;
					case 3:
						this._s = tabString + tabString + tabString;
						break;
					case 4:
						this._s = tabString + tabString + tabString + tabString;
						break;
					default:
					{
						string[] array = new string[this._indent];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = tabString;
						}
						return string.Concat(array);
					}
					}
				}
				return this._s;
			}
		}

		// Token: 0x04000E4D RID: 3661
		private readonly ExposedTabStringIndentedTextWriter _writer;

		// Token: 0x04000E4E RID: 3662
		private readonly int _indent;

		// Token: 0x04000E4F RID: 3663
		private string _s;
	}
}
