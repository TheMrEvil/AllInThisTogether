using System;

namespace System.Collections
{
	// Token: 0x02000A50 RID: 2640
	[Serializable]
	internal sealed class EmptyReadOnlyDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06005EAC RID: 24236 RVA: 0x0000259F File Offset: 0x0000079F
		public EmptyReadOnlyDictionaryInternal()
		{
		}

		// Token: 0x06005EAD RID: 24237 RVA: 0x0013DFB3 File Offset: 0x0013C1B3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x06005EAE RID: 24238 RVA: 0x0013DFBC File Offset: 0x0013C1BC
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."), "index");
			}
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06005EAF RID: 24239 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06005EB0 RID: 24240 RVA: 0x0000270D File Offset: 0x0000090D
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06005EB1 RID: 24241 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001097 RID: 4247
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
				}
				if (!key.GetType().IsSerializable)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument passed in is not serializable."), "key");
				}
				if (value != null && !value.GetType().IsSerializable)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument passed in is not serializable."), "value");
				}
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06005EB4 RID: 24244 RVA: 0x0013E0C7 File Offset: 0x0013C2C7
		public ICollection Keys
		{
			get
			{
				return EmptyArray<object>.Value;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x06005EB5 RID: 24245 RVA: 0x0013E0C7 File Offset: 0x0013C2C7
		public ICollection Values
		{
			get
			{
				return EmptyArray<object>.Value;
			}
		}

		// Token: 0x06005EB6 RID: 24246 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool Contains(object key)
		{
			return false;
		}

		// Token: 0x06005EB7 RID: 24247 RVA: 0x0013E0D0 File Offset: 0x0013C2D0
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
			}
			if (!key.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument passed in is not serializable."), "key");
			}
			if (value != null && !value.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument passed in is not serializable."), "value");
			}
			throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
		}

		// Token: 0x06005EB8 RID: 24248 RVA: 0x0013E14B File Offset: 0x0013C34B
		public void Clear()
		{
			throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06005EB9 RID: 24249 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06005EBA RID: 24250 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005EBB RID: 24251 RVA: 0x0013DFB3 File Offset: 0x0013C1B3
		public IDictionaryEnumerator GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x06005EBC RID: 24252 RVA: 0x0013E14B File Offset: 0x0013C34B
		public void Remove(object key)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
		}

		// Token: 0x02000A51 RID: 2641
		private sealed class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06005EBD RID: 24253 RVA: 0x0000259F File Offset: 0x0000079F
			public NodeEnumerator()
			{
			}

			// Token: 0x06005EBE RID: 24254 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x1700109C RID: 4252
			// (get) Token: 0x06005EBF RID: 24255 RVA: 0x0013E15C File Offset: 0x0013C35C
			public object Current
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
			}

			// Token: 0x06005EC0 RID: 24256 RVA: 0x00004BF9 File Offset: 0x00002DF9
			public void Reset()
			{
			}

			// Token: 0x1700109D RID: 4253
			// (get) Token: 0x06005EC1 RID: 24257 RVA: 0x0013E15C File Offset: 0x0013C35C
			public object Key
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
			}

			// Token: 0x1700109E RID: 4254
			// (get) Token: 0x06005EC2 RID: 24258 RVA: 0x0013E15C File Offset: 0x0013C35C
			public object Value
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
			}

			// Token: 0x1700109F RID: 4255
			// (get) Token: 0x06005EC3 RID: 24259 RVA: 0x0013E15C File Offset: 0x0013C35C
			public DictionaryEntry Entry
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
			}
		}
	}
}
