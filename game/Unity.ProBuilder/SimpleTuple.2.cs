using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000057 RID: 87
	internal struct SimpleTuple<T1, T2, T3>
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000204E7 File Offset: 0x0001E6E7
		// (set) Token: 0x0600034E RID: 846 RVA: 0x000204EF File Offset: 0x0001E6EF
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

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000204F8 File Offset: 0x0001E6F8
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00020500 File Offset: 0x0001E700
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

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00020509 File Offset: 0x0001E709
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00020511 File Offset: 0x0001E711
		public T3 item3
		{
			get
			{
				return this.m_Item3;
			}
			set
			{
				this.m_Item3 = value;
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0002051A File Offset: 0x0001E71A
		public SimpleTuple(T1 item1, T2 item2, T3 item3)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00020534 File Offset: 0x0001E734
		public override string ToString()
		{
			string format = "{0}, {1}, {2}";
			T1 item = this.item1;
			object arg = item.ToString();
			T2 item2 = this.item2;
			object arg2 = item2.ToString();
			T3 item3 = this.item3;
			return string.Format(format, arg, arg2, item3.ToString());
		}

		// Token: 0x040001F5 RID: 501
		private T1 m_Item1;

		// Token: 0x040001F6 RID: 502
		private T2 m_Item2;

		// Token: 0x040001F7 RID: 503
		private T3 m_Item3;
	}
}
