using System;

namespace System.Reflection
{
	/// <summary>Specifies flags that describe the attributes of a field.</summary>
	// Token: 0x0200089E RID: 2206
	[Flags]
	public enum FieldAttributes
	{
		/// <summary>Specifies the access level of a given field.</summary>
		// Token: 0x04002E92 RID: 11922
		FieldAccessMask = 7,
		/// <summary>Specifies that the field cannot be referenced.</summary>
		// Token: 0x04002E93 RID: 11923
		PrivateScope = 0,
		/// <summary>Specifies that the field is accessible only by the parent type.</summary>
		// Token: 0x04002E94 RID: 11924
		Private = 1,
		/// <summary>Specifies that the field is accessible only by subtypes in this assembly.</summary>
		// Token: 0x04002E95 RID: 11925
		FamANDAssem = 2,
		/// <summary>Specifies that the field is accessible throughout the assembly.</summary>
		// Token: 0x04002E96 RID: 11926
		Assembly = 3,
		/// <summary>Specifies that the field is accessible only by type and subtypes.</summary>
		// Token: 0x04002E97 RID: 11927
		Family = 4,
		/// <summary>Specifies that the field is accessible by subtypes anywhere, as well as throughout this assembly.</summary>
		// Token: 0x04002E98 RID: 11928
		FamORAssem = 5,
		/// <summary>Specifies that the field is accessible by any member for whom this scope is visible.</summary>
		// Token: 0x04002E99 RID: 11929
		Public = 6,
		/// <summary>Specifies that the field represents the defined type, or else it is per-instance.</summary>
		// Token: 0x04002E9A RID: 11930
		Static = 16,
		/// <summary>Specifies that the field is initialized only, and can be set only in the body of a constructor.</summary>
		// Token: 0x04002E9B RID: 11931
		InitOnly = 32,
		/// <summary>Specifies that the field's value is a compile-time (static or early bound) constant. Any attempt to set it throws a <see cref="T:System.FieldAccessException" />.</summary>
		// Token: 0x04002E9C RID: 11932
		Literal = 64,
		/// <summary>Specifies that the field does not have to be serialized when the type is remoted.</summary>
		// Token: 0x04002E9D RID: 11933
		NotSerialized = 128,
		/// <summary>Specifies a special method, with the name describing how the method is special.</summary>
		// Token: 0x04002E9E RID: 11934
		SpecialName = 512,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002E9F RID: 11935
		PinvokeImpl = 8192,
		/// <summary>Specifies that the common language runtime (metadata internal APIs) should check the name encoding.</summary>
		// Token: 0x04002EA0 RID: 11936
		RTSpecialName = 1024,
		/// <summary>Specifies that the field has marshaling information.</summary>
		// Token: 0x04002EA1 RID: 11937
		HasFieldMarshal = 4096,
		/// <summary>Specifies that the field has a default value.</summary>
		// Token: 0x04002EA2 RID: 11938
		HasDefault = 32768,
		/// <summary>Specifies that the field has a relative virtual address (RVA). The RVA is the location of the method body in the current image, as an address relative to the start of the image file in which it is located.</summary>
		// Token: 0x04002EA3 RID: 11939
		HasFieldRVA = 256,
		/// <summary>Reserved.</summary>
		// Token: 0x04002EA4 RID: 11940
		ReservedMask = 38144
	}
}
