using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace InControl
{
	// Token: 0x0200001B RID: 27
	public class PlayerOneAxisAction : OneAxisInputControl
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000FC RID: 252 RVA: 0x000049CC File Offset: 0x00002BCC
		// (remove) Token: 0x060000FD RID: 253 RVA: 0x00004A04 File Offset: 0x00002C04
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004A39 File Offset: 0x00002C39
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00004A41 File Offset: 0x00002C41
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

		// Token: 0x06000100 RID: 256 RVA: 0x00004A4A File Offset: 0x00002C4A
		internal PlayerOneAxisAction(PlayerAction negativeAction, PlayerAction positiveAction)
		{
			this.negativeAction = negativeAction;
			this.positiveAction = positiveAction;
			this.Raw = true;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004A68 File Offset: 0x00002C68
		internal void Update(ulong updateTick, float deltaTime)
		{
			this.ProcessActionUpdate(this.negativeAction);
			this.ProcessActionUpdate(this.positiveAction);
			float value = Utility.ValueFromSides(this.negativeAction, this.positiveAction);
			base.CommitWithValue(value, updateTick, deltaTime);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004AB4 File Offset: 0x00002CB4
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004B0D File Offset: 0x00002D0D
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00004B14 File Offset: 0x00002D14
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004B16 File Offset: 0x00002D16
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00004B1D File Offset: 0x00002D1D
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

		// Token: 0x04000111 RID: 273
		private PlayerAction negativeAction;

		// Token: 0x04000112 RID: 274
		private PlayerAction positiveAction;

		// Token: 0x04000113 RID: 275
		public BindingSourceType LastInputType;

		// Token: 0x04000114 RID: 276
		[CompilerGenerated]
		private Action<BindingSourceType> OnLastInputTypeChanged;

		// Token: 0x04000115 RID: 277
		[CompilerGenerated]
		private object <UserData>k__BackingField;
	}
}
