using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Reflectrify
{
	// Token: 0x0200000C RID: 12
	public static class ConstructorExtensions
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000025D4 File Offset: 0x000007D4
		public static ConstructorCache Cache
		{
			[CompilerGenerated]
			get
			{
				return ConstructorExtensions.<Cache>k__BackingField;
			}
		} = new ConstructorCache();

		// Token: 0x06000024 RID: 36 RVA: 0x000025DC File Offset: 0x000007DC
		public static T Construct<T>(this Type type, params object[] parameters)
		{
			if (!typeof(T).IsAssignableFrom(type))
			{
				throw new ArgumentException(string.Format("The provided type {0} cannot be assigned to {1}.", type, typeof(T)));
			}
			Type[] parameterTypes = (from p in parameters
			select p.GetType()).ToArray<Type>();
			ConstructorInfo constructor = ConstructorExtensions.Cache.GetConstructor(type, parameterTypes);
			if (constructor != null)
			{
				return (T)((object)constructor.Invoke(parameters));
			}
			throw new Exception("No constructor found for type " + type.Name + " with the provided parameter types.");
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000267E File Offset: 0x0000087E
		// Note: this type is marked as 'beforefieldinit'.
		static ConstructorExtensions()
		{
		}

		// Token: 0x0400000A RID: 10
		[CompilerGenerated]
		private static readonly ConstructorCache <Cache>k__BackingField;

		// Token: 0x02000016 RID: 22
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__3<T>
		{
			// Token: 0x06000056 RID: 86 RVA: 0x00003773 File Offset: 0x00001973
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__3()
			{
			}

			// Token: 0x06000057 RID: 87 RVA: 0x0000377F File Offset: 0x0000197F
			public <>c__3()
			{
			}

			// Token: 0x06000058 RID: 88 RVA: 0x00003787 File Offset: 0x00001987
			internal Type <Construct>b__3_0(object p)
			{
				return p.GetType();
			}

			// Token: 0x04000024 RID: 36
			public static readonly ConstructorExtensions.<>c__3<T> <>9 = new ConstructorExtensions.<>c__3<T>();

			// Token: 0x04000025 RID: 37
			public static Func<object, Type> <>9__3_0;
		}
	}
}
