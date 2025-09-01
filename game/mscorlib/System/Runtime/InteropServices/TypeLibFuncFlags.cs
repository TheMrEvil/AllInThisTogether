using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the original settings of the <see langword="FUNCFLAGS" /> in the COM type library from where this method was imported.</summary>
	// Token: 0x020006F7 RID: 1783
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibFuncFlags
	{
		/// <summary>This flag is intended for system-level functions or functions that type browsers should not display.</summary>
		// Token: 0x04002A55 RID: 10837
		FRestricted = 1,
		/// <summary>The function returns an object that is a source of events.</summary>
		// Token: 0x04002A56 RID: 10838
		FSource = 2,
		/// <summary>The function that supports data binding.</summary>
		// Token: 0x04002A57 RID: 10839
		FBindable = 4,
		/// <summary>When set, any call to a method that sets the property results first in a call to <see langword="IPropertyNotifySink::OnRequestEdit" />.</summary>
		// Token: 0x04002A58 RID: 10840
		FRequestEdit = 8,
		/// <summary>The function that is displayed to the user as bindable. <see cref="F:System.Runtime.InteropServices.TypeLibFuncFlags.FBindable" /> must also be set.</summary>
		// Token: 0x04002A59 RID: 10841
		FDisplayBind = 16,
		/// <summary>The function that best represents the object. Only one function in a type information can have this attribute.</summary>
		// Token: 0x04002A5A RID: 10842
		FDefaultBind = 32,
		/// <summary>The function should not be displayed to the user, although it exists and is bindable.</summary>
		// Token: 0x04002A5B RID: 10843
		FHidden = 64,
		/// <summary>The function supports <see langword="GetLastError" />.</summary>
		// Token: 0x04002A5C RID: 10844
		FUsesGetLastError = 128,
		/// <summary>Permits an optimization in which the compiler looks for a member named "xyz" on the type "abc". If such a member is found and is flagged as an accessor function for an element of the default collection, then a call is generated to that member function.</summary>
		// Token: 0x04002A5D RID: 10845
		FDefaultCollelem = 256,
		/// <summary>The type information member is the default member for display in the user interface.</summary>
		// Token: 0x04002A5E RID: 10846
		FUiDefault = 512,
		/// <summary>The property appears in an object browser, but not in a properties browser.</summary>
		// Token: 0x04002A5F RID: 10847
		FNonBrowsable = 1024,
		/// <summary>Tags the interface as having default behaviors.</summary>
		// Token: 0x04002A60 RID: 10848
		FReplaceable = 2048,
		/// <summary>The function is mapped as individual bindable properties.</summary>
		// Token: 0x04002A61 RID: 10849
		FImmediateBind = 4096
	}
}
