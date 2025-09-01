using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200003F RID: 63
	[ExecuteAlways]
	public abstract class BaseMeshEffect : UIBehaviour, IMeshModifier
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x000166B9 File Offset: 0x000148B9
		protected Graphic graphic
		{
			get
			{
				if (this.m_Graphic == null)
				{
					this.m_Graphic = base.GetComponent<Graphic>();
				}
				return this.m_Graphic;
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000166DB File Offset: 0x000148DB
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000166FC File Offset: 0x000148FC
		protected override void OnDisable()
		{
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
			base.OnDisable();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001671D File Offset: 0x0001491D
		protected override void OnDidApplyAnimationProperties()
		{
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
			base.OnDidApplyAnimationProperties();
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00016740 File Offset: 0x00014940
		public virtual void ModifyMesh(Mesh mesh)
		{
			using (VertexHelper vertexHelper = new VertexHelper(mesh))
			{
				this.ModifyMesh(vertexHelper);
				vertexHelper.FillMesh(mesh);
			}
		}

		// Token: 0x060004A8 RID: 1192
		public abstract void ModifyMesh(VertexHelper vh);

		// Token: 0x060004A9 RID: 1193 RVA: 0x00016780 File Offset: 0x00014980
		protected BaseMeshEffect()
		{
		}

		// Token: 0x04000190 RID: 400
		[NonSerialized]
		private Graphic m_Graphic;
	}
}
