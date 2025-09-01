using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that information was lost about a class or interface when it was imported from a type library to an assembly.</summary>
	// Token: 0x020006F5 RID: 1781
	[AttributeUsage(AttributeTargets.All, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComConversionLossAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="ComConversionLossAttribute" /> class.</summary>
		// Token: 0x06004081 RID: 16513 RVA: 0x00002050 File Offset: 0x00000250
		public ComConversionLossAttribute()
		{
		}
	}
}
