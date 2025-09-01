using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000004 RID: 4
	public class FastAction
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002334 File Offset: 0x00000534
		public void Add(Action rhs)
		{
			bool flag = this.lookup.ContainsKey(rhs);
			if (!flag)
			{
				this.lookup[rhs] = this.delegates.AddLast(rhs);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002370 File Offset: 0x00000570
		public void Remove(Action rhs)
		{
			LinkedListNode<Action> node;
			bool flag = this.lookup.TryGetValue(rhs, out node);
			if (flag)
			{
				this.lookup.Remove(rhs);
				this.delegates.Remove(node);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023AC File Offset: 0x000005AC
		public void Call()
		{
			for (LinkedListNode<Action> linkedListNode = this.delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023E5 File Offset: 0x000005E5
		public FastAction()
		{
		}

		// Token: 0x04000001 RID: 1
		private LinkedList<Action> delegates = new LinkedList<Action>();

		// Token: 0x04000002 RID: 2
		private Dictionary<Action, LinkedListNode<Action>> lookup = new Dictionary<Action, LinkedListNode<Action>>();
	}
}
