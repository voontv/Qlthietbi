using System;
using System.Collections.Generic;

namespace QlThietBi.Models;

public partial class User
{
    public string UserName { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string? PhoneNumber { get; set; }
}
