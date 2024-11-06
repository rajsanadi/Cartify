// src/components/Orders/OrderDetail.js
import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import { getOrderById, getProductsByOrderId } from '../../services/orderService';

const OrderDetail = () => {
  const { id } = useParams();
  const [order, setOrder] = useState(null);
  const [products, setProducts] = useState([]);

  useEffect(() => {
    fetchOrder();
    fetchProducts();
  }, []);

  const fetchOrder = async () => {
    try {
      const response = await getOrderById(id);
      setOrder(response.data);
    } catch (error) {
      console.error(error);
      alert('Failed to fetch order details.');
    }
  };

  const fetchProducts = async () => {
    try {
      const response = await getProductsByOrderId(id);
      setProducts(response.data);
    } catch (error) {
      console.error(error);
      alert('Failed to fetch products for this order.');
    }
  };

  if (!order) return <div>Loading...</div>;

  return (
    <div>
      <h2>Order Details</h2>
      <p><strong>Order Date:</strong> {new Date(order.orderDate).toLocaleDateString()}</p>
      <p><strong>Total Amount:</strong> {order.totalAmount}</p>

      <h3>Products in this Order</h3>
      {products.length > 0 ? (
        <table>
          <thead>
            <tr>
              <th>Product Name</th>
              <th>Price</th>
              <th>Quantity</th>
              <th>Unit Price</th>
            </tr>
          </thead>
          <tbody>
            {products.map((prod) => (
              <tr key={prod.productId}>
                <td>{prod.productName}</td>
                <td>{prod.price}</td>
                <td>{prod.quantity}</td>
                <td>{prod.unitPrice}</td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <p>No products found for this order.</p>
      )}
      <Link to="/orders">Back to Orders</Link>
    </div>
  );
};

export default OrderDetail;
