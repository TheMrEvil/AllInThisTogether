using System;
using System.Runtime.CompilerServices;

namespace WebSocketSharp.Server
{
	// Token: 0x0200004E RID: 78
	internal class WebSocketServiceHost<TBehavior> : WebSocketServiceHost where TBehavior : WebSocketBehavior, new()
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x0001D98C File Offset: 0x0001BB8C
		internal WebSocketServiceHost(string path, Action<TBehavior> initializer, Logger log) : base(path, log)
		{
			this._creator = WebSocketServiceHost<TBehavior>.createSessionCreator(initializer);
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0001D9A4 File Offset: 0x0001BBA4
		public override Type BehaviorType
		{
			get
			{
				return typeof(TBehavior);
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
		private static Func<TBehavior> createSessionCreator(Action<TBehavior> initializer)
		{
			bool flag = initializer == null;
			Func<TBehavior> result;
			if (flag)
			{
				result = (() => Activator.CreateInstance<TBehavior>());
			}
			else
			{
				result = delegate()
				{
					TBehavior tbehavior = Activator.CreateInstance<TBehavior>();
					initializer(tbehavior);
					return tbehavior;
				};
			}
			return result;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001DA1C File Offset: 0x0001BC1C
		protected override WebSocketBehavior CreateSession()
		{
			return this._creator();
		}

		// Token: 0x0400025E RID: 606
		private Func<TBehavior> _creator;

		// Token: 0x02000079 RID: 121
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x060005EF RID: 1519 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x060005F0 RID: 1520 RVA: 0x0001F7FC File Offset: 0x0001D9FC
			internal TBehavior <createSessionCreator>b__1()
			{
				TBehavior tbehavior = Activator.CreateInstance<TBehavior>();
				this.initializer(tbehavior);
				return tbehavior;
			}

			// Token: 0x04000310 RID: 784
			public Action<TBehavior> initializer;
		}

		// Token: 0x0200007A RID: 122
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005F1 RID: 1521 RVA: 0x0001F822 File Offset: 0x0001DA22
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005F2 RID: 1522 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c()
			{
			}

			// Token: 0x060005F3 RID: 1523 RVA: 0x0001F82E File Offset: 0x0001DA2E
			internal TBehavior <createSessionCreator>b__4_0()
			{
				return Activator.CreateInstance<TBehavior>();
			}

			// Token: 0x04000311 RID: 785
			public static readonly WebSocketServiceHost<TBehavior>.<>c <>9 = new WebSocketServiceHost<TBehavior>.<>c();

			// Token: 0x04000312 RID: 786
			public static Func<TBehavior> <>9__4_0;
		}
	}
}
