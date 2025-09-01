using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Prevents the Ildasm.exe (IL Disassembler) from disassembling an assembly. This class cannot be inherited.</summary>
	// Token: 0x02000807 RID: 2055
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module)]
	public sealed class SuppressIldasmAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.SuppressIldasmAttribute" /> class.</summary>
		// Token: 0x06004623 RID: 17955 RVA: 0x00002050 File Offset: 0x00000250
		public SuppressIldasmAttribute()
		{
		}
	}
}
