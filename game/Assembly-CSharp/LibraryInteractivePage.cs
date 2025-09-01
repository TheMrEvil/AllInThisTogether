using System;
using UnityEngine;

// Token: 0x020000D7 RID: 215
public class LibraryInteractivePage : SimpleDiagetic
{
	// Token: 0x060009C8 RID: 2504 RVA: 0x00040FE0 File Offset: 0x0003F1E0
	private void Start()
	{
		this.Activate();
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x00040FE8 File Offset: 0x0003F1E8
	public override void Select()
	{
		LorePanel.instance.Load(this);
		if (this.UseEyeIcon)
		{
			LorePanel.instance.Eyeball();
		}
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x00041007 File Offset: 0x0003F207
	public LibraryInteractivePage()
	{
	}

	// Token: 0x04000823 RID: 2083
	public string Title;

	// Token: 0x04000824 RID: 2084
	[TextArea(3, 4)]
	public string Subheading;

	// Token: 0x04000825 RID: 2085
	[TextArea(5, 8)]
	public string Body;

	// Token: 0x04000826 RID: 2086
	[TextArea]
	public string Signature;

	// Token: 0x04000827 RID: 2087
	public bool UseEyeIcon;
}
