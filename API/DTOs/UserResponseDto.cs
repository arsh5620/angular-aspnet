﻿namespace API;

public class UserResponseDto
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public int Age { get; set; }
    public string? PhotoUrl { get; set; }
    public List<PhotoDto>? Photos { get; set; }
}

public class PhotoDto
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public bool IsMain { get; set; }
}