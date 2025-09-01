using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality to commit or rollback changes to an object that is used as a data source.</summary>
	// Token: 0x020003FD RID: 1021
	public interface IEditableObject
	{
		/// <summary>Begins an edit on an object.</summary>
		// Token: 0x0600213B RID: 8507
		void BeginEdit();

		/// <summary>Pushes changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> or <see cref="M:System.ComponentModel.IBindingList.AddNew" /> call into the underlying object.</summary>
		// Token: 0x0600213C RID: 8508
		void EndEdit();

		/// <summary>Discards changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> call.</summary>
		// Token: 0x0600213D RID: 8509
		void CancelEdit();
	}
}
