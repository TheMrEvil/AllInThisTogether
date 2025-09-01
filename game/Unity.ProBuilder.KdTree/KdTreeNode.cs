using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.ProBuilder.KdTree
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	internal class KdTreeNode<TKey, TValue>
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002BAB File Offset: 0x00000DAB
		public KdTreeNode()
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002BB3 File Offset: 0x00000DB3
		public KdTreeNode(TKey[] point, TValue value)
		{
			this.Point = point;
			this.Value = value;
		}

		// Token: 0x17000007 RID: 7
		internal KdTreeNode<TKey, TValue> this[int compare]
		{
			get
			{
				if (compare <= 0)
				{
					return this.LeftChild;
				}
				return this.RightChild;
			}
			set
			{
				if (compare <= 0)
				{
					this.LeftChild = value;
					return;
				}
				this.RightChild = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002BF1 File Offset: 0x00000DF1
		public bool IsLeaf
		{
			get
			{
				return this.LeftChild == null && this.RightChild == null;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C06 File Offset: 0x00000E06
		public void AddDuplicate(TValue value)
		{
			if (this.Duplicates == null)
			{
				this.Duplicates = new List<TValue>
				{
					value
				};
				return;
			}
			this.Duplicates.Add(value);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C30 File Offset: 0x00000E30
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.Point.Length; i++)
			{
				stringBuilder.Append(this.Point[i].ToString());
			}
			if (this.Value == null)
			{
				stringBuilder.Append("null");
			}
			else
			{
				stringBuilder.Append(this.Value.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400000D RID: 13
		public TKey[] Point;

		// Token: 0x0400000E RID: 14
		public TValue Value;

		// Token: 0x0400000F RID: 15
		public List<TValue> Duplicates;

		// Token: 0x04000010 RID: 16
		internal KdTreeNode<TKey, TValue> LeftChild;

		// Token: 0x04000011 RID: 17
		internal KdTreeNode<TKey, TValue> RightChild;
	}
}
