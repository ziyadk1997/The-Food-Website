// *********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
// *********************************************************

namespace AuthManager
{
    using System;

    public class User
    {
        public User(Guid id)
        {
            this.Id = id;
        }
        
        public User(Guid id, string displayName, string email, string firstName = null, string lastName = null)
        {
            this.Id = id;
            this.DisplayName = displayName;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Guid Id { get; private set; }

        public string DisplayName { get;  private set; }

        public string Email { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }
    }
}
