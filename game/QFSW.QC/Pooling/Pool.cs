using System;
using System.Collections.Generic;

namespace QFSW.QC.Pooling
{
	// Token: 0x02000060 RID: 96
	public class Pool<T> : IPool<T> where T : class, new()
	{
		// Token: 0x060001FB RID: 507 RVA: 0x00009CB1 File Offset: 0x00007EB1
		public Pool()
		{
			this._objs = new Stack<T>();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009CC4 File Offset: 0x00007EC4
		public Pool(int objCount)
		{
			this._objs = new Stack<T>(objCount);
			for (int i = 0; i < objCount; i++)
			{
				this._objs.Push(Activator.CreateInstance<T>());
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009CFF File Offset: 0x00007EFF
		public T GetObject()
		{
			if (this._objs.Count > 0)
			{
				return this._objs.Pop();
			}
			return Activator.CreateInstance<T>();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009D20 File Offset: 0x00007F20
		public void Release(T obj)
		{
			this._objs.Push(obj);
		}

		// Token: 0x04000140 RID: 320
		private readonly Stack<T> _objs;
	}
}
