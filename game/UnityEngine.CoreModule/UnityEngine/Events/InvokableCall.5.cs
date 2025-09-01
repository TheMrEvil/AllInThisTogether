using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.Events
{
	// Token: 0x020002BD RID: 701
	internal class InvokableCall<T1, T2, T3, T4> : BaseInvokableCall
	{
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001D50 RID: 7504 RVA: 0x0002F234 File Offset: 0x0002D434
		// (remove) Token: 0x06001D51 RID: 7505 RVA: 0x0002F26C File Offset: 0x0002D46C
		protected event UnityAction<T1, T2, T3, T4> Delegate
		{
			[CompilerGenerated]
			add
			{
				UnityAction<T1, T2, T3, T4> unityAction = this.Delegate;
				UnityAction<T1, T2, T3, T4> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<T1, T2, T3, T4> value2 = (UnityAction<T1, T2, T3, T4>)System.Delegate.Combine(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<T1, T2, T3, T4>>(ref this.Delegate, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
			[CompilerGenerated]
			remove
			{
				UnityAction<T1, T2, T3, T4> unityAction = this.Delegate;
				UnityAction<T1, T2, T3, T4> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<T1, T2, T3, T4> value2 = (UnityAction<T1, T2, T3, T4>)System.Delegate.Remove(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<T1, T2, T3, T4>>(ref this.Delegate, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0002F2A1 File Offset: 0x0002D4A1
		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3, T4>)System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3, T4>), target, theFunction);
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0002F2C9 File Offset: 0x0002D4C9
		public InvokableCall(UnityAction<T1, T2, T3, T4> action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0002F2DC File Offset: 0x0002D4DC
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 4;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			BaseInvokableCall.ThrowOnInvalidArg<T4>(args[3]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]), (T4)((object)args[3]));
			}
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0002F364 File Offset: 0x0002D564
		public void Invoke(T1 args0, T2 args1, T3 args2, T4 args3)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1, args2, args3);
			}
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0002F394 File Offset: 0x0002D594
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}

		// Token: 0x0400099C RID: 2460
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private UnityAction<T1, T2, T3, T4> Delegate;
	}
}
