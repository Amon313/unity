using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
	// Color of the gizmo
	public Color gizmoColor = Color.red;


	//----------------------------------
	// Enemies and how many have been created and how many are to be created
	//----------------------------------
	public int totalEnemy = 2;
	private int numEnemy = 0;
	private int spawnedEnemy = 0;
	private static float frequency = 0.15f;
	public List<GameObject> Enemies;

	//----------------------------------
	// End of Enemy Settings
	//----------------------------------

	// The ID of the spawner
	private int SpawnID;

	//----------------------------------
	// Different Spawn states and ways of doing them
	//----------------------------------
	private bool waveSpawn = false;
	public bool Spawn = true;

	//Wave controls
	private int totalWaves = 5;
	private int numWaves = 0;
	



	public GameObject SpawnExplosion;
	private ParticleSystem PartSystem;
	private ParticleSystem.EmissionModule module;

	//private ParticleSystem PartSystem;
	private ParticleSystem.MainModule ma;


	//----------------------------------
	// End of Different Spawn states and ways of doing them
	//----------------------------------
	void Start()
	{
		// sets a random number for the id of the spawner
		SpawnID = Random.Range(1, 500);
		/*PartSystem = GetComponent<ParticleSystem>();
		module = PartSystem.emission;*/
		GetComponent<Renderer>().enabled = false;
		//module.enabled = false;
		/*ma = PartSystem.main;*/

	}

	/*// Draws a cube to show where the spawn point is... Useful if you don't have a object that show the spawn point
	void OnDrawGizmos()
	{
		// Sets the color to red
		Gizmos.color = gizmoColor;
		//draws a small cube at the location of the gam object that the script is attached to
		Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
	}*/


	void Update()
	{
		
		//ma.startColor = getColorPrimary();

		
		if (Spawn)
		{
			//Debug.Log(numEnemy);
			if (numWaves < totalWaves + 1)
			{
				if (waveSpawn)
				{
					//spawns an enemy
					//Debug.Log("start spawn enemy");
					StartCoroutine(spawnEnemy());

				}
				else if (numEnemy == 0)
				{
					// enables the wave spawner
					waveSpawn = true;
					//GetComponent<Renderer>().enabled = true;
					//module.enabled = true;
					//increase the number of waves
					numWaves++;
					transform.localPosition = new Vector3(Random.Range(-6, 6), Random.Range(-4, 3.5f), 0);
					Debug.Log("no more enemy begin anew");
					spawnedEnemy = 0;

				}
				if (spawnedEnemy >= totalEnemy)
				{
					// disables the wave spawner
					waveSpawn = false;
					//GetComponent<Renderer>().enabled = false;
					//module.enabled = false;
					//Debug.Log("more enemy then allowed: turing off");

				}
            }
            else
            {
				deleteSpawner();
			}
		}
	}
	// spawns an enemy based on the enemy level that you selected
	private IEnumerator spawnEnemy()
	{

		numEnemy++;
		spawnedEnemy++;
		Spawn = false;
		yield return new WaitForSeconds(frequency);

		//Random.Range(1, 6);
		int enemyChoosen = Random.Range(0, Enemies.Count);
		GameObject Enemy = (GameObject)Instantiate(Enemies[enemyChoosen], gameObject.transform.position, Quaternion.identity);
		Enemy.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * 375);
		Enemy.SendMessage("setName", SpawnID);

		ParticleSystem PartSystem = SpawnExplosion.GetComponent<ParticleSystem>();
		ParticleSystem.MainModule ma = PartSystem.main;
		//ma.startColor = getColorPrimary();
		GameObject e = Instantiate(SpawnExplosion, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
		


		/*ParticleSystem.EmissionModule module = PartSystem.emission;
		module.enabled = true;*/
		//PartSystem.Play();
		//ParticleSystem.MainModule ma = PartSystem.main;
		/*Debug.Log(Enemy.GetComponent("Enemy"));
		Enemy asdf = Enemy.GetComponent("Enemy");
		asdf.getColorPrimary();
		Debug.Log(asdf.getColorPrimary());*/
		//ma.startColor = Enemies[rand].getPrimaryColor();

		//GameObject e = Instantiate(DeathExplosion, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
		// Increase the total number of enemies spawned and the number of spawned enemies
		

		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f))); ;

		//PartSystem.emission = true;
		//ma.emission = true;
		//Debug.Log(frequency);
		Spawn = true;
		/*Spawn = true;
		module.enabled = false;*/
		//PartSystem.Stop();

		//ma.enableEmission = false;



	}
	// Call this function from the enemy when it "dies" to remove an enemy count
	public void killEnemy(int sID)
	{
		// if the enemy's spawnId is equal to this spawnersID then remove an enemy count
		if (SpawnID == sID)
		{
			numEnemy--;
			Debug.Log("numEnemy = " + numEnemy);
		}
	}
	//enable the spawner based on spawnerID
	public void enableSpawner(int sID)
	{
		if (SpawnID == sID)
		{
			Spawn = true;
		}
	}
	//disable the spawner based on spawnerID
	public void disableSpawner(int sID)
	{
		if (SpawnID == sID)
		{
			Spawn = false;
		}
	}

	// Enable the spawner, useful for trigger events because you don't know the spawner's ID.
	public void enableTrigger()
	{
		Spawn = true;
	}

	private IEnumerator deleteSpawner()
    {
		yield return new WaitForSeconds(frequency);
		Destroy(gameObject);
	}
}