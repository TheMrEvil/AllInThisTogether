using System;

namespace UnityEngine.Bindings
{
	// Token: 0x0200000F RID: 15
	internal interface IBindingsIsFreeFunctionProviderAttribute : IBindingsAttribute
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25
		// (set) Token: 0x0600001A RID: 26
		bool IsFreeFunction { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27
		// (set) Token: 0x0600001C RID: 28
		bool HasExplicitThis { get; set; }
	}
}
