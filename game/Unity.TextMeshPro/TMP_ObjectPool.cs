using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro
{
	// Token: 0x02000047 RID: 71
	internal class TMP_ObjectPool<T> where T : new()
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000248AD File Offset: 0x00022AAD
		// (set) Token: 0x06000351 RID: 849 RVA: 0x000248B5 File Offset: 0x00022AB5
		public int countAll
		{
			[CompilerGenerated]
			get
			{
				return this.<countAll>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<countAll>k__BackingField = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000352 RID: 850 RVA: 0x000248BE File Offset: 0x00022ABE
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000353 RID: 851 RVA: 0x000248CD File Offset: 0x00022ACD
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000248DA File Offset: 0x00022ADA
		public TMP_ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000248FC File Offset: 0x00022AFC
		public T Get()
		{
			T t;
			if (this.m_Stack.Count == 0)
			{
				t = Activator.CreateInstance<T>();
				int countAll = this.countAll;
				this.countAll = countAll + 1;
			}
			else
			{
				t = this.m_Stack.Pop();
			}
			if (this.m_ActionOnGet != null)
			{
				this.m_ActionOnGet(t);
			}
			return t;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00024950 File Offset: 0x00022B50
		public void Release(T element)
		{
			if (this.m_Stack.Count > 0 && this.m_Stack.Peek() == element)
			{
				Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
			}
			if (this.m_ActionOnRelease != null)
			{
				this.m_ActionOnRelease(element);
			}
			this.m_Stack.Push(element);
		}

		// Token: 0x0400028A RID: 650
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x0400028B RID: 651
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x0400028C RID: 652
		private readonly UnityAction<T> m_ActionOnRelease;

		// Token: 0x0400028D RID: 653
		[CompilerGenerated]
		private int <countAll>k__BackingField;
	}
}
