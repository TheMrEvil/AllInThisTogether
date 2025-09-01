using System;
using UnityEngine;

// Token: 0x02000375 RID: 885
public class LogNode : EffectNode
{
	// Token: 0x06001D4F RID: 7503 RVA: 0x000B2695 File Offset: 0x000B0895
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Debug",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001D50 RID: 7504 RVA: 0x000B26BC File Offset: 0x000B08BC
	internal override void Apply(EffectProperties properties)
	{
		this.DoLog(properties);
	}

	// Token: 0x06001D51 RID: 7505 RVA: 0x000B26C5 File Offset: 0x000B08C5
	public override void TryCancel(EffectProperties props)
	{
		this.DoLog(props);
		base.TryCancel(props);
	}

	// Token: 0x06001D52 RID: 7506 RVA: 0x000B26D5 File Offset: 0x000B08D5
	internal override void OnCancel(EffectProperties props)
	{
		this.DoLog(props);
		base.OnCancel(props);
	}

	// Token: 0x06001D53 RID: 7507 RVA: 0x000B26E5 File Offset: 0x000B08E5
	private void DoLog(EffectProperties properties)
	{
	}

	// Token: 0x06001D54 RID: 7508 RVA: 0x000B26E8 File Offset: 0x000B08E8
	private string GetInfo(EffectProperties properties)
	{
		string text = this.LogInfo;
		string text2 = text;
		string oldValue = "$Source";
		EntityControl sourceControl = properties.SourceControl;
		text = text2.Replace(oldValue, ((sourceControl != null) ? sourceControl.ToString() : null) ?? "NULL");
		string text3 = text;
		string oldValue2 = "$Affected";
		EntityControl affectedControl = properties.AffectedControl;
		text = text3.Replace(oldValue2, ((affectedControl != null) ? affectedControl.ToString() : null) ?? "NULL");
		string text4 = text;
		string oldValue3 = "$Target";
		EntityControl seekTargetControl = properties.SeekTargetControl;
		text = text4.Replace(oldValue3, ((seekTargetControl != null) ? seekTargetControl.ToString() : null) ?? "NULL");
		string text5 = text;
		string oldValue4 = "$Ally";
		EntityControl allyTargetControl = properties.AllyTargetControl;
		text = text5.Replace(oldValue4, ((allyTargetControl != null) ? allyTargetControl.ToString() : null) ?? "NULL");
		string text6 = text;
		string oldValue5 = "$Root";
		Node rootNode = this.RootNode;
		text = text6.Replace(oldValue5, (rootNode != null) ? rootNode.titleOverride : null);
		text = text.Replace("$UID", properties.InputID);
		if (this.Value != null)
		{
			text = text.Replace("$Value", (this.Value as NumberNode).Evaluate(properties).ToString());
		}
		if (base.EditorTreeRef != null)
		{
			text = "[" + base.EditorTreeRef.name + "]: " + text;
		}
		return text;
	}

	// Token: 0x06001D55 RID: 7509 RVA: 0x000B2826 File Offset: 0x000B0A26
	public bool NeedsLocation()
	{
		return this.logType == LogNode.LogType.Line || this.logType == LogNode.LogType.Sphere;
	}

	// Token: 0x06001D56 RID: 7510 RVA: 0x000B283C File Offset: 0x000B0A3C
	private string tooltip()
	{
		return "" + "$Root -> Root Node Title\n" + "$Source -> Source Control\n" + "$Affected -> Affected Control\n" + "$Target -> Target Control\n" + "$Value -> Value Node Output";
	}

	// Token: 0x06001D57 RID: 7511 RVA: 0x000B2878 File Offset: 0x000B0A78
	public LogNode()
	{
	}

	// Token: 0x04001E00 RID: 7680
	public LogNode.LogType logType;

	// Token: 0x04001E01 RID: 7681
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "Input", PortLocation.Default)]
	public Node Value;

	// Token: 0x04001E02 RID: 7682
	[HideInInspector]
	[SerializeField]
	[ShowPort("NeedsLocation")]
	[OutputPort(typeof(LocationNode), false, "Point", PortLocation.Header)]
	public Node Point;

	// Token: 0x04001E03 RID: 7683
	[TextArea(2, 5)]
	public string LogInfo = "Log Reached";

	// Token: 0x04001E04 RID: 7684
	public float Radius = 1f;

	// Token: 0x04001E05 RID: 7685
	public Location ToPoint;

	// Token: 0x04001E06 RID: 7686
	public Color DrawColor = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04001E07 RID: 7687
	public float Duration = 1.5f;

	// Token: 0x0200067E RID: 1662
	public enum LogType
	{
		// Token: 0x04002BC0 RID: 11200
		Info,
		// Token: 0x04002BC1 RID: 11201
		Error,
		// Token: 0x04002BC2 RID: 11202
		Sphere,
		// Token: 0x04002BC3 RID: 11203
		Line,
		// Token: 0x04002BC4 RID: 11204
		Pause
	}
}
