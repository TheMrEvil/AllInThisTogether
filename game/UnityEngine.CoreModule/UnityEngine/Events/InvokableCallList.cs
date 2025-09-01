using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x020002C2 RID: 706
	internal class InvokableCallList
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0002FAD8 File Offset: 0x0002DCD8
		public int Count
		{
			get
			{
				return this.m_PersistentCalls.Count + this.m_RuntimeCalls.Count;
			}
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0002FB01 File Offset: 0x0002DD01
		public void AddPersistentInvokableCall(BaseInvokableCall call)
		{
			this.m_PersistentCalls.Add(call);
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x0002FB18 File Offset: 0x0002DD18
		public void AddListener(BaseInvokableCall call)
		{
			this.m_RuntimeCalls.Add(call);
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0002FB30 File Offset: 0x0002DD30
		public void RemoveListener(object targetObj, MethodInfo method)
		{
			List<BaseInvokableCall> list = new List<BaseInvokableCall>();
			for (int i = 0; i < this.m_RuntimeCalls.Count; i++)
			{
				bool flag = this.m_RuntimeCalls[i].Find(targetObj, method);
				if (flag)
				{
					list.Add(this.m_RuntimeCalls[i]);
				}
			}
			this.m_RuntimeCalls.RemoveAll(new Predicate<BaseInvokableCall>(list.Contains));
			List<BaseInvokableCall> list2 = new List<BaseInvokableCall>(this.m_PersistentCalls.Count + this.m_RuntimeCalls.Count);
			list2.AddRange(this.m_PersistentCalls);
			list2.AddRange(this.m_RuntimeCalls);
			this.m_ExecutingCalls = list2;
			this.m_NeedsUpdate = false;
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0002FBEC File Offset: 0x0002DDEC
		public void Clear()
		{
			this.m_RuntimeCalls.Clear();
			List<BaseInvokableCall> executingCalls = new List<BaseInvokableCall>(this.m_PersistentCalls);
			this.m_ExecutingCalls = executingCalls;
			this.m_NeedsUpdate = false;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0002FC20 File Offset: 0x0002DE20
		public void ClearPersistent()
		{
			this.m_PersistentCalls.Clear();
			List<BaseInvokableCall> executingCalls = new List<BaseInvokableCall>(this.m_RuntimeCalls);
			this.m_ExecutingCalls = executingCalls;
			this.m_NeedsUpdate = false;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0002FC54 File Offset: 0x0002DE54
		public List<BaseInvokableCall> PrepareInvoke()
		{
			bool needsUpdate = this.m_NeedsUpdate;
			if (needsUpdate)
			{
				this.m_ExecutingCalls.Clear();
				this.m_ExecutingCalls.AddRange(this.m_PersistentCalls);
				this.m_ExecutingCalls.AddRange(this.m_RuntimeCalls);
				this.m_NeedsUpdate = false;
			}
			return this.m_ExecutingCalls;
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0002FCAF File Offset: 0x0002DEAF
		public InvokableCallList()
		{
		}

		// Token: 0x040009A9 RID: 2473
		private readonly List<BaseInvokableCall> m_PersistentCalls = new List<BaseInvokableCall>();

		// Token: 0x040009AA RID: 2474
		private readonly List<BaseInvokableCall> m_RuntimeCalls = new List<BaseInvokableCall>();

		// Token: 0x040009AB RID: 2475
		private List<BaseInvokableCall> m_ExecutingCalls = new List<BaseInvokableCall>();

		// Token: 0x040009AC RID: 2476
		private bool m_NeedsUpdate = true;
	}
}
