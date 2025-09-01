using System;
using QFSW.QC.Utilities;

namespace QFSW.QC
{
	// Token: 0x02000039 RID: 57
	public abstract class GenericQcSerializer : IQcSerializer
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000145 RID: 325
		protected abstract Type GenericType { get; }

		// Token: 0x06000146 RID: 326 RVA: 0x000073B7 File Offset: 0x000055B7
		protected GenericQcSerializer()
		{
			if (!this.GenericType.IsGenericType)
			{
				throw new ArgumentException("Generic Serializers must use a generic type as their base");
			}
			if (this.GenericType.IsConstructedGenericType)
			{
				throw new ArgumentException("Generic Serializers must use an incomplete generic type as their base");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000073EF File Offset: 0x000055EF
		public virtual int Priority
		{
			get
			{
				return -500;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000073F6 File Offset: 0x000055F6
		public bool CanSerialize(Type type)
		{
			return type.IsGenericTypeOf(this.GenericType);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007404 File Offset: 0x00005604
		string IQcSerializer.SerializeFormatted(object value, QuantumTheme theme, Func<object, QuantumTheme, string> recursiveSerializer)
		{
			this._recursiveSerializer = recursiveSerializer;
			return this.SerializeFormatted(value, theme);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007415 File Offset: 0x00005615
		protected string SerializeRecursive(object value, QuantumTheme theme)
		{
			return this._recursiveSerializer(value, theme);
		}

		// Token: 0x0600014B RID: 331
		public abstract string SerializeFormatted(object value, QuantumTheme theme);

		// Token: 0x04000104 RID: 260
		private Func<object, QuantumTheme, string> _recursiveSerializer;
	}
}
