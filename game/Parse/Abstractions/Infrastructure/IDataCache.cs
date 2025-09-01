using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parse.Abstractions.Infrastructure
{
	// Token: 0x0200008E RID: 142
	public interface IDataCache<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x06000565 RID: 1381
		Task AddAsync(TKey key, TValue value);

		// Token: 0x06000566 RID: 1382
		Task RemoveAsync(TKey key);
	}
}
