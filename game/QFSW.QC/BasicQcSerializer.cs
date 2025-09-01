using System;

namespace QFSW.QC
{
	// Token: 0x02000038 RID: 56
	public abstract class BasicQcSerializer<T> : IQcSerializer
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00007375 File Offset: 0x00005575
		public virtual int Priority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007378 File Offset: 0x00005578
		public bool CanSerialize(Type type)
		{
			return type == typeof(T);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000738A File Offset: 0x0000558A
		string IQcSerializer.SerializeFormatted(object value, QuantumTheme theme, Func<object, QuantumTheme, string> recursiveSerializer)
		{
			this._recursiveSerializer = recursiveSerializer;
			return this.SerializeFormatted((T)((object)value), theme);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000073A0 File Offset: 0x000055A0
		protected string SerializeRecursive(object value, QuantumTheme theme)
		{
			return this._recursiveSerializer(value, theme);
		}

		// Token: 0x06000143 RID: 323
		public abstract string SerializeFormatted(T value, QuantumTheme theme);

		// Token: 0x06000144 RID: 324 RVA: 0x000073AF File Offset: 0x000055AF
		protected BasicQcSerializer()
		{
		}

		// Token: 0x04000103 RID: 259
		private Func<object, QuantumTheme, string> _recursiveSerializer;
	}
}
