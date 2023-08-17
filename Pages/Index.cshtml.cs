using System.Data.SqlTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Proyecto_Web_Aplicativo.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        try{
            String connectionString= "Data Source=.\\sqlexpress;Initial Catalog=mystore";

            using (SqlConnection connection= new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "SELECT * FROM clients";
                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            ClientInfo clientInfo = new ClientInfo();
                            clientInfo.id = "" + reader.GetInt32(0);
                            clientInfo.name=  reader.GetString(1);
                            clientInfo.email= reader.GetString(2);
                            clientInfo.phone = reader.GetString(3);
                            clientInfo.address = reader.GetString(4);
                            clientInfo.created_at = reader.GetDameTime(5).ToString();
                            listClients.Add(clientInfo);
                        }
                    }
                }

            }

        }catch(Exception ex){
            Console.WriteLine("Exception: "+ ex.ToString());
        }
    } 
}

public class ClientInfo
{
    public String id;
    public String name;
    public String email;
    public String phone;
    public String address;



}
