using System;
using UnityEngine;

// Token: 0x02000281 RID: 641
public class Logic_Genre : LogicNode
{
	// Token: 0x060018F9 RID: 6393 RVA: 0x0009BE38 File Offset: 0x0009A038
	public override bool Evaluate(EffectProperties props)
	{
		Logic_Genre.GenreTest test = this.Test;
		bool result;
		if (test != Logic_Genre.GenreTest.IsCurrent)
		{
			result = (test == Logic_Genre.GenreTest.IsUnlocked && UnlockManager.IsGenreUnlocked(this.Graph));
		}
		else
		{
			result = (GameplayManager.instance.GameGraph == this.Graph);
		}
		return result;
	}

	// Token: 0x060018FA RID: 6394 RVA: 0x0009BE7E File Offset: 0x0009A07E
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Genre Test";
		inspectorProps.MinInspectorSize = new Vector2(130f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(130f, 0f);
		return inspectorProps;
	}

	// Token: 0x060018FB RID: 6395 RVA: 0x0009BEBB File Offset: 0x0009A0BB
	public Logic_Genre()
	{
	}

	// Token: 0x04001911 RID: 6417
	public GenreTree Graph;

	// Token: 0x04001912 RID: 6418
	public Logic_Genre.GenreTest Test;

	// Token: 0x02000634 RID: 1588
	public enum GenreTest
	{
		// Token: 0x04002A63 RID: 10851
		IsCurrent,
		// Token: 0x04002A64 RID: 10852
		IsUnlocked
	}
}
