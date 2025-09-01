using System;

namespace Parse
{
	// Token: 0x02000014 RID: 20
	public sealed class ParseRelation<T> : ParseRelationBase where T : ParseObject
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000069DD File Offset: 0x00004BDD
		internal ParseRelation(ParseObject parent, string key) : base(parent, key)
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000069E7 File Offset: 0x00004BE7
		internal ParseRelation(ParseObject parent, string key, string targetClassName) : base(parent, key, targetClassName)
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000069F2 File Offset: 0x00004BF2
		public void Add(T obj)
		{
			base.Add(obj);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006A00 File Offset: 0x00004C00
		public void Remove(T obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006A0E File Offset: 0x00004C0E
		public ParseQuery<T> Query
		{
			get
			{
				return base.GetQuery<T>();
			}
		}
	}
}
