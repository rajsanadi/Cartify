// src/components/Products/ProductDetail.js
import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import { getProductById } from '../../services/productService';

const ProductDetail = () => {
  const { id } = useParams();
  const [product, setProduct] = useState(null);

  useEffect(() => {
    fetchProduct();
  }, []);

  const fetchProduct = async () => {
    try {
      const response = await getProductById(id);
      setProduct(response.data);
    } catch (error) {
      console.error(error);
      alert('Failed to fetch product details.');
    }
  };

  if (!product) return <div>Loading...</div>;

  return (
    <div>
      <h2>Product Details</h2>
      <p><strong>Name:</strong> {product.productName}</p>
      <p><strong>Price:</strong> {product.price}</p>
      <p><strong>Stock Quantity:</strong> {product.stockQuantity}</p>
      <p><strong>Category:</strong> {product.categoryName}</p>
      <Link to="/products">Back to Products</Link>
    </div>
  );
};

export default ProductDetail;
