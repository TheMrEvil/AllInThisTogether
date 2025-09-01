using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class ActionScaler : MonoBehaviour
{
	// Token: 0x06000092 RID: 146 RVA: 0x00006D2E File Offset: 0x00004F2E
	public static void AddScalar(GameObject g, EffectProperties props, NumberNode node)
	{
		g.GetOrAddComponent<ActionScaler>().Setup(props, node);
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00006D40 File Offset: 0x00004F40
	private void Setup(EffectProperties props, NumberNode node)
	{
		this.baseScale = base.transform.localScale;
		this.eprops = props;
		this.num = node;
		base.transform.localScale = this.baseScale * Mathf.Max(this.num.Evaluate(this.eprops), 0.05f);
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00006D9D File Offset: 0x00004F9D
	private void Update()
	{
		base.transform.localScale = this.baseScale * Mathf.Max(this.num.Evaluate(this.eprops), 0.05f);
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00006DD0 File Offset: 0x00004FD0
	public ActionScaler()
	{
	}

	// Token: 0x0400008B RID: 139
	private EffectProperties eprops;

	// Token: 0x0400008C RID: 140
	private NumberNode num;

	// Token: 0x0400008D RID: 141
	private Vector3 baseScale;
}
