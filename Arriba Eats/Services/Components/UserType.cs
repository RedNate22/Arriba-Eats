using System;

namespace UIComponents;

/// <summary>
/// Defines the various types of users within the application.
/// Used to associate a user instance with their appropriate type.
/// </summary>
public enum UserType
{
    /// <summary>
    /// Represents the user state before authentication.
    /// Used as the default value when no user is logged in.
    /// </summary>
    Default,

    /// <summary>
    /// Represents a standard <see cref="Entities.Customer"/> who places orders from restaurants.
    /// </summary>
    Customer,

    /// <summary>
    /// Represents a <see cref="Entities.Deliverer"/> who transports orders from restaurants to the customers.
    /// </summary>
    Deliverer,

    /// <summary>
    /// Represents a <see cref="Entities.Restaurant"/> owner or manager responsible for cooking orders and handing 
    /// them to the delivery person. 
    /// </summary>
    Client
}