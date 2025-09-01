using System;

namespace System.Reflection.Emit
{
	/// <summary>Describes the types of the Microsoft intermediate language (MSIL) instructions.</summary>
	// Token: 0x02000907 RID: 2311
	public enum OpCodeType
	{
		/// <summary>This enumerator value is reserved and should not be used.</summary>
		// Token: 0x04003084 RID: 12420
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		Annotation,
		/// <summary>These are Microsoft intermediate language (MSIL) instructions that are used as a synonym for other MSIL instructions. For example, <see langword="ldarg.0" /> represents the <see langword="ldarg" /> instruction with an argument of 0.</summary>
		// Token: 0x04003085 RID: 12421
		Macro,
		/// <summary>Describes a reserved Microsoft intermediate language (MSIL) instruction.</summary>
		// Token: 0x04003086 RID: 12422
		Nternal,
		/// <summary>Describes a Microsoft intermediate language (MSIL) instruction that applies to objects.</summary>
		// Token: 0x04003087 RID: 12423
		Objmodel,
		/// <summary>Describes a prefix instruction that modifies the behavior of the following instruction.</summary>
		// Token: 0x04003088 RID: 12424
		Prefix,
		/// <summary>Describes a built-in instruction.</summary>
		// Token: 0x04003089 RID: 12425
		Primitive
	}
}
