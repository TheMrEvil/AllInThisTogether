using System;
using System.Collections.Generic;

namespace IKVM.Reflection.Writer
{
	// Token: 0x02000081 RID: 129
	internal sealed class UserStringHeap : SimpleHeap
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x00014888 File Offset: 0x00012A88
		internal UserStringHeap()
		{
			this.nextOffset = 1;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x000148AD File Offset: 0x00012AAD
		internal bool IsEmpty
		{
			get
			{
				return this.nextOffset == 1;
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000148B8 File Offset: 0x00012AB8
		internal int Add(string str)
		{
			int num;
			if (!this.strings.TryGetValue(str, out num))
			{
				int num2 = str.Length * 2 + 1 + MetadataWriter.GetCompressedUIntLength(str.Length * 2 + 1);
				if (this.nextOffset + num2 > 16777215)
				{
					throw new FileFormatLimitationExceededException("No logical space left to create more user strings.", -2146233960);
				}
				num = this.nextOffset;
				this.nextOffset += num2;
				this.list.Add(str);
				this.strings.Add(str, num);
			}
			return num;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001493E File Offset: 0x00012B3E
		protected override int GetLength()
		{
			return this.nextOffset;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00014948 File Offset: 0x00012B48
		protected override void WriteImpl(MetadataWriter mw)
		{
			mw.Write(0);
			foreach (string text in this.list)
			{
				mw.WriteCompressedUInt(text.Length * 2 + 1);
				byte b = 0;
				foreach (char c in text)
				{
					mw.Write((ushort)c);
					if (b == 0 && (c < ' ' || c > '~') && (c > '~' || (c >= '\u0001' && c <= '\b') || (c >= '\u000e' && c <= '\u001f') || c == '\'' || c == '-'))
					{
						b = 1;
					}
				}
				mw.Write(b);
			}
		}

		// Token: 0x04000289 RID: 649
		private List<string> list = new List<string>();

		// Token: 0x0400028A RID: 650
		private Dictionary<string, int> strings = new Dictionary<string, int>();

		// Token: 0x0400028B RID: 651
		private int nextOffset;
	}
}
