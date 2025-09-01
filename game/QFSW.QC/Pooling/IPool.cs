using System;

namespace QFSW.QC.Pooling
{
	// Token: 0x0200005F RID: 95
	public interface IPool<T> where T : class, new()
	{
		// Token: 0x060001F9 RID: 505
		T GetObject();

		// Token: 0x060001FA RID: 506
		void Release(T obj);
	}
}
