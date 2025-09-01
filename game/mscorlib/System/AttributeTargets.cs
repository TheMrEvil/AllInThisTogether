using System;

namespace System
{
	/// <summary>Specifies the application elements on which it is valid to apply an attribute.</summary>
	// Token: 0x020000FD RID: 253
	[Flags]
	public enum AttributeTargets
	{
		/// <summary>Attribute can be applied to an assembly.</summary>
		// Token: 0x04001054 RID: 4180
		Assembly = 1,
		/// <summary>Attribute can be applied to a module.</summary>
		// Token: 0x04001055 RID: 4181
		Module = 2,
		/// <summary>Attribute can be applied to a class.</summary>
		// Token: 0x04001056 RID: 4182
		Class = 4,
		/// <summary>Attribute can be applied to a structure; that is, a value type.</summary>
		// Token: 0x04001057 RID: 4183
		Struct = 8,
		/// <summary>Attribute can be applied to an enumeration.</summary>
		// Token: 0x04001058 RID: 4184
		Enum = 16,
		/// <summary>Attribute can be applied to a constructor.</summary>
		// Token: 0x04001059 RID: 4185
		Constructor = 32,
		/// <summary>Attribute can be applied to a method.</summary>
		// Token: 0x0400105A RID: 4186
		Method = 64,
		/// <summary>Attribute can be applied to a property.</summary>
		// Token: 0x0400105B RID: 4187
		Property = 128,
		/// <summary>Attribute can be applied to a field.</summary>
		// Token: 0x0400105C RID: 4188
		Field = 256,
		/// <summary>Attribute can be applied to an event.</summary>
		// Token: 0x0400105D RID: 4189
		Event = 512,
		/// <summary>Attribute can be applied to an interface.</summary>
		// Token: 0x0400105E RID: 4190
		Interface = 1024,
		/// <summary>Attribute can be applied to a parameter.</summary>
		// Token: 0x0400105F RID: 4191
		Parameter = 2048,
		/// <summary>Attribute can be applied to a delegate.</summary>
		// Token: 0x04001060 RID: 4192
		Delegate = 4096,
		/// <summary>Attribute can be applied to a return value.</summary>
		// Token: 0x04001061 RID: 4193
		ReturnValue = 8192,
		/// <summary>Attribute can be applied to a generic parameter.</summary>
		// Token: 0x04001062 RID: 4194
		GenericParameter = 16384,
		/// <summary>Attribute can be applied to any application element.</summary>
		// Token: 0x04001063 RID: 4195
		All = 32767
	}
}
