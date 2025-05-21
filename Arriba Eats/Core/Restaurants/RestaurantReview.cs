using System;

namespace Entities;

/// <summary>
/// Holds the review of a <see cref="Customer"/>'s order from a <see cref="Restaurant"/>.
/// The <see cref="Customer"/>, rating, and comment are stored.
/// </summary>
public class RestaurantReview
{
    /// <summary>
    /// Get the <see cref="CustomerOrder"/> associated with this order.
    /// </summary>
    public CustomerOrder CustomerOrder { get; private set; }

    /// <summary>
    /// The <see cref="Customer"/> who created the review.
    /// </summary>
    public Customer Reviewer { get; private set; }

    /// <summary>
    /// The rating given by the <see cref="Reviewer"/>, numbered between 1 and 5, inclusive.
    /// </summary>
    public int Rating { get; private set; }

    /// <summary>
    /// The comment given by the <see cref="Reviewer"/>.
    /// </summary>
    public string Comment { get; private set; }

    /// <summary>
    /// Initialises a new instance of the <see cref="RestaurantReview"/> class, representing
    /// a <see cref="Customer"/>'s review of an order from a <see cref="Restaurant"/>.
    /// </summary>
    /// <param name="reviewer"> The <see cref="Customer"/> who is reviewing the order. </param>
    /// <param name="rating"> The rating given by the <see cref="Customer"/>, between 1 and 5, inclusive. </param>
    /// <param name="comment"> The comment for the review given by the <see cref="Customer"/>. </param>
    public RestaurantReview(CustomerOrder customerOrder, Customer reviewer, int rating, string comment)
    {
        CustomerOrder = customerOrder;
        Reviewer = reviewer;
        Rating = rating;
        Comment = comment;
    }
}