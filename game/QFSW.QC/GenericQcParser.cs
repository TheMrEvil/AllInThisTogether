using System;
using QFSW.QC.Utilities;

namespace QFSW.QC
{
	// Token: 0x02000021 RID: 33
	public abstract class GenericQcParser : IQcParser
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006E RID: 110
		protected abstract Type GenericType { get; }

		// Token: 0x0600006F RID: 111 RVA: 0x00003320 File Offset: 0x00001520
		protected GenericQcParser()
		{
			if (!this.GenericType.IsGenericType)
			{
				throw new ArgumentException("Generic Parsers must use a generic type as their base");
			}
			if (this.GenericType.IsConstructedGenericType)
			{
				throw new ArgumentException("Generic Parsers must use an incomplete generic type as their base");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003358 File Offset: 0x00001558
		public virtual int Priority
		{
			get
			{
				return -500;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000335F File Offset: 0x0000155F
		public bool CanParse(Type type)
		{
			return type.IsGenericTypeOf(this.GenericType);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000336D File Offset: 0x0000156D
		public virtual object Parse(string value, Type type, Func<string, Type, object> recursiveParser)
		{
			this._recursiveParser = recursiveParser;
			return this.Parse(value, type);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000337E File Offset: 0x0000157E
		protected object ParseRecursive(string value, Type type)
		{
			return this._recursiveParser(value, type);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000338D File Offset: 0x0000158D
		protected TElement ParseRecursive<TElement>(string value)
		{
			return (TElement)((object)this._recursiveParser(value, typeof(TElement)));
		}

		// Token: 0x06000075 RID: 117
		public abstract object Parse(string value, Type type);

		// Token: 0x04000042 RID: 66
		private Func<string, Type, object> _recursiveParser;
	}
}
