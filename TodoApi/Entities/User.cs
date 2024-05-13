namespace TodoApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        //A User can be assigned multiple Tasks
        public ICollection<ToDo>? ToDos { get; set; }
        public User()
        {
            ToDos = new List<ToDo>();
        }

    }
}
