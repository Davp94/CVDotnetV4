using System;

namespace ComprasVentas.Models;

public class UserDevices
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string ip { get; set; }

    public string so { get; set; }

    public bool isActive { get; set; }

}
