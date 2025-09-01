using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x02000005 RID: 5
	public class FastAction<A, B, C>
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002264 File Offset: 0x00000464
		public void Add(Action<A, B, C> rhs)
		{
			if (this.lookup.ContainsKey(rhs))
			{
				return;
			}
			this.lookup[rhs] = this.delegates.AddLast(rhs);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002290 File Offset: 0x00000490
		public void Remove(Action<A, B, C> rhs)
		{
			LinkedListNode<Action<A, B, C>> node;
			if (this.lookup.TryGetValue(rhs, out node))
			{
				this.lookup.Remove(rhs);
				this.delegates.Remove(node);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022C8 File Offset: 0x000004C8
		public void Call(A a, B b, C c)
		{
			for (LinkedListNode<Action<A, B, C>> linkedListNode = this.delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value(a, b, c);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022FB File Offset: 0x000004FB
		public FastAction()
		{
		}

		// Token: 0x04000007 RID: 7
		private LinkedList<Action<A, B, C>> delegates = new LinkedList<Action<A, B, C>>();

		// Token: 0x04000008 RID: 8
		private Dictionary<Action<A, B, C>, LinkedListNode<Action<A, B, C>>> lookup = new Dictionary<Action<A, B, C>, LinkedListNode<Action<A, B, C>>>();
	}
}
