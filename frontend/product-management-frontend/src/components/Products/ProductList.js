// src/components/Products/ProductList.js
import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getAllProducts, deleteProduct } from '../../services/productService';

const ProductList = () => {
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

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this product?')) {
      try {
        await deleteProduct(id);
        fetchProducts();
      } catch (error) {
        console.error(error);
        alert('Failed to delete product.');
      }
    }
  };

  return (
    <div>
      <h2>Products</h2>
      <Link to="/products/new">Add New Product</Link>
      <table>
        <thead>
          <tr>
            <th>Product Name</th>
            <th>Price</th>
            <th>Stock Quantity</th>
            <th>Category</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {products.map((product) => (
            <tr key={product.productId}>
              <td>{product.productName}</td>
              <td>{product.price}</td>
              <td>{product.stockQuantity}</td>
              <td>{product.categoryName}</td>
              <td>
                <Link to={`/products/${product.productId}`}>View</Link>
                <Link to={`/products/edit/${product.productId}`}>Edit</Link>
                <button onClick={() => handleDelete(product.productId)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ProductList;
