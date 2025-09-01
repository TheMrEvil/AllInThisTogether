using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a way to group a series of design-time actions to improve performance and enable most types of changes to be undone.</summary>
	// Token: 0x02000448 RID: 1096
	public abstract class DesignerTransaction : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> class with no description.</summary>
		// Token: 0x060023AB RID: 9131 RVA: 0x000811B5 File Offset: 0x0007F3B5
		protected DesignerTransaction() : this("")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> class using the specified transaction description.</summary>
		/// <param name="description">A description for this transaction.</param>
		// Token: 0x060023AC RID: 9132 RVA: 0x000811C2 File Offset: 0x0007F3C2
		protected DesignerTransaction(string description)
		{
			this.Description = description;
		}

		/// <summary>Gets a value indicating whether the transaction was canceled.</summary>
		/// <returns>
		///   <see langword="true" /> if the transaction was canceled; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x000811D1 File Offset: 0x0007F3D1
		// (set) Token: 0x060023AE RID: 9134 RVA: 0x000811D9 File Offset: 0x0007F3D9
		public bool Canceled
		{
			[CompilerGenerated]
			get
			{
				return this.<Canceled>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Canceled>k__BackingField = value;
			}
		}

		/// <summary>Gets a value indicating whether the transaction was committed.</summary>
		/// <returns>
		///   <see langword="true" /> if the transaction was committed; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x000811E2 File Offset: 0x0007F3E2
		// (set) Token: 0x060023B0 RID: 9136 RVA: 0x000811EA File Offset: 0x0007F3EA
		public bool Committed
		{
			[CompilerGenerated]
			get
			{
				return this.<Committed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Committed>k__BackingField = value;
			}
		}

		/// <summary>Gets a description for the transaction.</summary>
		/// <returns>A description for the transaction.</returns>
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x000811F3 File Offset: 0x0007F3F3
		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
		}

		/// <summary>Cancels the transaction and attempts to roll back the changes made by the events of the transaction.</summary>
		// Token: 0x060023B2 RID: 9138 RVA: 0x000811FB File Offset: 0x0007F3FB
		public void Cancel()
		{
			if (!this.Canceled && !this.Committed)
			{
				this.Canceled = true;
				GC.SuppressFinalize(this);
				this._suppressedFinalization = true;
				this.OnCancel();
			}
		}

		/// <summary>Commits this transaction.</summary>
		// Token: 0x060023B3 RID: 9139 RVA: 0x00081227 File Offset: 0x0007F427
		public void Commit()
		{
			if (!this.Committed && !this.Canceled)
			{
				this.Committed = true;
				GC.SuppressFinalize(this);
				this._suppressedFinalization = true;
				this.OnCommit();
			}
		}

		/// <summary>Raises the <see langword="Cancel" /> event.</summary>
		// Token: 0x060023B4 RID: 9140
		protected abstract void OnCancel();

		/// <summary>Performs the actual work of committing a transaction.</summary>
		// Token: 0x060023B5 RID: 9141
		protected abstract void OnCommit();

		/// <summary>Releases the resources associated with this object. This override commits this transaction if it was not already committed.</summary>
		// Token: 0x060023B6 RID: 9142 RVA: 0x00081254 File Offset: 0x0007F454
		~DesignerTransaction()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.DesignerTransaction" />.</summary>
		// Token: 0x060023B7 RID: 9143 RVA: 0x00081284 File Offset: 0x0007F484
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			if (!this._suppressedFinalization)
			{
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060023B8 RID: 9144 RVA: 0x0008129B File Offset: 0x0007F49B
		protected virtual void Dispose(bool disposing)
		{
			this.Cancel();
		}

		// Token: 0x040010BA RID: 4282
		private bool _suppressedFinalization;

		// Token: 0x040010BB RID: 4283
		[CompilerGenerated]
		private bool <Canceled>k__BackingField;

		// Token: 0x040010BC RID: 4284
		[CompilerGenerated]
		private bool <Committed>k__BackingField;

		// Token: 0x040010BD RID: 4285
		[CompilerGenerated]
		private readonly string <Description>k__BackingField;
	}
}
