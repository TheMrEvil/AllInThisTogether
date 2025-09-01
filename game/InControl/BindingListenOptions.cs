using System;

namespace InControl
{
	// Token: 0x0200000B RID: 11
	public class BindingListenOptions
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002496 File Offset: 0x00000696
		public bool CallOnBindingFound(PlayerAction playerAction, BindingSource bindingSource)
		{
			return this.OnBindingFound == null || this.OnBindingFound(playerAction, bindingSource);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024AF File Offset: 0x000006AF
		public void CallOnBindingAdded(PlayerAction playerAction, BindingSource bindingSource)
		{
			if (this.OnBindingAdded != null)
			{
				this.OnBindingAdded(playerAction, bindingSource);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000024C6 File Offset: 0x000006C6
		public void CallOnBindingRejected(PlayerAction playerAction, BindingSource bindingSource, BindingSourceRejectionType bindingSourceRejectionType)
		{
			if (this.OnBindingRejected != null)
			{
				this.OnBindingRejected(playerAction, bindingSource, bindingSourceRejectionType);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024DE File Offset: 0x000006DE
		public void CallOnBindingEnded(PlayerAction playerAction)
		{
			if (this.OnBindingEnded != null)
			{
				this.OnBindingEnded(playerAction);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024F4 File Offset: 0x000006F4
		public BindingListenOptions()
		{
		}

		// Token: 0x04000021 RID: 33
		public bool IncludeControllers = true;

		// Token: 0x04000022 RID: 34
		public bool IncludeUnknownControllers;

		// Token: 0x04000023 RID: 35
		public bool IncludeNonStandardControls = true;

		// Token: 0x04000024 RID: 36
		public bool IncludeMouseButtons;

		// Token: 0x04000025 RID: 37
		public bool IncludeMouseScrollWheel;

		// Token: 0x04000026 RID: 38
		public bool IncludeKeys = true;

		// Token: 0x04000027 RID: 39
		public bool IncludeModifiersAsFirstClassKeys;

		// Token: 0x04000028 RID: 40
		public uint MaxAllowedBindings;

		// Token: 0x04000029 RID: 41
		public uint MaxAllowedBindingsPerType;

		// Token: 0x0400002A RID: 42
		public bool AllowDuplicateBindingsPerSet;

		// Token: 0x0400002B RID: 43
		public bool UnsetDuplicateBindingsOnSet;

		// Token: 0x0400002C RID: 44
		public bool RejectRedundantBindings;

		// Token: 0x0400002D RID: 45
		public BindingSource ReplaceBinding;

		// Token: 0x0400002E RID: 46
		public Func<PlayerAction, BindingSource, bool> OnBindingFound;

		// Token: 0x0400002F RID: 47
		public Action<PlayerAction, BindingSource> OnBindingAdded;

		// Token: 0x04000030 RID: 48
		public Action<PlayerAction, BindingSource, BindingSourceRejectionType> OnBindingRejected;

		// Token: 0x04000031 RID: 49
		public Action<PlayerAction> OnBindingEnded;
	}
}
