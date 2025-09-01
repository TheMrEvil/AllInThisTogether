using System;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Defines options for export providers.</summary>
	// Token: 0x020000D1 RID: 209
	[Flags]
	public enum CompositionOptions
	{
		/// <summary>No options are defined.</summary>
		// Token: 0x0400024F RID: 591
		Default = 0,
		/// <summary>Silent rejection is disabled, so all rejections will result in errors.</summary>
		// Token: 0x04000250 RID: 592
		DisableSilentRejection = 1,
		/// <summary>This provider should be thread-safe.</summary>
		// Token: 0x04000251 RID: 593
		IsThreadSafe = 2,
		/// <summary>This provider is an export composition service.</summary>
		// Token: 0x04000252 RID: 594
		ExportCompositionService = 4
	}
}
