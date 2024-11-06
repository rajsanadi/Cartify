// src/components/Orders/OrderItemForm.js
import React, { useState, useEffect } from 'react';
import { getAllProducts } from '../../services/productService';

const OrderItemForm = ({ orderItems, onChange }) => {
  const [items, setItems] = useState(orderItems || []);
  const [products, setProducts] = useState([]);

  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    try {
      const response = await getAllProducts();
      setProducts(response.data);
    } catch (error) {
      console.error(error);
      alert('Failed to fetch products.');
    }
  };

  const handleItemChange = (index, field, value) => {
    const updatedItems = [...items];
    updatedItems[index][field] = value;
    setItems(updatedItems);
    onChange(updatedItems);
  };

  const handleAddItem = () => {
    setItems([...items, { productId: '', quantity: '', unitPrice: '' }]);
    onChange([...items, { productId: '', quantity: '', unitPrice: '' }]);
  };

  const handleRemoveItem = (index) => {
    const updatedItems = items.filter((_, i) => i !== index);
    setItems(updatedItems);
    onChange(updatedItems);
  };

  return (
    <div>
      <h3>Order Items</h3>
      {items.map((item, index) => (
        <div key={index} style={{ border: '1px solid #ccc', padding: '10px', marginBottom: '10px' }}>
          <div>
            <label>Product:</label>
            <select
              value={item.productId}
              onChange={(e) => handleItemChange(index, 'productId', e.target.value)}
              required
            >
              <option value="">Select Product</option>
              {products.map((prod) => (
                <option key={prod.productId} value={prod.productId}>
                  {prod.productName}
                </option>
              ))}
            </select>
          </div>
          <div>
            <label>Quantity:</label>
            <input
              type="number"
              value={item.quantity}
              onChange={(e) => handleItemChange(index, 'quantity', e.target.value)}
              required
            />
          </div>
          <div>
            <label>Unit Price:</label>
            <input
              type="number"
              value={item.unitPrice}
              onChange={(e) => handleItemChange(index, 'unitPrice', e.target.value)}
              required
            />
          </div>
          <button type="button" onClick={() => handleRemoveItem(index)}>
            Remove
          </button>
        </div>
      ))}
      <button type="button" onClick={handleAddItem}>
        Add Order Item
      </button>
    </div>
  );
};

export default OrderItemForm;
