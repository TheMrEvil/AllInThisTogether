using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.Events
{
	// Token: 0x020002BC RID: 700
	internal class InvokableCall<T1, T2, T3> : BaseInvokableCall
	{
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001D49 RID: 7497 RVA: 0x0002F0B8 File Offset: 0x0002D2B8
		// (remove) Token: 0x06001D4A RID: 7498 RVA: 0x0002F0F0 File Offset: 0x0002D2F0
		protected event UnityAction<T1, T2, T3> Delegate
		{
			[CompilerGenerated]
			add
			{
				UnityAction<T1, T2, T3> unityAction = this.Delegate;
				UnityAction<T1, T2, T3> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<T1, T2, T3> value2 = (UnityAction<T1, T2, T3>)System.Delegate.Combine(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<T1, T2, T3>>(ref this.Delegate, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
			[CompilerGenerated]
			remove
			{
				UnityAction<T1, T2, T3> unityAction = this.Delegate;
				UnityAction<T1, T2, T3> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<T1, T2, T3> value2 = (UnityAction<T1, T2, T3>)System.Delegate.Remove(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<T1, T2, T3>>(ref this.Delegate, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0002F125 File Offset: 0x0002D325
		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3>)System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3>), target, theFunction);
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x0002F14D File Offset: 0x0002D34D
		public InvokableCall(UnityAction<T1, T2, T3> action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0002F160 File Offset: 0x0002D360
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 3;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]));
			}
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x0002F1D4 File Offset: 0x0002D3D4
		public void Invoke(T1 args0, T2 args1, T3 args2)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1, args2);
			}
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0002F200 File Offset: 0x0002D400
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}

		// Token: 0x0400099B RID: 2459
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private UnityAction<T1, T2, T3> Delegate;
	}
}
