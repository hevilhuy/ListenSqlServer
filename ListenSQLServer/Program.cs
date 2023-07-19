using System.Data.SqlClient;

string connectionString = "Data Source=VNNOT02172\\SQLEXPRESS;Initial Catalog=Research;Integrated Security=True";
SqlDependency.Start(connectionString);
using var connection = new SqlConnection(connectionString);
connection.Open();
using var command = new SqlCommand("SELECT * FROM [Users]", connection);
var dependency = new SqlDependency(command);
dependency.OnChange += Dependency_OnChange;
var reader = command.ExecuteReader();
Console.ReadLine();

static void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
{
    Console.WriteLine(e.Info);
    Console.WriteLine(e.Source);
    Console.WriteLine(e.Type);
}