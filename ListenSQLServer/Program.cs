using System.Data.SqlClient;

string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Research;Integrated Security=True";
SqlDependency.Start(connectionString);
RegisterOnChangeEvent(connectionString);
Console.ReadLine();

static void RegisterOnChangeEvent(string connectionString)
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();
    using var command = new SqlCommand("SELECT [Name] FROM dbo.Users WHERE [Id]=9", connection);
    var dependency = new SqlDependency(command);
    dependency.OnChange += Dependency_OnChange;
    var reader = command.ExecuteReader();
}

static void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
{
    string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Research;Integrated Security=True";
    Console.WriteLine(e.Info);
    Console.WriteLine(e.Source);
    Console.WriteLine(e.Type);

    if (sender is SqlDependency dependency)
    {
        dependency.OnChange -= Dependency_OnChange;
        RegisterOnChangeEvent(connectionString) ;
    }
}