﻿namespace AuthService.Controller;

public class UpdatePasswordRequest
{
    public string New { get; set; }
    public string NewConfirm { get; set; }
    public string Current { get; set; }
}