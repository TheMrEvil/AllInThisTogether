using System;
using System.Collections.Generic;

namespace QFSW.QC
{
	// Token: 0x02000020 RID: 32
	public abstract class GenericCachedQcParser : GenericQcParser
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000032C4 File Offset: 0x000014C4
		public override object Parse(string value, Type type, Func<string, Type, object> recursiveParser)
		{
			ValueTuple<string, Type> key = new ValueTuple<string, Type>(value, type);
			if (this._cacheLookup.ContainsKey(key))
			{
				return this._cacheLookup[key];
			}
			object obj = base.Parse(value, type, recursiveParser);
			this._cacheLookup[key] = obj;
			return obj;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000330D File Offset: 0x0000150D
		protected GenericCachedQcParser()
		{
		}

		// Token: 0x04000041 RID: 65
		private readonly Dictionary<ValueTuple<string, Type>, object> _cacheLookup = new Dictionary<ValueTuple<string, Type>, object>();
	}
}
