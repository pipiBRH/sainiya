using UnityEngine;  
using System;  
using System.Collections;  
using System.Data;  
using MySql.Data.MySqlClient;  

public class CMySql : MonoBehaviour {  
	public static MySqlConnection dbConnection;//Just like MyConn.conn in StoryTools before  
	static string host = "db.mis.kuas.edu.tw";  
	static string id = "s1100137105";  //***不要变***
	static string pwd = "aa107858";  //密码
	static string database = "s1100137105";//数据库名  
	static string result = "";  
	public string[] nam=new string[5],ti=new string[5],sco=new string[5];
	private string strCommand = "Select ID from Scor ;";  
	public static DataSet MyObj;
	public bool save=false,load=false;
	public menu mu;
	public string test;

	
	/*void OnGUI()  
	{  
		host = GUILayout.TextField( host, 200, GUILayout.Width(200));  
		id = GUILayout.TextField( id, 200, GUILayout.Width(200));  
		pwd = GUILayout.TextField( pwd, 200, GUILayout.Width(200));

		if(GUILayout.Button("Test"))  
		{  

			//读取数据函数
			ReaderData();
			
		}   
		GUILayout.Label(result);  
	}*/ 
	
	// On quit  
	void Start(){
	}
	void Update () {
	test="00:"+mu.game.m/10+""+mu.game.m%10+":"+mu.game.sc/10+""+mu.game.sc%10;
		if (!save) {
			if(mu.cofig){
				save=true;
				ReaderData();
			}
		}
	}
	public static void OnApplicationQuit() 
	{  
		closeSqlConnection();  
	}  
	
	// Connect to database  
	private static void openSqlConnection(string connectionString) 
	{  
		dbConnection = new MySqlConnection(connectionString);  
		dbConnection.Open();  
		result = dbConnection.ServerVersion;  //获得MySql的版本
	}  
	
	// Disconnect from database  
	private static void closeSqlConnection() 
	{  
		dbConnection.Close();  
		dbConnection = null;  
	}  
	
	// MySQL Query  
	public static void doQuery(string sqlQuery) 
	{  
		IDbCommand dbCommand = dbConnection.CreateCommand();      
		dbCommand.CommandText = sqlQuery;  
		IDataReader reader = dbCommand.ExecuteReader();  
		reader.Close();  
		reader = null;  
		dbCommand.Dispose();  
		dbCommand = null;  
	}  
	#region Get DataSet  
	public  DataSet GetDataSet(string sqlString)  
	{   
		DataSet ds = new DataSet();  
		try  
		{  
			MySqlDataAdapter da = new MySqlDataAdapter(sqlString, dbConnection);  
			da.Fill(ds);  
			
		}  
		catch (Exception ee)  
		{       
			throw new Exception("SQL:" + sqlString + "\n" + ee.Message.ToString());  
		}  
		return ds;  
		
	}  
	#endregion   
	
	//读取数据函数
	void ReaderData()
	{	
		string connectionString = string.Format("Server = {0}; Database = {1}; User ID = {2}; Password = {3};",host,database,id,pwd);  
		openSqlConnection(connectionString);    
		MyObj = GetDataSet(strCommand);
		if (save) {
			MySqlCommand mySqlCommand = new MySqlCommand ("INSERT INTO `s1100137105`.`Scor` (`Id`, `Name`, `Timer`, `Sco`) VALUES (NULL, '"+mu.na+"', '"+mu.game.h/10+""+mu.game.h%10+":"+mu.game.m/10+""+mu.game.m%10+":"+mu.game.sc/10+""+mu.game.sc%10+"', '"+mu.game.scor+"');", dbConnection);
			
			MySqlDataReader reader = mySqlCommand.ExecuteReader ();

				reader.Close ();
				}
		if (!load) {
						MySqlCommand mySqlCommand = new MySqlCommand ("SELECT * FROM `Scor` ORDER BY `Scor`.`Sco` DESC;", dbConnection);

						MySqlDataReader reader = mySqlCommand.ExecuteReader ();
						try {
								int i = 0;

								while (reader.Read()) {
										if (reader.HasRows && i < 5) {
												nam [i] = reader.GetString (1);
												ti [i] = reader.GetString (2);
												ti [i] = ti [i].Substring (3, 5);
												sco [i] = reader.GetString (3);
												i++;
										}
								}
						} catch (Exception) {
								Console.WriteLine ("查询失败了！");
						} finally {
				reader.Close ();
				load=true;
						}
		}
	}
}  
