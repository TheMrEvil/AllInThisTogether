using System;
using System.Collections;
using System.Reflection;
using System.Security.Permissions;

namespace System.Xml.Serialization
{
	// Token: 0x020002FC RID: 764
	internal static class DynamicAssemblies
	{
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x000C8634 File Offset: 0x000C6834
		private static FileIOPermission UnrestrictedFileIOPermission
		{
			get
			{
				if (DynamicAssemblies.fileIOPermission == null)
				{
					DynamicAssemblies.fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
				}
				return DynamicAssemblies.fileIOPermission;
			}
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000C8654 File Offset: 0x000C6854
		internal static bool IsTypeDynamic(Type type)
		{
			object obj = DynamicAssemblies.tableIsTypeDynamic[type];
			if (obj == null)
			{
				DynamicAssemblies.UnrestrictedFileIOPermission.Assert();
				Assembly assembly = type.Assembly;
				bool flag = assembly.IsDynamic || string.IsNullOrEmpty(assembly.Location);
				if (!flag)
				{
					if (type.IsArray)
					{
						flag = DynamicAssemblies.IsTypeDynamic(type.GetElementType());
					}
					else if (type.IsGenericType)
					{
						Type[] genericArguments = type.GetGenericArguments();
						if (genericArguments != null)
						{
							foreach (Type type2 in genericArguments)
							{
								if (!(type2 == null) && !type2.IsGenericParameter)
								{
									flag = DynamicAssemblies.IsTypeDynamic(type2);
									if (flag)
									{
										break;
									}
								}
							}
						}
					}
				}
				obj = (DynamicAssemblies.tableIsTypeDynamic[type] = flag);
			}
			return (bool)obj;
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x000C8718 File Offset: 0x000C6918
		internal static bool IsTypeDynamic(Type[] arguments)
		{
			for (int i = 0; i < arguments.Length; i++)
			{
				if (DynamicAssemblies.IsTypeDynamic(arguments[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x000C8744 File Offset: 0x000C6944
		internal static void Add(Assembly a)
		{
			Hashtable obj = DynamicAssemblies.nameToAssemblyMap;
			lock (obj)
			{
				if (DynamicAssemblies.assemblyToNameMap[a] == null)
				{
					Assembly left = DynamicAssemblies.nameToAssemblyMap[a.FullName] as Assembly;
					string text = null;
					if (left == null)
					{
						text = a.FullName;
					}
					else if (left != a)
					{
						text = a.FullName + ", " + DynamicAssemblies.nameToAssemblyMap.Count.ToString();
					}
					if (text != null)
					{
						DynamicAssemblies.nameToAssemblyMap.Add(text, a);
						DynamicAssemblies.assemblyToNameMap.Add(a, text);
					}
				}
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000C8810 File Offset: 0x000C6A10
		internal static Assembly Get(string fullName)
		{
			if (DynamicAssemblies.nameToAssemblyMap == null)
			{
				return null;
			}
			return (Assembly)DynamicAssemblies.nameToAssemblyMap[fullName];
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000C882F File Offset: 0x000C6A2F
		internal static string GetName(Assembly a)
		{
			if (DynamicAssemblies.assemblyToNameMap == null)
			{
				return null;
			}
			return (string)DynamicAssemblies.assemblyToNameMap[a];
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x000C884E File Offset: 0x000C6A4E
		// Note: this type is marked as 'beforefieldinit'.
		static DynamicAssemblies()
		{
		}

		// Token: 0x04001B02 RID: 6914
		private static ArrayList assembliesInConfig = new ArrayList();

		// Token: 0x04001B03 RID: 6915
		private static volatile Hashtable nameToAssemblyMap = new Hashtable();

		// Token: 0x04001B04 RID: 6916
		private static volatile Hashtable assemblyToNameMap = new Hashtable();

		// Token: 0x04001B05 RID: 6917
		private static Hashtable tableIsTypeDynamic = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04001B06 RID: 6918
		private static volatile FileIOPermission fileIOPermission;
	}
}
