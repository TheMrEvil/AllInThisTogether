using System;
using System.ComponentModel;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	[ExcludeFromObjectFactory]
	[Obsolete("GUIElement has been removed.", true)]
	[ExcludeFromPreset]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class GUIElement
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00007D8C File Offset: 0x00005F8C
		private static void FeatureRemoved()
		{
			throw new Exception("GUIElement has been removed from Unity.");
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007D9C File Offset: 0x00005F9C
		[Obsolete("GUIElement has been removed.", true)]
		public bool HitTest(Vector3 screenPosition)
		{
			GUIElement.FeatureRemoved();
			return false;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007DB8 File Offset: 0x00005FB8
		[Obsolete("GUIElement has been removed.", true)]
		public bool HitTest(Vector3 screenPosition, [UnityEngine.Internal.DefaultValue("null")] Camera camera)
		{
			GUIElement.FeatureRemoved();
			return false;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007DD4 File Offset: 0x00005FD4
		[Obsolete("GUIElement has been removed.", true)]
		public Rect GetScreenRect([UnityEngine.Internal.DefaultValue("null")] Camera camera)
		{
			GUIElement.FeatureRemoved();
			return new Rect(0f, 0f, 0f, 0f);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007E08 File Offset: 0x00006008
		[Obsolete("GUIElement has been removed.", true)]
		public Rect GetScreenRect()
		{
			GUIElement.FeatureRemoved();
			return new Rect(0f, 0f, 0f, 0f);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000073B2 File Offset: 0x000055B2
		public GUIElement()
		{
		}
	}
}
