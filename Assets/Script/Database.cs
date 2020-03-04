using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;


public class Database : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string connStr = "server=175.24.57.128;user=home;database=unityweb;port=3306;password=989600";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Debug.Log("Connecting to MySQL...");
            conn.Open();
            // Perform database operations
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
        conn.Close();
        Debug.Log("Done.");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
