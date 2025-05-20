using System;

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
    /// The collection of items in the order, stored by:
    /// <para> - Key: item name as a string</para>
    /// <para> - Value: (Quantity as an int, price per item as a decimal) </para>
    /// </summary>
    private Dictionary<string, (int Quantity, decimal ItemPrice)> ItemsOrdered
    { get; } = new Dictionary<string, (int Quantity, decimal ItemPrice)>();

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
        if (ItemsOrdered.ContainsKey(itemName))
        {
            // Get tuple associated with itemName
            (int Quantity, decimal ItemPrice) currentItem = ItemsOrdered[itemName];
            // Add to quantity, but keep price the same
            ItemsOrdered[itemName] = (currentItem.Quantity + quantity, currentItem.ItemPrice);
        }

        // Add a new entry when the item hasn't already been added
        else
        {
            ItemsOrdered[itemName] = (quantity, itemPrice);
        }
    }

    /// <summary>
    /// Determines whether the order contains no items.
    /// </summary>
    /// <returns> <c>true</c> if the order is empty, otherwise <c>false</c>. </returns>
    public bool IsOrderEmpty()
    {
        return ItemsOrdered.Count == 0;
    }

    /// <summary>
    /// Allows updating of the <see cref="Entities.OrderStatus"/>.
    /// </summary>
    /// <param name="orderStatus"> The <see cref="Entities.OrderStatus"/> of the current order. </param>
    public void UpdateOrderStatus(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
        // TODO
        // ???
        // Add functionality to prevent order status from being rushed to last stage immediately
    }
}