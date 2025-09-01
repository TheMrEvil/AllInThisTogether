using System;
using TMPro;
using UnityEngine;

// Token: 0x02000161 RID: 353
public class LibraryRaceEntry : MonoBehaviour
{
	// Token: 0x06000F7A RID: 3962 RVA: 0x000621F4 File Offset: 0x000603F4
	public void Setup(string username, float time, float authorTime)
	{
		if (time <= 0f)
		{
			this.NoDataDisplay.SetActive(true);
			this.Username.text = "";
			this.TimeBase.text = "";
			return;
		}
		this.Username.text = username;
		this.TimeBase.gameObject.SetActive(time > authorTime);
		this.TimeAuthor.gameObject.SetActive(time <= authorTime);
		string timerText = LibraryRaces.GetTimerText(time);
		this.TimeBase.text = timerText;
		this.TimeAuthor.text = timerText;
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x0006228C File Offset: 0x0006048C
	public LibraryRaceEntry()
	{
	}

	// Token: 0x04000D68 RID: 3432
	public TextMeshProUGUI Username;

	// Token: 0x04000D69 RID: 3433
	public TextMeshProUGUI TimeBase;

	// Token: 0x04000D6A RID: 3434
	public TextMeshProUGUI TimeAuthor;

	// Token: 0x04000D6B RID: 3435
	public GameObject NoDataDisplay;
}
