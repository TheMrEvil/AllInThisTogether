using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000104 RID: 260
public class AmplitudeEvent
{
	// Token: 0x06000C22 RID: 3106 RVA: 0x0004EB22 File Offset: 0x0004CD22
	public AmplitudeEvent(string event_type)
	{
		if (!this.Init())
		{
			return;
		}
		this.AddCoreProperty("event_type", event_type);
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x0004EB4C File Offset: 0x0004CD4C
	public AmplitudeEvent(string productId, float price, int quantity, AmplitudeEvent.RevenueType revenueType)
	{
		if (!this.Init())
		{
			return;
		}
		this.AddCoreProperty("event_type", "purchase");
		this.AddCoreProperty("productId", productId);
		this.AddCoreProperty("price", (revenueType == AmplitudeEvent.RevenueType.Refund) ? (-price) : price);
		this.AddCoreProperty("quantity", quantity);
		this.AddCoreProperty("revenueType", revenueType.ToString());
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x0004EBD4 File Offset: 0x0004CDD4
	private bool Init()
	{
		if (!Amplitude.WasInitialized)
		{
			Debug.LogError("AMP: Amplitude is not Initialized, make sure to call Amplitude.Initialize() before creating analytics events");
			return false;
		}
		this.AddCoreProperty("device_id", Amplitude.instance.device_id);
		if (Amplitude.instance.user_id.Length > 0)
		{
			this.AddCoreProperty("user_id", Amplitude.instance.user_id);
		}
		if (Amplitude.instance.ip.Length > 0)
		{
			this.AddCoreProperty("ip", Amplitude.instance.ip);
		}
		this.AddCoreProperty("app_version", Application.version + "_" + NetworkManager.instance.versionAdd);
		this.AddCoreProperty("session_id", Amplitude.instance.sessionid);
		this.AddCoreProperty("platform", Amplitude.instance.platform);
		this.AddCoreProperty("time", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
		return true;
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x0004ECC8 File Offset: 0x0004CEC8
	public void AddCoreProperty(string key, object value)
	{
		this.values.Add(key, value);
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x0004ECD8 File Offset: 0x0004CED8
	public void AddEventProperty(string key, object value)
	{
		if (!this.values.ContainsKey("event_properties"))
		{
			this.values.Add("event_properties", new Dictionary<string, object>());
		}
		(this.values["event_properties"] as Dictionary<string, object>).Add(key, value);
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x0004ED28 File Offset: 0x0004CF28
	public void AddUserProperty(string key, object value)
	{
		if (!this.values.ContainsKey("user_properties"))
		{
			this.values.Add("user_properties", new Dictionary<string, object>());
		}
		(this.values["user_properties"] as Dictionary<string, object>).Add(key, value);
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x0004ED78 File Offset: 0x0004CF78
	public string ToJSON()
	{
		return this.ToJSON(this.values);
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0004ED88 File Offset: 0x0004CF88
	private string ToJSON(Dictionary<string, object> pairs)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("{");
		int num = 0;
		foreach (KeyValuePair<string, object> keyValuePair in pairs)
		{
			num++;
			stringBuilder.Append("\"" + keyValuePair.Key + "\":");
			stringBuilder.Append(this.ParseElement(keyValuePair.Value));
			if (num < pairs.Count)
			{
				stringBuilder.Append(",");
			}
		}
		stringBuilder.Append("}");
		return stringBuilder.ToString();
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x0004EE40 File Offset: 0x0004D040
	private string ParseElement(object o)
	{
		string text = "";
		if (o == null)
		{
			return "\"None\"";
		}
		if (o is IList)
		{
			text += "[";
			foreach (object o2 in (o as IList))
			{
				text = text + this.ParseElement(o2) + ",";
			}
			if (text.Length > 1)
			{
				text = text.Substring(0, text.Length - 1);
			}
			text += "]";
		}
		else if (o is Dictionary<string, object>)
		{
			text = this.ToJSON(o as Dictionary<string, object>);
		}
		else if (o is float || o is long || o is int)
		{
			text = o.ToString();
		}
		else
		{
			text = "\"" + o.ToString() + "\"";
		}
		return text;
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x0004EF3C File Offset: 0x0004D13C
	private string InsertID()
	{
		return Guid.NewGuid().ToString();
	}

	// Token: 0x040009E2 RID: 2530
	private Dictionary<string, object> values = new Dictionary<string, object>();

	// Token: 0x020004FD RID: 1277
	public enum RevenueType
	{
		// Token: 0x04002559 RID: 9561
		Purchase,
		// Token: 0x0400255A RID: 9562
		Refund
	}
}
