using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	/// <summary>Provides data for loader resolution events, such as the <see cref="E:System.AppDomain.TypeResolve" />, <see cref="E:System.AppDomain.ResourceResolve" />, <see cref="E:System.AppDomain.ReflectionOnlyAssemblyResolve" />, and <see cref="E:System.AppDomain.AssemblyResolve" /> events.</summary>
	// Token: 0x02000178 RID: 376
	public class ResolveEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ResolveEventArgs" /> class, specifying the name of the item to resolve.</summary>
		/// <param name="name">The name of an item to resolve.</param>
		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003CA6D File Offset: 0x0003AC6D
		public ResolveEventArgs(string name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ResolveEventArgs" /> class, specifying the name of the item to resolve and the assembly whose dependency is being resolved.</summary>
		/// <param name="name">The name of an item to resolve.</param>
		/// <param name="requestingAssembly">The assembly whose dependency is being resolved.</param>
		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003CA7C File Offset: 0x0003AC7C
		public ResolveEventArgs(string name, Assembly requestingAssembly)
		{
			this.Name = name;
			this.RequestingAssembly = requestingAssembly;
		}

		/// <summary>Gets the name of the item to resolve.</summary>
		/// <returns>The name of the item to resolve.</returns>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0003CA92 File Offset: 0x0003AC92
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
		}

		/// <summary>Gets the assembly whose dependency is being resolved.</summary>
		/// <returns>The assembly that requested the item specified by the <see cref="P:System.ResolveEventArgs.Name" /> property.</returns>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0003CA9A File Offset: 0x0003AC9A
		public Assembly RequestingAssembly
		{
			[CompilerGenerated]
			get
			{
				return this.<RequestingAssembly>k__BackingField;
			}
		}

		// Token: 0x040012D6 RID: 4822
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;

		// Token: 0x040012D7 RID: 4823
		[CompilerGenerated]
		private readonly Assembly <RequestingAssembly>k__BackingField;
	}
}
