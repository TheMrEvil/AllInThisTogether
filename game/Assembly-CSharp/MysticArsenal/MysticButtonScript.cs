using System;
using UnityEngine;
using UnityEngine.UI;

namespace MysticArsenal
{
	// Token: 0x020003D5 RID: 981
	public class MysticButtonScript : MonoBehaviour
	{
		// Token: 0x06002006 RID: 8198 RVA: 0x000BE474 File Offset: 0x000BC674
		private void Start()
		{
			this.effectScript = GameObject.Find("FireProjectile").GetComponent<MysticFireProjectile>();
			this.getProjectileNames();
			this.MyButtonText = this.Button.transform.Find("Text").GetComponent<Text>();
			this.MyButtonText.text = this.projectileParticleName;
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x000BE4CD File Offset: 0x000BC6CD
		private void Update()
		{
			this.MyButtonText.text = this.projectileParticleName;
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x000BE4E0 File Offset: 0x000BC6E0
		public void getProjectileNames()
		{
			this.projectileScript = this.effectScript.projectiles[this.effectScript.currentProjectile].GetComponent<MysticProjectileScript>();
			this.projectileParticleName = this.projectileScript.projectileParticle.name;
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x000BE51C File Offset: 0x000BC71C
		public bool overButton()
		{
			Rect rect = new Rect(this.buttonsX, this.buttonsY, this.buttonsSizeX, this.buttonsSizeY);
			Rect rect2 = new Rect(this.buttonsX + this.buttonsDistance, this.buttonsY, this.buttonsSizeX, this.buttonsSizeY);
			return rect.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)) || rect2.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y));
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x000BE5C3 File Offset: 0x000BC7C3
		public MysticButtonScript()
		{
		}

		// Token: 0x04002050 RID: 8272
		public GameObject Button;

		// Token: 0x04002051 RID: 8273
		private Text MyButtonText;

		// Token: 0x04002052 RID: 8274
		private string projectileParticleName;

		// Token: 0x04002053 RID: 8275
		private MysticFireProjectile effectScript;

		// Token: 0x04002054 RID: 8276
		private MysticProjectileScript projectileScript;

		// Token: 0x04002055 RID: 8277
		public float buttonsX;

		// Token: 0x04002056 RID: 8278
		public float buttonsY;

		// Token: 0x04002057 RID: 8279
		public float buttonsSizeX;

		// Token: 0x04002058 RID: 8280
		public float buttonsSizeY;

		// Token: 0x04002059 RID: 8281
		public float buttonsDistance;
	}
}
