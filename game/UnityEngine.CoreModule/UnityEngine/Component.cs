using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020001FE RID: 510
	[NativeClass("Unity::Component")]
	[NativeHeader("Runtime/Export/Scripting/Component.bindings.h")]
	[RequiredByNativeCode]
	public class Component : Object
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600167A RID: 5754
		public extern Transform transform { [FreeFunction("GetTransform", HasExplicitThis = true, ThrowsException = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600167B RID: 5755
		public extern GameObject gameObject { [FreeFunction("GetGameObject", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600167C RID: 5756 RVA: 0x00023FFC File Offset: 0x000221FC
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponent(Type type)
		{
			return this.gameObject.GetComponent(type);
		}

		// Token: 0x0600167D RID: 5757
		[FreeFunction(HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x0600167E RID: 5758 RVA: 0x0002401C File Offset: 0x0002221C
		[SecuritySafeCritical]
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.GetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			return castHelper.t;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0002405C File Offset: 0x0002225C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public bool TryGetComponent(Type type, out Component component)
		{
			return this.gameObject.TryGetComponent(type, out component);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0002407C File Offset: 0x0002227C
		[SecuritySafeCritical]
		public bool TryGetComponent<T>(out T component)
		{
			return this.gameObject.TryGetComponent<T>(out component);
		}

		// Token: 0x06001681 RID: 5761
		[FreeFunction(HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Component GetComponent(string type);

		// Token: 0x06001682 RID: 5762 RVA: 0x0002409C File Offset: 0x0002229C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type t, bool includeInactive)
		{
			return this.gameObject.GetComponentInChildren(t, includeInactive);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x000240BC File Offset: 0x000222BC
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type t)
		{
			return this.GetComponentInChildren(t, false);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x000240D8 File Offset: 0x000222D8
		public T GetComponentInChildren<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), includeInactive));
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00024100 File Offset: 0x00022300
		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), false));
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00024128 File Offset: 0x00022328
		public Component[] GetComponentsInChildren(Type t, bool includeInactive)
		{
			return this.gameObject.GetComponentsInChildren(t, includeInactive);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00024148 File Offset: 0x00022348
		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type t)
		{
			return this.gameObject.GetComponentsInChildren(t, false);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00024168 File Offset: 0x00022368
		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return this.gameObject.GetComponentsInChildren<T>(includeInactive);
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00024186 File Offset: 0x00022386
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> result)
		{
			this.gameObject.GetComponentsInChildren<T>(includeInactive, result);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00024198 File Offset: 0x00022398
		public T[] GetComponentsInChildren<T>()
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x000241B1 File Offset: 0x000223B1
		public void GetComponentsInChildren<T>(List<T> results)
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000241C0 File Offset: 0x000223C0
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type t, bool includeInactive)
		{
			return this.gameObject.GetComponentInParent(t, includeInactive);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000241E0 File Offset: 0x000223E0
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type t)
		{
			return this.gameObject.GetComponentInParent(t, false);
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00024200 File Offset: 0x00022400
		public T GetComponentInParent<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInParent(typeof(T), includeInactive));
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00024228 File Offset: 0x00022428
		public T GetComponentInParent<T>()
		{
			return (T)((object)this.GetComponentInParent(typeof(T), false));
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00024250 File Offset: 0x00022450
		public Component[] GetComponentsInParent(Type t, [DefaultValue("false")] bool includeInactive)
		{
			return this.gameObject.GetComponentsInParent(t, includeInactive);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00024270 File Offset: 0x00022470
		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type t)
		{
			return this.GetComponentsInParent(t, false);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0002428C File Offset: 0x0002248C
		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return this.gameObject.GetComponentsInParent<T>(includeInactive);
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x000242AA File Offset: 0x000224AA
		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			this.gameObject.GetComponentsInParent<T>(includeInactive, results);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x000242BC File Offset: 0x000224BC
		public T[] GetComponentsInParent<T>()
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000242D8 File Offset: 0x000224D8
		public Component[] GetComponents(Type type)
		{
			return this.gameObject.GetComponents(type);
		}

		// Token: 0x06001696 RID: 5782
		[FreeFunction(HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetComponentsForListInternal(Type searchType, object resultList);

		// Token: 0x06001697 RID: 5783 RVA: 0x000242F6 File Offset: 0x000224F6
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsForListInternal(type, results);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00024302 File Offset: 0x00022502
		public void GetComponents<T>(List<T> results)
		{
			this.GetComponentsForListInternal(typeof(T), results);
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x00024317 File Offset: 0x00022517
		// (set) Token: 0x0600169A RID: 5786 RVA: 0x00024324 File Offset: 0x00022524
		public string tag
		{
			get
			{
				return this.gameObject.tag;
			}
			set
			{
				this.gameObject.tag = value;
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00024334 File Offset: 0x00022534
		public T[] GetComponents<T>()
		{
			return this.gameObject.GetComponents<T>();
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00024354 File Offset: 0x00022554
		public bool CompareTag(string tag)
		{
			return this.gameObject.CompareTag(tag);
		}

		// Token: 0x0600169D RID: 5789
		[FreeFunction(HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x0600169E RID: 5790 RVA: 0x00024372 File Offset: 0x00022572
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			this.SendMessageUpwards(methodName, value, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0002437F File Offset: 0x0002257F
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			this.SendMessageUpwards(methodName, null, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0002438C File Offset: 0x0002258C
		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00024399 File Offset: 0x00022599
		public void SendMessage(string methodName, object value)
		{
			this.SendMessage(methodName, value, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x000243A6 File Offset: 0x000225A6
		public void SendMessage(string methodName)
		{
			this.SendMessage(methodName, null, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x060016A3 RID: 5795
		[FreeFunction("SendMessage", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessage(string methodName, object value, SendMessageOptions options);

		// Token: 0x060016A4 RID: 5796 RVA: 0x000243B3 File Offset: 0x000225B3
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x060016A5 RID: 5797
		[FreeFunction("BroadcastMessage", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060016A6 RID: 5798 RVA: 0x000243C0 File Offset: 0x000225C0
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			this.BroadcastMessage(methodName, parameter, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x000243CD File Offset: 0x000225CD
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			this.BroadcastMessage(methodName, null, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x000243DA File Offset: 0x000225DA
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x000243E7 File Offset: 0x000225E7
		public Component()
		{
		}
	}
}
