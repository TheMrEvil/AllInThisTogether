using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the defined native object types.</summary>
	// Token: 0x02000548 RID: 1352
	public enum ResourceType
	{
		/// <summary>An unknown object type.</summary>
		// Token: 0x040024ED RID: 9453
		Unknown,
		/// <summary>A file or directory.</summary>
		// Token: 0x040024EE RID: 9454
		FileObject,
		/// <summary>A Windows service.</summary>
		// Token: 0x040024EF RID: 9455
		Service,
		/// <summary>A printer.</summary>
		// Token: 0x040024F0 RID: 9456
		Printer,
		/// <summary>A registry key.</summary>
		// Token: 0x040024F1 RID: 9457
		RegistryKey,
		/// <summary>A network share.</summary>
		// Token: 0x040024F2 RID: 9458
		LMShare,
		/// <summary>A local kernel object.</summary>
		// Token: 0x040024F3 RID: 9459
		KernelObject,
		/// <summary>A window station or desktop object on the local computer.</summary>
		// Token: 0x040024F4 RID: 9460
		WindowObject,
		/// <summary>A directory service (DS) object or a property set or property of a directory service object.</summary>
		// Token: 0x040024F5 RID: 9461
		DSObject,
		/// <summary>A directory service object and all of its property sets and properties.</summary>
		// Token: 0x040024F6 RID: 9462
		DSObjectAll,
		/// <summary>An object defined by a provider.</summary>
		// Token: 0x040024F7 RID: 9463
		ProviderDefined,
		/// <summary>A Windows Management Instrumentation (WMI) object.</summary>
		// Token: 0x040024F8 RID: 9464
		WmiGuidObject,
		/// <summary>An object for a registry entry under WOW64.</summary>
		// Token: 0x040024F9 RID: 9465
		RegistryWow6432Key
	}
}
