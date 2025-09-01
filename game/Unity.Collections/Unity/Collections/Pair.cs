using System;

namespace Unity.Collections
{
	// Token: 0x02000045 RID: 69
	internal struct Pair<Key, Value>
	{
		// Token: 0x06000138 RID: 312 RVA: 0x000047BE File Offset: 0x000029BE
		public Pair(Key k, Value v)
		{
			this.key = k;
			this.value = v;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000047CE File Offset: 0x000029CE
		public override string ToString()
		{
			return string.Format("{0} = {1}", this.key, this.value);
		}

		// Token: 0x0400009A RID: 154
		public Key key;

		// Token: 0x0400009B RID: 155
		public Value value;
	}
}
