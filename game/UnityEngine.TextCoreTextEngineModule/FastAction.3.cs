using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000006 RID: 6
	public class FastAction<A, B>
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000024D8 File Offset: 0x000006D8
		public void Add(Action<A, B> rhs)
		{
			bool flag = this.lookup.ContainsKey(rhs);
			if (!flag)
			{
				this.lookup[rhs] = this.delegates.AddLast(rhs);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002514 File Offset: 0x00000714
		public void Remove(Action<A, B> rhs)
		{
			LinkedListNode<Action<A, B>> node;
			bool flag = this.lookup.TryGetValue(rhs, out node);
			if (flag)
			{
				this.lookup.Remove(rhs);
				this.delegates.Remove(node);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002550 File Offset: 0x00000750
		public void Call(A a, B b)
		{
			for (LinkedListNode<Action<A, B>> linkedListNode = this.delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value(a, b);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000258B File Offset: 0x0000078B
		public FastAction()
		{
		}

		// Token: 0x04000005 RID: 5
		private LinkedList<Action<A, B>> delegates = new LinkedList<Action<A, B>>();

		// Token: 0x04000006 RID: 6
		private Dictionary<Action<A, B>, LinkedListNode<Action<A, B>>> lookup = new Dictionary<Action<A, B>, LinkedListNode<Action<A, B>>>();
	}
}
