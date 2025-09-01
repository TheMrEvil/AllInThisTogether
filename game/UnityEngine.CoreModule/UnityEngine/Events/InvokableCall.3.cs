using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.Events
{
	// Token: 0x020002BB RID: 699
	internal class InvokableCall<T1, T2> : BaseInvokableCall
	{
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001D42 RID: 7490 RVA: 0x0002EF4C File Offset: 0x0002D14C
		// (remove) Token: 0x06001D43 RID: 7491 RVA: 0x0002EF84 File Offset: 0x0002D184
		protected event UnityAction<T1, T2> Delegate
		{
			[CompilerGenerated]
			add
			{
				UnityAction<T1, T2> unityAction = this.Delegate;
				UnityAction<T1, T2> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<T1, T2> value2 = (UnityAction<T1, T2>)System.Delegate.Combine(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<T1, T2>>(ref this.Delegate, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
			[CompilerGenerated]
			remove
			{
				UnityAction<T1, T2> unityAction = this.Delegate;
				UnityAction<T1, T2> unityAction2;
				do
				{
					unityAction2 = unityAction;
					UnityAction<T1, T2> value2 = (UnityAction<T1, T2>)System.Delegate.Remove(unityAction2, value);
					unityAction = Interlocked.CompareExchange<UnityAction<T1, T2>>(ref this.Delegate, value2, unityAction2);
				}
				while (unityAction != unityAction2);
			}
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0002EFB9 File Offset: 0x0002D1B9
		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2>)System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2>), target, theFunction);
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0002EFE1 File Offset: 0x0002D1E1
		public InvokableCall(UnityAction<T1, T2> action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0002EFF4 File Offset: 0x0002D1F4
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 2;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]));
			}
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0002F058 File Offset: 0x0002D258
		public void Invoke(T1 args0, T2 args1)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1);
			}
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x0002F084 File Offset: 0x0002D284
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}

		// Token: 0x0400099A RID: 2458
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private UnityAction<T1, T2> Delegate;
	}
}
