﻿namespace ToDoList.Core.Exceptions;

public class InvalidRoleException : CustomException
{
    public string Role { get; }

    public InvalidRoleException(string role) : base($"Role {role} is invald")
    {
        Role = role;
    }
}