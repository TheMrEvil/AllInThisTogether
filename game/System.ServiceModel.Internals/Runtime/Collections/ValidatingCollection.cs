using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Runtime.Collections
{
	// Token: 0x02000056 RID: 86
	internal class ValidatingCollection<T> : Collection<T>
	{
		// Token: 0x0600033F RID: 831 RVA: 0x00010C4E File Offset: 0x0000EE4E
		public ValidatingCollection()
		{
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00010C56 File Offset: 0x0000EE56
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00010C5E File Offset: 0x0000EE5E
		public Action<T> OnAddValidationCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<OnAddValidationCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OnAddValidationCallback>k__BackingField = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00010C67 File Offset: 0x0000EE67
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00010C6F File Offset: 0x0000EE6F
		public Action OnMutateValidationCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<OnMutateValidationCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OnMutateValidationCallback>k__BackingField = value;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00010C78 File Offset: 0x0000EE78
		private void OnAdd(T item)
		{
			if (this.OnAddValidationCallback != null)
			{
				this.OnAddValidationCallback(item);
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00010C8E File Offset: 0x0000EE8E
		private void OnMutate()
		{
			if (this.OnMutateValidationCallback != null)
			{
				this.OnMutateValidationCallback();
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00010CA3 File Offset: 0x0000EEA3
		protected override void ClearItems()
		{
			this.OnMutate();
			base.ClearItems();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00010CB1 File Offset: 0x0000EEB1
		protected override void InsertItem(int index, T item)
		{
			this.OnAdd(item);
			base.InsertItem(index, item);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00010CC2 File Offset: 0x0000EEC2
		protected override void RemoveItem(int index)
		{
			this.OnMutate();
			base.RemoveItem(index);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00010CD1 File Offset: 0x0000EED1
		protected override void SetItem(int index, T item)
		{
			this.OnAdd(item);
			this.OnMutate();
			base.SetItem(index, item);
		}

		// Token: 0x04000205 RID: 517
		[CompilerGenerated]
		private Action<T> <OnAddValidationCallback>k__BackingField;

		// Token: 0x04000206 RID: 518
		[CompilerGenerated]
		private Action <OnMutateValidationCallback>k__BackingField;
	}
}
