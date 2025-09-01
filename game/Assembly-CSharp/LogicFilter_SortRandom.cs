using System;
using System.Collections.Generic;

// Token: 0x0200026E RID: 622
public class LogicFilter_SortRandom : LogicFilterNode
{
	// Token: 0x060018D1 RID: 6353 RVA: 0x0009B036 File Offset: 0x00099236
	public override void Filter(ref List<EffectProperties> entities, EffectProperties props)
	{
		entities.Shuffle(new Random(props.RandSeed));
	}

	// Token: 0x060018D2 RID: 6354 RVA: 0x0009B04A File Offset: 0x0009924A
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Sort: Random";
		return inspectorProps;
	}

	// Token: 0x060018D3 RID: 6355 RVA: 0x0009B05D File Offset: 0x0009925D
	public LogicFilter_SortRandom()
	{
	}
}
