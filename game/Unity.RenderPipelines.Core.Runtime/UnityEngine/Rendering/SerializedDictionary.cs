using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public class SerializedDictionary<K, V> : SerializedDictionary<K, V, K, V>
	{
		// Token: 0x06000321 RID: 801 RVA: 0x0000EFBD File Offset: 0x0000D1BD
		public override K SerializeKey(K key)
		{
			return key;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
		public override V SerializeValue(V val)
		{
			return val;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000EFC3 File Offset: 0x0000D1C3
		public override K DeserializeKey(K key)
		{
			return key;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000EFC6 File Offset: 0x0000D1C6
		public override V DeserializeValue(V val)
		{
			return val;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000EFC9 File Offset: 0x0000D1C9
		public SerializedDictionary()
		{
		}
	}
}
