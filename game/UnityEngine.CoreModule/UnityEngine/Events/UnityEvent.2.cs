using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x020002C7 RID: 711
	[Serializable]
	public class UnityEvent<T0> : UnityEventBase
	{
		// Token: 0x06001DAB RID: 7595 RVA: 0x00030300 File Offset: 0x0002E500
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x00030311 File Offset: 0x0002E511
		public void AddListener(UnityAction<T0> call)
		{
			base.AddCall(UnityEvent<T0>.GetDelegate(call));
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x000301EB File Offset: 0x0002E3EB
		public void RemoveListener(UnityAction<T0> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x00030324 File Offset: 0x0002E524
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[]
			{
				typeof(T0)
			});
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00030350 File Offset: 0x0002E550
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0>(target, theFunction);
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0003036C File Offset: 0x0002E56C
		private static BaseInvokableCall GetDelegate(UnityAction<T0> action)
		{
			return new InvokableCall<T0>(action);
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00030384 File Offset: 0x0002E584
		public void Invoke(T0 arg0)
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0> invokableCall = list[i] as InvokableCall<T0>;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke(arg0);
				}
				else
				{
					InvokableCall invokableCall2 = list[i] as InvokableCall;
					bool flag2 = invokableCall2 != null;
					if (flag2)
					{
						invokableCall2.Invoke();
					}
					else
					{
						BaseInvokableCall baseInvokableCall = list[i];
						bool flag3 = this.m_InvokeArray == null;
						if (flag3)
						{
							this.m_InvokeArray = new object[1];
						}
						this.m_InvokeArray[0] = arg0;
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x040009B1 RID: 2481
		private object[] m_InvokeArray = null;
	}
}
