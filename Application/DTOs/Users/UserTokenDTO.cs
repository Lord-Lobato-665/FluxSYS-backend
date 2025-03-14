public class UserTokenDto
{
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public UserDto User { get; set; }
}

public class UserDto
{
    public int Id_user { get; set; }
    public string Name_user { get; set; }
    public string Mail_user { get; set; }
    public long Phone_user { get; set; }
    public RoleDto Role { get; set; }
    public PositionDto Position { get; set; }
    public DepartmentDto Department { get; set; }
    public CompanyDto Company { get; set; }
}

public class RoleDto
{
    public int Id_role { get; set; }
    public string Name_role { get; set; }
}

public class PositionDto
{
    public int Id_position { get; set; }
    public string Name_position { get; set; }
}

public class DepartmentDto
{
    public int Id_department { get; set; }
    public string Name_department { get; set; }
}

public class CompanyDto
{
    public int Id_company { get; set; }
    public string Name_company { get; set; }
}