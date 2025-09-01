using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000308 RID: 776
	internal class BasicNode<T> : LinkedPoolItem<BasicNode<T>>
	{
		// Token: 0x0600199B RID: 6555 RVA: 0x00068A68 File Offset: 0x00066C68
		public void AppendTo(ref BasicNode<T> first)
		{
			bool flag = first == null;
			if (flag)
			{
				first = this;
			}
			else
			{
				BasicNode<T> basicNode = first;
				while (basicNode.next != null)
				{
					basicNode = basicNode.next;
				}
				basicNode.next = this;
			}
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00068AA5 File Offset: 0x00066CA5
		public BasicNode()
		{
		}

		// Token: 0x04000B19 RID: 2841
		public BasicNode<T> next;

		// Token: 0x04000B1A RID: 2842
		public T data;
	}
}
