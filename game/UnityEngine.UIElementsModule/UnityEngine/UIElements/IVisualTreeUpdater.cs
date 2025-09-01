using System;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x02000101 RID: 257
	internal interface IVisualTreeUpdater : IDisposable
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060007EC RID: 2028
		// (set) Token: 0x060007ED RID: 2029
		BaseVisualElementPanel panel { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060007EE RID: 2030
		ProfilerMarker profilerMarker { get; }

		// Token: 0x060007EF RID: 2031
		void Update();

		// Token: 0x060007F0 RID: 2032
		void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType);
	}
}
