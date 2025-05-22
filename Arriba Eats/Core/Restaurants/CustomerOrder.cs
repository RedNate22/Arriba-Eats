using System;
using UIComponents;

namespace Entities;

/// <summary>
/// Holds the details of a confirmed order from a <see cref="Customer"/> for any <see cref="Restaurant"/>.
/// The item names, price per unit, and quantities are all stored. 
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
    /// Tracks whether the <see cref="Entities.Deliverer"/> has arrived to pick up the order.
    /// </summary>
    public bool DelivererArrived { get; private set; }

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
        DelivererArrived = false;
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
    /// <param name="orderStatus"> The <see cref="Entities.OrderStatus"/> of the current order. </param>
    public void UpdateOrderStatus()
    {
        OrderStatus currentOrderStatus = OrderStatus;

        switch (currentOrderStatus)
        {   
            case (OrderStatus) 0:  // * Marked as not ordered by default.
                OrderStatus = OrderStatus.Ordered;
                break;

            case (OrderStatus) 1:
                OrderStatus = OrderStatus.Cooking;
                break;

            case (OrderStatus) 2:
                OrderStatus = OrderStatus.Cooked;
                break;

            case (OrderStatus) 3:
                OrderStatus = OrderStatus.BeingDelivered;
                break;

            case (OrderStatus) 4:
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
}