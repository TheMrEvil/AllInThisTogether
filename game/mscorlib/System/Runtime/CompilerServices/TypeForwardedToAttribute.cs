using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies a destination <see cref="T:System.Type" /> in another assembly.</summary>
	// Token: 0x0200080A RID: 2058
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class TypeForwardedToAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.TypeForwardedToAttribute" /> class specifying a destination <see cref="T:System.Type" />.</summary>
		/// <param name="destination">The destination <see cref="T:System.Type" /> in another assembly.</param>
		// Token: 0x06004628 RID: 17960 RVA: 0x000E5981 File Offset: 0x000E3B81
		public TypeForwardedToAttribute(Type destination)
		{
			this.Destination = destination;
		}

		/// <summary>Gets the destination <see cref="T:System.Type" /> in another assembly.</summary>
		/// <returns>The destination <see cref="T:System.Type" /> in another assembly.</returns>
		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06004629 RID: 17961 RVA: 0x000E5990 File Offset: 0x000E3B90
		public Type Destination
		{
			[CompilerGenerated]
			get
			{
				return this.<Destination>k__BackingField;
			}
		}

		// Token: 0x04002D44 RID: 11588
		[CompilerGenerated]
		private readonly Type <Destination>k__BackingField;
	}
}
