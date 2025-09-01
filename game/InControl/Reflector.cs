using System;
using System.Collections.Generic;
using System.Reflection;

namespace InControl
{
	// Token: 0x0200006E RID: 110
	public static class Reflector
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0001225B File Offset: 0x0001045B
		public static IEnumerable<Type> AllAssemblyTypes
		{
			get
			{
				IEnumerable<Type> result;
				if ((result = Reflector.assemblyTypes) == null)
				{
					result = (Reflector.assemblyTypes = Reflector.GetAllAssemblyTypes());
				}
				return result;
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00012274 File Offset: 0x00010474
		private static bool IgnoreAssemblyWithName(string assemblyName)
		{
			foreach (string value in Reflector.ignoreAssemblies)
			{
				if (assemblyName.StartsWith(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000122A8 File Offset: 0x000104A8
		private static IEnumerable<Type> GetAllAssemblyTypes()
		{
			List<Type> list = new List<Type>();
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (!Reflector.IgnoreAssemblyWithName(assembly.GetName().Name))
				{
					Type[] array = null;
					try
					{
						array = assembly.GetTypes();
					}
					catch
					{
					}
					if (array != null)
					{
						list.AddRange(array);
					}
				}
			}
			return list;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00012318 File Offset: 0x00010518
		// Note: this type is marked as 'beforefieldinit'.
		static Reflector()
		{
		}

		// Token: 0x04000412 RID: 1042
		private static readonly string[] ignoreAssemblies = new string[]
		{
			"Unity",
			"UnityEngine",
			"UnityEditor",
			"mscorlib",
			"Microsoft",
			"System",
			"Mono",
			"JetBrains",
			"nunit",
			"ExCSS",
			"ICSharpCode",
			"AssetStoreTools"
		};

		// Token: 0x04000413 RID: 1043
		private static IEnumerable<Type> assemblyTypes;
	}
}
