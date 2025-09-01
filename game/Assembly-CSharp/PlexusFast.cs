using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000256 RID: 598
public class PlexusFast : MonoBehaviour
{
	// Token: 0x06001810 RID: 6160 RVA: 0x00096BC4 File Offset: 0x00094DC4
	private void Awake()
	{
		this.system = base.GetComponent<ParticleSystem>();
		this.main = this.system.main;
		for (int i = 0; i < this.maxLines; i++)
		{
			LineRenderer lineRenderer = UnityEngine.Object.Instantiate<LineRenderer>(this.lineTemplate, base.transform, false);
			this.lines.Add(lineRenderer);
			PlexusFast.Connection connection = new PlexusFast.Connection();
			connection.start = 0;
			connection.end = 0;
			this.debugCons.Add(connection);
			this.connections.Add(lineRenderer, connection);
		}
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x00096C4C File Offset: 0x00094E4C
	private void LateUpdate()
	{
		Gradient colorGradient = this.lineTemplate.colorGradient;
		GradientAlphaKey gradientAlphaKey = new GradientAlphaKey(1f, 0f);
		GradientAlphaKey gradientAlphaKey2 = new GradientAlphaKey(1f, 1f);
		GradientAlphaKey[] array = new GradientAlphaKey[2];
		int maxParticles = this.main.maxParticles;
		if (this.particles == null || this.particles.Length < maxParticles)
		{
			this.particles = new ParticleSystem.Particle[maxParticles];
		}
		this.system.GetParticles(this.particles);
		int particleCount = this.system.particleCount;
		this.ParticleCount = particleCount;
		if (particleCount < 2)
		{
			return;
		}
		foreach (KeyValuePair<LineRenderer, PlexusFast.Connection> keyValuePair in this.connections)
		{
			LineRenderer lineRenderer;
			PlexusFast.Connection connection;
			keyValuePair.Deconstruct(out lineRenderer, out connection);
			LineRenderer lineRenderer2 = lineRenderer;
			PlexusFast.Connection connection2 = connection;
			if (connection2.start >= particleCount || connection2.end >= particleCount || connection2.start == connection2.end)
			{
				this.NewConnection(lineRenderer2, connection2, particleCount);
			}
			ParticleSystem.Particle particle = this.particles[connection2.start];
			ParticleSystem.Particle particle2 = this.particles[connection2.end];
			if (particle.remainingLifetime <= 0.02f || particle2.remainingLifetime <= 0.02f)
			{
				this.NewConnection(lineRenderer2, connection2, particleCount);
			}
			float num = Mathf.Min(particle.remainingLifetime, particle2.remainingLifetime);
			float alpha = Mathf.Lerp(lineRenderer2.colorGradient.alphaKeys[0].alpha, (float)(((double)num > 0.4) ? 1 : 0), Time.deltaTime * 4f);
			float alpha2 = Mathf.Lerp(lineRenderer2.colorGradient.alphaKeys[1].alpha, (float)(((double)num > 0.4) ? 1 : 0), Time.deltaTime * 4f);
			gradientAlphaKey.alpha = alpha;
			gradientAlphaKey2.alpha = alpha2;
			array[0] = gradientAlphaKey;
			array[1] = gradientAlphaKey2;
			colorGradient.SetKeys(colorGradient.colorKeys, array);
			lineRenderer2.colorGradient = colorGradient;
			lineRenderer2.SetPosition(0, particle.position);
			lineRenderer2.SetPosition(1, particle2.position);
			lineRenderer2.enabled = true;
		}
	}

	// Token: 0x06001812 RID: 6162 RVA: 0x00096EBC File Offset: 0x000950BC
	private void NewConnection(LineRenderer line, PlexusFast.Connection connection, int particleCount)
	{
		connection.Randomize(particleCount);
		Gradient colorGradient = this.lineTemplate.colorGradient;
		GradientAlphaKey gradientAlphaKey = new GradientAlphaKey(1f, 0f);
		GradientAlphaKey gradientAlphaKey2 = new GradientAlphaKey(1f, 1f);
		GradientAlphaKey[] array = new GradientAlphaKey[2];
		gradientAlphaKey.alpha = 0f;
		gradientAlphaKey2.alpha = 0f;
		array[0] = gradientAlphaKey;
		array[1] = gradientAlphaKey2;
		colorGradient.SetKeys(colorGradient.colorKeys, array);
		line.colorGradient = colorGradient;
	}

	// Token: 0x06001813 RID: 6163 RVA: 0x00096F41 File Offset: 0x00095141
	public PlexusFast()
	{
	}

	// Token: 0x040017DE RID: 6110
	public int maxLines = 150;

	// Token: 0x040017DF RID: 6111
	private ParticleSystem system;

	// Token: 0x040017E0 RID: 6112
	private ParticleSystem.Particle[] particles;

	// Token: 0x040017E1 RID: 6113
	private ParticleSystem.MainModule main;

	// Token: 0x040017E2 RID: 6114
	public LineRenderer lineTemplate;

	// Token: 0x040017E3 RID: 6115
	public List<LineRenderer> lines = new List<LineRenderer>();

	// Token: 0x040017E4 RID: 6116
	private Dictionary<LineRenderer, PlexusFast.Connection> connections = new Dictionary<LineRenderer, PlexusFast.Connection>();

	// Token: 0x040017E5 RID: 6117
	public List<PlexusFast.Connection> debugCons = new List<PlexusFast.Connection>();

	// Token: 0x040017E6 RID: 6118
	public int ParticleCount;

	// Token: 0x0200061A RID: 1562
	[Serializable]
	public class Connection
	{
		// Token: 0x06002744 RID: 10052 RVA: 0x000D5460 File Offset: 0x000D3660
		public void Randomize(int max)
		{
			this.start = UnityEngine.Random.Range(0, max);
			this.end = UnityEngine.Random.Range(0, max);
			if (this.start == this.end)
			{
				this.end -= ((this.start == 0) ? -1 : 1);
			}
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000D54AE File Offset: 0x000D36AE
		public Connection()
		{
		}

		// Token: 0x040029D1 RID: 10705
		public int start;

		// Token: 0x040029D2 RID: 10706
		public int end;
	}
}
