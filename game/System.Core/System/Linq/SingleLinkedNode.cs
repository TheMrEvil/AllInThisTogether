using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	// Token: 0x020000EB RID: 235
	internal sealed class SingleLinkedNode<TSource>
	{
		// Token: 0x0600083D RID: 2109 RVA: 0x0001C99E File Offset: 0x0001AB9E
		public SingleLinkedNode(TSource item)
		{
			this.Item = item;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001C9AD File Offset: 0x0001ABAD
		private SingleLinkedNode(SingleLinkedNode<TSource> linked, TSource item)
		{
			this.Linked = linked;
			this.Item = item;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0001C9C3 File Offset: 0x0001ABC3
		public TSource Item
		{
			[CompilerGenerated]
			get
			{
				return this.<Item>k__BackingField;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0001C9CB File Offset: 0x0001ABCB
		public SingleLinkedNode<TSource> Linked
		{
			[CompilerGenerated]
			get
			{
				return this.<Linked>k__BackingField;
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001C9D3 File Offset: 0x0001ABD3
		public SingleLinkedNode<TSource> Add(TSource item)
		{
			return new SingleLinkedNode<TSource>(this, item);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001C9DC File Offset: 0x0001ABDC
		public int GetCount()
		{
			int num = 0;
			for (SingleLinkedNode<TSource> singleLinkedNode = this; singleLinkedNode != null; singleLinkedNode = singleLinkedNode.Linked)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001C9FE File Offset: 0x0001ABFE
		public IEnumerator<TSource> GetEnumerator(int count)
		{
			return this.ToArray(count).GetEnumerator();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001CA0C File Offset: 0x0001AC0C
		public SingleLinkedNode<TSource> GetNode(int index)
		{
			SingleLinkedNode<TSource> singleLinkedNode = this;
			while (index > 0)
			{
				singleLinkedNode = singleLinkedNode.Linked;
				index--;
			}
			return singleLinkedNode;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001CA30 File Offset: 0x0001AC30
		private TSource[] ToArray(int count)
		{
			TSource[] array = new TSource[count];
			int num = count;
			for (SingleLinkedNode<TSource> singleLinkedNode = this; singleLinkedNode != null; singleLinkedNode = singleLinkedNode.Linked)
			{
				num--;
				array[num] = singleLinkedNode.Item;
			}
			return array;
		}

		// Token: 0x040005C2 RID: 1474
		[CompilerGenerated]
		private readonly TSource <Item>k__BackingField;

		// Token: 0x040005C3 RID: 1475
		[CompilerGenerated]
		private readonly SingleLinkedNode<TSource> <Linked>k__BackingField;
	}
}
