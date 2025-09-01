using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Provides data for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.DesignerNamespaceResolve" /> event.</summary>
	// Token: 0x02000797 RID: 1943
	[ComVisible(false)]
	public class DesignerNamespaceResolveEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.DesignerNamespaceResolveEventArgs" /> class.</summary>
		/// <param name="namespaceName">The name of the namespace to resolve.</param>
		// Token: 0x060044D8 RID: 17624 RVA: 0x000E4EE8 File Offset: 0x000E30E8
		public DesignerNamespaceResolveEventArgs(string namespaceName)
		{
			this.NamespaceName = namespaceName;
			this.ResolvedAssemblyFiles = new Collection<string>();
		}

		/// <summary>Gets the name of the namespace to resolve.</summary>
		/// <returns>The name of the namespace to resolve.</returns>
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060044D9 RID: 17625 RVA: 0x000E4F02 File Offset: 0x000E3102
		// (set) Token: 0x060044DA RID: 17626 RVA: 0x000E4F0A File Offset: 0x000E310A
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

		/// <summary>Gets a collection of assembly file paths; when the event handler for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.DesignerNamespaceResolve" /> event is invoked, the collection is empty, and the event handler is responsible for adding the necessary assembly files.</summary>
		/// <returns>A collection of assembly files that define the requested namespace.</returns>
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060044DB RID: 17627 RVA: 0x000E4F13 File Offset: 0x000E3113
		// (set) Token: 0x060044DC RID: 17628 RVA: 0x000E4F1B File Offset: 0x000E311B
		public Collection<string> ResolvedAssemblyFiles
		{
			[CompilerGenerated]
			get
			{
				return this.<ResolvedAssemblyFiles>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ResolvedAssemblyFiles>k__BackingField = value;
			}
		}

		// Token: 0x04002C3C RID: 11324
		[CompilerGenerated]
		private string <NamespaceName>k__BackingField;

		// Token: 0x04002C3D RID: 11325
		[CompilerGenerated]
		private Collection<string> <ResolvedAssemblyFiles>k__BackingField;
	}
}
