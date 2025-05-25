using System;
using Entities;
using UINavigation;
using UI;

namespace UIComponents;

// TODO xml
public static class RestaurantIO
{
    /// <summary>
    /// Calculates the average rating for a given <see cref="Restaurant"/> based on
    /// all <see cref="RestaurantReview.Rating"/>s for the <see cref="Restaurant"/>.
    /// </summary>
    /// <param name="restaurant"> The <see cref="Restaurant"/> to find the average rating for. </param>
    /// <returns> The average rating of a <see cref="Restaurant"/>, based on <see cref="RestaurantReview"/>s.
    /// If no reviews exist, returns <c>0</c>. </returns>
    public static double GetAverageRestaurantRating(Restaurant restaurant)
    {
        int totalRating = 0;
        int reviewsCount = 0;

        if (OrderRegistry.TryGetOrders(out List<CustomerOrder> customerOrders, restaurant))
        {
            foreach (CustomerOrder order in customerOrders)
            {
                if (order.Restaurant == restaurant && order.RestaurantReview != null)
                {
                    totalRating += order.RestaurantReview.Rating;
                    reviewsCount++;
                }
            }
        }
        return reviewsCount > 0 ? Math.Round((double)totalRating / reviewsCount, 1) : 0;
    }
}
