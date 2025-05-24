using System;

namespace Entities;

/// <summary>
/// Holds the review of a <see cref="Restaurant"/>'s order from a <see cref="Customer"/>.
/// The <see cref="Customer"/>, rating, and comment are stored.
/// <para> Intended to be referenced by a <see cref="CustomerOrder"/>. </para>
/// </summary>
public class RestaurantReview
{
    /// <summary>
    /// The rating given by the <see cref="Customer"/>, numbered between 1 and 5, inclusive.
    /// </summary>
    public int Rating { get; private set; }

    /// <summary>
    /// The comment given by the <see cref="Customer"/>.
    /// </summary>
    public string Comment { get; private set; }

    /// <summary>
    /// Initialises a new instance of the <see cref="RestaurantReview"/> class, representing
    /// a <see cref="Customer"/>'s review and rating of an order from a <see cref="Restaurant."/>
    /// </summary>
    /// <param name="rating"></param>
    /// <param name="comment"></param>
    public RestaurantReview(int rating, string comment)
    {
        Rating = rating;
        Comment = comment;
    }
}