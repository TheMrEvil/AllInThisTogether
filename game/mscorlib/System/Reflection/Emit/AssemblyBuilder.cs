using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;
using Mono.Security;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a dynamic assembly.</summary>
	// Token: 0x02000913 RID: 2323
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_AssemblyBuilder))]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AssemblyBuilder : Assembly, _AssemblyBuilder
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004E99 RID: 20121 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _AssemblyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004E9A RID: 20122 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _AssemblyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004E9B RID: 20123 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _AssemblyBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004E9C RID: 20124 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _AssemblyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004E9D RID: 20125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void basic_init(AssemblyBuilder ab);

		// Token: 0x06004E9E RID: 20126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateNativeCustomAttributes(AssemblyBuilder ab);

		// Token: 0x06004E9F RID: 20127 RVA: 0x000F6838 File Offset: 0x000F4A38
		[PreserveDependency("RuntimeResolve", "System.Reflection.Emit.ModuleBuilder")]
		internal AssemblyBuilder(AssemblyName n, string directory, AssemblyBuilderAccess access, bool corlib_internal)
		{
			this.pekind = PEFileKinds.Dll;
			this.corlib_object_type = typeof(object);
			this.corlib_value_type = typeof(ValueType);
			this.corlib_enum_type = typeof(Enum);
			this.corlib_void_type = typeof(void);
			base..ctor();
			if ((access & (AssemblyBuilderAccess)2048) != (AssemblyBuilderAccess)0)
			{
				throw new NotImplementedException("COMPILER_ACCESS is no longer supperted, use a newer mcs.");
			}
			if (!Enum.IsDefined(typeof(AssemblyBuilderAccess), access))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument value {0} is not valid.", (int)access), "access");
			}
			this.name = n.Name;
			this.access = (uint)access;
			this.flags = (uint)n.Flags;
			if (this.IsSave && (directory == null || directory.Length == 0))
			{
				this.dir = Directory.GetCurrentDirectory();
			}
			else
			{
				this.dir = directory;
			}
			if (n.CultureInfo != null)
			{
				this.culture = n.CultureInfo.Name;
				this.versioninfo_culture = n.CultureInfo.Name;
			}
			Version version = n.Version;
			if (version != null)
			{
				this.version = version.ToString();
			}
			if (n.KeyPair != null)
			{
				this.sn = n.KeyPair.StrongName();
			}
			else
			{
				byte[] publicKey = n.GetPublicKey();
				if (publicKey != null && publicKey.Length != 0)
				{
					this.sn = new Mono.Security.StrongName(publicKey);
				}
			}
			if (this.sn != null)
			{
				this.flags |= 1U;
			}
			this.corlib_internal = corlib_internal;
			if (this.sn != null)
			{
				this.pktoken = new byte[this.sn.PublicKeyToken.Length * 2];
				int num = 0;
				foreach (byte b in this.sn.PublicKeyToken)
				{
					string text = b.ToString("x2");
					this.pktoken[num++] = (byte)text[0];
					this.pktoken[num++] = (byte)text[1];
				}
			}
			AssemblyBuilder.basic_init(this);
		}

		/// <summary>Gets the location of the assembly, as specified originally (such as in an <see cref="T:System.Reflection.AssemblyName" /> object).</summary>
		/// <returns>The location of the assembly, as specified originally.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06004EA0 RID: 20128 RVA: 0x000F6A42 File Offset: 0x000F4C42
		public override string CodeBase
		{
			get
			{
				throw this.not_supported();
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06004EA1 RID: 20129 RVA: 0x000F3460 File Offset: 0x000F1660
		public override string EscapedCodeBase
		{
			get
			{
				return RuntimeAssembly.GetCodeBase(this, true);
			}
		}

		/// <summary>Returns the entry point of this assembly.</summary>
		/// <returns>The entry point of this assembly.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06004EA2 RID: 20130 RVA: 0x000F6A4A File Offset: 0x000F4C4A
		public override MethodInfo EntryPoint
		{
			get
			{
				return this.entry_point;
			}
		}

		/// <summary>Gets the location, in codebase format, of the loaded file that contains the manifest if it is not shadow-copied.</summary>
		/// <returns>The location of the loaded file that contains the manifest. If the loaded file has been shadow-copied, the <see langword="Location" /> is that of the file before being shadow-copied.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06004EA3 RID: 20131 RVA: 0x000F6A42 File Offset: 0x000F4C42
		public override string Location
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Gets the version of the common language runtime that will be saved in the file containing the manifest.</summary>
		/// <returns>A string representing the common language runtime version.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06004EA4 RID: 20132 RVA: 0x000F3471 File Offset: 0x000F1671
		public override string ImageRuntimeVersion
		{
			get
			{
				return RuntimeAssembly.InternalImageRuntimeVersion(this);
			}
		}

		/// <summary>Gets a value indicating whether the dynamic assembly is in the reflection-only context.</summary>
		/// <returns>
		///   <see langword="true" /> if the dynamic assembly is in the reflection-only context; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06004EA5 RID: 20133 RVA: 0x000F6A52 File Offset: 0x000F4C52
		public override bool ReflectionOnly
		{
			get
			{
				return this.access == 6U;
			}
		}

		/// <summary>Adds an existing resource file to this assembly.</summary>
		/// <param name="name">The logical name of the resource.</param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path; the file must be in the same directory as the assembly to which it is added.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined.  
		/// -or-  
		/// There is another file in the assembly named <paramref name="fileName" />.  
		/// -or-  
		/// The length of <paramref name="name" /> is zero.  
		/// -or-  
		/// The length of <paramref name="fileName" /> is zero, or if <paramref name="fileName" /> includes a path.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> is not found.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EA6 RID: 20134 RVA: 0x000F6A5D File Offset: 0x000F4C5D
		public void AddResourceFile(string name, string fileName)
		{
			this.AddResourceFile(name, fileName, ResourceAttributes.Public);
		}

		/// <summary>Adds an existing resource file to this assembly.</summary>
		/// <param name="name">The logical name of the resource.</param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path; the file must be in the same directory as the assembly to which it is added.</param>
		/// <param name="attribute">The resource attributes.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined.  
		/// -or-  
		/// There is another file in the assembly named <paramref name="fileName" />.  
		/// -or-  
		/// The length of <paramref name="name" /> is zero or if the length of <paramref name="fileName" /> is zero.  
		/// -or-  
		/// <paramref name="fileName" /> includes a path.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">If the file <paramref name="fileName" /> is not found.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EA7 RID: 20135 RVA: 0x000F6A68 File Offset: 0x000F4C68
		public void AddResourceFile(string name, string fileName, ResourceAttributes attribute)
		{
			this.AddResourceFile(name, fileName, attribute, true);
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x000F6A74 File Offset: 0x000F4C74
		private void AddResourceFile(string name, string fileName, ResourceAttributes attribute, bool fileNeedsToExists)
		{
			this.check_name_and_filename(name, fileName, fileNeedsToExists);
			if (this.dir != null)
			{
				fileName = Path.Combine(this.dir, fileName);
			}
			if (this.resources != null)
			{
				MonoResource[] destinationArray = new MonoResource[this.resources.Length + 1];
				Array.Copy(this.resources, destinationArray, this.resources.Length);
				this.resources = destinationArray;
			}
			else
			{
				this.resources = new MonoResource[1];
			}
			int num = this.resources.Length - 1;
			this.resources[num].name = name;
			this.resources[num].filename = fileName;
			this.resources[num].attrs = attribute;
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x000F6B24 File Offset: 0x000F4D24
		internal void AddPermissionRequests(PermissionSet required, PermissionSet optional, PermissionSet refused)
		{
			if (this.created)
			{
				throw new InvalidOperationException("Assembly was already saved.");
			}
			this._minimum = required;
			this._optional = optional;
			this._refuse = refused;
			if (required != null)
			{
				this.permissions_minimum = new RefEmitPermissionSet[1];
				this.permissions_minimum[0] = new RefEmitPermissionSet(SecurityAction.RequestMinimum, required.ToXml().ToString());
			}
			if (optional != null)
			{
				this.permissions_optional = new RefEmitPermissionSet[1];
				this.permissions_optional[0] = new RefEmitPermissionSet(SecurityAction.RequestOptional, optional.ToXml().ToString());
			}
			if (refused != null)
			{
				this.permissions_refused = new RefEmitPermissionSet[1];
				this.permissions_refused[0] = new RefEmitPermissionSet(SecurityAction.RequestRefuse, refused.ToXml().ToString());
			}
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x000F6BDF File Offset: 0x000F4DDF
		internal void EmbedResourceFile(string name, string fileName)
		{
			this.EmbedResourceFile(name, fileName, ResourceAttributes.Public);
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x000F6BEC File Offset: 0x000F4DEC
		private void EmbedResourceFile(string name, string fileName, ResourceAttributes attribute)
		{
			if (this.resources != null)
			{
				MonoResource[] destinationArray = new MonoResource[this.resources.Length + 1];
				Array.Copy(this.resources, destinationArray, this.resources.Length);
				this.resources = destinationArray;
			}
			else
			{
				this.resources = new MonoResource[1];
			}
			int num = this.resources.Length - 1;
			this.resources[num].name = name;
			this.resources[num].attrs = attribute;
			try
			{
				FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				long length = fileStream.Length;
				this.resources[num].data = new byte[length];
				fileStream.Read(this.resources[num].data, 0, (int)length);
				fileStream.Close();
			}
			catch
			{
			}
		}

		/// <summary>Defines a dynamic assembly that has the specified name and access rights.</summary>
		/// <param name="name">The name of the assembly.</param>
		/// <param name="access">The access rights of the assembly.</param>
		/// <returns>An object that represents the new assembly.</returns>
		// Token: 0x06004EAC RID: 20140 RVA: 0x000F6CC8 File Offset: 0x000F4EC8
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return new AssemblyBuilder(name, null, access, false);
		}

		/// <summary>Defines a new assembly that has the specified name, access rights, and attributes.</summary>
		/// <param name="name">The name of the assembly.</param>
		/// <param name="access">The access rights of the assembly.</param>
		/// <param name="assemblyAttributes">A collection that contains the attributes of the assembly.</param>
		/// <returns>An object that represents the new assembly.</returns>
		// Token: 0x06004EAD RID: 20141 RVA: 0x000F6CE4 File Offset: 0x000F4EE4
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(name, access);
			foreach (CustomAttributeBuilder customAttribute in assemblyAttributes)
			{
				assemblyBuilder.SetCustomAttribute(customAttribute);
			}
			return assemblyBuilder;
		}

		/// <summary>Defines a named transient dynamic module in this assembly.</summary>
		/// <param name="name">The name of the dynamic module.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> representing the defined dynamic module.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> begins with white space.  
		/// -or-  
		/// The length of <paramref name="name" /> is zero.  
		/// -or-  
		/// The length of <paramref name="name" /> is greater than the system-defined maximum length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.  
		///  -or-  
		///  The type that implements the default symbol writer interface cannot be found.</exception>
		// Token: 0x06004EAE RID: 20142 RVA: 0x000F6D38 File Offset: 0x000F4F38
		public ModuleBuilder DefineDynamicModule(string name)
		{
			return this.DefineDynamicModule(name, name, false, true);
		}

		/// <summary>Defines a named transient dynamic module in this assembly and specifies whether symbol information should be emitted.</summary>
		/// <param name="name">The name of the dynamic module.</param>
		/// <param name="emitSymbolInfo">
		///   <see langword="true" /> if symbol information is to be emitted; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> representing the defined dynamic module.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> begins with white space.  
		/// -or-  
		/// The length of <paramref name="name" /> is zero.  
		/// -or-  
		/// The length of <paramref name="name" /> is greater than the system-defined maximum length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.  
		///  -or-  
		///  The type that implements the default symbol writer interface cannot be found.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EAF RID: 20143 RVA: 0x000F6D44 File Offset: 0x000F4F44
		public ModuleBuilder DefineDynamicModule(string name, bool emitSymbolInfo)
		{
			return this.DefineDynamicModule(name, name, emitSymbolInfo, true);
		}

		/// <summary>Defines a persistable dynamic module with the given name that will be saved to the specified file. No symbol information is emitted.</summary>
		/// <param name="name">The name of the dynamic module.</param>
		/// <param name="fileName">The name of the file to which the dynamic module should be saved.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> object representing the defined dynamic module.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> or <paramref name="fileName" /> is zero.  
		///  -or-  
		///  The length of <paramref name="name" /> is greater than the system-defined maximum length.  
		///  -or-  
		///  <paramref name="fileName" /> contains a path specification (a directory component, for example).  
		///  -or-  
		///  There is a conflict with the name of another file that belongs to this assembly.</exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been previously saved.</exception>
		/// <exception cref="T:System.NotSupportedException">This assembly was called on a dynamic assembly with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Run" /> attribute.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.  
		///  -or-  
		///  The type that implements the default symbol writer interface cannot be found.</exception>
		// Token: 0x06004EB0 RID: 20144 RVA: 0x000F6D50 File Offset: 0x000F4F50
		public ModuleBuilder DefineDynamicModule(string name, string fileName)
		{
			return this.DefineDynamicModule(name, fileName, false, false);
		}

		/// <summary>Defines a persistable dynamic module, specifying the module name, the name of the file to which the module will be saved, and whether symbol information should be emitted using the default symbol writer.</summary>
		/// <param name="name">The name of the dynamic module.</param>
		/// <param name="fileName">The name of the file to which the dynamic module should be saved.</param>
		/// <param name="emitSymbolInfo">If <see langword="true" />, symbolic information is written using the default symbol writer.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> object representing the defined dynamic module.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> or <paramref name="fileName" /> is zero.  
		///  -or-  
		///  The length of <paramref name="name" /> is greater than the system-defined maximum length.  
		///  -or-  
		///  <paramref name="fileName" /> contains a path specification (a directory component, for example).  
		///  -or-  
		///  There is a conflict with the name of another file that belongs to this assembly.</exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been previously saved.</exception>
		/// <exception cref="T:System.NotSupportedException">This assembly was called on a dynamic assembly with the <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Run" /> attribute.</exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.  
		///  -or-  
		///  The type that implements the default symbol writer interface cannot be found.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EB1 RID: 20145 RVA: 0x000F6D5C File Offset: 0x000F4F5C
		public ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo)
		{
			return this.DefineDynamicModule(name, fileName, emitSymbolInfo, false);
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x000F6D68 File Offset: 0x000F4F68
		private ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo, bool transient)
		{
			this.check_name_and_filename(name, fileName, false);
			if (!transient)
			{
				if (Path.GetExtension(fileName) == string.Empty)
				{
					throw new ArgumentException("Module file name '" + fileName + "' must have file extension.");
				}
				if (!this.IsSave)
				{
					throw new NotSupportedException("Persistable modules are not supported in a dynamic assembly created with AssemblyBuilderAccess.Run");
				}
				if (this.created)
				{
					throw new InvalidOperationException("Assembly was already saved.");
				}
			}
			ModuleBuilder moduleBuilder = new ModuleBuilder(this, name, fileName, emitSymbolInfo, transient);
			if (this.modules != null && this.is_module_only)
			{
				throw new InvalidOperationException("A module-only assembly can only contain one module.");
			}
			if (this.modules != null)
			{
				ModuleBuilder[] destinationArray = new ModuleBuilder[this.modules.Length + 1];
				Array.Copy(this.modules, destinationArray, this.modules.Length);
				this.modules = destinationArray;
			}
			else
			{
				this.modules = new ModuleBuilder[1];
			}
			this.modules[this.modules.Length - 1] = moduleBuilder;
			return moduleBuilder;
		}

		/// <summary>Defines a standalone managed resource for this assembly with the default public resource attribute.</summary>
		/// <param name="name">The logical name of the resource.</param>
		/// <param name="description">A textual description of the resource.</param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path.</param>
		/// <returns>A <see cref="T:System.Resources.ResourceWriter" /> object for the specified resource.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined.  
		/// -or-  
		/// There is another file in the assembly named <paramref name="fileName" />.  
		/// -or-  
		/// The length of <paramref name="name" /> is zero.  
		/// -or-  
		/// The length of <paramref name="fileName" /> is zero.  
		/// -or-  
		/// <paramref name="fileName" /> includes a path.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EB3 RID: 20147 RVA: 0x000F6E4B File Offset: 0x000F504B
		public IResourceWriter DefineResource(string name, string description, string fileName)
		{
			return this.DefineResource(name, description, fileName, ResourceAttributes.Public);
		}

		/// <summary>Defines a standalone managed resource for this assembly. Attributes can be specified for the managed resource.</summary>
		/// <param name="name">The logical name of the resource.</param>
		/// <param name="description">A textual description of the resource.</param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path.</param>
		/// <param name="attribute">The resource attributes.</param>
		/// <returns>A <see cref="T:System.Resources.ResourceWriter" /> object for the specified resource.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined or if there is another file in the assembly named <paramref name="fileName" />.  
		/// -or-  
		/// The length of <paramref name="name" /> is zero.  
		/// -or-  
		/// The length of <paramref name="fileName" /> is zero.  
		/// -or-  
		/// <paramref name="fileName" /> includes a path.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EB4 RID: 20148 RVA: 0x000F6E58 File Offset: 0x000F5058
		public IResourceWriter DefineResource(string name, string description, string fileName, ResourceAttributes attribute)
		{
			this.AddResourceFile(name, fileName, attribute, false);
			IResourceWriter resourceWriter = new ResourceWriter(fileName);
			if (this.resource_writers == null)
			{
				this.resource_writers = new ArrayList();
			}
			this.resource_writers.Add(resourceWriter);
			return resourceWriter;
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x000F6E98 File Offset: 0x000F5098
		private void AddUnmanagedResource(Win32Resource res)
		{
			MemoryStream memoryStream = new MemoryStream();
			res.WriteTo(memoryStream);
			if (this.win32_resources != null)
			{
				MonoWin32Resource[] destinationArray = new MonoWin32Resource[this.win32_resources.Length + 1];
				Array.Copy(this.win32_resources, destinationArray, this.win32_resources.Length);
				this.win32_resources = destinationArray;
			}
			else
			{
				this.win32_resources = new MonoWin32Resource[1];
			}
			this.win32_resources[this.win32_resources.Length - 1] = new MonoWin32Resource(res.Type.Id, res.Name.Id, res.Language, memoryStream.ToArray());
		}

		/// <summary>Defines an unmanaged resource for this assembly as an opaque blob of bytes.</summary>
		/// <param name="resource">The opaque blob of bytes representing the unmanaged resource.</param>
		/// <exception cref="T:System.ArgumentException">An unmanaged resource was previously defined.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resource" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EB6 RID: 20150 RVA: 0x000F6F2F File Offset: 0x000F512F
		[MonoTODO("Not currently implemenented")]
		public void DefineUnmanagedResource(byte[] resource)
		{
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Unmanaged;
			throw new NotImplementedException();
		}

		/// <summary>Defines an unmanaged resource file for this assembly given the name of the resource file.</summary>
		/// <param name="resourceFileName">The name of the resource file.</param>
		/// <exception cref="T:System.ArgumentException">An unmanaged resource was previously defined.  
		///  -or-  
		///  The file <paramref name="resourceFileName" /> is not readable.  
		///  -or-  
		///  <paramref name="resourceFileName" /> is the empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="resourceFileName" /> is not found.  
		/// -or-  
		/// <paramref name="resourceFileName" /> is a directory.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EB7 RID: 20151 RVA: 0x000F6F60 File Offset: 0x000F5160
		public void DefineUnmanagedResource(string resourceFileName)
		{
			if (resourceFileName == null)
			{
				throw new ArgumentNullException("resourceFileName");
			}
			if (resourceFileName.Length == 0)
			{
				throw new ArgumentException("resourceFileName");
			}
			if (!File.Exists(resourceFileName) || Directory.Exists(resourceFileName))
			{
				throw new FileNotFoundException("File '" + resourceFileName + "' does not exist or is a directory.");
			}
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Unmanaged;
			using (FileStream fileStream = new FileStream(resourceFileName, FileMode.Open, FileAccess.Read))
			{
				foreach (object obj in new Win32ResFileReader(fileStream).ReadResources())
				{
					Win32EncodedResource win32EncodedResource = (Win32EncodedResource)obj;
					if (win32EncodedResource.Name.IsName || win32EncodedResource.Type.IsName)
					{
						throw new InvalidOperationException("resource files with named resources or non-default resource types are not supported.");
					}
					this.AddUnmanagedResource(win32EncodedResource);
				}
			}
		}

		/// <summary>Defines an unmanaged version information resource using the information specified in the assembly's AssemblyName object and the assembly's custom attributes.</summary>
		/// <exception cref="T:System.ArgumentException">An unmanaged version information resource was previously defined.  
		///  -or-  
		///  The unmanaged version information is too large to persist.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EB8 RID: 20152 RVA: 0x000F7064 File Offset: 0x000F5264
		public void DefineVersionInfoResource()
		{
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Assembly;
			this.version_res = new Win32VersionResource(1, 0, false);
		}

		/// <summary>Defines an unmanaged version information resource for this assembly with the given specifications.</summary>
		/// <param name="product">The name of the product with which this assembly is distributed.</param>
		/// <param name="productVersion">The version of the product with which this assembly is distributed.</param>
		/// <param name="company">The name of the company that produced this assembly.</param>
		/// <param name="copyright">Describes all copyright notices, trademarks, and registered trademarks that apply to this assembly. This should include the full text of all notices, legal symbols, copyright dates, trademark numbers, and so on. In English, this string should be in the format "Copyright Microsoft Corp. 1990-2001".</param>
		/// <param name="trademark">Describes all trademarks and registered trademarks that apply to this assembly. This should include the full text of all notices, legal symbols, trademark numbers, and so on. In English, this string should be in the format "Windows is a trademark of Microsoft Corporation".</param>
		/// <exception cref="T:System.ArgumentException">An unmanaged version information resource was previously defined.  
		///  -or-  
		///  The unmanaged version information is too large to persist.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EB9 RID: 20153 RVA: 0x000F7090 File Offset: 0x000F5290
		public void DefineVersionInfoResource(string product, string productVersion, string company, string copyright, string trademark)
		{
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Explicit;
			this.version_res = new Win32VersionResource(1, 0, false);
			this.version_res.ProductName = ((product != null) ? product : " ");
			this.version_res.ProductVersion = ((productVersion != null) ? productVersion : " ");
			this.version_res.CompanyName = ((company != null) ? company : " ");
			this.version_res.LegalCopyright = ((copyright != null) ? copyright : " ");
			this.version_res.LegalTrademarks = ((trademark != null) ? trademark : " ");
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x000F7138 File Offset: 0x000F5338
		private void DefineVersionInfoResourceImpl(string fileName)
		{
			if (this.versioninfo_culture != null)
			{
				this.version_res.FileLanguage = new CultureInfo(this.versioninfo_culture).LCID;
			}
			this.version_res.Version = ((this.version == null) ? "0.0.0.0" : this.version);
			if (this.cattrs != null)
			{
				NativeResourceType nativeResourceType = this.native_resource;
				if (nativeResourceType != NativeResourceType.Assembly)
				{
					if (nativeResourceType == NativeResourceType.Explicit)
					{
						foreach (CustomAttributeBuilder customAttributeBuilder in this.cattrs)
						{
							string fullName = customAttributeBuilder.Ctor.ReflectedType.FullName;
							if (fullName == "System.Reflection.AssemblyCultureAttribute")
							{
								this.version_res.FileLanguage = new CultureInfo(customAttributeBuilder.string_arg()).LCID;
							}
							else if (fullName == "System.Reflection.AssemblyDescriptionAttribute")
							{
								this.version_res.Comments = customAttributeBuilder.string_arg();
							}
						}
					}
				}
				else
				{
					foreach (CustomAttributeBuilder customAttributeBuilder2 in this.cattrs)
					{
						string fullName2 = customAttributeBuilder2.Ctor.ReflectedType.FullName;
						if (fullName2 == "System.Reflection.AssemblyProductAttribute")
						{
							this.version_res.ProductName = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCompanyAttribute")
						{
							this.version_res.CompanyName = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCopyrightAttribute")
						{
							this.version_res.LegalCopyright = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyTrademarkAttribute")
						{
							this.version_res.LegalTrademarks = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCultureAttribute")
						{
							this.version_res.FileLanguage = new CultureInfo(customAttributeBuilder2.string_arg()).LCID;
						}
						else if (fullName2 == "System.Reflection.AssemblyFileVersionAttribute")
						{
							this.version_res.FileVersion = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyInformationalVersionAttribute")
						{
							this.version_res.ProductVersion = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyTitleAttribute")
						{
							this.version_res.FileDescription = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyDescriptionAttribute")
						{
							this.version_res.Comments = customAttributeBuilder2.string_arg();
						}
					}
				}
			}
			this.version_res.OriginalFilename = fileName;
			this.version_res.InternalName = Path.GetFileNameWithoutExtension(fileName);
			this.AddUnmanagedResource(this.version_res);
		}

		/// <summary>Returns the dynamic module with the specified name.</summary>
		/// <param name="name">The name of the requested dynamic module.</param>
		/// <returns>A ModuleBuilder object representing the requested dynamic module.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EBB RID: 20155 RVA: 0x000F73C0 File Offset: 0x000F55C0
		public ModuleBuilder GetDynamicModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "name");
			}
			if (this.modules != null)
			{
				for (int i = 0; i < this.modules.Length; i++)
				{
					if (this.modules[i].name == name)
					{
						return this.modules[i];
					}
				}
			}
			return null;
		}

		/// <summary>Gets the exported types defined in this assembly.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> containing the exported types defined in this assembly.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EBC RID: 20156 RVA: 0x000F6A42 File Offset: 0x000F4C42
		public override Type[] GetExportedTypes()
		{
			throw this.not_supported();
		}

		/// <summary>Gets a <see cref="T:System.IO.FileStream" /> for the specified file in the file table of the manifest of this assembly.</summary>
		/// <param name="name">The name of the specified file.</param>
		/// <returns>A <see cref="T:System.IO.FileStream" /> for the specified file, or <see langword="null" />, if the file is not found.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EBD RID: 20157 RVA: 0x000F6A42 File Offset: 0x000F4C42
		public override FileStream GetFile(string name)
		{
			throw this.not_supported();
		}

		/// <summary>Gets the files in the file table of an assembly manifest, specifying whether to include resource modules.</summary>
		/// <param name="getResourceModules">
		///   <see langword="true" /> to include resource modules; otherwise, <see langword="false" />.</param>
		/// <returns>An array of <see cref="T:System.IO.FileStream" /> objects.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EBE RID: 20158 RVA: 0x000F6A42 File Offset: 0x000F4C42
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			throw this.not_supported();
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x000F742D File Offset: 0x000F562D
		internal override Module[] GetModulesInternal()
		{
			if (this.modules == null)
			{
				return new Module[0];
			}
			return (Module[])this.modules.Clone();
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x000F7450 File Offset: 0x000F5650
		internal override Type[] GetTypes(bool exportedOnly)
		{
			Type[] array = null;
			if (this.modules != null)
			{
				for (int i = 0; i < this.modules.Length; i++)
				{
					Type[] types = this.modules[i].GetTypes();
					if (array == null)
					{
						array = types;
					}
					else
					{
						Type[] destinationArray = new Type[array.Length + types.Length];
						Array.Copy(array, 0, destinationArray, 0, array.Length);
						Array.Copy(types, 0, destinationArray, array.Length, types.Length);
					}
				}
			}
			if (this.loaded_modules != null)
			{
				for (int j = 0; j < this.loaded_modules.Length; j++)
				{
					Type[] types2 = this.loaded_modules[j].GetTypes();
					if (array == null)
					{
						array = types2;
					}
					else
					{
						Type[] destinationArray2 = new Type[array.Length + types2.Length];
						Array.Copy(array, 0, destinationArray2, 0, array.Length);
						Array.Copy(types2, 0, destinationArray2, array.Length, types2.Length);
					}
				}
			}
			if (array != null)
			{
				List<Exception> list = null;
				foreach (Type type in array)
				{
					if (type is TypeBuilder)
					{
						if (list == null)
						{
							list = new List<Exception>();
						}
						list.Add(new TypeLoadException(string.Format("Type '{0}' is not finished", type.FullName)));
					}
				}
				if (list != null)
				{
					throw new ReflectionTypeLoadException(new Type[list.Count], list.ToArray());
				}
			}
			if (array != null)
			{
				return array;
			}
			return Type.EmptyTypes;
		}

		/// <summary>Returns information about how the given resource has been persisted.</summary>
		/// <param name="resourceName">The name of the resource.</param>
		/// <returns>
		///   <see cref="T:System.Reflection.ManifestResourceInfo" /> populated with information about the resource's topology, or <see langword="null" /> if the resource is not found.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EC1 RID: 20161 RVA: 0x000F6A42 File Offset: 0x000F4C42
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw this.not_supported();
		}

		/// <summary>Loads the specified manifest resource from this assembly.</summary>
		/// <returns>An array of type <see langword="String" /> containing the names of all the resources.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported on a dynamic assembly. To get the manifest resource names, use <see cref="M:System.Reflection.Assembly.GetManifestResourceNames" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EC2 RID: 20162 RVA: 0x000F6A42 File Offset: 0x000F4C42
		public override string[] GetManifestResourceNames()
		{
			throw this.not_supported();
		}

		/// <summary>Loads the specified manifest resource from this assembly.</summary>
		/// <param name="name">The name of the manifest resource being requested.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing this manifest resource.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EC3 RID: 20163 RVA: 0x000F6A42 File Offset: 0x000F4C42
		public override Stream GetManifestResourceStream(string name)
		{
			throw this.not_supported();
		}

		/// <summary>Loads the specified manifest resource, scoped by the namespace of the specified type, from this assembly.</summary>
		/// <param name="type">The type whose namespace is used to scope the manifest resource name.</param>
		/// <param name="name">The name of the manifest resource being requested.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing this manifest resource.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004EC4 RID: 20164 RVA: 0x000F6A42 File Offset: 0x000F4C42
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			throw this.not_supported();
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x000F7595 File Offset: 0x000F5795
		internal bool IsSave
		{
			get
			{
				return this.access != 1U;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06004EC6 RID: 20166 RVA: 0x000F75A3 File Offset: 0x000F57A3
		internal bool IsRun
		{
			get
			{
				return this.access == 1U || this.access == 3U || this.access == 9U;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06004EC7 RID: 20167 RVA: 0x000F75C3 File Offset: 0x000F57C3
		internal string AssemblyDir
		{
			get
			{
				return this.dir;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06004EC8 RID: 20168 RVA: 0x000F75CB File Offset: 0x000F57CB
		// (set) Token: 0x06004EC9 RID: 20169 RVA: 0x000F75D3 File Offset: 0x000F57D3
		internal bool IsModuleOnly
		{
			get
			{
				return this.is_module_only;
			}
			set
			{
				this.is_module_only = value;
			}
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x000F75DC File Offset: 0x000F57DC
		internal override Module GetManifestModule()
		{
			if (this.manifest_module == null)
			{
				this.manifest_module = this.DefineDynamicModule("Default Dynamic Module");
			}
			return this.manifest_module;
		}

		/// <summary>Saves this dynamic assembly to disk, specifying the nature of code in the assembly's executables and the target platform.</summary>
		/// <param name="assemblyFileName">The file name of the assembly.</param>
		/// <param name="portableExecutableKind">A bitwise combination of the <see cref="T:System.Reflection.PortableExecutableKinds" /> values that specifies the nature of the code.</param>
		/// <param name="imageFileMachine">One of the <see cref="T:System.Reflection.ImageFileMachine" /> values that specifies the target platform.</param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="assemblyFileName" /> is 0.  
		///  -or-  
		///  There are two or more modules resource files in the assembly with the same name.  
		///  -or-  
		///  The target directory of the assembly is invalid.  
		///  -or-  
		///  <paramref name="assemblyFileName" /> is not a simple file name (for example, has a directory or drive component), or more than one unmanaged resource, including a version information resources, was defined in this assembly.  
		///  -or-  
		///  The <see langword="CultureInfo" /> string in <see cref="T:System.Reflection.AssemblyCultureAttribute" /> is not a valid string and <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource(System.String,System.String,System.String,System.String,System.String)" /> was called prior to calling this method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been saved before.  
		///  -or-  
		///  This assembly has access <see langword="Run" /><see cref="T:System.Reflection.Emit.AssemblyBuilderAccess" /></exception>
		/// <exception cref="T:System.IO.IOException">An output error occurs during the save.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called for any of the types in the modules of the assembly to be written to disk.</exception>
		// Token: 0x06004ECB RID: 20171 RVA: 0x000F7604 File Offset: 0x000F5804
		[MonoLimitation("No support for PE32+ assemblies for AMD64 and IA64")]
		public void Save(string assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			this.peKind = portableExecutableKind;
			this.machine = imageFileMachine;
			if ((this.peKind & PortableExecutableKinds.PE32Plus) != PortableExecutableKinds.NotAPortableExecutableImage || (this.peKind & PortableExecutableKinds.Unmanaged32Bit) != PortableExecutableKinds.NotAPortableExecutableImage)
			{
				throw new NotImplementedException(this.peKind.ToString());
			}
			if (this.machine == ImageFileMachine.IA64 || this.machine == ImageFileMachine.AMD64)
			{
				throw new NotImplementedException(this.machine.ToString());
			}
			if (this.resource_writers != null)
			{
				foreach (object obj in this.resource_writers)
				{
					IResourceWriter resourceWriter = (IResourceWriter)obj;
					resourceWriter.Generate();
					resourceWriter.Close();
				}
			}
			ModuleBuilder moduleBuilder = null;
			if (this.modules != null)
			{
				foreach (ModuleBuilder moduleBuilder2 in this.modules)
				{
					if (moduleBuilder2.FileName == assemblyFileName)
					{
						moduleBuilder = moduleBuilder2;
					}
				}
			}
			if (moduleBuilder == null)
			{
				moduleBuilder = this.DefineDynamicModule("RefEmit_OnDiskManifestModule", assemblyFileName);
			}
			if (!this.is_module_only)
			{
				moduleBuilder.IsMain = true;
			}
			if (this.entry_point != null && this.entry_point.DeclaringType.Module != moduleBuilder)
			{
				Type[] array2;
				if (this.entry_point.GetParametersCount() == 1)
				{
					array2 = new Type[]
					{
						typeof(string)
					};
				}
				else
				{
					array2 = Type.EmptyTypes;
				}
				MethodBuilder methodBuilder = moduleBuilder.DefineGlobalMethod("__EntryPoint$", MethodAttributes.Static, this.entry_point.ReturnType, array2);
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				if (array2.Length == 1)
				{
					ilgenerator.Emit(OpCodes.Ldarg_0);
				}
				ilgenerator.Emit(OpCodes.Tailcall);
				ilgenerator.Emit(OpCodes.Call, this.entry_point);
				ilgenerator.Emit(OpCodes.Ret);
				this.entry_point = methodBuilder;
			}
			if (this.version_res != null)
			{
				this.DefineVersionInfoResourceImpl(assemblyFileName);
			}
			if (this.sn != null)
			{
				this.public_key = this.sn.PublicKey;
			}
			foreach (ModuleBuilder moduleBuilder3 in this.modules)
			{
				if (moduleBuilder3 != moduleBuilder)
				{
					moduleBuilder3.Save();
				}
			}
			moduleBuilder.Save();
			if (this.sn != null && this.sn.CanSign)
			{
				this.sn.Sign(Path.Combine(this.AssemblyDir, assemblyFileName));
			}
			this.created = true;
		}

		/// <summary>Saves this dynamic assembly to disk.</summary>
		/// <param name="assemblyFileName">The file name of the assembly.</param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="assemblyFileName" /> is 0.  
		///  -or-  
		///  There are two or more modules resource files in the assembly with the same name.  
		///  -or-  
		///  The target directory of the assembly is invalid.  
		///  -or-  
		///  <paramref name="assemblyFileName" /> is not a simple file name (for example, has a directory or drive component), or more than one unmanaged resource, including a version information resource, was defined in this assembly.  
		///  -or-  
		///  The <see langword="CultureInfo" /> string in <see cref="T:System.Reflection.AssemblyCultureAttribute" /> is not a valid string and <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource(System.String,System.String,System.String,System.String,System.String)" /> was called prior to calling this method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been saved before.  
		///  -or-  
		///  This assembly has access <see langword="Run" /><see cref="T:System.Reflection.Emit.AssemblyBuilderAccess" /></exception>
		/// <exception cref="T:System.IO.IOException">An output error occurs during the save.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called for any of the types in the modules of the assembly to be written to disk.</exception>
		// Token: 0x06004ECC RID: 20172 RVA: 0x000F7888 File Offset: 0x000F5A88
		public void Save(string assemblyFileName)
		{
			this.Save(assemblyFileName, PortableExecutableKinds.ILOnly, ImageFileMachine.I386);
		}

		/// <summary>Sets the entry point for this dynamic assembly, assuming that a console application is being built.</summary>
		/// <param name="entryMethod">A reference to the method that represents the entry point for this dynamic assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="entryMethod" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="entryMethod" /> is not contained within this assembly.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004ECD RID: 20173 RVA: 0x000F7897 File Offset: 0x000F5A97
		public void SetEntryPoint(MethodInfo entryMethod)
		{
			this.SetEntryPoint(entryMethod, PEFileKinds.ConsoleApplication);
		}

		/// <summary>Sets the entry point for this assembly and defines the type of the portable executable (PE file) being built.</summary>
		/// <param name="entryMethod">A reference to the method that represents the entry point for this dynamic assembly.</param>
		/// <param name="fileKind">The type of the assembly executable being built.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="entryMethod" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="entryMethod" /> is not contained within this assembly.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004ECE RID: 20174 RVA: 0x000F78A4 File Offset: 0x000F5AA4
		public void SetEntryPoint(MethodInfo entryMethod, PEFileKinds fileKind)
		{
			if (entryMethod == null)
			{
				throw new ArgumentNullException("entryMethod");
			}
			if (entryMethod.DeclaringType.Assembly != this)
			{
				throw new InvalidOperationException("Entry method is not defined in the same assembly.");
			}
			this.entry_point = entryMethod;
			this.pekind = fileKind;
		}

		/// <summary>Set a custom attribute on this assembly using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004ECF RID: 20175 RVA: 0x000F78F4 File Offset: 0x000F5AF4
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
			}
			else
			{
				this.cattrs = new CustomAttributeBuilder[1];
				this.cattrs[0] = customBuilder;
			}
			if (customBuilder.Ctor != null && customBuilder.Ctor.DeclaringType == typeof(RuntimeCompatibilityAttribute))
			{
				AssemblyBuilder.UpdateNativeCustomAttributes(this);
			}
		}

		/// <summary>Set a custom attribute on this assembly using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="con" /> is not a <see langword="RuntimeConstructorInfo" /> object.</exception>
		// Token: 0x06004ED0 RID: 20176 RVA: 0x000F798D File Offset: 0x000F5B8D
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x000F79BE File Offset: 0x000F5BBE
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x000F79CC File Offset: 0x000F5BCC
		private void check_name_and_filename(string name, string fileName, bool fileNeedsToExists)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "name");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "fileName");
			}
			if (Path.GetFileName(fileName) != fileName)
			{
				throw new ArgumentException("fileName '" + fileName + "' must not include a path.", "fileName");
			}
			string text = fileName;
			if (this.dir != null)
			{
				text = Path.Combine(this.dir, fileName);
			}
			if (fileNeedsToExists && !File.Exists(text))
			{
				throw new FileNotFoundException("Could not find file '" + fileName + "'");
			}
			if (this.resources != null)
			{
				for (int i = 0; i < this.resources.Length; i++)
				{
					if (this.resources[i].filename == text)
					{
						throw new ArgumentException("Duplicate file name '" + fileName + "'");
					}
					if (this.resources[i].name == name)
					{
						throw new ArgumentException("Duplicate name '" + name + "'");
					}
				}
			}
			if (this.modules != null)
			{
				for (int j = 0; j < this.modules.Length; j++)
				{
					if (!this.modules[j].IsTransient() && this.modules[j].FileName == fileName)
					{
						throw new ArgumentException("Duplicate file name '" + fileName + "'");
					}
					if (this.modules[j].Name == name)
					{
						throw new ArgumentException("Duplicate name '" + name + "'");
					}
				}
			}
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x000F7B80 File Offset: 0x000F5D80
		private string create_assembly_version(string version)
		{
			string[] array = version.Split('.', StringSplitOptions.None);
			int[] array2 = new int[4];
			if (array.Length < 0 || array.Length > 4)
			{
				throw new ArgumentException("The version specified '" + version + "' is invalid");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "*")
				{
					DateTime now = DateTime.Now;
					if (i == 2)
					{
						array2[2] = (now - new DateTime(2000, 1, 1)).Days;
						if (array.Length == 3)
						{
							array2[3] = (now.Second + now.Minute * 60 + now.Hour * 3600) / 2;
						}
					}
					else
					{
						if (i != 3)
						{
							throw new ArgumentException("The version specified '" + version + "' is invalid");
						}
						array2[3] = (now.Second + now.Minute * 60 + now.Hour * 3600) / 2;
					}
				}
				else
				{
					try
					{
						array2[i] = int.Parse(array[i]);
					}
					catch (FormatException)
					{
						throw new ArgumentException("The version specified '" + version + "' is invalid");
					}
				}
			}
			return string.Concat(new string[]
			{
				array2[0].ToString(),
				".",
				array2[1].ToString(),
				".",
				array2[2].ToString(),
				".",
				array2[3].ToString()
			});
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x000F7D14 File Offset: 0x000F5F14
		private string GetCultureString(string str)
		{
			if (!(str == "neutral"))
			{
				return str;
			}
			return string.Empty;
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x000F7D2A File Offset: 0x000F5F2A
		internal Type MakeGenericType(Type gtd, Type[] typeArguments)
		{
			return new TypeBuilderInstantiation(gtd, typeArguments);
		}

		/// <summary>Gets the specified type from the types that have been defined and created in the current <see cref="T:System.Reflection.Emit.AssemblyBuilder" />.</summary>
		/// <param name="name">The name of the type to search for.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw an exception if the type is not found; otherwise, <see langword="false" />.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to ignore the case of the type name when searching; otherwise, <see langword="false" />.</param>
		/// <returns>The specified type, or <see langword="null" /> if the type is not found or has not been created yet.</returns>
		// Token: 0x06004ED6 RID: 20182 RVA: 0x000F7D34 File Offset: 0x000F5F34
		public override Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			if (name == null)
			{
				throw new ArgumentNullException(name);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name", "Name cannot be empty");
			}
			Type type = base.InternalGetType(null, name, throwOnError, ignoreCase);
			if (!(type is TypeBuilder))
			{
				return type;
			}
			if (throwOnError)
			{
				throw new TypeLoadException(string.Format("Could not load type '{0}' from assembly '{1}'", name, this.name));
			}
			return null;
		}

		/// <summary>Gets the specified module in this assembly.</summary>
		/// <param name="name">The name of the requested module.</param>
		/// <returns>The module being requested, or <see langword="null" /> if the module is not found.</returns>
		// Token: 0x06004ED7 RID: 20183 RVA: 0x000F7D94 File Offset: 0x000F5F94
		public override Module GetModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Name can't be empty");
			}
			if (this.modules == null)
			{
				return null;
			}
			foreach (ModuleBuilder module in this.modules)
			{
				if (module.ScopeName == name)
				{
					return module;
				}
			}
			return null;
		}

		/// <summary>Gets all the modules that are part of this assembly, and optionally includes resource modules.</summary>
		/// <param name="getResourceModules">
		///   <see langword="true" /> to include resource modules; otherwise, <see langword="false" />.</param>
		/// <returns>The modules that are part of this assembly.</returns>
		// Token: 0x06004ED8 RID: 20184 RVA: 0x000F7DF8 File Offset: 0x000F5FF8
		public override Module[] GetModules(bool getResourceModules)
		{
			Module[] modulesInternal = this.GetModulesInternal();
			if (!getResourceModules)
			{
				List<Module> list = new List<Module>(modulesInternal.Length);
				foreach (Module module in modulesInternal)
				{
					if (!module.IsResource())
					{
						list.Add(module);
					}
				}
				return list.ToArray();
			}
			return modulesInternal;
		}

		/// <summary>Gets the <see cref="T:System.Reflection.AssemblyName" /> that was specified when the current dynamic assembly was created, and sets the code base as specified.</summary>
		/// <param name="copiedName">
		///   <see langword="true" /> to set the code base to the location of the assembly after it is shadow-copied; <see langword="false" /> to set the code base to the original location.</param>
		/// <returns>The name of the dynamic assembly.</returns>
		// Token: 0x06004ED9 RID: 20185 RVA: 0x000F7E48 File Offset: 0x000F6048
		public override AssemblyName GetName(bool copiedName)
		{
			AssemblyName assemblyName = AssemblyName.Create(this, false);
			if (this.sn != null)
			{
				assemblyName.SetPublicKey(this.sn.PublicKey);
				assemblyName.SetPublicKeyToken(this.sn.PublicKeyToken);
			}
			return assemblyName;
		}

		/// <summary>Gets an incomplete list of <see cref="T:System.Reflection.AssemblyName" /> objects for the assemblies that are referenced by this <see cref="T:System.Reflection.Emit.AssemblyBuilder" />.</summary>
		/// <returns>An array of assembly names for the referenced assemblies. This array is not a complete list.</returns>
		// Token: 0x06004EDA RID: 20186 RVA: 0x000F333D File Offset: 0x000F153D
		[MonoTODO("This always returns an empty array")]
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return Assembly.GetReferencedAssemblies(this);
		}

		/// <summary>Returns all the loaded modules that are part of this assembly, and optionally includes resource modules.</summary>
		/// <param name="getResourceModules">
		///   <see langword="true" /> to include resource modules; otherwise, <see langword="false" />.</param>
		/// <returns>The loaded modules that are part of this assembly.</returns>
		// Token: 0x06004EDB RID: 20187 RVA: 0x000F3396 File Offset: 0x000F1596
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.GetModules(getResourceModules);
		}

		/// <summary>Gets the satellite assembly for the specified culture.</summary>
		/// <param name="culture">The specified culture.</param>
		/// <returns>The specified satellite assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly cannot be found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">The satellite assembly with a matching file name was found, but the <see langword="CultureInfo" /> did not match the one specified.</exception>
		/// <exception cref="T:System.BadImageFormatException">The satellite assembly is not a valid assembly.</exception>
		// Token: 0x06004EDC RID: 20188 RVA: 0x000F7E88 File Offset: 0x000F6088
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetSatelliteAssembly(culture, null, true, ref stackCrawlMark);
		}

		/// <summary>Gets the specified version of the satellite assembly for the specified culture.</summary>
		/// <param name="culture">The specified culture.</param>
		/// <param name="version">The version of the satellite assembly.</param>
		/// <returns>The specified satellite assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileLoadException">The satellite assembly with a matching file name was found, but the <see langword="CultureInfo" /> or the version did not match the one specified.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly cannot be found.</exception>
		/// <exception cref="T:System.BadImageFormatException">The satellite assembly is not a valid assembly.</exception>
		// Token: 0x06004EDD RID: 20189 RVA: 0x000F7EA4 File Offset: 0x000F60A4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return base.GetSatelliteAssembly(culture, version, true, ref stackCrawlMark);
		}

		/// <summary>Gets the module in the current <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> that contains the assembly manifest.</summary>
		/// <returns>The manifest module.</returns>
		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06004EDE RID: 20190 RVA: 0x000F33D6 File Offset: 0x000F15D6
		public override Module ManifestModule
		{
			get
			{
				return this.GetManifestModule();
			}
		}

		/// <summary>Gets a value that indicates whether the assembly was loaded from the global assembly cache.</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06004EDF RID: 20191 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool GlobalAssemblyCache
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates that the current assembly is a dynamic assembly.</summary>
		/// <returns>Always <see langword="true" />.</returns>
		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06004EE0 RID: 20192 RVA: 0x000040F7 File Offset: 0x000022F7
		public override bool IsDynamic
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to the specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004EE1 RID: 20193 RVA: 0x000F7EBE File Offset: 0x000F60BE
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06004EE2 RID: 20194 RVA: 0x000F3732 File Offset: 0x000F1932
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x000F7EC7 File Offset: 0x000F60C7
		public override string ToString()
		{
			if (this.assemblyName != null)
			{
				return this.assemblyName;
			}
			this.assemblyName = this.FullName;
			return this.assemblyName;
		}

		/// <summary>Returns a value that indicates whether one or more instances of the specified attribute type is applied to this member.</summary>
		/// <param name="attributeType">The type of attribute to test for.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <returns>
		///   <see langword="true" /> if one or more instances of <paramref name="attributeType" /> is applied to this dynamic assembly; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004EE4 RID: 20196 RVA: 0x00052A66 File Offset: 0x00050C66
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		/// <summary>Returns all the custom attributes that have been applied to the current <see cref="T:System.Reflection.Emit.AssemblyBuilder" />.</summary>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <returns>An array that contains the custom attributes; the array is empty if there are no attributes.</returns>
		// Token: 0x06004EE5 RID: 20197 RVA: 0x000F1905 File Offset: 0x000EFB05
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		/// <summary>Returns all the custom attributes that have been applied to the current <see cref="T:System.Reflection.Emit.AssemblyBuilder" />, and that derive from a specified attribute type.</summary>
		/// <param name="attributeType">The base type from which attributes derive.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <returns>An array that contains the custom attributes that are derived at any level from <paramref name="attributeType" />; the array is empty if there are no such attributes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the runtime. For example, <paramref name="attributeType" /> is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> object.</exception>
		// Token: 0x06004EE6 RID: 20198 RVA: 0x000F190E File Offset: 0x000EFB0E
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		/// <summary>Gets the display name of the current dynamic assembly.</summary>
		/// <returns>The display name of the dynamic assembly.</returns>
		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06004EE7 RID: 20199 RVA: 0x000F3469 File Offset: 0x000F1669
		public override string FullName
		{
			get
			{
				return RuntimeAssembly.get_fullname(this);
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06004EE8 RID: 20200 RVA: 0x000F7EEA File Offset: 0x000F60EA
		internal override IntPtr MonoAssembly
		{
			get
			{
				return this._mono_assembly;
			}
		}

		/// <summary>Gets the evidence for this assembly.</summary>
		/// <returns>The evidence for this assembly.</returns>
		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06004EE9 RID: 20201 RVA: 0x000F378A File Offset: 0x000F198A
		public override Evidence Evidence
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
			get
			{
				return this.UnprotectedGetEvidence();
			}
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x000F7EF4 File Offset: 0x000F60F4
		internal override Evidence UnprotectedGetEvidence()
		{
			if (this._evidence == null)
			{
				lock (this)
				{
					this._evidence = Evidence.GetDefaultHostEvidence(this);
				}
			}
			return this._evidence;
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x000173AD File Offset: 0x000155AD
		internal AssemblyBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040030E5 RID: 12517
		internal IntPtr _mono_assembly;

		// Token: 0x040030E6 RID: 12518
		internal Evidence _evidence;

		// Token: 0x040030E7 RID: 12519
		private UIntPtr dynamic_assembly;

		// Token: 0x040030E8 RID: 12520
		private MethodInfo entry_point;

		// Token: 0x040030E9 RID: 12521
		private ModuleBuilder[] modules;

		// Token: 0x040030EA RID: 12522
		private string name;

		// Token: 0x040030EB RID: 12523
		private string dir;

		// Token: 0x040030EC RID: 12524
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040030ED RID: 12525
		private MonoResource[] resources;

		// Token: 0x040030EE RID: 12526
		private byte[] public_key;

		// Token: 0x040030EF RID: 12527
		private string version;

		// Token: 0x040030F0 RID: 12528
		private string culture;

		// Token: 0x040030F1 RID: 12529
		private uint algid;

		// Token: 0x040030F2 RID: 12530
		private uint flags;

		// Token: 0x040030F3 RID: 12531
		private PEFileKinds pekind;

		// Token: 0x040030F4 RID: 12532
		private bool delay_sign;

		// Token: 0x040030F5 RID: 12533
		private uint access;

		// Token: 0x040030F6 RID: 12534
		private Module[] loaded_modules;

		// Token: 0x040030F7 RID: 12535
		private MonoWin32Resource[] win32_resources;

		// Token: 0x040030F8 RID: 12536
		private RefEmitPermissionSet[] permissions_minimum;

		// Token: 0x040030F9 RID: 12537
		private RefEmitPermissionSet[] permissions_optional;

		// Token: 0x040030FA RID: 12538
		private RefEmitPermissionSet[] permissions_refused;

		// Token: 0x040030FB RID: 12539
		private PortableExecutableKinds peKind;

		// Token: 0x040030FC RID: 12540
		private ImageFileMachine machine;

		// Token: 0x040030FD RID: 12541
		private bool corlib_internal;

		// Token: 0x040030FE RID: 12542
		private Type[] type_forwarders;

		// Token: 0x040030FF RID: 12543
		private byte[] pktoken;

		// Token: 0x04003100 RID: 12544
		internal PermissionSet _minimum;

		// Token: 0x04003101 RID: 12545
		internal PermissionSet _optional;

		// Token: 0x04003102 RID: 12546
		internal PermissionSet _refuse;

		// Token: 0x04003103 RID: 12547
		internal PermissionSet _granted;

		// Token: 0x04003104 RID: 12548
		internal PermissionSet _denied;

		// Token: 0x04003105 RID: 12549
		private string assemblyName;

		// Token: 0x04003106 RID: 12550
		internal Type corlib_object_type;

		// Token: 0x04003107 RID: 12551
		internal Type corlib_value_type;

		// Token: 0x04003108 RID: 12552
		internal Type corlib_enum_type;

		// Token: 0x04003109 RID: 12553
		internal Type corlib_void_type;

		// Token: 0x0400310A RID: 12554
		private ArrayList resource_writers;

		// Token: 0x0400310B RID: 12555
		private Win32VersionResource version_res;

		// Token: 0x0400310C RID: 12556
		private bool created;

		// Token: 0x0400310D RID: 12557
		private bool is_module_only;

		// Token: 0x0400310E RID: 12558
		private Mono.Security.StrongName sn;

		// Token: 0x0400310F RID: 12559
		private NativeResourceType native_resource;

		// Token: 0x04003110 RID: 12560
		private string versioninfo_culture;

		// Token: 0x04003111 RID: 12561
		private const AssemblyBuilderAccess COMPILER_ACCESS = (AssemblyBuilderAccess)2048;

		// Token: 0x04003112 RID: 12562
		private ModuleBuilder manifest_module;
	}
}
