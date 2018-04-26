using System.Collections.Generic;
using System.Web.Services;
using DAL;

[WebService(Namespace = "http://silverapples.com/", Description = "<b> SilverApples webbtjänster </b>")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Controller to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class Controller : System.Web.Services.WebService
{
    public Controller () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<string> GetEmployees()
    {
        return CronusDAL.GetEmployees();
    }

    [WebMethod]
    public List<string> GetEmplTables()
    {
        return CronusDAL.GetEmplTables();
    }

    [WebMethod]
    public List<List<string>> GetAllInfo(string tablename)
    {
        return CronusDAL.GetAllInfo(tablename);
    }

    [WebMethod]
    public List<string> GetMetaDataColumnName(string tablename)
    {
        return CronusDAL.GetMetaDataColumnName(tablename);
    }

    [WebMethod]
    public List<List<string>> GetAllKeys()
    {
        return CronusDAL.GetAllKeys();
    }

    [WebMethod]
    public List<List<string>> GetAllIndexes()
    {
        return CronusDAL.GetAllIndexes();
    }

    [WebMethod]
    public List<List<string>> GetAllTableConstraints()
    {
        return CronusDAL.GetAllTableConstraints();
    }

    [WebMethod]
    public List<List<string>> GetAllTablesOne()
    {
        return CronusDAL.GetAllTablesOne();
    }

    [WebMethod]
    public List<List<string>> GetAllTablesTwo()
    {
        return CronusDAL.GetAllTablesTwo();
    }

    [WebMethod]
    public List<List<string>> GetAllEmployeeColumnsOne()
    {
        return CronusDAL.GetAllEmployeeColumnsOne();
    }

    [WebMethod]
    public List<List<string>> GetAllEmployeeColumnsTwo()
    {
        return CronusDAL.GetAllEmployeeColumnsTwo();
    }

    [WebMethod]
    public List<List<string>> GetEmplRelatives(string no)
    {
        return CronusDAL.GetEmplRelatives(no);
    }

    [WebMethod]
    public List<List<string>> GetSick2004()
    {
        return CronusDAL.GetSick2004();
    }

    [WebMethod]
    public List<List<string>> GetFirstNameTopSick()
    {
        return CronusDAL.GetFirstNameTopSick();
    }

    [WebMethod]
    public void CreateEmplContract(string code, string description)
    {
        CronusDAL.CreateEmplContract(code, description);
    }

    [WebMethod]
    public List<string> GetEmplContract(string code)
    {
        return CronusDAL.GetEmplContract(code);
    }

    [WebMethod]
    public void UpdateEmplContract(string code, string description)
    {
        CronusDAL.UpdateEmplContract(code, description);
    }

    [WebMethod]
    public void DeleteEmplContract(string code)
    {
        CronusDAL.DeleteEmplContract(code);
    }

    [WebMethod]
    public string Search(string fileName)
    {
        return CronusDAL.Search(fileName);
    }

    [WebMethod]
    public List<string> GetCustomer(string cPnr)
    {
        return CronusDAL.GetCustomer(cPnr);
    }

    [WebMethod]
    public List<List<string>> GetCustomers()
    {
        return CronusDAL.GetCustomers();
    }
}