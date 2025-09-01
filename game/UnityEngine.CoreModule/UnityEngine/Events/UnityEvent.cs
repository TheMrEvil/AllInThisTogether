using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x020002C5 RID: 709
	[Serializable]
	public class UnityEvent : UnityEventBase
	{
		// Token: 0x06001DA0 RID: 7584 RVA: 0x000301CA File Offset: 0x0002E3CA
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x000301DB File Offset: 0x0002E3DB
		public void AddListener(UnityAction call)
		{
			base.AddCall(UnityEvent.GetDelegate(call));
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x000301EB File Offset: 0x0002E3EB
		public void RemoveListener(UnityAction call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00030204 File Offset: 0x0002E404
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[0]);
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x00030224 File Offset: 0x0002E424
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall(target, theFunction);
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x00030240 File Offset: 0x0002E440
		private static BaseInvokableCall GetDelegate(UnityAction action)
		{
			return new InvokableCall(action);
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x00030258 File Offset: 0x0002E458
		public void Invoke()
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall invokableCall = list[i] as InvokableCall;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke();
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
							this.m_InvokeArray = new object[0];
						}
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x040009B0 RID: 2480
		private object[] m_InvokeArray = null;
	}
}
