using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000629 RID: 1577
	[Serializable]
	internal class MessageDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06003B5C RID: 15196 RVA: 0x000CF02E File Offset: 0x000CD22E
		public MessageDictionary(IMethodMessage message)
		{
			this._message = message;
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x000CF03D File Offset: 0x000CD23D
		internal bool HasUserData()
		{
			if (this._internalProperties == null)
			{
				return false;
			}
			if (this._internalProperties is MessageDictionary)
			{
				return ((MessageDictionary)this._internalProperties).HasUserData();
			}
			return this._internalProperties.Count > 0;
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06003B5E RID: 15198 RVA: 0x000CF075 File Offset: 0x000CD275
		internal IDictionary InternalDictionary
		{
			get
			{
				if (this._internalProperties != null && this._internalProperties is MessageDictionary)
				{
					return ((MessageDictionary)this._internalProperties).InternalDictionary;
				}
				return this._internalProperties;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x000CF0A3 File Offset: 0x000CD2A3
		// (set) Token: 0x06003B60 RID: 15200 RVA: 0x000CF0AB File Offset: 0x000CD2AB
		public string[] MethodKeys
		{
			get
			{
				return this._methodKeys;
			}
			set
			{
				this._methodKeys = value;
			}
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x000CF0B4 File Offset: 0x000CD2B4
		protected virtual IDictionary AllocInternalProperties()
		{
			this._ownProperties = true;
			return new Hashtable();
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x000CF0C2 File Offset: 0x000CD2C2
		public IDictionary GetInternalProperties()
		{
			if (this._internalProperties == null)
			{
				this._internalProperties = this.AllocInternalProperties();
			}
			return this._internalProperties;
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x000CF0E0 File Offset: 0x000CD2E0
		private bool IsOverridenKey(string key)
		{
			if (this._ownProperties)
			{
				return false;
			}
			foreach (string b in this._methodKeys)
			{
				if (key == b)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x000CF11C File Offset: 0x000CD31C
		public MessageDictionary(string[] keys)
		{
			this._methodKeys = keys;
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06003B65 RID: 15205 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06003B66 RID: 15206 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008E4 RID: 2276
		public object this[object key]
		{
			get
			{
				string text = (string)key;
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					if (this._methodKeys[i] == text)
					{
						return this.GetMethodProperty(text);
					}
				}
				if (this._internalProperties != null)
				{
					return this._internalProperties[key];
				}
				return null;
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x000CF18C File Offset: 0x000CD38C
		protected virtual object GetMethodProperty(string key)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1637783905U)
			{
				if (num <= 1201911322U)
				{
					if (num != 990701179U)
					{
						if (num == 1201911322U)
						{
							if (key == "__CallContext")
							{
								return this._message.LogicalCallContext;
							}
						}
					}
					else if (key == "__Uri")
					{
						return this._message.Uri;
					}
				}
				else if (num != 1619225942U)
				{
					if (num == 1637783905U)
					{
						if (key == "__Return")
						{
							return ((IMethodReturnMessage)this._message).ReturnValue;
						}
					}
				}
				else if (key == "__Args")
				{
					return this._message.Args;
				}
			}
			else if (num <= 2010141056U)
			{
				if (num != 1960967436U)
				{
					if (num == 2010141056U)
					{
						if (key == "__TypeName")
						{
							return this._message.TypeName;
						}
					}
				}
				else if (key == "__OutArgs")
				{
					return ((IMethodReturnMessage)this._message).OutArgs;
				}
			}
			else if (num != 3166241401U)
			{
				if (num == 3679129400U)
				{
					if (key == "__MethodSignature")
					{
						return this._message.MethodSignature;
					}
				}
			}
			else if (key == "__MethodName")
			{
				return this._message.MethodName;
			}
			return null;
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x000CF310 File Offset: 0x000CD510
		protected virtual void SetMethodProperty(string key, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1637783905U)
			{
				if (num <= 1201911322U)
				{
					if (num != 990701179U)
					{
						if (num != 1201911322U)
						{
							return;
						}
						key == "__CallContext";
						return;
					}
					else
					{
						if (!(key == "__Uri"))
						{
							return;
						}
						((IInternalMessage)this._message).Uri = (string)value;
						return;
					}
				}
				else
				{
					if (num == 1619225942U)
					{
						key == "__Args";
						return;
					}
					if (num != 1637783905U)
					{
						return;
					}
					key == "__Return";
					return;
				}
			}
			else if (num <= 2010141056U)
			{
				if (num == 1960967436U)
				{
					key == "__OutArgs";
					return;
				}
				if (num != 2010141056U)
				{
					return;
				}
				key == "__TypeName";
				return;
			}
			else
			{
				if (num == 3166241401U)
				{
					key == "__MethodName";
					return;
				}
				if (num != 3679129400U)
				{
					return;
				}
				key == "__MethodSignature";
				return;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x000CF408 File Offset: 0x000CD608
		public ICollection Keys
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					arrayList.Add(this._methodKeys[i]);
				}
				if (this._internalProperties != null)
				{
					foreach (object obj in this._internalProperties.Keys)
					{
						string text = (string)obj;
						if (!this.IsOverridenKey(text))
						{
							arrayList.Add(text);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06003B6C RID: 15212 RVA: 0x000CF4A4 File Offset: 0x000CD6A4
		public ICollection Values
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					arrayList.Add(this.GetMethodProperty(this._methodKeys[i]));
				}
				if (this._internalProperties != null)
				{
					foreach (object obj in this._internalProperties)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (!this.IsOverridenKey((string)dictionaryEntry.Key))
						{
							arrayList.Add(dictionaryEntry.Value);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x000CF554 File Offset: 0x000CD754
		public void Add(object key, object value)
		{
			string text = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == text)
				{
					this.SetMethodProperty(text, value);
					return;
				}
			}
			if (this._internalProperties == null)
			{
				this._internalProperties = this.AllocInternalProperties();
			}
			this._internalProperties[key] = value;
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x000CF5B5 File Offset: 0x000CD7B5
		public void Clear()
		{
			if (this._internalProperties != null)
			{
				this._internalProperties.Clear();
			}
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000CF5CC File Offset: 0x000CD7CC
		public bool Contains(object key)
		{
			string b = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == b)
				{
					return true;
				}
			}
			return this._internalProperties != null && this._internalProperties.Contains(key);
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x000CF61C File Offset: 0x000CD81C
		public void Remove(object key)
		{
			string b = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == b)
				{
					throw new ArgumentException("key was invalid");
				}
			}
			if (this._internalProperties != null)
			{
				this._internalProperties.Remove(key);
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x000CF672 File Offset: 0x000CD872
		public int Count
		{
			get
			{
				if (this._internalProperties != null)
				{
					return this._internalProperties.Count + this._methodKeys.Length;
				}
				return this._methodKeys.Length;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06003B72 RID: 15218 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x0000270D File Offset: 0x0000090D
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x000CF699 File Offset: 0x000CD899
		public void CopyTo(Array array, int index)
		{
			this.Values.CopyTo(array, index);
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x000CF6A8 File Offset: 0x000CD8A8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MessageDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x000CF6A8 File Offset: 0x000CD8A8
		public IDictionaryEnumerator GetEnumerator()
		{
			return new MessageDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x040026A5 RID: 9893
		private IDictionary _internalProperties;

		// Token: 0x040026A6 RID: 9894
		protected IMethodMessage _message;

		// Token: 0x040026A7 RID: 9895
		private string[] _methodKeys;

		// Token: 0x040026A8 RID: 9896
		private bool _ownProperties;

		// Token: 0x0200062A RID: 1578
		private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06003B77 RID: 15223 RVA: 0x000CF6B0 File Offset: 0x000CD8B0
			public DictionaryEnumerator(MessageDictionary methodDictionary)
			{
				this._methodDictionary = methodDictionary;
				this._hashtableEnum = ((this._methodDictionary._internalProperties != null) ? this._methodDictionary._internalProperties.GetEnumerator() : null);
				this._posMethod = -1;
			}

			// Token: 0x170008EA RID: 2282
			// (get) Token: 0x06003B78 RID: 15224 RVA: 0x000CF6EC File Offset: 0x000CD8EC
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06003B79 RID: 15225 RVA: 0x000CF6FC File Offset: 0x000CD8FC
			public bool MoveNext()
			{
				if (this._posMethod != -2)
				{
					this._posMethod++;
					if (this._posMethod < this._methodDictionary._methodKeys.Length)
					{
						return true;
					}
					this._posMethod = -2;
				}
				if (this._hashtableEnum == null)
				{
					return false;
				}
				while (this._hashtableEnum.MoveNext())
				{
					if (!this._methodDictionary.IsOverridenKey((string)this._hashtableEnum.Key))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06003B7A RID: 15226 RVA: 0x000CF777 File Offset: 0x000CD977
			public void Reset()
			{
				this._posMethod = -1;
				this._hashtableEnum.Reset();
			}

			// Token: 0x170008EB RID: 2283
			// (get) Token: 0x06003B7B RID: 15227 RVA: 0x000CF78C File Offset: 0x000CD98C
			public DictionaryEntry Entry
			{
				get
				{
					if (this._posMethod >= 0)
					{
						return new DictionaryEntry(this._methodDictionary._methodKeys[this._posMethod], this._methodDictionary.GetMethodProperty(this._methodDictionary._methodKeys[this._posMethod]));
					}
					if (this._posMethod == -1 || this._hashtableEnum == null)
					{
						throw new InvalidOperationException("The enumerator is positioned before the first element of the collection or after the last element");
					}
					return this._hashtableEnum.Entry;
				}
			}

			// Token: 0x170008EC RID: 2284
			// (get) Token: 0x06003B7C RID: 15228 RVA: 0x000CF800 File Offset: 0x000CDA00
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x170008ED RID: 2285
			// (get) Token: 0x06003B7D RID: 15229 RVA: 0x000CF81C File Offset: 0x000CDA1C
			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x040026A9 RID: 9897
			private MessageDictionary _methodDictionary;

			// Token: 0x040026AA RID: 9898
			private IDictionaryEnumerator _hashtableEnum;

			// Token: 0x040026AB RID: 9899
			private int _posMethod;
		}
	}
}
