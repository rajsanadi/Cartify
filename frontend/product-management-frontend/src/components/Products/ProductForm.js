// src/components/Products/ProductForm.js
import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { addProduct, getProductById, updateProduct } from '../../services/productService';
import { getCategories } from '../../services/categoryService';

const ProductForm = () => {
  const { id } = useParams();
  const isEdit = Boolean(id);
  const navigate = useNavigate();

  const [product, setProduct] = useState({
    productName: '',
    price: '',
    stockQuantity: '',
    categoryId: '',
    isAvailable: true,
  });

  const [categories, setCategories] = useState([]);

  useEffect(() => {
    fetchCategories();
    if (isEdit) {
      fetchProduct();
    }
  }, []);

  //Usage
  const fetchCategories = async () => {
    try {
      const categories = await getCategories();
      setCategories(categories); // Assuming response.data is already the categories array
    } catch (error) {
      console.error(error);
      alert('Failed to fetch categories.');
    }
  };
  

  const fetchProduct = async () => {
    try {
      const response = await getProductById(id);
      setProduct({
        productName: response.data.productName,
        price: response.data.price,
        stockQuantity: response.data.stockQuantity,
        categoryId: response.data.categoryId,
        isAvailable: response.data.isAvailable,
      });
    } catch (error) {
      console.error(error);
      alert('Failed to fetch product details.');
    }
  };

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setProduct((prev) => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (isEdit) {
        await updateProduct(id, product);
        alert('Product updated successfully.');
      } else {
        await addProduct(product);
        alert('Product added successfully.');
      }
      navigate('/products');
    } catch (error) {
      console.error(error);
      alert('Failed to save product.');
    }
  };

  return (
    <div>
      <h2>{isEdit ? 'Edit Product' : 'Add New Product'}</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Product Name:</label>
          <input
            type="text"
            name="productName"
            value={product.productName}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label>Price:</label>
          <input
            type="number"
            name="price"
            value={product.price}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label>Stock Quantity:</label>
          <input
            type="number"
            name="stockQuantity"
            value={product.stockQuantity}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label>Category:</label>
          <select name="categoryId" value={product.categoryId} onChange={handleChange} required>
            <option value="">Select Category</option>
            {categories.map((cat) => (
              <option key={cat.id} value={cat.id}>
                {cat.categoryName}
              </option>
            ))}
          </select>
        </div>
        <div>
          <label>Available:</label>
          <input
            type="checkbox"
            name="isAvailable"
            checked={product.isAvailable}
            onChange={handleChange}
          />
        </div>
        <button type="submit">{isEdit ? 'Update' : 'Add'} Product</button>
      </form>
    </div>
  );
};

export default ProductForm;
