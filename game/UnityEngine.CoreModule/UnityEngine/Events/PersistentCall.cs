using System;
using System.Reflection;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x020002C0 RID: 704
	[Serializable]
	internal class PersistentCall : ISerializationCallbackReceiver
	{
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0002F3EC File Offset: 0x0002D5EC
		public Object target
		{
			get
			{
				return this.m_Target;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0002F404 File Offset: 0x0002D604
		public string targetAssemblyTypeName
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.m_TargetAssemblyTypeName) && this.m_Target != null;
				if (flag)
				{
					this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_Target.GetType().AssemblyQualifiedName);
				}
				return this.m_TargetAssemblyTypeName;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0002F45C File Offset: 0x0002D65C
		public string methodName
		{
			get
			{
				return this.m_MethodName;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x0002F474 File Offset: 0x0002D674
		// (set) Token: 0x06001D5E RID: 7518 RVA: 0x0002F48C File Offset: 0x0002D68C
		public PersistentListenerMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x0002F498 File Offset: 0x0002D698
		public ArgumentCache arguments
		{
			get
			{
				return this.m_Arguments;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0002F4B0 File Offset: 0x0002D6B0
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x0002F4C8 File Offset: 0x0002D6C8
		public UnityEventCallState callState
		{
			get
			{
				return this.m_CallState;
			}
			set
			{
				this.m_CallState = value;
			}
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0002F4D4 File Offset: 0x0002D6D4
		public bool IsValid()
		{
			return !string.IsNullOrEmpty(this.targetAssemblyTypeName) && !string.IsNullOrEmpty(this.methodName);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0002F504 File Offset: 0x0002D704
		public BaseInvokableCall GetRuntimeCall(UnityEventBase theEvent)
		{
			bool flag = this.m_CallState == UnityEventCallState.Off || theEvent == null;
			BaseInvokableCall result;
			if (flag)
			{
				result = null;
			}
			else
			{
				MethodInfo methodInfo = theEvent.FindMethod(this);
				bool flag2 = methodInfo == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = !methodInfo.IsStatic && this.target == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						Object target = methodInfo.IsStatic ? null : this.target;
						switch (this.m_Mode)
						{
						case PersistentListenerMode.EventDefined:
							result = theEvent.GetDelegate(target, methodInfo);
							break;
						case PersistentListenerMode.Void:
							result = new InvokableCall(target, methodInfo);
							break;
						case PersistentListenerMode.Object:
							result = PersistentCall.GetObjectCall(target, methodInfo, this.m_Arguments);
							break;
						case PersistentListenerMode.Int:
							result = new CachedInvokableCall<int>(target, methodInfo, this.m_Arguments.intArgument);
							break;
						case PersistentListenerMode.Float:
							result = new CachedInvokableCall<float>(target, methodInfo, this.m_Arguments.floatArgument);
							break;
						case PersistentListenerMode.String:
							result = new CachedInvokableCall<string>(target, methodInfo, this.m_Arguments.stringArgument);
							break;
						case PersistentListenerMode.Bool:
							result = new CachedInvokableCall<bool>(target, methodInfo, this.m_Arguments.boolArgument);
							break;
						default:
							result = null;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0002F62C File Offset: 0x0002D82C
		private static BaseInvokableCall GetObjectCall(Object target, MethodInfo method, ArgumentCache arguments)
		{
			Type type = typeof(Object);
			bool flag = !string.IsNullOrEmpty(arguments.unityObjectArgumentAssemblyTypeName);
			if (flag)
			{
				type = (Type.GetType(arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object));
			}
			Type typeFromHandle = typeof(CachedInvokableCall<>);
			Type type2 = typeFromHandle.MakeGenericType(new Type[]
			{
				type
			});
			ConstructorInfo constructor = type2.GetConstructor(new Type[]
			{
				typeof(Object),
				typeof(MethodInfo),
				type
			});
			Object @object = arguments.unityObjectArgument;
			bool flag2 = @object != null && !type.IsAssignableFrom(@object.GetType());
			if (flag2)
			{
				@object = null;
			}
			return constructor.Invoke(new object[]
			{
				target,
				method,
				@object
			}) as BaseInvokableCall;
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0002F70D File Offset: 0x0002D90D
		public void RegisterPersistentListener(Object ttarget, Type targetType, string mmethodName)
		{
			this.m_Target = ttarget;
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(targetType.AssemblyQualifiedName);
			this.m_MethodName = mmethodName;
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0002F72F File Offset: 0x0002D92F
		public void UnregisterPersistentListener()
		{
			this.m_MethodName = string.Empty;
			this.m_Target = null;
			this.m_TargetAssemblyTypeName = string.Empty;
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0002F74F File Offset: 0x0002D94F
		public void OnBeforeSerialize()
		{
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_TargetAssemblyTypeName);
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x0002F74F File Offset: 0x0002D94F
		public void OnAfterDeserialize()
		{
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_TargetAssemblyTypeName);
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0002F763 File Offset: 0x0002D963
		public PersistentCall()
		{
		}

		// Token: 0x040009A2 RID: 2466
		[FormerlySerializedAs("instance")]
		[SerializeField]
		private Object m_Target;

		// Token: 0x040009A3 RID: 2467
		[SerializeField]
		private string m_TargetAssemblyTypeName;

		// Token: 0x040009A4 RID: 2468
		[FormerlySerializedAs("methodName")]
		[SerializeField]
		private string m_MethodName;

		// Token: 0x040009A5 RID: 2469
		[SerializeField]
		[FormerlySerializedAs("mode")]
		private PersistentListenerMode m_Mode = PersistentListenerMode.EventDefined;

		// Token: 0x040009A6 RID: 2470
		[SerializeField]
		[FormerlySerializedAs("arguments")]
		private ArgumentCache m_Arguments = new ArgumentCache();

		// Token: 0x040009A7 RID: 2471
		[SerializeField]
		[FormerlySerializedAs("enabled")]
		[FormerlySerializedAs("m_Enabled")]
		private UnityEventCallState m_CallState = UnityEventCallState.RuntimeOnly;
	}
}
