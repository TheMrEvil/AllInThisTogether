using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class VisualDB : ScriptableObject
{
	// Token: 0x060002DB RID: 731 RVA: 0x00018A03 File Offset: 0x00016C03
	public void SetShaderValues()
	{
		Shader.SetGlobalTexture("_SketchLight", this._SketchLight);
		Shader.SetGlobalTexture("_SketchDark", this._SketchDark);
	}

	// Token: 0x060002DC RID: 732 RVA: 0x00018A25 File Offset: 0x00016C25
	public VisualDB()
	{
	}

	// Token: 0x040002C0 RID: 704
	public Texture2D _SketchLight;

	// Token: 0x040002C1 RID: 705
	public Texture2D _SketchDark;
}
