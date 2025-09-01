using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EE RID: 494
	public abstract class KeyboardEventBase<T> : EventBase<T>, IKeyboardEvent where T : KeyboardEventBase<T>, new()
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0003FB25 File Offset: 0x0003DD25
		// (set) Token: 0x06000F84 RID: 3972 RVA: 0x0003FB2D File Offset: 0x0003DD2D
		public EventModifiers modifiers
		{
			[CompilerGenerated]
			get
			{
				return this.<modifiers>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<modifiers>k__BackingField = value;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0003FB36 File Offset: 0x0003DD36
		// (set) Token: 0x06000F86 RID: 3974 RVA: 0x0003FB3E File Offset: 0x0003DD3E
		public char character
		{
			[CompilerGenerated]
			get
			{
				return this.<character>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<character>k__BackingField = value;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003FB47 File Offset: 0x0003DD47
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x0003FB4F File Offset: 0x0003DD4F
		public KeyCode keyCode
		{
			[CompilerGenerated]
			get
			{
				return this.<keyCode>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<keyCode>k__BackingField = value;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0003FB58 File Offset: 0x0003DD58
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0003FB78 File Offset: 0x0003DD78
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0003FB98 File Offset: 0x0003DD98
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0003FBB8 File Offset: 0x0003DDB8
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0003FBD8 File Offset: 0x0003DDD8
		public bool actionKey
		{
			get
			{
				bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
				bool result;
				if (flag)
				{
					result = this.commandKey;
				}
				else
				{
					result = this.ctrlKey;
				}
				return result;
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0003FC11 File Offset: 0x0003DE11
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0003FC22 File Offset: 0x0003DE22
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.SkipDisabledElements);
			this.modifiers = EventModifiers.None;
			this.character = '\0';
			this.keyCode = KeyCode.None;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0003FC48 File Offset: 0x0003DE48
		public static T GetPooled(char c, KeyCode keyCode, EventModifiers modifiers)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.modifiers = modifiers;
			pooled.character = c;
			pooled.keyCode = keyCode;
			return pooled;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0003FC88 File Offset: 0x0003DE88
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				pooled.modifiers = systemEvent.modifiers;
				pooled.character = systemEvent.character;
				pooled.keyCode = systemEvent.keyCode;
			}
			return pooled;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0003FCEE File Offset: 0x0003DEEE
		protected KeyboardEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x04000716 RID: 1814
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private EventModifiers <modifiers>k__BackingField;

		// Token: 0x04000717 RID: 1815
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private char <character>k__BackingField;

		// Token: 0x04000718 RID: 1816
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private KeyCode <keyCode>k__BackingField;
	}
}
