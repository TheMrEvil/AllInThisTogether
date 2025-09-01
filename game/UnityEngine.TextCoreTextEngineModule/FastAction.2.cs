using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000005 RID: 5
	public class FastAction<A>
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002404 File Offset: 0x00000604
		public void Add(Action<A> rhs)
		{
			bool flag = this.lookup.ContainsKey(rhs);
			if (!flag)
			{
				this.lookup[rhs] = this.delegates.AddLast(rhs);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002440 File Offset: 0x00000640
		public void Remove(Action<A> rhs)
		{
			LinkedListNode<Action<A>> node;
			bool flag = this.lookup.TryGetValue(rhs, out node);
			if (flag)
			{
				this.lookup.Remove(rhs);
				this.delegates.Remove(node);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000247C File Offset: 0x0000067C
		public void Call(A a)
		{
			for (LinkedListNode<Action<A>> linkedListNode = this.delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value(a);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024B6 File Offset: 0x000006B6
		public FastAction()
		{
		}

		// Token: 0x04000003 RID: 3
		private LinkedList<Action<A>> delegates = new LinkedList<Action<A>>();

		// Token: 0x04000004 RID: 4
		private Dictionary<Action<A>, LinkedListNode<Action<A>>> lookup = new Dictionary<Action<A>, LinkedListNode<Action<A>>>();
	}
}
