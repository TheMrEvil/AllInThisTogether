using System;
using System.Collections.Generic;

namespace QFSW.QC
{
	// Token: 0x02000025 RID: 37
	public abstract class PolymorphicCachedQcParser<T> : PolymorphicQcParser<T> where T : class
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003488 File Offset: 0x00001688
		public override object Parse(string value, Type type, Func<string, Type, object> recursiveParser)
		{
			ValueTuple<string, Type> key = new ValueTuple<string, Type>(value, type);
			if (this._cacheLookup.ContainsKey(key))
			{
				return this._cacheLookup[key];
			}
			T t = (T)((object)base.Parse(value, type, recursiveParser));
			this._cacheLookup[key] = t;
			return t;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000034E0 File Offset: 0x000016E0
		protected PolymorphicCachedQcParser()
		{
		}

		// Token: 0x04000044 RID: 68
		private readonly Dictionary<ValueTuple<string, Type>, T> _cacheLookup = new Dictionary<ValueTuple<string, Type>, T>();
	}
}
