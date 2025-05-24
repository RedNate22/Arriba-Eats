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
    /// The rating given by the <see cref="Customer"/>, displayed in a range of stars, <c>*</c>,
    /// one for each numbered rating. E.g., a <see cref="Rating"/> of 3 becomes <c>***</c>.
    /// </summary>
    public string? RatingInStars { get; private set; }

    /// <summary>
    /// The comment given by the <see cref="Customer"/>.
    /// </summary>
    public string Comment { get; private set; }

    /// <summary>
    /// Initialises a new instance of the <see cref="RestaurantReview"/> class, representing
    /// a <see cref="Customer"/>'s review and rating of an order from a <see cref="Restaurant."/>.
    /// <para> The rating is also converted to asterisks, <c>*</c>, denoting stars. </para>
    /// </summary>
    /// <param name="rating"> The integar rating for the <see cref="Restaurant"/>, numbered 
    /// between 1 to 5 (inclusive. </param>
    /// <param name="comment"> The string comment for the <see cref="RestaurantReview"/>, of
    /// any length. </param>
    public RestaurantReview(int rating, string comment)
    {
        Rating = rating;
        Comment = comment;

        string oneStars = "*";
        string twoStars = "**";
        string threeStars = "***";
        string fourStars = "****";
        string fiveStars = "*****";

        switch (rating)
        {
            case 1:
                RatingInStars = oneStars;
                break;

            case 2:
                RatingInStars = twoStars;
                break;
            
            case 3:
                RatingInStars = threeStars;
                break;
            
            case 4:
                RatingInStars = fourStars;
                break;
            
            case 5:
                RatingInStars = fiveStars;
                break;
        }
    }
}