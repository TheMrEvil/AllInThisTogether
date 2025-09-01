using System;
using System.Runtime.CompilerServices;

namespace InControl
{
	// Token: 0x02000025 RID: 37
	public class InputControl : OneAxisInputControl
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00005682 File Offset: 0x00003882
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000568A File Offset: 0x0000388A
		public string Handle
		{
			[CompilerGenerated]
			get
			{
				return this.<Handle>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Handle>k__BackingField = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005693 File Offset: 0x00003893
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000569B File Offset: 0x0000389B
		public InputControlType Target
		{
			[CompilerGenerated]
			get
			{
				return this.<Target>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Target>k__BackingField = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000056A4 File Offset: 0x000038A4
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000056AC File Offset: 0x000038AC
		public bool IsButton
		{
			[CompilerGenerated]
			get
			{
				return this.<IsButton>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsButton>k__BackingField = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000056B5 File Offset: 0x000038B5
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000056BD File Offset: 0x000038BD
		public bool IsAnalog
		{
			[CompilerGenerated]
			get
			{
				return this.<IsAnalog>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<IsAnalog>k__BackingField = value;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000056C6 File Offset: 0x000038C6
		private InputControl()
		{
			this.Handle = "None";
			this.Target = InputControlType.None;
			this.Passive = false;
			this.IsButton = false;
			this.IsAnalog = false;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000056F5 File Offset: 0x000038F5
		public InputControl(string handle, InputControlType target)
		{
			this.Handle = handle;
			this.Target = target;
			this.Passive = false;
			this.IsButton = Utility.TargetIsButton(target);
			this.IsAnalog = !this.IsButton;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000572D File Offset: 0x0000392D
		public InputControl(string handle, InputControlType target, bool passive) : this(handle, target)
		{
			this.Passive = passive;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000573E File Offset: 0x0000393E
		internal void SetZeroTick()
		{
			this.zeroTick = base.UpdateTick;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000574C File Offset: 0x0000394C
		internal bool IsOnZeroTick
		{
			get
			{
				return base.UpdateTick == this.zeroTick;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000575C File Offset: 0x0000395C
		public bool IsStandard
		{
			get
			{
				return Utility.TargetIsStandard(this.Target);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005769 File Offset: 0x00003969
		// Note: this type is marked as 'beforefieldinit'.
		static InputControl()
		{
		}

		// Token: 0x0400013E RID: 318
		public static readonly InputControl Null = new InputControl
		{
			isNullControl = true
		};

		// Token: 0x0400013F RID: 319
		[CompilerGenerated]
		private string <Handle>k__BackingField;

		// Token: 0x04000140 RID: 320
		[CompilerGenerated]
		private InputControlType <Target>k__BackingField;

		// Token: 0x04000141 RID: 321
		public bool Passive;

		// Token: 0x04000142 RID: 322
		[CompilerGenerated]
		private bool <IsButton>k__BackingField;

		// Token: 0x04000143 RID: 323
		[CompilerGenerated]
		private bool <IsAnalog>k__BackingField;

		// Token: 0x04000144 RID: 324
		private ulong zeroTick;
	}
}
