using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides return values for the <see cref="M:System.Runtime.InteropServices.ICustomQueryInterface.GetInterface(System.Guid@,System.IntPtr@)" /> method.</summary>
	// Token: 0x02000717 RID: 1815
	[ComVisible(false)]
	[Serializable]
	public enum CustomQueryInterfaceResult
	{
		/// <summary>The interface pointer that is returned from the <see cref="M:System.Runtime.InteropServices.ICustomQueryInterface.GetInterface(System.Guid@,System.IntPtr@)" /> method can be used as the result of IUnknown::QueryInterface.</summary>
		// Token: 0x04002AFC RID: 11004
		Handled,
		/// <summary>The custom <see langword="QueryInterface" /> was not used. Instead, the default implementation of IUnknown::QueryInterface should be used.</summary>
		// Token: 0x04002AFD RID: 11005
		NotHandled,
		/// <summary>The interface for a specific interface ID is not available. In this case, the returned interface is <see langword="null" />. E_NOINTERFACE is returned to the caller of IUnknown::QueryInterface.</summary>
		// Token: 0x04002AFE RID: 11006
		Failed
	}
}
