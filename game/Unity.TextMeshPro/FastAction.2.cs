using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x02000003 RID: 3
	public class FastAction<A>
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002102 File Offset: 0x00000302
		public void Add(Action<A> rhs)
		{
			if (this.lookup.ContainsKey(rhs))
			{
				return;
			}
			this.lookup[rhs] = this.delegates.AddLast(rhs);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000212C File Offset: 0x0000032C
		public void Remove(Action<A> rhs)
		{
			LinkedListNode<Action<A>> node;
			if (this.lookup.TryGetValue(rhs, out node))
			{
				this.lookup.Remove(rhs);
				this.delegates.Remove(node);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002164 File Offset: 0x00000364
		public void Call(A a)
		{
			for (LinkedListNode<Action<A>> linkedListNode = this.delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value(a);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002195 File Offset: 0x00000395
		public FastAction()
		{
		}

		// Token: 0x04000003 RID: 3
		private LinkedList<Action<A>> delegates = new LinkedList<Action<A>>();

		// Token: 0x04000004 RID: 4
		private Dictionary<Action<A>, LinkedListNode<Action<A>>> lookup = new Dictionary<Action<A>, LinkedListNode<Action<A>>>();
	}
}
