using System;

namespace QFSW.QC
{
	// Token: 0x0200001D RID: 29
	public abstract class BasicQcParser<T> : IQcParser
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000323E File Offset: 0x0000143E
		public virtual int Priority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003241 File Offset: 0x00001441
		public bool CanParse(Type type)
		{
			return type == typeof(T);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003253 File Offset: 0x00001453
		public virtual object Parse(string value, Type type, Func<string, Type, object> recursiveParser)
		{
			this._recursiveParser = recursiveParser;
			return this.Parse(value);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003268 File Offset: 0x00001468
		protected object ParseRecursive(string value, Type type)
		{
			return this._recursiveParser(value, type);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003277 File Offset: 0x00001477
		protected TElement ParseRecursive<TElement>(string value)
		{
			return (TElement)((object)this._recursiveParser(value, typeof(TElement)));
		}

		// Token: 0x06000066 RID: 102
		public abstract T Parse(string value);

		// Token: 0x06000067 RID: 103 RVA: 0x00003294 File Offset: 0x00001494
		protected BasicQcParser()
		{
		}

		// Token: 0x04000040 RID: 64
		private Func<string, Type, object> _recursiveParser;
	}
}
