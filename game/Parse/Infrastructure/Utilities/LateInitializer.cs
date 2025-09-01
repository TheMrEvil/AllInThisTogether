using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x02000057 RID: 87
	internal class LateInitializer
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
		private Lazy<Dictionary<Func<object>, object>> Storage
		{
			[CompilerGenerated]
			get
			{
				return this.<Storage>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Storage>k__BackingField = value;
			}
		} = new Lazy<Dictionary<Func<object>, object>>();

		// Token: 0x0600043F RID: 1087 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public TData GetValue<TData>(Func<TData> generator)
		{
			TData result;
			lock (generator)
			{
				if (this.Storage.IsValueCreated)
				{
					Func<TData> func = this.Storage.Value.Keys.OfType<Func<TData>>().FirstOrDefault<Func<TData>>();
					object obj;
					if (func != null && this.Storage.Value.TryGetValue(func as Func<object>, out obj))
					{
						return (TData)((object)obj);
					}
				}
				TData tdata = generator();
				this.Storage.Value.Add(generator as Func<object>, tdata);
				result = tdata;
			}
			return result;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000D694 File Offset: 0x0000B894
		public bool ClearValue<TData>()
		{
			Lazy<Dictionary<Func<object>, object>> storage = this.Storage;
			lock (storage)
			{
				if (this.Storage.IsValueCreated)
				{
					Func<TData> func = this.Storage.Value.Keys.OfType<Func<TData>>().FirstOrDefault<Func<TData>>();
					if (func != null)
					{
						Func<TData> obj = func;
						lock (obj)
						{
							this.Storage.Value.Remove(func as Func<object>);
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000D740 File Offset: 0x0000B940
		public bool SetValue<TData>(TData value, bool initialize = true)
		{
			Lazy<Dictionary<Func<object>, object>> storage = this.Storage;
			lock (storage)
			{
				if (this.Storage.IsValueCreated)
				{
					Func<TData> func = this.Storage.Value.Keys.OfType<Func<TData>>().FirstOrDefault<Func<TData>>();
					if (func != null)
					{
						Func<TData> obj = func;
						lock (obj)
						{
							this.Storage.Value[func as Func<object>] = value;
							return true;
						}
					}
				}
				if (initialize)
				{
					this.Storage.Value[(() => value) as Func<object>] = value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000D838 File Offset: 0x0000BA38
		public bool Reset()
		{
			Lazy<Dictionary<Func<object>, object>> storage = this.Storage;
			lock (storage)
			{
				if (this.Storage.IsValueCreated)
				{
					this.Storage.Value.Clear();
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000D898 File Offset: 0x0000BA98
		public bool Used
		{
			get
			{
				return this.Storage.IsValueCreated;
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000D8A5 File Offset: 0x0000BAA5
		public LateInitializer()
		{
		}

		// Token: 0x040000D7 RID: 215
		[CompilerGenerated]
		private Lazy<Dictionary<Func<object>, object>> <Storage>k__BackingField;

		// Token: 0x02000128 RID: 296
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0<TData>
		{
			// Token: 0x0600078B RID: 1931 RVA: 0x00016FF1 File Offset: 0x000151F1
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x0600078C RID: 1932 RVA: 0x00016FF9 File Offset: 0x000151F9
			internal TData <SetValue>b__0()
			{
				return this.value;
			}

			// Token: 0x040002B1 RID: 689
			public TData value;
		}
	}
}
