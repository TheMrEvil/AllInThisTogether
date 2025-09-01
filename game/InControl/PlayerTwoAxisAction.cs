using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace InControl
{
	// Token: 0x0200001C RID: 28
	public class PlayerTwoAxisAction : TwoAxisInputControl
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004B1F File Offset: 0x00002D1F
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00004B27 File Offset: 0x00002D27
		public bool InvertXAxis
		{
			[CompilerGenerated]
			get
			{
				return this.<InvertXAxis>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InvertXAxis>k__BackingField = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004B30 File Offset: 0x00002D30
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00004B38 File Offset: 0x00002D38
		public bool InvertYAxis
		{
			[CompilerGenerated]
			get
			{
				return this.<InvertYAxis>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InvertYAxis>k__BackingField = value;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600010B RID: 267 RVA: 0x00004B44 File Offset: 0x00002D44
		// (remove) Token: 0x0600010C RID: 268 RVA: 0x00004B7C File Offset: 0x00002D7C
		public event Action<BindingSourceType> OnLastInputTypeChanged
		{
			[CompilerGenerated]
			add
			{
				Action<BindingSourceType> action = this.OnLastInputTypeChanged;
				Action<BindingSourceType> action2;
				do
				{
					action2 = action;
					Action<BindingSourceType> value2 = (Action<BindingSourceType>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<BindingSourceType>>(ref this.OnLastInputTypeChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<BindingSourceType> action = this.OnLastInputTypeChanged;
				Action<BindingSourceType> action2;
				do
				{
					action2 = action;
					Action<BindingSourceType> value2 = (Action<BindingSourceType>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<BindingSourceType>>(ref this.OnLastInputTypeChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00004BB1 File Offset: 0x00002DB1
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00004BB9 File Offset: 0x00002DB9
		public object UserData
		{
			[CompilerGenerated]
			get
			{
				return this.<UserData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UserData>k__BackingField = value;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004BC2 File Offset: 0x00002DC2
		internal PlayerTwoAxisAction(PlayerAction negativeXAction, PlayerAction positiveXAction, PlayerAction negativeYAction, PlayerAction positiveYAction)
		{
			this.negativeXAction = negativeXAction;
			this.positiveXAction = positiveXAction;
			this.negativeYAction = negativeYAction;
			this.positiveYAction = positiveYAction;
			this.InvertXAxis = false;
			this.InvertYAxis = false;
			this.Raw = true;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004BFC File Offset: 0x00002DFC
		internal void Update(ulong updateTick, float deltaTime)
		{
			this.ProcessActionUpdate(this.negativeXAction);
			this.ProcessActionUpdate(this.positiveXAction);
			this.ProcessActionUpdate(this.negativeYAction);
			this.ProcessActionUpdate(this.positiveYAction);
			float x = Utility.ValueFromSides(this.negativeXAction, this.positiveXAction, this.InvertXAxis);
			float y = Utility.ValueFromSides(this.negativeYAction, this.positiveYAction, InputManager.InvertYAxis || this.InvertYAxis);
			base.UpdateWithAxes(x, y, updateTick, deltaTime);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004C94 File Offset: 0x00002E94
		private void ProcessActionUpdate(PlayerAction action)
		{
			BindingSourceType lastInputType = this.LastInputType;
			if (action.UpdateTick > base.UpdateTick)
			{
				base.UpdateTick = action.UpdateTick;
				lastInputType = action.LastInputType;
			}
			if (this.LastInputType != lastInputType)
			{
				this.LastInputType = lastInputType;
				if (this.OnLastInputTypeChanged != null)
				{
					this.OnLastInputTypeChanged(lastInputType);
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004CED File Offset: 0x00002EED
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00004CF4 File Offset: 0x00002EF4
		[Obsolete("Please set this property on device controls directly. It does nothing here.")]
		public new float LowerDeadZone
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004CF6 File Offset: 0x00002EF6
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00004CFD File Offset: 0x00002EFD
		[Obsolete("Please set this property on device controls directly. It does nothing here.")]
		public new float UpperDeadZone
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x04000116 RID: 278
		private PlayerAction negativeXAction;

		// Token: 0x04000117 RID: 279
		private PlayerAction positiveXAction;

		// Token: 0x04000118 RID: 280
		private PlayerAction negativeYAction;

		// Token: 0x04000119 RID: 281
		private PlayerAction positiveYAction;

		// Token: 0x0400011A RID: 282
		[CompilerGenerated]
		private bool <InvertXAxis>k__BackingField;

		// Token: 0x0400011B RID: 283
		[CompilerGenerated]
		private bool <InvertYAxis>k__BackingField;

		// Token: 0x0400011C RID: 284
		public BindingSourceType LastInputType;

		// Token: 0x0400011D RID: 285
		[CompilerGenerated]
		private Action<BindingSourceType> OnLastInputTypeChanged;

		// Token: 0x0400011E RID: 286
		[CompilerGenerated]
		private object <UserData>k__BackingField;
	}
}
