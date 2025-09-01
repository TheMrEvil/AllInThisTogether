using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Deprecated. Freezes a string literal when creating native images using the Ngen.exe (Native Image Generator). This class cannot be inherited.</summary>
	// Token: 0x02000806 RID: 2054
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[Serializable]
	public sealed class StringFreezingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.StringFreezingAttribute" /> class.</summary>
		// Token: 0x06004622 RID: 17954 RVA: 0x00002050 File Offset: 0x00000250
		public StringFreezingAttribute()
		{
		}
	}
}
