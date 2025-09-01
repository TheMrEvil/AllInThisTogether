using System;

namespace QFSW.QC
{
	// Token: 0x02000026 RID: 38
	public abstract class PolymorphicQcParser<T> : IQcParser where T : class
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000034F3 File Offset: 0x000016F3
		public virtual int Priority
		{
			get
			{
				return -1000;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000034FA File Offset: 0x000016FA
		public bool CanParse(Type type)
		{
			return typeof(T).IsAssignableFrom(type);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000350C File Offset: 0x0000170C
		public virtual object Parse(string value, Type type, Func<string, Type, object> recursiveParser)
		{
			this._recursiveParser = recursiveParser;
			return this.Parse(value, type);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003522 File Offset: 0x00001722
		protected object ParseRecursive(string value, Type type)
		{
			return this._recursiveParser(value, type);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003531 File Offset: 0x00001731
		protected TElement ParseRecursive<TElement>(string value)
		{
			return (TElement)((object)this._recursiveParser(value, typeof(TElement)));
		}

		// Token: 0x0600008B RID: 139
		public abstract T Parse(string value, Type type);

		// Token: 0x0600008C RID: 140 RVA: 0x0000354E File Offset: 0x0000174E
		protected PolymorphicQcParser()
		{
		}

		// Token: 0x04000045 RID: 69
		private Func<string, Type, object> _recursiveParser;
	}
}
