using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines a constant value that a compiler can persist for a field or method parameter.</summary>
	// Token: 0x020007EB RID: 2027
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[Serializable]
	public abstract class CustomConstantAttribute : Attribute
	{
		/// <summary>Gets the constant value stored by this attribute.</summary>
		/// <returns>The constant value stored by this attribute.</returns>
		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060045F1 RID: 17905
		public abstract object Value { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.CustomConstantAttribute" /> class.</summary>
		// Token: 0x060045F2 RID: 17906 RVA: 0x00002050 File Offset: 0x00000250
		protected CustomConstantAttribute()
		{
		}
	}
}
