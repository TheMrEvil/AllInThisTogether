using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that this object supports a simple, transacted notification for batch initialization.</summary>
	// Token: 0x02000370 RID: 880
	public interface ISupportInitialize
	{
		/// <summary>Signals the object that initialization is starting.</summary>
		// Token: 0x06001D1A RID: 7450
		void BeginInit();

		/// <summary>Signals the object that initialization is complete.</summary>
		// Token: 0x06001D1B RID: 7451
		void EndInit();
	}
}
