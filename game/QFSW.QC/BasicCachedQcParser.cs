using System;
using System.Collections.Generic;

namespace QFSW.QC
{
	// Token: 0x0200001C RID: 28
	public abstract class BasicCachedQcParser<T> : BasicQcParser<T>
	{
		// Token: 0x0600005F RID: 95 RVA: 0x000031DC File Offset: 0x000013DC
		public override object Parse(string value, Type type, Func<string, Type, object> recursiveParser)
		{
			if (this._cacheLookup.ContainsKey(value))
			{
				return this._cacheLookup[value];
			}
			T t = (T)((object)base.Parse(value, type, recursiveParser));
			this._cacheLookup[value] = t;
			return t;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000322B File Offset: 0x0000142B
		protected BasicCachedQcParser()
		{
		}

		// Token: 0x0400003F RID: 63
		private readonly Dictionary<string, T> _cacheLookup = new Dictionary<string, T>();
	}
}
