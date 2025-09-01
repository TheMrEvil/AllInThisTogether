using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Provides data for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve" /> event.</summary>
	// Token: 0x02000798 RID: 1944
	[ComVisible(false)]
	public class NamespaceResolveEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.NamespaceResolveEventArgs" /> class, specifying the namespace to resolve and the assembly whose dependency is being resolved.</summary>
		/// <param name="namespaceName">The namespace to resolve.</param>
		/// <param name="requestingAssembly">The assembly whose dependency is being resolved.</param>
		// Token: 0x060044DD RID: 17629 RVA: 0x000E4F24 File Offset: 0x000E3124
		public NamespaceResolveEventArgs(string namespaceName, Assembly requestingAssembly)
		{
			this.NamespaceName = namespaceName;
			this.RequestingAssembly = requestingAssembly;
			this.ResolvedAssemblies = new Collection<Assembly>();
		}

		/// <summary>Gets the name of the namespace to resolve.</summary>
		/// <returns>The name of the namespace to resolve.</returns>
		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060044DE RID: 17630 RVA: 0x000E4F45 File Offset: 0x000E3145
		// (set) Token: 0x060044DF RID: 17631 RVA: 0x000E4F4D File Offset: 0x000E314D
		public string NamespaceName
		{
			[CompilerGenerated]
			get
			{
				return this.<NamespaceName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<NamespaceName>k__BackingField = value;
			}
		}

		/// <summary>Gets the name of the assembly whose dependency is being resolved.</summary>
		/// <returns>The name of the assembly whose dependency is being resolved.</returns>
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060044E0 RID: 17632 RVA: 0x000E4F56 File Offset: 0x000E3156
		// (set) Token: 0x060044E1 RID: 17633 RVA: 0x000E4F5E File Offset: 0x000E315E
		public Assembly RequestingAssembly
		{
			[CompilerGenerated]
			get
			{
				return this.<RequestingAssembly>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RequestingAssembly>k__BackingField = value;
			}
		}

		/// <summary>Gets a collection of assemblies; when the event handler for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve" /> event is invoked, the collection is empty, and the event handler is responsible for adding the necessary assemblies.</summary>
		/// <returns>A collection of assemblies that define the requested namespace.</returns>
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060044E2 RID: 17634 RVA: 0x000E4F67 File Offset: 0x000E3167
		// (set) Token: 0x060044E3 RID: 17635 RVA: 0x000E4F6F File Offset: 0x000E316F
		public Collection<Assembly> ResolvedAssemblies
		{
			[CompilerGenerated]
			get
			{
				return this.<ResolvedAssemblies>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ResolvedAssemblies>k__BackingField = value;
			}
		}

		// Token: 0x04002C3E RID: 11326
		[CompilerGenerated]
		private string <NamespaceName>k__BackingField;

		// Token: 0x04002C3F RID: 11327
		[CompilerGenerated]
		private Assembly <RequestingAssembly>k__BackingField;

		// Token: 0x04002C40 RID: 11328
		[CompilerGenerated]
		private Collection<Assembly> <ResolvedAssemblies>k__BackingField;
	}
}
