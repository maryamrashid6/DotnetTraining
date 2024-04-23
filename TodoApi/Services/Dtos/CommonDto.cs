namespace TodoApi.Services.Dtos
{
    public class CommonDto
    {
        public class BaseDto
        {
            public required string Title { get; set; }
            public string? Description { get; set; }
        }

    }
}
