using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x0200000D RID: 13
	public class MagicFX5_Decal : MonoBehaviour
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000035A7 File Offset: 0x000017A7
		private void Initialize()
		{
			this._rend = base.GetComponent<Renderer>();
			this._initialized = true;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000035BC File Offset: 0x000017BC
		private void OnEnable()
		{
			if (!this._initialized)
			{
				this.Initialize();
			}
			if (this.UsePropertyBlock)
			{
				this._rend.GetPropertyBlock(MagicFX5_CoreUtils.SharedMaterialPropertyBlock);
				this._rend.SetPropertyBlock(MagicFX5_CoreUtils.SharedMaterialPropertyBlock);
			}
			if (this.AutoRaycast && Application.isPlaying)
			{
				base.transform.localPosition = Vector3.zero;
				Vector3 position = base.transform.position;
				position.y += this.RaycastLength;
				RaycastHit raycastHit;
				if (Physics.Raycast(base.transform.position, Vector3.down, out raycastHit, this.RaycastLength * 2f))
				{
					position.y = raycastHit.point.y + this.RaycastOffset;
					base.transform.position = position;
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003686 File Offset: 0x00001886
		private void OnDrawGizmos()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000036A7 File Offset: 0x000018A7
		public MagicFX5_Decal()
		{
		}

		// Token: 0x04000054 RID: 84
		public bool UsePropertyBlock = true;

		// Token: 0x04000055 RID: 85
		public bool AutoRaycast;

		// Token: 0x04000056 RID: 86
		public float RaycastLength = 1f;

		// Token: 0x04000057 RID: 87
		public float RaycastOffset = 0.15f;

		// Token: 0x04000058 RID: 88
		private Renderer _rend;

		// Token: 0x04000059 RID: 89
		private bool _initialized;
	}
}
