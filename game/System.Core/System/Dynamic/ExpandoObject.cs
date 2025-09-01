using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents an object whose members can be dynamically added and removed at run time.</summary>
	// Token: 0x0200030D RID: 781
	public sealed class ExpandoObject : IDynamicMetaObjectProvider, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, INotifyPropertyChanged
	{
		/// <summary>Initializes a new <see langword="ExpandoObject" /> that does not have members.</summary>
		// Token: 0x0600177C RID: 6012 RVA: 0x0004E444 File Offset: 0x0004C644
		public ExpandoObject()
		{
			this._data = ExpandoObject.ExpandoData.Empty;
			this.LockObject = new object();
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0004E464 File Offset: 0x0004C664
		internal bool TryGetValue(object indexClass, int index, string name, bool ignoreCase, out object value)
		{
			ExpandoObject.ExpandoData data = this._data;
			if (data.Class != indexClass || ignoreCase)
			{
				index = data.Class.GetValueIndex(name, ignoreCase, this);
				if (index == -2)
				{
					throw Error.AmbiguousMatchInExpandoObject(name);
				}
			}
			if (index == -1)
			{
				value = null;
				return false;
			}
			object obj = data[index];
			if (obj == ExpandoObject.Uninitialized)
			{
				value = null;
				return false;
			}
			value = obj;
			return true;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0004E4CC File Offset: 0x0004C6CC
		internal void TrySetValue(object indexClass, int index, object value, string name, bool ignoreCase, bool add)
		{
			object lockObject = this.LockObject;
			ExpandoObject.ExpandoData expandoData;
			object obj;
			lock (lockObject)
			{
				expandoData = this._data;
				if (expandoData.Class != indexClass || ignoreCase)
				{
					index = expandoData.Class.GetValueIndex(name, ignoreCase, this);
					if (index == -2)
					{
						throw Error.AmbiguousMatchInExpandoObject(name);
					}
					if (index == -1)
					{
						int num = ignoreCase ? expandoData.Class.GetValueIndexCaseSensitive(name) : index;
						if (num != -1)
						{
							index = num;
						}
						else
						{
							ExpandoClass newClass = expandoData.Class.FindNewClass(name);
							expandoData = this.PromoteClassCore(expandoData.Class, newClass);
							index = expandoData.Class.GetValueIndexCaseSensitive(name);
						}
					}
				}
				obj = expandoData[index];
				if (obj == ExpandoObject.Uninitialized)
				{
					this._count++;
				}
				else if (add)
				{
					throw Error.SameKeyExistsInExpando(name);
				}
				expandoData[index] = value;
			}
			PropertyChangedEventHandler propertyChanged = this._propertyChanged;
			if (propertyChanged != null && value != obj)
			{
				propertyChanged(this, new PropertyChangedEventArgs(expandoData.Class.Keys[index]));
			}
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0004E5EC File Offset: 0x0004C7EC
		internal bool TryDeleteValue(object indexClass, int index, string name, bool ignoreCase, object deleteValue)
		{
			object lockObject = this.LockObject;
			ExpandoObject.ExpandoData data;
			lock (lockObject)
			{
				data = this._data;
				if (data.Class != indexClass || ignoreCase)
				{
					index = data.Class.GetValueIndex(name, ignoreCase, this);
					if (index == -2)
					{
						throw Error.AmbiguousMatchInExpandoObject(name);
					}
				}
				if (index == -1)
				{
					return false;
				}
				object obj = data[index];
				if (obj == ExpandoObject.Uninitialized)
				{
					return false;
				}
				if (deleteValue != ExpandoObject.Uninitialized && !object.Equals(obj, deleteValue))
				{
					return false;
				}
				data[index] = ExpandoObject.Uninitialized;
				this._count--;
			}
			PropertyChangedEventHandler propertyChanged = this._propertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(data.Class.Keys[index]));
			}
			return true;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0004E6D4 File Offset: 0x0004C8D4
		internal bool IsDeletedMember(int index)
		{
			return index != this._data.Length && this._data[index] == ExpandoObject.Uninitialized;
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x0004E6F9 File Offset: 0x0004C8F9
		internal ExpandoClass Class
		{
			get
			{
				return this._data.Class;
			}
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0004E706 File Offset: 0x0004C906
		private ExpandoObject.ExpandoData PromoteClassCore(ExpandoClass oldClass, ExpandoClass newClass)
		{
			if (this._data.Class == oldClass)
			{
				this._data = this._data.UpdateClass(newClass);
			}
			return this._data;
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0004E730 File Offset: 0x0004C930
		internal void PromoteClass(object oldClass, object newClass)
		{
			object lockObject = this.LockObject;
			lock (lockObject)
			{
				this.PromoteClassCore((ExpandoClass)oldClass, (ExpandoClass)newClass);
			}
		}

		/// <summary>The provided MetaObject will dispatch to the dynamic virtual methods. The object can be encapsulated inside another MetaObject to provide custom behavior for individual actions.</summary>
		/// <param name="parameter">The expression that represents the MetaObject to dispatch to the Dynamic virtual methods.</param>
		/// <returns>The object of the <see cref="T:System.Dynamic.DynamicMetaObject" /> type.</returns>
		// Token: 0x06001784 RID: 6020 RVA: 0x0004E780 File Offset: 0x0004C980
		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return new ExpandoObject.MetaExpando(parameter, this);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0004E789 File Offset: 0x0004C989
		private void TryAddMember(string key, object value)
		{
			ContractUtils.RequiresNotNull(key, "key");
			this.TrySetValue(null, -1, value, key, false, true);
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0004E7A2 File Offset: 0x0004C9A2
		private bool TryGetValueForKey(string key, out object value)
		{
			return this.TryGetValue(null, -1, key, false, out value);
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0004E7AF File Offset: 0x0004C9AF
		private bool ExpandoContainsKey(string key)
		{
			return this._data.Class.GetValueIndexCaseSensitive(key) >= 0;
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x0004E7C8 File Offset: 0x0004C9C8
		ICollection<string> IDictionary<string, object>.Keys
		{
			get
			{
				return new ExpandoObject.KeyCollection(this);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x0004E7D0 File Offset: 0x0004C9D0
		ICollection<object> IDictionary<string, object>.Values
		{
			get
			{
				return new ExpandoObject.ValueCollection(this);
			}
		}

		// Token: 0x1700040C RID: 1036
		object IDictionary<string, object>.this[string key]
		{
			get
			{
				object result;
				if (!this.TryGetValueForKey(key, out result))
				{
					throw Error.KeyDoesNotExistInExpando(key);
				}
				return result;
			}
			set
			{
				ContractUtils.RequiresNotNull(key, "key");
				this.TrySetValue(null, -1, value, key, false, false);
			}
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0004E811 File Offset: 0x0004CA11
		void IDictionary<string, object>.Add(string key, object value)
		{
			this.TryAddMember(key, value);
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0004E81C File Offset: 0x0004CA1C
		bool IDictionary<string, object>.ContainsKey(string key)
		{
			ContractUtils.RequiresNotNull(key, "key");
			ExpandoObject.ExpandoData data = this._data;
			int valueIndexCaseSensitive = data.Class.GetValueIndexCaseSensitive(key);
			return valueIndexCaseSensitive >= 0 && data[valueIndexCaseSensitive] != ExpandoObject.Uninitialized;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0004E85F File Offset: 0x0004CA5F
		bool IDictionary<string, object>.Remove(string key)
		{
			ContractUtils.RequiresNotNull(key, "key");
			return this.TryDeleteValue(null, -1, key, false, ExpandoObject.Uninitialized);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0004E87B File Offset: 0x0004CA7B
		bool IDictionary<string, object>.TryGetValue(string key, out object value)
		{
			return this.TryGetValueForKey(key, out value);
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0004E885 File Offset: 0x0004CA85
		int ICollection<KeyValuePair<string, object>>.Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x000023D1 File Offset: 0x000005D1
		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0004E88D File Offset: 0x0004CA8D
		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			this.TryAddMember(item.Key, item.Value);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0004E8A4 File Offset: 0x0004CAA4
		void ICollection<KeyValuePair<string, object>>.Clear()
		{
			object lockObject = this.LockObject;
			ExpandoObject.ExpandoData data;
			lock (lockObject)
			{
				data = this._data;
				this._data = ExpandoObject.ExpandoData.Empty;
				this._count = 0;
			}
			PropertyChangedEventHandler propertyChanged = this._propertyChanged;
			if (propertyChanged != null)
			{
				int i = 0;
				int num = data.Class.Keys.Length;
				while (i < num)
				{
					if (data[i] != ExpandoObject.Uninitialized)
					{
						propertyChanged(this, new PropertyChangedEventArgs(data.Class.Keys[i]));
					}
					i++;
				}
			}
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0004E94C File Offset: 0x0004CB4C
		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
		{
			object objA;
			return this.TryGetValueForKey(item.Key, out objA) && object.Equals(objA, item.Value);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0004E97C File Offset: 0x0004CB7C
		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			ContractUtils.RequiresNotNull(array, "array");
			object lockObject = this.LockObject;
			lock (lockObject)
			{
				ContractUtils.RequiresArrayRange<KeyValuePair<string, object>>(array, arrayIndex, this._count, "arrayIndex", "Count");
				foreach (KeyValuePair<string, object> keyValuePair in ((IEnumerable<KeyValuePair<string, object>>)this))
				{
					array[arrayIndex++] = keyValuePair;
				}
			}
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0004EA14 File Offset: 0x0004CC14
		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
		{
			return this.TryDeleteValue(null, -1, item.Key, false, item.Value);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0004EA30 File Offset: 0x0004CC30
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			ExpandoObject.ExpandoData data = this._data;
			return this.GetExpandoEnumerator(data, data.Version);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001798 RID: 6040 RVA: 0x0004EA54 File Offset: 0x0004CC54
		IEnumerator IEnumerable.GetEnumerator()
		{
			ExpandoObject.ExpandoData data = this._data;
			return this.GetExpandoEnumerator(data, data.Version);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0004EA75 File Offset: 0x0004CC75
		private IEnumerator<KeyValuePair<string, object>> GetExpandoEnumerator(ExpandoObject.ExpandoData data, int version)
		{
			int num;
			for (int i = 0; i < data.Class.Keys.Length; i = num + 1)
			{
				if (this._data.Version != version || data != this._data)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
				object obj = data[i];
				if (obj != ExpandoObject.Uninitialized)
				{
					yield return new KeyValuePair<string, object>(data.Class.Keys[i], obj);
				}
				num = i;
			}
			yield break;
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600179A RID: 6042 RVA: 0x0004EA92 File Offset: 0x0004CC92
		// (remove) Token: 0x0600179B RID: 6043 RVA: 0x0004EAAB File Offset: 0x0004CCAB
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				this._propertyChanged = (PropertyChangedEventHandler)Delegate.Combine(this._propertyChanged, value);
			}
			remove
			{
				this._propertyChanged = (PropertyChangedEventHandler)Delegate.Remove(this._propertyChanged, value);
			}
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0004EAC4 File Offset: 0x0004CCC4
		// Note: this type is marked as 'beforefieldinit'.
		static ExpandoObject()
		{
		}

		// Token: 0x04000B9B RID: 2971
		private static readonly MethodInfo s_expandoTryGetValue = typeof(RuntimeOps).GetMethod("ExpandoTryGetValue");

		// Token: 0x04000B9C RID: 2972
		private static readonly MethodInfo s_expandoTrySetValue = typeof(RuntimeOps).GetMethod("ExpandoTrySetValue");

		// Token: 0x04000B9D RID: 2973
		private static readonly MethodInfo s_expandoTryDeleteValue = typeof(RuntimeOps).GetMethod("ExpandoTryDeleteValue");

		// Token: 0x04000B9E RID: 2974
		private static readonly MethodInfo s_expandoPromoteClass = typeof(RuntimeOps).GetMethod("ExpandoPromoteClass");

		// Token: 0x04000B9F RID: 2975
		private static readonly MethodInfo s_expandoCheckVersion = typeof(RuntimeOps).GetMethod("ExpandoCheckVersion");

		// Token: 0x04000BA0 RID: 2976
		internal readonly object LockObject;

		// Token: 0x04000BA1 RID: 2977
		private ExpandoObject.ExpandoData _data;

		// Token: 0x04000BA2 RID: 2978
		private int _count;

		// Token: 0x04000BA3 RID: 2979
		internal static readonly object Uninitialized = new object();

		// Token: 0x04000BA4 RID: 2980
		internal const int AmbiguousMatchFound = -2;

		// Token: 0x04000BA5 RID: 2981
		internal const int NoMatch = -1;

		// Token: 0x04000BA6 RID: 2982
		private PropertyChangedEventHandler _propertyChanged;

		// Token: 0x0200030E RID: 782
		private sealed class KeyCollectionDebugView
		{
			// Token: 0x0600179D RID: 6045 RVA: 0x0004EB58 File Offset: 0x0004CD58
			public KeyCollectionDebugView(ICollection<string> collection)
			{
				ContractUtils.RequiresNotNull(collection, "collection");
				this._collection = collection;
			}

			// Token: 0x1700040F RID: 1039
			// (get) Token: 0x0600179E RID: 6046 RVA: 0x0004EB74 File Offset: 0x0004CD74
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public string[] Items
			{
				get
				{
					string[] array = new string[this._collection.Count];
					this._collection.CopyTo(array, 0);
					return array;
				}
			}

			// Token: 0x04000BA7 RID: 2983
			private readonly ICollection<string> _collection;
		}

		// Token: 0x0200030F RID: 783
		[DebuggerTypeProxy(typeof(ExpandoObject.KeyCollectionDebugView))]
		[DebuggerDisplay("Count = {Count}")]
		private class KeyCollection : ICollection<string>, IEnumerable<string>, IEnumerable
		{
			// Token: 0x0600179F RID: 6047 RVA: 0x0004EBA0 File Offset: 0x0004CDA0
			internal KeyCollection(ExpandoObject expando)
			{
				object lockObject = expando.LockObject;
				lock (lockObject)
				{
					this._expando = expando;
					this._expandoVersion = expando._data.Version;
					this._expandoCount = expando._count;
					this._expandoData = expando._data;
				}
			}

			// Token: 0x060017A0 RID: 6048 RVA: 0x0004EC10 File Offset: 0x0004CE10
			private void CheckVersion()
			{
				if (this._expando._data.Version != this._expandoVersion || this._expandoData != this._expando._data)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
			}

			// Token: 0x060017A1 RID: 6049 RVA: 0x0004EC43 File Offset: 0x0004CE43
			public void Add(string item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x060017A2 RID: 6050 RVA: 0x0004EC43 File Offset: 0x0004CE43
			public void Clear()
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x060017A3 RID: 6051 RVA: 0x0004EC4C File Offset: 0x0004CE4C
			public bool Contains(string item)
			{
				object lockObject = this._expando.LockObject;
				bool result;
				lock (lockObject)
				{
					this.CheckVersion();
					result = this._expando.ExpandoContainsKey(item);
				}
				return result;
			}

			// Token: 0x060017A4 RID: 6052 RVA: 0x0004ECA0 File Offset: 0x0004CEA0
			public void CopyTo(string[] array, int arrayIndex)
			{
				ContractUtils.RequiresNotNull(array, "array");
				ContractUtils.RequiresArrayRange<string>(array, arrayIndex, this._expandoCount, "arrayIndex", "Count");
				object lockObject = this._expando.LockObject;
				lock (lockObject)
				{
					this.CheckVersion();
					ExpandoObject.ExpandoData data = this._expando._data;
					for (int i = 0; i < data.Class.Keys.Length; i++)
					{
						if (data[i] != ExpandoObject.Uninitialized)
						{
							array[arrayIndex++] = data.Class.Keys[i];
						}
					}
				}
			}

			// Token: 0x17000410 RID: 1040
			// (get) Token: 0x060017A5 RID: 6053 RVA: 0x0004ED50 File Offset: 0x0004CF50
			public int Count
			{
				get
				{
					this.CheckVersion();
					return this._expandoCount;
				}
			}

			// Token: 0x17000411 RID: 1041
			// (get) Token: 0x060017A6 RID: 6054 RVA: 0x00007E1D File Offset: 0x0000601D
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060017A7 RID: 6055 RVA: 0x0004EC43 File Offset: 0x0004CE43
			public bool Remove(string item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x060017A8 RID: 6056 RVA: 0x0004ED5E File Offset: 0x0004CF5E
			public IEnumerator<string> GetEnumerator()
			{
				int i = 0;
				int j = this._expandoData.Class.Keys.Length;
				while (i < j)
				{
					this.CheckVersion();
					if (this._expandoData[i] != ExpandoObject.Uninitialized)
					{
						yield return this._expandoData.Class.Keys[i];
					}
					int num = i;
					i = num + 1;
				}
				yield break;
			}

			// Token: 0x060017A9 RID: 6057 RVA: 0x0004ED6D File Offset: 0x0004CF6D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000BA8 RID: 2984
			private readonly ExpandoObject _expando;

			// Token: 0x04000BA9 RID: 2985
			private readonly int _expandoVersion;

			// Token: 0x04000BAA RID: 2986
			private readonly int _expandoCount;

			// Token: 0x04000BAB RID: 2987
			private readonly ExpandoObject.ExpandoData _expandoData;

			// Token: 0x02000310 RID: 784
			[CompilerGenerated]
			private sealed class <GetEnumerator>d__15 : IEnumerator<string>, IDisposable, IEnumerator
			{
				// Token: 0x060017AA RID: 6058 RVA: 0x0004ED75 File Offset: 0x0004CF75
				[DebuggerHidden]
				public <GetEnumerator>d__15(int <>1__state)
				{
					this.<>1__state = <>1__state;
				}

				// Token: 0x060017AB RID: 6059 RVA: 0x00003A59 File Offset: 0x00001C59
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
				}

				// Token: 0x060017AC RID: 6060 RVA: 0x0004ED84 File Offset: 0x0004CF84
				bool IEnumerator.MoveNext()
				{
					int num = this.<>1__state;
					ExpandoObject.KeyCollection keyCollection = this;
					if (num == 0)
					{
						this.<>1__state = -1;
						i = 0;
						j = keyCollection._expandoData.Class.Keys.Length;
						goto IL_9A;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					IL_8A:
					int num2 = i;
					i = num2 + 1;
					IL_9A:
					if (i >= j)
					{
						return false;
					}
					keyCollection.CheckVersion();
					if (keyCollection._expandoData[i] != ExpandoObject.Uninitialized)
					{
						this.<>2__current = keyCollection._expandoData.Class.Keys[i];
						this.<>1__state = 1;
						return true;
					}
					goto IL_8A;
				}

				// Token: 0x17000412 RID: 1042
				// (get) Token: 0x060017AD RID: 6061 RVA: 0x0004EE3A File Offset: 0x0004D03A
				string IEnumerator<string>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x060017AE RID: 6062 RVA: 0x000080E3 File Offset: 0x000062E3
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x17000413 RID: 1043
				// (get) Token: 0x060017AF RID: 6063 RVA: 0x0004EE3A File Offset: 0x0004D03A
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x04000BAC RID: 2988
				private int <>1__state;

				// Token: 0x04000BAD RID: 2989
				private string <>2__current;

				// Token: 0x04000BAE RID: 2990
				public ExpandoObject.KeyCollection <>4__this;

				// Token: 0x04000BAF RID: 2991
				private int <i>5__2;

				// Token: 0x04000BB0 RID: 2992
				private int <n>5__3;
			}
		}

		// Token: 0x02000311 RID: 785
		private sealed class ValueCollectionDebugView
		{
			// Token: 0x060017B0 RID: 6064 RVA: 0x0004EE42 File Offset: 0x0004D042
			public ValueCollectionDebugView(ICollection<object> collection)
			{
				ContractUtils.RequiresNotNull(collection, "collection");
				this._collection = collection;
			}

			// Token: 0x17000414 RID: 1044
			// (get) Token: 0x060017B1 RID: 6065 RVA: 0x0004EE5C File Offset: 0x0004D05C
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					object[] array = new object[this._collection.Count];
					this._collection.CopyTo(array, 0);
					return array;
				}
			}

			// Token: 0x04000BB1 RID: 2993
			private readonly ICollection<object> _collection;
		}

		// Token: 0x02000312 RID: 786
		[DebuggerTypeProxy(typeof(ExpandoObject.ValueCollectionDebugView))]
		[DebuggerDisplay("Count = {Count}")]
		private class ValueCollection : ICollection<object>, IEnumerable<object>, IEnumerable
		{
			// Token: 0x060017B2 RID: 6066 RVA: 0x0004EE88 File Offset: 0x0004D088
			internal ValueCollection(ExpandoObject expando)
			{
				object lockObject = expando.LockObject;
				lock (lockObject)
				{
					this._expando = expando;
					this._expandoVersion = expando._data.Version;
					this._expandoCount = expando._count;
					this._expandoData = expando._data;
				}
			}

			// Token: 0x060017B3 RID: 6067 RVA: 0x0004EEF8 File Offset: 0x0004D0F8
			private void CheckVersion()
			{
				if (this._expando._data.Version != this._expandoVersion || this._expandoData != this._expando._data)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
			}

			// Token: 0x060017B4 RID: 6068 RVA: 0x0004EC43 File Offset: 0x0004CE43
			public void Add(object item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x060017B5 RID: 6069 RVA: 0x0004EC43 File Offset: 0x0004CE43
			public void Clear()
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x060017B6 RID: 6070 RVA: 0x0004EF2C File Offset: 0x0004D12C
			public bool Contains(object item)
			{
				object lockObject = this._expando.LockObject;
				bool result;
				lock (lockObject)
				{
					this.CheckVersion();
					ExpandoObject.ExpandoData data = this._expando._data;
					for (int i = 0; i < data.Class.Keys.Length; i++)
					{
						if (object.Equals(data[i], item))
						{
							return true;
						}
					}
					result = false;
				}
				return result;
			}

			// Token: 0x060017B7 RID: 6071 RVA: 0x0004EFB0 File Offset: 0x0004D1B0
			public void CopyTo(object[] array, int arrayIndex)
			{
				ContractUtils.RequiresNotNull(array, "array");
				ContractUtils.RequiresArrayRange<object>(array, arrayIndex, this._expandoCount, "arrayIndex", "Count");
				object lockObject = this._expando.LockObject;
				lock (lockObject)
				{
					this.CheckVersion();
					ExpandoObject.ExpandoData data = this._expando._data;
					for (int i = 0; i < data.Class.Keys.Length; i++)
					{
						if (data[i] != ExpandoObject.Uninitialized)
						{
							array[arrayIndex++] = data[i];
						}
					}
				}
			}

			// Token: 0x17000415 RID: 1045
			// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0004F058 File Offset: 0x0004D258
			public int Count
			{
				get
				{
					this.CheckVersion();
					return this._expandoCount;
				}
			}

			// Token: 0x17000416 RID: 1046
			// (get) Token: 0x060017B9 RID: 6073 RVA: 0x00007E1D File Offset: 0x0000601D
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060017BA RID: 6074 RVA: 0x0004EC43 File Offset: 0x0004CE43
			public bool Remove(object item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x060017BB RID: 6075 RVA: 0x0004F066 File Offset: 0x0004D266
			public IEnumerator<object> GetEnumerator()
			{
				ExpandoObject.ExpandoData data = this._expando._data;
				int num;
				for (int i = 0; i < data.Class.Keys.Length; i = num + 1)
				{
					this.CheckVersion();
					object obj = data[i];
					if (obj != ExpandoObject.Uninitialized)
					{
						yield return obj;
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x060017BC RID: 6076 RVA: 0x0004F075 File Offset: 0x0004D275
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000BB2 RID: 2994
			private readonly ExpandoObject _expando;

			// Token: 0x04000BB3 RID: 2995
			private readonly int _expandoVersion;

			// Token: 0x04000BB4 RID: 2996
			private readonly int _expandoCount;

			// Token: 0x04000BB5 RID: 2997
			private readonly ExpandoObject.ExpandoData _expandoData;

			// Token: 0x02000313 RID: 787
			[CompilerGenerated]
			private sealed class <GetEnumerator>d__15 : IEnumerator<object>, IDisposable, IEnumerator
			{
				// Token: 0x060017BD RID: 6077 RVA: 0x0004F07D File Offset: 0x0004D27D
				[DebuggerHidden]
				public <GetEnumerator>d__15(int <>1__state)
				{
					this.<>1__state = <>1__state;
				}

				// Token: 0x060017BE RID: 6078 RVA: 0x00003A59 File Offset: 0x00001C59
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
				}

				// Token: 0x060017BF RID: 6079 RVA: 0x0004F08C File Offset: 0x0004D28C
				bool IEnumerator.MoveNext()
				{
					int num = this.<>1__state;
					ExpandoObject.ValueCollection valueCollection = this;
					if (num == 0)
					{
						this.<>1__state = -1;
						data = valueCollection._expando._data;
						i = 0;
						goto IL_7F;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					IL_6F:
					int num2 = i;
					i = num2 + 1;
					IL_7F:
					if (i >= data.Class.Keys.Length)
					{
						return false;
					}
					valueCollection.CheckVersion();
					object obj = data[i];
					if (obj != ExpandoObject.Uninitialized)
					{
						this.<>2__current = obj;
						this.<>1__state = 1;
						return true;
					}
					goto IL_6F;
				}

				// Token: 0x17000417 RID: 1047
				// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0004F133 File Offset: 0x0004D333
				object IEnumerator<object>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x060017C1 RID: 6081 RVA: 0x000080E3 File Offset: 0x000062E3
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x17000418 RID: 1048
				// (get) Token: 0x060017C2 RID: 6082 RVA: 0x0004F133 File Offset: 0x0004D333
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x04000BB6 RID: 2998
				private int <>1__state;

				// Token: 0x04000BB7 RID: 2999
				private object <>2__current;

				// Token: 0x04000BB8 RID: 3000
				public ExpandoObject.ValueCollection <>4__this;

				// Token: 0x04000BB9 RID: 3001
				private ExpandoObject.ExpandoData <data>5__2;

				// Token: 0x04000BBA RID: 3002
				private int <i>5__3;
			}
		}

		// Token: 0x02000314 RID: 788
		private class MetaExpando : DynamicMetaObject
		{
			// Token: 0x060017C3 RID: 6083 RVA: 0x0004D617 File Offset: 0x0004B817
			public MetaExpando(Expression expression, ExpandoObject value) : base(expression, BindingRestrictions.Empty, value)
			{
			}

			// Token: 0x060017C4 RID: 6084 RVA: 0x0004F13C File Offset: 0x0004D33C
			private DynamicMetaObject BindGetOrInvokeMember(DynamicMetaObjectBinder binder, string name, bool ignoreCase, DynamicMetaObject fallback, Func<DynamicMetaObject, DynamicMetaObject> fallbackInvoke)
			{
				ExpandoClass @class = this.Value.Class;
				int valueIndex = @class.GetValueIndex(name, ignoreCase, this.Value);
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "value");
				Expression test = Expression.Call(ExpandoObject.s_expandoTryGetValue, new Expression[]
				{
					this.GetLimitedSelf(),
					Expression.Constant(@class, typeof(object)),
					Utils.Constant(valueIndex),
					Expression.Constant(name),
					Utils.Constant(ignoreCase),
					parameterExpression
				});
				DynamicMetaObject dynamicMetaObject = new DynamicMetaObject(parameterExpression, BindingRestrictions.Empty);
				if (fallbackInvoke != null)
				{
					dynamicMetaObject = fallbackInvoke(dynamicMetaObject);
				}
				dynamicMetaObject = new DynamicMetaObject(Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[]
				{
					parameterExpression
				}), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Condition(test, dynamicMetaObject.Expression, fallback.Expression, typeof(object))
				})), dynamicMetaObject.Restrictions.Merge(fallback.Restrictions));
				return this.AddDynamicTestAndDefer(binder, this.Value.Class, null, dynamicMetaObject);
			}

			// Token: 0x060017C5 RID: 6085 RVA: 0x0004F250 File Offset: 0x0004D450
			public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				return this.BindGetOrInvokeMember(binder, binder.Name, binder.IgnoreCase, binder.FallbackGetMember(this), null);
			}

			// Token: 0x060017C6 RID: 6086 RVA: 0x0004F278 File Offset: 0x0004D478
			public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				return this.BindGetOrInvokeMember(binder, binder.Name, binder.IgnoreCase, binder.FallbackInvokeMember(this, args), (DynamicMetaObject value) => binder.FallbackInvoke(value, args, null));
			}

			// Token: 0x060017C7 RID: 6087 RVA: 0x0004F2EC File Offset: 0x0004D4EC
			public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				ContractUtils.RequiresNotNull(value, "value");
				ExpandoClass expandoClass;
				int value2;
				ExpandoClass classEnsureIndex = this.GetClassEnsureIndex(binder.Name, binder.IgnoreCase, this.Value, out expandoClass, out value2);
				return this.AddDynamicTestAndDefer(binder, expandoClass, classEnsureIndex, new DynamicMetaObject(Expression.Call(ExpandoObject.s_expandoTrySetValue, new Expression[]
				{
					this.GetLimitedSelf(),
					Expression.Constant(expandoClass, typeof(object)),
					Utils.Constant(value2),
					Expression.Convert(value.Expression, typeof(object)),
					Expression.Constant(binder.Name),
					Utils.Constant(binder.IgnoreCase)
				}), BindingRestrictions.Empty));
			}

			// Token: 0x060017C8 RID: 6088 RVA: 0x0004F3A8 File Offset: 0x0004D5A8
			public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				int valueIndex = this.Value.Class.GetValueIndex(binder.Name, binder.IgnoreCase, this.Value);
				Expression expression = Expression.Call(ExpandoObject.s_expandoTryDeleteValue, this.GetLimitedSelf(), Expression.Constant(this.Value.Class, typeof(object)), Utils.Constant(valueIndex), Expression.Constant(binder.Name), Utils.Constant(binder.IgnoreCase));
				DynamicMetaObject dynamicMetaObject = binder.FallbackDeleteMember(this);
				DynamicMetaObject succeeds = new DynamicMetaObject(Expression.IfThen(Expression.Not(expression), dynamicMetaObject.Expression), dynamicMetaObject.Restrictions);
				return this.AddDynamicTestAndDefer(binder, this.Value.Class, null, succeeds);
			}

			// Token: 0x060017C9 RID: 6089 RVA: 0x0004F461 File Offset: 0x0004D661
			public override IEnumerable<string> GetDynamicMemberNames()
			{
				ExpandoObject.ExpandoData expandoData = this.Value._data;
				ExpandoClass klass = expandoData.Class;
				int num;
				for (int i = 0; i < klass.Keys.Length; i = num + 1)
				{
					if (expandoData[i] != ExpandoObject.Uninitialized)
					{
						yield return klass.Keys[i];
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x060017CA RID: 6090 RVA: 0x0004F474 File Offset: 0x0004D674
			private DynamicMetaObject AddDynamicTestAndDefer(DynamicMetaObjectBinder binder, ExpandoClass klass, ExpandoClass originalClass, DynamicMetaObject succeeds)
			{
				Expression expression = succeeds.Expression;
				if (originalClass != null)
				{
					expression = Expression.Block(Expression.Call(null, ExpandoObject.s_expandoPromoteClass, this.GetLimitedSelf(), Expression.Constant(originalClass, typeof(object)), Expression.Constant(klass, typeof(object))), succeeds.Expression);
				}
				return new DynamicMetaObject(Expression.Condition(Expression.Call(null, ExpandoObject.s_expandoCheckVersion, this.GetLimitedSelf(), Expression.Constant(originalClass ?? klass, typeof(object))), expression, binder.GetUpdateExpression(expression.Type)), this.GetRestrictions().Merge(succeeds.Restrictions));
			}

			// Token: 0x060017CB RID: 6091 RVA: 0x0004F51C File Offset: 0x0004D71C
			private ExpandoClass GetClassEnsureIndex(string name, bool caseInsensitive, ExpandoObject obj, out ExpandoClass klass, out int index)
			{
				ExpandoClass @class = this.Value.Class;
				index = @class.GetValueIndex(name, caseInsensitive, obj);
				if (index == -2)
				{
					klass = @class;
					return null;
				}
				if (index == -1)
				{
					ExpandoClass expandoClass = @class.FindNewClass(name);
					klass = expandoClass;
					index = expandoClass.GetValueIndexCaseSensitive(name);
					return @class;
				}
				klass = @class;
				return null;
			}

			// Token: 0x060017CC RID: 6092 RVA: 0x0004F571 File Offset: 0x0004D771
			private Expression GetLimitedSelf()
			{
				if (TypeUtils.AreEquivalent(base.Expression.Type, base.LimitType))
				{
					return base.Expression;
				}
				return Expression.Convert(base.Expression, base.LimitType);
			}

			// Token: 0x060017CD RID: 6093 RVA: 0x0004E0BE File Offset: 0x0004C2BE
			private BindingRestrictions GetRestrictions()
			{
				return BindingRestrictions.GetTypeRestriction(this);
			}

			// Token: 0x17000419 RID: 1049
			// (get) Token: 0x060017CE RID: 6094 RVA: 0x0004F5A3 File Offset: 0x0004D7A3
			public new ExpandoObject Value
			{
				get
				{
					return (ExpandoObject)base.Value;
				}
			}

			// Token: 0x02000315 RID: 789
			[CompilerGenerated]
			private sealed class <>c__DisplayClass3_0
			{
				// Token: 0x060017CF RID: 6095 RVA: 0x00002162 File Offset: 0x00000362
				public <>c__DisplayClass3_0()
				{
				}

				// Token: 0x060017D0 RID: 6096 RVA: 0x0004F5B0 File Offset: 0x0004D7B0
				internal DynamicMetaObject <BindInvokeMember>b__0(DynamicMetaObject value)
				{
					return this.binder.FallbackInvoke(value, this.args, null);
				}

				// Token: 0x04000BBB RID: 3003
				public InvokeMemberBinder binder;

				// Token: 0x04000BBC RID: 3004
				public DynamicMetaObject[] args;
			}

			// Token: 0x02000316 RID: 790
			[CompilerGenerated]
			private sealed class <GetDynamicMemberNames>d__6 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
			{
				// Token: 0x060017D1 RID: 6097 RVA: 0x0004F5C5 File Offset: 0x0004D7C5
				[DebuggerHidden]
				public <GetDynamicMemberNames>d__6(int <>1__state)
				{
					this.<>1__state = <>1__state;
					this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
				}

				// Token: 0x060017D2 RID: 6098 RVA: 0x00003A59 File Offset: 0x00001C59
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
				}

				// Token: 0x060017D3 RID: 6099 RVA: 0x0004F5E0 File Offset: 0x0004D7E0
				bool IEnumerator.MoveNext()
				{
					int num = this.<>1__state;
					ExpandoObject.MetaExpando metaExpando = this;
					if (num == 0)
					{
						this.<>1__state = -1;
						expandoData = metaExpando.Value._data;
						klass = expandoData.Class;
						i = 0;
						goto IL_99;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					IL_89:
					int num2 = i;
					i = num2 + 1;
					IL_99:
					if (i >= klass.Keys.Length)
					{
						return false;
					}
					if (expandoData[i] != ExpandoObject.Uninitialized)
					{
						this.<>2__current = klass.Keys[i];
						this.<>1__state = 1;
						return true;
					}
					goto IL_89;
				}

				// Token: 0x1700041A RID: 1050
				// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0004F69C File Offset: 0x0004D89C
				string IEnumerator<string>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x060017D5 RID: 6101 RVA: 0x000080E3 File Offset: 0x000062E3
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x1700041B RID: 1051
				// (get) Token: 0x060017D6 RID: 6102 RVA: 0x0004F69C File Offset: 0x0004D89C
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x060017D7 RID: 6103 RVA: 0x0004F6A4 File Offset: 0x0004D8A4
				[DebuggerHidden]
				IEnumerator<string> IEnumerable<string>.GetEnumerator()
				{
					ExpandoObject.MetaExpando.<GetDynamicMemberNames>d__6 <GetDynamicMemberNames>d__;
					if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
					{
						this.<>1__state = 0;
						<GetDynamicMemberNames>d__ = this;
					}
					else
					{
						<GetDynamicMemberNames>d__ = new ExpandoObject.MetaExpando.<GetDynamicMemberNames>d__6(0);
						<GetDynamicMemberNames>d__.<>4__this = this;
					}
					return <GetDynamicMemberNames>d__;
				}

				// Token: 0x060017D8 RID: 6104 RVA: 0x0004F6E7 File Offset: 0x0004D8E7
				[DebuggerHidden]
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
				}

				// Token: 0x04000BBD RID: 3005
				private int <>1__state;

				// Token: 0x04000BBE RID: 3006
				private string <>2__current;

				// Token: 0x04000BBF RID: 3007
				private int <>l__initialThreadId;

				// Token: 0x04000BC0 RID: 3008
				public ExpandoObject.MetaExpando <>4__this;

				// Token: 0x04000BC1 RID: 3009
				private ExpandoObject.ExpandoData <expandoData>5__2;

				// Token: 0x04000BC2 RID: 3010
				private ExpandoClass <klass>5__3;

				// Token: 0x04000BC3 RID: 3011
				private int <i>5__4;
			}
		}

		// Token: 0x02000317 RID: 791
		private class ExpandoData
		{
			// Token: 0x1700041C RID: 1052
			internal object this[int index]
			{
				get
				{
					return this._dataArray[index];
				}
				set
				{
					this._version++;
					this._dataArray[index] = value;
				}
			}

			// Token: 0x1700041D RID: 1053
			// (get) Token: 0x060017DB RID: 6107 RVA: 0x0004F712 File Offset: 0x0004D912
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x1700041E RID: 1054
			// (get) Token: 0x060017DC RID: 6108 RVA: 0x0004F71A File Offset: 0x0004D91A
			internal int Length
			{
				get
				{
					return this._dataArray.Length;
				}
			}

			// Token: 0x060017DD RID: 6109 RVA: 0x0004F724 File Offset: 0x0004D924
			private ExpandoData()
			{
				this.Class = ExpandoClass.Empty;
				this._dataArray = Array.Empty<object>();
			}

			// Token: 0x060017DE RID: 6110 RVA: 0x0004F742 File Offset: 0x0004D942
			internal ExpandoData(ExpandoClass klass, object[] data, int version)
			{
				this.Class = klass;
				this._dataArray = data;
				this._version = version;
			}

			// Token: 0x060017DF RID: 6111 RVA: 0x0004F760 File Offset: 0x0004D960
			internal ExpandoObject.ExpandoData UpdateClass(ExpandoClass newClass)
			{
				if (this._dataArray.Length >= newClass.Keys.Length)
				{
					this[newClass.Keys.Length - 1] = ExpandoObject.Uninitialized;
					return new ExpandoObject.ExpandoData(newClass, this._dataArray, this._version);
				}
				int index = this._dataArray.Length;
				object[] array = new object[ExpandoObject.ExpandoData.GetAlignedSize(newClass.Keys.Length)];
				Array.Copy(this._dataArray, 0, array, 0, this._dataArray.Length);
				ExpandoObject.ExpandoData expandoData = new ExpandoObject.ExpandoData(newClass, array, this._version);
				expandoData[index] = ExpandoObject.Uninitialized;
				return expandoData;
			}

			// Token: 0x060017E0 RID: 6112 RVA: 0x0004F7F2 File Offset: 0x0004D9F2
			private static int GetAlignedSize(int len)
			{
				return len + 7 & -8;
			}

			// Token: 0x060017E1 RID: 6113 RVA: 0x0004F7FA File Offset: 0x0004D9FA
			// Note: this type is marked as 'beforefieldinit'.
			static ExpandoData()
			{
			}

			// Token: 0x04000BC4 RID: 3012
			internal static ExpandoObject.ExpandoData Empty = new ExpandoObject.ExpandoData();

			// Token: 0x04000BC5 RID: 3013
			internal readonly ExpandoClass Class;

			// Token: 0x04000BC6 RID: 3014
			private readonly object[] _dataArray;

			// Token: 0x04000BC7 RID: 3015
			private int _version;
		}

		// Token: 0x02000318 RID: 792
		[CompilerGenerated]
		private sealed class <GetExpandoEnumerator>d__51 : IEnumerator<KeyValuePair<string, object>>, IDisposable, IEnumerator
		{
			// Token: 0x060017E2 RID: 6114 RVA: 0x0004F806 File Offset: 0x0004DA06
			[DebuggerHidden]
			public <GetExpandoEnumerator>d__51(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060017E3 RID: 6115 RVA: 0x00003A59 File Offset: 0x00001C59
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060017E4 RID: 6116 RVA: 0x0004F818 File Offset: 0x0004DA18
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ExpandoObject expandoObject = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					i = 0;
					goto IL_B1;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_A1:
				int num2 = i;
				i = num2 + 1;
				IL_B1:
				if (i >= data.Class.Keys.Length)
				{
					return false;
				}
				if (expandoObject._data.Version != version || data != expandoObject._data)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
				object obj = data[i];
				if (obj != ExpandoObject.Uninitialized)
				{
					this.<>2__current = new KeyValuePair<string, object>(data.Class.Keys[i], obj);
					this.<>1__state = 1;
					return true;
				}
				goto IL_A1;
			}

			// Token: 0x1700041F RID: 1055
			// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0004F8F4 File Offset: 0x0004DAF4
			KeyValuePair<string, object> IEnumerator<KeyValuePair<string, object>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060017E6 RID: 6118 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000420 RID: 1056
			// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0004F8FC File Offset: 0x0004DAFC
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000BC8 RID: 3016
			private int <>1__state;

			// Token: 0x04000BC9 RID: 3017
			private KeyValuePair<string, object> <>2__current;

			// Token: 0x04000BCA RID: 3018
			public ExpandoObject <>4__this;

			// Token: 0x04000BCB RID: 3019
			public int version;

			// Token: 0x04000BCC RID: 3020
			public ExpandoObject.ExpandoData data;

			// Token: 0x04000BCD RID: 3021
			private int <i>5__2;
		}
	}
}
