using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager Instance;

    private string serverIP = "13.124.31.216";
    private string dbName = "game";
    private string tableName = "users";
    private string rootPasswd = "8508";

    private MySqlConnection conn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        DBConnect();
    }


    public void DBConnect()
    {
        string config = $"server = {serverIP}; port = 3306; Database = {dbName};" + $"uid = root; pwd = {rootPasswd}; charSet = utf8";
        conn = new MySqlConnection(config);
        conn.Open();
    }

    public void LogIn(string id, string pw, Action success, Action failure)
    {
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = $"SELECT * FROM {tableName} WHERE email = '{id}' AND pw = '{pw}'";

        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
        DataSet set = new DataSet();

        adapter.Fill(set);

        bool isLogin = set.Tables[0].Rows.Count > 0 && set.Tables[0].Rows[0]["email"].ToString() == id && set.Tables[0].Rows.Count > 0;

        if (isLogin)
        {
            SceneManager.Instance.LoadScene("Playground");

        }
        else
        {
            failure();
        }
    }
}

