using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Discovers attributed parts in a managed code assembly.</summary>
	// Token: 0x020000A1 RID: 161
	[DebuggerTypeProxy(typeof(AssemblyCatalogDebuggerProxy))]
	public class AssemblyCatalog : ComposablePartCatalog, ICompositionElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified code base.</summary>
		/// <param name="codeBase">A string that specifies the code base of the assembly (that is, the path to the assembly file) that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="codeBase" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or earlier of the common language runtime is currently loaded and <paramref name="codeBase" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have path discovery permission.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="codeBase" /> could not be loaded.  
		/// -or-  
		/// <paramref name="codeBase" /> specified a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="codeBase" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="codeBase" /> is not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codeBase" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x0600043F RID: 1087 RVA: 0x0000C033 File Offset: 0x0000A233
		public AssemblyCatalog(string codeBase)
		{
			Requires.NotNullOrEmpty(codeBase, "codeBase");
			this.InitializeAssemblyCatalog(AssemblyCatalog.LoadAssembly(codeBase));
			this._definitionOrigin = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified code base and reflection context.</summary>
		/// <param name="codeBase">A string that specifies the code base of the assembly (that is, the path to the assembly file) that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <param name="reflectionContext">The context used by the catalog to interpret types.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="codeBase" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="codeBase" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have path discovery permission.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="codeBase" /> could not be loaded.  
		/// -or-  
		/// <paramref name="codeBase" /> specified a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="codebase" /> or <paramref name="reflectionContext" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="codeBase" /> is not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codeBase" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x06000440 RID: 1088 RVA: 0x0000C064 File Offset: 0x0000A264
		public AssemblyCatalog(string codeBase, ReflectionContext reflectionContext)
		{
			Requires.NotNullOrEmpty(codeBase, "codeBase");
			Requires.NotNull<ReflectionContext>(reflectionContext, "reflectionContext");
			this.InitializeAssemblyCatalog(AssemblyCatalog.LoadAssembly(codeBase));
			this._reflectionContext = reflectionContext;
			this._definitionOrigin = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified code base.</summary>
		/// <param name="codeBase">A string that specifies the code base of the assembly (that is, the path to the assembly file) that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the sources of parts.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="codeBase" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="codeBase" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have path discovery permission.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="codeBase" /> could not be loaded.  
		/// -or-  
		/// <paramref name="codeBase" /> specified a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="codebase" /> or <paramref name="definitionOrigin" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="codeBase" /> is not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codeBase" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x06000441 RID: 1089 RVA: 0x0000C0B2 File Offset: 0x0000A2B2
		public AssemblyCatalog(string codeBase, ICompositionElement definitionOrigin)
		{
			Requires.NotNullOrEmpty(codeBase, "codeBase");
			Requires.NotNull<ICompositionElement>(definitionOrigin, "definitionOrigin");
			this.InitializeAssemblyCatalog(AssemblyCatalog.LoadAssembly(codeBase));
			this._definitionOrigin = definitionOrigin;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified code base and reflection context.</summary>
		/// <param name="codeBase">A string that specifies the code base of the assembly (that is, the path to the assembly file) that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <param name="reflectionContext">The context used by the catalog to interpret types.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the sources of parts.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="codeBase" /> is not a valid assembly.  
		/// -or-  
		/// Version 2.0 or later of the common language runtime is currently loaded and <paramref name="codeBase" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have path discovery permission.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="codeBase" /> could not be loaded.  
		/// -or-  
		/// <paramref name="codeBase" /> specified a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="codebase" />, <paramref name="definitionOrigin" /> or <paramref name="reflectionContext" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="codeBase" /> is not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codeBase" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x06000442 RID: 1090 RVA: 0x0000C0F0 File Offset: 0x0000A2F0
		public AssemblyCatalog(string codeBase, ReflectionContext reflectionContext, ICompositionElement definitionOrigin)
		{
			Requires.NotNullOrEmpty(codeBase, "codeBase");
			Requires.NotNull<ReflectionContext>(reflectionContext, "reflectionContext");
			Requires.NotNull<ICompositionElement>(definitionOrigin, "definitionOrigin");
			this.InitializeAssemblyCatalog(AssemblyCatalog.LoadAssembly(codeBase));
			this._reflectionContext = reflectionContext;
			this._definitionOrigin = definitionOrigin;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified assembly and reflection context.</summary>
		/// <param name="assembly">The assembly that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <param name="reflectionContext">The context used by the catalog to interpret types.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" /> or <paramref name="reflectionContext" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="assembly" /> was loaded in the reflection-only context.</exception>
		// Token: 0x06000443 RID: 1091 RVA: 0x0000C149 File Offset: 0x0000A349
		public AssemblyCatalog(Assembly assembly, ReflectionContext reflectionContext)
		{
			Requires.NotNull<Assembly>(assembly, "assembly");
			Requires.NotNull<ReflectionContext>(reflectionContext, "reflectionContext");
			this.InitializeAssemblyCatalog(assembly);
			this._reflectionContext = reflectionContext;
			this._definitionOrigin = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified assembly and reflection context.</summary>
		/// <param name="assembly">The assembly that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <param name="reflectionContext">The context used by the catalog to interpret types.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the sources of parts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" />, <paramref name="definitionOrigin" />, or <paramref name="reflectionContext" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="assembly" /> was loaded in the reflection-only context.</exception>
		// Token: 0x06000444 RID: 1092 RVA: 0x0000C188 File Offset: 0x0000A388
		public AssemblyCatalog(Assembly assembly, ReflectionContext reflectionContext, ICompositionElement definitionOrigin)
		{
			Requires.NotNull<Assembly>(assembly, "assembly");
			Requires.NotNull<ReflectionContext>(reflectionContext, "reflectionContext");
			Requires.NotNull<ICompositionElement>(definitionOrigin, "definitionOrigin");
			this.InitializeAssemblyCatalog(assembly);
			this._reflectionContext = reflectionContext;
			this._definitionOrigin = definitionOrigin;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified assembly.</summary>
		/// <param name="assembly">The assembly that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="assembly" /> was loaded in the reflection-only context.</exception>
		// Token: 0x06000445 RID: 1093 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		public AssemblyCatalog(Assembly assembly)
		{
			Requires.NotNull<Assembly>(assembly, "assembly");
			this.InitializeAssemblyCatalog(assembly);
			this._definitionOrigin = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified assembly.</summary>
		/// <param name="assembly">The assembly that contains the attributed <see cref="T:System.Type" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</param>
		/// <param name="definitionOrigin">The element used by diagnostics to identify the sources of parts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" /> or <paramref name="definitionOrigin" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="assembly" /> was loaded in the reflection-only context.</exception>
		// Token: 0x06000446 RID: 1094 RVA: 0x0000C208 File Offset: 0x0000A408
		public AssemblyCatalog(Assembly assembly, ICompositionElement definitionOrigin)
		{
			Requires.NotNull<Assembly>(assembly, "assembly");
			Requires.NotNull<ICompositionElement>(definitionOrigin, "definitionOrigin");
			this.InitializeAssemblyCatalog(assembly);
			this._definitionOrigin = definitionOrigin;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000C23F File Offset: 0x0000A43F
		private void InitializeAssemblyCatalog(Assembly assembly)
		{
			this._assembly = assembly;
		}

		/// <summary>Gets a collection of exports that match the conditions specified by the import definition.</summary>
		/// <param name="definition">Conditions that specify which exports to match.</param>
		/// <returns>A collection of exports that match the conditions specified by <paramref name="definition" />.</returns>
		// Token: 0x06000448 RID: 1096 RVA: 0x0000C24A File Offset: 0x0000A44A
		public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			return this.InnerCatalog.GetExports(definition);
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000C258 File Offset: 0x0000A458
		private ComposablePartCatalog InnerCatalog
		{
			get
			{
				this.ThrowIfDisposed();
				if (this._innerCatalog == null)
				{
					CatalogReflectionContextAttribute firstAttribute = this._assembly.GetFirstAttribute<CatalogReflectionContextAttribute>();
					Assembly assembly = (firstAttribute != null) ? firstAttribute.CreateReflectionContext().MapAssembly(this._assembly) : this._assembly;
					object thisLock = this._thisLock;
					lock (thisLock)
					{
						if (this._innerCatalog == null)
						{
							TypeCatalog innerCatalog = (this._reflectionContext != null) ? new TypeCatalog(assembly.GetTypes(), this._reflectionContext, this._definitionOrigin) : new TypeCatalog(assembly.GetTypes(), this._definitionOrigin);
							Thread.MemoryBarrier();
							this._innerCatalog = innerCatalog;
						}
					}
				}
				return this._innerCatalog;
			}
		}

		/// <summary>Gets the assembly whose attributed types are contained in the assembly catalog.</summary>
		/// <returns>The assembly whose attributed <see cref="T:System.Type" /> objects are contained in the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" />.</returns>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000C32C File Offset: 0x0000A52C
		public Assembly Assembly
		{
			get
			{
				return this._assembly;
			}
		}

		/// <summary>Gets the display name of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</summary>
		/// <returns>A string that represents the type and assembly of this <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object.</returns>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000C336 File Offset: 0x0000A536
		string ICompositionElement.DisplayName
		{
			get
			{
				return this.GetDisplayName();
			}
		}

		/// <summary>Gets the composition element that this element originated from.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000AD5C File Offset: 0x00008F5C
		ICompositionElement ICompositionElement.Origin
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a string representation of the assembly catalog.</summary>
		/// <returns>A representation of the assembly catalog.</returns>
		// Token: 0x0600044D RID: 1101 RVA: 0x0000C336 File Offset: 0x0000A536
		public override string ToString()
		{
			return this.GetDisplayName();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600044E RID: 1102 RVA: 0x0000C340 File Offset: 0x0000A540
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (Interlocked.CompareExchange(ref this._isDisposed, 1, 0) == 0 && disposing && this._innerCatalog != null)
				{
					this._innerCatalog.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Returns an enumerator that iterates through the catalog.</summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		// Token: 0x0600044F RID: 1103 RVA: 0x0000C390 File Offset: 0x0000A590
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			return this.InnerCatalog.GetEnumerator();
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000C39D File Offset: 0x0000A59D
		private void ThrowIfDisposed()
		{
			if (this._isDisposed == 1)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000C3AF File Offset: 0x0000A5AF
		private string GetDisplayName()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} (Assembly=\"{1}\")", base.GetType().Name, this.Assembly.FullName);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
		private static Assembly LoadAssembly(string codeBase)
		{
			Requires.NotNullOrEmpty(codeBase, "codeBase");
			AssemblyName assemblyName;
			try
			{
				assemblyName = AssemblyName.GetAssemblyName(codeBase);
			}
			catch (ArgumentException)
			{
				assemblyName = new AssemblyName();
				assemblyName.CodeBase = codeBase;
			}
			return Assembly.Load(assemblyName);
		}

		// Token: 0x040001AB RID: 427
		private readonly object _thisLock = new object();

		// Token: 0x040001AC RID: 428
		private readonly ICompositionElement _definitionOrigin;

		// Token: 0x040001AD RID: 429
		private volatile Assembly _assembly;

		// Token: 0x040001AE RID: 430
		private volatile ComposablePartCatalog _innerCatalog;

		// Token: 0x040001AF RID: 431
		private int _isDisposed;

		// Token: 0x040001B0 RID: 432
		private ReflectionContext _reflectionContext;
	}
}
