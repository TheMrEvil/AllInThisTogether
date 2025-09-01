using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Reflectrify
{
	// Token: 0x0200000B RID: 11
	public class TypeCache : ICache
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000255C File Offset: 0x0000075C
		public void Clear()
		{
			this.cache.Clear();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000256C File Offset: 0x0000076C
		public MethodInfo[] GetOrAddMethods(Type type, BindingFlags flags, bool inherited)
		{
			return this.cache.GetOrAdd(new ValueTuple<Type, BindingFlags?>(type, new BindingFlags?(flags)), delegate(ValueTuple<Type, BindingFlags?> tuple)
			{
				if (!inherited)
				{
					return type.GetMethods(flags);
				}
				return type.GetMethods(flags | BindingFlags.FlattenHierarchy);
			});
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025C1 File Offset: 0x000007C1
		public TypeCache()
		{
		}

		// Token: 0x04000009 RID: 9
		private ConcurrentDictionary<ValueTuple<Type, BindingFlags?>, MethodInfo[]> cache = new ConcurrentDictionary<ValueTuple<Type, BindingFlags?>, MethodInfo[]>();

		// Token: 0x02000015 RID: 21
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x06000054 RID: 84 RVA: 0x0000373B File Offset: 0x0000193B
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x06000055 RID: 85 RVA: 0x00003743 File Offset: 0x00001943
			internal MethodInfo[] <GetOrAddMethods>b__0(ValueTuple<Type, BindingFlags?> tuple)
			{
				if (!this.inherited)
				{
					return this.type.GetMethods(this.flags);
				}
				return this.type.GetMethods(this.flags | BindingFlags.FlattenHierarchy);
			}

			// Token: 0x04000021 RID: 33
			public bool inherited;

			// Token: 0x04000022 RID: 34
			public Type type;

			// Token: 0x04000023 RID: 35
			public BindingFlags flags;
		}
	}
}
