using System;
using UIComponents;

namespace Entities;

/// <summary>
/// Holds the details of a confirmed order from a <see cref="Entities.Customer"/> for any <see cref="Entities.Restaurant"/>.
/// The item names, price per unit, and quantities are all stored, as well as the attached <see cref="Entities.Customer"/>, <see cref="Entities.Deliverer"/>
/// and <see cref="Entities.RestaurantReview"/> for quick reference. 
/// </summary>
public class CustomerOrder
{
    /// <summary>
    /// Get the <see cref="Entities.Customer"/> associated with the order. 
    /// </summary>
    public Customer Customer { get; private set; }

    /// <summary>
    /// The unique order number assigned to the order when confirmed.
    /// </summary>
    public int OrderNumber { get; private set; }

    /// <summary>
    /// Get the <see cref="Entities.Restaurant"/> the order was placed from.
    /// </summary>
    public Restaurant Restaurant { get; private set; }

    /// <summary>
    /// Track the current status of the order.
    /// </summary>
    public OrderStatus OrderStatus { get; private set; }

    /// <summary>
    /// Get the <see cref="Entities.RestaurantReview"/> associated with this order.
    /// </summary>
    public RestaurantReview? RestaurantReview { get; private set; }

    /// <summary>
    /// Get the <see cref="Entities.Deliverer"/> who has been assigned to deliver the order.
    /// </summary>
    public Deliverer? Deliverer { get; private set; }

    /// <summary>
    /// Tracks whether the <see cref="Entities.Deliverer"/> has arrived at the
    /// <see cref="Entities.Restaurant"/> to pick up the order.
    /// </summary>
    public bool DelivererArrivedAtRestaurant { get; private set; }

    /// <summary>
    /// Gets the total taxicab distance a <see cref="Entities.Deliverer"/> must travel
    /// to pick up and deliver an order.
    /// </summary>
    public int TotalDistance { get; private set; }

    /// <summary>
    /// The collection of items in the order, stored by:
    /// <para> - Key: item name as a string</para>
    /// <para> - Value: (Quantity as an int, price per item as a decimal) </para>
    /// </summary>
    private Dictionary<string, (int Quantity, decimal ItemPrice)> _itemsOrdered
        = new Dictionary<string, (int Quantity, decimal ItemPrice)>();

    /// <summary>
    /// Initialises a new instance of the <see cref="CustomerOrder"/> class, storing the
    /// unique order number.
    /// </summary>
    /// <param name="orderNumber"> The assigned order ID. </param>
    public CustomerOrder(Customer customer, int orderNumber, Restaurant restaurant)
    {
        Customer = customer;
        OrderNumber = orderNumber;
        Restaurant = restaurant;
        OrderStatus = OrderStatus.NotOrdered;
        DelivererArrivedAtRestaurant = false;
    }

    /// <summary>
    /// Adds an item to the current order. If the item has already been added before, its
    /// quantity is increased. If not, a new entry is added to the order.
    /// </summary>
    /// <param name="itemName"> The name of the item being ordered. </param>
    /// <param name="quantity"> The quantity of the item being ordered. </param>
    /// <param name="itemPrice"> The price of the item being ordered, per unit. </param>
    public void AddItemToOrder(string itemName, int quantity, decimal itemPrice)
    {
        // Check if the item already exists in the current order
        if (_itemsOrdered.ContainsKey(itemName))
        {
            // Get tuple associated with itemName
            (int Quantity, decimal ItemPrice) currentItem = _itemsOrdered[itemName];
            // Add to quantity, but keep price the same
            _itemsOrdered[itemName] = (currentItem.Quantity + quantity, currentItem.ItemPrice);
        }

        // Add a new entry when the item hasn't already been added
        else
        {
            _itemsOrdered[itemName] = (quantity, itemPrice);
        }
    }

    /// <summary>
    /// Determines whether the order contains no items.
    /// </summary>
    /// <returns> <c>true</c> if the order is empty, otherwise <c>false</c>. </returns>
    public bool IsOrderEmpty()
    {
        return _itemsOrdered.Count == 0;
    }

    /// <summary>
    /// Progresses the <see cref="Entities.OrderStatus"/> on each call.
    /// <para> Orders go in order as: </para>
    /// <para> - NotOrdered (default) </para>
    /// <para> - Ordered </para>
    /// <para> - Cooking </para>
    /// <para> - Cooked </para>
    /// <para> - BeingDelivered </para>
    /// <para> - Delivered </para>
    /// </summary>
    public void UpdateOrderStatus()
    {
        OrderStatus currentOrderStatus = OrderStatus;

        switch (currentOrderStatus)
        {
            case 0:  // Not ordered
                OrderStatus = OrderStatus.Ordered;
                break;

            case (OrderStatus)1:  // Ordered
                OrderStatus = OrderStatus.Cooking;
                break;

            case (OrderStatus)2:  // Cooking
                OrderStatus = OrderStatus.Cooked;
                break;

            case (OrderStatus)3:  // Cooked
                OrderStatus = OrderStatus.BeingDelivered;
                break;

            case (OrderStatus)4:  // Being Delivered
                OrderStatus = OrderStatus.Delivered;
                break;
        }
    }

    /// <summary>
    /// Displays each item ordered, along with the quantity.
    /// </summary>
    public void DisplayOrderedItems()
    {
        foreach (var item in _itemsOrdered)
        {
            IODisplay.DisplayMessage($"{item.Value.Quantity} x {item.Key}");
        }
    }

    /// <summary>
    /// Gets the total amount spent on the order.
    /// </summary>
    /// <returns> The total amount, in decimal. </returns>
    public decimal GetTotalSpent()
    {
        decimal totalSpent = 0.00M;
        foreach (var item in _itemsOrdered)
        {
            totalSpent += item.Value.Quantity * item.Value.ItemPrice;
        }
        return totalSpent;
    }

    /// <summary>
    /// Assigns a <see cref="Entities.Deliverer"/> to the order.
    /// </summary>
    /// <param name="deliverer"></param>
    public void AssignDeliverer(Deliverer deliverer)
    {
        Deliverer ??= deliverer;
    }

    /// <summary>
    /// Updates the total taxicab distance for the <see cref="Entities.Deliverer"/> to 
    /// reach the <see cref="Entities.Customer"/> with the order.
    /// </summary>
    /// <param name="totalDistance"> The new total taxicab distance. </param>
    public void UpdateTotalDistance(int totalDistance)
    {
        TotalDistance = totalDistance;
    }

    /// <summary>
    /// Updates the order details to mark the <see cref="Entities.Deliverer"/> has arrived at the 
    /// <see cref="Entities.Restaurant"/>.
    /// </summary>
    public void DelivererAtRestaurant()
    {
        DelivererArrivedAtRestaurant = true;
    }

    /// <summary>
    /// Attaches a <see cref="Entities.RestaurantReview"/> the <see cref="CustomerOrder"/>.
    /// </summary>
    /// <param name="review"> The <see cref="Entities.RestaurantReview"/> to add. </param>
    public void AddReviewToOrder(RestaurantReview review)
    {
        RestaurantReview = review;
    }
}