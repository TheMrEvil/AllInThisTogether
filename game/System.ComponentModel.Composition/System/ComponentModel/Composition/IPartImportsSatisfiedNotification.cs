using System;

namespace System.ComponentModel.Composition
{
	/// <summary>Notifies a part when its imports have been satisfied.</summary>
	// Token: 0x02000043 RID: 67
	public interface IPartImportsSatisfiedNotification
	{
		/// <summary>Called when a part's imports have been satisfied and it is safe to use.</summary>
		// Token: 0x060001E2 RID: 482
		void OnImportsSatisfied();
	}
}
