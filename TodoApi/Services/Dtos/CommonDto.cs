namespace TodoApi.Services.Dtos
{
    public class CommonDto
    {
        public class BaseDto
        {
            public required string Title { get; set; }
            public string? Description { get; set; }
        }

        // pagination
        public class PaginationDto
        {
            public int? PageNo { get; set; }
            public int? PageSize { get; set; }
        }

    }
}
