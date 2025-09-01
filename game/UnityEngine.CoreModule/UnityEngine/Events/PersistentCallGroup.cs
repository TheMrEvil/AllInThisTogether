using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x020002C1 RID: 705
	[Serializable]
	internal class PersistentCallGroup
	{
		// Token: 0x06001D6A RID: 7530 RVA: 0x0002F785 File Offset: 0x0002D985
		public PersistentCallGroup()
		{
			this.m_Calls = new List<PersistentCall>();
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0002F79C File Offset: 0x0002D99C
		public int Count
		{
			get
			{
				return this.m_Calls.Count;
			}
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0002F7BC File Offset: 0x0002D9BC
		public PersistentCall GetListener(int index)
		{
			return this.m_Calls[index];
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0002F7DC File Offset: 0x0002D9DC
		public IEnumerable<PersistentCall> GetListeners()
		{
			return this.m_Calls;
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0002F7F4 File Offset: 0x0002D9F4
		public void AddListener()
		{
			this.m_Calls.Add(new PersistentCall());
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0002F808 File Offset: 0x0002DA08
		public void AddListener(PersistentCall call)
		{
			this.m_Calls.Add(call);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0002F818 File Offset: 0x0002DA18
		public void RemoveListener(int index)
		{
			this.m_Calls.RemoveAt(index);
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0002F828 File Offset: 0x0002DA28
		public void Clear()
		{
			this.m_Calls.Clear();
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0002F838 File Offset: 0x0002DA38
		public void RegisterEventPersistentListener(int index, Object targetObj, Type targetObjType, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.EventDefined;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x0002F864 File Offset: 0x0002DA64
		public void RegisterVoidPersistentListener(int index, Object targetObj, Type targetObjType, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Void;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0002F890 File Offset: 0x0002DA90
		public void RegisterObjectPersistentListener(int index, Object targetObj, Type targetObjType, Object argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Object;
			listener.arguments.unityObjectArgument = argument;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0002F8C8 File Offset: 0x0002DAC8
		public void RegisterIntPersistentListener(int index, Object targetObj, Type targetObjType, int argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Int;
			listener.arguments.intArgument = argument;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0002F900 File Offset: 0x0002DB00
		public void RegisterFloatPersistentListener(int index, Object targetObj, Type targetObjType, float argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Float;
			listener.arguments.floatArgument = argument;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0002F938 File Offset: 0x0002DB38
		public void RegisterStringPersistentListener(int index, Object targetObj, Type targetObjType, string argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.String;
			listener.arguments.stringArgument = argument;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0002F970 File Offset: 0x0002DB70
		public void RegisterBoolPersistentListener(int index, Object targetObj, Type targetObjType, bool argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Bool;
			listener.arguments.boolArgument = argument;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0002F9A8 File Offset: 0x0002DBA8
		public void UnregisterPersistentListener(int index)
		{
			PersistentCall listener = this.GetListener(index);
			listener.UnregisterPersistentListener();
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0002F9C8 File Offset: 0x0002DBC8
		public void RemoveListeners(Object target, string methodName)
		{
			List<PersistentCall> list = new List<PersistentCall>();
			for (int i = 0; i < this.m_Calls.Count; i++)
			{
				bool flag = this.m_Calls[i].target == target && this.m_Calls[i].methodName == methodName;
				if (flag)
				{
					list.Add(this.m_Calls[i]);
				}
			}
			this.m_Calls.RemoveAll(new Predicate<PersistentCall>(list.Contains));
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0002FA5C File Offset: 0x0002DC5C
		public void Initialize(InvokableCallList invokableList, UnityEventBase unityEventBase)
		{
			foreach (PersistentCall persistentCall in this.m_Calls)
			{
				bool flag = !persistentCall.IsValid();
				if (!flag)
				{
					BaseInvokableCall runtimeCall = persistentCall.GetRuntimeCall(unityEventBase);
					bool flag2 = runtimeCall != null;
					if (flag2)
					{
						invokableList.AddPersistentInvokableCall(runtimeCall);
					}
				}
			}
		}

		// Token: 0x040009A8 RID: 2472
		[FormerlySerializedAs("m_Listeners")]
		[SerializeField]
		private List<PersistentCall> m_Calls;
	}
}
