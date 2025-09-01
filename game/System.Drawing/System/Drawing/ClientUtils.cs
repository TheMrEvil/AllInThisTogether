using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Drawing
{
	// Token: 0x02000019 RID: 25
	internal static class ClientUtils
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00003A0C File Offset: 0x00001C0C
		public static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is ExecutionEngineException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003A49 File Offset: 0x00001C49
		public static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || ClientUtils.IsCriticalException(ex);
		}

		// Token: 0x0200001A RID: 26
		internal class WeakRefCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06000051 RID: 81 RVA: 0x00003A5B File Offset: 0x00001C5B
			internal WeakRefCollection() : this(4)
			{
			}

			// Token: 0x06000052 RID: 82 RVA: 0x00003A64 File Offset: 0x00001C64
			internal WeakRefCollection(int size)
			{
				this.InnerList = new ArrayList(size);
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000053 RID: 83 RVA: 0x00003A83 File Offset: 0x00001C83
			internal ArrayList InnerList
			{
				[CompilerGenerated]
				get
				{
					return this.<InnerList>k__BackingField;
				}
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000054 RID: 84 RVA: 0x00003A8B File Offset: 0x00001C8B
			// (set) Token: 0x06000055 RID: 85 RVA: 0x00003A93 File Offset: 0x00001C93
			public int RefCheckThreshold
			{
				[CompilerGenerated]
				get
				{
					return this.<RefCheckThreshold>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<RefCheckThreshold>k__BackingField = value;
				}
			} = int.MaxValue;

			// Token: 0x17000006 RID: 6
			public object this[int index]
			{
				get
				{
					ClientUtils.WeakRefCollection.WeakRefObject weakRefObject = this.InnerList[index] as ClientUtils.WeakRefCollection.WeakRefObject;
					if (weakRefObject != null && weakRefObject.IsAlive)
					{
						return weakRefObject.Target;
					}
					return null;
				}
				set
				{
					this.InnerList[index] = this.CreateWeakRefObject(value);
				}
			}

			// Token: 0x06000058 RID: 88 RVA: 0x00003AE4 File Offset: 0x00001CE4
			public void ScavengeReferences()
			{
				int num = 0;
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					if (this[num] == null)
					{
						this.InnerList.RemoveAt(num);
					}
					else
					{
						num++;
					}
				}
			}

			// Token: 0x06000059 RID: 89 RVA: 0x00003B24 File Offset: 0x00001D24
			public override bool Equals(object obj)
			{
				ClientUtils.WeakRefCollection weakRefCollection = obj as ClientUtils.WeakRefCollection;
				if (weakRefCollection == null)
				{
					return true;
				}
				if (weakRefCollection == null || this.Count != weakRefCollection.Count)
				{
					return false;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (this.InnerList[i] != weakRefCollection.InnerList[i] && (this.InnerList[i] == null || !this.InnerList[i].Equals(weakRefCollection.InnerList[i])))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00003BAB File Offset: 0x00001DAB
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x0600005B RID: 91 RVA: 0x00003BB3 File Offset: 0x00001DB3
			private ClientUtils.WeakRefCollection.WeakRefObject CreateWeakRefObject(object value)
			{
				if (value == null)
				{
					return null;
				}
				return new ClientUtils.WeakRefCollection.WeakRefObject(value);
			}

			// Token: 0x0600005C RID: 92 RVA: 0x00003BC0 File Offset: 0x00001DC0
			private static void Copy(ClientUtils.WeakRefCollection sourceList, int sourceIndex, ClientUtils.WeakRefCollection destinationList, int destinationIndex, int length)
			{
				if (sourceIndex < destinationIndex)
				{
					sourceIndex += length;
					destinationIndex += length;
					while (length > 0)
					{
						destinationList.InnerList[--destinationIndex] = sourceList.InnerList[--sourceIndex];
						length--;
					}
					return;
				}
				while (length > 0)
				{
					destinationList.InnerList[destinationIndex++] = sourceList.InnerList[sourceIndex++];
					length--;
				}
			}

			// Token: 0x0600005D RID: 93 RVA: 0x00003C3C File Offset: 0x00001E3C
			public void RemoveByHashCode(object value)
			{
				if (value == null)
				{
					return;
				}
				int hashCode = value.GetHashCode();
				for (int i = 0; i < this.InnerList.Count; i++)
				{
					if (this.InnerList[i] != null && this.InnerList[i].GetHashCode() == hashCode)
					{
						this.RemoveAt(i);
						return;
					}
				}
			}

			// Token: 0x0600005E RID: 94 RVA: 0x00003C94 File Offset: 0x00001E94
			public void Clear()
			{
				this.InnerList.Clear();
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600005F RID: 95 RVA: 0x00003CA1 File Offset: 0x00001EA1
			public bool IsFixedSize
			{
				get
				{
					return this.InnerList.IsFixedSize;
				}
			}

			// Token: 0x06000060 RID: 96 RVA: 0x00003CAE File Offset: 0x00001EAE
			public bool Contains(object value)
			{
				return this.InnerList.Contains(this.CreateWeakRefObject(value));
			}

			// Token: 0x06000061 RID: 97 RVA: 0x00003CC2 File Offset: 0x00001EC2
			public void RemoveAt(int index)
			{
				this.InnerList.RemoveAt(index);
			}

			// Token: 0x06000062 RID: 98 RVA: 0x00003CD0 File Offset: 0x00001ED0
			public void Remove(object value)
			{
				this.InnerList.Remove(this.CreateWeakRefObject(value));
			}

			// Token: 0x06000063 RID: 99 RVA: 0x00003CE4 File Offset: 0x00001EE4
			public int IndexOf(object value)
			{
				return this.InnerList.IndexOf(this.CreateWeakRefObject(value));
			}

			// Token: 0x06000064 RID: 100 RVA: 0x00003CF8 File Offset: 0x00001EF8
			public void Insert(int index, object value)
			{
				this.InnerList.Insert(index, this.CreateWeakRefObject(value));
			}

			// Token: 0x06000065 RID: 101 RVA: 0x00003D0D File Offset: 0x00001F0D
			public int Add(object value)
			{
				if (this.Count > this.RefCheckThreshold)
				{
					this.ScavengeReferences();
				}
				return this.InnerList.Add(this.CreateWeakRefObject(value));
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000066 RID: 102 RVA: 0x00003D35 File Offset: 0x00001F35
			public int Count
			{
				get
				{
					return this.InnerList.Count;
				}
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000067 RID: 103 RVA: 0x00003D42 File Offset: 0x00001F42
			object ICollection.SyncRoot
			{
				get
				{
					return this.InnerList.SyncRoot;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000068 RID: 104 RVA: 0x00003D4F File Offset: 0x00001F4F
			public bool IsReadOnly
			{
				get
				{
					return this.InnerList.IsReadOnly;
				}
			}

			// Token: 0x06000069 RID: 105 RVA: 0x00003D5C File Offset: 0x00001F5C
			public void CopyTo(Array array, int index)
			{
				this.InnerList.CopyTo(array, index);
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x0600006A RID: 106 RVA: 0x00003D6B File Offset: 0x00001F6B
			bool ICollection.IsSynchronized
			{
				get
				{
					return this.InnerList.IsSynchronized;
				}
			}

			// Token: 0x0600006B RID: 107 RVA: 0x00003D78 File Offset: 0x00001F78
			public IEnumerator GetEnumerator()
			{
				return this.InnerList.GetEnumerator();
			}

			// Token: 0x04000151 RID: 337
			[CompilerGenerated]
			private readonly ArrayList <InnerList>k__BackingField;

			// Token: 0x04000152 RID: 338
			[CompilerGenerated]
			private int <RefCheckThreshold>k__BackingField;

			// Token: 0x0200001B RID: 27
			internal class WeakRefObject
			{
				// Token: 0x0600006C RID: 108 RVA: 0x00003D85 File Offset: 0x00001F85
				internal WeakRefObject(object obj)
				{
					this._weakHolder = new WeakReference(obj);
					this._hash = obj.GetHashCode();
				}

				// Token: 0x1700000C RID: 12
				// (get) Token: 0x0600006D RID: 109 RVA: 0x00003DA5 File Offset: 0x00001FA5
				internal bool IsAlive
				{
					get
					{
						return this._weakHolder.IsAlive;
					}
				}

				// Token: 0x1700000D RID: 13
				// (get) Token: 0x0600006E RID: 110 RVA: 0x00003DB2 File Offset: 0x00001FB2
				internal object Target
				{
					get
					{
						return this._weakHolder.Target;
					}
				}

				// Token: 0x0600006F RID: 111 RVA: 0x00003DBF File Offset: 0x00001FBF
				public override int GetHashCode()
				{
					return this._hash;
				}

				// Token: 0x06000070 RID: 112 RVA: 0x00003DC8 File Offset: 0x00001FC8
				public override bool Equals(object obj)
				{
					ClientUtils.WeakRefCollection.WeakRefObject weakRefObject = obj as ClientUtils.WeakRefCollection.WeakRefObject;
					return weakRefObject == this || (weakRefObject != null && (weakRefObject.Target == this.Target || (this.Target != null && this.Target.Equals(weakRefObject.Target))));
				}

				// Token: 0x04000153 RID: 339
				private int _hash;

				// Token: 0x04000154 RID: 340
				private WeakReference _weakHolder;
			}
		}
	}
}
