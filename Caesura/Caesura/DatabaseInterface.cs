using System;
using System.Collections.Generic;


namespace Caesura
{
    public interface DatabaseInterface
    {
        List<User> users { get; set; }
        User getUser(String username);
        bool registerUser(User toRegister);

    }
}