using System;
using System.ComponentModel;

// Token: 0x02000012 RID: 18
[EditorBrowsable(EditorBrowsableState.Never)]
[Serializable]
public class ES3SerializableSettings : ES3Settings
{
	// Token: 0x06000154 RID: 340 RVA: 0x00005E39 File Offset: 0x00004039
	public ES3SerializableSettings() : base(false)
	{
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00005E42 File Offset: 0x00004042
	public ES3SerializableSettings(bool applyDefaults) : base(applyDefaults)
	{
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00005E4B File Offset: 0x0000404B
	public ES3SerializableSettings(string path) : base(false)
	{
		this.path = path;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00005E5B File Offset: 0x0000405B
	public ES3SerializableSettings(string path, ES3.Location location) : base(false)
	{
		base.location = location;
	}
}
