using System;
using System.Collections.Generic;

namespace APISolution3;

public partial class MstUser
{
    public int UserId { get; set; }

    public int? CompanyId { get; set; }

    public string? LoginId { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? EmailId { get; set; }

    public string? Role { get; set; }

    public string? Status { get; set; }

    public string? Created { get; set; }

    public string? Modified { get; set; }

    public string? Accessed { get; set; }
}
