namespace Firebase_API.Models
{
    public class HobbyModel
    {
        public string? Id { get; set; }
        public string NameHobby { get; set; }
        public string DescriptionHobby { get; set; }

        public HobbyModel() { }

        //Construtor com parâmetros
        public HobbyModel(string nameHobby, string descriptionHobby)
        {
            NameHobby = nameHobby;
            DescriptionHobby = descriptionHobby;
        }
    }
}
