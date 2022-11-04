using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DBManager : MonoBehaviour
{
    IDbConnection dbConnection;

    private int numTotalIngredients = 0;

    public GameObject IngredientButton;
    public Transform IngredientGrid;

    public class InterfazProperties
    {
        public Text Ingredient;
        public Text Balance;
        

    }

    public List<InterfazProperties> Interfaz;

    private void OpenDatabase()
    {
        string dbUri = "URI=file:alchENTImistDB.db";
        dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();
    }

    private void UpdateUI()
    {

    }

    
    private void GetNumIngredients()
    {

        string query = "SELECT COUNT (*) FROM ingredients";

        IDbCommand cmd = dbConnection.CreateCommand();
        IDataReader datareader = cmd.ExecuteReader();
        while (datareader.Read())
        {
            numTotalIngredients = datareader.GetInt32(0);
        }
    }




    private void Start()
    {
        OpenDatabase();
        GetNumIngredients();
        string query = "SELECT * FROM ingredients";

        
        
        
        
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;
        IDataReader datareader = cmd.ExecuteReader();
        while (datareader.Read())
        {
            for (int i = 0; i < numTotalIngredients; i++)
            {
                GameObject NewIngredient = Instantiate(IngredientButton, IngredientGrid);
                NewIngredient.transform.GetChild(0).GetComponent<Text>().text = datareader.GetString(1);
            }
        }





    }

}

