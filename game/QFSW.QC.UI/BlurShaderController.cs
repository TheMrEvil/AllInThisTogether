using System;
using UnityEngine;

namespace QFSW.QC.UI
{
	// Token: 0x02000002 RID: 2
	[ExecuteInEditMode]
	public class BlurShaderController : MonoBehaviour
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private void LateUpdate()
		{
			if (this._blurMaterial)
			{
				float value = new Vector2((float)Screen.width, (float)Screen.height).y / this._referenceResolution.y;
				this._blurMaterial.SetFloat("_Radius", this._blurRadius);
				this._blurMaterial.SetFloat("_BlurMultiplier", value);
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020B4 File Offset: 0x000002B4
		public BlurShaderController()
		{
		}

		// Token: 0x04000001 RID: 1
		[SerializeField]
		private Material _blurMaterial;

		// Token: 0x04000002 RID: 2
		[SerializeField]
		private float _blurRadius = 1f;

		// Token: 0x04000003 RID: 3
		[SerializeField]
		private Vector2 _referenceResolution = new Vector2(1920f, 1080f);
	}
}
