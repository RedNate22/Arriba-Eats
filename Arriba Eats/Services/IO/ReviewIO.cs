using System;
using Entities;
using UI;

namespace UIComponents;

/// <summary>
/// Contains various static methods for I/O associated with <see cref="CustomerOrder"/> reviews.
/// </summary>
public class ReviewIO
{
    // TODO xml
    public static void GetReview(CustomerOrder order, int rating, string comment)
    {
        RestaurantReview restaurantReview = new RestaurantReview(rating, comment);
        order.AddReviewToOrder(restaurantReview);
    }

    // TODO xml
    public static int GetRating()
    {
        while (true)
        {
            string? ratingString = DisplayIO.ReadInput();

            if (int.TryParse(ratingString, out int ratingInt))
            {
                if (ratingInt >= 0 && ratingInt < 6) return ratingInt;
            }

            DisplayIO.DisplayMessage(CustomerConstants.INVALID_QUANTITY_STR);
            continue;
        }
    }

    /// <summary>
    /// Attempts to locate the currently selected <see cref="Restaurant"/>'s <see cref="RestaurantReview"/>s 
    /// by matching any <see cref="CustomerOrder"/>s attached to the restaurant, containing a review.
    /// The reviews are then displayed with the <see cref="Customer.Name"/>, <see cref="RestaurantReview.Rating"/>,
    /// and <see cref="RestaurantReview.Comment"/>.
    /// </summary>
    /// <param name="ordersWithReviews"> </param>
    /// <returns> <c>true</c> if the <see cref="Restaurant"/>'s <see cref="CustomerOrder"/>s,
    /// contain any reviews, otherwise, <c>false</c>. </returns>
    public static bool DisplayRestaurantReviews(List<CustomerOrder> ordersWithReviews)
    {
        bool containsReviews = false;

        if (ordersWithReviews.Count > 1)  // * Sort by rating, descending order
        {
            static int SortByRating(CustomerOrder a, CustomerOrder b)
            {
                return b.RestaurantReview!.Rating.CompareTo(a.RestaurantReview!.Rating);
            }
            ordersWithReviews.Sort(SortByRating);
        }

        foreach (CustomerOrder order in ordersWithReviews)
        {
            if (order.Restaurant == SessionManager.SelectedRestaurant && order.RestaurantReview != null)
            {
                DisplayIO.DisplayMessage(String.Format(CustomerConstants.REVIEW_DETAILS_STR,
                    order.Customer.Name, order.RestaurantReview.RatingInStars, order.RestaurantReview.Comment));

                DisplayIO.DisplayEmptyLine();

                containsReviews = true;
            }
        }
        return containsReviews;
    } 

    /// <summary>
    /// Iterates through the given list of <see cref="CustomerOrder"/>s,
    /// finding any orders marked as <see cref="OrderStatus.Delivered"/> and
    /// there have been no reviews for them yet.
    /// Then displays them dynamically with an indexed number, the order number,
    /// and the <see cref="Restaurant"/>'s name. Then returns the final value of the index.
    /// </summary>
    /// <param name="customerOrders"> The list of <see cref="CustomerOrder"/>s to iterate through. </param>
    /// <returns> The final value of the index. </returns>
    public static int DisplayOrdersReadyToReview(out List<CustomerOrder> foundOrdersToReview, List<CustomerOrder> customerOrders)
    {
        List<CustomerOrder> ordersToReview = new();
        int choiceIndex = 1;

        foreach (CustomerOrder order in customerOrders)
        {
            if (OrderIO.IsDelivered(order.OrderStatus) && order.RestaurantReview == null)
            {
                DisplayIO.DisplayMessage(String.Format(CustomerConstants.ORDER_DETAILS_FOR_REVIEW_STR,
                    choiceIndex, order.OrderNumber, order.Restaurant.RestaurantName));
                ordersToReview.Add(order);
                choiceIndex++;
            }
        }
        foundOrdersToReview = ordersToReview;
        return choiceIndex;
    }

    /// <summary>
    /// Attempts to locate any <see cref="RestaurantReview"/>s for the currently selected
    /// <see cref="Restaurant"/> and assign the <see cref="CustomerOrder"/>s containing them to a list
    /// via the <c>out</c> parameter.
    /// </summary>
    /// <returns> The list of <see cref="CustomerOrder"/>s containing <see cref="RestaurantReview"/>s. </returns>
    public static List<CustomerOrder> GetOrdersWithReviews()
    {
        if (SessionManager.SelectedRestaurant != null)
        {
            if (OrderRegistry.TryGetOrdersWithReviews(SessionManager.SelectedRestaurant, out List<CustomerOrder> ordersWithReviews))
            {
                return ordersWithReviews;
            }
            return new List<CustomerOrder>();
        }
        else return new List<CustomerOrder>();
    }
}
