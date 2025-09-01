using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x02000105 RID: 261
	internal class VisualTreeBindingsUpdater : BaseVisualTreeHierarchyTrackerUpdater
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0001DA06 File Offset: 0x0001BC06
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeBindingsUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0001DA0D File Offset: 0x0001BC0D
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x0001DA14 File Offset: 0x0001BC14
		public static bool disableBindingsThrottling
		{
			[CompilerGenerated]
			get
			{
				return VisualTreeBindingsUpdater.<disableBindingsThrottling>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				VisualTreeBindingsUpdater.<disableBindingsThrottling>k__BackingField = value;
			}
		} = false;

		// Token: 0x06000808 RID: 2056 RVA: 0x0001DA1C File Offset: 0x0001BC1C
		private IBinding GetBindingObjectFromElement(VisualElement ve)
		{
			IBindable bindable = ve as IBindable;
			bool flag = bindable != null;
			if (flag)
			{
				bool flag2 = bindable.binding != null;
				if (flag2)
				{
					return bindable.binding;
				}
			}
			return VisualTreeBindingsUpdater.GetAdditionalBinding(ve);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001DA5B File Offset: 0x0001BC5B
		private void StartTracking(VisualElement ve)
		{
			this.m_ElementsToAdd.Add(ve);
			this.m_ElementsToRemove.Remove(ve);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001DA78 File Offset: 0x0001BC78
		private void StopTracking(VisualElement ve)
		{
			this.m_ElementsToRemove.Add(ve);
			this.m_ElementsToAdd.Remove(ve);
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0001DA95 File Offset: 0x0001BC95
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x0001DA9D File Offset: 0x0001BC9D
		public Dictionary<object, object> temporaryObjectCache
		{
			[CompilerGenerated]
			get
			{
				return this.<temporaryObjectCache>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<temporaryObjectCache>k__BackingField = value;
			}
		} = new Dictionary<object, object>();

		// Token: 0x0600080D RID: 2061 RVA: 0x0001DAA8 File Offset: 0x0001BCA8
		public static void SetAdditionalBinding(VisualElement ve, IBinding b)
		{
			IBinding additionalBinding = VisualTreeBindingsUpdater.GetAdditionalBinding(ve);
			if (additionalBinding != null)
			{
				additionalBinding.Release();
			}
			ve.SetProperty(VisualTreeBindingsUpdater.s_AdditionalBindingObjectVEPropertyName, b);
			ve.IncrementVersion(VersionChangeType.Bindings);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001DADE File Offset: 0x0001BCDE
		public static void ClearAdditionalBinding(VisualElement ve)
		{
			VisualTreeBindingsUpdater.SetAdditionalBinding(ve, null);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001DAEC File Offset: 0x0001BCEC
		public static IBinding GetAdditionalBinding(VisualElement ve)
		{
			return ve.GetProperty(VisualTreeBindingsUpdater.s_AdditionalBindingObjectVEPropertyName) as IBinding;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001DB10 File Offset: 0x0001BD10
		public static void AddBindingRequest(VisualElement ve, IBindingRequest req)
		{
			List<IBindingRequest> list = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName) as List<IBindingRequest>;
			bool flag = list == null;
			if (flag)
			{
				list = ObjectListPool<IBindingRequest>.Get();
				ve.SetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName, list);
			}
			list.Add(req);
			ve.IncrementVersion(VersionChangeType.Bindings);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001DB5C File Offset: 0x0001BD5C
		public static void RemoveBindingRequest(VisualElement ve, IBindingRequest req)
		{
			List<IBindingRequest> list = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName) as List<IBindingRequest>;
			bool flag = list != null;
			if (flag)
			{
				req.Release();
				list.Remove(req);
				bool flag2 = list.Count == 0;
				if (flag2)
				{
					ObjectListPool<IBindingRequest>.Release(list);
					ve.SetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName, null);
				}
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001DBB8 File Offset: 0x0001BDB8
		public static void ClearBindingRequests(VisualElement ve)
		{
			List<IBindingRequest> list = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName) as List<IBindingRequest>;
			bool flag = list != null;
			if (flag)
			{
				foreach (IBindingRequest bindingRequest in list)
				{
					bindingRequest.Release();
				}
				ObjectListPool<IBindingRequest>.Release(list);
				ve.SetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName, null);
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001DC3C File Offset: 0x0001BE3C
		private void StartTrackingRecursive(VisualElement ve)
		{
			IBinding bindingObjectFromElement = this.GetBindingObjectFromElement(ve);
			bool flag = bindingObjectFromElement != null;
			if (flag)
			{
				this.StartTracking(ve);
			}
			object property = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName);
			bool flag2 = property != null;
			if (flag2)
			{
				this.m_ElementsToBind.Add(ve);
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement ve2 = ve.hierarchy[i];
				this.StartTrackingRecursive(ve2);
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		private void StopTrackingRecursive(VisualElement ve)
		{
			this.StopTracking(ve);
			object property = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName);
			bool flag = property != null;
			if (flag)
			{
				this.m_ElementsToBind.Remove(ve);
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement ve2 = ve.hierarchy[i];
				this.StopTrackingRecursive(ve2);
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001DD4C File Offset: 0x0001BF4C
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			base.OnVersionChanged(ve, versionChangeType);
			bool flag = (versionChangeType & VersionChangeType.Bindings) == VersionChangeType.Bindings;
			if (flag)
			{
				bool flag2 = this.GetBindingObjectFromElement(ve) != null;
				if (flag2)
				{
					this.StartTracking(ve);
				}
				else
				{
					this.StopTracking(ve);
				}
				object property = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName);
				bool flag3 = property != null;
				if (flag3)
				{
					this.m_ElementsToBind.Add(ve);
				}
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		protected override void OnHierarchyChange(VisualElement ve, HierarchyChangeType type)
		{
			if (type != HierarchyChangeType.Add)
			{
				if (type == HierarchyChangeType.Remove)
				{
					this.StopTrackingRecursive(ve);
				}
			}
			else
			{
				this.StartTrackingRecursive(ve);
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001DDEC File Offset: 0x0001BFEC
		private static long CurrentTime()
		{
			return Panel.TimeSinceStartupMs();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001DE04 File Offset: 0x0001C004
		public static bool ShouldThrottle(long startTime)
		{
			return !VisualTreeBindingsUpdater.disableBindingsThrottling && VisualTreeBindingsUpdater.CurrentTime() - startTime < 100L;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001DE2C File Offset: 0x0001C02C
		public void PerformTrackingOperations()
		{
			foreach (VisualElement visualElement in this.m_ElementsToAdd)
			{
				IBinding bindingObjectFromElement = this.GetBindingObjectFromElement(visualElement);
				bool flag = bindingObjectFromElement != null;
				if (flag)
				{
					this.m_ElementsWithBindings.Add(visualElement);
				}
			}
			this.m_ElementsToAdd.Clear();
			foreach (VisualElement item in this.m_ElementsToRemove)
			{
				this.m_ElementsWithBindings.Remove(item);
			}
			this.m_ElementsToRemove.Clear();
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001DF04 File Offset: 0x0001C104
		public override void Update()
		{
			base.Update();
			bool flag = this.m_ElementsToBind.Count > 0;
			if (flag)
			{
				using (VisualTreeBindingsUpdater.s_ProfilerBindingRequestsMarker.Auto())
				{
					long num = VisualTreeBindingsUpdater.CurrentTime();
					while (this.m_ElementsToBind.Count > 0 && VisualTreeBindingsUpdater.CurrentTime() - num < 100L)
					{
						VisualElement visualElement = this.m_ElementsToBind.FirstOrDefault<VisualElement>();
						bool flag2 = visualElement != null;
						if (!flag2)
						{
							break;
						}
						this.m_ElementsToBind.Remove(visualElement);
						List<IBindingRequest> list = visualElement.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName) as List<IBindingRequest>;
						bool flag3 = list != null;
						if (flag3)
						{
							visualElement.SetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName, null);
							foreach (IBindingRequest bindingRequest in list)
							{
								bindingRequest.Bind(visualElement);
							}
							ObjectListPool<IBindingRequest>.Release(list);
						}
					}
				}
			}
			this.PerformTrackingOperations();
			bool flag4 = this.m_ElementsWithBindings.Count > 0;
			if (flag4)
			{
				long num2 = VisualTreeBindingsUpdater.CurrentTime();
				bool flag5 = this.m_LastUpdateTime + 100L < num2;
				if (flag5)
				{
					this.UpdateBindings();
					this.m_LastUpdateTime = num2;
				}
			}
			bool flag6 = this.m_ElementsToBind.Count == 0;
			if (flag6)
			{
				this.temporaryObjectCache.Clear();
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001E0AC File Offset: 0x0001C2AC
		private void UpdateBindings()
		{
			VisualTreeBindingsUpdater.s_MarkerUpdate.Begin();
			foreach (VisualElement visualElement in this.m_ElementsWithBindings)
			{
				IBinding bindingObjectFromElement = this.GetBindingObjectFromElement(visualElement);
				bool flag = bindingObjectFromElement == null || visualElement.elementPanel != base.panel;
				if (flag)
				{
					if (bindingObjectFromElement != null)
					{
						bindingObjectFromElement.Release();
					}
					this.StopTracking(visualElement);
				}
				else
				{
					this.updatedBindings.Add(bindingObjectFromElement);
				}
			}
			foreach (IBinding binding in this.updatedBindings)
			{
				binding.PreUpdate();
			}
			foreach (IBinding binding2 in this.updatedBindings)
			{
				binding2.Update();
			}
			this.updatedBindings.Clear();
			VisualTreeBindingsUpdater.s_MarkerUpdate.End();
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001E1FC File Offset: 0x0001C3FC
		internal void PollElementsWithBindings(Action<VisualElement, IBinding> callback)
		{
			VisualTreeBindingsUpdater.s_MarkerPoll.Begin();
			this.PerformTrackingOperations();
			bool flag = this.m_ElementsWithBindings.Count > 0;
			if (flag)
			{
				foreach (VisualElement visualElement in this.m_ElementsWithBindings)
				{
					IBinding bindingObjectFromElement = this.GetBindingObjectFromElement(visualElement);
					bool flag2 = bindingObjectFromElement == null || visualElement.elementPanel != base.panel;
					if (flag2)
					{
						if (bindingObjectFromElement != null)
						{
							bindingObjectFromElement.Release();
						}
						this.StopTracking(visualElement);
					}
					else
					{
						callback(visualElement, bindingObjectFromElement);
					}
				}
			}
			VisualTreeBindingsUpdater.s_MarkerPoll.End();
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
		public VisualTreeBindingsUpdater()
		{
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001E328 File Offset: 0x0001C528
		// Note: this type is marked as 'beforefieldinit'.
		static VisualTreeBindingsUpdater()
		{
		}

		// Token: 0x04000353 RID: 851
		private static readonly PropertyName s_BindingRequestObjectVEPropertyName = "__unity-binding-request-object";

		// Token: 0x04000354 RID: 852
		private static readonly PropertyName s_AdditionalBindingObjectVEPropertyName = "__unity-additional-binding-object";

		// Token: 0x04000355 RID: 853
		private static readonly string s_Description = "Update Bindings";

		// Token: 0x04000356 RID: 854
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeBindingsUpdater.s_Description);

		// Token: 0x04000357 RID: 855
		private static readonly ProfilerMarker s_ProfilerBindingRequestsMarker = new ProfilerMarker("Bindings.Requests");

		// Token: 0x04000358 RID: 856
		private static ProfilerMarker s_MarkerUpdate = new ProfilerMarker("Bindings.Update");

		// Token: 0x04000359 RID: 857
		private static ProfilerMarker s_MarkerPoll = new ProfilerMarker("Bindings.PollElementsWithBindings");

		// Token: 0x0400035A RID: 858
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static bool <disableBindingsThrottling>k__BackingField;

		// Token: 0x0400035B RID: 859
		private readonly HashSet<VisualElement> m_ElementsWithBindings = new HashSet<VisualElement>();

		// Token: 0x0400035C RID: 860
		private readonly HashSet<VisualElement> m_ElementsToAdd = new HashSet<VisualElement>();

		// Token: 0x0400035D RID: 861
		private readonly HashSet<VisualElement> m_ElementsToRemove = new HashSet<VisualElement>();

		// Token: 0x0400035E RID: 862
		private const int k_MinUpdateDelayMs = 100;

		// Token: 0x0400035F RID: 863
		private const int k_MaxBindingTimeMs = 100;

		// Token: 0x04000360 RID: 864
		private long m_LastUpdateTime = 0L;

		// Token: 0x04000361 RID: 865
		private HashSet<VisualElement> m_ElementsToBind = new HashSet<VisualElement>();

		// Token: 0x04000362 RID: 866
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<object, object> <temporaryObjectCache>k__BackingField;

		// Token: 0x04000363 RID: 867
		private List<IBinding> updatedBindings = new List<IBinding>();

		// Token: 0x02000106 RID: 262
		private class RequestObjectListPool : ObjectListPool<IBindingRequest>
		{
			// Token: 0x0600081F RID: 2079 RVA: 0x0001E39F File Offset: 0x0001C59F
			public RequestObjectListPool()
			{
			}
		}
	}
}
