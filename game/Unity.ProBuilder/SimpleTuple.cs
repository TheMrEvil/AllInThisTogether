using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000056 RID: 86
	public struct SimpleTuple<T1, T2>
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00020474 File Offset: 0x0001E674
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0002047C File Offset: 0x0001E67C
		public T1 item1
		{
			get
			{
				return this.m_Item1;
			}
			set
			{
				this.m_Item1 = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00020485 File Offset: 0x0001E685
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0002048D File Offset: 0x0001E68D
		public T2 item2
		{
			get
			{
				return this.m_Item2;
			}
			set
			{
				this.m_Item2 = value;
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00020496 File Offset: 0x0001E696
		public SimpleTuple(T1 item1, T2 item2)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000204A8 File Offset: 0x0001E6A8
		public override string ToString()
		{
			string format = "{0}, {1}";
			T1 item = this.item1;
			object arg = item.ToString();
			T2 item2 = this.item2;
			return string.Format(format, arg, item2.ToString());
		}

		// Token: 0x040001F3 RID: 499
		private T1 m_Item1;

		// Token: 0x040001F4 RID: 500
		private T2 m_Item2;
	}
}
