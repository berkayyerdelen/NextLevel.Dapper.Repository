namespace NextLevel.Dapper.Repository.Service
{
    public class Commands
    {
        public string InsertCommand { get; set; } = "INSERT INTO";
        public string DeleteCommand { get; set; } = "DELETE * FROM";
        public string SelectCommand { get; set; } = "Select  ";
    }
}