using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.Events
{
	// Token: 0x020002B9 RID: 697
	internal class InvokableCall : BaseInvokableCall
	{
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001D34 RID: 7476 RVA: 0x0002ECBC File Offset: 0x0002CEBC
		// (remove) Token: 0x06001D35 RID: 7477 RVA: 0x0002ECF4 File Offset: 0x0002CEF4
		private event UnityAction Delegate
		{
			[CompilerGenerated]
			add
			{
				UnityAction unityAction = this.Delegate;
				UnityAction unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction value2 = (UnityAction)System.Delegate.Combine(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction>(ref this.Delegate, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
			[CompilerGenerated]
			remove
			{
				UnityAction unityAction = this.Delegate;
				UnityAction unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction value2 = (UnityAction)System.Delegate.Remove(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction>(ref this.Delegate, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x0002ED29 File Offset: 0x0002CF29
		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate += (UnityAction)System.Delegate.CreateDelegate(typeof(UnityAction), target, theFunction);
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x0002ED52 File Offset: 0x0002CF52
		public InvokableCall(UnityAction action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0002ED64 File Offset: 0x0002CF64
		public override void Invoke(object[] args)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate();
			}
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0002ED90 File Offset: 0x0002CF90
		public void Invoke()
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate();
			}
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0002EDBC File Offset: 0x0002CFBC
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}

		// Token: 0x04000998 RID: 2456
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private UnityAction Delegate;
	}
}
