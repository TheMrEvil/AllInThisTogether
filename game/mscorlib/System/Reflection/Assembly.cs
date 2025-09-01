using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using Mono;

namespace System.Reflection
{
	/// <summary>Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.</summary>
	// Token: 0x020008E8 RID: 2280
	[ComDefaultInterface(typeof(_Assembly))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class Assembly : ICustomAttributeProvider, _Assembly, IEvidenceFactory, ISerializable
	{
		/// <summary>Occurs when the common language runtime class loader cannot resolve a reference to an internal module of an assembly through normal means.</summary>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06004BE3 RID: 19427 RVA: 0x000479F8 File Offset: 0x00045BF8
		// (remove) Token: 0x06004BE4 RID: 19428 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual event ModuleResolveEventHandler ModuleResolve
		{
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			add
			{
				throw new NotImplementedException();
			}
			[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
			remove
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the location of the assembly as specified originally, for example, in an <see cref="T:System.Reflection.AssemblyName" /> object.</summary>
		/// <returns>The location of the assembly as specified originally.</returns>
		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06004BE5 RID: 19429 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual string CodeBase
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the URI, including escape characters, that represents the codebase.</summary>
		/// <returns>A URI with escape characters.</returns>
		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06004BE6 RID: 19430 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual string EscapedCodeBase
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the display name of the assembly.</summary>
		/// <returns>The display name of the assembly.</returns>
		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06004BE7 RID: 19431 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual string FullName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the entry point of this assembly.</summary>
		/// <returns>An object that represents the entry point of this assembly. If no entry point is found (for example, the assembly is a DLL), <see langword="null" /> is returned.</returns>
		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06004BE8 RID: 19432 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual MethodInfo EntryPoint
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the evidence for this assembly.</summary>
		/// <returns>The evidence for this assembly.</returns>
		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06004BE9 RID: 19433 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual Evidence Evidence
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal virtual Evidence UnprotectedGetEvidence()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06004BEB RID: 19435 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal virtual IntPtr MonoAssembly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (set) Token: 0x06004BEC RID: 19436 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal virtual bool FromByteArray
		{
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the full path or UNC location of the loaded file that contains the manifest.</summary>
		/// <returns>The location of the loaded file that contains the manifest. If the loaded file was shadow-copied, the location is that of the file after being shadow-copied. If the assembly is loaded from a byte array, such as when using the <see cref="M:System.Reflection.Assembly.Load(System.Byte[])" /> method overload, the value returned is an empty string ("").</returns>
		/// <exception cref="T:System.NotSupportedException">The current assembly is a dynamic assembly, represented by an <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> object.</exception>
		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06004BED RID: 19437 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual string Location
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a string representing the version of the common language runtime (CLR) saved in the file containing the manifest.</summary>
		/// <returns>The CLR version folder name. This is not a full path.</returns>
		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06004BEE RID: 19438 RVA: 0x000479F8 File Offset: 0x00045BF8
		[ComVisible(false)]
		public virtual string ImageRuntimeVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets serialization information with all of the data needed to reinstantiate this assembly.</summary>
		/// <param name="info">The object to be populated with serialization information.</param>
		/// <param name="context">The destination context of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06004BEF RID: 19439 RVA: 0x000479F8 File Offset: 0x00045BF8
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether or not a specified attribute has been applied to the assembly.</summary>
		/// <param name="attributeType">The type of the attribute to be checked for this assembly.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <returns>
		///   <see langword="true" /> if the attribute has been applied to the assembly; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> uses an invalid type.</exception>
		// Token: 0x06004BF0 RID: 19440 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets all the custom attributes for this assembly.</summary>
		/// <param name="inherit">This argument is ignored for objects of type <see cref="T:System.Reflection.Assembly" />.</param>
		/// <returns>An array that contains the custom attributes for this assembly.</returns>
		// Token: 0x06004BF1 RID: 19441 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the custom attributes for this assembly as specified by type.</summary>
		/// <param name="attributeType">The type for which the custom attributes are to be returned.</param>
		/// <param name="inherit">This argument is ignored for objects of type <see cref="T:System.Reflection.Assembly" />.</param>
		/// <returns>An array that contains the custom attributes for this assembly as specified by <paramref name="attributeType" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a runtime type.</exception>
		// Token: 0x06004BF2 RID: 19442 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the files in the file table of an assembly manifest.</summary>
		/// <returns>An array of streams that contain the files.</returns>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">A file was not found.</exception>
		/// <exception cref="T:System.BadImageFormatException">A file was not a valid assembly.</exception>
		// Token: 0x06004BF3 RID: 19443 RVA: 0x000F19E1 File Offset: 0x000EFBE1
		public virtual FileStream[] GetFiles()
		{
			return this.GetFiles(false);
		}

		/// <summary>Gets the files in the file table of an assembly manifest, specifying whether to include resource modules.</summary>
		/// <param name="getResourceModules">
		///   <see langword="true" /> to include resource modules; otherwise, <see langword="false" />.</param>
		/// <returns>An array of streams that contain the files.</returns>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">A file was not found.</exception>
		/// <exception cref="T:System.BadImageFormatException">A file was not a valid assembly.</exception>
		// Token: 0x06004BF4 RID: 19444 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual FileStream[] GetFiles(bool getResourceModules)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.IO.FileStream" /> for the specified file in the file table of the manifest of this assembly.</summary>
		/// <param name="name">The name of the specified file. Do not include the path to the file.</param>
		/// <returns>A stream that contains the specified file, or <see langword="null" /> if the file is not found.</returns>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string ("").</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly.</exception>
		// Token: 0x06004BF5 RID: 19445 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual FileStream GetFile(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads the specified manifest resource from this assembly.</summary>
		/// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
		/// <returns>The manifest resource; or <see langword="null" /> if no resources were specified during compilation or if the resource is not visible to the caller.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string ("").</exception>
		/// <exception cref="T:System.IO.FileLoadException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.  
		///
		///
		///
		///
		///  A file that was found could not be loaded.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly.</exception>
		/// <exception cref="T:System.NotImplementedException">Resource length is greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06004BF6 RID: 19446 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual Stream GetManifestResourceStream(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads the specified manifest resource, scoped by the namespace of the specified type, from this assembly.</summary>
		/// <param name="type">The type whose namespace is used to scope the manifest resource name.</param>
		/// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
		/// <returns>The manifest resource; or <see langword="null" /> if no resources were specified during compilation or if the resource is not visible to the caller.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string ("").</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly.</exception>
		/// <exception cref="T:System.NotImplementedException">Resource length is greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06004BF7 RID: 19447 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual Stream GetManifestResourceStream(Type type, string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x000F19EC File Offset: 0x000EFBEC
		internal Stream GetManifestResourceStream(Type type, string name, bool skipSecurityCheck, ref StackCrawlMark stackMark)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (type == null)
			{
				if (name == null)
				{
					throw new ArgumentNullException("type");
				}
			}
			else
			{
				string @namespace = type.Namespace;
				if (@namespace != null)
				{
					stringBuilder.Append(@namespace);
					if (name != null)
					{
						stringBuilder.Append(Type.Delimiter);
					}
				}
			}
			if (name != null)
			{
				stringBuilder.Append(name);
			}
			return this.GetManifestResourceStream(stringBuilder.ToString());
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x000F1A4E File Offset: 0x000EFC4E
		internal Stream GetManifestResourceStream(string name, ref StackCrawlMark stackMark, bool skipSecurityCheck)
		{
			return this.GetManifestResourceStream(null, name, skipSecurityCheck, ref stackMark);
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x000F1A5A File Offset: 0x000EFC5A
		internal string GetSimpleName()
		{
			return this.GetName(true).Name;
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x000F1A68 File Offset: 0x000EFC68
		internal byte[] GetPublicKey()
		{
			return this.GetName(true).GetPublicKey();
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x000F1A76 File Offset: 0x000EFC76
		internal Version GetVersion()
		{
			return this.GetName(true).Version;
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x000F1A84 File Offset: 0x000EFC84
		private AssemblyNameFlags GetFlags()
		{
			return this.GetName(true).Flags;
		}

		// Token: 0x06004BFE RID: 19454
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal virtual extern Type[] GetTypes(bool exportedOnly);

		/// <summary>Gets the types defined in this assembly.</summary>
		/// <returns>An array that contains all the types that are defined in this assembly.</returns>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">The assembly contains one or more types that cannot be loaded. The array returned by the <see cref="P:System.Reflection.ReflectionTypeLoadException.Types" /> property of this exception contains a <see cref="T:System.Type" /> object for each type that was loaded and <see langword="null" /> for each type that could not be loaded, while the <see cref="P:System.Reflection.ReflectionTypeLoadException.LoaderExceptions" /> property contains an exception for each type that could not be loaded.</exception>
		// Token: 0x06004BFF RID: 19455 RVA: 0x000F1A92 File Offset: 0x000EFC92
		public virtual Type[] GetTypes()
		{
			return this.GetTypes(false);
		}

		/// <summary>Gets the public types defined in this assembly that are visible outside the assembly.</summary>
		/// <returns>An array that represents the types defined in this assembly that are visible outside the assembly.</returns>
		/// <exception cref="T:System.NotSupportedException">The assembly is a dynamic assembly.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">Unable to load a dependent assembly.</exception>
		// Token: 0x06004C00 RID: 19456 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual Type[] GetExportedTypes()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object with the specified name in the assembly instance and optionally throws an exception if the type is not found.</summary>
		/// <param name="name">The full name of the type.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw an exception if the type is not found; <see langword="false" /> to return <see langword="null" />.</param>
		/// <returns>An object that represents the specified class.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is invalid.  
		/// -or-  
		/// The length of <paramref name="name" /> exceeds 1024 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is <see langword="true" />, and the type cannot be found.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> requires a dependent assembly that could not be found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="name" /> requires a dependent assembly that was found but could not be loaded.  
		/// -or-  
		/// The current assembly was loaded into the reflection-only context, and <paramref name="name" /> requires a dependent assembly that was not preloaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> requires a dependent assembly, but the file is not a valid assembly.  
		/// -or-  
		/// <paramref name="name" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x06004C01 RID: 19457 RVA: 0x000F1A9B File Offset: 0x000EFC9B
		public virtual Type GetType(string name, bool throwOnError)
		{
			return this.GetType(name, throwOnError, false);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object with the specified name in the assembly instance.</summary>
		/// <param name="name">The full name of the type.</param>
		/// <returns>An object that represents the specified class, or <see langword="null" /> if the class is not found.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> requires a dependent assembly that could not be found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.  
		///
		///
		///     <paramref name="name" /> requires a dependent assembly that was found but could not be loaded.  
		///  -or-  
		///  The current assembly was loaded into the reflection-only context, and <paramref name="name" /> requires a dependent assembly that was not preloaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> requires a dependent assembly, but the file is not a valid assembly.  
		/// -or-  
		/// <paramref name="name" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x06004C02 RID: 19458 RVA: 0x000F1AA6 File Offset: 0x000EFCA6
		public virtual Type GetType(string name)
		{
			return this.GetType(name, false, false);
		}

		// Token: 0x06004C03 RID: 19459
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type InternalGetType(Module module, string name, bool throwOnError, bool ignoreCase);

		// Token: 0x06004C04 RID: 19460
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalGetAssemblyName(string assemblyFile, out MonoAssemblyName aname, out string codebase);

		/// <summary>Gets an <see cref="T:System.Reflection.AssemblyName" /> for this assembly, setting the codebase as specified by <paramref name="copiedName" />.</summary>
		/// <param name="copiedName">
		///   <see langword="true" /> to set the <see cref="P:System.Reflection.Assembly.CodeBase" /> to the location of the assembly after it was shadow copied; <see langword="false" /> to set <see cref="P:System.Reflection.Assembly.CodeBase" /> to the original location.</param>
		/// <returns>An object that contains the fully parsed display name for this assembly.</returns>
		// Token: 0x06004C05 RID: 19461 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual AssemblyName GetName(bool copiedName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an <see cref="T:System.Reflection.AssemblyName" /> for this assembly.</summary>
		/// <returns>An object that contains the fully parsed display name for this assembly.</returns>
		// Token: 0x06004C06 RID: 19462 RVA: 0x000F1AB1 File Offset: 0x000EFCB1
		public virtual AssemblyName GetName()
		{
			return this.GetName(false);
		}

		/// <summary>Returns the full name of the assembly, also known as the display name.</summary>
		/// <returns>The full name of the assembly, or the class name if the full name of the assembly cannot be determined.</returns>
		// Token: 0x06004C07 RID: 19463 RVA: 0x00097E1B File Offset: 0x0009601B
		public override string ToString()
		{
			return base.ToString();
		}

		/// <summary>Creates the name of a type qualified by the display name of its assembly.</summary>
		/// <param name="assemblyName">The display name of an assembly.</param>
		/// <param name="typeName">The full name of a type.</param>
		/// <returns>The full name of the type qualified by the display name of the assembly.</returns>
		// Token: 0x06004C08 RID: 19464 RVA: 0x000F1ABA File Offset: 0x000EFCBA
		public static string CreateQualifiedName(string assemblyName, string typeName)
		{
			return typeName + ", " + assemblyName;
		}

		/// <summary>Gets the currently loaded assembly in which the specified type is defined.</summary>
		/// <param name="type">An object representing a type in the assembly that will be returned.</param>
		/// <returns>The assembly in which the specified type is defined.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06004C09 RID: 19465 RVA: 0x000F1AC8 File Offset: 0x000EFCC8
		public static Assembly GetAssembly(Type type)
		{
			if (type != null)
			{
				return type.Assembly;
			}
			throw new ArgumentNullException("type");
		}

		/// <summary>Gets the process executable in the default application domain. In other application domains, this is the first executable that was executed by <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" />.</summary>
		/// <returns>The assembly that is the process executable in the default application domain, or the first executable that was executed by <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" />. Can return <see langword="null" /> when called from unmanaged code.</returns>
		// Token: 0x06004C0A RID: 19466
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetEntryAssembly();

		// Token: 0x06004C0B RID: 19467 RVA: 0x000F1AE4 File Offset: 0x000EFCE4
		internal Assembly GetSatelliteAssembly(CultureInfo culture, Version version, bool throwOnError, ref StackCrawlMark stackMark)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			string name = this.GetSimpleName() + ".resources";
			return this.InternalGetSatelliteAssembly(name, culture, version, true, ref stackMark);
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x000F1B1C File Offset: 0x000EFD1C
		internal RuntimeAssembly InternalGetSatelliteAssembly(string name, CultureInfo culture, Version version, bool throwOnFileNotFound, ref StackCrawlMark stackMark)
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.SetPublicKey(this.GetPublicKey());
			assemblyName.Flags = (this.GetFlags() | AssemblyNameFlags.PublicKey);
			if (version == null)
			{
				assemblyName.Version = this.GetVersion();
			}
			else
			{
				assemblyName.Version = version;
			}
			assemblyName.CultureInfo = culture;
			assemblyName.Name = name;
			try
			{
				Assembly assembly = AppDomain.CurrentDomain.LoadSatellite(assemblyName, false, ref stackMark);
				if (assembly != null)
				{
					return (RuntimeAssembly)assembly;
				}
			}
			catch (FileNotFoundException)
			{
			}
			if (string.IsNullOrEmpty(this.Location))
			{
				return null;
			}
			string text = Path.Combine(Path.GetDirectoryName(this.Location), Path.Combine(culture.Name, assemblyName.Name + ".dll"));
			RuntimeAssembly result;
			try
			{
				result = (RuntimeAssembly)Assembly.LoadFrom(text, false, ref stackMark);
			}
			catch
			{
				if (throwOnFileNotFound || File.Exists(text))
				{
					throw;
				}
				result = null;
			}
			return result;
		}

		/// <summary>Returns the type of the current instance.</summary>
		/// <returns>An object that represents the <see cref="T:System.Reflection.Assembly" /> type.</returns>
		// Token: 0x06004C0D RID: 19469 RVA: 0x00047210 File Offset: 0x00045410
		Type _Assembly.GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004C0E RID: 19470
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly LoadFrom(string assemblyFile, bool refOnly, ref StackCrawlMark stackMark);

		// Token: 0x06004C0F RID: 19471
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly LoadFile_internal(string assemblyFile, ref StackCrawlMark stackMark);

		/// <summary>Loads an assembly given its file name or path.</summary>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.
		/// -or-
		/// The ability to execute code in remote assemblies is disabled. See &lt;loadFromRemoteSources&gt;.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string ("").</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name exceeds the system-defined maximum length.</exception>
		// Token: 0x06004C10 RID: 19472 RVA: 0x000F1C1C File Offset: 0x000EFE1C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Assembly.LoadFrom(assemblyFile, false, ref stackCrawlMark);
		}

		/// <summary>Loads an assembly given its file name or path and supplying security evidence.</summary>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly.</param>
		/// <param name="securityEvidence">Evidence for loading the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.  
		///  -or-  
		///  The <paramref name="securityEvidence" /> is not ambiguous and is determined to be invalid.
		/// -or-
		/// The ability to execute code in remote assemblies is disabled. See &lt;loadFromRemoteSources&gt;.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string ("").</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name exceeds the system-defined maximum length.</exception>
		// Token: 0x06004C11 RID: 19473 RVA: 0x000F1C34 File Offset: 0x000EFE34
		[Obsolete]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Assembly assembly = Assembly.LoadFrom(assemblyFile, false, ref stackCrawlMark);
			if (assembly != null && securityEvidence != null)
			{
				assembly.Evidence.Merge(securityEvidence);
			}
			return assembly;
		}

		/// <summary>Loads an assembly given its file name or path, security evidence, hash value, and hash algorithm.</summary>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly.</param>
		/// <param name="securityEvidence">Evidence for loading the assembly.</param>
		/// <param name="hashValue">The value of the computed hash code.</param>
		/// <param name="hashAlgorithm">The hash algorithm used for hashing files and for generating the strong name.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.  
		///  -or-  
		///  The <paramref name="securityEvidence" /> is not ambiguous and is determined to be invalid.
		/// -or-
		/// The ability to execute code in remote assemblies is disabled. See &lt;loadFromRemoteSources&gt;.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string ("").</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name exceeds the system-defined maximum length.</exception>
		// Token: 0x06004C12 RID: 19474 RVA: 0x000479F8 File Offset: 0x00045BF8
		[Obsolete]
		[MonoTODO("This overload is not currently implemented")]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads an assembly given its file name or path, hash value, and hash algorithm.</summary>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly.</param>
		/// <param name="hashValue">The value of the computed hash code.</param>
		/// <param name="hashAlgorithm">The hash algorithm used for hashing files and for generating the strong name.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a file name extension.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.
		/// -or-
		/// The ability to execute code in remote assemblies is disabled. See &lt;loadFromRemoteSources&gt;.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly; for example, a 32-bit assembly in a 64-bit process. See the exception topic for more information.  
		/// -or-  
		/// <paramref name="assemblyFile" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string ("").</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name exceeds the system-defined maximum length.</exception>
		// Token: 0x06004C13 RID: 19475 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public static Assembly LoadFrom(string assemblyFile, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads an assembly into the load-from context, bypassing some security checks.</summary>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly.  
		/// -or-  
		/// <paramref name="assemblyFile" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string ("").</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name exceeds the system-defined maximum length.</exception>
		// Token: 0x06004C14 RID: 19476 RVA: 0x000F1C68 File Offset: 0x000EFE68
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly UnsafeLoadFrom(string assemblyFile)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Assembly.LoadFrom(assemblyFile, false, ref stackCrawlMark);
		}

		/// <summary>Loads an assembly given its path, loading the assembly into the domain of the caller using the supplied evidence.</summary>
		/// <param name="path">The fully qualified path of the assembly file.</param>
		/// <param name="securityEvidence">Evidence for loading the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> argument is not an absolute path.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The <paramref name="path" /> parameter is an empty string ("") or does not exist.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.
		/// -or-
		/// The ability to execute code in remote assemblies is disabled. See &lt;loadFromRemoteSources&gt;.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="path" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="path" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="securityEvidence" /> is not <see langword="null" />. By default, legacy CAS policy is not enabled in the .NET Framework 4; when it is not enabled, <paramref name="securityEvidence" /> must be <see langword="null" />.</exception>
		// Token: 0x06004C15 RID: 19477 RVA: 0x000F1C80 File Offset: 0x000EFE80
		[Obsolete]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFile(string path, Evidence securityEvidence)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path == string.Empty)
			{
				throw new ArgumentException("Path can't be empty", "path");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Assembly assembly = Assembly.LoadFile_internal(path, ref stackCrawlMark);
			if (assembly != null && securityEvidence != null)
			{
				throw new NotImplementedException();
			}
			return assembly;
		}

		/// <summary>Loads the contents of an assembly file on the specified path.</summary>
		/// <param name="path">The fully qualified path of the file to load.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> argument is not an absolute path.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.
		/// -or-
		/// The ability to execute code in remote assemblies is disabled. See &lt;loadFromRemoteSources&gt;.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The <paramref name="path" /> parameter is an empty string ("") or does not exist.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="path" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="path" /> was compiled with a later version.</exception>
		// Token: 0x06004C16 RID: 19478 RVA: 0x000F1CD4 File Offset: 0x000EFED4
		public static Assembly LoadFile(string path)
		{
			return Assembly.LoadFile(path, null);
		}

		/// <summary>Loads an assembly given the long form of its name.</summary>
		/// <param name="assemblyString">The long form of the assembly name.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyString" /> is a zero-length string.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		// Token: 0x06004C17 RID: 19479 RVA: 0x000F1CDD File Offset: 0x000EFEDD
		public static Assembly Load(string assemblyString)
		{
			return AppDomain.CurrentDomain.Load(assemblyString);
		}

		/// <summary>Loads an assembly given its display name, loading the assembly into the domain of the caller using the supplied evidence.</summary>
		/// <param name="assemblyString">The display name of the assembly.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.  
		///  -or-  
		///  An assembly or module was loaded twice with two different evidences.</exception>
		// Token: 0x06004C18 RID: 19480 RVA: 0x000F1CEA File Offset: 0x000EFEEA
		[Obsolete]
		public static Assembly Load(string assemblyString, Evidence assemblySecurity)
		{
			return AppDomain.CurrentDomain.Load(assemblyString, assemblySecurity);
		}

		/// <summary>Loads an assembly given its <see cref="T:System.Reflection.AssemblyName" />.</summary>
		/// <param name="assemblyRef">The object that describes the assembly to be loaded.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyRef" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyRef" /> is not found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.  
		///
		///
		///
		///
		///  A file that was found could not be loaded.
		/// -or-
		/// <paramref name="assemblyRef" /> specifies a remote assembly, but the ability to execute code in remote assemblies is disabled. See &lt;loadFromRemoteSources&gt;.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyRef" /> is not a valid assembly. -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyRef" /> was compiled with a later version.</exception>
		// Token: 0x06004C19 RID: 19481 RVA: 0x000F1CF8 File Offset: 0x000EFEF8
		public static Assembly Load(AssemblyName assemblyRef)
		{
			return AppDomain.CurrentDomain.Load(assemblyRef);
		}

		/// <summary>Loads an assembly given its <see cref="T:System.Reflection.AssemblyName" />. The assembly is loaded into the domain of the caller using the supplied evidence.</summary>
		/// <param name="assemblyRef">The object that describes the assembly to be loaded.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyRef" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyRef" /> is not found.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyRef" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyRef" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.
		/// -or-
		/// <paramref name="assemblyRef" /> specifies a remote assembly, but the ability to execute code in remote assemblies is disabled. See &lt;loadFromRemoteSources&gt;.</exception>
		// Token: 0x06004C1A RID: 19482 RVA: 0x000F1D05 File Offset: 0x000EFF05
		[Obsolete]
		public static Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
		{
			return AppDomain.CurrentDomain.Load(assemblyRef, assemblySecurity);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly. The assembly is loaded into the application domain of the caller.</summary>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		// Token: 0x06004C1B RID: 19483 RVA: 0x000F1D13 File Offset: 0x000EFF13
		public static Assembly Load(byte[] rawAssembly)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly, optionally including symbols for the assembly. The assembly is loaded into the application domain of the caller.</summary>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly.</param>
		/// <param name="rawSymbolStore">A byte array that contains the raw bytes representing the symbols for the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		// Token: 0x06004C1C RID: 19484 RVA: 0x000F1D20 File Offset: 0x000EFF20
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly, optionally including symbols and evidence for the assembly. The assembly is loaded into the application domain of the caller.</summary>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly.</param>
		/// <param name="rawSymbolStore">A byte array that contains the raw bytes representing the symbols for the assembly.</param>
		/// <param name="securityEvidence">Evidence for loading the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="securityEvidence" /> is not <see langword="null" />.  By default, legacy CAS policy is not enabled in the .NET Framework 4; when it is not enabled, <paramref name="securityEvidence" /> must be <see langword="null" />.</exception>
		// Token: 0x06004C1D RID: 19485 RVA: 0x000F1D2E File Offset: 0x000EFF2E
		[Obsolete]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore, securityEvidence);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly, optionally including symbols and specifying the source for the security context. The assembly is loaded into the application domain of the caller.</summary>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly.</param>
		/// <param name="rawSymbolStore">A byte array that contains the raw bytes representing the symbols for the assembly.</param>
		/// <param name="securityContextSource">The source of the security context.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly.  
		/// -or-  
		/// <paramref name="rawAssembly" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="securityContextSource" /> is not one of the enumeration values.</exception>
		// Token: 0x06004C1E RID: 19486 RVA: 0x000F1D20 File Offset: 0x000EFF20
		[MonoLimitation("Argument securityContextSource is ignored")]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, SecurityContextSource securityContextSource)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore);
		}

		/// <summary>Loads the assembly from a common object file format (COFF)-based image containing an emitted assembly. The assembly is loaded into the reflection-only context of the caller's application domain.</summary>
		/// <param name="rawAssembly">A byte array that is a COFF-based image containing an emitted assembly.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="rawAssembly" /> cannot be loaded.</exception>
		// Token: 0x06004C1F RID: 19487 RVA: 0x000F1D3D File Offset: 0x000EFF3D
		public static Assembly ReflectionOnlyLoad(byte[] rawAssembly)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, null, null, true);
		}

		/// <summary>Loads an assembly into the reflection-only context, given its display name.</summary>
		/// <param name="assemblyString">The display name of the assembly, as returned by the <see cref="P:System.Reflection.AssemblyName.FullName" /> property.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyString" /> is an empty string ("").</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="assemblyString" /> is found, but cannot be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		// Token: 0x06004C20 RID: 19488 RVA: 0x000F1D50 File Offset: 0x000EFF50
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly ReflectionOnlyLoad(string assemblyString)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return AppDomain.CurrentDomain.Load(assemblyString, null, true, ref stackCrawlMark);
		}

		/// <summary>Loads an assembly into the reflection-only context, given its path.</summary>
		/// <param name="assemblyFile">The path of the file that contains the manifest of the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a file name extension.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="assemblyFile" /> is found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyFile" /> is an empty string ("").</exception>
		// Token: 0x06004C21 RID: 19489 RVA: 0x000F1D70 File Offset: 0x000EFF70
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly ReflectionOnlyLoadFrom(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Assembly.LoadFrom(assemblyFile, true, ref stackCrawlMark);
		}

		/// <summary>Loads an assembly from the application directory or from the global assembly cache using a partial name.</summary>
		/// <param name="partialName">The display name of the assembly.</param>
		/// <returns>The loaded assembly. If <paramref name="partialName" /> is not found, this method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="partialName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="partialName" /> was compiled with a later version.</exception>
		// Token: 0x06004C22 RID: 19490 RVA: 0x000F1D96 File Offset: 0x000EFF96
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static Assembly LoadWithPartialName(string partialName)
		{
			return Assembly.LoadWithPartialName(partialName, null);
		}

		/// <summary>Loads the module, internal to this assembly, with a common object file format (COFF)-based image containing an emitted module, or a resource file.</summary>
		/// <param name="moduleName">The name of the module. This string must correspond to a file name in this assembly's manifest.</param>
		/// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource.</param>
		/// <returns>The loaded module.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="moduleName" /> or <paramref name="rawModule" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="moduleName" /> does not match a file entry in this assembly's manifest.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawModule" /> is not a valid module.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.</exception>
		// Token: 0x06004C23 RID: 19491 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO("Not implemented")]
		public Module LoadModule(string moduleName, byte[] rawModule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads the module, internal to this assembly, with a common object file format (COFF)-based image containing an emitted module, or a resource file. The raw bytes representing the symbols for the module are also loaded.</summary>
		/// <param name="moduleName">The name of the module. This string must correspond to a file name in this assembly's manifest.</param>
		/// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource.</param>
		/// <param name="rawSymbolStore">A byte array containing the raw bytes representing the symbols for the module. Must be <see langword="null" /> if this is a resource file.</param>
		/// <returns>The loaded module.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="moduleName" /> or <paramref name="rawModule" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="moduleName" /> does not match a file entry in this assembly's manifest.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawModule" /> is not a valid module.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.</exception>
		// Token: 0x06004C24 RID: 19492 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO("Not implemented")]
		public virtual Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004C25 RID: 19493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly load_with_partial_name(string name, Evidence e);

		/// <summary>Loads an assembly from the application directory or from the global assembly cache using a partial name. The assembly is loaded into the domain of the caller using the supplied evidence.</summary>
		/// <param name="partialName">The display name of the assembly.</param>
		/// <param name="securityEvidence">Evidence for loading the assembly.</param>
		/// <returns>The loaded assembly. If <paramref name="partialName" /> is not found, this method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different sets of evidence.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="partialName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="partialName" /> was compiled with a later version.</exception>
		// Token: 0x06004C26 RID: 19494 RVA: 0x000F1D9F File Offset: 0x000EFF9F
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence)
		{
			return Assembly.LoadWithPartialName(partialName, securityEvidence, true);
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x000F1DA9 File Offset: 0x000EFFA9
		internal static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence, bool oldBehavior)
		{
			if (!oldBehavior)
			{
				throw new NotImplementedException();
			}
			if (partialName == null)
			{
				throw new NullReferenceException();
			}
			return Assembly.load_with_partial_name(partialName, securityEvidence);
		}

		/// <summary>Locates the specified type from this assembly and creates an instance of it using the system activator, using case-sensitive search.</summary>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate.</param>
		/// <returns>An instance of the specified type created with the default constructor; or <see langword="null" /> if <paramref name="typeName" /> is not found. The type is resolved using the default binder, without specifying culture or activation attributes, and with <see cref="T:System.Reflection.BindingFlags" /> set to <see langword="Public" /> or <see langword="Instance" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> is an empty string ("") or a string beginning with a null character.  
		/// -or-  
		/// The current assembly was loaded into the reflection-only context.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="typeName" /> requires a dependent assembly that could not be found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="typeName" /> requires a dependent assembly that was found but could not be loaded.  
		/// -or-  
		/// The current assembly was loaded into the reflection-only context, and <paramref name="typeName" /> requires a dependent assembly that was not preloaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeName" /> requires a dependent assembly, but the file is not a valid assembly.  
		/// -or-  
		/// <paramref name="typeName" /> requires a dependent assembly that was compiled for a version of the runtime that is later than the currently loaded version.</exception>
		// Token: 0x06004C28 RID: 19496 RVA: 0x000F1DC4 File Offset: 0x000EFFC4
		public object CreateInstance(string typeName)
		{
			return this.CreateInstance(typeName, false);
		}

		/// <summary>Locates the specified type from this assembly and creates an instance of it using the system activator, with optional case-sensitive search.</summary>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to ignore the case of the type name; otherwise, <see langword="false" />.</param>
		/// <returns>An instance of the specified type created with the default constructor; or <see langword="null" /> if <paramref name="typeName" /> is not found. The type is resolved using the default binder, without specifying culture or activation attributes, and with <see cref="T:System.Reflection.BindingFlags" /> set to <see langword="Public" /> or <see langword="Instance" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> is an empty string ("") or a string beginning with a null character.  
		/// -or-  
		/// The current assembly was loaded into the reflection-only context.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="typeName" /> requires a dependent assembly that could not be found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="typeName" /> requires a dependent assembly that was found but could not be loaded.  
		/// -or-  
		/// The current assembly was loaded into the reflection-only context, and <paramref name="typeName" /> requires a dependent assembly that was not preloaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeName" /> requires a dependent assembly, but the file is not a valid assembly.  
		/// -or-  
		/// <paramref name="typeName" /> requires a dependent assembly that was compiled for a version of the runtime that is later than the currently loaded version.</exception>
		// Token: 0x06004C29 RID: 19497 RVA: 0x000F1DD0 File Offset: 0x000EFFD0
		public object CreateInstance(string typeName, bool ignoreCase)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			object result;
			try
			{
				result = Activator.CreateInstance(type);
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException("It is illegal to invoke a method on a Type loaded via ReflectionOnly methods.");
			}
			return result;
		}

		/// <summary>Locates the specified type from this assembly and creates an instance of it using the system activator, with optional case-sensitive search and having the specified culture, arguments, and binding and activation attributes.</summary>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to ignore the case of the type name; otherwise, <see langword="false" />.</param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects via reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="args">An array that contains the arguments to be passed to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to be invoked. If the default constructor is desired, <paramref name="args" /> must be an empty array or <see langword="null" />.</param>
		/// <param name="culture">An instance of <see langword="CultureInfo" /> used to govern the coercion of types. If this is <see langword="null" />, the <see langword="CultureInfo" /> for the current thread is used. (This is necessary to convert a <see langword="String" /> that represents 1000 to a <see langword="Double" /> value, for example, since 1000 is represented differently by different cultures.)</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object that specifies the URL that is required to activate a remote object.  This parameter is related to client-activated objects. Client activation is a legacy technology that is retained for backward compatibility but is not recommended for new development. Distributed applications should instead use Windows Communication Foundation.</param>
		/// <returns>An instance of the specified type, or <see langword="null" /> if <paramref name="typeName" /> is not found. The supplied arguments are used to resolve the type, and to bind the constructor that is used to create the instance.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> is an empty string ("") or a string beginning with a null character.  
		/// -or-  
		/// The current assembly was loaded into the reflection-only context.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found.</exception>
		/// <exception cref="T:System.NotSupportedException">A non-empty activation attributes array is passed to a type that does not inherit from <see cref="T:System.MarshalByRefObject" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="typeName" /> requires a dependent assembly that could not be found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="typeName" /> requires a dependent assembly that was found but could not be loaded.  
		/// -or-  
		/// The current assembly was loaded into the reflection-only context, and <paramref name="typeName" /> requires a dependent assembly that was not preloaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeName" /> requires a dependent assembly, but the file is not a valid assembly.  
		/// -or-  
		/// <paramref name="typeName" /> requires a dependent assembly which that was compiled for a version of the runtime that is later than the currently loaded version.</exception>
		// Token: 0x06004C2A RID: 19498 RVA: 0x000F1E1C File Offset: 0x000F001C
		public virtual object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			object result;
			try
			{
				result = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException("It is illegal to invoke a method on a Type loaded via ReflectionOnly methods.");
			}
			return result;
		}

		/// <summary>Gets all the loaded modules that are part of this assembly.</summary>
		/// <returns>An array of modules.</returns>
		// Token: 0x06004C2B RID: 19499 RVA: 0x000F1E70 File Offset: 0x000F0070
		public Module[] GetLoadedModules()
		{
			return this.GetLoadedModules(false);
		}

		/// <summary>Gets all the modules that are part of this assembly.</summary>
		/// <returns>An array of modules.</returns>
		/// <exception cref="T:System.IO.FileNotFoundException">The module to be loaded does not specify a file name extension.</exception>
		// Token: 0x06004C2C RID: 19500 RVA: 0x000F1E79 File Offset: 0x000F0079
		public Module[] GetModules()
		{
			return this.GetModules(false);
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal virtual Module[] GetModulesInternal()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the assembly that contains the code that is currently executing.</summary>
		/// <returns>The assembly that contains the code that is currently executing.</returns>
		// Token: 0x06004C2E RID: 19502
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetExecutingAssembly();

		/// <summary>Returns the <see cref="T:System.Reflection.Assembly" /> of the method that invoked the currently executing method.</summary>
		/// <returns>The <see langword="Assembly" /> object of the method that invoked the currently executing method.</returns>
		// Token: 0x06004C2F RID: 19503
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetCallingAssembly();

		// Token: 0x06004C30 RID: 19504
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr InternalGetReferencedAssemblies(Assembly module);

		/// <summary>Returns the names of all the resources in this assembly.</summary>
		/// <returns>An array that contains the names of all the resources.</returns>
		// Token: 0x06004C31 RID: 19505 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual string[] GetManifestResourceNames()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x000F1E84 File Offset: 0x000F0084
		internal unsafe static AssemblyName[] GetReferencedAssemblies(Assembly module)
		{
			AssemblyName[] result;
			using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(Assembly.InternalGetReferencedAssemblies(module)))
			{
				int length = safeGPtrArrayHandle.Length;
				try
				{
					AssemblyName[] array = new AssemblyName[length];
					for (int i = 0; i < length; i++)
					{
						AssemblyName assemblyName = new AssemblyName();
						MonoAssemblyName* native = (MonoAssemblyName*)((void*)safeGPtrArrayHandle[i]);
						assemblyName.FillName(native, null, true, false, true, true);
						array[i] = assemblyName;
					}
					result = array;
				}
				finally
				{
					for (int j = 0; j < length; j++)
					{
						MonoAssemblyName* name = (MonoAssemblyName*)((void*)safeGPtrArrayHandle[j]);
						RuntimeMarshal.FreeAssemblyName(ref *name, true);
					}
				}
			}
			return result;
		}

		/// <summary>Returns information about how the given resource has been persisted.</summary>
		/// <param name="resourceName">The case-sensitive name of the resource.</param>
		/// <returns>An object that is populated with information about the resource's topology, or <see langword="null" /> if the resource is not found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="resourceName" /> parameter is an empty string ("").</exception>
		// Token: 0x06004C33 RID: 19507 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the host context with which the assembly was loaded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the host context with which the assembly was loaded, if any.</returns>
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06004C34 RID: 19508 RVA: 0x0005CD46 File Offset: 0x0005AF46
		[MonoTODO("Currently it always returns zero")]
		[ComVisible(false)]
		public virtual long HostContext
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x06004C35 RID: 19509 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal virtual Module GetManifestModule()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value indicating whether this assembly was loaded into the reflection-only context.</summary>
		/// <returns>
		///   <see langword="true" /> if the assembly was loaded into the reflection-only context, rather than the execution context; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06004C36 RID: 19510 RVA: 0x000479F8 File Offset: 0x00045BF8
		[ComVisible(false)]
		public virtual bool ReflectionOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06004C37 RID: 19511 RVA: 0x000930D0 File Offset: 0x000912D0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines whether this assembly and the specified object are equal.</summary>
		/// <param name="o">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004C38 RID: 19512 RVA: 0x00097E12 File Offset: 0x00096012
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06004C39 RID: 19513 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal virtual PermissionSet GrantedPermissionSet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06004C3A RID: 19514 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal virtual PermissionSet DeniedPermissionSet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the grant set of the current assembly.</summary>
		/// <returns>The grant set of the current assembly.</returns>
		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06004C3B RID: 19515 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual PermissionSet PermissionSet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates which set of security rules the common language runtime (CLR) enforces for this assembly.</summary>
		/// <returns>The security rule set that the CLR enforces for this assembly.</returns>
		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06004C3C RID: 19516 RVA: 0x000F1F3C File Offset: 0x000F013C
		public virtual SecurityRuleSet SecurityRuleSet
		{
			get
			{
				throw Assembly.CreateNIE();
			}
		}

		// Token: 0x06004C3D RID: 19517 RVA: 0x000F1F43 File Offset: 0x000F0143
		private static Exception CreateNIE()
		{
			return new NotImplementedException("Derived classes must implement it");
		}

		/// <summary>Returns information about the attributes that have been applied to the current <see cref="T:System.Reflection.Assembly" />, expressed as <see cref="T:System.Reflection.CustomAttributeData" /> objects.</summary>
		/// <returns>A generic list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the current assembly.</returns>
		// Token: 0x06004C3E RID: 19518 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value that indicates whether the current assembly is loaded with full trust.</summary>
		/// <returns>
		///   <see langword="true" /> if the current assembly is loaded with full trust; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06004C3F RID: 19519 RVA: 0x000040F7 File Offset: 0x000022F7
		[MonoTODO]
		public bool IsFullyTrusted
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object with the specified name in the assembly instance, with the options of ignoring the case, and of throwing an exception if the type is not found.</summary>
		/// <param name="name">The full name of the type.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw an exception if the type is not found; <see langword="false" /> to return <see langword="null" />.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to ignore the case of the type name; otherwise, <see langword="false" />.</param>
		/// <returns>An object that represents the specified class.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is invalid.  
		/// -or-  
		/// The length of <paramref name="name" /> exceeds 1024 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is <see langword="true" />, and the type cannot be found.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> requires a dependent assembly that could not be found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="name" /> requires a dependent assembly that was found but could not be loaded.  
		/// -or-  
		/// The current assembly was loaded into the reflection-only context, and <paramref name="name" /> requires a dependent assembly that was not preloaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> requires a dependent assembly, but the file is not a valid assembly.  
		/// -or-  
		/// <paramref name="name" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x06004C40 RID: 19520 RVA: 0x000F1F3C File Offset: 0x000F013C
		public virtual Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets the specified module in this assembly.</summary>
		/// <param name="name">The name of the module being requested.</param>
		/// <returns>The module being requested, or <see langword="null" /> if the module is not found.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string ("").</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly.</exception>
		// Token: 0x06004C41 RID: 19521 RVA: 0x000F1F3C File Offset: 0x000F013C
		public virtual Module GetModule(string name)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets the <see cref="T:System.Reflection.AssemblyName" /> objects for all the assemblies referenced by this assembly.</summary>
		/// <returns>An array that contains the fully parsed display names of all the assemblies referenced by this assembly.</returns>
		// Token: 0x06004C42 RID: 19522 RVA: 0x000F1F3C File Offset: 0x000F013C
		public virtual AssemblyName[] GetReferencedAssemblies()
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets all the modules that are part of this assembly, specifying whether to include resource modules.</summary>
		/// <param name="getResourceModules">
		///   <see langword="true" /> to include resource modules; otherwise, <see langword="false" />.</param>
		/// <returns>An array of modules.</returns>
		// Token: 0x06004C43 RID: 19523 RVA: 0x000F1F3C File Offset: 0x000F013C
		public virtual Module[] GetModules(bool getResourceModules)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets all the loaded modules that are part of this assembly, specifying whether to include resource modules.</summary>
		/// <param name="getResourceModules">
		///   <see langword="true" /> to include resource modules; otherwise, <see langword="false" />.</param>
		/// <returns>An array of modules.</returns>
		// Token: 0x06004C44 RID: 19524 RVA: 0x000F1F3C File Offset: 0x000F013C
		[MonoTODO("Always returns the same as GetModules")]
		public virtual Module[] GetLoadedModules(bool getResourceModules)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets the satellite assembly for the specified culture.</summary>
		/// <param name="culture">The specified culture.</param>
		/// <returns>The specified satellite assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly cannot be found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">The satellite assembly with a matching file name was found, but the <see langword="CultureInfo" /> did not match the one specified.</exception>
		/// <exception cref="T:System.BadImageFormatException">The satellite assembly is not a valid assembly.</exception>
		// Token: 0x06004C45 RID: 19525 RVA: 0x000F1F3C File Offset: 0x000F013C
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			throw Assembly.CreateNIE();
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
		// Token: 0x06004C46 RID: 19526 RVA: 0x000F1F3C File Offset: 0x000F013C
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			throw Assembly.CreateNIE();
		}

		/// <summary>Gets the module that contains the manifest for the current assembly.</summary>
		/// <returns>The module that contains the manifest for the assembly.</returns>
		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06004C47 RID: 19527 RVA: 0x000F1F3C File Offset: 0x000F013C
		public virtual Module ManifestModule
		{
			get
			{
				throw Assembly.CreateNIE();
			}
		}

		/// <summary>Gets a value indicating whether the assembly was loaded from the global assembly cache.</summary>
		/// <returns>
		///   <see langword="true" /> if the assembly was loaded from the global assembly cache; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06004C48 RID: 19528 RVA: 0x000F1F3C File Offset: 0x000F013C
		public virtual bool GlobalAssemblyCache
		{
			get
			{
				throw Assembly.CreateNIE();
			}
		}

		/// <summary>Gets a value that indicates whether the current assembly was generated dynamically in the current process by using reflection emit.</summary>
		/// <returns>
		///   <see langword="true" /> if the current assembly was generated dynamically in the current process; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06004C49 RID: 19529 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsDynamic
		{
			get
			{
				return false;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Assembly" /> objects are equal.</summary>
		/// <param name="left">The assembly to compare to <paramref name="right" />.</param>
		/// <param name="right">The assembly to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004C4A RID: 19530 RVA: 0x000F1F4F File Offset: 0x000F014F
		public static bool operator ==(Assembly left, Assembly right)
		{
			return left == right || (!(left == null ^ right == null) && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Assembly" /> objects are not equal.</summary>
		/// <param name="left">The assembly to compare to <paramref name="right" />.</param>
		/// <param name="right">The assembly to compare to <paramref name="left" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004C4B RID: 19531 RVA: 0x000F1F6B File Offset: 0x000F016B
		public static bool operator !=(Assembly left, Assembly right)
		{
			return left != right && ((left == null ^ right == null) || !left.Equals(right));
		}

		/// <summary>Gets a collection of the types defined in this assembly.</summary>
		/// <returns>A collection of the types defined in this assembly.</returns>
		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06004C4C RID: 19532 RVA: 0x000F1F8A File Offset: 0x000F018A
		public virtual IEnumerable<TypeInfo> DefinedTypes
		{
			get
			{
				foreach (Type type in this.GetTypes())
				{
					yield return type.GetTypeInfo();
				}
				Type[] array = null;
				yield break;
			}
		}

		/// <summary>Gets a collection of the public types defined in this assembly that are visible outside the assembly.</summary>
		/// <returns>A collection of the public types defined in this assembly that are visible outside the assembly.</returns>
		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06004C4D RID: 19533 RVA: 0x000F1F9A File Offset: 0x000F019A
		public virtual IEnumerable<Type> ExportedTypes
		{
			get
			{
				return this.GetExportedTypes();
			}
		}

		/// <summary>Gets a collection that contains the modules in this assembly.</summary>
		/// <returns>A collection that contains the modules in this assembly.</returns>
		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06004C4E RID: 19534 RVA: 0x000F1FA2 File Offset: 0x000F01A2
		public virtual IEnumerable<Module> Modules
		{
			get
			{
				return this.GetModules();
			}
		}

		/// <summary>Gets a collection that contains this assembly's custom attributes.</summary>
		/// <returns>A collection that contains this assembly's custom attributes.</returns>
		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06004C4F RID: 19535 RVA: 0x000F1FAA File Offset: 0x000F01AA
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x0001B98B File Offset: 0x00019B8B
		public virtual Type[] GetForwardedTypes()
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Assembly" /> class.</summary>
		// Token: 0x06004C51 RID: 19537 RVA: 0x0000259F File Offset: 0x0000079F
		protected Assembly()
		{
		}

		// Token: 0x020008E9 RID: 2281
		internal class ResolveEventHolder
		{
			// Token: 0x1400001D RID: 29
			// (add) Token: 0x06004C52 RID: 19538 RVA: 0x000F1FB4 File Offset: 0x000F01B4
			// (remove) Token: 0x06004C53 RID: 19539 RVA: 0x000F1FEC File Offset: 0x000F01EC
			public event ModuleResolveEventHandler ModuleResolve
			{
				[CompilerGenerated]
				add
				{
					ModuleResolveEventHandler moduleResolveEventHandler = this.ModuleResolve;
					ModuleResolveEventHandler moduleResolveEventHandler2;
					do
					{
						moduleResolveEventHandler2 = moduleResolveEventHandler;
						ModuleResolveEventHandler value2 = (ModuleResolveEventHandler)Delegate.Combine(moduleResolveEventHandler2, value);
						moduleResolveEventHandler = Interlocked.CompareExchange<ModuleResolveEventHandler>(ref this.ModuleResolve, value2, moduleResolveEventHandler2);
					}
					while (moduleResolveEventHandler != moduleResolveEventHandler2);
				}
				[CompilerGenerated]
				remove
				{
					ModuleResolveEventHandler moduleResolveEventHandler = this.ModuleResolve;
					ModuleResolveEventHandler moduleResolveEventHandler2;
					do
					{
						moduleResolveEventHandler2 = moduleResolveEventHandler;
						ModuleResolveEventHandler value2 = (ModuleResolveEventHandler)Delegate.Remove(moduleResolveEventHandler2, value);
						moduleResolveEventHandler = Interlocked.CompareExchange<ModuleResolveEventHandler>(ref this.ModuleResolve, value2, moduleResolveEventHandler2);
					}
					while (moduleResolveEventHandler != moduleResolveEventHandler2);
				}
			}

			// Token: 0x06004C54 RID: 19540 RVA: 0x0000259F File Offset: 0x0000079F
			public ResolveEventHolder()
			{
			}

			// Token: 0x0400300D RID: 12301
			[CompilerGenerated]
			private ModuleResolveEventHandler ModuleResolve;
		}

		// Token: 0x020008EA RID: 2282
		[CompilerGenerated]
		private sealed class <get_DefinedTypes>d__127 : IEnumerable<TypeInfo>, IEnumerable, IEnumerator<TypeInfo>, IDisposable, IEnumerator
		{
			// Token: 0x06004C55 RID: 19541 RVA: 0x000F2021 File Offset: 0x000F0221
			[DebuggerHidden]
			public <get_DefinedTypes>d__127(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06004C56 RID: 19542 RVA: 0x00004BF9 File Offset: 0x00002DF9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06004C57 RID: 19543 RVA: 0x000F203C File Offset: 0x000F023C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Assembly assembly = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					i++;
				}
				else
				{
					this.<>1__state = -1;
					array = assembly.GetTypes();
					i = 0;
				}
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				Type type = array[i];
				this.<>2__current = type.GetTypeInfo();
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000C48 RID: 3144
			// (get) Token: 0x06004C58 RID: 19544 RVA: 0x000F20CC File Offset: 0x000F02CC
			TypeInfo IEnumerator<TypeInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004C59 RID: 19545 RVA: 0x000472C8 File Offset: 0x000454C8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000C49 RID: 3145
			// (get) Token: 0x06004C5A RID: 19546 RVA: 0x000F20CC File Offset: 0x000F02CC
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004C5B RID: 19547 RVA: 0x000F20D4 File Offset: 0x000F02D4
			[DebuggerHidden]
			IEnumerator<TypeInfo> IEnumerable<TypeInfo>.GetEnumerator()
			{
				Assembly.<get_DefinedTypes>d__127 <get_DefinedTypes>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_DefinedTypes>d__ = this;
				}
				else
				{
					<get_DefinedTypes>d__ = new Assembly.<get_DefinedTypes>d__127(0);
					<get_DefinedTypes>d__.<>4__this = this;
				}
				return <get_DefinedTypes>d__;
			}

			// Token: 0x06004C5C RID: 19548 RVA: 0x000F2117 File Offset: 0x000F0317
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.TypeInfo>.GetEnumerator();
			}

			// Token: 0x0400300E RID: 12302
			private int <>1__state;

			// Token: 0x0400300F RID: 12303
			private TypeInfo <>2__current;

			// Token: 0x04003010 RID: 12304
			private int <>l__initialThreadId;

			// Token: 0x04003011 RID: 12305
			public Assembly <>4__this;

			// Token: 0x04003012 RID: 12306
			private Type[] <>7__wrap1;

			// Token: 0x04003013 RID: 12307
			private int <>7__wrap2;
		}
	}
}
