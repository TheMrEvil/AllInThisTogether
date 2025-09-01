using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000438 RID: 1080
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class WeakHashtable : Hashtable
	{
		// Token: 0x0600236D RID: 9069 RVA: 0x00080D7A File Offset: 0x0007EF7A
		internal WeakHashtable() : base(WeakHashtable._comparer)
		{
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x00080D87 File Offset: 0x0007EF87
		public override void Clear()
		{
			base.Clear();
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x00080D8F File Offset: 0x0007EF8F
		public override void Remove(object key)
		{
			base.Remove(key);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x00080D98 File Offset: 0x0007EF98
		public void SetWeak(object key, object value)
		{
			this.ScavengeKeys();
			this[new WeakHashtable.EqualityWeakReference(key)] = value;
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x00080DB0 File Offset: 0x0007EFB0
		private void ScavengeKeys()
		{
			int count = this.Count;
			if (count == 0)
			{
				return;
			}
			if (this._lastHashCount == 0)
			{
				this._lastHashCount = count;
				return;
			}
			long totalMemory = GC.GetTotalMemory(false);
			if (this._lastGlobalMem == 0L)
			{
				this._lastGlobalMem = totalMemory;
				return;
			}
			float num = (float)(totalMemory - this._lastGlobalMem) / (float)this._lastGlobalMem;
			float num2 = (float)(count - this._lastHashCount) / (float)this._lastHashCount;
			if (num < 0f && num2 >= 0f)
			{
				ArrayList arrayList = null;
				foreach (object obj in this.Keys)
				{
					WeakReference weakReference = obj as WeakReference;
					if (weakReference != null && !weakReference.IsAlive)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(weakReference);
					}
				}
				if (arrayList != null)
				{
					foreach (object key in arrayList)
					{
						this.Remove(key);
					}
				}
			}
			this._lastGlobalMem = totalMemory;
			this._lastHashCount = count;
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x00080EF0 File Offset: 0x0007F0F0
		// Note: this type is marked as 'beforefieldinit'.
		static WeakHashtable()
		{
		}

		// Token: 0x040010A6 RID: 4262
		private static IEqualityComparer _comparer = new WeakHashtable.WeakKeyComparer();

		// Token: 0x040010A7 RID: 4263
		private long _lastGlobalMem;

		// Token: 0x040010A8 RID: 4264
		private int _lastHashCount;

		// Token: 0x02000439 RID: 1081
		private class WeakKeyComparer : IEqualityComparer
		{
			// Token: 0x06002373 RID: 9075 RVA: 0x00080EFC File Offset: 0x0007F0FC
			bool IEqualityComparer.Equals(object x, object y)
			{
				if (x == null)
				{
					return y == null;
				}
				if (y != null && x.GetHashCode() == y.GetHashCode())
				{
					WeakReference weakReference = x as WeakReference;
					WeakReference weakReference2 = y as WeakReference;
					if (weakReference != null)
					{
						if (!weakReference.IsAlive)
						{
							return false;
						}
						x = weakReference.Target;
					}
					if (weakReference2 != null)
					{
						if (!weakReference2.IsAlive)
						{
							return false;
						}
						y = weakReference2.Target;
					}
					return x == y;
				}
				return false;
			}

			// Token: 0x06002374 RID: 9076 RVA: 0x00080F60 File Offset: 0x0007F160
			int IEqualityComparer.GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}

			// Token: 0x06002375 RID: 9077 RVA: 0x0000219B File Offset: 0x0000039B
			public WeakKeyComparer()
			{
			}
		}

		// Token: 0x0200043A RID: 1082
		private sealed class EqualityWeakReference : WeakReference
		{
			// Token: 0x06002376 RID: 9078 RVA: 0x00080F68 File Offset: 0x0007F168
			internal EqualityWeakReference(object o) : base(o)
			{
				this._hashCode = o.GetHashCode();
			}

			// Token: 0x06002377 RID: 9079 RVA: 0x00080F7D File Offset: 0x0007F17D
			public override bool Equals(object o)
			{
				return o != null && o.GetHashCode() == this._hashCode && (o == this || (this.IsAlive && o == this.Target));
			}

			// Token: 0x06002378 RID: 9080 RVA: 0x00080FAC File Offset: 0x0007F1AC
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x040010A9 RID: 4265
			private int _hashCode;
		}
	}
}
