using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class Colorizer : MonoBehaviour
{
	// Token: 0x06000037 RID: 55 RVA: 0x000049CF File Offset: 0x00002BCF
	private void Start()
	{
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000049D1 File Offset: 0x00002BD1
	private void Update()
	{
		if (this.oldColor != this.TintColor)
		{
			this.ChangeColor(base.gameObject, this.TintColor);
		}
		this.oldColor = this.TintColor;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00004A04 File Offset: 0x00002C04
	private void ChangeColor(GameObject effect, Color color)
	{
		foreach (Renderer renderer in effect.GetComponentsInChildren<Renderer>())
		{
			Material material;
			if (this.UseInstanceWhenNotEditorMode)
			{
				material = renderer.material;
			}
			else
			{
				material = renderer.sharedMaterial;
			}
			if (!(material == null) && material.HasProperty("_TintColor"))
			{
				Color color2 = material.GetColor("_TintColor");
				color.a = color2.a;
				material.SetColor("_TintColor", color);
			}
		}
		Light componentInChildren = effect.GetComponentInChildren<Light>();
		if (componentInChildren != null)
		{
			componentInChildren.color = color;
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00004A9C File Offset: 0x00002C9C
	public Colorizer()
	{
	}

	// Token: 0x04000021 RID: 33
	public Color TintColor;

	// Token: 0x04000022 RID: 34
	public bool UseInstanceWhenNotEditorMode = true;

	// Token: 0x04000023 RID: 35
	private Color oldColor;
}
