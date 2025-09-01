using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.Events
{
	// Token: 0x020002BA RID: 698
	internal class InvokableCall<T1> : BaseInvokableCall
	{
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001D3B RID: 7483 RVA: 0x0002EDF0 File Offset: 0x0002CFF0
		// (remove) Token: 0x06001D3C RID: 7484 RVA: 0x0002EE28 File Offset: 0x0002D028
		protected event UnityAction<T1> Delegate
		{
			[CompilerGenerated]
			add
			{
				UnityAction<T1> unityAction = this.Delegate;
				UnityAction<T1> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<T1> value2 = (UnityAction<T1>)System.Delegate.Combine(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<T1>>(ref this.Delegate, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
			[CompilerGenerated]
			remove
			{
				UnityAction<T1> unityAction = this.Delegate;
				UnityAction<T1> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<T1> value2 = (UnityAction<T1>)System.Delegate.Remove(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<T1>>(ref this.Delegate, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x0002EE5D File Offset: 0x0002D05D
		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate += (UnityAction<T1>)System.Delegate.CreateDelegate(typeof(UnityAction<T1>), target, theFunction);
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x0002EE86 File Offset: 0x0002D086
		public InvokableCall(UnityAction<T1> action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x0002EE98 File Offset: 0x0002D098
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 1;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]));
			}
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x0002EEEC File Offset: 0x0002D0EC
		public virtual void Invoke(T1 args0)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0);
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0002EF18 File Offset: 0x0002D118
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}

		// Token: 0x04000999 RID: 2457
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private UnityAction<T1> Delegate;
	}
}
