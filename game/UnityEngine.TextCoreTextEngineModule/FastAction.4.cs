using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000007 RID: 7
	public class FastAction<A, B, C>
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000025AC File Offset: 0x000007AC
		public void Add(Action<A, B, C> rhs)
		{
			bool flag = this.lookup.ContainsKey(rhs);
			if (!flag)
			{
				this.lookup[rhs] = this.delegates.AddLast(rhs);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025E8 File Offset: 0x000007E8
		public void Remove(Action<A, B, C> rhs)
		{
			LinkedListNode<Action<A, B, C>> node;
			bool flag = this.lookup.TryGetValue(rhs, out node);
			if (flag)
			{
				this.lookup.Remove(rhs);
				this.delegates.Remove(node);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002624 File Offset: 0x00000824
		public void Call(A a, B b, C c)
		{
			for (LinkedListNode<Action<A, B, C>> linkedListNode = this.delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value(a, b, c);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002660 File Offset: 0x00000860
		public FastAction()
		{
		}

		// Token: 0x04000007 RID: 7
		private LinkedList<Action<A, B, C>> delegates = new LinkedList<Action<A, B, C>>();

		// Token: 0x04000008 RID: 8
		private Dictionary<Action<A, B, C>, LinkedListNode<Action<A, B, C>>> lookup = new Dictionary<Action<A, B, C>, LinkedListNode<Action<A, B, C>>>();
	}
}
