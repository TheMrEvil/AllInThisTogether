using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x02000041 RID: 65
	public abstract class ImmediateModeElement : VisualElement
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008168 File Offset: 0x00006368
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00008180 File Offset: 0x00006380
		public bool cullingEnabled
		{
			get
			{
				return this.m_CullingEnabled;
			}
			set
			{
				this.m_CullingEnabled = value;
				base.IncrementVersion(VersionChangeType.Repaint);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008198 File Offset: 0x00006398
		public ImmediateModeElement()
		{
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			Type type = base.GetType();
			bool flag = !ImmediateModeElement.s_Markers.TryGetValue(type, out this.m_ImmediateRepaintMarker);
			if (flag)
			{
				this.m_ImmediateRepaintMarker = new ProfilerMarker(base.typeName + ".ImmediateRepaint");
				ImmediateModeElement.s_Markers[type] = this.m_ImmediateRepaintMarker;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008225 File Offset: 0x00006425
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			mgc.painter.DrawImmediate(new Action(this.CallImmediateRepaint), this.cullingEnabled);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008248 File Offset: 0x00006448
		private void CallImmediateRepaint()
		{
			using (this.m_ImmediateRepaintMarker.Auto())
			{
				this.ImmediateRepaint();
			}
		}

		// Token: 0x060001A8 RID: 424
		protected abstract void ImmediateRepaint();

		// Token: 0x060001A9 RID: 425 RVA: 0x00008290 File Offset: 0x00006490
		// Note: this type is marked as 'beforefieldinit'.
		static ImmediateModeElement()
		{
		}

		// Token: 0x040000BB RID: 187
		private static readonly Dictionary<Type, ProfilerMarker> s_Markers = new Dictionary<Type, ProfilerMarker>();

		// Token: 0x040000BC RID: 188
		private readonly ProfilerMarker m_ImmediateRepaintMarker;

		// Token: 0x040000BD RID: 189
		private bool m_CullingEnabled = false;
	}
}
