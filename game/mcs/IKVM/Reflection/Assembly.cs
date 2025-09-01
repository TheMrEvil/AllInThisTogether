using System;
using System.Collections.Generic;
using System.IO;

namespace IKVM.Reflection
{
	// Token: 0x02000005 RID: 5
	public abstract class Assembly : ICustomAttributeProvider
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002075 File Offset: 0x00000275
		internal Assembly(Universe universe)
		{
			this.universe = universe;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002084 File Offset: 0x00000284
		public sealed override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000B RID: 11 RVA: 0x0000208C File Offset: 0x0000028C
		// (remove) Token: 0x0600000C RID: 12 RVA: 0x000020AD File Offset: 0x000002AD
		public event ModuleResolveEventHandler ModuleResolve
		{
			add
			{
				if (this.resolvers == null)
				{
					this.resolvers = new List<ModuleResolveEventHandler>();
				}
				this.resolvers.Add(value);
			}
			remove
			{
				this.resolvers.Remove(value);
			}
		}

		// Token: 0x0600000D RID: 13
		public abstract Type[] GetTypes();

		// Token: 0x0600000E RID: 14
		public abstract AssemblyName GetName();

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15
		public abstract string ImageRuntimeVersion { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16
		public abstract Module ManifestModule { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17
		public abstract MethodInfo EntryPoint { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18
		public abstract string Location { get; }

		// Token: 0x06000013 RID: 19
		public abstract AssemblyName[] GetReferencedAssemblies();

		// Token: 0x06000014 RID: 20
		public abstract Module[] GetModules(bool getResourceModules);

		// Token: 0x06000015 RID: 21
		public abstract Module[] GetLoadedModules(bool getResourceModules);

		// Token: 0x06000016 RID: 22
		public abstract Module GetModule(string name);

		// Token: 0x06000017 RID: 23
		public abstract string[] GetManifestResourceNames();

		// Token: 0x06000018 RID: 24
		public abstract ManifestResourceInfo GetManifestResourceInfo(string resourceName);

		// Token: 0x06000019 RID: 25
		public abstract Stream GetManifestResourceStream(string name);

		// Token: 0x0600001A RID: 26
		internal abstract Type FindType(TypeName name);

		// Token: 0x0600001B RID: 27
		internal abstract Type FindTypeIgnoreCase(TypeName lowerCaseName);

		// Token: 0x0600001C RID: 28 RVA: 0x000020BC File Offset: 0x000002BC
		internal Type ResolveType(Module requester, TypeName typeName)
		{
			return this.FindType(typeName) ?? this.universe.GetMissingTypeOrThrow(requester, this.ManifestModule, null, typeName);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000020E0 File Offset: 0x000002E0
		public string FullName
		{
			get
			{
				string result;
				if ((result = this.fullName) == null)
				{
					result = (this.fullName = this.GetName().FullName);
				}
				return result;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000210B File Offset: 0x0000030B
		public Module[] GetModules()
		{
			return this.GetModules(true);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002114 File Offset: 0x00000314
		public IEnumerable<Module> Modules
		{
			get
			{
				return this.GetLoadedModules();
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000211C File Offset: 0x0000031C
		public Module[] GetLoadedModules()
		{
			return this.GetLoadedModules(true);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002125 File Offset: 0x00000325
		public AssemblyName GetName(bool copiedName)
		{
			return this.GetName();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000212D File Offset: 0x0000032D
		public bool ReflectionOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002130 File Offset: 0x00000330
		public Type[] GetExportedTypes()
		{
			List<Type> list = new List<Type>();
			foreach (Type type in this.GetTypes())
			{
				if (type.IsVisible)
				{
					list.Add(type);
				}
			}
			return list.ToArray();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002171 File Offset: 0x00000371
		public IEnumerable<Type> ExportedTypes
		{
			get
			{
				return this.GetExportedTypes();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000217C File Offset: 0x0000037C
		public IEnumerable<TypeInfo> DefinedTypes
		{
			get
			{
				Type[] types = this.GetTypes();
				TypeInfo[] array = new TypeInfo[types.Length];
				for (int i = 0; i < types.Length; i++)
				{
					array[i] = types[i].GetTypeInfo();
				}
				return array;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000021B3 File Offset: 0x000003B3
		public Type GetType(string name)
		{
			return this.GetType(name, false);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000021BD File Offset: 0x000003BD
		public Type GetType(string name, bool throwOnError)
		{
			return this.GetType(name, throwOnError, false);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000021C8 File Offset: 0x000003C8
		public Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			TypeNameParser typeNameParser = TypeNameParser.Parse(name, throwOnError);
			if (typeNameParser.Error)
			{
				return null;
			}
			if (typeNameParser.AssemblyName != null)
			{
				if (throwOnError)
				{
					throw new ArgumentException("Type names passed to Assembly.GetType() must not specify an assembly.");
				}
				return null;
			}
			else
			{
				TypeName name2 = TypeName.Split(TypeNameParser.Unescape(typeNameParser.FirstNamePart));
				Type type = ignoreCase ? this.FindTypeIgnoreCase(name2.ToLowerInvariant()) : this.FindType(name2);
				if (type == null && this.__IsMissing)
				{
					throw new MissingAssemblyException((MissingAssembly)this);
				}
				return typeNameParser.Expand(type, this.ManifestModule, throwOnError, name, false, ignoreCase);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000225C File Offset: 0x0000045C
		public virtual Module LoadModule(string moduleName, byte[] rawModule)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002263 File Offset: 0x00000463
		public Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
		{
			return this.LoadModule(moduleName, rawModule);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000226D File Offset: 0x0000046D
		public bool IsDefined(Type attributeType, bool inherit)
		{
			return CustomAttributeData.__GetCustomAttributes(this, attributeType, inherit).Count != 0;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000227F File Offset: 0x0000047F
		public IList<CustomAttributeData> __GetCustomAttributes(Type attributeType, bool inherit)
		{
			return CustomAttributeData.__GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002289 File Offset: 0x00000489
		public IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002291 File Offset: 0x00000491
		public IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002299 File Offset: 0x00000499
		public static string CreateQualifiedName(string assemblyName, string typeName)
		{
			return typeName + ", " + assemblyName;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000022A7 File Offset: 0x000004A7
		public static Assembly GetAssembly(Type type)
		{
			return type.Assembly;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000022B0 File Offset: 0x000004B0
		public string CodeBase
		{
			get
			{
				string text = this.Location.Replace(Path.DirectorySeparatorChar, '/');
				if (!text.StartsWith("/"))
				{
					text = "/" + text;
				}
				return "file://" + text;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsDynamic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool __IsMissing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000022F7 File Offset: 0x000004F7
		public AssemblyNameFlags __AssemblyFlags
		{
			get
			{
				return this.GetAssemblyFlags();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000022FF File Offset: 0x000004FF
		protected virtual AssemblyNameFlags GetAssemblyFlags()
		{
			return this.GetName().Flags;
		}

		// Token: 0x06000036 RID: 54
		internal abstract IList<CustomAttributeData> GetCustomAttributesData(Type attributeType);

		// Token: 0x04000020 RID: 32
		internal readonly Universe universe;

		// Token: 0x04000021 RID: 33
		protected string fullName;

		// Token: 0x04000022 RID: 34
		protected List<ModuleResolveEventHandler> resolvers;
	}
}
