using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200020F RID: 527
	[NativeHeader("Runtime/Scripting/DelayedCallUtility.h")]
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[ExtensionOfNativeClass]
	[RequiredByNativeCode]
	public class MonoBehaviour : Behaviour
	{
		// Token: 0x0600172E RID: 5934 RVA: 0x00025584 File Offset: 0x00023784
		public bool IsInvoking()
		{
			return MonoBehaviour.Internal_IsInvokingAll(this);
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0002559C File Offset: 0x0002379C
		public void CancelInvoke()
		{
			MonoBehaviour.Internal_CancelInvokeAll(this);
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x000255A6 File Offset: 0x000237A6
		public void Invoke(string methodName, float time)
		{
			MonoBehaviour.InvokeDelayed(this, methodName, time, 0f);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x000255B8 File Offset: 0x000237B8
		public void InvokeRepeating(string methodName, float time, float repeatRate)
		{
			bool flag = repeatRate <= 1E-05f && repeatRate != 0f;
			if (flag)
			{
				throw new UnityException("Invoke repeat rate has to be larger than 0.00001F)");
			}
			MonoBehaviour.InvokeDelayed(this, methodName, time, repeatRate);
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x000255F5 File Offset: 0x000237F5
		public void CancelInvoke(string methodName)
		{
			MonoBehaviour.CancelInvoke(this, methodName);
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00025600 File Offset: 0x00023800
		public bool IsInvoking(string methodName)
		{
			return MonoBehaviour.IsInvoking(this, methodName);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0002561C File Offset: 0x0002381C
		[ExcludeFromDocs]
		public Coroutine StartCoroutine(string methodName)
		{
			object value = null;
			return this.StartCoroutine(methodName, value);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00025638 File Offset: 0x00023838
		public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
		{
			bool flag = string.IsNullOrEmpty(methodName);
			if (flag)
			{
				throw new NullReferenceException("methodName is null or empty");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			return this.StartCoroutineManaged(methodName, value);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00025680 File Offset: 0x00023880
		public Coroutine StartCoroutine(IEnumerator routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			return this.StartCoroutineManaged2(routine);
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x000256C4 File Offset: 0x000238C4
		[Obsolete("StartCoroutine_Auto has been deprecated. Use StartCoroutine instead (UnityUpgradable) -> StartCoroutine([mscorlib] System.Collections.IEnumerator)", false)]
		public Coroutine StartCoroutine_Auto(IEnumerator routine)
		{
			return this.StartCoroutine(routine);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x000256E0 File Offset: 0x000238E0
		public void StopCoroutine(IEnumerator routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			this.StopCoroutineFromEnumeratorManaged(routine);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00025724 File Offset: 0x00023924
		public void StopCoroutine(Coroutine routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			this.StopCoroutineManaged(routine);
		}

		// Token: 0x0600173A RID: 5946
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopCoroutine(string methodName);

		// Token: 0x0600173B RID: 5947
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopAllCoroutines();

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600173C RID: 5948
		// (set) Token: 0x0600173D RID: 5949
		public extern bool useGUILayout { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600173E RID: 5950 RVA: 0x00025765 File Offset: 0x00023965
		public static void print(object message)
		{
			Debug.Log(message);
		}

		// Token: 0x0600173F RID: 5951
		[FreeFunction("CancelInvoke")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CancelInvokeAll([NotNull("NullExceptionObject")] MonoBehaviour self);

		// Token: 0x06001740 RID: 5952
		[FreeFunction("IsInvoking")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_IsInvokingAll([NotNull("NullExceptionObject")] MonoBehaviour self);

		// Token: 0x06001741 RID: 5953
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InvokeDelayed([NotNull("NullExceptionObject")] MonoBehaviour self, string methodName, float time, float repeatRate);

		// Token: 0x06001742 RID: 5954
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CancelInvoke([NotNull("NullExceptionObject")] MonoBehaviour self, string methodName);

		// Token: 0x06001743 RID: 5955
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsInvoking([NotNull("NullExceptionObject")] MonoBehaviour self, string methodName);

		// Token: 0x06001744 RID: 5956
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsObjectMonoBehaviour([NotNull("NullExceptionObject")] Object obj);

		// Token: 0x06001745 RID: 5957
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Coroutine StartCoroutineManaged(string methodName, object value);

		// Token: 0x06001746 RID: 5958
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Coroutine StartCoroutineManaged2(IEnumerator enumerator);

		// Token: 0x06001747 RID: 5959
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StopCoroutineManaged(Coroutine routine);

		// Token: 0x06001748 RID: 5960
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StopCoroutineFromEnumeratorManaged(IEnumerator routine);

		// Token: 0x06001749 RID: 5961
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string GetScriptClassName();

		// Token: 0x0600174A RID: 5962 RVA: 0x000084C0 File Offset: 0x000066C0
		public MonoBehaviour()
		{
		}
	}
}
