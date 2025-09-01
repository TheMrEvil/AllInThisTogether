using System;
using System.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x02000255 RID: 597
	internal struct UIDocumentHierarchicalIndex : IComparable<UIDocumentHierarchicalIndex>
	{
		// Token: 0x0600121A RID: 4634 RVA: 0x000476D8 File Offset: 0x000458D8
		public int CompareTo(UIDocumentHierarchicalIndex other)
		{
			bool flag = this.pathToParent == null;
			int result;
			if (flag)
			{
				bool flag2 = other.pathToParent == null;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					result = 1;
				}
			}
			else
			{
				bool flag3 = other.pathToParent == null;
				if (flag3)
				{
					result = -1;
				}
				else
				{
					int num = this.pathToParent.Length;
					int num2 = other.pathToParent.Length;
					int num3 = 0;
					while (num3 < num && num3 < num2)
					{
						bool flag4 = this.pathToParent[num3] < other.pathToParent[num3];
						if (flag4)
						{
							return -1;
						}
						bool flag5 = this.pathToParent[num3] > other.pathToParent[num3];
						if (flag5)
						{
							return 1;
						}
						num3++;
					}
					bool flag6 = num > num2;
					if (flag6)
					{
						result = 1;
					}
					else
					{
						bool flag7 = num < num2;
						if (flag7)
						{
							result = -1;
						}
						else
						{
							result = 0;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000477C4 File Offset: 0x000459C4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("pathToParent = [");
			bool flag = this.pathToParent != null;
			if (flag)
			{
				int num = this.pathToParent.Length;
				for (int i = 0; i < num; i++)
				{
					stringBuilder.Append(this.pathToParent[i]);
					bool flag2 = i < num - 1;
					if (flag2)
					{
						stringBuilder.Append(", ");
					}
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400081C RID: 2076
		internal int[] pathToParent;
	}
}
