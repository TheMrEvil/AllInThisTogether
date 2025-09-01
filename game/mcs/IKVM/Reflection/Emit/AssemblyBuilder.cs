using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.IO;
using System.Resources;
using System.Security.Cryptography;
using IKVM.Reflection.Metadata;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000D4 RID: 212
	public sealed class AssemblyBuilder : Assembly
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x00021800 File Offset: 0x0001FA00
		internal AssemblyBuilder(Universe universe, AssemblyName name, string dir, IEnumerable<CustomAttributeBuilder> customAttributes) : base(universe)
		{
			this.name = name.Name;
			this.SetVersionHelper(name.Version);
			if (!string.IsNullOrEmpty(name.Culture))
			{
				this.culture = name.Culture;
			}
			this.flags = name.RawFlags;
			this.hashAlgorithm = name.HashAlgorithm;
			if (this.hashAlgorithm == AssemblyHashAlgorithm.None)
			{
				this.hashAlgorithm = AssemblyHashAlgorithm.SHA1;
			}
			this.keyPair = name.KeyPair;
			if (this.keyPair != null)
			{
				this.publicKey = this.keyPair.PublicKey;
			}
			else
			{
				byte[] array = name.GetPublicKey();
				if (array != null && array.Length != 0)
				{
					this.publicKey = (byte[])array.Clone();
				}
			}
			this.dir = (dir ?? ".");
			if (customAttributes != null)
			{
				this.customAttributes.AddRange(customAttributes);
			}
			if (universe.HasMscorlib && !universe.Mscorlib.__IsMissing && universe.Mscorlib.ImageRuntimeVersion != null)
			{
				this.imageRuntimeVersion = universe.Mscorlib.ImageRuntimeVersion;
			}
			else
			{
				this.imageRuntimeVersion = typeof(object).Assembly.ImageRuntimeVersion;
			}
			universe.RegisterDynamicAssembly(this);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00021984 File Offset: 0x0001FB84
		private void SetVersionHelper(Version version)
		{
			if (version == null)
			{
				this.majorVersion = 0;
				this.minorVersion = 0;
				this.buildVersion = 0;
				this.revisionVersion = 0;
				return;
			}
			this.majorVersion = (ushort)version.Major;
			this.minorVersion = (ushort)version.Minor;
			this.buildVersion = ((version.Build == -1) ? 0 : ((ushort)version.Build));
			this.revisionVersion = ((version.Revision == -1) ? 0 : ((ushort)version.Revision));
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00021A03 File Offset: 0x0001FC03
		private void Rename(AssemblyName oldName)
		{
			this.fullName = null;
			this.universe.RenameAssembly(this, oldName);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00021A1C File Offset: 0x0001FC1C
		public void __SetAssemblyVersion(Version version)
		{
			AssemblyName oldName = this.GetName();
			this.SetVersionHelper(version);
			this.Rename(oldName);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00021A40 File Offset: 0x0001FC40
		public void __SetAssemblyCulture(string cultureName)
		{
			AssemblyName oldName = this.GetName();
			this.culture = cultureName;
			this.Rename(oldName);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00021A64 File Offset: 0x0001FC64
		public void __SetAssemblyKeyPair(StrongNameKeyPair keyPair)
		{
			AssemblyName oldName = this.GetName();
			this.keyPair = keyPair;
			if (keyPair != null)
			{
				this.publicKey = keyPair.PublicKey;
			}
			this.Rename(oldName);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00021A98 File Offset: 0x0001FC98
		public void __SetAssemblyPublicKey(byte[] publicKey)
		{
			AssemblyName oldName = this.GetName();
			this.publicKey = ((publicKey == null) ? null : ((byte[])publicKey.Clone()));
			this.Rename(oldName);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00021ACA File Offset: 0x0001FCCA
		public void __SetAssemblyAlgorithmId(AssemblyHashAlgorithm hashAlgorithm)
		{
			this.hashAlgorithm = hashAlgorithm;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00021AD3 File Offset: 0x0001FCD3
		[Obsolete("Use __AssemblyFlags property instead.")]
		public void __SetAssemblyFlags(AssemblyNameFlags flags)
		{
			this.__AssemblyFlags = flags;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00021ADC File Offset: 0x0001FCDC
		protected override AssemblyNameFlags GetAssemblyFlags()
		{
			return this.flags;
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00021ADC File Offset: 0x0001FCDC
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x00021AE4 File Offset: 0x0001FCE4
		public new AssemblyNameFlags __AssemblyFlags
		{
			get
			{
				return this.flags;
			}
			set
			{
				AssemblyName oldName = this.GetName();
				this.flags = value;
				this.Rename(oldName);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00021B06 File Offset: 0x0001FD06
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00021B10 File Offset: 0x0001FD10
		public override AssemblyName GetName()
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = this.name;
			assemblyName.Version = new Version((int)this.majorVersion, (int)this.minorVersion, (int)this.buildVersion, (int)this.revisionVersion);
			assemblyName.Culture = (this.culture ?? "");
			assemblyName.HashAlgorithm = this.hashAlgorithm;
			assemblyName.RawFlags = this.flags;
			assemblyName.SetPublicKey((this.publicKey != null) ? ((byte[])this.publicKey.Clone()) : Empty<byte>.Array);
			assemblyName.KeyPair = this.keyPair;
			return assemblyName;
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0000225C File Offset: 0x0000045C
		public override string Location
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00021BAF File Offset: 0x0001FDAF
		public ModuleBuilder DefineDynamicModule(string name, string fileName)
		{
			return this.DefineDynamicModule(name, fileName, false);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00021BBC File Offset: 0x0001FDBC
		public ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo)
		{
			ModuleBuilder moduleBuilder = new ModuleBuilder(this, name, fileName, emitSymbolInfo);
			this.modules.Add(moduleBuilder);
			return moduleBuilder;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00021BE0 File Offset: 0x0001FDE0
		public ModuleBuilder GetDynamicModule(string name)
		{
			foreach (ModuleBuilder moduleBuilder in this.modules)
			{
				if (moduleBuilder.Name == name)
				{
					return moduleBuilder;
				}
			}
			return null;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00021C44 File Offset: 0x0001FE44
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00021C53 File Offset: 0x0001FE53
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.customAttributes.Add(customBuilder);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00021C61 File Offset: 0x0001FE61
		public void __AddDeclarativeSecurity(CustomAttributeBuilder customBuilder)
		{
			this.declarativeSecurity.Add(customBuilder);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00021C6F File Offset: 0x0001FE6F
		public void __AddTypeForwarder(Type type)
		{
			this.__AddTypeForwarder(type, true);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00021C79 File Offset: 0x0001FE79
		public void __AddTypeForwarder(Type type, bool includeNested)
		{
			this.typeForwarders.Add(new AssemblyBuilder.TypeForwarder(type, includeNested));
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00021C8D File Offset: 0x0001FE8D
		public void SetEntryPoint(MethodInfo entryMethod)
		{
			this.SetEntryPoint(entryMethod, PEFileKinds.ConsoleApplication);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00021C97 File Offset: 0x0001FE97
		public void SetEntryPoint(MethodInfo entryMethod, PEFileKinds fileKind)
		{
			this.entryPoint = entryMethod;
			this.fileKind = fileKind;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00021CA8 File Offset: 0x0001FEA8
		public void __Save(Stream stream, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			if (!stream.CanRead || !stream.CanWrite || !stream.CanSeek || stream.Position != 0L)
			{
				throw new ArgumentException("Stream must support read/write/seek and current position must be zero.", "stream");
			}
			if (this.modules.Count != 1)
			{
				throw new NotSupportedException("Saving to a stream is only supported for single module assemblies.");
			}
			this.SaveImpl(this.modules[0].fileName, stream, portableExecutableKind, imageFileMachine);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00021D18 File Offset: 0x0001FF18
		public void Save(string assemblyFileName)
		{
			this.Save(assemblyFileName, PortableExecutableKinds.ILOnly, ImageFileMachine.I386);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00021D27 File Offset: 0x0001FF27
		public void Save(string assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			this.SaveImpl(assemblyFileName, null, portableExecutableKind, imageFileMachine);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00021D34 File Offset: 0x0001FF34
		private void SaveImpl(string assemblyFileName, Stream streamOrNull, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			ModuleBuilder moduleBuilder = null;
			foreach (ModuleBuilder moduleBuilder2 in this.modules)
			{
				moduleBuilder2.SetIsSaved();
				moduleBuilder2.PopulatePropertyAndEventTables();
				if (moduleBuilder == null && string.Compare(moduleBuilder2.fileName, assemblyFileName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					moduleBuilder = moduleBuilder2;
				}
			}
			if (moduleBuilder == null)
			{
				moduleBuilder = this.DefineDynamicModule("RefEmit_OnDiskManifestModule", assemblyFileName, false);
			}
			AssemblyTable.Record newRecord = default(AssemblyTable.Record);
			newRecord.HashAlgId = (int)this.hashAlgorithm;
			newRecord.Name = moduleBuilder.Strings.Add(this.name);
			newRecord.MajorVersion = this.majorVersion;
			newRecord.MinorVersion = this.minorVersion;
			newRecord.BuildNumber = this.buildVersion;
			newRecord.RevisionNumber = this.revisionVersion;
			if (this.publicKey != null)
			{
				newRecord.PublicKey = moduleBuilder.Blobs.Add(ByteBuffer.Wrap(this.publicKey));
				newRecord.Flags = (int)(this.flags | AssemblyNameFlags.PublicKey);
			}
			else
			{
				newRecord.Flags = (int)(this.flags & ~AssemblyNameFlags.PublicKey);
			}
			if (this.culture != null)
			{
				newRecord.Culture = moduleBuilder.Strings.Add(this.culture);
			}
			moduleBuilder.AssemblyTable.AddRecord(newRecord);
			ResourceSection resourceSection = (this.versionInfo != null || this.win32icon != null || this.win32manifest != null || this.win32resources != null) ? new ResourceSection() : null;
			if (this.versionInfo != null)
			{
				this.versionInfo.SetName(this.GetName());
				this.versionInfo.SetFileName(assemblyFileName);
				foreach (CustomAttributeBuilder customAttributeBuilder in this.customAttributes)
				{
					if (!customAttributeBuilder.HasBlob || this.universe.DecodeVersionInfoAttributeBlobs)
					{
						this.versionInfo.SetAttribute(this, customAttributeBuilder);
					}
				}
				ByteBuffer bb = new ByteBuffer(512);
				this.versionInfo.Write(bb);
				resourceSection.AddVersionInfo(bb);
			}
			if (this.win32icon != null)
			{
				resourceSection.AddIcon(this.win32icon);
			}
			if (this.win32manifest != null)
			{
				resourceSection.AddManifest(this.win32manifest, (this.fileKind == PEFileKinds.Dll) ? 2 : 1);
			}
			if (this.win32resources != null)
			{
				resourceSection.ExtractResources(this.win32resources);
			}
			foreach (CustomAttributeBuilder customBuilder in this.customAttributes)
			{
				moduleBuilder.SetCustomAttribute(536870913, customBuilder);
			}
			moduleBuilder.AddDeclarativeSecurity(536870913, this.declarativeSecurity);
			foreach (AssemblyBuilder.TypeForwarder typeForwarder in this.typeForwarders)
			{
				moduleBuilder.AddTypeForwarder(typeForwarder.Type, typeForwarder.IncludeNested);
			}
			foreach (AssemblyBuilder.ResourceFile resourceFile in this.resourceFiles)
			{
				if (resourceFile.Writer != null)
				{
					resourceFile.Writer.Generate();
					resourceFile.Writer.Close();
				}
				int implementation = this.AddFile(moduleBuilder, resourceFile.FileName, 1);
				ManifestResourceTable.Record newRecord2 = default(ManifestResourceTable.Record);
				newRecord2.Offset = 0;
				newRecord2.Flags = (int)resourceFile.Attributes;
				newRecord2.Name = moduleBuilder.Strings.Add(resourceFile.Name);
				newRecord2.Implementation = implementation;
				moduleBuilder.ManifestResource.AddRecord(newRecord2);
			}
			int num = 0;
			foreach (ModuleBuilder moduleBuilder3 in this.modules)
			{
				moduleBuilder3.FillAssemblyRefTable();
				moduleBuilder3.EmitResources();
				if (moduleBuilder3 != moduleBuilder)
				{
					int fileToken;
					if (this.entryPoint != null && this.entryPoint.Module == moduleBuilder3)
					{
						ModuleWriter.WriteModule(null, null, moduleBuilder3, this.fileKind, portableExecutableKind, imageFileMachine, moduleBuilder3.unmanagedResources, this.entryPoint.MetadataToken);
						fileToken = (num = this.AddFile(moduleBuilder, moduleBuilder3.fileName, 0));
					}
					else
					{
						ModuleWriter.WriteModule(null, null, moduleBuilder3, this.fileKind, portableExecutableKind, imageFileMachine, moduleBuilder3.unmanagedResources, 0);
						fileToken = this.AddFile(moduleBuilder, moduleBuilder3.fileName, 0);
					}
					moduleBuilder3.ExportTypes(fileToken, moduleBuilder);
				}
				moduleBuilder3.CloseResources();
			}
			foreach (Module module in this.addedModules)
			{
				int fileToken2 = this.AddFile(moduleBuilder, module.FullyQualifiedName, 0);
				module.ExportTypes(fileToken2, moduleBuilder);
			}
			if (num == 0 && this.entryPoint != null)
			{
				num = this.entryPoint.MetadataToken;
			}
			ModuleWriter.WriteModule(this.keyPair, this.publicKey, moduleBuilder, this.fileKind, portableExecutableKind, imageFileMachine, resourceSection ?? moduleBuilder.unmanagedResources, num, streamOrNull);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000222A4 File Offset: 0x000204A4
		private int AddFile(ModuleBuilder manifestModule, string fileName, int flags)
		{
			SHA1Managed sha1Managed = new SHA1Managed();
			string path = fileName;
			if (this.dir != null)
			{
				path = Path.Combine(this.dir, fileName);
			}
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, sha1Managed, CryptoStreamMode.Write))
				{
					byte[] buf = new byte[8192];
					ModuleWriter.HashChunk(fileStream, cryptoStream, buf, (int)fileStream.Length);
				}
			}
			return manifestModule.__AddModule(flags, Path.GetFileName(fileName), sha1Managed.Hash);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00022348 File Offset: 0x00020548
		public void AddResourceFile(string name, string fileName)
		{
			this.AddResourceFile(name, fileName, ResourceAttributes.Public);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00022354 File Offset: 0x00020554
		public void AddResourceFile(string name, string fileName, ResourceAttributes attribs)
		{
			AssemblyBuilder.ResourceFile item = default(AssemblyBuilder.ResourceFile);
			item.Name = name;
			item.FileName = fileName;
			item.Attributes = attribs;
			this.resourceFiles.Add(item);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002238D File Offset: 0x0002058D
		public IResourceWriter DefineResource(string name, string description, string fileName)
		{
			return this.DefineResource(name, description, fileName, ResourceAttributes.Public);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002239C File Offset: 0x0002059C
		public IResourceWriter DefineResource(string name, string description, string fileName, ResourceAttributes attribute)
		{
			string fileName2 = fileName;
			if (this.dir != null)
			{
				fileName2 = Path.Combine(this.dir, fileName);
			}
			ResourceWriter resourceWriter = new ResourceWriter(fileName2);
			AssemblyBuilder.ResourceFile item;
			item.Name = name;
			item.FileName = fileName;
			item.Attributes = attribute;
			item.Writer = resourceWriter;
			this.resourceFiles.Add(item);
			return resourceWriter;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000223F5 File Offset: 0x000205F5
		public void DefineVersionInfoResource()
		{
			if (this.versionInfo != null || this.win32resources != null)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.versionInfo = new VersionInfo();
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00022420 File Offset: 0x00020620
		public void DefineVersionInfoResource(string product, string productVersion, string company, string copyright, string trademark)
		{
			if (this.versionInfo != null || this.win32resources != null)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.versionInfo = new VersionInfo();
			this.versionInfo.product = product;
			this.versionInfo.informationalVersion = productVersion;
			this.versionInfo.company = company;
			this.versionInfo.copyright = copyright;
			this.versionInfo.trademark = trademark;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00022491 File Offset: 0x00020691
		public void __DefineIconResource(byte[] iconFile)
		{
			if (this.win32icon != null || this.win32resources != null)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.win32icon = (byte[])iconFile.Clone();
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000224BF File Offset: 0x000206BF
		public void __DefineManifestResource(byte[] manifest)
		{
			if (this.win32manifest != null || this.win32resources != null)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.win32manifest = (byte[])manifest.Clone();
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000224ED File Offset: 0x000206ED
		public void __DefineUnmanagedResource(byte[] resource)
		{
			if (this.versionInfo != null || this.win32icon != null || this.win32manifest != null || this.win32resources != null)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.win32resources = (byte[])resource.Clone();
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002252B File Offset: 0x0002072B
		public void DefineUnmanagedResource(string resourceFileName)
		{
			this.__DefineUnmanagedResource(File.ReadAllBytes(resourceFileName));
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002253C File Offset: 0x0002073C
		public override Type[] GetTypes()
		{
			List<Type> list = new List<Type>();
			foreach (ModuleBuilder moduleBuilder in this.modules)
			{
				moduleBuilder.GetTypesImpl(list);
			}
			foreach (Module module in this.addedModules)
			{
				module.GetTypesImpl(list);
			}
			return list.ToArray();
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000225DC File Offset: 0x000207DC
		internal override Type FindType(TypeName typeName)
		{
			foreach (ModuleBuilder moduleBuilder in this.modules)
			{
				Type type = moduleBuilder.FindType(typeName);
				if (type != null)
				{
					return type;
				}
			}
			foreach (Module module in this.addedModules)
			{
				Type type2 = module.FindType(typeName);
				if (type2 != null)
				{
					return type2;
				}
			}
			return null;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00022690 File Offset: 0x00020890
		internal override Type FindTypeIgnoreCase(TypeName lowerCaseName)
		{
			foreach (ModuleBuilder moduleBuilder in this.modules)
			{
				Type type = moduleBuilder.FindTypeIgnoreCase(lowerCaseName);
				if (type != null)
				{
					return type;
				}
			}
			foreach (Module module in this.addedModules)
			{
				Type type2 = module.FindTypeIgnoreCase(lowerCaseName);
				if (type2 != null)
				{
					return type2;
				}
			}
			return null;
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00022744 File Offset: 0x00020944
		public override string ImageRuntimeVersion
		{
			get
			{
				return this.imageRuntimeVersion;
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002274C File Offset: 0x0002094C
		public void __SetImageRuntimeVersion(string imageRuntimeVersion, int mdStreamVersion)
		{
			this.imageRuntimeVersion = imageRuntimeVersion;
			this.mdStreamVersion = mdStreamVersion;
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0002275C File Offset: 0x0002095C
		public override Module ManifestModule
		{
			get
			{
				if (this.pseudoManifestModule == null)
				{
					this.pseudoManifestModule = new ManifestModule(this);
				}
				return this.pseudoManifestModule;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x00022778 File Offset: 0x00020978
		public override MethodInfo EntryPoint
		{
			get
			{
				return this.entryPoint;
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00022780 File Offset: 0x00020980
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return Empty<AssemblyName>.Array;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00022787 File Offset: 0x00020987
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.GetModules(getResourceModules);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00022790 File Offset: 0x00020990
		public override Module[] GetModules(bool getResourceModules)
		{
			List<Module> list = new List<Module>();
			foreach (ModuleBuilder moduleBuilder in this.modules)
			{
				if (getResourceModules || !moduleBuilder.IsResource())
				{
					list.Add(moduleBuilder);
				}
			}
			foreach (Module module in this.addedModules)
			{
				if (getResourceModules || !module.IsResource())
				{
					list.Add(module);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0002284C File Offset: 0x00020A4C
		public override Module GetModule(string name)
		{
			foreach (ModuleBuilder moduleBuilder in this.modules)
			{
				if (moduleBuilder.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
				{
					return moduleBuilder;
				}
			}
			foreach (Module module in this.addedModules)
			{
				if (module.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
				{
					return module;
				}
			}
			return null;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00022900 File Offset: 0x00020B00
		public Module __AddModule(RawModule module)
		{
			Module module2 = module.ToModule(this);
			this.addedModules.Add(module2);
			return module2;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0000225C File Offset: 0x0000045C
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0000225C File Offset: 0x0000045C
		public override string[] GetManifestResourceNames()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0000225C File Offset: 0x0000045C
		public override Stream GetManifestResourceStream(string resourceName)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsDynamic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00022922 File Offset: 0x00020B22
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			return new Universe().DefineDynamicAssembly(name, access);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00022930 File Offset: 0x00020B30
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			return new Universe().DefineDynamicAssembly(name, access, assemblyAttributes);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00022940 File Offset: 0x00020B40
		internal override IList<CustomAttributeData> GetCustomAttributesData(Type attributeType)
		{
			List<CustomAttributeData> list = new List<CustomAttributeData>();
			foreach (CustomAttributeBuilder customAttributeBuilder in this.customAttributes)
			{
				if (attributeType == null || attributeType.IsAssignableFrom(customAttributeBuilder.Constructor.DeclaringType))
				{
					list.Add(customAttributeBuilder.ToData(this));
				}
			}
			return list;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x000229BC File Offset: 0x00020BBC
		internal bool IsWindowsRuntime
		{
			get
			{
				return (this.flags & (AssemblyNameFlags)512) > AssemblyNameFlags.None;
			}
		}

		// Token: 0x040003F9 RID: 1017
		private readonly string name;

		// Token: 0x040003FA RID: 1018
		private ushort majorVersion;

		// Token: 0x040003FB RID: 1019
		private ushort minorVersion;

		// Token: 0x040003FC RID: 1020
		private ushort buildVersion;

		// Token: 0x040003FD RID: 1021
		private ushort revisionVersion;

		// Token: 0x040003FE RID: 1022
		private string culture;

		// Token: 0x040003FF RID: 1023
		private AssemblyNameFlags flags;

		// Token: 0x04000400 RID: 1024
		private AssemblyHashAlgorithm hashAlgorithm;

		// Token: 0x04000401 RID: 1025
		private StrongNameKeyPair keyPair;

		// Token: 0x04000402 RID: 1026
		private byte[] publicKey;

		// Token: 0x04000403 RID: 1027
		internal readonly string dir;

		// Token: 0x04000404 RID: 1028
		private PEFileKinds fileKind = PEFileKinds.Dll;

		// Token: 0x04000405 RID: 1029
		private MethodInfo entryPoint;

		// Token: 0x04000406 RID: 1030
		private VersionInfo versionInfo;

		// Token: 0x04000407 RID: 1031
		private byte[] win32icon;

		// Token: 0x04000408 RID: 1032
		private byte[] win32manifest;

		// Token: 0x04000409 RID: 1033
		private byte[] win32resources;

		// Token: 0x0400040A RID: 1034
		private string imageRuntimeVersion;

		// Token: 0x0400040B RID: 1035
		internal int mdStreamVersion = 131072;

		// Token: 0x0400040C RID: 1036
		private Module pseudoManifestModule;

		// Token: 0x0400040D RID: 1037
		private readonly List<AssemblyBuilder.ResourceFile> resourceFiles = new List<AssemblyBuilder.ResourceFile>();

		// Token: 0x0400040E RID: 1038
		private readonly List<ModuleBuilder> modules = new List<ModuleBuilder>();

		// Token: 0x0400040F RID: 1039
		private readonly List<Module> addedModules = new List<Module>();

		// Token: 0x04000410 RID: 1040
		private readonly List<CustomAttributeBuilder> customAttributes = new List<CustomAttributeBuilder>();

		// Token: 0x04000411 RID: 1041
		private readonly List<CustomAttributeBuilder> declarativeSecurity = new List<CustomAttributeBuilder>();

		// Token: 0x04000412 RID: 1042
		private readonly List<AssemblyBuilder.TypeForwarder> typeForwarders = new List<AssemblyBuilder.TypeForwarder>();

		// Token: 0x02000363 RID: 867
		private struct TypeForwarder
		{
			// Token: 0x0600263C RID: 9788 RVA: 0x000B56D7 File Offset: 0x000B38D7
			internal TypeForwarder(Type type, bool includeNested)
			{
				this.Type = type;
				this.IncludeNested = includeNested;
			}

			// Token: 0x04000F03 RID: 3843
			internal readonly Type Type;

			// Token: 0x04000F04 RID: 3844
			internal readonly bool IncludeNested;
		}

		// Token: 0x02000364 RID: 868
		private struct ResourceFile
		{
			// Token: 0x04000F05 RID: 3845
			internal string Name;

			// Token: 0x04000F06 RID: 3846
			internal string FileName;

			// Token: 0x04000F07 RID: 3847
			internal ResourceAttributes Attributes;

			// Token: 0x04000F08 RID: 3848
			internal ResourceWriter Writer;
		}
	}
}
