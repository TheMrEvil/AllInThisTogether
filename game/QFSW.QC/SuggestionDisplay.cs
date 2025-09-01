using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QFSW.QC
{
	// Token: 0x0200003E RID: 62
	public class SuggestionDisplay : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00007688 File Offset: 0x00005888
		public void OnPointerClick(PointerEventData eventData)
		{
			int num = TMP_TextUtilities.FindIntersectingLink(this._textArea, eventData.position, null);
			if (num >= 0)
			{
				TMP_LinkInfo tmp_LinkInfo = this._textArea.textInfo.linkInfo[num];
				int suggestion;
				if (int.TryParse(tmp_LinkInfo.GetLinkID(), out suggestion))
				{
					this._quantumConsole.SetSuggestion(suggestion);
				}
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000076E4 File Offset: 0x000058E4
		public SuggestionDisplay()
		{
		}

		// Token: 0x0400010D RID: 269
		[SerializeField]
		private QuantumConsole _quantumConsole;

		// Token: 0x0400010E RID: 270
		[SerializeField]
		private TextMeshProUGUI _textArea;
	}
}
