using System;
using System.Text;

namespace QFSW.QC.Pooling
{
	// Token: 0x02000063 RID: 99
	public class StringBuilderPool<TPool> where TPool : IPool<StringBuilder>, new()
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00009D40 File Offset: 0x00007F40
		public StringBuilder GetStringBuilder(int minCapacity = 0)
		{
			TPool pool = this._pool;
			StringBuilder @object = pool.GetObject();
			@object.Clear();
			@object.EnsureCapacity(minCapacity);
			return @object;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009D70 File Offset: 0x00007F70
		public string ReleaseAndToString(StringBuilder stringBuilder)
		{
			string result = stringBuilder.ToString();
			TPool pool = this._pool;
			pool.Release(stringBuilder);
			return result;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009D98 File Offset: 0x00007F98
		public StringBuilderPool()
		{
		}

		// Token: 0x04000141 RID: 321
		private readonly TPool _pool = Activator.CreateInstance<TPool>();
	}
}
