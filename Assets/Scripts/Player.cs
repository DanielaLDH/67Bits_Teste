using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] Color[] levelUpColors;
    [SerializeField] GameObject playerRender;
    [SerializeField] int stackCapacity;
    [SerializeField] float stackingOffset ;
    [SerializeField] float inertiaFactor ; 
    [SerializeField] List<GameObject> stackedEnemies = new List<GameObject>();

    public float money;
    public float lvlCost;

    private Rigidbody rb;
    private Vector3 MoveInput;
    private Vector3 MoveVelocity;


    private void FixedUpdate()
    {
        UpdateStackEnemyPositions();
    }


    void Punch(GameObject enemy)
    {
        if (stackedEnemies.Count < stackCapacity && !stackedEnemies.Contains(enemy) )
        {
            stackedEnemies.Add(enemy);
            enemy.GetComponent<Rigidbody>().isKinematic = true;
        }
        

    }

    void UpdateStackEnemyPositions()
    {
        for(int i = 0; i < stackedEnemies.Count; i++)
        {
            
            Vector3 targetPosition = transform.position - transform.forward * 2f + Vector3.up * (i * stackingOffset);
            float adjestedInertiaFactor = inertiaFactor / (i + 1);
            stackedEnemies[i].transform.position = Vector3.Lerp(stackedEnemies[i].transform.position, targetPosition, Time.fixedDeltaTime * adjestedInertiaFactor);
            stackedEnemies[i].transform.rotation = Quaternion.Lerp(stackedEnemies[i].transform.rotation, Quaternion.Euler(0, transform.eulerAngles.y, 90), Time.fixedDeltaTime * adjestedInertiaFactor);
           
        }
    }

    void GetMoney()
    {
        StartCoroutine(DesactivateEnemy());
        
    }

    IEnumerator DesactivateEnemy()
    {
        while (stackedEnemies.Count > 0)
        {
            GameObject enemy = stackedEnemies[0];
            stackedEnemies.RemoveAt(0);
            Destroy(enemy);
            money += 2;
            Debug.Log(money);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void LevelUp(int lvl)
    {
        money -= lvlCost;
        stackCapacity += 5;

        playerRender.GetComponent<Renderer>().material.color = levelUpColors[lvl -1];

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Punch(other.gameObject);
        }
        if (other.gameObject.tag == "Money")
        {
            GetMoney();
        }
        if(other.gameObject.tag == "Fall")
        {
            SceneManager.LoadScene(0);
        }
    }


}
