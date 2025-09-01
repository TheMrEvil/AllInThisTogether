using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x020002C3 RID: 707
	[UsedByNativeCode]
	[Serializable]
	public abstract class UnityEventBase : ISerializationCallbackReceiver
	{
		// Token: 0x06001D84 RID: 7556 RVA: 0x0002FCE0 File Offset: 0x0002DEE0
		protected UnityEventBase()
		{
			this.m_Calls = new InvokableCallList();
			this.m_PersistentCalls = new PersistentCallGroup();
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0002FD07 File Offset: 0x0002DF07
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.DirtyPersistentCalls();
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0002FD07 File Offset: 0x0002DF07
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.DirtyPersistentCalls();
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0002FD14 File Offset: 0x0002DF14
		protected MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return this.FindMethod_Impl(name, targetObj.GetType());
		}

		// Token: 0x06001D88 RID: 7560
		protected abstract MethodInfo FindMethod_Impl(string name, Type targetObjType);

		// Token: 0x06001D89 RID: 7561
		internal abstract BaseInvokableCall GetDelegate(object target, MethodInfo theFunction);

		// Token: 0x06001D8A RID: 7562 RVA: 0x0002FD34 File Offset: 0x0002DF34
		internal MethodInfo FindMethod(PersistentCall call)
		{
			Type argumentType = typeof(Object);
			bool flag = !string.IsNullOrEmpty(call.arguments.unityObjectArgumentAssemblyTypeName);
			if (flag)
			{
				argumentType = (Type.GetType(call.arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object));
			}
			Type listenerType = (call.target != null) ? call.target.GetType() : Type.GetType(call.targetAssemblyTypeName, false);
			return this.FindMethod(call.methodName, listenerType, call.mode, argumentType);
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0002FDC4 File Offset: 0x0002DFC4
		internal MethodInfo FindMethod(string name, Type listenerType, PersistentListenerMode mode, Type argumentType)
		{
			MethodInfo result;
			switch (mode)
			{
			case PersistentListenerMode.EventDefined:
				result = this.FindMethod_Impl(name, listenerType);
				break;
			case PersistentListenerMode.Void:
				result = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[0]);
				break;
			case PersistentListenerMode.Object:
				result = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[]
				{
					argumentType ?? typeof(Object)
				});
				break;
			case PersistentListenerMode.Int:
				result = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[]
				{
					typeof(int)
				});
				break;
			case PersistentListenerMode.Float:
				result = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[]
				{
					typeof(float)
				});
				break;
			case PersistentListenerMode.String:
				result = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[]
				{
					typeof(string)
				});
				break;
			case PersistentListenerMode.Bool:
				result = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[]
				{
					typeof(bool)
				});
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0002FEBC File Offset: 0x0002E0BC
		public int GetPersistentEventCount()
		{
			return this.m_PersistentCalls.Count;
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0002FEDC File Offset: 0x0002E0DC
		public Object GetPersistentTarget(int index)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			return (listener != null) ? listener.target : null;
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0002FF08 File Offset: 0x0002E108
		public string GetPersistentMethodName(int index)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			return (listener != null) ? listener.methodName : string.Empty;
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0002FF37 File Offset: 0x0002E137
		private void DirtyPersistentCalls()
		{
			this.m_Calls.ClearPersistent();
			this.m_CallsDirty = true;
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0002FF50 File Offset: 0x0002E150
		private void RebuildPersistentCallsIfNeeded()
		{
			bool callsDirty = this.m_CallsDirty;
			if (callsDirty)
			{
				this.m_PersistentCalls.Initialize(this.m_Calls, this);
				this.m_CallsDirty = false;
			}
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0002FF84 File Offset: 0x0002E184
		public void SetPersistentListenerState(int index, UnityEventCallState state)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			bool flag = listener != null;
			if (flag)
			{
				listener.callState = state;
			}
			this.DirtyPersistentCalls();
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0002FFB8 File Offset: 0x0002E1B8
		public UnityEventCallState GetPersistentListenerState(int index)
		{
			bool flag = index < 0 || index > this.m_PersistentCalls.Count;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range of the {1} persistent listeners.", index, this.GetPersistentEventCount()));
			}
			return this.m_PersistentCalls.GetListener(index).callState;
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x00030015 File Offset: 0x0002E215
		protected void AddListener(object targetObj, MethodInfo method)
		{
			this.m_Calls.AddListener(this.GetDelegate(targetObj, method));
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0003002C File Offset: 0x0002E22C
		internal void AddCall(BaseInvokableCall call)
		{
			this.m_Calls.AddListener(call);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x0003003C File Offset: 0x0002E23C
		protected void RemoveListener(object targetObj, MethodInfo method)
		{
			this.m_Calls.RemoveListener(targetObj, method);
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0003004D File Offset: 0x0002E24D
		public void RemoveAllListeners()
		{
			this.m_Calls.Clear();
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0003005C File Offset: 0x0002E25C
		internal List<BaseInvokableCall> PrepareInvoke()
		{
			this.RebuildPersistentCallsIfNeeded();
			return this.m_Calls.PrepareInvoke();
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00030080 File Offset: 0x0002E280
		protected void Invoke(object[] parameters)
		{
			List<BaseInvokableCall> list = this.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				list[i].Invoke(parameters);
			}
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x000300B8 File Offset: 0x0002E2B8
		public override string ToString()
		{
			return base.ToString() + " " + base.GetType().FullName;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x000300E8 File Offset: 0x0002E2E8
		public static MethodInfo GetValidMethodInfo(object obj, string functionName, Type[] argumentTypes)
		{
			return UnityEventBase.GetValidMethodInfo(obj.GetType(), functionName, argumentTypes);
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00030108 File Offset: 0x0002E308
		public static MethodInfo GetValidMethodInfo(Type objectType, string functionName, Type[] argumentTypes)
		{
			while (objectType != typeof(object) && objectType != null)
			{
				MethodInfo method = objectType.GetMethod(functionName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, argumentTypes, null);
				bool flag = method != null;
				if (flag)
				{
					ParameterInfo[] parameters = method.GetParameters();
					bool flag2 = true;
					int num = 0;
					foreach (ParameterInfo parameterInfo in parameters)
					{
						Type type = argumentTypes[num];
						Type parameterType = parameterInfo.ParameterType;
						flag2 = (type.IsPrimitive == parameterType.IsPrimitive);
						bool flag3 = !flag2;
						if (flag3)
						{
							break;
						}
						num++;
					}
					bool flag4 = flag2;
					if (flag4)
					{
						return method;
					}
				}
				objectType = objectType.BaseType;
			}
			return null;
		}

		// Token: 0x040009AD RID: 2477
		private InvokableCallList m_Calls;

		// Token: 0x040009AE RID: 2478
		[FormerlySerializedAs("m_PersistentListeners")]
		[SerializeField]
		private PersistentCallGroup m_PersistentCalls;

		// Token: 0x040009AF RID: 2479
		private bool m_CallsDirty = true;
	}
}
