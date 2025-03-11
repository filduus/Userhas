using System;

[Serializable]
public class Admin : User
{
    public Admin(string username, string password) : base(username, password) { }
}
