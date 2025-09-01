using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.IO;
using IKVM.Reflection.Metadata;

namespace IKVM.Reflection.Reader
{
	// Token: 0x02000091 RID: 145
	internal sealed class AssemblyReader : Assembly
	{
		// Token: 0x06000767 RID: 1895 RVA: 0x0001883D File Offset: 0x00016A3D
		internal AssemblyReader(string location, ModuleReader manifestModule) : base(manifestModule.universe)
		{
			this.location = location;
			this.manifestModule = manifestModule;
			this.externalModules = new Module[manifestModule.File.records.Length];
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00018871 File Offset: 0x00016A71
		public override string Location
		{
			get
			{
				return this.location ?? "";
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00018882 File Offset: 0x00016A82
		public override AssemblyName GetName()
		{
			return this.GetNameImpl(ref this.manifestModule.AssemblyTable.records[0]);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000188A0 File Offset: 0x00016AA0
		private AssemblyName GetNameImpl(ref AssemblyTable.Record rec)
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = this.manifestModule.GetString(rec.Name);
			assemblyName.Version = new Version((int)rec.MajorVersion, (int)rec.MinorVersion, (int)rec.BuildNumber, (int)rec.RevisionNumber);
			if (rec.PublicKey != 0)
			{
				assemblyName.SetPublicKey(this.manifestModule.GetBlobCopy(rec.PublicKey));
			}
			else
			{
				assemblyName.SetPublicKey(Empty<byte>.Array);
			}
			if (rec.Culture != 0)
			{
				assemblyName.Culture = this.manifestModule.GetString(rec.Culture);
			}
			else
			{
				assemblyName.Culture = "";
			}
			assemblyName.HashAlgorithm = (AssemblyHashAlgorithm)rec.HashAlgId;
			assemblyName.CodeBase = base.CodeBase;
			PortableExecutableKinds portableExecutableKinds;
			ImageFileMachine imageFileMachine;
			this.manifestModule.GetPEKind(out portableExecutableKinds, out imageFileMachine);
			if (imageFileMachine <= ImageFileMachine.ARM)
			{
				if (imageFileMachine != ImageFileMachine.I386)
				{
					if (imageFileMachine == ImageFileMachine.ARM)
					{
						assemblyName.ProcessorArchitecture = ProcessorArchitecture.Arm;
					}
				}
				else if ((portableExecutableKinds & (PortableExecutableKinds.Required32Bit | PortableExecutableKinds.Preferred32Bit)) != PortableExecutableKinds.NotAPortableExecutableImage)
				{
					assemblyName.ProcessorArchitecture = ProcessorArchitecture.X86;
				}
				else if ((rec.Flags & 112) == 112)
				{
					assemblyName.ProcessorArchitecture = ProcessorArchitecture.None;
				}
				else
				{
					assemblyName.ProcessorArchitecture = ProcessorArchitecture.MSIL;
				}
			}
			else if (imageFileMachine != ImageFileMachine.IA64)
			{
				if (imageFileMachine == ImageFileMachine.AMD64)
				{
					assemblyName.ProcessorArchitecture = ProcessorArchitecture.Amd64;
				}
			}
			else
			{
				assemblyName.ProcessorArchitecture = ProcessorArchitecture.IA64;
			}
			assemblyName.RawFlags = (AssemblyNameFlags)rec.Flags;
			return assemblyName;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000189EC File Offset: 0x00016BEC
		public override Type[] GetTypes()
		{
			if (this.externalModules.Length == 0)
			{
				return this.manifestModule.GetTypes();
			}
			List<Type> list = new List<Type>();
			foreach (Module module in this.GetModules(false))
			{
				list.AddRange(module.GetTypes());
			}
			return list.ToArray();
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00018A40 File Offset: 0x00016C40
		internal override Type FindType(TypeName typeName)
		{
			Type type = this.manifestModule.FindType(typeName);
			int num = 0;
			while (type == null && num < this.externalModules.Length)
			{
				if ((this.manifestModule.File.records[num].Flags & 1) == 0)
				{
					type = this.GetModule(num).FindType(typeName);
				}
				num++;
			}
			return type;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00018AA4 File Offset: 0x00016CA4
		internal override Type FindTypeIgnoreCase(TypeName lowerCaseName)
		{
			Type type = this.manifestModule.FindTypeIgnoreCase(lowerCaseName);
			int num = 0;
			while (type == null && num < this.externalModules.Length)
			{
				if ((this.manifestModule.File.records[num].Flags & 1) == 0)
				{
					type = this.GetModule(num).FindTypeIgnoreCase(lowerCaseName);
				}
				num++;
			}
			return type;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x00018B08 File Offset: 0x00016D08
		public override string ImageRuntimeVersion
		{
			get
			{
				return this.manifestModule.__ImageRuntimeVersion;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x00018B15 File Offset: 0x00016D15
		public override Module ManifestModule
		{
			get
			{
				return this.manifestModule;
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00018B20 File Offset: 0x00016D20
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			List<Module> list = new List<Module>();
			list.Add(this.manifestModule);
			foreach (Module module in this.externalModules)
			{
				if (module != null)
				{
					list.Add(module);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00018B68 File Offset: 0x00016D68
		public override Module[] GetModules(bool getResourceModules)
		{
			if (this.externalModules.Length == 0)
			{
				return new Module[]
				{
					this.manifestModule
				};
			}
			List<Module> list = new List<Module>();
			list.Add(this.manifestModule);
			for (int i = 0; i < this.manifestModule.File.records.Length; i++)
			{
				if (getResourceModules || (this.manifestModule.File.records[i].Flags & 1) == 0)
				{
					list.Add(this.GetModule(i));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00018BF4 File Offset: 0x00016DF4
		public override Module GetModule(string name)
		{
			if (name.Equals(this.manifestModule.ScopeName, StringComparison.OrdinalIgnoreCase))
			{
				return this.manifestModule;
			}
			int moduleIndex = this.GetModuleIndex(name);
			if (moduleIndex != -1)
			{
				return this.GetModule(moduleIndex);
			}
			return null;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00018C34 File Offset: 0x00016E34
		private int GetModuleIndex(string name)
		{
			for (int i = 0; i < this.manifestModule.File.records.Length; i++)
			{
				if (name.Equals(this.manifestModule.GetString(this.manifestModule.File.records[i].Name), StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00018C90 File Offset: 0x00016E90
		private Module GetModule(int index)
		{
			if (this.externalModules[index] != null)
			{
				return this.externalModules[index];
			}
			return this.LoadModule(index, null, this.manifestModule.GetString(this.manifestModule.File.records[index].Name));
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00018CE0 File Offset: 0x00016EE0
		private Module LoadModule(int index, byte[] rawModule, string name)
		{
			string path = (name == null) ? null : Path.Combine(Path.GetDirectoryName(this.location), name);
			if ((this.manifestModule.File.records[index].Flags & 1) != 0)
			{
				return this.externalModules[index] = new ResourceModule(this.manifestModule, index, path);
			}
			if (rawModule == null)
			{
				try
				{
					rawModule = File.ReadAllBytes(path);
				}
				catch (FileNotFoundException)
				{
					if (this.resolvers != null)
					{
						ResolveEventArgs e = new ResolveEventArgs(name, this);
						foreach (ModuleResolveEventHandler moduleResolveEventHandler in this.resolvers)
						{
							Module module = moduleResolveEventHandler(this, e);
							if (module != null)
							{
								return module;
							}
						}
					}
					if (this.universe.MissingMemberResolution)
					{
						return this.externalModules[index] = new MissingModule(this, index);
					}
					throw;
				}
			}
			return this.externalModules[index] = new ModuleReader(this, this.manifestModule.universe, new MemoryStream(rawModule), path, false);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00018E08 File Offset: 0x00017008
		public override Module LoadModule(string moduleName, byte[] rawModule)
		{
			int moduleIndex = this.GetModuleIndex(moduleName);
			if (moduleIndex == -1)
			{
				throw new ArgumentException();
			}
			if (this.externalModules[moduleIndex] != null)
			{
				return this.externalModules[moduleIndex];
			}
			return this.LoadModule(moduleIndex, rawModule, null);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00018E43 File Offset: 0x00017043
		public override MethodInfo EntryPoint
		{
			get
			{
				return this.manifestModule.GetEntryPoint();
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00018E50 File Offset: 0x00017050
		public override string[] GetManifestResourceNames()
		{
			return this.manifestModule.GetManifestResourceNames();
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00018E5D File Offset: 0x0001705D
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			return this.manifestModule.GetManifestResourceInfo(resourceName);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00018E6B File Offset: 0x0001706B
		public override Stream GetManifestResourceStream(string resourceName)
		{
			return this.manifestModule.GetManifestResourceStream(resourceName);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00018E79 File Offset: 0x00017079
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return this.manifestModule.__GetReferencedAssemblies();
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00018E86 File Offset: 0x00017086
		protected override AssemblyNameFlags GetAssemblyFlags()
		{
			return (AssemblyNameFlags)this.manifestModule.AssemblyTable.records[0].Flags;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00018EA3 File Offset: 0x000170A3
		internal string Name
		{
			get
			{
				return this.manifestModule.GetString(this.manifestModule.AssemblyTable.records[0].Name);
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00018ECC File Offset: 0x000170CC
		internal override IList<CustomAttributeData> GetCustomAttributesData(Type attributeType)
		{
			IList<CustomAttributeData> customAttributesImpl = CustomAttributeData.GetCustomAttributesImpl(null, this.manifestModule, 536870913, attributeType);
			return customAttributesImpl ?? CustomAttributeData.EmptyList;
		}

		// Token: 0x04000300 RID: 768
		private const int ContainsNoMetaData = 1;

		// Token: 0x04000301 RID: 769
		private readonly string location;

		// Token: 0x04000302 RID: 770
		private readonly ModuleReader manifestModule;

		// Token: 0x04000303 RID: 771
		private readonly Module[] externalModules;
	}
}
