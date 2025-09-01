using System;

namespace QFSW.QC
{
	// Token: 0x0200003B RID: 59
	public abstract class PolymorphicQcSerializer<T> : IQcSerializer where T : class
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00007424 File Offset: 0x00005624
		public virtual int Priority
		{
			get
			{
				return -1000;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000742B File Offset: 0x0000562B
		public bool CanSerialize(Type type)
		{
			return typeof(T).IsAssignableFrom(type);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000743D File Offset: 0x0000563D
		string IQcSerializer.SerializeFormatted(object value, QuantumTheme theme, Func<object, QuantumTheme, string> recursiveSerializer)
		{
			this._recursiveSerializer = recursiveSerializer;
			return this.SerializeFormatted((T)((object)value), theme);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007453 File Offset: 0x00005653
		protected string SerializeRecursive(object value, QuantumTheme theme)
		{
			return this._recursiveSerializer(value, theme);
		}

		// Token: 0x06000153 RID: 339
		public abstract string SerializeFormatted(T value, QuantumTheme theme);

		// Token: 0x06000154 RID: 340 RVA: 0x00007462 File Offset: 0x00005662
		protected PolymorphicQcSerializer()
		{
		}

		// Token: 0x04000105 RID: 261
		private Func<object, QuantumTheme, string> _recursiveSerializer;
	}
}
