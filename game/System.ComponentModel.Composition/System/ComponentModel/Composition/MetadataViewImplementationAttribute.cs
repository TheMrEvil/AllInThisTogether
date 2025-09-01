using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Composition
{
	/// <summary>Specifies the type used to implement a metadata view.</summary>
	// Token: 0x0200004E RID: 78
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class MetadataViewImplementationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.MetadataViewImplementationAttribute" /> class.</summary>
		/// <param name="implementationType">The type of the metadata view.</param>
		// Token: 0x0600021B RID: 539 RVA: 0x000066CC File Offset: 0x000048CC
		public MetadataViewImplementationAttribute(Type implementationType)
		{
			this.ImplementationType = implementationType;
		}

		/// <summary>Gets the type of the metadata view.</summary>
		/// <returns>The type of the metadata view.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600021C RID: 540 RVA: 0x000066DB File Offset: 0x000048DB
		// (set) Token: 0x0600021D RID: 541 RVA: 0x000066E3 File Offset: 0x000048E3
		public Type ImplementationType
		{
			[CompilerGenerated]
			get
			{
				return this.<ImplementationType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ImplementationType>k__BackingField = value;
			}
		}

		// Token: 0x040000E2 RID: 226
		[CompilerGenerated]
		private Type <ImplementationType>k__BackingField;
	}
}
