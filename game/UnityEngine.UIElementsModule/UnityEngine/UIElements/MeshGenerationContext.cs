using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000263 RID: 611
	public class MeshGenerationContext
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x0004A5AC File Offset: 0x000487AC
		public VisualElement visualElement
		{
			get
			{
				return this.painter.visualElement;
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0004A5C9 File Offset: 0x000487C9
		internal MeshGenerationContext(IStylePainter painter)
		{
			this.painter = painter;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0004A5DC File Offset: 0x000487DC
		public MeshWriteData Allocate(int vertexCount, int indexCount, Texture texture = null)
		{
			return this.painter.DrawMesh(vertexCount, indexCount, texture, null, MeshGenerationContext.MeshFlags.None);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0004A600 File Offset: 0x00048800
		internal MeshWriteData Allocate(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags)
		{
			return this.painter.DrawMesh(vertexCount, indexCount, texture, material, flags);
		}

		// Token: 0x040008A1 RID: 2209
		internal IStylePainter painter;

		// Token: 0x02000264 RID: 612
		[Flags]
		internal enum MeshFlags
		{
			// Token: 0x040008A3 RID: 2211
			None = 0,
			// Token: 0x040008A4 RID: 2212
			UVisDisplacement = 1,
			// Token: 0x040008A5 RID: 2213
			SkipDynamicAtlas = 2
		}
	}
}
