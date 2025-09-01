using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x02000004 RID: 4
	public class FastAction<A, B>
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021B3 File Offset: 0x000003B3
		public void Add(Action<A, B> rhs)
		{
			if (this.lookup.ContainsKey(rhs))
			{
				return;
			}
			this.lookup[rhs] = this.delegates.AddLast(rhs);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021DC File Offset: 0x000003DC
		public void Remove(Action<A, B> rhs)
		{
			LinkedListNode<Action<A, B>> node;
			if (this.lookup.TryGetValue(rhs, out node))
			{
				this.lookup.Remove(rhs);
				this.delegates.Remove(node);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002214 File Offset: 0x00000414
		public void Call(A a, B b)
		{
			for (LinkedListNode<Action<A, B>> linkedListNode = this.delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value(a, b);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002246 File Offset: 0x00000446
		public FastAction()
		{
		}

		// Token: 0x04000005 RID: 5
		private LinkedList<Action<A, B>> delegates = new LinkedList<Action<A, B>>();

		// Token: 0x04000006 RID: 6
		private Dictionary<Action<A, B>, LinkedListNode<Action<A, B>>> lookup = new Dictionary<Action<A, B>, LinkedListNode<Action<A, B>>>();
	}
}
