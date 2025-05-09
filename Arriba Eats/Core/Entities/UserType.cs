using System;

namespace Entities;

/// <summary>
/// Defines the various types of users within the application.
/// </para> Used to associate a user instance with their appropriate type. </para>
/// </summary>
public enum UserType
{
    /// <summary>
    /// Represents a standard customer who places orders from restaurants.
    /// </summary>
    Customer,

    /// <summary>
    /// Represents a delivery person who transports orders from restaurants to the customers.
    /// </summary>
    Deliverer,

    /// <summary>
    /// Represents a restaurant owner or manager responsible for cooking orders and handing 
    /// them to the delivery person. 
    /// </summary>
    Client
}