using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x02000042 RID: 66
	internal static class DependencyInjector
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003FD0 File Offset: 0x000021D0
		internal static ISystemDependencyProvider SystemProvider
		{
			get
			{
				if (DependencyInjector.systemDependency != null)
				{
					return DependencyInjector.systemDependency;
				}
				object obj = DependencyInjector.locker;
				ISystemDependencyProvider result;
				lock (obj)
				{
					if (DependencyInjector.systemDependency != null)
					{
						result = DependencyInjector.systemDependency;
					}
					else
					{
						DependencyInjector.systemDependency = DependencyInjector.ReflectionLoad();
						if (DependencyInjector.systemDependency == null)
						{
							throw new PlatformNotSupportedException("Cannot find 'Mono.SystemDependencyProvider, System' dependency");
						}
						result = DependencyInjector.systemDependency;
					}
				}
				return result;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004048 File Offset: 0x00002248
		internal static void Register(ISystemDependencyProvider provider)
		{
			object obj = DependencyInjector.locker;
			lock (obj)
			{
				if (DependencyInjector.systemDependency != null && DependencyInjector.systemDependency != provider)
				{
					throw new InvalidOperationException();
				}
				DependencyInjector.systemDependency = provider;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000409C File Offset: 0x0000229C
		[PreserveDependency("get_Instance()", "Mono.SystemDependencyProvider", "System")]
		private static ISystemDependencyProvider ReflectionLoad()
		{
			Type type = Type.GetType("Mono.SystemDependencyProvider, System");
			if (type == null)
			{
				return null;
			}
			PropertyInfo property = type.GetProperty("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
			if (property == null)
			{
				return null;
			}
			return (ISystemDependencyProvider)property.GetValue(null);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000040E4 File Offset: 0x000022E4
		// Note: this type is marked as 'beforefieldinit'.
		static DependencyInjector()
		{
		}

		// Token: 0x04000DCC RID: 3532
		private const string TypeName = "Mono.SystemDependencyProvider, System";

		// Token: 0x04000DCD RID: 3533
		private static object locker = new object();

		// Token: 0x04000DCE RID: 3534
		private static ISystemDependencyProvider systemDependency;
	}
}
