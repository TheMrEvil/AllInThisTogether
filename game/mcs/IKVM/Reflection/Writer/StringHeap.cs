using System;
using System.Collections.Generic;
using System.Text;

namespace IKVM.Reflection.Writer
{
	// Token: 0x02000080 RID: 128
	internal sealed class StringHeap : SimpleHeap
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x00014733 File Offset: 0x00012933
		internal StringHeap()
		{
			this.Add("");
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00014760 File Offset: 0x00012960
		internal int Add(string str)
		{
			int num;
			if (!this.strings.TryGetValue(str, out num))
			{
				num = this.nextOffset;
				this.nextOffset += Encoding.UTF8.GetByteCount(str) + 1;
				this.list.Add(str);
				this.strings.Add(str, num);
			}
			return num;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000147B8 File Offset: 0x000129B8
		internal string Find(int index)
		{
			foreach (KeyValuePair<string, int> keyValuePair in this.strings)
			{
				if (keyValuePair.Value == index)
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001481C File Offset: 0x00012A1C
		protected override int GetLength()
		{
			return this.nextOffset;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00014824 File Offset: 0x00012A24
		protected override void WriteImpl(MetadataWriter mw)
		{
			foreach (string s in this.list)
			{
				mw.Write(Encoding.UTF8.GetBytes(s));
				mw.Write(0);
			}
		}

		// Token: 0x04000286 RID: 646
		private List<string> list = new List<string>();

		// Token: 0x04000287 RID: 647
		private Dictionary<string, int> strings = new Dictionary<string, int>();

		// Token: 0x04000288 RID: 648
		private int nextOffset;
	}
}
