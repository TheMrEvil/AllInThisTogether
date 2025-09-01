using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public abstract class SerializedDictionary<K, V, SK, SV> : Dictionary<K, V>, ISerializationCallbackReceiver
	{
		// Token: 0x06000326 RID: 806
		public abstract SK SerializeKey(K key);

		// Token: 0x06000327 RID: 807
		public abstract SV SerializeValue(V value);

		// Token: 0x06000328 RID: 808
		public abstract K DeserializeKey(SK serializedKey);

		// Token: 0x06000329 RID: 809
		public abstract V DeserializeValue(SV serializedValue);

		// Token: 0x0600032A RID: 810 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
		public void OnBeforeSerialize()
		{
			this.m_Keys.Clear();
			this.m_Values.Clear();
			foreach (KeyValuePair<K, V> keyValuePair in this)
			{
				this.m_Keys.Add(this.SerializeKey(keyValuePair.Key));
				this.m_Values.Add(this.SerializeValue(keyValuePair.Value));
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000F064 File Offset: 0x0000D264
		public void OnAfterDeserialize()
		{
			for (int i = 0; i < this.m_Keys.Count; i++)
			{
				base.Add(this.DeserializeKey(this.m_Keys[i]), this.DeserializeValue(this.m_Values[i]));
			}
			this.m_Keys.Clear();
			this.m_Values.Clear();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000F0C7 File Offset: 0x0000D2C7
		protected SerializedDictionary()
		{
		}

		// Token: 0x0400020B RID: 523
		[SerializeField]
		private List<SK> m_Keys = new List<SK>();

		// Token: 0x0400020C RID: 524
		[SerializeField]
		private List<SV> m_Values = new List<SV>();
	}
}
