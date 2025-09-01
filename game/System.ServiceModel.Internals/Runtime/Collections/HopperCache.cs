using System;
using System.Collections;
using System.Threading;

namespace System.Runtime.Collections
{
	// Token: 0x02000050 RID: 80
	internal class HopperCache
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x0000FB29 File Offset: 0x0000DD29
		public HopperCache(int hopperSize, bool weak)
		{
			this.hopperSize = hopperSize;
			this.weak = weak;
			this.outstandingHopper = new Hashtable(hopperSize * 2);
			this.strongHopper = new Hashtable(hopperSize * 2);
			this.limitedHopper = new Hashtable(hopperSize * 2);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000FB6C File Offset: 0x0000DD6C
		public void Add(object key, object value)
		{
			if (this.weak && value != DBNull.Value)
			{
				value = new WeakReference(value);
			}
			if (this.strongHopper.Count >= this.hopperSize * 2)
			{
				Hashtable hashtable = this.limitedHopper;
				hashtable.Clear();
				hashtable.Add(key, value);
				try
				{
					return;
				}
				finally
				{
					this.limitedHopper = this.strongHopper;
					this.strongHopper = hashtable;
				}
			}
			this.strongHopper[key] = value;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		public object GetValue(object syncObject, object key)
		{
			HopperCache.LastHolder lastHolder = this.mruEntry;
			WeakReference weakReference;
			object obj;
			if (lastHolder != null && key.Equals(lastHolder.Key))
			{
				if (!this.weak || (weakReference = (lastHolder.Value as WeakReference)) == null)
				{
					return lastHolder.Value;
				}
				obj = weakReference.Target;
				if (obj != null)
				{
					return obj;
				}
				this.mruEntry = null;
			}
			object obj2 = this.outstandingHopper[key];
			obj = ((this.weak && (weakReference = (obj2 as WeakReference)) != null) ? weakReference.Target : obj2);
			if (obj != null)
			{
				this.mruEntry = new HopperCache.LastHolder(key, obj2);
				return obj;
			}
			obj2 = this.strongHopper[key];
			obj = ((this.weak && (weakReference = (obj2 as WeakReference)) != null) ? weakReference.Target : obj2);
			if (obj == null)
			{
				obj2 = this.limitedHopper[key];
				obj = ((this.weak && (weakReference = (obj2 as WeakReference)) != null) ? weakReference.Target : obj2);
				if (obj == null)
				{
					return null;
				}
			}
			this.mruEntry = new HopperCache.LastHolder(key, obj2);
			int num = 1;
			try
			{
				try
				{
				}
				finally
				{
					num = Interlocked.CompareExchange(ref this.promoting, 1, 0);
				}
				if (num == 0)
				{
					if (this.outstandingHopper.Count >= this.hopperSize)
					{
						lock (syncObject)
						{
							Hashtable hashtable = this.limitedHopper;
							hashtable.Clear();
							hashtable.Add(key, obj2);
							try
							{
								return obj;
							}
							finally
							{
								this.limitedHopper = this.strongHopper;
								this.strongHopper = this.outstandingHopper;
								this.outstandingHopper = hashtable;
							}
						}
					}
					this.outstandingHopper[key] = obj2;
				}
			}
			finally
			{
				if (num == 0)
				{
					this.promoting = 0;
				}
			}
			return obj;
		}

		// Token: 0x040001E9 RID: 489
		private readonly int hopperSize;

		// Token: 0x040001EA RID: 490
		private readonly bool weak;

		// Token: 0x040001EB RID: 491
		private Hashtable outstandingHopper;

		// Token: 0x040001EC RID: 492
		private Hashtable strongHopper;

		// Token: 0x040001ED RID: 493
		private Hashtable limitedHopper;

		// Token: 0x040001EE RID: 494
		private int promoting;

		// Token: 0x040001EF RID: 495
		private HopperCache.LastHolder mruEntry;

		// Token: 0x02000093 RID: 147
		private class LastHolder
		{
			// Token: 0x06000412 RID: 1042 RVA: 0x00012FC6 File Offset: 0x000111C6
			internal LastHolder(object key, object value)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x06000413 RID: 1043 RVA: 0x00012FDC File Offset: 0x000111DC
			internal object Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x06000414 RID: 1044 RVA: 0x00012FE4 File Offset: 0x000111E4
			internal object Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x040002F7 RID: 759
			private readonly object key;

			// Token: 0x040002F8 RID: 760
			private readonly object value;
		}
	}
}
