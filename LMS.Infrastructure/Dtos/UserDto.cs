namespace LMS.Infrastructure.Dtos
{
    public record UserDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        //public string Email { get; init}

        //public Guid? CourseId { get; init; }
        //public CourseDto? Course { get; init; }

        //public string? RefreshToken { get; init; }
        //public DateTime RefreshTokenExpireTime { get; init; }
    }
}
